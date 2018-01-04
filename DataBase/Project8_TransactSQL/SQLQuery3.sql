USE DistribComp

SELECT Team, SUM(AllScore)
FROM tblUsers
GROUP BY Team
ORDER BY SUM(AllScore) DESC