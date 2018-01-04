SELECT UserName, Country, RegisterDate, 
Projects.value('/Projects[1]/Project[1]/@Name', 'VARCHAR(20)')
	
FROM Users