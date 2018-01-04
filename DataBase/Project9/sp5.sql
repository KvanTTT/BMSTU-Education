CREATE PROCEDURE sp_last_payments
AS
BEGIN
	DECLARE @PaymentID INT
	DECLARE @PaymentDate DATETIME

	OPEN TableCursor
	FETCH NEXT FROM TableCursor INTO @PaymentID, @PaymentDate	

	WHILE @@FETCH_STATUS = 0
	BEGIN
		INSERT tblLastPayments (PaymentID, PaymentDate)
		VALUES (@PaymentID, @PaymentDate)
		FETCH NEXT FROM TableCursor INTO @PaymentID, @PaymentDate
	END

	CLOSE TableCursor
END