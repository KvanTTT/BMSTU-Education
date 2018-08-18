

#ifndef __MATRIX_H__
#define __MATRIX_H__

// includes
#include "base.h"

class Matrix;

#include "vector.h"


enum {_XX = 0, _XY, _XZ, _YX, _YY, _YZ, _ZX, _ZY, _ZZ};



class Matrix
{
public:
	float	p[9];

	Matrix()								{ memset(p,0,sizeof(float)*9); }					
	Matrix(	float xx, float xy, float xz,
			float yx, float yy, float yz,
			float zx, float zy, float zz)	{ Set(xx, xy, xz,  yx, yy, yz,  zx, zy, zz); }		
	Matrix(float ax, float ay, float az)	{ SetAng(ax, ay, az); };							
	Matrix(Matrix &m)						{ memcpy(p, m.p, sizeof(float)*9); }	
	Matrix(float a);
	Matrix(float angle, Vector &v);

	void MakeIdentity();
	Matrix operator *(Matrix &m);	
	Vector operator *(Vector &v);	
	Matrix operator *(float f);		
	void operator *=(Matrix &m);	
	void operator *=(float f);		
	float Det();
	void AddRotationX(float angle);
	void AddRotationY(float angle);
	void AddRotationZ(float angle);

	Matrix operator !();			


	void Set(	float xx, float xy, float xz,
				float yx, float yy, float yz,
				float zx, float zy, float zz);	
	void SetAng(float ax, float ay, float az);	
	void Identity();							
	void Invert();								
};



#endif 
