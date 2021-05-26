-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Proc_AdminStaffAdd]
	-- Add the parameters for the stored procedure here
	@adminDetails udtt_AdminStaff READONLY
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	--Insert into [dbo].[Staff] values (@name, @phone);
	--Insert into [dbo].[AdminStaff] values (SCOPE_IDENTITY(), @department);
	Insert into [dbo].[Staff] (name, phone)
	Select name, phone from @adminDetails;
	
	Insert into [dbo].[AdminStaff] (staff_id, department)
	Select Staff.staff_id, ad.department from @adminDetails ad inner join Staff on ad.phone = Staff.phone;
END