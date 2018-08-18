#ifndef __TERRAIN_H__
#define __TERRAIN_H__

#include "base.h"
#include "vector.h"

#define N_CAUSTICS_TEX	32


class Terrain
{
public:
	Terrain();
	~Terrain();

	void SetHeightmapTerrain(char *pHeightMapName, Vector &origin, Vector &dimen, float density);
	void SetRandomTerrain(Vector &origin, Vector &dimen, Vector &Size, float density, float rand_seed);
	
	void SetParams(Vector &origin, Vector &dimen, float density);
	void SetTextures(Texture *pTexTerrain, Texture *pTexDetail, Texture *pTexLightmap, Texture *pTexSkyBox);
	void SetHeightmap(Texture *pHeightmap);

	void BuildVegetation(unsigned TreeCount);

	void Render();

	float GetHeight(float x, float z);
	float GetHeightAndDir(float x, float z, Vector &Dir, Vector &Side, Vector &Up);

	void SetWaterHeight(float NewHeight);
	float GetWaterHeight();
	void SetRandGenerate(bool RandGen); 

	dword	drawDetail, drawLightmap, drawTexture;
	dword	wireFrame;

	float		 waterHeight;	

private:
	void BuildTerrain();
	void PutTerrainVertex(dword i, dword j);
	void RenderTerrain();
	void RenderSkyBox();
	void RenderWater();


	Image		 imgHeightMap;			
	dword		 mapXRes, mapYRes;		

	Vector		 origin, dimen;			
	dword		 vertXRes, vertYRes;	

	float HeightDim;
	
	float TileX, TileY;

	float rand_seed;
	bool         RandGen;

	VertexData	*pVertexData;			
	unsigned TreeCount;

	word		*pIndexArray;
	Vector      *pNormals;

	Vector		 skyBoxVerts[12];		

	float     *LightMap;
	Vector *VertexNormal;

	float  RefractCoef;

	Texture	*pTexTerrain, *pTexDetail;	
	Texture	*pTexSkyBox;				
	Texture	*pTexLightmap;				
};

extern GLfloat fLightPos[4];

#endif 