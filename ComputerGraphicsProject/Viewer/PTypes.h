
#ifndef __Defines_
#define __Defines_


typedef long Bool;
typedef long Int;
typedef float Float;

typedef unsigned char UInt8;
typedef signed char   Int8;

typedef unsigned short  UInt16;
typedef short           Int16;

typedef unsigned long UInt32;
typedef long          Int32;

typedef char*         PStr;	        // pointer to a string
typedef const char *  PConstStr;	    // pointer to a constant string

typedef UInt8*  PUInt8;
typedef Int8*   PInt8;

typedef UInt16* PUInt16;
typedef Int16*  PInt16;

typedef UInt32* PUInt32;
typedef Int32*  PInt32;

// Free pointer based allocations in one fell swoop.
#define NUKE(x) if(x != 0) {delete x; x=0;} 

// Platform specific debug info.
#ifdef _DEBUG
	#define __DEBUG
#endif //DEBUG

// CDECL is used for functions that take variable arguments.
#ifndef CDECL
	#define CDECL __cdecl
#endif

// APIDECL is used on global public functions.
#ifndef APIDECL
	#define APIDECL __stdcall
#endif

//-------------------------------------------
#define sqr( X )	( (X) * (X) )	                   	// Square the X
#define PI				3.14159265358979323846f
#define DEG2RAD(X)		(PI*(X)/180.0)	// Degrees -> Radians
#define RAD2DEG(X)		((X)*180.0/PI)	// Radians -> Degrees
#define rnd()		((float)rand()*(1.0f /(Float)RAND_MAX))  // Floating point random number generator ( 0 -> 1)

// fabs: Absolute function.
//#undef abs
//#define abs(a)     ( (a) > 0 ? (a) : -(a) )

// Force sign clamping to (-1;0;1)
#define sgn(a)      ((a)<0?-1:((a)?1:0))

#ifndef max
	#define max( a, b )   ((a) > (b) ? (a) : (b))
#endif

#ifndef min
	#define min( a, b )   ((a) < (b) ? (a) : (b))
#endif

#endif
