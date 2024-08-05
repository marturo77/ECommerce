CREATE TABLE Products (
    productId INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(255),
    description NVARCHAR(MAX),
    price DECIMAL(18, 2),
    category NVARCHAR(100),
    stock INT
);