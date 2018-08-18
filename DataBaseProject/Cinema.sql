USE Cinema;
go

if OBJECT_ID('tblSoldTicket') is not null drop table tblSoldTicket
if OBJECT_ID('tblUser') is not null drop table tblUser
if OBJECT_ID('tblSeat') is not null drop table tblSeat
if OBJECT_ID('tblPrices') is not null drop table tblPrices
if OBJECT_ID('tblTicket') is not null drop table tblTicket
if OBJECT_ID('tblShow') is not null drop table tblShow
if OBJECT_ID('tblRoom') is not null drop table tblRoom
if OBJECT_ID('tblMovieCountry') is not null drop table tblMovieCountry
if OBJECT_ID('tblMovieGenre') is not null drop table tblMovieGenre
if OBJECT_ID('tblCountry') is not null drop table tblCountry
if OBJECT_ID('tblGenre') is not null drop table tblGenre
if OBJECT_ID('tblMovie') is not null drop table tblMovie


create table tblMovie
(
	MovieID int identity primary key,
	MovieName varchar (100) unique NOT NULL, 
	MovieDirector varchar (60), 
	MovieDuration int NOT NULL,
	MovieYear int,
	MinAge int,
	MainActor varchar (100),
)

create table tblGenre 
(
	GenreID int identity primary key, 
	GenreName varchar (50) unique NOT NULL
)

create table tblCountry 
(
	CountryID int identity primary key, 
	CountryName varchar (50) unique NOT NULL
)

create table tblMovieGenre 
(
	MovieID int NOT NULL,
	GenreID int NOT NULL,
	
	Constraint PK_MovieGenre primary key (MovieID, GenreID),
	Constraint FK_MMovieGenre foreign key (MovieID) references tblMovie (MovieID) on delete cascade,
	Constraint FK_CMovieGenre foreign key (GenreID) references tblGenre (GenreID) on delete cascade
	
)

create table tblMovieCountry
(
	MovieID int NOT NULL,
	CountryID int NOT NULL,
	
	Constraint PK_MovieCountry primary key (MovieID, CountryID),
	Constraint FK_MMovieCountry foreign key (MovieID) references tblMovie (MovieID) on delete cascade,
	Constraint FK_CMovieCountry foreign key (CountryID) references tblCountry (CountryID) on delete cascade
)

create table tblRoom
(
	RoomID int primary key,
	RowNumber int NOT NULL,
	SeatsNumber int NOT NULL,
	NumOfSeats int NOT NULL,
)

create table tblShow 
(
	ShowID int identity primary key, 
	MovieID int NOT NULL, 
	RoomID int NOT NULL,
	Date datetime NOT NULL, 
	Session datetime NOT NULL,
	NotVipPrice int NOT NULL,
	VipPrice int NOT NULL,
	NumOfSoldTickets int NOT NULL,
	Constraint FK_ShowMovie foreign key (MovieID) references tblMovie (MovieID) on delete cascade,
	Constraint FK_ShowRoom foreign key (RoomID) references tblRoom (RoomID) on delete cascade
)

create table tblTicket 
(
	TicketID int identity primary key,
	ShowID int, 
	Row int, 
	Seat int, 
	Status int, 
	Constraint FK_TicketShow foreign key (ShowID) references tblShow (ShowID) on delete cascade,
)


create table tblPrices 
(
	WeekDay int NOT NULL,
	SessionStart datetime NOT NULL,
	Sessionend datetime NOT NULL,
	NotVipPrice int NOT NULL, 
	VipPrice int NOT NULL, 
	primary key(WeekDay, SessionStart, Sessionend)
)

create table tblSeat 
(
	RoomID int NOT NULL,
	Row int NOT NULL,
	Seat int NOT NULL,
	Vip int NOT NULL, 
	primary key (RoomID, Row, Seat),
	Constraint FK_SeatRoom foreign key (RoomID) references tblRoom (RoomID) on delete cascade 
)

create table tblUser
(
	UserID int identity primary key, 
	Username varchar (20), 
	Password varchar (20), 
	Rights varchar (2) 
)

create table tblSoldTicket 
(
	SoldTicketID int identity primary key, 
	UserID int NOT NULL,
	TicketID int NOT NULL,
	Constraint FK_STU foreign key (UserID) references tblUser (UserID) on delete cascade, 
	Constraint FK_STT foreign key (TicketID) references tblTicket (TicketID) on delete cascade 
)

go
