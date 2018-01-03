// Lists (Lab 6(2)).cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "FileCatalog.h"
#include "conio.h"

int Menu(FileCatalog &FC);

void main()
{
	FileCatalog FC;
	Init(FC);

	while (!Menu(FC));										
}

int Menu(FileCatalog &FC)
{
		FileCatalogElem *MAF;

		printf("0 - Add File\n");
		printf("1 - Random Generate Catalog\n");
        printf("2 - Print Catalog\n");
        printf("3 - Delete Files with less data\n");
		printf("4 - Print Max Accses File\n");
		printf("5 - Exit\n");
		printf("Select Action: ");
		
		char Act = getch();
		while ((Act < '0') || (Act > '5'))
			Act = getch();
		putch(Act);
		printf("\n\n");

		switch (Act)
		{
			case '0':
				char Name[16];
				uint Data, AccsessCount;
				printf("Enter Name, Data, Accsess of File: ");
				scanf("%s %u %u", &Name, &Data, &AccsessCount);
				Add(FC, Name, Data, AccsessCount);
			break;
			case '1':
				uint Count, MinLength, MaxLength, MinAccessCount, MaxAccsessCount, MinDataCreation, MaxDataCreation;
				char MinDataCreationS[11], MaxDataCreationS[11];
				printf("Enter Count, MinLength, MaxLength, MinAccessCount, MaxAccsessCount, MinDataCreation, MaxDataCreation: ");
				scanf("%u %u %u %u %u %s %s", &Count, &MinLength, &MaxLength, &MinAccessCount, &MaxAccsessCount, &MinDataCreationS, &MaxDataCreationS);
				if (CharToData(MinDataCreationS, MinDataCreation) && CharToData(MaxDataCreationS, MaxDataCreation))
					Generate(FC, Count, MinLength, MaxLength, MinAccessCount, MaxAccsessCount, MinDataCreation, MaxDataCreation);
				else
					printf("Incorrect Input");
			break;
			case '2':
			    Print(FC);
			break;
			case '3':
				printf("Enter Data: ");
				scanf("%u", Data);
                DeleteEarlies(FC, Data);
			break;
			case '4':
                MAF = MaxAccsessFile(FC);
				printf("Max Accsess File:\n");
				printf("%s %u %u", MAF->Name, MAF->Data, MAF->AccsessCount);
			break;
			case '5':
				return 1;
			break;
		}
		printf("--------------------------------------------------------\n\n");
		return 0;
}

