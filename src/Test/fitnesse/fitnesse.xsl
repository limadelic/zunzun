<?xml version="1.0" encoding="UTF-8" ?>
<!-- Written by Gregor Gramlich -->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:include href="fitnesseResultsToHtml.xsl" />

<xsl:template name="css_and_javascript">
    <style type="text/css">
        <xsl:value-of select="document('FitNesseRoot/files/css/fitnesse_base.css')" disable-output-escaping="yes" />
    </style>
    <script language="JavaScript" type="text/javascript">
        <xsl:value-of select="document('FitNesseRoot/files/javascript/fitnesse.js')" disable-output-escaping="yes" />
        <xsl:value-of select="document('FitNesseRoot/files/javascript/fitnesseResults.js')" disable-output-escaping="yes" />
        <![CDATA[
if (isIE()) {
    collapsableOpenImg = "http://fitnesse.org/files/images/collapsableOpen.gif";
    collapsableClosedImg = "http://fitnesse.org/files/images/collapsableClosed.gif";
} else {
    collapsableOpenImg = "data:image/gif;base64,R0lGODlhDAAMANU5AD8+Pu7u7vz8/CsrK/39/RoZGSorKxkaGqOjo8jIyD4+P1RTVD4+PsTExPf39/b29nx7e0FBQeLi4s7OzpmZmdPT0yMkJAsLCv7+/tnZ2eTk5PPz84eHhwoKCgsLC1NTUz8/Pl5eXufn51NTUjMzMvv7+42Njd3d3QsKCrW1tQsKC25ubri4uHh3dz09PZCQkAoLCq2urREQEJeXl2lpaRkZGUdHR/Hx8WZmZv///wAAAAAAAAAAAAAAAAAAAAAAACH5BAEAADkALAAAAAAMAAwAAAZXwNwjcCsaA5tcLuWyOZ8MijJjQXVgnotKxlLmTIVa7VAr0AReTWQwMBhIDa/yBVIAAKuSPCcKfUYLCXtKCDg4HASDORIQLROKOQIxMw6QOScVljkYiXJBADs=";
    collapsableClosedImg = "data:image/gif;base64,R0lGODlhDAAMANU8APPz8/v7+/f39/Ly8l9fX0hISElISEhJSDMyMsDAwB0dHerq6oyMiwwNDGNjY9bW1nR0dHNzdF5eXl1cXfr6+u7u7vz8/MTExOPj4zo6OqWlpVZWVlBQUF9eXmtrayMjIh4eHR0dHvn5+Xx7fHt8e/X19WBgYEtKSgwMDB8eHxMTE5aWlpKSkpmZmqampl5fX6GhoYGBgejp6ba2t2dnZ93d3bKyssPDw/b29r29vfT09DIyMv///wAAAAAAAAAAACH5BAEAADwALAAAAAAMAAwAAAZTQJ5wSBQCNLcisZJROS5K3oCDaqQ2CQsRMFGEQIqPZDYQ6jy79A6xNtkCJVKhYDjUT6NEAMd4dQgENCsPIkICLREQMS4YRQIwLDkyURQ1C1GYSkEAOw==";
}
        ]]>
    </script>
</xsl:template>

</xsl:stylesheet>

