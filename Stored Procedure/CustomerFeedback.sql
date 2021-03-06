CREATE TABLE CustomerFeedback (
    UserId int,
    BookId int ,
	feedbackId INT PRIMARY KEY IDENTITY(1,1),
	rating float,
	FeedBack varchar(1000)
    FOREIGN KEY (BookId) REFERENCES Books(BookId),
	FOREIGN KEY (UserId) REFERENCES Users(UserId)
	); 
	drop table CustomerFeedback;


USE [BookStoreApplication]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[AddFeedback]
	
	@BookId int,
	@UserId int ,
	@Rating float,
	@FeedBack varchar (1000) 
	
AS
	BEGIN
		INSERT into CustomerFeedback(
		
		BookId,
		UserId,
		rating,
		Feedback
		)

		values
		(
		@BookId ,
	@UserId  ,
	@Rating,
	@FeedBack 

		)
	END
RETURN 0


USE [BookStoreApplication]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[GetCustomerFeedback]
   @bookid int
 as
 begin
    select Users.userId,FullName,Feedback,Rating
	from Users 
	inner join CustomerFeedback 
	on CustomerFeedback.UserId=Users.userId where CustomerFeedback.BookId=@bookid
 end