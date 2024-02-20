CREATE PROCEDURE [dbo].[spPolicy_Update]
    @Id INT,
    @PolicyNumber NVARCHAR(15),
    @PolicyAmount DECIMAL(10, 2),
    @PartnerId INT
AS
BEGIN
    UPDATE [Policy] SET 
        PolicyNumber = ISNULL(@PolicyNumber, PolicyNumber),
        PolicyAmount = @PolicyAmount,
        PartnerId = @PartnerId
    WHERE Id = @Id;
END;
