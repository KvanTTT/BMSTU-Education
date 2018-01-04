CREATE PROCEDURE sp_calcul_total_avg_score
AS
BEGIN
	DECLARE @Avg FLOAT

	SELECT @Avg = AVG(AvgScorePerDay)
	FROM tblUsers

	RETURN @Avg
END