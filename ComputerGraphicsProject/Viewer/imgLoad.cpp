
#include "std.h"
#include "imgLoad.h"
#include "Image.h"
#include "IOFile.h"
#include "IJL/ijl.h"

#pragma comment(lib, "IJL/ijl15.lib" )

typedef struct bmpFileHeader_tag_
{
	UInt8  B, M;
	UInt32 nSize;
	UInt32 nReserved;
	UInt32 nOffsetToBits;
} BMPFileHeader;

typedef struct bmpImageHeader_tag_
{
	UInt32 nSize;
	Int32  nWidth;
	Int32  nHeight;
	UInt16 nPlanes;
	UInt16 nBPP;
	UInt32 nCompression;
	UInt32 nImageSize;
	Int32  nWidthPPM;
	Int32  nHeightPPM;
	UInt32 nColorUsed;
	UInt32 nColorImportant;
} BMPImageHeader;

// This is our actual BMP image loading function.
bool imgLoadBMP(char* szImageName, Image *img)
{
	BMPFileHeader bfh;
	BMPImageHeader bih;
	UInt8 *pBMPData=0;

	IOFile filebmp; 

	if( img == 0 )
		return false;

	if( filebmp.Create( szImageName, "rb" ) == false )
		return false;

	filebmp >> bfh.B; filebmp >> bfh.M;
	filebmp >> bfh.nSize;
	filebmp >> bfh.nReserved;
	filebmp >> bfh.nOffsetToBits;

	if(( bfh.B != 'B' ) || (bfh.M != 'M') || filebmp.IsEOF())
	{
		filebmp.Free();
		return false;
	}

	filebmp >> bih.nSize;
	filebmp >> bih.nWidth;
	filebmp >> bih.nHeight;
	filebmp >> bih.nPlanes;
	filebmp >> bih.nBPP;
	filebmp >> bih.nCompression;
	filebmp >> bih.nImageSize;
	filebmp >> bih.nWidthPPM;
	filebmp >> bih.nHeightPPM;
	filebmp >> bih.nColorUsed;
	filebmp >> bih.nColorImportant;

	// Validate the image header results.
	if( (bih.nCompression != 0) ||
		(bih.nHeight == 0) ||
		(bih.nWidth == 0) )
	{
		filebmp.Free();
		return false;
	}

	// We must have either an 8, 24, or 32bit image.
	if( (bih.nBPP != 8) && (bih.nBPP != 24) && (bih.nBPP != 32) )
	{
		filebmp.Free();
		return false;
	}

	// Jump to the image data.
	filebmp.SeekFromStart( bfh.nOffsetToBits );

	// Prep the image data for loading.
	UInt32 nStride = (bih.nWidth * (bih.nBPP/8) + 1) & (~1); // Stride is the BMP's bytes per row.
	bih.nImageSize = nStride * bih.nHeight;
	pBMPData = new UInt8[bih.nImageSize];
	if( pBMPData == 0 )
	{
		filebmp.Free();
		return false;
	}

	// Load it up.. we want the image data raw.
	//  for (int i=bih.nHeight-1; i; i--)
	for (signed int i=bih.nHeight-1; i>=0; i--)
	{
		if( filebmp.Read( pBMPData+i*nStride, nStride ) == false )
		{
			filebmp.Free();
			delete pBMPData;
			return false; // In case we hit an IOFile error by now.
		}
	}

	/*  if( filebmp.Read( pBMPData, bih.nImageSize ) == false )
	{
	filebmp.Free();
	delete pBMPData;
	return false; // In case we hit an IOFile error by now.
	}*/

	// Create our target Image.
	img->Create( bih.nWidth, bih.nHeight, bih.nBPP );

	

	bool bFlipY;
	if( bih.nHeight < 0 )  // Negative height is top to bottom, right side up.
	{
		bih.nHeight = -bih.nHeight;
		bFlipY = false;
	}
	else // Positive height is bottom to top, or upside down.
		bFlipY = true;

	UInt8 *pDest=(UInt8*)img->GetData(), *pSrc;
	for( UInt32 y=0; y<(UInt32)bih.nHeight; y++ )
	{
		if( bFlipY ) // Handle pointer calculations for yFlip situations.
			pSrc = pBMPData + bih.nImageSize - (y * nStride) - nStride;
		else
			pSrc = pBMPData + (y * nStride);

		// Perform row copy of image data, and in 24 & 32bit cases,
		// swizzle those blue and red bytes around.
		switch( bih.nBPP )
		{
		case 8:
			{
				memcpy( pDest, pSrc, img->GetStride() );
			}
			break;

		case 24:  // from BGR order to RGB. So swap blue and red bytes.
			{
				for( UInt32 x=0; x<img->GetStride(); x+=3 )
				{
					pDest[x] = pSrc[x+2];
					pDest[x+1] = pSrc[x+1];
					pDest[x+2] = pSrc[x];
				}
			}
			break;

		case 32:  // 32bit is just like 24bit, but we go from BGRA to RGBA,
			{         // so green and alpha are left alone.
				for( UInt32 x=0; x<img->GetStride(); x+=4 )
				{
					pDest[x] = pSrc[x+2];
					pDest[x+1] = pSrc[x+1];
					pDest[x+2] = pSrc[x];
					pDest[x+3] = pSrc[x+3];
				}
			}
			break;

		default: // Only supporting 8, 24, and 32 image formats.
			{
				filebmp.Free();
				delete pBMPData;
				img->Free();
				return false;
			}
		}

		pDest += img->GetStride();
	}

	filebmp.Free();
	delete pBMPData;
	return true;
}


bool imgLoadJPG(char* szImageName, Image *img)
{
	BYTE Bpp8 = 3;		
	int RetVal;		
	JPEG_CORE_PROPERTIES image;
	ZeroMemory(&image, sizeof( JPEG_CORE_PROPERTIES ) );
	if( ijlInit(&image ) != IJL_OK )
		return false;

	image.JPGFile = szImageName;

	if ((RetVal = ijlRead(&image,IJL_JFILE_READPARAMS)) == IJL_OK)
	{
		img->Create(image.JPGHeight, image.JPGWidth, Bpp8*8);

		UInt8 *pDest = (UInt8*)img->GetData();
		UINT ImageSize = img->GetHeight() * img->GetWidth() * Bpp8;
		UInt8 *pSrc = new UInt8[ImageSize];

		if (pSrc)
		{
			image.DIBBytes = pSrc;
			image.DIBColor = IJL_RGB;
			image.DIBHeight = img->GetHeight();
			image.DIBWidth = img->GetWidth();
			image.DIBChannels = Bpp8;

			if ((RetVal = ijlRead(&image, IJL_JFILE_READWHOLEIMAGE)) == IJL_OK)
			{
				switch (img->GetBPP())
				{
				case 24: 
					{
						for(UINT i = 0; i < ImageSize; i += 3)	
						{									
							pDest[i + 0] = pSrc[i + 0];	
							pDest[i + 1] = pSrc[i + 1];
							pDest[i + 2] = pSrc[i + 2];
						}
						break;  
					}
				case 32:  
					{
						for(UINT i = 0; i < ImageSize; i += 4)	
						{									
							pDest[i + 0] = pSrc[i + 0];	
							pDest[i + 1] = pSrc[i + 1];
							pDest[i + 2] = pSrc[i + 2];
							pDest[i + 3] = pSrc[i + 3];
						}
						break; 
					}
				}
			}
		}
		delete pSrc;  
	}
	ijlFree(&image); 

	return true;
}