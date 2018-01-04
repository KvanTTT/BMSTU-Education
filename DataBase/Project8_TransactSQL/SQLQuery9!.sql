SELECT ContractID, ContractWeeklySum
FROM tblContracts
WHERE ContractWeeklySum = SOME
(SELECT C.ContractWeeklySum
FROM tblContracts C
WHERE tblContracts.ContractWeeklySum = C.ContractWeeklySum AND
      tblContracts.ContractID <> C.ContractID)

ORDER BY ContractWeeklySum