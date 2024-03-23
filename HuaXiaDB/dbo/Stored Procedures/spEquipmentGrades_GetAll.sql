CREATE PROCEDURE [dbo].[spEquipmentGrades_GetAll]

AS

begin 
	set  nocount on;
	select [Id], [Grade] from dbo.EquipmentGrades
end;
