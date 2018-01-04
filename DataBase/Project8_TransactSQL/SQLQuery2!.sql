USE DistribComp

SELECT UserID, UserName, Team
FROM tblUsers AS P JOIN tblTasks AS C
ON P.Country = 'Russian Federation' AND C.TaskCost = 272