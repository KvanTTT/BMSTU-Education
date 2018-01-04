<?xml version="1.0"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
<xsl:template match="/">
  <html>		
    <body>
      <h1>Russian Users</h1>
      <hr />
      <table width="80%" border="2">
        <tr bgcolor= "#e6e6fa">
          <td><b>UserName</b></td>
          <td><b>Project</b></td>
          <td><b>RegisterDate</b></td>
          <td><b>AllScore</b></td>
        </tr>
        <xsl:for-each select="Users/User">
        <tr bgcolor = "#faf8e6" color = "#003153">
          <td><xsl:value-of select="UserName" /></td>
          <td><xsl:value-of select="Project" /></td>
          <td><xsl:value-of select="RegisterDate" /></td>
          <td><xsl:value-of select="AllScore" /></td>
        </tr>
        </xsl:for-each>
      </table>		
    </body>
  </html>		
</xsl:template>
</xsl:stylesheet>