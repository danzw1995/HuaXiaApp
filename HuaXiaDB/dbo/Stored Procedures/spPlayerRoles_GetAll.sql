CREATE PROCEDURE [dbo].[spPlayerRoles_GetAll]

AS
begin
	set nocount on;
	select [Id], [Name], [Description] from dbo.PlayerRoles;
end

