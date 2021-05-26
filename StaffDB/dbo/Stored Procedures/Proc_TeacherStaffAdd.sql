-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Proc_TeacherStaffAdd]
	-- Add the parameters for the stored procedure here
	--@name varchar(50),
	--@phone varchar(50),
	--@subject varchar(50)
	@teacherDetails udtt_TeacherStaff READONLY
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	--Insert into [dbo].[Staff] values (@name, @phone);
	--Insert into [dbo].[TeacherStaff] values (SCOPE_IDENTITY(), @subject);

	Insert into [dbo].[Staff] (name, phone)
	Select name, phone from @teacherDetails;

	Insert into [dbo].[TeacherStaff] (staff_id, subject )
	Select Staff.staff_id, td.subject from @teacherDetails td inner join Staff on Staff.phone = td.phone;
END