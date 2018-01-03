#include "stdafx.h"
#include "VideoCameraList.h"

void Init(VideoCameraList &VCL)
{
	VCL.Camera = NULL;
	VCL.LastCamera = NULL;
	VCL.Count = 0;
}

void Clear(VideoCameraList &VCL)
{
	VideoCamera *T = VCL.Camera, *T1;
	while (T != NULL)
	{
        T1 = T->NextCamera;
		delete T;
		T = T1;
	}
	VCL.Camera = NULL;
	VCL.LastCamera = NULL;
	VCL.Count = 0;
	printf("\nList is Empty\n");
}

		
void AddToEnd(VideoCameraList &VCL, VideoCamera &NewCamera)
{
	if (VCL.Count == 0)
	{
		VCL.Camera = new VideoCamera;
		*(VCL.Camera) = NewCamera;
		VCL.Camera->NextCamera = NULL;
        VCL.LastCamera = VCL.Camera;
		VCL.Count = 1;
	}
	else
	{
		VideoCamera *T = new VideoCamera;
		*T = NewCamera;
		T->NextCamera = NULL;
		VCL.LastCamera->NextCamera = T;
		VCL.LastCamera = T;
		VCL.Count++;
	}
}

void AddToBegin(VideoCameraList &VCL, VideoCamera &NewCamera)
{
	if (VCL.Count == 0)
	{
		VCL.Camera = new VideoCamera;
		*(VCL.Camera) = NewCamera;
		VCL.Camera->NextCamera = NULL;
        VCL.LastCamera = VCL.Camera;
		VCL.Count = 1;
	}
	else
	{
		VideoCamera *T = new VideoCamera;
		*T = NewCamera;
        T->NextCamera = VCL.Camera;
		VCL.Camera = T;
		VCL.Count++;
	}
}

void DeleteFromBegin(VideoCameraList &VCL)
{
    if (VCL.Camera != NULL)
    {
        VideoCamera *T = VCL.Camera->NextCamera;
        delete VCL.Camera;
        VCL.Camera = T;
        VCL.Count--;
    }
}


void Print(VideoCameraList &VCL)
{
	VideoCamera *T = VCL.Camera;
	int I = 1;
	printf("\nAll Cameras:\n");
	printf("-----------------------------------------------------------\n");
	printf("    %11s | %11s | %11s | %11s |\n", "Name", "Country", "Resolution", "Cost");
	printf("-----------------------------------------------------------\n");
	while (T != NULL)
	{
		printf("%2i. %11s | %11s | %11i | %11i |\n", I, T->Name, T->Country, T->Resolution, T->Cost);
		T = T->NextCamera;
		I++;
	}
}

bool Greater(char Word1[], char Word2[])
{
	char *T1 = Word1;
	char *T2 = Word2;
	while ((*T1 != '\0') && (*T2 != '\0'))
	{
		if (*T1 > *T2)
			return true;
		else
		if (*T1 < *T2)
			return false;
		T1++;
		T2++;
	}
	if ((*T1 != '\0') && (*T2 == '\0'))
		return true;
	else
		return false;
}

bool Equal(char Word1[], char Word2[])
{
	char *T1 = Word1;
	char *T2 = Word2;
	while ((*T1 != '\0') && (*T2 != '\0'))
	{
		if (*T1 != *T2)
			return false;
		T1++;
		T2++;
	}
	if ((*T1 == '\0') && (*T2 == '\0'))
		return true;
	else
		return false;
}

void InverseSortByName(VideoCameraList &VCL)
{	
	VideoCamera *T = VCL.Camera, *T1;
	VideoCamera *MaxName, Temp;
    VideoCamera *C;
	while (T != NULL)
	{
		T1 = T;
		MaxName = T;
		while (T1 != NULL)
		{
			if (Greater(T1->Name, MaxName->Name))
				MaxName = T1;
			T1 = T1->NextCamera;
		}


        Temp = *T;
        C = T->NextCamera;
        *T = *MaxName;
        T->NextCamera = C;
        C = MaxName->NextCamera;
        *MaxName = Temp;
        MaxName->NextCamera = C; 

		T = T->NextCamera;
	}
}

void SearchByResolution(VideoCameraList &VCL, int Resolution)
{
    VideoCamera *T = VCL.Camera;
    int I = 0;

    printf("\nCameras with %i Resolution:\n", Resolution);
    while (T != NULL)
    {
        if (T->Resolution == Resolution)
        {
            printf("%i. %s %s %i %i\n", I, T->Name, T->Country, T->Resolution, T->Cost);
            I++;
        }
        T = T->NextCamera;
    }
}

void SearchByCost(VideoCameraList &VCL, int DownCost, int UpCost)
{
    VideoCamera *T = VCL.Camera;
    int I = 0;

    printf("\nCameras with %i to %i costs:\n", DownCost, UpCost);
    while (T != NULL)
    {
        if ((T->Cost >= DownCost) && (T->Cost <= UpCost))
        {
            printf("%i. %s %s %i %i\n", I, T->Name, T->Country, T->Resolution, T->Cost);
            I++;
        }
        T = T->NextCamera;
    }
}

void DeleteAllChinese(VideoCameraList &VCL)
{
	VideoCamera *T = VCL.Camera->NextCamera, *T1 = VCL.Camera;
    while (T != NULL)
    {
        if (Equal(T->Country, "china"))
        {
				T1->NextCamera = T->NextCamera;
				delete T;
				VCL.Count--;
				T = T1->NextCamera;
				continue;
        }
        T1 = T;
        T = T->NextCamera;
    }
	if (Equal(VCL.Camera->Country, "china"))
	{
		T = VCL.Camera->NextCamera;
		delete VCL.Camera;
		VCL.Count--;
		VCL.Camera = T;
	}
}