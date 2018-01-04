SELECT UserID, UserName, Projects.query(
'for $b in /Projects/Project
where $b/@Name = "Folding@home"
return (sql:column("UserName"))')
FROM Users