CREATE PROCEDURE [dbo].[spEquipments_SearchByRoleAndEquipmentPart]
	@roleId int,
	@equipmentPartId int
AS
begin
	set nocount on;

	select [e].[Id], [e].[Name], [e].[Description], [e].[Image], [e].[EquipmentPartId], [e].[PlayerLevelId], [e].[PlayerRoleId], [e].[EquipmentGradeId], 
	[pl].[Level],
	[pr].[Name] as PlayerRoleName,
	[ep].[Name] as EquipmentPartName,
	[eg].[Grade]
	from dbo.Equipments e
	inner join dbo.PlayerLevels pl on e.PlayerLevelId = pl.Id
	inner join dbo.PlayerRoles pr on e.PlayerRoleId = pr.Id
	inner join dbo.EquipmentParts ep on e.EquipmentPartId = ep.Id
	inner join dbo.EquipmentGrades eg on e.EquipmentGradeId = eg.Id
	where (@roleId is null or  e.PlayerRoleId = @roleId) and (@equipmentPartId is null or e.EquipmentPartId = @equipmentPartId);

end