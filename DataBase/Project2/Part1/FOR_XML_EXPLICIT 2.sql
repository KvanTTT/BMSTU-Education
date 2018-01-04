select 1 as Tag,
	NULL as Parent,
    UserName as [User!1!Name],
    Team as [User!1!Team!Element],
	AllScore as [User!1!AllScore!Element]
from tblUsers
where Country = 'Russian Federation'

order by [User!1!Name]

for xml explicit

