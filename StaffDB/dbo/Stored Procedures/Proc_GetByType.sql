-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Proc_GetByType]
	-- Add the parameters for the stored procedure here
	@staffType tinyint
AS
BEGIN
BEGIN TRY
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select s.staff_id, s.name, s.phone, s.type, a.department, t.subject, st.age
	from Staff s left join  
	AdminStaff a on s.staff_id = a.staff_id  left join
	TeacherStaff t  on s.staff_id = t.staff_id  left join
	SupportStaff st on s.staff_id = st.staff_id where s.type = @staffType
	END TRY

	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK

		PRINT ('Error on access');

		THROW;
	END CATCH
END