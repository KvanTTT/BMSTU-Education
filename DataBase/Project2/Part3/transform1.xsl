<?xml version="1.0"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
 
    <xsl:output method="xml" indent="yes"/>
 
    <xsl:template match="Users">
        <transform>
            <xsl:apply-templates/>
        </transform>
    </xsl:template>
 
    <xsl:template match="User">
        <record>
            <xsl:apply-templates select="@*|*"/>
        </record>
    </xsl:template>
 
    <xsl:template match="@UserName">
        <username>
            <xsl:value-of select="."/>
        </username>
    </xsl:template>
 
    <xsl:template match="Project">
        <fullname>
            <xsl:apply-templates/>
            <xsl:apply-templates select="following-sibling::AllScore" mode="info"/>
        </fullname>
    </xsl:template>
 
    <xsl:template match="AllScore"/>
 
    <xsl:template match="AllScore" mode="info">
        <xsl:text> </xsl:text>
        <xsl:apply-templates/>
    </xsl:template>
 
</xsl:stylesheet>