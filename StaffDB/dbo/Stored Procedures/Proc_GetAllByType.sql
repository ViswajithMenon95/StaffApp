-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE Proc_GetAllByType
	-- Add the parameters for the stored procedure here
	@staffType varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF(@staffType = 'Teacher')
	Select Staff.staff_id, Staff.name, Staff.phone, TeacherStaff.subject from Staff inner join TeacherStaff on Staff.staff_id = TeacherStaff.staff_id; 
	ELSE IF(@staffType = 'Admin')
	Select Staff.staff_id, Staff.name, Staff.phone, AdminStaff.department from Staff inner join AdminStaff on Staff.staff_id = AdminStaff.staff_id;
	ELSE IF(@staffType = 'Support')
	Select Staff.staff_id, Staff.name, Staff.phone, SupportStaff.age from Staff inner join SupportStaff on Staff.staff_id = SupportStaff.staff_id;
END