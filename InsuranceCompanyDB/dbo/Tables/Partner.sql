CREATE TABLE [Partner] (
    Id INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(255) NOT NULL CHECK (LEN(FirstName) >= 2 AND LEN(FirstName) <= 255),
    LastName NVARCHAR(255) NOT NULL CHECK (LEN(LastName) >= 2 AND LEN(LastName) <= 255),
    [Address] NVARCHAR(255),
    PartnerNumber VARCHAR(20) NOT NULL CHECK (LEN(PartnerNumber) = 20 AND (PartnerNumber NOT LIKE '%[^0-9]%')),
    CroatianPIN NVARCHAR(11),
    PartnerTypeId INT NOT NULL CHECK (PartnerTypeId IN (1, 2)),
    CreatedAtUtc DATETIME DEFAULT GETUTCDATE(),
    CreateByUser NVARCHAR(255) NOT NULL CHECK (CreateByUser LIKE '%@%.%'),
    IsForeign BIT NOT NULL,
    ExternalCode NVARCHAR(20) UNIQUE NOT NULL CHECK (LEN(ExternalCode) >= 10 AND LEN(ExternalCode) <= 20),
    Gender CHAR(1) NOT NULL CHECK (Gender IN ('M', 'F', 'N'))
);
