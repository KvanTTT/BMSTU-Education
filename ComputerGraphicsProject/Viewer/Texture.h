#ifndef __Texture_h_
#define __Texture_h_

#include "Image.h"

class Texture
{
public:
	Texture();
	~Texture();

	bool Create( char *szImageName, fncImageLoader imgLoader);

	bool Create(Image *pImage, Image *pAlpha=0);
	void Free();

	void Use();

protected:
	unsigned int m_nID;
};

#endif 