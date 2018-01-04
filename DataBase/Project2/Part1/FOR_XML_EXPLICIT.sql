--USE DistribComp
--
--SELECT 1                       as Tag, 
--     NULL                      as Parent,
--     tblUsers.Team           as [User!1!Team],
--     tblUsers.Country             as [User!1!Country],
--     NULL                      as [Task!2!TaskID],
--     NULL                      as [Task!2!TaskFinishDate]
--    -- NULL                      as [Task!2!TaskCost]
--FROM tblUsers
--WHERE tblUsers.Country = 'RUSSIAN FEDERATION' OR tblUsers.Country = 'UKRAINE'
--
--UNION ALL
--
--SELECT 2, 
--      1,
--      tblUsers.UserID,
--      tblUsers.UserName,
--      tblTasks.TaskID,
--      tblTasks.TaskFinishDate
--FROM tblUsers
--JOIN tblTasks
--ON tblUsers.UserID = tblTasks.TaskID
--WHERE tblUsers.Country = 'RUSSIAN FEDERATION' OR tblUsers.Country = 'UKRAINE'
----ORDER BY [User!1!UserID], [Task!2!TaskID]
--FOR XML EXPLICIT

select 1 as tag, -- первый подзапрос
	NULL as parent,
    Country as 'Countries!1!Country',
    NULL as 'User!2!UserName',
    NULL as 'User!2!Project',
	NULL as 'User!2!AllScore'
from tblUsers
where tblUsers.Country = 'RUSSIAN FEDERATION' OR tblUsers.Country = 'UKRAINE'

union all 

select 2 as tag, -- второй подзапрос
    1 as parent,
    Country,
    UserName,
    Project,
	AllScore
from tblUsers
where tblUsers.Country = 'RUSSIAN FEDERATION' OR tblUsers.Country = 'UKRAINE'

--join tblUsers
--order by 'User!1!Country', 'User!2!UserName', 'User!2!Project', 'User!2!AllScore'

for xml explicit
