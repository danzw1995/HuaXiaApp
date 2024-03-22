CREATE PROCEDURE [dbo].[spEquipments_Pagination]
	@roleId int,
	@equipmentPartId int,
	@pageIndex int,
	@pageSize int

AS
begin
	set nocount on;

	select [e].[Id], [e].[Name], [e].[Description], [e].[Image], [e].[EquipmentPartId], [e].[PlayerLevelId], [e].[PlayerRoleId], 
	[pl].[Level],
	[pr].[Name] as PlayerRoleName,
	[ep].[Name] as EquipmentPartName
	from dbo.Equipments e
	inner join dbo.PlayerLevels pl on e.PlayerLevelId = pl.Id
	inner join dbo.PlayerRoles pr on e.PlayerRoleId = pr.Id
	inner join dbo.EquipmentParts ep on e.EquipmentPartId = ep.Id
	where (@roleId is null or  e.PlayerRoleId = @roleId) and (@equipmentPartId is null or e.EquipmentPartId = @equipmentPartId)
	order by [e].[Id]
	offset (@pageIndex - 1) * @pageSize rows
	fetch next
	@pageSize rows only;

end