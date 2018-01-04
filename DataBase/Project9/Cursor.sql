DECLARE TableCursor CURSOR
GLOBAL 
SCROLL
STATIC
FOR
   SELECT PaymentID, PaymentDate FROM tblPayments
	  WHERE PaymentDate > '2009-01-01'

IF OBJECT_ID('dbo.tblLastPayments') IS NULL
BEGIN
CREATE TABLE tblLastPayments
(
	PaymentID INT PRIMARY KEY,
	PaymentDate DATETIME NOT NULL,
)
END	
GO

EXEC LastPayments

DEALLOCATE TableCursor