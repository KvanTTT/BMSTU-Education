#pragma once

#include "base.h"

enum RENDER_MODE 
{
	NONE,
	RENDERMODE_3D,							
	RENDERMODE_2D,							
	RENDERMODE_RESTORE_LAST,				
};

extern RENDER_MODE CurrentRenderMode;

void SwitchRenderMode(RENDER_MODE mode);