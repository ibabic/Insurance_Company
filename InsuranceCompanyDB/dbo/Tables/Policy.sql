CREATE TABLE [Policy] (
    Id INT PRIMARY KEY IDENTITY(1,1),
    PolicyNumber NVARCHAR(15) NOT NULL CHECK (LEN(PolicyNumber) >= 10 AND LEN(PolicyNumber) <= 15),
    PolicyAmount DECIMAL(18, 2) NOT NULL,
    PartnerId INT FOREIGN KEY REFERENCES [Partner](Id)
);
