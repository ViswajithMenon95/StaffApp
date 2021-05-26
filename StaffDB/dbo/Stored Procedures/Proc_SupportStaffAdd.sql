-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Proc_SupportStaffAdd]
	-- Add the parameters for the stored procedure here
	--@name varchar(50),
	--@phone varchar(50),
	--@age int
	@supportDetails udtt_SupportStaff READONLY
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	--Insert into [dbo].[Staff] values (@name, @phone);
	--Insert into [dbo].[SupportStaff] values (SCOPE_IDENTITY(), @age);
	Insert into [dbo].[Staff] (name, phone)
	Select name, phone from @supportDetails;

	Insert into [dbo].[SupportStaff] (staff_id, age)
	Select Staff.staff_id, sd.age from Staff inner join @supportDetails sd on Staff.phone = sd.phone;
END