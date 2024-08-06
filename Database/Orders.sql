CREATE TABLE Orders (
    [orderId]             INT                   NOT NULL IDENTITY(1,1),
    [customerName]        VARCHAR(255)          NOT NULL,
    [orderDate]           DATETIME              NOT NULL DEFAULT GETDATE(),
    [status]              NVARCHAR(50)          NOT NULL,
    [total]               DECIMAL(18, 2)        NOT NULL, 
    CONSTRAINT PK_ORDERS PRIMARY KEY([orderId])
);