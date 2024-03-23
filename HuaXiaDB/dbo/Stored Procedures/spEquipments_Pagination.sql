CREATE PROCEDURE [dbo].[spEquipments_Pagination]
	@roleId int,
	@equipmentPartId int,
	@equipmentGradeId int,
	@pageIndex int,
	@pageSize int

AS
begin
	set nocount on;

	select [e].[Id], [e].[Name], [e].[Description], [e].[Image], [e].[EquipmentPartId], [e].[PlayerLevelId], [e].[PlayerRoleId], 
	[pl].[Level],
	[pr].[Name] as PlayerRoleName,
	[ep].[Name] as EquipmentPartName,
	[eg].[Grade]
	from dbo.Equipments e
	inner join dbo.PlayerLevels pl on e.PlayerLevelId = pl.Id
	inner join dbo.PlayerRoles pr on e.PlayerRoleId = pr.Id
	inner join dbo.EquipmentParts ep on e.EquipmentPartId = ep.Id
	inner join dbo.EquipmentGrades eg on e.EquipmentGradeId = eg.Id
	where (@roleId is null or  e.PlayerRoleId = @roleId) and  (@equipmentGradeId is null or  e.EquipmentGradeId = @equipmentGradeId) and (@equipmentPartId is null or e.EquipmentPartId = @equipmentPartId)
	order by [e].[Id]
	offset (@pageIndex - 1) * @pageSize rows
	fetch next
	@pageSize rows only;

end