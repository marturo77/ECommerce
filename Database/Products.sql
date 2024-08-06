CREATE TABLE Products (
    [productId]                   INT             IDENTITY(1,1),
    [name]                        NVARCHAR(255)   NOT NULL,
    [description]                 NVARCHAR(MAX)   NOT NULL,
    [price]                       DECIMAL(18,2)   NOT NULL,
    [category]                    NVARCHAR(100)   NOT NULL,
    [stock]                       INT             NOT NULL,
    CONSTRAINT PK_PRODUCTS PRIMARY KEY(productId)
);