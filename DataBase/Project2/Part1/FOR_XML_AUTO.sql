USE DistribComp
SELECT UserName, Project, AllScore
FROM tblUsers
WHERE (Team = N'Russia' OR Country = N'Russian Federation') AND AllScore > 1000000
ORDER BY tblUsers.UserID
FOR XML AUTO
