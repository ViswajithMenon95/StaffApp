-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Proc_TeacherStaffUpdate]
	-- Add the parameters for the stored procedure here
	@id int,
	@teacherDetails udtt_TeacherStaff READONLY
AS
BEGIN
BEGIN TRY
	BEGIN TRANSACTION
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Update [dbo].[Staff] set name = td.name, phone = td.phone from @teacherDetails td where staff_id = @id;
	Update [dbo].[TeacherStaff] set subject = td.subject from @teacherDetails td where staff_id = @id;
	COMMIT
	END TRY

	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK

		PRINT ('Error on updating staff');

		THROW;
	END CATCH
END