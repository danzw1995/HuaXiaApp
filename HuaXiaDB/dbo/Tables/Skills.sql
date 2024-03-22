CREATE TABLE [dbo].[Skills]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(2000) NOT NULL, 
    [PlayerLevelId] INT NOT NULL , 
    [PlayerRoleId] INT NOT NULL, 
    [Image] NVARCHAR(2000) NOT NULL, 
    CONSTRAINT [FK_Skills_PlayerRoles] FOREIGN KEY ([PlayerRoleId]) REFERENCES [PlayerRoles]([Id]), 
    CONSTRAINT [FK_Skills_PlayerLevels] FOREIGN KEY ([PlayerLevelId]) REFERENCES [PlayerLevels]([Id]),
)
