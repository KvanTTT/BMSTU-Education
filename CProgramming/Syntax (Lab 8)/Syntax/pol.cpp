#include "stdafx.h"
#include "pol.h"
#include "stdlib.h"


char *replace(char *str, char *symbols, int *numbers)   //удаление из строки пробелов и
														// замена цифрами символов
{
	unsigned int i, j, k;
	char *str1 = new char[MAX_LENGTH];
	char *str2 = new char[MAX_LENGTH];
	int ind = 0;
	bool symb;
	char t[2];
	t[1] = '\0';
	char *n = new char[10];
	bool plus;

	for (i = 0; i < strlen(str); i++)
	{
		symb = false;
		for (j = 0; j < strlen(symbols); j++)
			if (str[i] == symbols[j])
			{
				itoa(numbers[j], n, 10);
				for (k = 0; n[k] != '\0'; k++)
				{
					str1[ind] = n[k];
					ind++;
				}
				symb = true;
				break;
			}
		if (symb) 
			continue;
		if ((str[i] == ' ') || (str[i] == '\t') || (str[i] == '\n'))
			continue;
		str1[ind] = str[i];	
		ind++;
	}
	str1[ind] = '\0';

	ind = 0;
	if ((str1[0] < '0') || (str1[0] > '9'))
	{
		str2[0] = '0';
		ind = 1;
	}
	for (i = 0; i < strlen(str1); i++)
	{
		if ((str1[i] == '+') || (str1[i] == '-'))
		{
			plus = true;
			for (k = i; ((str1[k] == '+') || (str1[k] == '-')); k++)
				if (str1[k] == '+')
					plus = !(plus ^ true);
				else
					plus = !(plus ^ false);
			if (plus)
				str2[ind] = '+';
			else
				str2[ind] = '-';
			ind++;
			i = k-1;
			continue;
		}
		str2[ind] = str1[i];
		ind++;
	}
	str2[ind] = '\0';


	delete str1;
	return str2;
}

double solve(char *str) // вычислние значния выражения
{
	_stack*  b = make_pol(str); // делаем постфиксную польскую нотацию из выражения

	double res = calc(b); // вычисляем выражение по польской нотации с извесными числами

	free_stack(b); // освобождаем стек

	return res;
}
//-------------------------------------------------------
_stack* make_pol(char* str)  // делаем постфиксную польскую нотацию из выражения
{
	_stack *operators = NULL,
		*operands = NULL,
		*deck = NULL;

	for(unsigned i = 0; i < strlen(str); i++)

		if (str[i]>='0' && str[i]<='9')
			operands = add_symbol( operands,find_number(str+i,i) );

		else if (str[i]==')')
		{
			for(operators = deck; operators && operators->oper!='(';)
			{
				operands = add_oper(operands, deck->oper);
				del_elem(deck,operators);
			}
			del_elem(deck,operators);
		}

		else
		{
			if(str[i]!='(')
				for(operators = deck; operators && prior(str[i])<=prior(operators->oper);)
				{
					operands = add_oper(operands,deck->oper);
					del_elem(deck,operators);
				}
				deck = add_oper(deck,str[i]);
		}


		for(operators = deck; operators; )
		{
			operands = add_oper(operands, deck->oper);
			del_elem(deck,operators);
		}

		deck = reverse_stack(operands);
		free_stack(operands);

		return deck;
}
//-------------------------------------------------------
double calc(_stack* stack)// вычисляем выражение по польской нотации с извесными числами
{
	_stack* beg = NULL;

	for(_stack* c = stack; c; c=c->next)

		if (!(c->idenf))
			beg = add_symbol(beg, c->zhach);

		else if (prior(c->oper)==3)
			beg = add_symbol(beg, unary(elem(beg),c->oper) );

		else
			beg = add_symbol(beg, binary(elem(beg),elem(beg),c->oper) );


		double result = beg->zhach;
		delete beg;

		return result;
}
//-------------------------------------------------------
double binary(double a, double b, char sym) // вычисление бинарной операции, 
											//  в зависимости от знака операции
{
	double binar;
	try
	{
		switch (sym)
		{
		case '+': binar = a+b; break;
		case '*': binar = a*b; break;
		case '-': binar = a-b; break;
		case '/': 
			if (b == 0) throw DIVISION_BY_ZERO;
			binar = a/b; 
		break;
		}
	}
	catch (int a)
	{
		if (a == 1)
		{
			printf("\nDivision by zero");
			binar = 0;
			//exit(0);
		}
	}

	return binar;
}
//-------------------------------------------------------
double unary(double a, char sym)
											// вычисление унарной операции, 
											//  в зависимости от знака операции
{
	double unar;
	switch (sym)
	{
	case 's': unar = sin(a); break;
	case 'c': unar = cos(a); break;
	case 'r': unar = a*a;    break;
	case 'k': unar = sqrt(a);break;
	}

	return unar;
}
//-------------------------------------------------------
int prior(char sym)
{
	int pri;
	switch (sym)
	{
	case '(':           pri = 0; break;

	case '+': case '-': pri = 1; break;

	case '*': case '/': pri = 2; break;

	case 's': case 'c':
	case 'r': case 'k': pri = 3; break;
	}

	return pri;
}
//-------------------------------------------------------
double find_number(char* sb, unsigned& i)
{
	char* u = new char[MAX_LENGTH];
	u[0] = 0;

	for(int j = 0; sb[j]>='0' && sb[j]<='9' || sb[j]=='.'; j++)
	{
		u[strlen(u)+1] = 0;
		u[strlen(u)] = sb[j];
	}

	i += strlen(u)-1;

	double  qw = atof(u);
	delete u;

	return qw;
}
