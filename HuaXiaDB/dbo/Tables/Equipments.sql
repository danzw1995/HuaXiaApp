CREATE TABLE [dbo].[Equipments]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(2000) NOT NULL, 
    [Image] NVARCHAR(2000) NOT NULL, 
    [EquipmentPartId] INT NOT NULL, 
    [PlayerLevelId] INT NOT NULL, 
    [PlayerRoleId] INT NOT NULL, 
    CONSTRAINT [FK_Equipments_EquipmentParts] FOREIGN KEY ([EquipmentPartId]) REFERENCES [EquipmentParts]([Id]),
    CONSTRAINT [FK_Equipments_PlayerLevels] FOREIGN KEY ([PlayerLevelId]) REFERENCES [PlayerLevels]([Id]), 
    CONSTRAINT [FK_Equipments_PlayerRoles] FOREIGN KEY ([PlayerRoleId]) REFERENCES [PlayerRoles]([Id]), 
)
