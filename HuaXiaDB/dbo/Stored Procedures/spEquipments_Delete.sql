CREATE PROCEDURE [dbo].[spEquipments_Delete]
	@id int

AS

begin
	set nocount on;
	delete dbo.Equipments where Id = @id;
end