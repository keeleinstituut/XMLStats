<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

	<xsl:output method="xml" omit-xml-declaration="no" indent="no" encoding="utf-8"/>

  <xsl:variable name="fr_sym">^0123456789_%+/|AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsŠšZzŽžTtUuVvWwÕõÄäÖöÜüXxYy()[]</xsl:variable>
  <xsl:variable name="to_sym">&#xE001;&#xE002;&#xE003;&#xE004;&#xE005;&#xE006;&#xE007;&#xE008;&#xE009;&#xE00A;&#xE00B;&#xE00C;&#xE00D;&#xE00E;&#xE00F;&#xE010;&#xE011;&#xE011;&#xE014;&#xE014;&#xE017;&#xE017;&#xE01A;&#xE01A;&#xE01D;&#xE01D;&#xE020;&#xE020;&#xE023;&#xE023;&#xE026;&#xE026;&#xE029;&#xE029;&#xE02C;&#xE02C;&#xE02F;&#xE02F;&#xE032;&#xE032;&#xE035;&#xE035;&#xE038;&#xE038;&#xE03B;&#xE03B;&#xE03E;&#xE03E;&#xE041;&#xE041;&#xE044;&#xE044;&#xE047;&#xE047;&#xE04A;&#xE04A;&#xE04D;&#xE04D;&#xE050;&#xE050;&#xE053;&#xE053;&#xE056;&#xE056;&#xE059;&#xE059;&#xE05C;&#xE05C;&#xE05F;&#xE05F;&#xE062;&#xE062;&#xE065;&#xE065;&#xE068;&#xE068;&#xE06B;&#xE06B;&#xE06E;&#xE06E;</xsl:variable>


  <xsl:variable name="dic_desc"></xsl:variable>
  
  <!--ei kõlba EVS jaoks, seal on vaja järjestamisel ka tühikut arvestada-->
  <xsl:variable name="etfr">
    <xsl:value-of select="$fr_sym"/>
    <xsl:if test="not($dic_desc = 'evs')">
      <xsl:text> </xsl:text>
    </xsl:if>
  </xsl:variable>


  <xsl:template match="/">
		<xsl:copy>
			<xsl:apply-templates select="al:sr" />
		</xsl:copy>
	</xsl:template>


	<xsl:template match="al:sr">
		<xsl:copy>
      <xsl:copy-of select="@*" />

      <xsl:apply-templates select="al:A">
        <xsl:sort select="asendus/trahh/blaah" data-type="text" order="ascending" />
      </xsl:apply-templates>

      <!--<xsl:choose>
        <xsl:when test="$dic_desc = 'od_'">
          <xsl:apply-templates select="al:A">
            <xsl:sort select="al:P/al:tmnr/@al:O" data-type="number" order="ascending" case-order="lower-first" />
          </xsl:apply-templates>
        </xsl:when>
        <xsl:when test="$dic_desc = 'sp_'">
          <xsl:apply-templates select="al:A">
            <xsl:sort select="translate(al:P/al:mg/al:mag/al:m/@al:O, $etfr, $to_sym)" data-type="text" order="ascending" />
          </xsl:apply-templates>
        </xsl:when>
        <xsl:when test="$dic_desc = 'har'">
          <xsl:apply-templates select="al:A">
            <xsl:sort select="translate(al:P/al:ep/al:terg/al:ter/@al:O, $etfr, $to_sym)" data-type="text" order="ascending" />
          </xsl:apply-templates>
        </xsl:when>
        <xsl:when test="$dic_desc = 'ief'">
          <xsl:apply-templates select="al:A">
            <xsl:sort select="concat(translate(al:P/al:terg/al:ter/@al:O, $etfr, $to_sym), al:P/al:terg/al:ter/@al:O)" data-type="text" order="ascending" />
            <xsl:sort select="al:P/al:terg/al:ter/@al:i" data-type="text" order="ascending" />
          </xsl:apply-templates>
        </xsl:when>

        <xsl:otherwise>
          <xsl:apply-templates select="al:A">
            <xsl:sort select="translate(al:P/al:mg/al:m/@al:O, $etfr, $to_sym)" data-type="text" order="ascending" />
          </xsl:apply-templates>
        </xsl:otherwise>
      </xsl:choose>-->

      
    </xsl:copy>
	</xsl:template>


	<xsl:template match="al:A">
		<xsl:copy-of select="current()" />
	</xsl:template>


</xsl:stylesheet>
