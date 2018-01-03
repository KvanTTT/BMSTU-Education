#include "stdafx.h"
#include "pol.h"
#include <iostream>

using namespace std;

int enter_size();
char *enter_symbols(int &size);
int *enter_numbers(int &size);
char *enter_expression();


void main(void)
{
	int size;
	char *str = NULL;
	char *dstr = NULL;
	char *symbols = NULL;
	int *numbers = NULL;
	
	while (1)
	{    
		size = enter_size();
		symbols = enter_symbols(size);
		numbers = enter_numbers(size);
		str = enter_expression();
		dstr = replace(str, symbols, numbers);

		cout << "\nExpression with replaced coefs and deleted spaces:  " << dstr;
		cout << "\nResult = " << solve(dstr);
		cout << "\n**************************************************\n\n";

		delete str;
		delete symbols;
	}
}

int enter_size()
{
	int result;
	cout << "Enter count of symbols: ";
	cin >> result;
	return result;
}

char *enter_symbols(int &size)
{
	char *result = new char[size];

	result = new char[size];
	cout << "Enter " << size << " symbols: ";
	for (int i = 0; i < size; i++)
		cin >> result[i];

	return result;
}

int *enter_numbers(int &size)
{
	int *result = new int[size];

	cout << "Enter " << size << " coefs: ";
	for (int i = 0; i < size; i++)
		cin >> result[i];

	return result;
}

char *enter_expression()
{
	char *result = new char[MAX_LENGTH];

	cout << "Enter expression: ";
	cin >> result;
	//*dstr = replace(str, symbols, numbers);		

	return result;
}