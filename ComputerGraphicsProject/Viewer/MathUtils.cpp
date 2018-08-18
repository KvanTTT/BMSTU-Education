#include "MathUtils.h"

const float EPSILON2 = 0.00001;

bool RayTriangleIntersect(Vector &RayPos, Vector &RayDir, Vector &P1, Vector &P2, Vector &P3, 
						  Vector &ResultPoint, Vector &ResultNormal)
{
	Vector v1 = P2 - P1;
	Vector v2 = P3 - P1;
	Vector pvec = RayDir ^ v2;
	float det = v1 * pvec;
	if ((det < EPSILON2) && (det > -EPSILON2))
	{
		return false;
	}
	float invDet = 1/det;
	Vector tvec = RayPos - P1;
	float u = (tvec * pvec) * invDet;
	if ((u < 0) || (u > 1))
		return false;
	else 
	{
		Vector qvec = tvec ^ v1;
		float v  = (RayDir * qvec) * invDet;
		if ((v < 0) || (u+v > 1))
			return false;
		float t = (v2 * qvec) * invDet;
		if (t > 0)
		{
			ResultPoint =  RayPos + RayDir * t;
			ResultNormal = v1 ^ v2;
			return true;
		} 
		else 
			return false;
	}
}