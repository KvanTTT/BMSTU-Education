USE DistribComp
GO

DROP PROCEDURE PROCEDURE1
DROP PROCEDURE PROCEDURE2
DROP PROCEDURE PROCEDURE3
DROP PROCEDURE PROCEDURE4
DROP PROCEDURE PROCEDURE5
DROP TRIGGER TRIGGER1
DROP TRIGGER TRIGGER2
DROP FUNCTION FUNCTION1
--DROP FUNCTION FUNCTION2
DROP FUNCTION FUNCTION3
DROP ASSEMBLY SProgramm
GO

CREATE ASSEMBLY SProgramm FROM 'bin\Debug\Project10_CLRinSQL.dll';
GO

CREATE PROCEDURE PROCEDURE1 
AS EXTERNAL NAME SProgramm.StoredProcedures.StoredProcedure1; 
GO

CREATE PROCEDURE PROCEDURE2 @Name nvarchar, @Project nvarchar, @RegisterDate datetime
AS EXTERNAL NAME SProgramm.StoredProcedures.StoredProcedure2; 
GO

CREATE PROCEDURE PROCEDURE3 @Year INT, @YearPaymentCredit Money OUTPUT
AS EXTERNAL NAME SProgramm.StoredProcedures.StoredProcedure3;
GO

CREATE PROCEDURE PROCEDURE4 @AVG MONEY OUTPUT
AS EXTERNAL NAME SProgramm.StoredProcedures.StoredProcedure4;
GO

CREATE PROCEDURE PROCEDURE5
AS EXTERNAL NAME SProgramm.StoredProcedures.StoredProcedure5;
GO

CREATE TRIGGER TRIGGER1
ON tblUsers
AFTER DELETE
AS EXTERNAL NAME SProgramm.Triggers.Trigger1;
GO

CREATE TRIGGER TRIGGER2
ON tblUsers
AFTER INSERT
AS EXTERNAL NAME SProgramm.Triggers.Trigger2;
GO

CREATE FUNCTION FUNCTION1()
RETURNS FLOAT
AS EXTERNAL NAME SProgramm.UserDefinedFunctions.Function1;
GO

--CREATE FUNCTION FUNCTION2()
--RETURNS TABLE(PlayerID INT, PlayerName NVARCHAR(60), PlayerAge INT, PlayerPosition NVARCHAR(30))
--AS EXTERNAL NAME SProgramm.UserDefinedFunctions.Function2;
--GO

CREATE FUNCTION FUNCTION3()
RETURNS TABLE(UserID INT, UserName NVARCHAR (60), AllScore INT)
AS EXTERNAL NAME SProgramm.UserDefinedFunctions.Function3;
GO