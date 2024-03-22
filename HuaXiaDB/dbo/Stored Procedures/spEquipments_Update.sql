CREATE PROCEDURE [dbo].[spEquipments_Update]
	@id int,
	@name nvarchar(50),
	@description nvarchar(2000),
	@equipmentPartId int,
	@playerLevelId int,
	@playerRoleId int,
	@image nvarchar(2000)

AS

begin
	set nocount on;

	update dbo.Equipments 
	set Name = @name,
	Description = @description,
	EquipmentPartId = @equipmentPartId,
	PlayerLevelId = @playerLevelId,
	PlayerRoleId = @playerRoleId,
	Image = @image
	where Id = @id;
end
