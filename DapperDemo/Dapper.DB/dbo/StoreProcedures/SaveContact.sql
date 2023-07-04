create procedure [dbo].[SaveContact]
	@Id     	int output,
	@FullName	varchar(50),	
	@Company	varchar(50),
	@Title		varchar(50),
	@Email		varchar(50)
AS
BEGIN
	UPDATE	Contacts
	SET		FullName = @FullName,
			Company   = @Company,
			Title     = @Title,
			Email     = @Email
	WHERE	Id        = @Id

	IF @@ROWCOUNT = 0
	BEGIN
		INSERT INTO [dbo].[Contacts]
           ([FullName]
           ,[Company]
           ,[Title]
           ,[Email])
		VALUES
           (@FullName,
           @Company,
           @Title,
           @Email);
		SET @Id = cast(scope_identity() as int)
	END;
END;