CREATE TABLE Orders (
    orderId INT PRIMARY KEY IDENTITY(1,1),
    customerId INT,
    orderDate DATETIME DEFAULT GETDATE(),
    status NVARCHAR(50),
    total DECIMAL(18, 2),
    FOREIGN KEY (customerId) REFERENCES Customers(customerId)
);