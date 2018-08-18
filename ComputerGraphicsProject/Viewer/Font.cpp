#include "Font.h"
#include <stdarg.h>			
#include "Render.h"

unsigned int Base = 0;
char buf[256];

void BuildFont(void)								// Build Our Bitmap Font
{
	HFONT	font;										// Windows Font ID
	HFONT	oldfont;									// Used For Good House Keeping

	Base = glGenLists(96);								// Storage For 96 Characters

	font = CreateFont(	-16,							// Height Of Font
						0,								// Width Of Font
						0,								// Angle Of Escapement
						0,								// Orientation Angle
						FW_DONTCARE,						// Font Weight
						FALSE,							// Italic
						FALSE,							// Underline
						FALSE,							// Strikeout
						ANSI_CHARSET,					// Character Set Identifier
						OUT_TT_PRECIS,					// Output Precision
						CLIP_DEFAULT_PRECIS,			// Clipping Precision
						ANTIALIASED_QUALITY,			// Output Quality
						FF_DONTCARE|DEFAULT_PITCH,		// Family And Pitch
						"Lucida Console");					// Font Name

	oldfont = (HFONT)SelectObject(hDC, font);           // Selects The Font We Want
	wglUseFontBitmaps(hDC, 32, 96, Base);				// Builds 96 Characters Starting At Character 32
	SelectObject(hDC, oldfont);							// Selects The Font We Want
	DeleteObject(font);									// Delete The Font
}

void KillFont(void)									// Delete The Font List
{
	glDeleteLists(Base, 96);							// Delete All 96 Characters
}


void Print2D(float x, float y, unsigned int rgba, const char *Text,...)
{
	va_list		va;									
	if (Text=="")									
		return;										

	va_start(va, Text);
	vsprintf(buf, Text, va);
	va_end(va);
	
	glPushMatrix();
	glLoadIdentity();
	//glColor4ubv((unsigned char*) &rgba);
	glColor3f(1, 1, 1);
	glRasterPos2f(x, y);
	glListBase(Base-32);
	glCallLists((GLsizei)strlen(buf), GL_UNSIGNED_BYTE, buf);
	glPopMatrix();
}

