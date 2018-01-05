// ControlWork1.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

// http://uva.onlinejudge.org/external/3/327.html
// Runtime: 0.052
// Tag: Adhoc 

// @BEGIN_OF_SOURCE_CODE

#include <iostream>
#include <cstdio>
#include <algorithm>
#include <cstring>
#include <string>
#include <cctype>
#include <stack>
#include <queue>
#include <list>
#include <vector>
#include <map>
#include <sstream>
#include <cmath>
#include <bitset>
#include <utility>
#include <set>
#include <numeric>

#define INF_MAX 2147483647
#define INF_MIN -2147483647
#define pi acos(-1.0)
#define N 100010
#define LL long long

#define For(i, a, b) for( int i = (a); i < (b); i++ )
#define Fors(i, sz) for( size_t i = 0; i < sz.size (); i++ )
#define Fore(it, x) for(typeof (x.begin()) it = x.begin(); it != x.end (); it++)
#define Set(a, s) memset(a, s, sizeof (a))
#define Read(r) freopen(r, "r", stdin)
#define Write(w) freopen(w, "w", stdout)

using namespace std;

char input [1000];
string str;
bool isVarPresent [30];
int value [30];
int final_value [30];

void reset ()
{
	str = "";
	for ( int i = 0; i < 30; i++ ) {
		value [i] = i;
		isVarPresent [i] = false;
	}
}

int main ()
{
	while ( gets (input) ) {
		reset ();

		for ( int i = 0; input [i]; i++ ) {
			if ( input [i] != ' ' ) str += input [i];
			if ( isalpha (input [i]) ) isVarPresent [input [i] - 'a' + 1] = true;
		}

		// eliminating pre-inc | pre-dec
		for ( size_t i = 2; i < str.length (); i++ ) {
			if ( str [i] >= 'a' && str [i] <= 'z' && str [i - 1] == str [i - 2] ) {
				if ( str [i - 1] == '+' ) value [str [i] - 'a' + 1]++;
				else value [str [i] - 'a' + 1]--;
				str.erase (str.begin () + (i - 1));
				str.erase (str.begin () + (i - 2));
				i--;
			}
		}

		for ( int i = 0; i < 30; i++ ) final_value [i] = value [i];

		// eliminating post-inc | post-dec
		int p = str.length ();
		for ( int i = 0; i < p - 2; i++ ) {
			if ( str [i] >= 'a' && str [i] <= 'z' && str [i + 1] == str [i + 2] ) {
				if ( str [i + 1] == '+' ) final_value [str [i] - 'a' + 1]++;
				else final_value [str [i] - 'a' + 1]--;
				str.erase (str.begin () + (i + 1));
				str.erase (str.begin () + (i + 1));
			}
			p = str.length ();
		}

		int res = 0;
		int op;
		if ( str [0] != '-' ) str = "+" + str;

		for ( size_t i = 0; i < str.length (); i++ ) {
			if ( str [i] == '+' ) op = 1;
			else if( str [i] == '-' ) op = 2;
			else {
				if ( op == 1 ) res += value [str [i] - 'a' + 1];
				else res -= value [str [i] - 'a' + 1];
			}
		}

		printf ("Expression: %s\n", input);
		printf ("    value = %d\n", res);

		for ( int i = 1; i <= 26; i++ ) {
			if ( isVarPresent [i] ) printf ("    %c = %d\n", 'a' + (i - 1), final_value [i]);
		}

	}

	return 0;
}

