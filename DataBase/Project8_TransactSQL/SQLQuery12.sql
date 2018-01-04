
INSERT tblUsers (UserID, UserName, Team, Country, Project, RegisterDate, AvgScorePerDay, AllScore)
SELECT 1002, UserName, NULL, NULL, Project, RegisterDate, AvgScorePerDay, AllScore 
FROM tblUsers
WHERE UserID = 5
