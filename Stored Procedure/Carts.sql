
CREATE TABLE Carts(	
	[UserId] [int] ,
	[CartId] [int] IDENTITY(1,1) NOT NULL,
	[BookId] [int] ,
	[result] [int],
	[BookCount] [int] ,
	[NoOfBook] [int] DEFAULT 1 NOT NULL
	PRIMARY KEY (CartId));

	alter table Carts add constraint Cart_Users  Foreign Key (UserId) references Users (UserId);
	alter table Carts add constraint Cart_Books  Foreign Key (BookId) references Books (BookId);

	drop table Carts;

USE [BookStoreApplication]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter procedure dbo.AddTOCart
 @BookId int,
 @UserId int,
 @NoOfBook int,
 @result int output

 as
 begin
  BEGIN TRY
      if Exists(select * from Carts where UserId=@UserId and BookId=@BookId)
	     begin
		   set @result=0;
		 end
	  else
	     begin
		   insert into Carts (BookId,UserId,NoOfBook) values (@BookId,@UserId,@NoOfBook)
		   update Books
				   set Books.BookCount=Books.BookCount-1
				   where BookId=@BookId;
		   set @result=1;
		 end
  END TRY
  begin catch
      set @result=0;
  end catch
 end

 USE [BookStoreApplication]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[GetCartItems]
 @userId int

as
begin
    select Books.BookId,BookName,AuthorName,Price,OriginalPrice,CartId,Carts.BookCount,Books.BookCount,Image,Carts.Userid 
	from Books 
	inner join Carts 
	on Carts.BookId=Books.BookId where Carts.Userid=@userId
end

USE [BookStoreApplication]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter procedure dbo.EditNumberOfBooks
 @CartId int,
 @type bit,
 @result int output

 as
 begin
  BEGIN TRY
  BEGIN Transaction
  declare @count  int , @bookid int;

      if Exists(select * from Carts where CartId=@CartId)
	     begin
		   select @count=BookCount,@bookid=BookId from Carts where CartId=@CartId; 
		   if(@type=1)		
 begin
			   if exists(select * from Books where Books.BookCount !=0 and BookId=@bookid)
			    begin
					update Carts
				   set BookCount=@count+1
				   where CartId=@CartId;
				   update Books
				   set Books.BookCount=Books.BookCount-1
				   where BookId=@bookid;
				   set @result=1;
				end
				else
				 begin
				  set @result=0;
				 end
			 end
			 else
			   begin
			     update Carts
				   set BookCount=@count-1
				   where CartId=@CartId;
				  update Books
				   set Books.BookCount=Books.BookCount+1
				   where BookId=@bookid;
				   set @result=1;
			   end
			end
	  else
	     begin
		   set @result=0;
		 end
   if(@result=1)
	      begin
		  Commit Tran
		end

  END TRY
  begin catch
  set @result=0;
  Rollback Tran
  end catch
 end

 USE [BookStoreApplication]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure dbo.RemoveFromCart
 @CartId int,
 @result int output

 as
 begin
  BEGIN TRY
  declare @count  int,@bookid int
      if Exists(select  * from Carts where CartId=@CartId)
	     begin
			select  @count=BookCount,@bookid=BookId from Carts where CartId=@CartId
		   DELETE FROM Carts WHERE CartId=@CartId;
		   update Books
				   set Books.BookCount=Books.BookCount+@count
				   where BookId=@bookId;
		   set @result=1;
		 end
	  else
	     begin
		   set @result=0;
		 end
  END TRY
  begin catch
      set @result=0;
  end catch
 end

 select * from Books;