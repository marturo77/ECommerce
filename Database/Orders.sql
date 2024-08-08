
/* Es un modelo de datos simple para escenarios masivos deberia considerarse una des-normalizacion 
de la informacion, de forma que se particionen los datos por cliente, o la fecha
Escenarios de datawarehouse pueden colaborar para determinar mas eficientemente como guardar estos
datos que pueden ser considerados como historicos. La desicion final dependera de los requerimientos concretos
*/
CREATE TABLE Orders (
    [orderId]             INT                   NOT NULL IDENTITY(1,1),
    [customerName]        VARCHAR(255)          NOT NULL,
    [orderDate]           DATETIME              NOT NULL DEFAULT GETDATE(),
    [status]              NVARCHAR(50)          NOT NULL,
    [total]               DECIMAL(18, 2)        NOT NULL, 
    CONSTRAINT PK_ORDERS PRIMARY KEY([orderId])
);