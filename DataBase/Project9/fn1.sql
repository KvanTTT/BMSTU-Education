CREATE FUNCTION dbo.fn_average_task_cost()
RETURNS INT
AS
BEGIN
	RETURN (SELECT AVG(TaskCost) FROM dbo.tblTasks)
END