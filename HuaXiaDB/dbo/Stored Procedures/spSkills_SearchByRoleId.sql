CREATE PROCEDURE [dbo].[spSkills_SearchByRoleId]
	@roleId int
AS
begin
	set nocount on;

	select [s].[Id], [s].[Name], [s].[Description], [s].[PlayerLevelId], [s].[PlayerRoleId], [s].[Image],
	[pr].[Name] as RoleName, 
	 [pl].[Level]
	from dbo.Skills s 
	inner join dbo.PlayerRoles pr on s.PlayerRoleId = pr.Id 
	inner join dbo.PlayerLevels pl on s.PlayerLevelId = pl.Id
	where s.PlayerRoleId = @roleId;
end
