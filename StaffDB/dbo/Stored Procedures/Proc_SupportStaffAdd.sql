-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Proc_SupportStaffAdd]
	-- Add the parameters for the stored procedure here
	@supportDetails udtt_SupportStaff READONLY
AS
BEGIN
BEGIN TRY
	BEGIN TRANSACTION
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Insert into [dbo].[Staff] (name, phone, type)
	Select name, phone, type from @supportDetails;

	Insert into [dbo].[SupportStaff] (staff_id, age)
	Select Staff.staff_id, sd.age from Staff inner join @supportDetails sd on Staff.phone = sd.phone;
	COMMIT
	END TRY

	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK

		PRINT ('Error on inserting staff');

		THROW;
	END CATCH
END