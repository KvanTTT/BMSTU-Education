CREATE DATABASE DistribComp;  -- Создание базы данных "Распределенные вычисления"

GO  -- Конец пакета

USE DistribComp;  -- Изменяет текущую БД для данной сессии

GO  -- Конец пакета

CREATE TABLE tblUsers  -- Создать таблицу "Участники"
(
	UserID INT NOT NULL,  -- Идентификатор участника
	UserName VARCHAR (60) NOT NULL, -- Имя участника
	Team VARCHAR (60),  -- Команда участника
	Country CHAR (20),  -- Страна
	Project VARCHAR (30) NOT NULL, -- Специальность
	RegisterDate DATETIME NOT NULL, -- Дата регистрации
	AvgScorePerDay FLOAT NOT NULL, -- Количество набранных очков в день
	AllScore INT NOT NULL -- Количество набранных очков всего
);

CREATE TABLE tblTasks  -- Создать таблицу "Задания"
(
	TaskID INT NOT NULL,  -- Идентификатор задания
	TaskName VARCHAR (32) NOT NULL,  -- UID задания
	TaskCost FLOAT NOT NULL,  -- Стоимость
	TaskStartDate DATETIME NOT NULL,  -- Дата запуска задания
	TaskFinishDate DATETIME NOT NULL  -- Дата завершения задания
);

CREATE TABLE tblPayments  -- Создать таблицу "Начисления"
(
	PaymentID INT NOT NULL,  -- Идентификатор начисления
	UserID INT NOT NULL,  -- Идентификатор пользователя (КОМУ ВЫПЛАТА)
	TaskID INT NOT NULL,  -- Идентификатор проекта(задания) (ЗА КАКОЙ ПРОЕКТ)
	PaymentDate DATETIME NOT NULL,  -- Дата выплаты
	PaymentCredit MONEY NOT NULL  -- Сумма выплаты
);

GO  -- Конец пакета