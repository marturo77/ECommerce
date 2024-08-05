CREATE TABLE OrderItems (
    orderItemId INT PRIMARY KEY IDENTITY(1,1),
    orderId INT,
    productId INT,
    quantity INT,
    price DECIMAL(18, 2),
    FOREIGN KEY (orderId) REFERENCES Orders(orderId),
    FOREIGN KEY (productId) REFERENCES Products(productId)
);