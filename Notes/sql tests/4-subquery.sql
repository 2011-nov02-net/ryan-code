-- sometimes the most natural way to express a query
-- is with a condition based on another query

-- we have some operators for subqueries --
-- IN, NOT IN, EXISTS, ANY, ALL.

-- bit contrived example of ALL:
-- the artist who made the album with the longest title.
SELECT * FROM Artist WHERE ArtistId = (
    SELECT ArtistId FROM Album WHERE
        LEN(Title) >= ALL(SELECT LEN(Title) FROM Album)
);

-- every track that has never been purchased.
-- subquery version
SELECT * FROM Track WHERE TrackId NOT IN (
    SELECT TrackId FROM InvoiceLine
);

-- there is a syntax called "common table expression" (CTE)
-- it lets you run one query in advance, put it in a temporary variable,
-- and use it in the "main query"
WITH purchased_tracks AS (
    SELECT TrackId FROM InvoiceLine
)
SELECT * FROM Track WHERE TrackId NOT IN (
    SELECT * FROM purchased_tracks
);

-- join version
SELECT Track.*
FROM Track LEFT JOIN InvoiceLine ON Track.TrackId = InvoiceLine.TrackId
WHERE InvoiceLine.InvoiceLineId IS NULL;

-- you can't do "= NULL" in SQL, it will always be false
-- - we have IS NULL and IS NOT NULL

-- set operator version (but we only get the IDs)
SELECT TrackId FROM Track
EXCEPT
SELECT TrackId FROM InvoiceLine;


-- exercise set 3 #2
-- which artists did not record any tracks of the Latin genre?
SELECT *
FROM Artist
WHERE ArtistId NOT IN ( -- all the artists who wrote such an album
	SELECT ArtistId FROM Album WHERE AlbumId IN ( -- all the albums with a latin song
		SELECT AlbumId
		FROM Track           -- all the genres named latin
		WHERE GenreId IN (SELECT GenreId FROM Genre WHERE Name = 'Latin')
	)
);

-- join + set-op version
SELECT * FROM Artist
EXCEPT
SELECT ar.*
FROM Artist AS ar
	INNER JOIN Album AS al ON ar.ArtistId = al.ArtistId
	INNER JOIN Track AS t ON al.AlbumId = t.AlbumId
	INNER JOIN Genre AS g ON t.GenreId = g.GenreId
WHERE g.Name = 'Latin';

-- exercises

-- solve these with a mixture of joins, subqueries, CTE, and set operators.
-- solve at least one of them in two different ways, and see if the execution
-- plan for them is the same, or different.

-- 1. which artists did not make any albums at all?
SELECT *
FROM Artist
WHERE ArtistId NOT IN (
	SELECT ArtistId FROM Album
);

SELECT * FROM Artist
EXCEPT
SELECT ar.*
FROM Artist AS ar
	INNER JOIN Album AS al ON ar.ArtistId = al.ArtistId

-- 2. which artists did not record any tracks of the Latin genre?

-- 3. which video track has the longest length? (use media type table)
WITH videoId AS (
    SELECT MediaTypeId FROM MediaType
	WHERE CHARINDEX('video', Name) > 0
)
SELECT TOP(1) *
FROM Track AS t
WHERE t.MediaTypeId IN (
	SELECT * FROM videoId
)
ORDER BY t.Milliseconds DESC;

-- 4. find the names of the customers who live in the same city as the
--    boss employee (the one who reports to nobody)
SELECT c.FirstName + ' ' + c.LastName AS "Full Name", c.City
FROM Customer AS c 
WHERE c.City IN (
	SELECT e.City
	FROM Employee AS e
	WHERE e.ReportsTo IS NULL
);

-- 5. how many audio tracks were bought by German customers, and what was
--    the total price paid for them?
SELECT COUNT(*) AS "Number of Tracks", SUM(il.UnitPrice) AS "Sales Total"
FROM InvoiceLine as il
WHERE il.InvoiceId IN (
	SELECT i.InvoiceId
	FROM Invoice AS i
	WHERE BillingCountry = 'Germany' AND il.TrackId IN (
		SELECT TrackId FROM Track
		WHERE MediaTypeId IN (
			SELECT MediaTypeId FROM MediaType
			WHERE CHARINDEX('audio', Name) > 0
		)
	)
);

-- 6. list the names and countries of the customers supported by an employee
--    who was hired younger than 35.
SELECT c.FirstName + ' ' + c.LastName AS "Full Name", c.Country
FROM Customer AS c
WHERE c.SupportRepId IN (
	SELECT e.EmployeeId FROM Employee AS e
	WHERE YEAR(e.HireDate) - YEAR(e.BirthDate) <= 35
);
