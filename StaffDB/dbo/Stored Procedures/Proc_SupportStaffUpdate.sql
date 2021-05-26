-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Proc_SupportStaffUpdate]
	-- Add the parameters for the stored procedure here
	@id int,
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
	Update [dbo].[Staff] set name = sd.name, phone = sd.phone from @supportDetails sd where staff_id = @id;
	Update [dbo].[SupportStaff] set age = sd.age from @supportDetails sd where staff_id = @id;
END