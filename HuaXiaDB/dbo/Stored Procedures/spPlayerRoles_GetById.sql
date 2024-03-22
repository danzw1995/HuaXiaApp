CREATE PROCEDURE [dbo].[spPlayerRoles_GetById]
	@id int
AS

begin 
	set nocount on;
	select * from dbo.PlayerRoles where Id = @id;
end
