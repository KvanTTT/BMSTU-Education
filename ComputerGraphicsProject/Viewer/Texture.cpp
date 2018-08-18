
#include "std.h"
#include "Texture.h"
#include "Image.h"

Texture::Texture()
{
  m_nID=0;
}

Texture::~Texture()
{
  Free();
}

bool Texture::Create( char *szImageName, fncImageLoader imgLoader )
{
  Image img;
  if( img.Create( szImageName, imgLoader ) == false )
    return false;

  return Create( &img );
}

// Create is protected, since you're supposed to use the LoadBLAH() methods now.
bool Texture::Create( Image *pImage, Image *pAlpha )
{
  Free();

  // Generate texture id.
  glGenTextures(1, &m_nID);
  m_nID++; // Use an offset of +1 to differentiate from non-initialized state.

  glBindTexture(GL_TEXTURE_2D, m_nID-1);
  glTexParameterf(GL_TEXTURE_2D,GL_TEXTURE_WRAP_S, GL_REPEAT);
  glTexParameterf(GL_TEXTURE_2D,GL_TEXTURE_WRAP_T, GL_REPEAT);
  glTexParameterf(GL_TEXTURE_2D,GL_TEXTURE_MAG_FILTER, GL_LINEAR);
  glTexParameterf(GL_TEXTURE_2D,GL_TEXTURE_MIN_FILTER, GL_LINEAR_MIPMAP_NEAREST);

  if( pAlpha && (pImage->GetBPP() == 8))
  {
    if( ((pAlpha->GetBPP() == 8) &&
        (pAlpha->GetWidth()==pImage->GetWidth()) &&
        (pAlpha->GetHeight()==pImage->GetHeight())
       ) == false )
    {
      Free();
      return false;
    }

    // Build a 16bit final image, for texture creation...
    Image imgFinal;
    if( imgFinal.Create( pImage->GetWidth(), pImage->GetHeight(), 16 ) == false)
    { // Make sure we catch a bad image creation failure.
      Free();
      return false;
    }

    // Combine Blit the source image & alpha into the final 32bit image.
    {
      UInt8 *pFinal = (UInt8*)imgFinal.GetData();
      UInt8 *pSrcData = (UInt8*)pImage->GetData();
      UInt8 *pSrcAlpha = (UInt8*)pAlpha->GetData();

      // Now blit 8 (Image and Alpha) to 16bit (LumiAlpha)
      for( UInt32 iY=0; iY<pImage->GetHeight(); iY++)
      {
        for( UInt32 iX=0; iX<pImage->GetWidth(); iX++, pFinal+=2, pSrcData++, pSrcAlpha++)
        {
          pFinal[0] = *pSrcData;
          pFinal[1] = *pSrcAlpha;
        }
      }
    }

    gluBuild2DMipmaps(GL_TEXTURE_2D, 2, imgFinal.GetWidth(), imgFinal.GetHeight(),
                      GL_LUMINANCE_ALPHA, GL_UNSIGNED_BYTE, imgFinal.GetData());
    imgFinal.Free();
    return true;
  } else if( pAlpha && (pImage->GetBPP() == 24))
  {
    if( ((pAlpha->GetBPP() == 8) &&
        (pAlpha->GetWidth()==pImage->GetWidth()) &&
        (pAlpha->GetHeight()==pImage->GetHeight())
       ) == false )
    {
      Free();
      return false;
    }

    // Build a 32bit final image, for texture creation...
    Image imgFinal;
    if( imgFinal.Create( pImage->GetWidth(), pImage->GetHeight(), 32 ) == false)
    { 
      Free();
      return false;
    }

    {
      UInt8 *pFinal = (UInt8*)imgFinal.GetData();
      UInt8 *pSrcData = (UInt8*)pImage->GetData();
      UInt8 *pSrcAlpha = (UInt8*)pAlpha->GetData();

      // Now blit 8 (Image and Alpha) to 16bit (LumiAlpha)
      for( UInt32 iY=0; iY<pImage->GetHeight(); iY++)
      {
        for( UInt32 iX=0; iX<pImage->GetWidth(); iX++, pFinal+=4, pSrcData+=3, pSrcAlpha++)
        {
          pFinal[0] = pSrcData[0];
          pFinal[1] = pSrcData[1];
          pFinal[2] = pSrcData[2];
          pFinal[3] = pSrcAlpha[0];
        }
      }
    }

    // Upload the texture, and finish our work.
    gluBuild2DMipmaps(GL_TEXTURE_2D, 4, imgFinal.GetWidth(), imgFinal.GetHeight(),
                      GL_RGBA, GL_UNSIGNED_BYTE, imgFinal.GetData());
    imgFinal.Free();
    return true;
  } else
  { 
    switch( pImage->GetBPP() )
    { 
      case 8: // Upload a grayscale image.
        gluBuild2DMipmaps(GL_TEXTURE_2D, 1, pImage->GetWidth(), pImage->GetHeight(),
          GL_LUMINANCE, GL_UNSIGNED_BYTE, pImage->GetData());
        return true;
      case 24: // Upload an RGB 24bit image.
        gluBuild2DMipmaps(GL_TEXTURE_2D, 3, pImage->GetWidth(), pImage->GetHeight(),
          GL_RGB, GL_UNSIGNED_BYTE, pImage->GetData());
        return true;
      case 32: // Upload an RGBA 32bit image.
        gluBuild2DMipmaps(GL_TEXTURE_2D, 4, pImage->GetWidth(), pImage->GetHeight(),
          GL_RGBA, GL_UNSIGNED_BYTE, pImage->GetData());
        return true;
    }
  }

  return true;
}

void Texture::Free()
{
  if( m_nID )
  {
    m_nID--;
    glDeleteTextures(1, &m_nID);
    m_nID=0;
  }
}

void Texture::Use()
{
  glBindTexture(GL_TEXTURE_2D, m_nID-1);
}

