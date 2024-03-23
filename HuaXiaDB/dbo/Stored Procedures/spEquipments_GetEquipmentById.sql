CREATE PROCEDURE [dbo].[spEquipments_GetEquipmentById]
@id int

AS

begin
	set nocount on;

	select  [e].[Id], [e].[Name], [e].[Description], [e].[Image], [e].[EquipmentPartId], [e].[PlayerLevelId], [e].[PlayerRoleId], [e].[EquipmentGradeId], 
	[pl].[Level],
	[pr].[Name] as PlayerRoleName,
	[ep].[Name] as EquipmentPartName, 
	[eg].[Grade] from dbo.Equipments e 
	left join dbo.EquipmentParts ep on e.EquipmentPartId = ep.Id
	left join dbo.PlayerLevels pl on e.PlayerLevelId = pl.Id
	left join dbo.PlayerRoles  pr on e.PlayerRoleId = pr.Id
	left join dbo.EquipmentGrades eg on e.EquipmentGradeId = eg.Id
	
	where e.Id = @id;
end

