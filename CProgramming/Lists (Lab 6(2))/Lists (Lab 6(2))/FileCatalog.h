typedef unsigned int uint;

struct FileCatalogElem
{
	char *Name;
	uint Data;
	uint AccsessCount;
	FileCatalogElem *NextFile;
};

struct FileCatalog
{
	FileCatalogElem *FirstFile;
	FileCatalogElem *LastFile;
	uint Count;
};

int rand(int LowValue, int HighValue);
char *DataToChar(uint Data);
bool CharToData(char *String, uint &Data);

void Init(FileCatalog &FC);
void Add(FileCatalog &FC, char *Name, uint Data, uint AccsessCount);
void Generate(FileCatalog &FC, uint Count, uint MinLength, uint MaxLength, uint MinAccess, uint MaxAccsess, uint MinData, uint MaxData);
void Print(FileCatalog &FC);
void DeleteEarlies(FileCatalog &FC, uint Data);
FileCatalogElem *MaxAccsessFile(FileCatalog &FC);

