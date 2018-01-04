DECLARE @Projects XML
SET @Projects = (SELECT top 1 Projects FROM Users)

SELECT @Projects.exist('/Projects/Project')
SELECT @Projects.exist('/Projects/Team')