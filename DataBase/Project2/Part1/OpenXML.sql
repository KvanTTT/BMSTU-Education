USE DistribComp

DECLARE @idoc      int 
DECLARE @xmldoc    nvarchar(4000) 

-- define the XML document 
SET @xmldoc = ' 
<ROOT> 
<Task TaskID="71"	TaskName="307B1D39A3C76293311C020DFA457EAE"	TaskCost="237"
TaskStartDate="2007-04-23 00:00:00.000"	TaskFinishDate="2007-04-23 00:00:00.000"/> 
</ROOT> 
' 

--Load and parse the XML document in memory 
EXEC sp_xml_preparedocument @idoc OUTPUT, @xmldoc 

--List out what our Tasks table looks like before the insert
SELECT * FROM tblTasks

-- TaskID is an IDENTITY column, so we need to allow direct updates
--SET IDENTITY_INSERT tblTasks ON

--See our XML data in a tabular format
SELECT * FROM OPENXML (@idoc, '/ROOT/Task', 0) WITH ( 
	TaskID INT,
	TaskName VARCHAR(32),
	TaskCost FLOAT,
	TaskStartDate DATETIME,
	TaskFinishDate DATETIME
) 

--Perform and insert based on that data
INSERT INTO tblTasks
(TaskID, TaskName, TaskCost, TaskStartDate, TaskFinishDate)
SELECT * FROM OPENXML (@idoc, '/ROOT/Task', 0) WITH ( 
	TaskID INT,
	TaskName VARCHAR(32),
	TaskCost FLOAT,
	TaskStartDate DATETIME,
	TaskFinishDate DATETIME
) 

--Set things back to normal
--SET IDENTITY_INSERT tblTasks OFF

--Now look at the Tasks table after our insert
SELECT * FROM tblTasks

--Now clear the XML document from memory
EXEC sp_xml_removedocument @idoc
