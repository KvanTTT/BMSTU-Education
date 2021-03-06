#include "PerlinNoise.h"


void PerlinNoise::setup()
{
	float step = 6.283185f / k_tableSize;
	float val=0.0f;

	srand((unsigned)time(NULL)); 
	for (int i=0; i<k_tableSize; ++i)
	{
		m_vecTable[i].x = (float)sin(val);
		m_vecTable[i].y = (float)cos(val);
		val += step;

		m_lut[i] = rand() & k_tableMask;
	}
}

const PerlinNoise::vec2& PerlinNoise::getVec(
	int x, 
	int y)const
{
	unsigned char a = m_lut[x&k_tableMask]; 
	unsigned char b = m_lut[y&k_tableMask]; 
	unsigned char val = m_lut[(a+b)&k_tableMask];
	return m_vecTable[val];
}


float PerlinNoise::noise(
	float x, 
	float y,
	float scale)
{
	vec2 pos(x*scale,y*scale);

	float X0 = (float)floor(pos.x);
	float X1 = X0 + 1.0f;
	float Y0 = (float)floor(pos.y);
	float Y1 = Y0 + 1.0f;

	const vec2& v0 = 
		getVec((int)X0, (int)Y0);
	const vec2& v1 = 
		getVec((int)X0, (int)Y1);
	const vec2& v2 = 
		getVec((int)X1, (int)Y0);
	const vec2& v3 = 
		getVec((int)X1, (int)Y1);

	vec2 d0(pos.x-X0, pos.y-Y0);
	vec2 d1(pos.x-X0, pos.y-Y1);
	vec2 d2(pos.x-X1, pos.y-Y0);
	vec2 d3(pos.x-X1, pos.y-Y1);

	float h0 = (d0.x * v0.x)+(d0.y * v0.y);
	float h1 = (d1.x * v1.x)+(d1.y * v1.y);
	float h2 = (d2.x * v2.x)+(d2.y * v2.y);
	float h3 = (d3.x * v3.x)+(d3.y * v3.y);

	float Sx,Sy;



	/*Sx = (3*powf(d0.x,2.0f))
		-(2*powf(d0.x,3.0f));

	Sy = (3*powf(d0.y,2.0f))
		-(2*powf(d0.y,3.0f));*/


	// the revised blend equation is 
	// considered more ideal, but is
	// slower to compute
	Sx = (6*powf(d0.x,5.0f))
		-(15*powf(d0.x,4.0f))
		+(10*powf(d0.x,3.0f));

	Sy = (6*powf(d0.y,5.0f))
		-(15*powf(d0.y,4.0f))
		+(10*powf(d0.y,3.0f));


	float avgX0 = h0 + (Sx*(h2 - h0));
	float avgX1 = h1 + (Sx*(h3 - h1));
	float result = avgX0 + (Sy*(avgX1 - avgX0)); 
	//float result = 

	return result;
}

float PerlinNoise::noise(
	int x, 
	int y, 
	float scale)
{
	return noise((float)x, (float)y, scale);
}

PerlinNoise::PerlinNoise()
{
	setup();
}

PerlinNoise::~PerlinNoise()
{
}
