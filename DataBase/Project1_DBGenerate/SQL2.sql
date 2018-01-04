USE DistribComp;  -- Изменяет текущую БД для данной сессии

GO  -- Конец пакета

ALTER TABLE tblUsers  -- Модификация таблицы "Участники"
ADD  -- Добавление
CONSTRAINT PK_UserID PRIMARY KEY (UserID);
--CONSTRAINT UQ_UserName UNIQUE (UserName);

ALTER TABLE tblTasks  -- Модификация таблицы "Задания"
ADD  -- Добавление
CONSTRAINT PK_TaskID PRIMARY KEY (TaskID);
--CONSTRAINT CK_TaskUID CHECK (TaskUID LIKE '[0-F][0-9][0-9]')

ALTER TABLE tblPayments  -- Модификация таблицы "Начисления"
ADD  -- Добавление
CONSTRAINT PK_PaymentID PRIMARY KEY (PaymentID),
CONSTRAINT FK_UserID FOREIGN KEY (UserID) REFERENCES tblUsers (UserID),
CONSTRAINT FK_TaskID FOREIGN KEY (TaskID) REFERENCES tblTasks (TaskID);

GO  -- Конец пакета

CREATE DEFAULT DEF_Unknown AS 'Неизвестно';  -- Создание выражения по умолчанию

GO  -- Конец пакета

EXEC sp_bindefault 'DEF_Unknown', 'tblUsers.Team';  -- Связать поле с выражением по умолчанию
EXEC sp_bindefault 'DEF_Unknown', 'tblUsers.Country';  -- Связать поле с выражением по умолчанию

GO  -- Конец пакета

CREATE RULE RUL_GreaterOrEqualZero AS @value >= 0;  -- Создать правило

GO  -- Конец пакета

EXEC sp_bindrule 'RUL_GreaterOrEqualZero', 'tblTasks.TaskCost';  -- Связать поле с правилом
EXEC sp_bindrule 'RUL_GreaterOrEqualZero', 'tblPayments.PaymentCredit';  -- Связать поле с правилом

GO  -- Конец пакета