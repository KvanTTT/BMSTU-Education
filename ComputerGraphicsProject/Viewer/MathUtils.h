#pragma once

#include "matrix.h"

bool RayTriangleIntersect(Vector &RayPos, Vector &RayDir, Vector &P1, Vector &P2, Vector &P3, 
						  Vector &ResultPoint, Vector &ResultNormal);