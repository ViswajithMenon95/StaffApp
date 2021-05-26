-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Proc_TeacherStaffGetById] 
	-- Add the parameters for the stored procedure here
	@id int
AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select Staff.staff_id, Staff.name, Staff.phone, TeacherStaff.subject from Staff Inner Join TeacherStaff on Staff.staff_id = TeacherStaff.staff_id And Staff.staff_id = @id;
END