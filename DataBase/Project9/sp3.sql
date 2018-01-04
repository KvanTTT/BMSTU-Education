CREATE PROCEDURE sp_get_new_user_count @NewUsersCount INT OUTPUT
AS
BEGIN
	SELECT UserName
	FROM tblUsers
	WHERE tblUsers.RegisterDate >= '2009-01-01'

	SET @NewUsersCount = @@ROWCOUNT
END