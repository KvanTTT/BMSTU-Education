// Type modeling (Lab 7).cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "conio.h"
#include "StringList.h"

int Menu(StringList &SL);


void main() // головная функция
{
    StringList SL;
	Init(SL);
    while (!Menu(SL));
	Clear(SL);
}


int Menu(StringList &SL) //вызов меню
{
    char str[256];
    StringList SLT;
    int Act;
    int Ind;
	int c;

    printf("0 - Enter string\n");
    printf("1 - Concat to left\n");
    printf("2 - Concat to right\n");
    printf("3 - Find SubStr Index\n");
	printf("4 - Delete SubStr\n");
    printf("5 - UPPER CASE\n");
    printf("6 - lower case\n");
	printf("7 - Compare\n");
	printf("8 - Copy\n");
	printf("9 - Reverse\n");
    printf("a - Clear\n");
	printf("b - Print string\n");
    printf("c - Exit\n\n");  

	/*printf("\n");
	Print(SL);
	printf("\n\n");		*/
    printf("Select Action: ");

    Act = getch();
    while (!((Act >= '0') && (Act <= '9')) && !((Act >= 'a') && (Act <= 'c')))
        Act = getch();
    putch(Act);
    printf("\n\n");

    switch (Act)
    {
    case '0':
        printf("Enter string:\n");
        scanf("%s", str);
        Init(SL, str);
        break;
    case '1':
        printf("Enter added left string:\n");
        scanf("%s", str);
        Init(SLT, str);
        ConcatLeft(SL, SLT);
        Clear(SLT);
		printf("\n");
        Print(SL);
        break;
    case '2':
        printf("Enter added right string:\n");
        scanf("%s", str);
        Init(SLT, str);
        ConcatRight(SL, SLT);
        Clear(SLT);
		printf("\n");
        Print(SL);
        break;
	case'3':
        printf("Enter find string:\n");
        scanf("%s", str);
        Init(SLT, str);
		try
		{
			Ind = SubStr(SL, SLT);
			if (Ind == -1) throw -1;
			printf("\nInd = %d", Ind);
		}
		catch (int e)
		{
			if (e == -1)
				printf("\nThese string does not contain particular substring");
		}
        printf("\n");
        Clear(SLT);
        Print(SL);
        break;
	 case '4':
        printf("Enter deleting substring:\n");
        scanf("%s", str);
        Init(SLT, str);
		try
		{
			Ind = DelSubStr(SL, SLT);
			if (Ind == -1) throw -1;
			printf("\nInd = %d", Ind);
		}
		catch (int e)
		{
			if (e == -1)
				printf("\nThese string does not contain particular substring");
		}
        printf("\n");
        Clear(SLT);
        Print(SL);
        break;
    case '5':
        UpperCase(SL);
		printf("\n");
        Print(SL);
        break;
    case '6':
        LowerCase(SL);
		printf("\n");
        Print(SL);
        break;
	case '7':
		printf("Enter comparing string:\n");
		scanf("%s", str);
		Init(SLT, str);
		c = Equal(SL, SLT);
		printf("\n");
		Print(SL);
		if (c == -1)
			printf(" < ");
		else
		if (!c)
			printf(" = ");
		else
			printf(" > ");		
		Print(SLT);
		Clear(SLT);
		break;
	case '8':
		printf("Enter new string:\n");
		scanf("%s", str);
		Clear(SL);
		Init(SL, str);
		printf("\n");
		break;
	case '9':
		Reverse(SL);
		Print(SL);
		printf("\n");
    case 'a':
		Clear(SL);
        break;
    case 'b':
        printf("\n");
        Print(SL);
        break;
    case 'c': 
        return 1;
        break;	   
    }
    printf("\n-----------------------------------------------------------\n\n");
    return 0;
}
