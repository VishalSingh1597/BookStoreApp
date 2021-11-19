CREATE TABLE WishList (
    WishListId INT PRIMARY KEY IDENTITY(1,1),
    BookId int ,
    UserId int,   
    FOREIGN KEY (BookId) REFERENCES Books(BookId),
	 FOREIGN KEY (UserId) REFERENCES Users(UserId)

);


USE [BookStoreApplication]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[InsertIntoWishList]
@UserId INT ,
@BookId INT ,
@result int output
AS
BEGIN
BEGIN TRY
INSERT INTO WishList(
UserId,
BookId)
VALUES(
@UserId  ,
@BookId 
);
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
create PROC [dbo].[GetWishList]
(@userId INT)
AS
BEGIN
select 
Books.BookId,BookName,AuthorName,Price,OriginalPrice,Image,WishListId
FROM Books
inner join Wishlist
on WishList.BookId=Books.BookId where WishList.UserId=@userId
END


USE [BookStoreApplication]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[RemoveFromWishList]
@WishListId INT ,
@result int output
AS
BEGIN
BEGIN TRY
Delete FROM WishList Where WishListId =@WishListId;
set @result=1;
END TRY
BEGIN CATCH
set @result=0;
END CATCH
END