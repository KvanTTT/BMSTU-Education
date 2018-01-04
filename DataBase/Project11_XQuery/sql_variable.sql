DECLARE @ProjectName VARCHAR(30)
SET @ProjectName = 'SIMAP'

SELECT Projects.query(
'for $b in/Projects/Project
where $b/@Name = "SIMAP"
return(sql:variable("@ProjectName"))')
FROM Users