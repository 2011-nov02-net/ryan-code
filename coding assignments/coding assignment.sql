--Ryan Towner
Create SCHEMA Assign;
GO

--Create products table
CREATE TABLE Assign.Products (
	ID INT NOT NULL PRIMARY KEY IDENTITY,
	Name NVARCHAR(150) NOT NULL,
	Price MONEY NOT NULL
);

--Create customers table
CREATE TABLE Assign.Customers (
	ID INT NOT NULL PRIMARY KEY IDENTITY,
	FirstName NVARCHAR(150) NOT NULL,
	LastName NVARCHAR(150) NOT NULL,
	CardNumber NVARCHAR(16) NOT NULL
);

--Create products table
CREATE TABLE Assign.Orders (
	ID INT NOT NULL IDENTITY,
	ProductID INT NOT NULL FOREIGN KEY REFERENCES Assign.Products (ID),
	CustomerID INT NOT NULL FOREIGN KEY REFERENCES Assign.Customers (ID),
);

-- Add 3 records to each table
INSERT INTO Assign.Products (Name, Price) VALUES
	('Super Coffee', 15.99),
	('Duper Coffee', 16.99),
	('Ultra Coffee', 19.99);

INSERT INTO Assign.Customers (FirstName, LastName, CardNumber) VALUES
	('Bob', 'Bobson', '1234123412341243'),
	('Andy', 'Anderson', '4212235264564564'),
	('John', 'Johnson', '1234345567673344');

INSERT INTO Assign.Orders (ProductID, CustomerID) VALUES
	(1, 1),
	(2, 2),
	(3, 3);

-- Add iphone priced at 200
INSERT INTO Assign.Products (Name, Price) VALUES
	('Iphone', 200);

-- Add tina smith
INSERT INTO Assign.Customers(FirstName, LastName, CardNumber) VALUES
	('Tina', 'Smith', '2342324523341243');

--Create tina smith order for an iphone
INSERT INTO Assign.Orders (ProductID, CustomerID) VALUES
	(4, 4); --both would have id of 4

--get all tina smith orders
SELECT * FROM Assign.Orders AS o
WHERE o.CustomerID IN (
	SELECT c.ID FROM Assign.Customers AS c
	WHERE c.FirstName = 'Tina' AND c.LastName = 'Smith'
);

--get all revenue from iphone
SELECT SUM(p.Price) AS "TOTAL IPHONE REVENUE" FROM Assign.Orders AS o
	INNER JOIN Assign.Products AS p ON o.ProductID = p.ID
WHERE p.Name = 'Iphone'
GROUP BY p.Price

--increase iphone price to 250
UPDATE Assign.Products
SET price = 250
WHERE Name = 'Iphone';