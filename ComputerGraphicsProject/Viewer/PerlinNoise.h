#pragma once

#include <cstdlib>
#include <ctime>
#include <math.h>

class PerlinNoise
{
public:

	struct vec2
	{
		float x,y;

		vec2(float _x, float _y):x(_x),y(_y){}; 
		vec2(){};
	};

	enum
	{
		k_tableSize = 256,
		k_tableMask = k_tableSize-1,
	};

    PerlinNoise();
    ~PerlinNoise();

	float noise(int x, int y, float scale);
	inline float noise(float x, float y, float scale);

private:
	vec2 m_vecTable[k_tableSize];
	unsigned char m_lut[k_tableSize];

	void setup();
	inline const vec2& getVec(int x, int y)const;
};


