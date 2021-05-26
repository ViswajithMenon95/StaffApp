-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Proc_SupportStaffGetById] 
	-- Add the parameters for the stored procedure here
	@id int
AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select Staff.staff_id, Staff.name, Staff.phone, SupportStaff.age from Staff Inner Join SupportStaff on Staff.staff_id = SupportStaff.staff_id And Staff.staff_id = @id;
END