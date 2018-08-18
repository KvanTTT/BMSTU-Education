
#ifndef __VECTOR_H__
#define __VECTOR_H__


#include "base.h"

class Vector;

#include "matrix.h"



class Vector
{
public:
	float	x, y, z;

	// constructors
	Vector()								{ Set(0, 0, 0); }
	Vector(float _x, float _y, float _z)	{ Set(_x, _y, _z); }
	Vector(Vector &v)						{ Set(v.x, v.y, v.z); }

	// operators
	Vector operator +(Vector &v);	
	void operator +=(Vector &v);	

	Vector operator -(Vector &v);	
	void operator -=(Vector &v);	

	Vector operator *(Matrix &m);	
	float operator *(Vector &v);	
	Vector operator *(float f);		
	void operator *=(Matrix &m);	
	void operator *=(Vector &v);	
	void operator *=(float f);		

	Vector operator ^(Vector &v);	
	void operator ^=(Vector &v);	
	float Angle(Vector &v);

	Vector operator !();			

	// functions
	void	Set(float _x, float _y, float _z);
	float	Dot(Vector &v);
	float	SelfDot();
	Vector	Normalize();
	void	SelfNormalize();
	float	Length();
	float	Distance(Vector &v);
	Vector	Lerp(Vector &v, float f);
	void	SelfLerp(Vector &v, float f);

	float	*GetPtr() { return (float*)this; };
};

struct VertexData	// (3+2+2+4)*4 = 44 bytes
{
	Vector	pos;	// position
	byte	col[4];	// color
	float	s, t;	// texture coordinates
	float	u, v;	// detail-texture coordinates
};

struct Vector2D
{
	dword	x, y;
};


#endif //__VECTOR_H__