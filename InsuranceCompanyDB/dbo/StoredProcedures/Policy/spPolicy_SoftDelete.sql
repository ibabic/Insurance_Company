CREATE PROCEDURE [dbo].[spPolicy_SoftDelete]
	@Id INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO PolicyArchive (Id, PolicyNumber, PolicyAmount, PartnerId)
        SELECT Id, PolicyNumber, PolicyAmount, PartnerId
        FROM Policy
        WHERE Id = @Id;

        DELETE FROM Policy
        WHERE Id = @Id;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
