CREATE PROCEDURE [dbo].[spEquipmentParts_GetAll]

AS

begin
	set nocount on;
	select [Id], [Name] from dbo.EquipmentParts;
end;
