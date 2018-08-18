#ifndef __imgLoadBMP_h_
#define __imgLoadBMP_h_

#if _MSC_VER > 1000
#pragma once
#endif 

class Image;

bool imgLoadBMP(char* szImageName, Image *img);
bool imgLoadJPG(char* szImageName, Image *img);

#endif