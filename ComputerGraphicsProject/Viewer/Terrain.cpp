#include "Terrain.h"

#include "Engine.h"

#include "math.h"

#include "time.h"

#include "PerlinNoise.h"

#include "Render.h"

GLfloat fLightPos[4]   = { -100.0f, 100.0f, 50.0f, 1.0f };  // Point source
GLfloat fNoLight[] = { 0.0f, 0.0f, 0.0f, 0.0f };
GLfloat fLowLight[] = { 0.5f, 0.5f, 0.5f, 1.0f };
GLfloat fBrightLight[] = { 1.0f, 1.0f, 1.0f, 1.0f };


Terrain::Terrain()
{
	this->wireFrame = false;
	this->drawDetail = true;
	this->drawLightmap = true;
	this->drawTexture = true;

	glLightModelfv(GL_LIGHT_MODEL_AMBIENT, fNoLight);
    glLightfv(GL_LIGHT0, GL_AMBIENT, fLowLight);
    glLightfv(GL_LIGHT0, GL_DIFFUSE, fBrightLight);
    glLightfv(GL_LIGHT0, GL_SPECULAR, fBrightLight);    
}

Terrain::~Terrain()
{
	delete pVertexData;
}

void Terrain::SetHeightmapTerrain(char *pHeightMapName, Vector &origin, Vector &dimen, float density)
{
	imgHeightMap.Create(pHeightMapName, imgLoadBMP);

	mapXRes = imgHeightMap.GetWidth();
	mapYRes = imgHeightMap.GetHeight();

	HeightDim = (1 << imgHeightMap.GetBPP());
	
	this->RandGen = false;
	SetParams(origin, dimen, density);
}

void Terrain::SetRandomTerrain(Vector &origin, Vector &dimen, Vector &Size, float density, float rand_seed)
{
	mapXRes = (dword)Size.x;
	mapYRes = (dword)Size.y;
	
	this->rand_seed = rand_seed;
	this->RandGen = true;
	SetParams(origin, dimen, density);
}

void Terrain::SetParams(Vector &origin, Vector &dimen, float density)
{
	this->origin = origin;
	this->dimen = dimen;
	//this->waterHeight = waterHeight;

	// calculate the resolution of our vertex-map
	vertXRes = (dword)(mapXRes * density);
	vertYRes = (dword)(mapYRes * density);

	if (vertXRes < 3)
		vertXRes = 3;

	if (vertYRes < 3)
		vertYRes = 3;

	vertXRes = 1 + ((vertXRes-1)>>1)*2;
	vertYRes = 1 + ((vertYRes-1)>>1)*2;

	TileX = dimen.x / vertXRes;
	TileY = dimen.z / vertYRes;

	pVertexData = new VertexData[vertXRes*vertYRes];

	BuildTerrain();

	// vertices
	Vector	center, radius;
	const float rad = dimen.x*12;

	center = origin + (dimen * 0.5f);
	center.y = origin.y+dimen.y*this->waterHeight;
	radius.Set(rad, rad, rad);

	// top
	skyBoxVerts[0].Set(center.x-radius.x, center.y+radius.y, center.z+radius.z);
	skyBoxVerts[1].Set(center.x+radius.x, center.y+radius.y, center.z+radius.z);
	skyBoxVerts[2].Set(center.x+radius.x, center.y+radius.y, center.z-radius.z);
	skyBoxVerts[3].Set(center.x-radius.x, center.y+radius.y, center.z-radius.z);

	// bottom
	skyBoxVerts[4].Set(center.x-radius.x, center.y, center.z+radius.z);
	skyBoxVerts[5].Set(center.x+radius.x, center.y, center.z+radius.z);
	skyBoxVerts[6].Set(center.x+radius.x, center.y, center.z-radius.z);
	skyBoxVerts[7].Set(center.x-radius.x, center.y, center.z-radius.z);
}

float GetPixel(byte *pData, dword xRes, dword yRes, float s, float t)
{
	dword	uInt, vInt;
	float	uFrac, vFrac;

	byte	colors[4];
	float	retCol;

	// find integer coordinates
	uInt = (dword)s;
	vInt = (dword)t;

	uFrac = s - uInt;
	vFrac = t - vInt;

	// bilinear filtering
	colors[0] = pData[vInt*xRes + uInt];
	colors[1] = pData[vInt*xRes + ((uInt+1)%xRes)];
	colors[2] = pData[((vInt+1)%yRes)*xRes + ((uInt+1)%xRes)];
	colors[3] = pData[((vInt+1)%yRes)*xRes + uInt];

	retCol =	colors[0] * ((1-uFrac)*(1-vFrac)) +
				colors[1] * (uFrac*(1-vFrac)) +
				colors[2] * (uFrac*vFrac) +
				colors[3] * ((1-uFrac)*vFrac);

	return retCol;
}


void Terrain::BuildTerrain()
{
	float		 minY, deltaY, yScale;
	dword		 x, y;
	float		 stepX, stepY;
	float		 sCoord, tCoord;
	float		 rDensX, rDensY;
	byte *pHeightMap;
	//if ((imgHeightMap.GetBPP() == 8) || (imgHeightMap.GetBPP() == 24))
		pHeightMap = (byte*)imgHeightMap.GetData();

	minY = origin.y;
	deltaY = dimen.y - minY;
	yScale = deltaY / 256;

	rDensX = 1.f / vertXRes;
	rDensY = 1.f / vertYRes;

	float t;

	unsigned k = 0;
	PerlinNoise *pNoise = new PerlinNoise();

	for (y=0; y<vertYRes; y++)
	{
		stepY = (float)y*rDensX;

		tCoord = stepY*mapYRes;

		for (x=0; x<vertXRes; x++)
		{
			stepX = (float)x*rDensY;

			sCoord = stepX*mapXRes;

			float	pixelHeight;

			if (RandGen)
			{
				yScale = 100.0f;
				t = pNoise->noise(stepX*dimen.x, stepY*dimen.z, rand_seed);
				pVertexData[k].pos.Set(origin.x+stepX*dimen.x, 
									   minY + t*yScale, 
									   origin.z+stepY*dimen.z);		
			}
			else
			{
				pixelHeight = GetPixel(pHeightMap, mapXRes, mapYRes, sCoord, tCoord);
	//			if (pixelHeight/HeightDim < waterHeight) {
	//				pixelHeight = waterHeight*HeightDim - 2;
	//			}
				pVertexData[k].pos.Set(origin.x+stepX*dimen.x, minY + pixelHeight*yScale, origin.z+stepY*dimen.z );
			}


			pVertexData[k].s = stepX;
			pVertexData[k].t = stepY;
			pVertexData[k].u = stepX*20.f;
			pVertexData[k].v = stepY*20.f;

			/*pVertexData[k].col[0] = 1.f;
			pVertexData[k].col[1] = 1.f;
			pVertexData[k].col[2] = 1.f;
			pVertexData[k].col[3] = 1.f;*/

			k++;
		}
	}

	pNoise->~PerlinNoise();
	imgHeightMap.Free();


	// vertices
	glVertexPointer(3, GL_FLOAT, sizeof(VertexData), pVertexData);
	glEnableClientState(GL_VERTEX_ARRAY);

	// color
	glColorPointer(4, GL_UNSIGNED_BYTE, sizeof(VertexData), (float*)pVertexData+3);
	glEnableClientState(GL_COLOR_ARRAY);

	// 1'st texture coordinates
	glClientActiveTextureARB(GL_TEXTURE0_ARB); 
	glTexCoordPointer(2, GL_FLOAT, sizeof(VertexData), (float*)pVertexData+4);
	glEnableClientState(GL_TEXTURE_COORD_ARRAY);
	
	// 2'nd texture coordinates
	glClientActiveTextureARB(GL_TEXTURE1_ARB); 
	glTexCoordPointer(2, GL_FLOAT, sizeof(VertexData), (float*)pVertexData+6);
	glEnableClientState(GL_TEXTURE_COORD_ARRAY);

	// 3'nd texture coordinates
	glClientActiveTextureARB(GL_TEXTURE2_ARB); 
	glTexCoordPointer(2, GL_FLOAT, sizeof(VertexData), (float*)pVertexData+4);
	glEnableClientState(GL_TEXTURE_COORD_ARRAY);	

	glClientActiveTextureARB(GL_TEXTURE0_ARB); 

	glLockArraysEXT(0, vertXRes*vertYRes);
	GLenum blet;
	blet = glGetError();

	// generate an index-array
	pIndexArray = new word[vertYRes*vertXRes*2];
	pNormals = new Vector[(vertYRes-1)*(vertXRes-1)*2];
	k = 0;
	int i = 0;
	for (y=0; y < vertYRes-1; y++)
	{
		pIndexArray[k] = y * vertXRes;
		k++;
		pIndexArray[k] = (y+1) * vertXRes;
		k++;
		for (dword x = 1; x < vertXRes; x++)
		{
			pIndexArray[k] = y * vertXRes + x;
			k++;
			pIndexArray[k] = (y+1) * vertXRes + x;
			k++;

			pNormals[i] =   ((pVertexData[pIndexArray[k-3]].pos - pVertexData[pIndexArray[k-4]].pos) ^ 
						     (pVertexData[pIndexArray[k-2]].pos - pVertexData[pIndexArray[k-4]].pos)).Normalize();
			//i++;
			
			pNormals[i+1] =  ((pVertexData[pIndexArray[k-2]].pos - pVertexData[pIndexArray[k-1]].pos) ^ 
						     (pVertexData[pIndexArray[k-3]].pos - pVertexData[pIndexArray[k-1]].pos)).Normalize();
			//i++;
			i += 2;
		}
	}

}

void Terrain::SetWaterHeight(float NewHeight)
{
	this->waterHeight = NewHeight;

	Vector	center, radius;
	const float rad = dimen.x*12;

	center = origin + (dimen * 0.5f);
	center.y = origin.y+dimen.y*this->waterHeight;
	radius.Set(rad, rad, rad);

	skyBoxVerts[0].Set(center.x-radius.x, center.y+radius.y, center.z+radius.z);
	skyBoxVerts[1].Set(center.x+radius.x, center.y+radius.y, center.z+radius.z);
	skyBoxVerts[2].Set(center.x+radius.x, center.y+radius.y, center.z-radius.z);
	skyBoxVerts[3].Set(center.x-radius.x, center.y+radius.y, center.z-radius.z);

	skyBoxVerts[4].Set(center.x-radius.x, center.y, center.z+radius.z);
	skyBoxVerts[5].Set(center.x+radius.x, center.y, center.z+radius.z);
	skyBoxVerts[6].Set(center.x+radius.x, center.y, center.z-radius.z);
	skyBoxVerts[7].Set(center.x-radius.x, center.y, center.z-radius.z);
}

float Terrain::GetWaterHeight()
{
	return origin.y + dimen.y*waterHeight;
}

void Terrain::SetRandGenerate(bool RandGen)
{
	this->RandGen = RandGen;
	BuildTerrain();
}

void Terrain::SetTextures(Texture *pTexTerrain, Texture *pTexDetail, Texture *pTexLightmap, Texture *pTexSkyBox)
{
	this->pTexTerrain = pTexTerrain;
	this->pTexDetail = pTexDetail;
	this->pTexSkyBox = pTexSkyBox;
	this->pTexLightmap = pTexLightmap;
}


float Terrain::GetHeight(float x, float z)
{
	int x1 = (int)floor((x - origin.x) / TileX);
	int z1 = floor((z - origin.z) / TileY);
	if ((x1 < 0) || (x1 >= vertXRes) || (z1 < 0) || (z1 >= vertYRes))
		return origin.y;
	else
	{
		float xt = x1*TileX + origin.x;
		float zt = z1*TileY + origin.z;
		if ((x - xt)*TileY + (z - zt)*TileX <= TileX*TileY)
		{
			Vector n = Vector(pVertexData[z1*vertXRes + x1 + 1].pos -  pVertexData[z1*vertXRes + x1].pos) ^ 
					   Vector(pVertexData[(z1+1)*vertXRes + x1].pos -  pVertexData[z1*vertXRes + x1].pos);
			float d = -n.Dot(pVertexData[z1*vertXRes + x1].pos);   
			return (-n.x * x - n.z * z - d) / n.y;
		}
		else
		{
			int x2 = x1 + 1;
			int z2 = z1 + 1;
			Vector n = Vector(pVertexData[z1*vertXRes + x1 + 1].pos -  pVertexData[z2*vertXRes + x2].pos) ^ 
					   Vector(pVertexData[(z1+1)*vertXRes + x1].pos -  pVertexData[z2*vertXRes + x2].pos);
			float d = -n.Dot(pVertexData[z2*vertXRes + x2].pos);   
			return (-n.x * x - n.z * z - d) / n.y;
		}
	}
}

float Terrain::GetHeightAndDir(float x, float z, Vector &Dir, Vector &Side, Vector &Up)
{
	int x1 = floor((x - origin.x) / TileX);
	int z1 = floor((z - origin.z) / TileY);
	if ((x1 < 0) || (x1 >= vertXRes) || (z1 < 0) || (z1 >= vertYRes))
		return origin.y;
	else
	{
		float xt = x1*TileX + origin.x;
		float zt = z1*TileY + origin.z;
		if ((x - xt)*TileY + (z - zt)*TileX <= TileX*TileY)
		{
			Vector n = (Vector(pVertexData[z1*vertXRes + x1 + 1].pos -  pVertexData[z1*vertXRes + x1].pos) ^ 
						Vector(pVertexData[(z1+1)*vertXRes + x1].pos -  pVertexData[z1*vertXRes + x1].pos)).Normalize();
			Vector n1 = (n ^ Up).Normalize();
			Matrix Rot(-n.Angle(Up), n1);
			Up = n;
			Dir = Rot * Dir;
			Side = Rot * Side;
			float d = -n.Dot(pVertexData[z1*vertXRes + x1].pos);
			return (-n.x * x - n.z * z - d) / n.y;
		}
		else
		{
			int x2 = x1 + 1;
			int z2 = z1 + 1;
			Vector n = Vector(pVertexData[z1*vertXRes + x1 + 1].pos -  pVertexData[z2*vertXRes + x2].pos) ^ 
					   Vector(pVertexData[(z1+1)*vertXRes + x1].pos -  pVertexData[z2*vertXRes + x2].pos);
			float d = -n.Dot(pVertexData[z2*vertXRes + x2].pos);   
			return (-n.x * x - n.z * z - d) / n.y;
		}
	}
}

void Terrain::Render()
{
	double plane_eqn[4] = { 0, -1, 0, origin.y + dimen.y*waterHeight}; 
	glClipPlane( GL_CLIP_PLANE0, plane_eqn ); 
	glEnable( GL_CLIP_PLANE0 ); 



	glTranslatef(0, 2*waterHeight*dimen.y, 0);
	glScalef(1, -1, 1);

		glCullFace(GL_BACK);
		
		RenderTerrain();
		RenderSkyBox();

		glCullFace(GL_FRONT);

	glScalef(1, -1, 1);
	glTranslatef(0, -2*waterHeight*dimen.y, 0);

	glEnable(GL_BLEND);
	glBlendFunc(GL_SRC_COLOR, GL_DST_COLOR);
	RenderTerrain();
	glDisable(GL_BLEND);


	glDisable( GL_CLIP_PLANE0 ); 

	RenderWater();
	RenderTerrain();

	
	/*glBegin(GL_LINES);
	glColor3f(1.0f, 1.0f, 1.0f);
	int i = 0, j = 0;
	float s = 10;
	for (dword y = 0; y < vertYRes-1; y++) 
	{
		for (dword x = 0; x < vertXRes*2 - 2; x++)
		{
			glVertex3f(pVertexData[pIndexArray[i]].pos.x, pVertexData[pIndexArray[i]].pos.y, pVertexData[pIndexArray[i]].pos.z);
			glVertex3f(
				pVertexData[pIndexArray[i]].pos.x + pNormals[j].x*s, 
				pVertexData[pIndexArray[i]].pos.y + pNormals[j].y*s, 
				pVertexData[pIndexArray[i]].pos.z + pNormals[j].z*s);
			i++;
			j++;
		}
		i += 2;
	}	
	glEnd();*/

	/*glBegin(GL_LINES);
	dword i;
	float s = 10.f;
	glColor3f(1.0f, 1.0f, 1.0f);
	for (i = 0; i < vertXRes*vertYRes; i++)
	{
		glVertex3f(pVertexData[i].pos.x, pVertexData[i].pos.y, pVertexData[i].pos.z);
		glVertex3f(
			pVertexData[i].pos.x + VertexNormal[i].x*s, 
			pVertexData[i].pos.y + VertexNormal[i].y*s, 
			pVertexData[i].pos.z + VertexNormal[i].z*s);
	}
	glEnd();*/

	RenderSkyBox();
}


void Terrain::RenderTerrain()
{
	dword	y;

	if (drawTexture)
	{
		glActiveTextureARB(GL_TEXTURE0_ARB); 
		pTexTerrain->Use();
		glEnable(GL_TEXTURE_2D);
		//glTexEnvi(GL_TEXTURE_ENV, GL_TEXTURE_ENV_MODE, GL_DECAL);
		//glTexEnvi(GL_TEXTURE_ENV, GL_TEXTURE_ENV_MODE, GL_COMBINE_EXT); 
	}

	if (drawDetail) 
	{
		glActiveTextureARB(GL_TEXTURE1_ARB); 
		pTexDetail->Use();
		glEnable(GL_TEXTURE_2D);
		//glTexEnvi( GL_TEXTURE_ENV, GL_TEXTURE_ENV_MODE, GL_MODULATE);
		//glTexEnvi(GL_TEXTURE_ENV, GL_COMBINE_RGB_EXT, GL_ADD_SIGNED_EXT); 
		glTexEnvi(GL_TEXTURE_ENV, GL_TEXTURE_ENV_MODE, GL_COMBINE_EXT); 
		glTexEnvi(GL_TEXTURE_ENV, GL_COMBINE_RGB_EXT, GL_ADD_SIGNED_EXT); 
	}

	if (drawLightmap)
	{
		glActiveTextureARB(GL_TEXTURE2_ARB); 
		pTexLightmap->Use();
		glEnable(GL_TEXTURE_2D);
		glTexEnvi(GL_TEXTURE_ENV, GL_TEXTURE_ENV_MODE, GL_MODULATE);
		glTexEnvi(GL_TEXTURE_ENV, GL_COMBINE_RGB_EXT, GL_ADD_SIGNED_EXT); 
	}

	
	for (y = 0; y < vertYRes-1; y++) 
		glDrawElements(wireFrame ? GL_LINE_STRIP : GL_TRIANGLE_STRIP, vertXRes*2, GL_UNSIGNED_SHORT, pIndexArray + y*vertXRes*2);

	if (drawLightmap)
	{
		glDisable(GL_TEXTURE_2D);
		glActiveTextureARB(GL_TEXTURE0_ARB); 
	}

	if (drawDetail)
	{
		if (!drawLightmap) 
		{
			glDisable(GL_TEXTURE_2D);
			glActiveTextureARB(GL_TEXTURE0_ARB); 
		}
	}

}


void Terrain::RenderSkyBox()
{
	glEnable(GL_TEXTURE_2D);
	// skybox

	const float u0 = 0.5f / 256.f,
				u1 = (256.f-0.5f) / 256.f,
				v0 = 0.5f / 256.f,
				v1 = (256.f-0.5f) / 256.f;

	const dword dataOfs[5][4] = {
		{7,6,2,3},
		{1,2,6,5},
		{0,1,5,4},
		{4,7,3,0},
		{3,2,1,0}
	};

	const float mapping[4][2] = { {u0, v1}, {u1,v1}, {u1,v0}, {u0, v0} };

	dword i;


	for (i=0; i<5; i++) {
		pTexSkyBox[i].Use();
		glBegin( GL_QUADS );

		sdword j;

		for (j=0; j<4; j++) {
			glTexCoord2d(mapping[j][0], mapping[j][1]);
			glVertex3fv(skyBoxVerts[dataOfs[i][j]].GetPtr());
		}

		glEnd();
	}

	glDisable(GL_TEXTURE_2D);
}


void Terrain::RenderWater()
{
	glEnable(GL_TEXTURE_2D);

	float waterTile = 2*60.f;

#define	WATER_STRIP_LEN	30
#define WATER_STRIP_LEN_1	(WATER_STRIP_LEN-1)

	Vector	temp;
	Vector	dU, dV;

	dU = skyBoxVerts[5] - skyBoxVerts[4];
	dV = skyBoxVerts[7] - skyBoxVerts[4];


	glEnable(GL_BLEND);
	glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);
	glColor4f(1,1,1,0.45f);

	pTexSkyBox[5].Use();


	for (dword j=0; j<WATER_STRIP_LEN; j++) {
		glBegin( GL_QUAD_STRIP );
		for (dword i=0; i<WATER_STRIP_LEN+1; i++) {
			for (sdword dy=1; dy>=0; dy--) {
				temp = skyBoxVerts[4];
				temp += dU * ((float)i / WATER_STRIP_LEN);
				temp += dV * ((float)(j+dy) / WATER_STRIP_LEN);
				glTexCoord2d((j+dy)*waterTile/WATER_STRIP_LEN, i*waterTile/WATER_STRIP_LEN);
				glVertex3fv(temp.GetPtr());
				//glVertex3f(temp.x, temp.y - (rand() % 2 - 1), temp.z); 
			}
		}
		glEnd();
	}

	glBlendFunc(GL_SRC_COLOR, GL_DST_COLOR);
	glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);

	float anim = sys_Time*0.00003f;

	for (int j=0; j<WATER_STRIP_LEN; j++) {
		glBegin( GL_QUAD_STRIP );
		for (dword i=0; i<WATER_STRIP_LEN+1; i++) {
			for (sdword dy=1; dy>=0; dy--) {
				temp = skyBoxVerts[4];
				temp += dU * ((float)i / WATER_STRIP_LEN);
				temp += dV * ((float)(j+dy) / WATER_STRIP_LEN);
				glTexCoord2d(anim+((j+dy)*waterTile/WATER_STRIP_LEN), anim+(i*waterTile/WATER_STRIP_LEN));
				glVertex3fv(temp.GetPtr());
				//glVertex3f(temp.x, temp.y - (rand() % 2 - 1), temp.z); 
			}
		}
		glEnd();
	}

	glDisable(GL_BLEND);

	glDisable(GL_TEXTURE_2D);
}