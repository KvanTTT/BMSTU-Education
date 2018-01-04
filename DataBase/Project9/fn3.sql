CREATE FUNCTION dbo.fn_get_old_users()
RETURNS @Users TABLE(UserID INT, UserName VARCHAR(60), AllScore INT)
AS
BEGIN
	INSERT INTO @Users
	SELECT UserID, UserName, AllScore
	FROM tblUsers
	WHERE RegisterDate < '2005-01-01' AND AllScore > 3000000
	
	DELETE @Users
	WHERE UserID > '3000'

	RETURN
END