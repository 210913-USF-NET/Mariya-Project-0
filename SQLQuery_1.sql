
DROP TABLE Customer
CREATE TABLE Customer
(
CustomerId INT NOT NULL IDENTITY(1000,1000),
CustomerName NVARCHAR (20) NOT NULL,
CustomerUserName NVARCHAR (20) NOT NULL,
CustomerPassWord NVARCHAR (20) NOT NULL,
CustomerEmail NVARCHAR (40) NOT NULL,
CustomerAddress NVARCHAR (MAX) NULL,
CustomerStore INT NOT NULL,
CONSTRAINT Acc_Store PRIMARY KEY (CustomerId, CustomerStore)
) 
DROP TABLE Product
CREATE TABLE Product
(
ProductId INT PRIMARY KEY NOT NULL IDENTITY (10,10),
ProductName NVARCHAR (MAX) NOT NULL,
ProductPrice SMALLMONEY NOT NULL,
ProductGenere NVARCHAR (MAX) NULL,
ProductDescription NVARCHAR (MAX) NULL,
)
DROP TABLE StoreFront
CREATE TABLE StoreFront
(
StoreId INT PRIMARY KEY IDENTITY(100,100)NOT NULL,
StoreName NVARCHAR (30) NOT NULL,
StoreAddress NVARCHAR (60) NOT NULL,
)
DROP TABLE Inventory
CREATE TABLE Inventory
(
--InventoryID INT PRIMARY KEY IDENTITY(4,4)NOT NULL,
InvenStoreID INT NOT NULL,
InvenProductID INT NOT NULL,
InventoryQuantity INT NOT NULL,
CONSTRAINT InvenStoreID FOREIGN KEY (InvenStoreID) REFERENCES StoreFront(StoreId),
CONSTRAINT InvenProductID FOREIGN KEY (InvenProductID) REFERENCES Product(ProductId),
CONSTRAINT PKInventory_ID PRIMARY KEY (InvenStoreID, InvenProductID)
)
DROP TABLE Orders
CREATE TABLE Orders
(
OrderID INT PRIMARY KEY IDENTITY(1010,1010)NOT NULL,
OrderAccountId INT NOT NULL,
OrderStoreID INT NOT NULL,
--OrderInvenID INT NOT NULL,
OrderTotal SMALLMONEY NOT NULL,
OrderDate DATETIME NOT NULL DEFAULT(GETDATE()),
--CONSTRAINT OrderAccountId FOREIGN KEY (OrderAccountId) REFERENCES Customer(CustomerId)
CONSTRAINT FKAcc_Store FOREIGN KEY (OrderAccountId, OrderStoreID) REFERENCES Customer(CustomerId, CustomerStore),
--CONSTRAINT OrderInvenID FOREIGN KEY (OrderInvenID) REFERENCES Inventory(InventoryID),
)
DROP TABLE LineItem

CREATE TABLE LineItem
(
--LineItemID INT PRIMARY KEY IDENTITY(2,2)NOT NULL,
LineOrderID INT NOT NULL,
LineStoreID INT NOT NULL,
LineInvenProdID INT NOT NULL,
OrderProductQantity INT NULL,
CONSTRAINT LineOrderID  FOREIGN KEY (LineOrderID) REFERENCES Orders(OrderID),
CONSTRAINT FKInventory_ID FOREIGN KEY (LineStoreID, LineInvenProdID)  REFERENCES Inventory (InvenStoreID, InvenProductID),
--CONSTRAINT PKLine_ID PRIMARY KEY (InvenStoreID, InvenProductID)
CONSTRAINT PKLineITem_ID PRIMARY KEY (LineOrderID, LineStoreID, LineInvenProdID)
)

INSERT INTO StoreFront ( StoreName, StoreAddress)
VALUES ('The Little Read Book','5008 Main St. Narnia IT');
INSERT INTO StoreFront (StoreName, StoreAddress)
VALUES ('Batmans Best Books','380 S San Rafael Ave. Gotham NJ');
INSERT INTO StoreFront (StoreName, StoreAddress)
VALUES ('Hobbit Hall','1 Quenya Ln. Mordor ME' );

--customer info
INSERT INTO Customer (CustomerName, CustomerUserName, CustomerPassWord, CustomerEmail, CustomerAddress, CustomerStore) 
VALUES ('James Barns','SummerChild', 'password1', 'winters@stark.org', '147 Main St. New York NY',300);
INSERT INTO Customer (CustomerName, CustomerUserName, CustomerPassWord, CustomerEmail, CustomerAddress, CustomerStore) 
VALUES ('Steve Grant', 'LoveNotWar', 'password2','cameric@stark.org', '12-4 James Ct. Brooklyn NY', 100);
INSERT INTO Customer (CustomerName, CustomerUserName, CustomerPassWord, CustomerEmail, CustomerAddress, CustomerStore) 
VALUES('Natasha Romanov', 'ItsyBitsy', 'password3','spider@stark.org', '1838 Lubyanka Ln Odessa MO',300);
INSERT INTO Customer (CustomerName, CustomerUserName, CustomerPassWord, CustomerEmail, CustomerAddress, CustomerStore)
VALUES('Tony Stark','MeanMachine', 'password4', 'thestark@stark.org', '1 Stak St. New York NY', 200);
INSERT INTO Customer (CustomerName, CustomerUserName, CustomerPassWord, CustomerEmail, CustomerAddress, CustomerStore)
VALUES('Clint Barton', 'CantMiss', 'password5','hawk@stark.org', '357 Quincy St. Brooklyn NY', 200);

--SELECT * FROM Store ORDER BY StoreProductId;
--SELECT * from AccountStoreLocation;
----SELECT * from MembersAccount;
---ProductInfo

INSERT INTO Product (ProductName, ProductPrice, ProductGenere, ProductDescription)--10
VALUES ('The History Of Middle Earth Box Collection', 355.00, 'History', 'Full collection hardcover');
INSERT INTO Product (ProductName, ProductPrice, ProductGenere, ProductDescription)--20
VALUES ('The Art Of War', 50.00, 'Military', 'Could be dangerous');
INSERT INTO Product (ProductName, ProductPrice, ProductGenere, ProductDescription) --30
VALUES ('Do Androids Dream of Electric Sheep? ', 15.00, 'Sci-Fi', 'Dystopian science fiction novel ');
INSERT INTO Product (ProductName, ProductPrice, ProductGenere, ProductDescription)--40
VALUES ('Shakespeare for Squirrels', 20.00, 'Therapy',  'Marooned pirate becomes a court jester');
UPDATE Inventory 
SET 
    InventoryQuantity = 20
WHERE
    InvenStoreID = 100 and InvenProductID = 20
INSERT INTO Inventory(InvenStoreID,InvenProductID, InventoryQuantity)
VALUES (100,20,10);
INSERT INTO Inventory(InvenStoreID,InvenProductID, InventoryQuantity)
VALUES (100,20,10);
INSERT INTO Inventory(InvenStoreID,InvenProductID, InventoryQuantity)
VALUES (100,40,10);
INSERT INTO Inventory(InvenStoreID,InvenProductID, InventoryQuantity)
VALUES (100,10,10);
INSERT INTO Inventory(InvenStoreID,InvenProductID, InventoryQuantity)
VALUES (100,30,10);
INSERT INTO Inventory(InvenStoreID,InvenProductID, InventoryQuantity)
VALUES (200,10,20);
INSERT INTO Inventory(InvenStoreID,InvenProductID, InventoryQuantity)
VALUES (200,20,20);
INSERT INTO Inventory(InvenStoreID,InvenProductID, InventoryQuantity)
VALUES (200,30,20);
INSERT INTO Inventory(InvenStoreID,InvenProductID, InventoryQuantity)
VALUES (200,40,20);
INSERT INTO Inventory(InvenStoreID,InvenProductID, InventoryQuantity)
VALUES (300,10,30);
INSERT INTO Inventory(InvenStoreID,InvenProductID, InventoryQuantity)
VALUES (300,20,30);
INSERT INTO Inventory(InvenStoreID,InvenProductID, InventoryQuantity)
VALUES (300,30,30);
INSERT INTO Inventory(InvenStoreID,InvenProductID, InventoryQuantity)
VALUES (300,40,30);