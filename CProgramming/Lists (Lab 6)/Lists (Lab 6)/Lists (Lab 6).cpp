// Lists (Lab 6).cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "conio.h"
#include "VideoCameraList.h"

int Menu(VideoCameraList &VCL);

void main()
{
	VideoCameraList VCL;
	Init(VCL);

	while (!Menu(VCL));										
}

int Menu(VideoCameraList &VCL)
{
		int Act;
		int Resolution;
		int UpCost, DownCost;
		VideoCamera Elem;

		printf("0 - Add to Begin\n");
        printf("1 - Add to End\n");
        printf("2 - Delete from Begin\n");
		printf("3 - PrintList\n");
		printf("4 - Inverse Sort by Name\n");
		printf("5 - Search by Resolution\n");
		printf("6 - Search by Cost\n");
		printf("7 - Delete All Chinese\n");
		printf("8 - Clear All\n");
		printf("9 - Exit\n\n");
		printf("Select Action: ");
		
		Act = getch();
		while ((Act < '0') || (Act > '9'))
			Act = getch();
		putch(Act);
		printf("\n\n");

		switch (Act)
		{
			case '0':
				printf("Enter Name, Country, Resolution, Cost: ");
				scanf("%s %s %i %i", &Elem.Name, &Elem.Country, &Elem.Resolution, &Elem.Cost);
				AddToBegin(VCL, Elem);
			break;
			case '1':
				printf("Enter Name, Country, Resolution, Cost: ");
				scanf("%s %s %i %i", &Elem.Name, &Elem.Country, &Elem.Resolution, &Elem.Cost);
				AddToEnd(VCL, Elem);				
			break;
			case '2':
			    DeleteFromBegin(VCL);	
			break;
			case '3':
                Print(VCL);
			break;
			case '4':
                InverseSortByName(VCL);
			break;
			case '5':
                printf("Enter Resolution: ");
                scanf("%i", &Resolution);
                SearchByResolution(VCL, Resolution);
			break;
			case '6':
                printf("Enter Down and Up Costs: ");
                scanf("%i %i", &UpCost, &DownCost);
                SearchByCost(VCL, UpCost, DownCost);
			break;
            case '7':
                DeleteAllChinese(VCL);
			break;
			case '8':
				Clear(VCL);
			break;
			case '9':
				printf("\n\n");
				return 1;
			break;
		}
		printf("-----------------------------------------------------------\n\n");
		return 0;
}

