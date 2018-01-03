#ifndef POL_H
#define POL_H

#include <stdio.h>
#include <conio.h>
#include <iostream>
#include <math.h>
#include "stack.h"

const int MAX_LENGTH = 40;
const int DIVISION_BY_ZERO = 1;

char *replace(char *str, char *symbols, int *numbers);

double  solve(char* str);

_stack* make_pol(char* str);
double  calc(_stack* stack);

double  binary(double a, double b, char t);
double  unary(double a, char t);
int     prior(char sym);
double  find_number(char* str, unsigned& i);

#endif
