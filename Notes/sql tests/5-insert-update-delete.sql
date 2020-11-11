-- SQL commands are divided in a few categories/sublanguages.
-- Data Manipulation Language (DML)
--   SELECT, INSERT, UPDATE, DELETE, TRUNCATE
--  these commands all operate on rows of tables.

-- Data Definition Language
--   CREATE, ALTER, DROP
--  these commands operate on other objects, like entire tables, or functions, views, etc.

-- Data Control Language (DCL) manages permissions/users/auth
--     GRANT, REVOKE

-- rest of DML besides SELECT is for adding/changing/removing rows

-- INSERT

SELECT * FROM Genre;

INSERT INTO Genre VALUES (100, 'Miscellaneous');

INSERT INTO Genre (GenreId) VALUES (101);

INSERT INTO Genre (GenreId, Name) VALUES (102, 'Misc 2');

SELECT * FROM Genre WHERE GenreId >= 100;

INSERT INTO Genre (GenreId, Name) VALUES
	(103, 'Misc 3'),
	(104, 'Misc 4');

INSERT INTO Genre (GenreId, Name)
	SELECT GenreId + 10, Name + '!'
	FROM Genre
	WHERE GenreId = 104;

-- INSERT can also do things like read CSV files etc

-- UPDATE

-- without a WHERE, would modify every row
UPDATE Genre
SET Name = 'Misc 1'  -- , otherthing = value
WHERE GenreId = 101;

-- without a WHERE, would delete every row (one by one)
DELETE FROM Genre
WHERE GenreId >= 100;

-- this command deletes all rows all at once
--TRUNCATE TABLE Genre;

-- exercises

-- 1. insert two new records into the employee table.
SELECT * FROM Employee
INSERT INTO Employee VALUES
(9, 'Johnson', 'John', 'IT Staff', 6, '1973-01-09 00:00:00.000', SYSDATETIME(), '123 Test Drive', 'Miami', 'FL', 'USA', '23452', '+1(777)123-4567', '+1(777)123-4567', 'test@test1.com');

INSERT INTO Employee VALUES
(10, 'Johnson', 'John', 'IT Staff', 6, '1973-01-09 00:00:00.000', SYSDATETIME(), '123 Test Drive', 'Miami', 'FL', 'USA', '23452', '+1(777)123-4567', '+1(777)123-4567', 'test@test1.com');

-- 2. insert two new records into the tracks table.
SELECT * FROM Track
SELECT * FROM Album
INSERT INTO Track VALUES
(3504, 'Test Song 1', 348, 1, 1, 'Test Composer', 2345232, 3242234, .99);

INSERT INTO Track VALUES
(3505, 'Test Song 2', 348, 1, 1, 'Test Composer 2', 53225235, 65445645, .99);

-- 3. update customer Aaron Mitchell's name to Robert Walter
UPDATE Customer
SET FirstName = 'Robert',
	LastName = 'Walter'
WHERE FirstName = 'Aaron' AND LastName = 'Mitchell';

-- 4. delete one of the employees you inserted.
DELETE FROM Employee
WHERE EmployeeId = 10;

-- 5. delete customer Robert Walter.
-- delete invoice lists and invoice that reference robert first
DELETE FROM Customer
WHERE FirstName = 'Robert' AND LastName = 'Walter';
