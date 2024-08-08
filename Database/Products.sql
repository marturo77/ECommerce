/*
 Listado de productos, para escenarios masivos puede recomendarse consultar la informacion de forma paginada
 o segmentando los productos por la categoria por ejemplo. Tablas de particionamiento pueden ayudar al desempeño o
 establecer indices cluster para acelerar las consultas.

 El el programa se opta por otras opciones como la carga de la lista de productos en cache en memoria para acelerar 
 este proceso para efectos de la prueba tecnica.
*/
CREATE TABLE Products (
    [productId]                   INT             IDENTITY(1,1),
    [name]                        NVARCHAR(255)   NOT NULL,
    [description]                 NVARCHAR(MAX)   NOT NULL,
    [price]                       DECIMAL(18,2)   NOT NULL,
    [category]                    NVARCHAR(100)   NOT NULL,
    [stock]                       INT             NOT NULL,
    CONSTRAINT PK_PRODUCTS PRIMARY KEY(productId)
);