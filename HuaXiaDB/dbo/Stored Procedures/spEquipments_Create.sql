CREATE PROCEDURE [dbo].[spEquipments_Create]
	@name nvarchar(50),
	@description nvarchar(2000),
	@equipmentPartId int,
	@playerLevelId int,
	@playerRoleId int,
	@image nvarchar(2000)
AS

begin
	set nocount on;

	insert dbo.Equipments(Name, Description, EquipmentPartId, PlayerLevelId, PlayerRoleId, Image)
	values(@name, @description, @equipmentPartId, @playerLevelId, @playerRoleId, @image)
end