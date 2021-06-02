-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
CREATE PROCEDURE [dbo].[Proc_TeacherStaffAdd]  
 -- Add the parameters for the stored procedure here   
	@teacherDetails udtt_TeacherStaff READONLY  
AS  
BEGIN 
BEGIN TRY		BEGIN TRANSACTION
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
  
    -- Insert statements for procedure here   
	Insert into [dbo].[Staff] (name, phone, type)  
	Select name, phone, type from @teacherDetails;  
  
	Insert into [dbo].[TeacherStaff] (staff_id, subject )  
	Select Staff.staff_id, td.subject from @teacherDetails td inner join Staff on Staff.phone = td.phone;  

		COMMIT	END TRY	BEGIN CATCH		IF @@TRANCOUNT > 0			ROLLBACK		PRINT ('Error on inserting staff');		THROW;	END CATCH
END