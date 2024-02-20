CREATE PROCEDURE [dbo].[spPartner_Delete]
	@Id int
AS
begin
	delete 
	from dbo.[Partner]
	where Id = @Id;
end 
