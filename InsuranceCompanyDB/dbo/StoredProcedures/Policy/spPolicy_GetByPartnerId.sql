CREATE PROCEDURE [dbo].[spPolicy_GetByPartnerId]
    @PartnerId INT
AS
BEGIN
    SELECT * FROM [Policy] WHERE PartnerId = @PartnerId;
END;
