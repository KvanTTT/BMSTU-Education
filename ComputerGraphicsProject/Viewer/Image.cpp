
#include "std.h"
#include "Image.h"

Image::Image()
{
  m_nBPP = 0;
  m_nWidth = m_nHeight = m_nStride = 0;
  m_nData = 0;
  m_pData = 0;
}

Image::~Image()
{
  Free();
}


bool Image::Create( char *szImageName, fncImageLoader imgLoader)
{
  return imgLoader( szImageName, this );
}


bool Image::Create( UInt32 width, UInt32 height, UInt32 bpp )
{
  Free(); // Nuke any old image data.

  if( width==0 || height==0 || bpp==0 )
    return false;

  m_nWidth = width;
  m_nStride = width * ((bpp+7)/8);
  m_nHeight = height;
  m_nBPP = bpp;

  m_nData = m_nStride * m_nHeight;
  m_pData = new BYTE[m_nData];
  if( m_pData == 0 )
    return false;

  return true;
}


void Image::Free()
{
  if( m_pData )
  {
		delete [] (BYTE *) m_pData;
    m_pData = 0;
    m_nData = 0;
    m_nBPP = 0;
    m_nWidth = m_nHeight = m_nStride = 0;
  }
}
