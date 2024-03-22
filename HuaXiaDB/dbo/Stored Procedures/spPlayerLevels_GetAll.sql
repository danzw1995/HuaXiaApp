CREATE PROCEDURE [dbo].[spPlayerLevels_GetAll]

AS

begin
	set nocount on;

	select [Id], [Level] from dbo.PlayerLevels;
end;
