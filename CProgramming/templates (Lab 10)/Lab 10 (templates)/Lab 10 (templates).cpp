// Lab 10 (templates).cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "ArrayProcessing.cpp"
#include <iostream>
#include "conio.h"

using namespace std;


int main()
{
	int Size;
	int t;

	while (1)
	{
		cout << "Enter count of elements: ";
		cin >> Size;
		cout << "Enter type of array(0-int; 1-float; 2-double): ";
		cin >> t;
		switch (t)
		{
			case 0:
				int *ArI;
				ArI = new int[Size];
				InputProcessOutput(ArI, Size);
				delete ArI;
			break;
			case 1:
				float *ArF;
				ArF = new float[Size];
				InputProcessOutput(ArF, Size);
				delete ArF;
			break;
			case 2:
				double *ArD;
				ArD = new double[Size];
				InputProcessOutput(ArD, Size);
				delete ArD;
			break;
		}
		cout << "\n\n";
	}
	return 0;
}

