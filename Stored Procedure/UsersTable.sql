Create database BookStoreApplication;
Create table Users(UserID int IDENTITY(1,1) PRIMARY KEY,
    FullName varchar(255),
    EmailId varchar(255),
    Password varchar(255),
    PhoneNo BigInt
	);
	drop table Users;

	CREATE PROCEDURE dbo.UserRegistration
	
	@FullName varchar(255),
	@EmailId varchar(255) ,
	@Password varchar(255) ,
	@PhoneNo BigInt
AS
	BEGIN
		INSERT into Users(
		
		FullName,
		EmailId,
		Password,
		PhoneNo)

		values
		(
		@FullName ,
		@EmailId  ,
		@Password  ,
		@PhoneNo 
		)
	END

Create PROCEDURE UserLogin 
	-- Add the parameters for the stored procedure here
	@EmailId varchar(255),
	@Password varchar(255)
AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
SET XACT_ABORT on;
SET NOCOUNT ON;
BEGIN
BEGIN TRY
BEGIN TRANSACTION;

	DECLARE @result int = 0;
	DECLARE @UserID int;
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	if((select count(EmailId) from Users where EmailId = @EmailId) = 0)
		begin;
		set @result = 2;
		THROW 52000, 'Email not registered', -1;
		end
	if((select count(EmailId) from Users where EmailId = @EmailId and Password = @Password) = 0)
	begin;
		set @result = 3;
		THROW 52000, 'wrong password', -1;
	end
	else
	begin
	
	select UserID ,
    FullName varchar,
    EmailId varchar,
    Password varchar,
    PhoneNo BigInt
	from Users where EmailId = @EmailId;
		set @result = 1;
	end
	
COMMIT TRANSACTION
return @result;
END TRY
BEGIN CATCH
--SELECT ERROR_NUMBER() as ErrorNumber, ERROR_MESSAGE() as ErrorMessage;
IF(XACT_STATE()) = -1
	BEGIN
		PRINT
		'transaction is uncommitable' + ' rolling back transaction'
		ROLLBACK TRANSACTION;
		print @result;
		return @result;
	END;
ELSE IF(XACT_STATE()) = 1
	BEGIN
		PRINT
		'transaction is commitable' + ' commiting back transaction'
		COMMIT TRANSACTION;
		print @result;
		return @result;
END;
END CATCH
	
END
GO

USE [BookStoreApplication]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE dbo.UpdatePassword
(@UserId INT,  @NewPassword varchar(20),@result int output)
AS
BEGIN
BEGIN TRY
UPDATE Users 
SET Users.Password = @NewPassword where Users.userId = @UserId;
set @result=1;
END TRY
BEGIN CATCH
set @result=0;
END CATCH
END

USE [BookStoreApplication]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE dbo.EmailValidity
    @EmailId Varchar(200),
	@userId INT OUTPUT,
	@result INT OUTPUT
AS
BEGIN
Begin Try
     IF(EXISTS(SELECT * FROM Users WHERE EmailId=@EmailId))
	  begin
	    set @result=1
	    select @userId=userId from Users where EmailId=@EmailId
	 end
	 else
	   begin
	    set @result=0
		set @userId=0;
	   end
end try
begin Catch
	    set @result=0
end catch
End

CREATE PROC spUserLogin
	@email VARCHAR(255),
	@password VARCHAR(25),
	@user INT = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	IF EXISTS(SELECT * FROM Users WHERE EmailId = @email)
	BEGIN
		IF EXISTS(SELECT * FROM Users WHERE EmailId = @email AND Password = @password)
		BEGIN
			SET @user = 2;
		END
		ELSE
		BEGIN
			SET @user = 1;
		END
	END
	ELSE
	BEGIN
		SET @user = NULL;
	END
END

