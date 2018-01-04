SELECT PaymentID, UserID, TaskID
FROM tblPayments
WHERE ((PaymentDate >= '2006-01-01') AND (PaymentDate <= '2009-12-31')) OR (PaymentCredit > 220)