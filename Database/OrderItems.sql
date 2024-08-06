CREATE TABLE OrderItems (
    [orderItemId]               INT NOT NULL IDENTITY(1,1),
    [orderId]                   INT NOT NULL,
    [productId]                 INT NOT NULL,
    [quantity]                  INT NOT NULL,
    [price]                     DECIMAL(18, 2),
    FOREIGN KEY (orderId)       REFERENCES Orders(orderId),
    FOREIGN KEY (productId)     REFERENCES Products(productId),
    CONSTRAINT PK_ORDER_ITEMS   PRIMARY KEY(orderItemId)
);