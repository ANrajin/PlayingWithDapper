CREATE TABLE [dbo].[Addresses]
(
	[Id] INT NOT NULL IDENTITY, 
    [ContactId] INT NOT NULL, 
    [AddressType] NVARCHAR(10) NOT NULL, 
    [StreetAddress] NVARCHAR(50) NOT NULL, 
    [City] NVARCHAR(50) NOT NULL, 
    [StateId] INT NOT NULL, 
    [PostalCode] NVARCHAR(20) NOT NULL, 
    CONSTRAINT [PK_Addresses] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_Addresses_States] FOREIGN KEY ([StateId]) REFERENCES [States]([Id]), 
    CONSTRAINT [FK_Addresses_Contacts] FOREIGN KEY ([ContactId]) REFERENCES [Contacts]([Id]) ON DELETE CASCADE
)
