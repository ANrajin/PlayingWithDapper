CREATE TABLE [dbo].[Contacts]
(
	[Id] INT NOT NULL IDENTITY, 
    [FullName] NVARCHAR(50) NOT NULL, 
    [Email] NVARCHAR(50) NOT NULL, 
    [Company] NVARCHAR(50) NOT NULL, 
    [Title] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [PK_Contacts] PRIMARY KEY ([Id]) 
)
