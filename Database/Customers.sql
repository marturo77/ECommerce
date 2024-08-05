CREATE TABLE Customers (
    customerId INT PRIMARY KEY IDENTITY(1,1),
    firstName NVARCHAR(100),
    lastName NVARCHAR(100),
    email NVARCHAR(255),
    phoneNumber NVARCHAR(50)
);