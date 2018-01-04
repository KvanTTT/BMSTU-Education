goto BEGIN

**********************************************************
usage: bcp {dbtable | query} {in | out | queryout | format} datafile
  [-m maxerrors]            [-f formatfile]          [-e errfile]
  [-F firstrow]             [-L lastrow]             [-b batchsize]
  [-n native type]          [-c character type]      [-w wide character type]
  [-N keep non-text native] [-V file format version] [-q quoted identifier]
  [-C code page specifier]  [-t field terminator]    [-r row terminator]
  [-i inputfile]            [-o outfile]             [-a packetsize]
  [-S server name]          [-U username]            [-P password]
  [-T trusted connection]   [-v version]             [-R regional enable]
  [-k keep null values]     [-E keep identity values]
  [-h "load hints"]         [-x generate xml format file]


%1.dbo.%2 -- ИМЯ_БАЗЫ_ДАННЫХ.dbo.ИМЯ_ТАБЛИЦЫ - Полностью квалифицированное имя таблицы
in -- Направление копирования из файла в таблицу
%3 -- Файл, из которого данные копируются в таблицу
-S ИМЯ_СЕРВЕРА
-T -- Доверительное подключение, не требуется ввод имени пользователя и пароля
-C ACP -- Спецификатор кодовой таблицы, ACP - ANSI CODE PAGE
-c -- Входной файл является текстовым файлом
-t -- Разделитель полей
-r -- Разделитель строк
**********************************************************

:BEGIN
bcp %1.dbo.%2 in %3 -S (local)\SQLEXPRESS -T -C ACP -c -t"\t" -r"\n"

pause