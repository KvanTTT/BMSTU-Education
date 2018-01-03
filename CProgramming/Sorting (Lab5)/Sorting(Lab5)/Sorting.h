
inline int *InitArr(int Size, int Bottom, int Top);
void PrintArr(int *Arr, int Size, bool Numbers = false);
int *Copy(int *Arr, int Size);
void Reverse(int *Arr, int Size);

//--SORTS-----------------------------------------------
inline void QuickSort(int *Arr, int Size);
void QSort(int *Arr, int BottomInd, int TopInd);

void QSort2(int* Arr, int a, int b);
int partition(int* Arr, int a, int b); 
inline void QuickSort2(int* Arr, int Size); 

inline void MergeSort(int *Arr, int Size);
inline int *Merge(int *m1, int *m2, int l1, int l2);

int increment(int* inc, int size);
inline void ShellSort(int *Arr, int Size);

inline void DigitalSort(int *Arr, int Size);

inline void InsertSort(int  *Arr, int Size);

//--ALL SORTS--------------------------------------------
 void Sort(void (*Func)(int *, int), int Number, int *Arr, int Size);

void AllSortsRandom(int *Arr, int Size);
void AllSortsSorted(int *SortedArr, int Size);
void AllSortsReverseSorted(int *ReverseSortedArr, int Size);