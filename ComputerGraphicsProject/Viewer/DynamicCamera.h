#ifndef __DYNAMICCAMERA_H__
#define __DYNAMICCAMERA_H__

// includes
#include "base.h"
#include "Terrain.h"

#include "vector.h"
#include "matrix.h"


class DynamicCamera
{
public:
	DynamicCamera(Vector &vecSrc, Terrain *land);
	DynamicCamera();
	~DynamicCamera();

	void SetSnap(bool snap);
	void Update();
	Vector	camSrc, camVel, camAng;

private:
	bool snap;
	float	angSpeed, moveSpeed;
	float	camFov, camRoll;
	POINT  CursorPos;
	Matrix	viewMat;
	Terrain *landscape;
	//Vector dirVec, upVec;
};


#endif 