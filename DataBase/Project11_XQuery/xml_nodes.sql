DECLARE @Xml XML
SET @Xml = (SELECT Projects FROM Users WHERE AllScore > 9000000)

SELECT @Xml.query('.') AS RESULT
FROM @Xml.nodes('/Projects/Project[1]') AS C(D)