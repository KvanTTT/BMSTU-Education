#include "Tree.h"
#include "stdio.h"
#include "string.h"

Tree::Tree(char *FileName, Vector Position)
{
	FILE *f;

	fopen_s(&f, FileName,"r");
	if(f == NULL)
	{
		 return;
	}

	VertexCount = 0;
	const unsigned MaxCount = 64;
	char Buffer[MaxCount];
	Buffer[63] = '\0';

	unsigned i;
	while (!feof(f))
	{
		fgets(Buffer, MaxCount, f);
		if ((Buffer[0] == 'v') && (Buffer[1] == ' '))
			VertexCount++;
	}

	Vertices = new VertexData[VertexCount];
	fseek(f, 0, 0);

	char *stopstr;
	
	i = 0;
	while (!feof(f))
	{
		fgets(Buffer, MaxCount, f);
		if ((Buffer[0] == 'v') && (Buffer[1] == ' '))
		{
			Vertices[i].pos.x = (float)strtod(&Buffer[2], &stopstr) + Position.x;
			stopstr++;
			Vertices[i].pos.y = (float)strtod(stopstr, &stopstr) + Position.y;
			stopstr++;
			Vertices[i].pos.z = (float)strtod(stopstr, &stopstr) + Position.z;
			i++;
		}
	}	

	fclose(f);	
}

void Tree::Build(char *FileName, Vector Position)
{
	FILE *f;

	fopen_s(&f, FileName,"r");
	if(f == NULL)
	{
		 return;
	}

	VertexCount = 0;
	const unsigned MaxCount = 64;
	char Buffer[MaxCount];
	Buffer[63] = '\0';

	unsigned i;
	while (!feof(f))
	{
		fgets(Buffer, MaxCount, f);
		if ((Buffer[0] == 'v') && (Buffer[1] == ' '))
			VertexCount++;
	}

	Vertices = new VertexData[VertexCount];
	fseek(f, 0, 0);

	char *stopstr;
	
	i = 0;
	while (!feof(f))
	{
		fgets(Buffer, MaxCount, f);
		if ((Buffer[0] == 'v') && (Buffer[1] == ' '))
		{
			Vertices[i].pos.x = (float)strtod(&Buffer[2], &stopstr) + Position.x;
			stopstr++;
			Vertices[i].pos.y = (float)strtod(stopstr, &stopstr) + Position.y;
			stopstr++;
			Vertices[i].pos.z = (float)strtod(stopstr, &stopstr) + Position.z;
			i++;
		}
	}	

	fclose(f);	
}

Tree::~Tree()
{
	delete Vertices;
}