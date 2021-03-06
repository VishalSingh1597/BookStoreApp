Create table Books(BookId int IDENTITY(1,1) PRIMARY KEY,
   	originalPrice int,
  BookName varchar(250),
  AuthorName varchar(250),
  Price int,
  BookDescription varchar(500),
  Image varchar(100),
  Rating int,
  BookCount int
	);

	 select * from Books;

USE [BookStoreApplication]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetBook]
  @BookId int
AS
BEGIN
BEGIN TRY
     IF(EXISTS(SELECT * FROM Books WHERE BookId=@BookId))
	 begin
	   SELECT * FROM Books WHERE BookId=@BookId;
   	 end
	 else
	   THROW  52000, 'Book Not Available', 1;
END TRY
BEGIN CATCH  
       SELECT  
    ERROR_NUMBER() AS ErrorNumber  
    ,ERROR_MESSAGE() AS ErrorMessage;
END CATCH;
End


select * from Books;


USE [BookStoreApplication]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[UpdateBook]
   @BookId int,
  @originalPrice int,
  @BookName varchar(250),
  @AuthorName varchar(250),
  @Price int,
  @BookDescription varchar(500),
  @Image varchar(100),
  @Rating int,
   @BookCount int,
   @result int output
AS
	BEGIN
	if Exists(select * from Books where BookId=@BookId)
	 begin
		Update  Books 
		set
		 BookName =@BookName,
   AuthorName=@AuthorName,
  Price = @Price,
  BookDescription=@BookDescription,
  Image =@Image,
  Rating =@Rating,
  BookCount=@BookCount,
  OriginalPrice=@originalPrice
  where BookId=@BookId;
  set @result=1
  end
  else
  begin
   set @result=0;
   end
END


USE [BookStoreApplication]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[RemoveBookByAdmin]
@BookId INT ,
@result int output
AS
BEGIN
BEGIN TRY
SET XACT_ABORT on;
BEGIN TRANSACTION
    if exists(select * from Books where BookId=@BookId)
	begin
       Delete FROM Books Where Bookid=@BookId;
	   set @result=1;
	end
	else
	 begin
	   set @result=0;
	 end
COMMIT TRANSACTION;	
END TRY
BEGIN CATCH
IF(XACT_STATE()) = -1
	BEGIN
		ROLLBACK TRANSACTION;
		set @result=0;
	END;
ELSE IF(XACT_STATE()) = 1
	BEGIN
		PRINT
		'transaction is commitable' + ' commiting back transaction'
		COMMIT TRANSACTION;
		set @result=1;
	END;
END CATCH
END

USE [BookStoreApplication]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[InsertBooks]
	@originalPrice int,
  @BookName varchar(250),
  @AuthorName varchar(250),
  @Price int,
  @BookDescription varchar(500),
  @Image varchar(100),
  @Rating int,
   @BookCount int
AS
	BEGIN
		INSERT into Books (
		 BookName ,
   AuthorName ,
  Price ,
  BookDescription,
  Image ,
  Rating ,
  BookCount,
  OriginalPrice)

		values
		(
		 @BookName ,
  @AuthorName ,
  @Price ,
  @BookDescription ,
  @Image ,
  @Rating ,
   @BookCount ,
   @originalPrice
		)
END