-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Proc_AdminStaffUpdate]
	-- Add the parameters for the stored procedure here
	@id int,
	--@name varchar(50),
	--@phone varchar(50),
	--@department varchar(50)
	@adminDetails udtt_AdminStaff READONLY
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Update [dbo].[Staff] set name = ad.name, phone = ad.phone from @adminDetails ad where staff_id = @id;
	Update [dbo].[AdminStaff] set department = ad.department from @adminDetails ad where staff_id = @id;
END