
/* User define table type for student table */
USE [TestDB]
GO
/* ***** Object:  UserDefinedTableType [dbo].[tblStudent]    Script Date: 16/06/2015 06:55:36 PM ******/
CREATE TYPE [dbo].[tblStudent] AS TABLE(
	[srno] [int] NULL,
	[roll] [int] NULL,
	[fname] [varchar](100) NULL,
	[lname] [varchar](100) NULL,
	[mobile] [varchar](100) NULL
)
GO


/*Stored procedure to insert the data before creation of this stored procedure **/
/*create one user define Table type for your table */


USE [TestDB]
GO
/****** Object:  StoredProcedure [dbo].[insert_sp]    Script Date: 16/06/2015 06:51:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[insert_sp](
 @content dbo.tblStudent readonly,
 @retval int out,
 @retmsg varchar(100) out
 )
 As 
 Begin try
	  Select * into #studenttbl from @content;
	  insert into student(sroll,fname,Lname,Mobile) select roll, fname,lname,mobile from #studenttbl;
	  Set @retval=1;
	  Set @retmsg='OK';
 End Try
 Begin Catch
      Set @retval=0;
	  Set @retmsg=ERROR_MESSAGE();
 End Catch 
