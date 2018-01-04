CREATE PROCEDURE sp_select_active_users
AS
BEGIN
	IF OBJECT_ID('dbo.tblActiveUsers') IS NULL
	BEGIN
	CREATE TABLE tblActiveUsers
	(
		UserID INT PRIMARY KEY,
		UserName VARCHAR (60) NOT NULL,
	)
	END
	INSERT tblActiveUsers (UserID, UserName)
	SELECT tblUsers.UserID, tblUsers.UserName
	FROM tblUsers
	WHERE tblUsers.AvgScorePerDay > 200.0
END

GO