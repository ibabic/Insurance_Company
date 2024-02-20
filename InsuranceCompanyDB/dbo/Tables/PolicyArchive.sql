CREATE TABLE [dbo].[PolicyArchive] (
    Id INT PRIMARY KEY,
    PolicyNumber NVARCHAR(15) NOT NULL CHECK (LEN(PolicyNumber) >= 10 AND LEN(PolicyNumber) <= 15),
    PolicyAmount DECIMAL(18, 2) NOT NULL,
    PartnerId INT,
    DeletedAtUtc DATETIME DEFAULT GETUTCDATE()
);
