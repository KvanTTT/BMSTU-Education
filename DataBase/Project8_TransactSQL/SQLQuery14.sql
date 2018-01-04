UPDATE tblTasks
SET TaskFinishDate = getdate()
WHERE TaskStartDate >= '2009-08-01'