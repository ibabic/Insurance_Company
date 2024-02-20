CREATE PROCEDURE [dbo].[spPartner_Insert]
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
begin
    INSERT INTO dbo.[Partner] (FirstName, LastName, [Address], PartnerNumber, CroatianPIN, PartnerTypeId, CreatedAtUtc, CreateByUser, IsForeign, ExternalCode, Gender)
    VALUES (@FirstName, @LastName, @Address, @PartnerNumber, @CroatianPIN, @PartnerTypeId, GETUTCDATE(), @CreateByUser, @IsForeign, @ExternalCode, @Gender);
end;
