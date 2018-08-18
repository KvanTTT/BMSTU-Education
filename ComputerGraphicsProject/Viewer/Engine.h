#ifndef __YourCode_H__
#define __YourCode_H__

#include "base.h"


bool appPreInit();  
bool appInitGL();     
bool appRender();   
void appShutdown(); 
bool ProcessKey(HWND hWnd, WPARAM wParam);

// Return a null terminated string.
char *appGetTitleBar();      // The title bar refreshes every half sec.


extern float g_frameTime;


#endif 