select 1 as Tag,
	NULL as Parent,
	UserID as [User!1!UserID],
    UserName as [User!1!UserName],	
	Team as [User!1!Team!Element],
	Country as [User!1!Country!Element],
    Project as [User!1!Project!Element],
	RegisterDate as [User!1!RegDate!Element],
	AvgScorePerDay as [User!1!AvgScore!Element],
	AllScore as [User!1!AllScore!Element]
from tblUsers

order by [User!1!UserName]

for xml explicit

