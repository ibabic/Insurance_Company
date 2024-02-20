CREATE PROCEDURE [dbo].[spPolicy_Delete]
    @Id INT
AS
BEGIN
    DELETE FROM [Policy] WHERE Id = @Id;
END;