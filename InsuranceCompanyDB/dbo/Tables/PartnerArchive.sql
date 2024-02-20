CREATE TABLE PartnerArchive (
    Id INT PRIMARY KEY,
    FirstName NVARCHAR(255),
    LastName NVARCHAR(255),
    [Address] NVARCHAR(255),
    PartnerNumber VARCHAR(20),
    CroatianPIN NVARCHAR(11),
    PartnerTypeId INT,
    CreatedAtUtc DATETIME,
    CreateByUser NVARCHAR(255),
    IsForeign BIT,
    ExternalCode NVARCHAR(20),
    Gender CHAR(1),
    DeletedAtUtc DATETIME DEFAULT GETUTCDATE()
);
