if OBJECT_ID('CountPrice') is not null drop procedure CountPrice
if OBJECT_ID('ConcatCountries') is not null drop function ConcatCountries
if OBJECT_ID('ConcatGenres') is not null drop function ConcatGenres
if OBJECT_ID('CountCashM') is not null drop function CountCashM
if OBJECT_ID('CountCashPeriod') is not null drop function CountCashPeriod
if OBJECT_ID('isVipF') is not null drop function isVipF
if OBJECT_ID('hasShows') is not null drop function hasShows
if OBJECT_ID('countTicketPrice') is not null drop function countTicketPrice
if OBJECT_ID('FreePlaces') is not null drop function FreePlaces
if OBJECT_ID('MintoTime') is not null drop function MintoTime
if OBJECT_ID('isShowCorrect') is not null drop function isShowCorrect

if OBJECT_ID('onAddShow') is not null drop trigger onAddShow
if OBJECT_ID('ondeleteShow') is not null drop trigger ondeleteShow
if OBJECT_ID('onupdateTicket') is not null drop trigger onupdateTicket

if OBJECT_ID('ShowFilmsAfterCurrentTime') is not null drop view ShowFilmsAfterCurrentTime

go

create procedure CountPrice @ShowID int 
as
begin
	set DateFirst 1;
	declare @VipPrice int,
			@NotVipPrice int
	
	select top 1 @VipPrice=p.VipPrice, @NotVipPrice=p.NotVipPrice 
	from tblShow s join tblPrices p 
	on s.Session BETWEEN p.SessionStart AND p.Sessionend AND DATEPART(dw, s.Date)=p.WeekDay
	where s.ShowID=@ShowID

	update tblShow 
	set VipPrice=@VipPrice, NotVipPrice=@NotVipPrice
	where ShowID=@ShowID
end
go

create function ConcatCountries (@MovieID int)
	returnS varchar ( 200 )
as
begin
	declare @AllCountries varchar ( 200 ),
		@Country varchar ( 20 ) 

	declare mycursor cursor local for 
		select g.CountryName 
		from tblMovieCountry mg join tblCountry g
		on mg.CountryID = g.CountryID
		where mg.MovieID = @MovieID
		
	set @AllCountries = ''
	open mycursor fetch next from mycursor into @Country
	while @@FETCH_STATUS=0
	begin
		set @AllCountries = @AllCountries + @Country + ' '
		FETCH NEXT from MYCURSOR into @Country
	end
	return @AllCountries
end
go

create function ConcatGenres (@MovieID int)
	returns varchar ( 200 )
as
begin
	declare @AllGenres varchar ( 200 ),
		@Genre varchar ( 20 ) 

	declare mycursor cursor local for 
		select g.GenreName 
		from tblMovieGenre mg join tblGenre g
		on mg.GenreID = g.GenreID
		where mg.MovieID = @MovieID
		
	set @AllGenres = ''
	open mycursor fetch next from mycursor into @Genre
	while @@FETCH_STATUS=0
	begin
		set @AllGenres = @AllGenres + @Genre + ' '
		FETCH NEXT from MYCURSOR into @Genre
	end
	return @AllGenres
end
go

create function CountCashM (@MovieID int)
	returns int
as
begin
	declare @ShowID int, @row int, @seat int,
			@TicketPrice int,
		    @Sum int 

	set @Sum = 0;
	declare mycursor cursor local for 
		select s.ShowID , t.Row, t.Seat 
		from tblShow s join tblTicket t 
		on s.ShowID=t.ShowID
		where s.MovieID = @MovieID AND t.Status = 1
	open mycursor fetch next from mycursor into @ShowID, @row, @seat
	while @@FETCH_STATUS=0
	begin
		set @Sum = @Sum + dbo.countTicketPrice(@ShowID, @row, @seat)
	FETCH NEXT from MYCURSOR into @ShowID, @row, @seat
	end

	return @sum
end
go

create function CountCashPeriod (@Datefrom datetime, @DateTo datetime)
	returns int
as
begin
	declare @ShowID int, @row int, @seat int,
			@TicketPrice int,
		    @Sum int 

	set @Sum = 0;
	declare mycursor cursor local for 
		select s.ShowID , t.Row, t.Seat 
		from tblShow s join tblTicket t 
		on s.ShowID=t.ShowID
		where s.Date >= @Datefrom AND s.Date < @DateTo AND t.Status = 1
	open mycursor fetch next from mycursor into @ShowID, @row, @seat
	while @@FETCH_STATUS=0
	begin
		set @Sum = @Sum + dbo.countTicketPrice(@ShowID, @row, @seat)
	FETCH NEXT from MYCURSOR into @ShowID, @row, @seat
	end

	return @sum
end
go

create function IsVipF (@ShowID int, @Row int, @Seat int)
	returns bit
as
begin
	return
	(select top 1 seat.Vip 
	from tblSeat seat
	where seat.RoomID=(select top 1 RoomID from tblShow s where s.ShowID=@ShowID) 
														AND seat.Row=@Row AND seat.Seat=@Seat
	)
end
go

create function HasShows(@MovieID int)
	returns int
as
begin
	IF exists (select * from tblShow where MovieID=@MovieID)
		return 1
	return 0
end
go

create function CountTicketPrice (@ShowID int, @Row int, @Seat int)
	returns int
as
begin
	declare @isVip int, 
	       @Cost int 
	
	set @isVip = [Cinema].[dbo].[isVipF](@ShowID, @Row, @Seat)
	IF @isVip = 1
		set @Cost = (select top 1 s.VipPrice from tblShow s where s.ShowID=@ShowID)
	ELSE
		set @Cost = (select top 1 s.NotVipPrice from tblShow s where s.ShowID=@ShowID)
	
	return @Cost
end
go

create function FreePlaces(@ShowID int)
returns int
as
begin
	declare @Result int,
			@SoldTick int,
			@AllTick int 
	select top 1 @SoldTick = s.NumOfSoldTickets, @AllTick = r.NumOfSeats
	from tblShow s join tblRoom r on s.RoomID = r.RoomID
	where s.ShowID = @ShowID

	set @Result = @AllTick-@SoldTick
	return @Result	
end
go

create function MintoTime(@Min int)
	returns datetime
as
begin
	declare @Hour int,
			@Minute int,
			@Result datetime

	set @Hour = @Min / 60;
	set @Minute = @Min - @Hour * 60;

    set @Result = CasT (@Hour as varchar (3))+ ':' +CasT (@Minute as varchar (3));
	return @Result;
end
go

set ANSI_NULLS on
set QUOTED_IDENTIFIER on
go

create function IsShowCorrect (@NewRoomID int, @NewMovieID int, @NewDate datetime, @NewSession datetime)
returns int 
as
begin
	declare @NewDuration int
	set @NewDuration = (select top 1 m.MovieDuration from tblMovie m where m.MovieID = @NewMovieID)

	return (
		select top 1 s.ShowID 
		from tblShow s 
		join tblMovie m
		on m.MovieID = s.MovieID
		where (@NewRoomID = s.RoomID) AND (@NewDate = s.Date) AND
				( 
					( (@NewSession+[Cinema].[dbo].[MintoTime](@NewDuration) > s.Session) AND 
					(@NewSession+[Cinema].[dbo].[MintoTime](@NewDuration) < s.Session+[Cinema].[dbo].[MintoTime](m.MovieDuration))
					)
						OR
					 ( ( @NewSession > s.Session) AND (@NewSession < s.Session + [Cinema].[dbo].[MintoTime](m.MovieDuration)))
				)
			)

end
go

create trigger OnAddShow
on tblShow
for insert
as
begin
	declare @ShowID int, @row int, @seat int
	select @ShowID=ShowID from inserted
	declare mycursor cursor local for 
		select ShowID, Row, Seat from tblShow show join tblSeat seat on  seat.RoomID = show.RoomID
		where ShowID = @ShowID
	open mycursor fetch next from mycursor into @ShowID, @row, @seat
while @@FETCH_STATUS=0
begin
	INSERT into tblTicket(ShowID, Row, Seat, Status) VALUES (@ShowID, @row, @seat, 0)
FETCH NEXT from MYCURSOR into @ShowID, @row, @seat
end

EXEC CountPrice @ShowID
end
go

create trigger OnSeleteShow
on tblShow
for delete
as
begin
	declare @N int, @Date datetime
	select top 1 @N = NumOfSoldTickets, @Date=Date from deleted
	if ((@N > 0) AND (@Date>GETDATE()))
		rollback transaction
end
go

create trigger OnUpdateTicket
on tblTicket
for update
as
begin
	declare @ShowID int
	select @ShowID=ShowID from inserted
	update tblShow  set NumOfSoldTickets = NumOfSoldTickets + 1 where ShowID=@ShowID
end
go

create view ShowFilmsAfterCurrentTime as
	select min(MovieName) as Name, m.MovieID, min(ShowID) showid
    from tblMovie m LEFT OUTER join tblShow s
    on m.MovieID=s.MovieID
    where showid is not Null AND date >= GETDATE()
    group by m.MovieID
go
