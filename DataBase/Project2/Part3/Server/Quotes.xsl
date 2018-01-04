<?xml version="1.0"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  version="1.0">
  <xsl:template match="/">
    <html>
      <body>
        <h1 style="background-color: teal; color: white;
           font-size: 24pt; text-align: center; letter-spacing: 1.0em">
          Famous Quotes
        </h1>
        <table border="1">
          <tr style="font-size: 12pt; font-family: verdana;
            font-weight: bold">
            <td style="text-align: center">Quote</td>
            <td style="text-align: center">Author</td>
          </tr>
          <xsl:for-each select="Quotes/Quote">
            <xsl:sort select="Author" />
            <tr style="font-size: 10pt; font-family: verdana">
              <td><xsl:value-of select="Text"/></td>
              <td><i><xsl:value-of select="Author"/></i></td>
            </tr>
          </xsl:for-each>
        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>