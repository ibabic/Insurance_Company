CREATE PROCEDURE [dbo].[spPartner_SoftDelete]
	 @Id INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO PolicyArchive (Id, PolicyNumber, PolicyAmount, PartnerId)
        SELECT Id, PolicyNumber, PolicyAmount, PartnerId
        FROM Policy
        WHERE PartnerId = @Id;

        DELETE FROM Policy
        WHERE PartnerId = @Id;

        INSERT INTO PartnerArchive (Id, FirstName, LastName, [Address], PartnerNumber, CroatianPIN, PartnerTypeId, CreatedAtUtc, CreateByUser, IsForeign, ExternalCode, Gender)
        SELECT Id, FirstName, LastName, [Address], PartnerNumber, CroatianPIN, PartnerTypeId, CreatedAtUtc, CreateByUser, IsForeign, ExternalCode, Gender
        FROM Partner
        WHERE Id = @Id;

        DELETE FROM Partner
        WHERE Id = @Id;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH

        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;
    END CATCH;
END;
