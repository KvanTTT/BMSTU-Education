USE DistribComp
GO

CREATE PROCEDURE sp_add_user @Name VARCHAR(60), @Project VARCHAR (30), @RegisterDate DATETIME
AS
	DECLARE @max_id INT
	SELECT @max_id = Max(UserID)
	FROM tblUsers
	INSERT tblUsers (UserID, UserName, Team, Country, Project, RegisterDate, AvgScorePerDay, AllScore)
	VALUES(@max_id + 1, @Name, NULL, NULL, @Project, @RegisterDate, 0, 0)
GO