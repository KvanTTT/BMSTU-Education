#pragma once

#include "base.h"
#include "Win32.h"

void BuildFont(GLvoid);
void KillFont(GLvoid);

void Print2D(float x, float y, unsigned int rgba, const char *Text,...);

extern unsigned int Base;
