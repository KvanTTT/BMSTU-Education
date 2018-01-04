CREATE XML SCHEMA COLLECTION projects_xsd1 AS
(
'<?xml version="1.0" encoding="UTF-8" ?> 
  <xs:schema xmlns:xs="http://www.w3.org/2001/XMLSchema">
  <xs:element name="Project">
  <xs:complexType>
  <xs:attribute name="Name" type="xs:string" use="required" /> 
  </xs:complexType>
  </xs:element>
  <xs:element name="Projects">
  <xs:complexType>
  <xs:sequence>
  <xs:element ref="Project" minOccurs = "1" maxOccurs="unbounded" /> 
  </xs:sequence>
  </xs:complexType>
  </xs:element>
  </xs:schema>'
)
GO

CREATE TABLE Users
(
	UserID INT PRIMARY KEY NOT NULL, --IDENTITY(1, 1),
	UserName VARCHAR(60) NOT NULL,	
	Team VARCHAR (60), 
	Country CHAR (20), 
	Projects XML (projects_xsd1) NOT NULL, 
	RegisterDate DATETIME NOT NULL, 
	AvgScorePerDay FLOAT NOT NULL,
	AllScore INT NOT NULL
)
GO

CREATE PRIMARY XML INDEX ind
ON Users(Projects)
GO

CREATE XML INDEX ind1
ON Users(Projects)
USING XML INDEX ind FOR PATH
GO

INSERT INTO Users
VALUES (20, 'MichaelTaylor', 'folding@evga', 'SLOVENIA', 
'<Projects>
	<Project Name =	"Rosetta@home"/>
	<Project Name = "Folding@home"/>
</Projects>',
'2000-7-29', 533.196, 7529720)

INSERT INTO Users
VALUES (21, 'SevTrek', 'MSI HQ Red Rockets', 'FRANCE',
'<Projects>
	<Project Name =	"Climate Prediction"/>
</Projects>',
'1999-5-18', 899.406, 4860052)

INSERT INTO Users
VALUES (22, 'stkitts', '[H]ardOCP', 'ARGENTINA',
'<Projects>
	<Project Name =	"SIMAP"/>
	<Project Name = "Einstein@home"/>
	<Project Name = "World Community Grid"/>
</Projects>',
 '1999-12-20', 856.126, 9223808)

INSERT INTO Users
VALUES (23, 'GreenVail', 'Team Hack-a-Day', 'UNITED_KINGDOM',
'<Projects>
	<Project Name =	"Folding@home"/>
	<Project Name = "SETI@home"/>
	<Project Name = "LHC@homr"/>
</Projects>',
'2007-4-15', 886.366, 7792113)

INSERT INTO Users
VALUES (24, 'google250285246873631', 'Team MacOS X', 'POLAND',
'<Projects>
	<Project Name =	"PrimeGrid"/>
</Projects>',
'1999-1-10', 728.456, 4445108)