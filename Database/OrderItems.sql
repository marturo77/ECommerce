
/*
 Listado de ordenes, se hizo simple con una llave foranea a productos, en un escenario empresarial, donde
 la tabla de ordenes sea historica seguramente se necesitara un manejo diferente, des-normalizado, indices optimizados etc.
*/
CREATE TABLE OrderItems (
    [orderItemId]               INT NOT NULL IDENTITY(1,1),
    [orderId]                   INT NOT NULL,
    [productId]                 INT NOT NULL,
    [quantity]                  INT NOT NULL,
    [price]                     DECIMAL(18, 2),
    CONSTRAINT FK_ORDERITEMS01 FOREIGN KEY (orderId)     REFERENCES Orders(orderId)      ON DELETE CASCADE,
    CONSTRAINT FK_ORDERITEMS02 FOREIGN KEY (productId)   REFERENCES Products(productId)  ON DELETE NO ACTION,
    CONSTRAINT PK_ORDER_ITEMS   PRIMARY KEY(orderItemId)
);