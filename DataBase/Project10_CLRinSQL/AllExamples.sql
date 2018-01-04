
exec procedure1
go

exec procedure2 'NewName', 'folding@home', '2010-01-01'
go

declare @x1 int
exec procedure3 2008, @x1 out
print @x1
go

declare @x2 money
exec procedure4 @x2
print @x2
go

exec procedure5
go

declare @s float
set @s = dbo.function1()
print @s
go

select * from dbo.function3()
go