CREATE PROCEDURE [dbo].[spPartner_Update]
    @Id INT,
    @FirstName NVARCHAR(255),
    @LastName NVARCHAR(255),
    @Address NVARCHAR(255),
    @PartnerNumber VARCHAR(20),
    @CroatianPIN NVARCHAR(11) = NULL,
    @PartnerTypeId INT,
    @CreateByUser NVARCHAR(255),
    @IsForeign BIT,
    @ExternalCode NVARCHAR(20),
    @Gender CHAR(1)
AS
BEGIN
    UPDATE dbo.[Partner]
    SET 
        FirstName = ISNULL(@FirstName, FirstName),
        LastName = ISNULL(@LastName, LastName),
        Address = ISNULL(@Address, Address),
        PartnerNumber = @PartnerNumber,
        CroatianPIN = ISNULL(@CroatianPIN, CroatianPIN),
        PartnerTypeId = @PartnerTypeId,
        CreateByUser = ISNULL(@CreateByUser, CreateByUser),
        IsForeign = @IsForeign,
        ExternalCode = ISNULL(@ExternalCode, ExternalCode),
        Gender = @Gender
    WHERE 
        Id = @Id;
END;