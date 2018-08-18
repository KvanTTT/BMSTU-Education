//
// DynamicCamera.cpp
//
//
//

#include "DynamicCamera.h"

#include "Engine.h"


DynamicCamera::DynamicCamera()
{
	DynamicCamera(Vector(0,0,0), NULL);
}

DynamicCamera::DynamicCamera(Vector &vecSrc, Terrain *land)
{
	landscape = land;

	snap = false;

	GetCursorPos(&CursorPos);
	ShowCursor(false);

	camSrc = vecSrc;
	camAng.Set(0, 0, 0);
	viewMat.Identity();

	camFov = 60.f;
	camRoll = 0;

	angSpeed = 0.1f;
	moveSpeed = 9.0f;

	glMatrixMode (GL_PROJECTION);
	glLoadIdentity ();
	gluPerspective(camFov, (GLdouble)sys_glWidth/(GLdouble)sys_glHeight, 1.0, 1500000.0);
	
	glMatrixMode (GL_MODELVIEW);
}

DynamicCamera::~DynamicCamera()
{
}

void DynamicCamera::SetSnap(bool snap)
{
	this->snap = snap;
	this->Update();
}

void DynamicCamera::Update()
{
	float newAngSpeed = angSpeed * g_frameTime;

	camAng *= expf(-2.0f*g_frameTime);
	camRoll += camAng.z*70;
	

	// вектор бинормали
	Vector	sideVec(viewMat.p[0], viewMat.p[1], viewMat.p[2]);
	//вектор нормали (указывает "верх" камеры)
	Vector	upVec(viewMat.p[3], viewMat.p[4], viewMat.p[5]);
	// вектор направления
	Vector	dirVec(viewMat.p[6], viewMat.p[7], viewMat.p[8]);
	
	// построение матрицы поворота
	Matrix Rot(camAng.y, (dirVec ^ upVec));
	// добавление вращения по оси Y (он указывает верх камеры), которое зависит от смещение мыши по х
	Rot.AddRotationY(camAng.x);
	// обновление векторов камеры (домножение на матрицу поворота)
	upVec = Rot * upVec;
	dirVec = Rot * dirVec;
	sideVec = Rot * sideVec;
	// обновление матрицы камеры
	viewMat.p[0] = sideVec.x; viewMat.p[1] = sideVec.y; viewMat.p[2] = sideVec.z;
	viewMat.p[3] = upVec.x; viewMat.p[4] = upVec.y; viewMat.p[5] = upVec.z;
	viewMat.p[6] = dirVec.x; viewMat.p[7] = dirVec.y; viewMat.p[8] = dirVec.z;



	LONG dx, dy;

	POINT Pos;
	GetCursorPos(&Pos);
	dx = Pos.x - CursorPos.x;
	dy = Pos.y - CursorPos.y;
	CursorPos = Pos;
	
	if ((dx != 0) || (dy != 0))
	{
		/*RECT Screen;
		GetClientRect(GetForegroundWindow(), &Screen);
		POINT center;
		CursorPos.x = Screen.right / 2;
		CursorPos.y = Screen.bottom / 2;
		ClientToScreen(GetForegroundWindow(), &CursorPos);
		SetCursorPos(CursorPos.x, CursorPos.y); */

		camAng.x -= newAngSpeed * dx / 4;
		camAng.y -= newAngSpeed * dy / 4;
	}
	

	if (kbrd.KeyDown[VK_RIGHT] == true)
		camAng.x -= newAngSpeed;
	if (kbrd.KeyDown[VK_LEFT] == true)
		camAng.x += newAngSpeed;

	if (kbrd.KeyDown[VK_UP] == true)
		camAng.y += newAngSpeed;
	if (kbrd.KeyDown[VK_DOWN] == true)
		camAng.y -= newAngSpeed;

	// forward-backward
	if (kbrd.KeyDown[87] == true)
		camVel += dirVec*moveSpeed;
	if (kbrd.KeyDown[83] == true)
		camVel -= dirVec*moveSpeed;

	// left-right
	if (kbrd.KeyDown[68] == true)
		camVel += sideVec*moveSpeed;
	if (kbrd.KeyDown[65] == true)
		camVel -= sideVec*moveSpeed;

	// up-down
	if (kbrd.KeyDown[81] == true)
		camVel += upVec*moveSpeed;
	if (kbrd.KeyDown[69] == true)
		camVel -= upVec*moveSpeed;

	//roll
	if (kbrd.KeyDown[45] == true)
		camAng.z -= newAngSpeed;
	if (kbrd.KeyDown[46] == true)
		camAng.z += newAngSpeed;

	camSrc += camVel * g_frameTime;
	if (snap && (camSrc.y < landscape->GetHeight(camSrc.x, camSrc.z) + 5))
	{
		camSrc.y = landscape->GetHeight(camSrc.x, camSrc.z) + 5;
		/*viewMat.p[0] = sideVec.x;
		viewMat.p[1] = sideVec.y;
		viewMat.p[2] = sideVec.z;

		viewMat.p[3] = upVec.x;
		viewMat.p[4] = upVec.y;
		viewMat.p[5] = upVec.z;

		viewMat.p[6] = dirVec.x;
		viewMat.p[7] = dirVec.y;
		viewMat.p[8] = dirVec.z;*/
	}
	camVel *= expf(-2.0f*g_frameTime);


	glLoadIdentity ();

	// обновление матрицы камеры с учетом ее позиции
	float m[16] = {
		viewMat.p[0], viewMat.p[3], -viewMat.p[6], 0,
		viewMat.p[1], viewMat.p[4], -viewMat.p[7], 0,
		viewMat.p[2], viewMat.p[5], -viewMat.p[8], 0,
		-(viewMat.p[0]*camSrc.x + viewMat.p[1]*camSrc.y + viewMat.p[2]*camSrc.z),
		-(viewMat.p[3]*camSrc.x + viewMat.p[4]*camSrc.y + viewMat.p[5]*camSrc.z),
		 (viewMat.p[6]*camSrc.x + viewMat.p[7]*camSrc.y + viewMat.p[8]*camSrc.z), 
		 2
	};

	glLoadMatrixf(m);
}
