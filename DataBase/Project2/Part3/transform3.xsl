<?xml version="1.0"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="html" />
	<xsl:template match="/">
	<html>
		<body>
		<h2>Russian Users</h2>
		<xsl:apply-templates/>
		</body>
	</html>
	</xsl:template>
	<xsl:template match="tblUsers">
	<p>
		<xsl:apply-templates select="UserName"/>
		<xsl:apply-templates select="Project"/>	
		<xsl:apply-templates select="RegisterDate"/>
		<xsl:apply-templates select="AllScore"/>
	</p>
	</xsl:template>

	<xsl:template match="UserName">
		UserName: <span style="color:#f06b65; font-weight:bold;">
		<xsl:value-of select="."/>
		</span>
		<br />
	</xsl:template>
	
	<xsl:template match="Project">
		Project: <span style="color:#666623">
		<xsl:value-of select="."/>
		</span>
		<br />
	</xsl:template>
	
	<xsl:template match="RegisterDate">
		RegisterDate: <span style="color:#492737">
		<xsl:value-of select="."/>
		</span>
		<br />
	</xsl:template>
	
	<xsl:template match="AllScore">
		AllScore: <span style="color:#735184">
		<xsl:value-of select="."/>
		</span>
		<br />
	</xsl:template>
</xsl:stylesheet>