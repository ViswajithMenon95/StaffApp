-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Proc_CheckUniquePhone] 
	-- Add the parameters for the stored procedure here
	@id int,
	@phone varchar(50),
	@isUnique bit output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here	
	IF EXISTS (Select * from [dbo].[Staff] where staff.phone = @phone and staff.staff_id != @id)
		Set @isUnique = 0;
	ELSE
		Set @isUnique = 1;
END