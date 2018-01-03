struct VideoCamera
{
	char Name[16];
	char Country[16];
	int Resolution;
	int Cost;
	VideoCamera *NextCamera;
};

struct VideoCameraList
{
	VideoCamera *Camera;
	VideoCamera *LastCamera;
	int Count;
};

void Init(VideoCameraList &VCL);
void Clear(VideoCameraList &VCL);
void AddToEnd(VideoCameraList &VCL, VideoCamera &NewCamera);
void AddToBegin(VideoCameraList &VCL, VideoCamera &NewCamera);
void DeleteFromBegin(VideoCameraList &VCL);
void Print(VideoCameraList &VCL);
void PrintMenu();
void SearchByResolution(VideoCameraList &VCL, int Resolution);
void SearchByCost(VideoCameraList &VCL, int DownCost, int UpCost);
void DeleteAllChinese(VideoCameraList &VCL);
void InverseSortByName(VideoCameraList &VCL);

bool Greater(char Word1[], char Word2[]);
bool Equal(char Word1[], char Word2[]);