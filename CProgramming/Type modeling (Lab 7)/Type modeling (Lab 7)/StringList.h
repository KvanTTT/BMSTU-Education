#ifndef STRINGLIST_H
#define STRINGLIST_H

typedef unsigned int uint;

struct CharElem
{
	char Value;
	CharElem *NextElem;
	CharElem *PrevElem;
};

struct StringList 
{
	CharElem *FirstElem;
	CharElem *LastElem;
	uint Count;
};

void LowerCase(char &C);
void UpperCase(char &C);

void Init(StringList &SL);
void Init(StringList &SL, char *String);
void Clear(StringList &SL);
void Copy(StringList &ToSL, StringList &FromSL); 
void AddFirst(StringList &SL, char C);
void AddLast(StringList &SL, char C);
void DeleteFirst(StringList &SL);
void DeleteLast(StringList &SL);
int Equal(StringList &SL1, StringList &SL2);
int SubStr(StringList &SL, StringList &SubSL);
int DelSubStr(StringList &SL, StringList &SubSL);
void ConcatRight(StringList &ToSL, StringList &FromSL);
void ConcatLeft(StringList &ToSL, StringList &FromSL);
void Clear(StringList &SL);
void Print(StringList &SL);
void UpperCase(StringList &SL);
void LowerCase(StringList &SL);
void Reverse(StringList &SL);

#endif

	