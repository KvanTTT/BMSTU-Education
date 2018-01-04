DECLARE @Xml XML
SET @Xml = (SELECT Projects FROM Users WHERE UserName = 'SevTrek')

SELECT @Xml
SET @Xml.modify('insert <Project Name = "The Lattice Project"/> after (/Projects/Project)[2]') 
SET @Xml.modify('insert <Project Name = "MilkyWay@home"/> as first into (/Projects)[1]')
SET @Xml.modify('insert <Project Name = "Magnetism@home"/> as last into (/Projects)[1]')
SET @Xml.modify('insert <Project Name = "Spinhenge@home"/> before (/Projects/Project[3])[1]')
SELECT @Xml

SET @Xml.modify('delete /Projects/Project[2]')
SELECT @Xml

SET @Xml.modify('replace value of (/Projects/Project[3]/@Name)[1] with "All World"')
SELECT @Xml
