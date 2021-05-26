-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Proc_TeacherStaffUpdate]
	-- Add the parameters for the stored procedure here
	@id int,
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
	Update [dbo].[Staff] set name = td.name, phone = td.phone from @teacherDetails td where staff_id = @id;
	Update [dbo].[TeacherStaff] set subject = td.subject from @teacherDetails td where staff_id = @id;
END