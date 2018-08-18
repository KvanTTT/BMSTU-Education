#include "Render.h"

RENDER_MODE CurrentRenderMode;

void SwitchRenderMode(RENDER_MODE mode)
{
	switch (mode)
	{
	case RENDERMODE_3D:
		if (CurrentRenderMode!= RENDERMODE_3D)
		{
			SwitchRenderMode(RENDERMODE_RESTORE_LAST);
			CurrentRenderMode = RENDERMODE_3D;
		}
		break;
	case RENDERMODE_2D:
		if (CurrentRenderMode!= RENDERMODE_2D)
		{
			SwitchRenderMode(RENDERMODE_RESTORE_LAST);
			glPushAttrib(GL_BLEND | GL_DRAW_BUFFER | GL_COLOR_BUFFER_BIT |
				GL_LOGIC_OP | GL_DEPTH_BUFFER_BIT |
				GL_DEPTH_TEST | GL_ENABLE_BIT | GL_LIGHTING_BIT | 
				GL_STENCIL_BUFFER_BIT );
			glMatrixMode(GL_PROJECTION);
			glPushMatrix();
			glLoadIdentity();
			glOrtho(0, 1, 1, 0, -100, 100);
			glMatrixMode(GL_MODELVIEW);
			glPushMatrix();
			glLoadIdentity();
			glDisable(GL_DEPTH_TEST);
			glBlendFunc(GL_ONE,GL_ONE);
			glDepthMask(FALSE);
			CurrentRenderMode = RENDERMODE_2D;
		}
		break;
	case RENDERMODE_RESTORE_LAST:
		if (CurrentRenderMode == RENDERMODE_2D)
		{
			glMatrixMode(GL_PROJECTION);
			glPopMatrix();
			glMatrixMode(GL_MODELVIEW);
			glPopMatrix();
			glPopAttrib();
		}
		CurrentRenderMode = NONE;
		break;
	}
}