CREATE PROCEDURE [dbo].[spPolicy_Insert]
    @PolicyNumber NVARCHAR(15),
    @PolicyAmount DECIMAL(10, 2),
    @PartnerId INT
AS
BEGIN
    INSERT INTO [Policy] (PolicyNumber, PolicyAmount, PartnerId)
    VALUES (@PolicyNumber, @PolicyAmount, @PartnerId);
END;