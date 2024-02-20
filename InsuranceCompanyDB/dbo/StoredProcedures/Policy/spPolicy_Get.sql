CREATE PROCEDURE [dbo].[spPolicy_Get]
    @Id INT
AS
BEGIN
    SELECT * FROM [Policy] WHERE Id = @Id;
END;
