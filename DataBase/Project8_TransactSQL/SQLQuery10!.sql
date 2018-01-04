SELECT ContractID, ContractWeeklySum
FROM tblContracts
WHERE ContractID < 100 AND EXISTS
(SELECT ContractWeeklySum
FROM tblContracts C
WHERE ContractID >= 100 AND
tblContracts.ContractWeeklySum = C.ContractWeeklySum)
ORDER BY ContractID