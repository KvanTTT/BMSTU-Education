BULK INSERT DistribComp.dbo.tblTasks
FROM 'out\Tasks.db'
WITH (CODEPAGE = 'ACP', DATAFILETYPE = 'char', FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n')

GO

BULK INSERT DistribComp.dbo.tblUsers
FROM 'out\Users.db'
WITH (CODEPAGE = 'ACP', DATAFILETYPE = 'char', FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n')

GO

BULK INSERT DistribComp.dbo.tblPayments
FROM 'out\Payments.db'
WITH (CODEPAGE = 'ACP', DATAFILETYPE = 'char', FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n')

GO
