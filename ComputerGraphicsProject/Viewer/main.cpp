#pragma comment _CRT_SECURE_NO_WARNINGS

// includes
#include "Engine.h"
#include "DynamicCamera.h"
#include "Terrain.h"
#include "Font.h"

#include "Render.h"

#include "stdio.h"

static Texture			 texHeightmap, texTerrain, texDetail, texLightmap;
static Texture			 texSkyBox[6];
static DynamicCamera	*DynCam;
static Terrain			*pTerrain;
float					 g_frameTime = 0;

static bool RandGen = false;
static float waterHeight = 0.1f;
static bool Snap = false;
static float FOV = 60;
static float rand_seed = 0.02f;

struct Settings
{
	char SkyFolder[64];
	char LandscapeFolder[64];
};

Settings set;

GLuint filter;                          // Используемый фильтр для текстур 
GLuint fogMode[]= { GL_EXP, GL_EXP2, GL_LINEAR }; // Хранит три типа тумана
GLuint fogfilter= 0;                    // Тип используемого тумана
GLfloat fogColor[4]= {0.5f, 0.5f, 0.5f, 1.0f}; // Цвет тумана


static char	titleBar[1024];
char *appGetTitleBar()      {return titleBar;}

void DeleteEnterChars(char *Str)
{
	int i = 0;
	while (Str[i] != (char)0)
	{
		if (Str[i] == (char)10)
			Str[i] = (char)0;
		i++;
	}
}

void LoadTexture(Texture *pTex, char *pName, char *pName2=0)
{
	Image imgTmp;

	char Ext[10];
	strcpy_s(Ext, strrchr(pName, (int)'.'));
	
	if (!strcmp(Ext, ".bmp"))
		imgTmp.Create(pName, imgLoadBMP);
	else
	if (!strcmp(Ext, ".jpg") || strcmp(Ext, ".jpeg") || strcmp(Ext, ".jpe"))
		imgTmp.Create(pName, imgLoadJPG);

	if (pName2)
	{
		Image imgTmp2;
		imgTmp2.Create(pName2, imgLoadBMP);
		pTex->Create( &imgTmp, &imgTmp2 ); 
		imgTmp.Free();
	}
	else
		pTex->Create( &imgTmp ); 

	imgTmp.Free();
}

void LoadSettings(char *FileName)
{
	FILE *f;

	fopen_s(&f, FileName, "r");
	if (f == NULL)
		return;

	char tempStr[128];
	fgets(set.LandscapeFolder, 64, f);
	DeleteEnterChars(set.LandscapeFolder);
	fgets(set.SkyFolder, 64, f);
	DeleteEnterChars(set.SkyFolder);

	strcpy(tempStr, set.LandscapeFolder);
	LoadTexture(&texTerrain, strcat(tempStr, "Texture.bmp"));

	strcpy(tempStr, set.LandscapeFolder);
	LoadTexture(&texLightmap, strcat(tempStr, "Lightmap.bmp"));

	char number[128];
	for(int i = 0; i < 6; i++)
	{
		strcpy(tempStr, set.SkyFolder);
		number[0] = (char)(i+48);
		number[1] = '.';
		number[2] = 'b';
		number[3] = 'm';
		number[4] = 'p';
		number[5] = (char)0;
		//strcat(number, ".bmp");
		strcat(tempStr, number);
		LoadTexture(&texSkyBox[i], tempStr);
	}
	

	pTerrain = new Terrain();
	strcpy(tempStr, set.LandscapeFolder);
	pTerrain->SetHeightmapTerrain(strcat(tempStr, "Heightmap.bmp"), Vector(0,0,0), Vector(1000,200,1000), 0.4f);
	
	LoadTexture(&texDetail, "data/detail.bmp");
	pTerrain->SetTextures(&texTerrain, &texDetail, &texLightmap, texSkyBox);			
}


bool appPreInit()
{
	return true;
}


bool appInitGL()	
{
	// zbuffering
	glEnable(GL_DEPTH_TEST);
	glDepthFunc(GL_LEQUAL);

	// culling
	glEnable(GL_CULL_FACE);
	glCullFace(GL_FRONT);

	// We want to use textures, so enable them.
	glEnable( GL_TEXTURE_2D );
	glTexParameterf(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);
	glTexParameterf(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);

	glEnable(GL_FOG);                // активизируем туман
	glFogi(GL_FOG_MODE, GL_LINEAR);  // затенение линейно
	glFogfv(GL_FOG_COLOR, fogColor); // устанавливаем цвет тумана
	glFogf(GL_FOG_START,  0.0f);     // устанавливаем начальную плотность тумана
	glFogf(GL_FOG_END,    1.0f);     // устанавливаем конечную плотность тумана
	glHint(GL_FOG_HINT, GL_NICEST);  // вычисляем туман по пикселям
	glFogi(GL_FOG_COORDINATE_SOURCE_EXT, GL_FOG_COORDINATE_EXT);

	/*glEnable(GL_FOG);                       // Включает туман (GL_FOG)
	glFogi(GL_FOG_MODE, fogMode[fogfilter]);// Выбираем тип тумана
	glFogfv(GL_FOG_COLOR, fogColor);        // Устанавливаем цвет тумана
	glFogf(GL_FOG_DENSITY, 0.1f);          // Насколько густым будет туман
	glHint(GL_FOG_HINT, GL_DONT_CARE);      // Вспомогательная установка тумана
	glFogf(GL_FOG_START, 500.0f);             // Глубина, с которой начинается туман
	glFogf(GL_FOG_END, 700.0f);               // Глубина, где туман заканчивается.*/

	
	LoadSettings("ViewerSettings.ini");

	pTerrain->SetWaterHeight(waterHeight);				

	DynCam = new DynamicCamera( Vector(500,160,500), pTerrain);

	BuildFont();

	return true;
}


// Draw all the scene related stuff.
bool appRender()
{
	// We like to use floating point seconds, just for kicks.
	static float	lastTime = 0.0f;
	float			fTime = sys_Time/1000.f;
	static float	lastSecTime = fTime;
	static dword	frameCounter = 0;

	frameCounter++;
	g_frameTime = fTime-lastTime;
	lastTime = fTime;

	if (fTime-lastSecTime > 1.f)
	{
		sprintf_s(titleBar, "LandViewer..., at %.1ffps", (float)frameCounter/(fTime-lastSecTime));
		frameCounter = 0;
		lastSecTime = fTime;
	}
	
	DynCam->Update();

	glClear( GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT );
	glClear( GL_DEPTH_BUFFER_BIT );

	SwitchRenderMode(RENDERMODE_3D);
	pTerrain->Render();


	SwitchRenderMode(RENDERMODE_2D);
	if (DynCam->camSrc.y < pTerrain->GetWaterHeight())
	{
		glEnable(GL_BLEND);
		glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);
		glColor4f(0.734375, 0.8515625, 0.8359375, 0.6f);
		glRectf(0, 0, 1, 1);
		glDisable(GL_BLEND);
	}
	unsigned FontColor = 0xFFFFFFFF;
	int Height = 800;
	Print2D(0.02f, 0.025f, FontColor, "Press W, A, S, D to move camera; Q, E to decrease/increase camera height");
	Print2D(0.02f, 0.050f, FontColor, "     '[' ']' to decrease/increase water height");
	Print2D(0.02f, 0.075f, FontColor, "     'z' - texture, 'x' - lightmap, 'c' - detail, 'v' - snap");
	//Print2D(0.02, 0.075, FontColor, "     'z' - wireframe, 'x' - details");

	SwitchRenderMode(RENDERMODE_3D);
	
	return true;
}



void appShutdown() 
{
	delete DynCam;

	delete pTerrain;

	KillFont();

	texTerrain.Free();
	texDetail.Free();

	dword i;

	for (i=0; i<6; i++)
	{
		texSkyBox[i].Free();
	}

	for (i=0; i<N_CAUSTICS_TEX; i++)
	{
//		texCaustics[i].Free();
	}

}

bool ProcessKey(HWND hWnd,WPARAM wParam)
{
	if (wParam == VK_ESCAPE) 
		DestroyWindow(hWnd);

	if (wParam == 90)          // 'z'
		pTerrain->drawTexture = !pTerrain->drawTexture;
	if (wParam == 88)          // 'x'
		pTerrain->drawLightmap = !pTerrain->drawLightmap;
	if (wParam == 67)          // 'c'
		pTerrain->drawDetail = !pTerrain->drawDetail;
	if (wParam == 86)          // 'v'
	{ 
		Snap = !Snap;
		DynCam->SetSnap(Snap);
	}

	if (wParam == 49)
		pTerrain->wireFrame = !pTerrain->wireFrame;
	/*if (wParam == 49)
	{
		RandGen = !RandGen;
		if (!RandGen)
		{
			pTerrain->SetHeightmapTerrain("../data/heightmap.bmp", Vector(0,0,0), Vector(1000,160,1000), 0.4f);
		}
		else
		{
			pTerrain->SetRandomTerrain(Vector(0,0,0), Vector(1000,160,1000), Vector(2048, 2048, 256), 0.4f, rand_seed);
		}
	}*/
	if (kbrd.KeyDown[221] == true)
	{
		waterHeight += 0.025f;
		if (waterHeight > 2.0f)
			waterHeight = 2.0f;
		pTerrain->SetWaterHeight(waterHeight);
	}
	if (kbrd.KeyDown[219] == true)
	{
		waterHeight -= 0.025f;
		if (waterHeight < -1.0f)
			waterHeight = -1.0f;
		pTerrain->SetWaterHeight(waterHeight);
	}
	if (kbrd.KeyDown[188] == true)
	{
		fLightPos[1] -= 10;
	}
	if (kbrd.KeyDown[190] == true)
	{
		fLightPos[1] += 10;
	}

	return true;
}
