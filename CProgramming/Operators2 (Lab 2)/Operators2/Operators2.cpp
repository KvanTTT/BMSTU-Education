// Operators2.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

// Часть 2. Решение простых задач через битовую арифметику.
// Подключаем заголовочные файлы
#include <stdio.h>
#include <string>
#include <iostream>
#include <conio.h>

// Используем пространство имен std (для cin и cout)
using namespace std;

// Прототипы функций
void row();				// Новая строка с нумерацией
void readkey();			// Ждем нажатия новой клавиши
string ru(const string);	// Перекодировка ANSI -> OEM

// Прототипы функций, входящих в решение задачи
string XorCrypt(string str, char Key);
void SetBit(int &A, int B);
bool GetBit(int A, int B);
void ClearBit(int &A, int B);
bool Odd(int A);
unsigned int Mod(unsigned int A, unsigned int B);
int Mul(int A, int B);
unsigned int Div(unsigned int A, unsigned int B);
string Binary(int A);

void main()
{
	// Целые числа для экспериментов
int x, y, z;


// На основе свойства (A ^ B ^ B == A) 
// можно написать простейшую программу для шифрования,
// которая шифрует строку символов по ключу и потом ее расшифровывает!
// Введите строку в массив char mas[255], Введите любой ключ char Key, 
// например Key = 66.
// Далее пробегая всю строку от начала до символа '\0' делайте операцию 
// mas[i] = mas[i] ^ Key 
// или mas[i] ^= Key (по аналогии a += b значит то же, что и a = a + b).
// После этого выведите на экран шифрованную строку.
// Далее проделайте шифрование еще раз с ТЕМ ЖЕ ключом, и вы получите расшифрованную // строку.
// Выведите расшифрованную строку на экран, чтобы убедиться в правильности 
// шифрования.
// Для работы со строками можно использовать не только C-массив char[], но и класс 
// string
// Для работы со строками в C++ надо сделать #include <string>
// Объявляется строка как string ИМЯ_СТРОКИ
// string - это класс! Например, размер строки можно узнать, вызвав метод Size()
// Имя_Строки.Size()
// Пример работы со строкой - функция string ru(string)
// Написать функцию string XorCrypt(string str, char Key) для шифрования/расшифровки

row();	// 1 строка
string str;
cout << "Enter string: ";
int i = -1;
str.resize(80);
char c;
do
{
i++;
str[i] = cin.get();
}
while (str[i] != 10);
str.resize(i);

str = XorCrypt(str, 234);
cout << "Encoding string: ";
cout << str;
str = ru(str);
str = XorCrypt(str, 234);
cout << "\nDecoding string: ";
cout << str << '\n';


/*
char mas[255];
char Key;
int i;

cout << ru("\nXOR-шифрование\nВведите исходную строку:\n");
cin.get();
cin.getline(&mas[0], 255, '\n');	// Читаем строку в массив mas

cout << ru("Введите ключ: ");
cin >> Key;

string STR(mas);	// Создаем объект-строку на основе массива mas
STR = XorCrypt(STR, Key);
cout << ru("\nШифрованная строка: \n") << STR;

STR = XorCrypt(STR, Key);
cout << ru("\nРасшифрованная строка: \n") << STR;
*/


// На основе полученных навыков работы с битами написать функции 
// SetBit(Установка бита) и GetBit(Чтение бита) и ClearBit(Очищение бита).

// void SetBit(int &A, int B);
//  A - это переменная, в которой будут устанавливаться биты,
//  B - это НОМЕР бита(начиная от нуля), который следует установить.

// bool GetBit(int A, int B);
//  A - это переменная, из которой читаются биты, 
//  B - это НОМЕР читаемого бита(начиная от нуля), а 
//  результат функции - это true(если бит с порядком B равен 1) и 
//  false(если бит с порядком B равен 0)

// void ClearBit(int &A, int B);
//  A - это переменная, в которой будет очищаться бит,
//  B - это НОМЕР бита(начиная от нуля), который следует очистить.

// ! Подсказка: 
// Для создания флага используйте 1<<.., 
// установка/сброс - &
// чтение - |

// Использование. Например: 
int k = 0;
row();	// 2 строка


SetBit(k, 2);
SetBit(k, 4);
cout << ru("\nТекущие биты: ");
for(int j = 0; j < (sizeof(k) << 3); j++)
	if(GetBit(k, j))
		cout << j << " ";
cout << endl;

// Ставим биты
SetBit(k, 0);
SetBit(k, 31);
SetBit(k, 11);
SetBit(k, 5);

ClearBit(k, 4);
ClearBit(k, 2);
cout << ru("Новые биты: ");
for(int j = 0; j < (sizeof(k) << 3); j++)
	if(GetBit(k, j))
		cout << j << " ";
cout << endl;


// Используя функции SetBit и GetBit реализовать функцию Odd(A).
// bool Odd(int A);
// Функция возвращает true, если число A нечетное, и false если оно четное
cout << "\nEnter int number: ";
cin >> k;
cout << "\n" << k << " is ";
(Odd(k)) ? (cout << "even") : (cout << "odd");
cout << '\n';

row();	// 3 строка

// Реализовать операцию остатка от деления типа unsigned int
// на тип unsigned int, не используя операции %,*,/
// Операции должны быть на уровне бит.
// Написать функцию unsigned int Mod(unsigned int A, unsigned int B);
int A, B;
cout << "Enter A, B: ";
cin >> A >> B;
cout << ru("Остаток от деления ") << A << ru(" на ") << B << " = " << Mod(A, B);
cout << '\n';

row();	// 4 строка


// Реализовать операцию целочисленного знакового умножения 
// числа типа int на число типа int, не используя операции %,*,/
// Операции должны быть на уровне бит.
// Написать функцию int Mul(int A, int B);
cout << "Enter A, B: ";
cin >> A >> B;
cout << A << " * " << B << " = " << Mul(A, B);
cout << '\n';

row();	// 5 строка


// Реализовать операцию целочисленного беззнакового деления
// числа типа unsigned int на число типа unsigned int,
// не используя операции %,*,/
// Операции должны быть на уровне бит.
// Написать функцию unsigned int Div(unsigned int A, unsigned int B);
// При делении может возникнуть исключение - деление на 0.

// Для его обработки используется конструкция try {...} catch(...) {...}
// Где try {...} - Это возможное место исключения
// catch(...) {...} - Это место обработки исключения. В (...) задается
// определение исключения. А само исключение генерируется командой throw
// Например:
// try {...
//		if(...) throw "Error";		// Передаем строку, т.е. char*
// } catch(char* str)				// Получаем строку str при генерации
// {... cout << str; ...}

// Возможное начало функции Div:
// if(B == 0) 
// {
//  throw "\n#Error: Division by zero!\n";
//	return 0;
// }
cout << "Enter A, B: ";
unsigned int C;
cin >> A >> B;
try
{
	C = Div(A, B);
	cout << "Div( " << A << ", " << B << " ) = " << C;
}
catch(char* str)
{
	cout << str;
}
cout << '\n';

row();	// 6 строка

/*
cout << "A, B: ";
cin >> a >> b;
try
{
	c = Div(a, b);
	cout << "Div( " << a << ", " << b << " ) = " << c;
}
catch(char* str)
{
	cout << str;
}
*/

// Конец =)
    readkey();
}



void row()
{
	static int i = 0;
	cout.flags(ios::dec);
	cout << "\n[" << ++i << "]: ";
}



void readkey()
{	
	cout << ru("\nНажмите любую клавишу для продолжения...");
	getch();
}



string ru(const string arg)
{
    	string s = arg;
	unsigned int i, l;
	l = arg.size();
	for (i = 0; i <= l; i++)
	{
		switch (s[i])
		{
		    case 'ё': s[i] = 240; break;
		    case 'Ё': s[i] = 241; break;
		    case '№': s[i] = 252; break;
		}
		
		if ((s[i] >= 'А') & (s[i] <= 'п'))   s[i] = s[i] - 64;
		if ((s[i] >= 'р') & (s[i] <= 'я'))   s[i] = s[i] - 16;
	}
	return s;
}

string XorCrypt(string str, char Key)
{
	int i;
	for (i=0; i < str.length(); i++)
		str[i] ^= Key;
	return str;
}

void SetBit(int &A, int B)
//  A - это переменная, в которой будут устанавливаться биты,
//  B - это НОМЕР бита(начиная от нуля), который следует установить.
{
	A = A | (1<<B);
}

bool GetBit(int A, int B)
//  A - это переменная, из которой читаются биты, 
//  B - это НОМЕР читаемого бита(начиная от нуля), а 
//  результат функции - это true(если бит с порядком B равен 1) и 
//  false(если бит с порядком B равен 0)
{
	return ((A >> B) & 1); 
}

void ClearBit(int &A, int B)
//  A - это переменная, в которой будет очищаться бит,
//  B - это НОМЕР бита(начиная от нуля), который следует очистить.
{
	A = A & (~(1<<B));
}

bool Odd(int A)
{
	if (GetBit(A, 0))	
		return false;
	else
		return true;
}

unsigned int Mod(unsigned int A, unsigned int B)
{
	unsigned int c = 1;
	unsigned int d;
	while (B*c <= A)
		c++;
	c--;
	d = A - B*c;
	return d;
}

int Mul(int A, int B)
{
	int result = 0;
	int t = 1;
	int k;

	for (int i=0; i<32; i++)
	{
		t = 1 << i;
		k = (B & t) >> i;
		if (k != 0)
			k = 0XFFFFFFFF;
		result = result + ((A & k) << i);		
	}
	return result;	
}

unsigned int Div(unsigned int A, unsigned int B)
{
	unsigned int c = 1;
	unsigned int d;
	try 
	{
		if (B == 0) throw "\nDivision by zero!";
		while (B*c <= A)
			c++;
		c--;
		return c;
	}
	catch (char* ch)
	{
		cout << ch;
	}
}

string Binary(int A)
{
	string s;
	s.resize(32);
	for (int i=0; i<32; i++)
	{
		if ((A & (-2147483648 >> i)) == 0)
			s[i] = '0';
		else
			s[i] = '1';
	}
	return s;
}

