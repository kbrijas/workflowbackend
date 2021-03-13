
CREATE PROCEDURE spGetMenuDropdown 
AS
BEGIN

	SET NOCOUNT ON;

	SELECT SeqNo as Id, 
		   SeqNo as [Value],  
		   Menu as [Text] from MenuMaster
	where IsActive = 1

END