CREATE procedure [dbo].[GetContact]
	@Id int
AS
BEGIN
	SELECT [Id]
		  ,[FullName]
		  ,[Email]
		  ,[Company]
		  ,[Title]
	  FROM [dbo].[Contacts]
	WHERE Id = @Id;

	SELECT 
		Id,
		ContactId,
		AddressType,
		StreetAddress,
		City,
		StateId,
		PostalCode
	FROM [dbo].[Addresses] 
	WHERE ContactID = @Id;

END
