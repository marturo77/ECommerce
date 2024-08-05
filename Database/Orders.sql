CREATE TABLE Orders (
    orderId INT PRIMARY KEY IDENTITY(1,1),
    customerName varchar(255) not null,
    orderDate DATETIME DEFAULT GETDATE(),
    status NVARCHAR(50),
    total DECIMAL(18, 2),    
);