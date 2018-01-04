ALTER FUNCTION dbo.fn_get_top_users()
RETURNS TABLE
AS
RETURN
(SELECT * 
FROM tblUsers
WHERE tblUsers.AllScore >= 9000000)