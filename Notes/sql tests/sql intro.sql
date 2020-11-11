SELECT 'Hello World';
SELECT 2 + 2;
SELECT SYSDATETIME();


-- SELECT (COLUMNS) FROM TABLE;
SELECT * FROM dbo.Customer;
SELECT * FROM dbo.Employee;
SELECT * FROM dbo.Invoice;

SELECT FirstName, LastName FROM dbo.Customer;
SELECT FirstName + ' ' + LastName FROM dbo.Customer;

-- Column Alias : Names a column
SELECT FirstName + ' ' + LastName AS "Full Name" FROM dbo.Customer;

-- Filtering
SELECT * FROM dbo.Invoice WHERE CustomerId = '4';
SELECT * FROM dbo.Invoice WHERE CustomerId = '35';
SELECT * FROM Customer WHERE LEN(FirstName) > 5;
SELECT * FROM Customer WHERE Country = 'Brazil';

--First name starts with B
SELECT * FROM Customer WHERE FirstName >= 'B' AND FirstName < 'C';

--Aggregate Functions : SUM, MIN, MAX, AVG, COUNT
--By default aggregate all rows into 1 result row
SELECT COUNT(*) FROM Customer;
SELECT SUM(Total) FROM Invoice;

--Group by
--Group into more than 1 result row
SELECT * FROM Invoice GROUP BY CustomerId; -- doesnt work
SELECT CustomerID, SUM(Total) FROM Invoice GROUP BY CustomerId;
--where
SELECT CustomerID, SUM(Total) FROM Invoice WHERE SUM(Total) > 40 GROUP BY CustomerId; --doesnt work use having : where applys before group by
--having
SELECT CustomerID, SUM(Total) FROM Invoice GROUP BY CustomerId HAVING SUM(Total) > 40;
--Order By
SELECT CustomerID, SUM(Total) FROM Invoice GROUP BY CustomerId HAVING SUM(Total) > 40 ORDER BY SUM(Total) DESC;
SELECT CustomerID, SUM(Total) FROM Invoice GROUP BY CustomerId HAVING SUM(Total) > 40 ORDER BY SUM(Total) DESC, CustomerId; -- if tie when order, break it by using CustomerId

--Other
-- SELECT DISTINCT (remove duplicate rows as last step)
-- SELECT TOP(N) (discards rows after first N)(Only gets first N rows)

--Order of Execution for SELECT

-- FROM
-- WHERE
-- GROUP BY
-- HAVING
-- SELECT
-- ORDER BY

--SELECT WITH JOINS FOR MULTIPLE TABLES
SELECT Name, dbo.Track.TrackId, PlaylistId FROM dbo.Track INNER JOIN dbo.PlaylistTrack ON dbo.Track.TrackId = dbo.PlaylistTrack.TrackId WHERE PlaylistId = '1';
SELECT Name, dbo.Track.AlbumId, dbo.Track.UnitPrice FROM dbo.Track INNER JOIN dbo.Album ON  dbo.Track.AlbumId = dbo.Album.AlbumId WHERE ArtistId = 3;


--Excersises:
-- 1. list all customers (full names, customer ID, and country) who are not in the US
SELECT FirstName + ' ' + LastName AS "Full Name", CustomerId, Country FROM Customer WHERE Country != 'USA';

-- 2. list all customers from brazil
SELECT FirstName + ' ' + LastName AS "Full Name", CustomerId, Country FROM Customer WHERE Country = 'Brazil';

-- 3. list all sales agents
SELECT FirstName + ' ' + LastName AS "Full Name", Title, EmployeeId FROM Employee WHERE Title = 'Sales Support Agent';

-- 4. show a list of all countries in billing addresses on invoices.
SELECT DISTINCT BillingCountry FROM Invoice;

-- 5. how many invoices were there in 2009, and what was the sales total for that year?
SELECT COUNT(*) AS "Num of Orders" , SUM(Total) AS "Yearly Sales Total" FROM Invoice WHERE InvoiceDate >= '2009' AND InvoiceDate < '2010';

--    (extra challenge: find the invoice count sales total for every year, using one query)
SELECT YEAR(InvoiceDate) AS "Year", COUNT(*) AS "Num of Orders" , SUM(Total) AS "Yearly Sales Total" FROM Invoice GROUP BY YEAR(InvoiceDate);

-- 6. how many line items were there for invoice #37?
SELECT COUNT(*) AS "Num of Items" FROM InvoiceLine WHERE InvoiceId = 37;

-- 7. how many invoices per country?
SELECT COUNT(*) AS "Num of Invoices", BillingCountry FROM Invoice GROUP BY BillingCountry;

-- 8. show total sales per country, ordered by highest sales first.
SELECT SUM(Total) AS "Total Sales", BillingCountry FROM Invoice GROUP BY BillingCountry ORDER BY SUM(Total) DESC;