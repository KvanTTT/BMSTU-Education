#include "matrix.h"

void Matrix::MakeIdentity()
{
	p[0] = 1;
	p[1] = 0;
	p[2] = 0;
	p[3] = 0;
	p[4] = 1;
	p[5] = 0;
	p[6] = 0;
	p[7] = 0;
	p[8] = 1;
}

Matrix::Matrix(float a)
{
	p[0] = a;
	p[1] = 0;
	p[2] = 0;
	p[3] = 0;
	p[4] = a;
	p[5] = 0;
	p[6] = 0;
	p[7] = 0;
	p[8] = a;
}

Matrix Matrix::operator *(Matrix &m)	
{
	Matrix temp;

	temp.p[_XX] = p[_XX]*m.p[_XX]+p[_XY]*m.p[_YX]+p[_XZ]*m.p[_ZX];
	temp.p[_YX] = p[_YX]*m.p[_XX]+p[_YY]*m.p[_YX]+p[_YZ]*m.p[_ZX];
	temp.p[_ZX] = p[_ZX]*m.p[_XX]+p[_ZY]*m.p[_YX]+p[_ZZ]*m.p[_ZX];

	temp.p[_XY] = p[_XX]*m.p[_XY]+p[_XY]*m.p[_YY]+p[_XZ]*m.p[_ZY];
	temp.p[_YY] = p[_YX]*m.p[_XY]+p[_YY]*m.p[_YY]+p[_YZ]*m.p[_ZY];
	temp.p[_ZY] = p[_ZX]*m.p[_XY]+p[_ZY]*m.p[_YY]+p[_ZZ]*m.p[_ZY];

	temp.p[_XZ] = p[_XX]*m.p[_XZ]+p[_XY]*m.p[_YZ]+p[_XZ]*m.p[_ZZ];
	temp.p[_YZ] = p[_YX]*m.p[_XZ]+p[_YY]*m.p[_YZ]+p[_YZ]*m.p[_ZZ];
	temp.p[_ZZ] = p[_ZX]*m.p[_XZ]+p[_ZY]*m.p[_YZ]+p[_ZZ]*m.p[_ZZ];

	return temp;
}

Vector Matrix::operator *(Vector &v)	
{
	Vector	temp;

	temp.x = p[_XX] * v.x + p[_XY] * v.y + p[_XZ] * v.z;
	temp.y = p[_YX] * v.x + p[_YY] * v.y + p[_YZ] * v.z;
	temp.z = p[_ZX] * v.x + p[_ZY] * v.y + p[_ZZ] * v.z;

	return temp;
}

Matrix Matrix::operator *(float f)		
{
	Matrix temp;

	for (int i=0; i<9; i++)
		temp.p[i] = p[i] * f;

	return temp;
}


void Matrix::operator *=(Matrix &m)		
{
	*this = *this * m;
}


void Matrix::operator *=(float f)	
{
	*this = *this * f;
}

float Matrix::Det()
{
  return    p[_XX] * (p[_YY] * p[_ZZ] - p[_YZ] * p[_ZY])
          - p[_YX] * (p[_XY] * p[_ZZ] - p[_XZ] * p[_ZY])
          + p[_ZX] * (p[_XY] * p[_YZ] - p[_XZ] * p[_YY]);
}

void Matrix::AddRotationX(float angle)
{
	float sina = sin(angle);
	float cosa = cos(angle);
	float t;

	t = p[3];
	p[3] = t*cosa - p[6]*sina;
	p[6] = p[6]*cosa + t*sina;
	t = p[4];
	p[4] = t*cosa - p[7]*sina;
	p[7] = p[7]*cosa + t*sina;
	t = p[5];
	p[5] = t*cosa - p[8]*sina;
	p[8] = p[8]*cosa + t*sina;
}

void Matrix::AddRotationY(float angle)
{	
	float sina = sin(angle);
	float cosa = cos(angle);
	float t;

	t = p[0];
	p[0] = t*cosa - p[6]*sina;
	p[6] = p[6]*cosa + t*sina;
	t = p[1];
	p[1] = t*cosa - p[7]*sina;
	p[7] = p[7]*cosa + t*sina;
	t = p[2];
	p[2] = t*cosa - p[8]*sina;
	p[8] = p[8]*cosa + t*sina;

	/*float sina = sin(angle);
	float cosa = cos(angle);
	float t[3];
	t[0] = p[0]; t[1] = p[1]; t[2] = p[2];

	p[0] =  cosa*t[0] + sina*p[6];
	p[1] =  cosa*t[1] + sina*p[7];	
	p[2] =  cosa*t[2] + sina*p[8];	

	p[6] = -sina*t[0] + cosa*p[6];
	p[7] = -sina*t[1] + cosa*p[7];	
	p[8] = -sina*t[2] + cosa*p[8];	*/
}

void Matrix::AddRotationZ(float angle)
{
	float sina = sin(angle);
	float cosa = cos(angle);
	float t;

	t = p[0];
	p[0] = t*cosa - p[3]*sina;
	p[3] = p[3]*cosa + t*sina;
	t = p[1];
	p[1] = t*cosa - p[4]*sina;
	p[4] = p[4]*cosa + t*sina;
	t = p[2];
	p[2] = t*cosa - p[5]*sina;
	p[5] = p[5]*cosa + t*sina;
}

Matrix::Matrix(float angle, Vector &v)
{
	float sina = sin(angle);
	float cosa = cos(angle);
	float one_cosa = 1 - cosa;

	p[0] = cosa + one_cosa*v.x*v.x;
	p[1] = one_cosa*v.x*v.y - sina*v.z;
	p[2] = one_cosa*v.x*v.z + sina*v.y;
	p[3] = one_cosa*v.y*v.x + sina*v.z;
	p[4] = cosa + one_cosa*v.y*v.y;
	p[5] = one_cosa*v.y*v.z - sina*v.x;
	p[6] = one_cosa*v.z*v.x - sina*v.y;
	p[7] = one_cosa*v.z*v.y + sina*v.x;
	p[8] = cosa + one_cosa*v.z*v.z;
}

Matrix Matrix::operator !()				
{
	Matrix temp;

	for (int i=0; i<3; i++)
		for (int j=0; j<3; j++)
			temp.p[i+j*3] = p[j+i*3];
		
	return temp;
}




void Matrix::Set(	float xx, float xy, float xz,
					float yx, float yy, float yz,
					float zx, float zy, float zz)
{
	p[_XX] = xx; p[_XY] = xy; p[_XZ] = xz;
	p[_YX] = yx; p[_YY] = yy; p[_YZ] = yz;
	p[_ZX] = zx; p[_ZY] = zy; p[_ZZ] = zz;
}

void Matrix::SetAng(float ax, float ay, float az)
{
	float sinx, siny, sinz, cosx, cosy, cosz, syz, cxz, sxcz;

	sinx = sinf(ax); cosx = cosf(ax);
	siny = sinf(ay); cosy = cosf(ay);
	sinz = sinf(az); cosz = cosf(az);
	
	syz = siny * sinz;
	cxz = cosx * cosz;
	sxcz = sinx * cosz;

	p[_XX] = sinx * syz + cxz;
	p[_XY] = cosy * sinz;
	p[_XZ] = sxcz - cosx * syz;
	p[_YX] = sxcz * siny - cosx * sinz;
	p[_YY] = cosy * cosz;
	p[_YZ] = -cxz * siny - sinx * sinz;
	p[_ZX] = -sinx * cosy;
	p[_ZY] = siny;
	p[_ZZ] = cosx * cosy;
}

void Matrix::Identity()
{
	p[_XX] = 1.f; p[_XY] = 0;   p[_XZ] = 0;
	p[_YX] = 0;   p[_YY] = 1.f; p[_YZ] = 0;
	p[_ZX] = 0;   p[_ZY] = 0;   p[_ZZ] = 1.f;
}

void Matrix::Invert()
{
	float a1, a2, a3, b1, b2, b3, c1, c2, c3, one_div_det;
	one_div_det = 1/Det();

	a1 = p[_XX]; a2 = p[_YX]; a3 = p[_ZX];
	b1 = p[_XY]; b2 = p[_YY]; b3 = p[_ZY];
	c1 = p[_XZ]; c2 = p[_YZ]; c3 = p[_ZZ];

	p[_XX] = (b2*c3-c2*b3);
	p[_XY] = -(b1*c3-c1*b3);
	p[_XZ] = (b1*c2-c1*b2);

	p[_YX] = -(a2*c3-c2*a3);
	p[_YY] = (a1*c3-c1*a3);
	p[_YZ] = -(a1*c2-c1*a2);

	p[_ZX] = (a2*b3-b2*a3);
	p[_ZY] = -(a1*b3-b1*a3);
	p[_ZZ] = (a1*b2-b1*a2);

	*this = *this * one_div_det;
}
