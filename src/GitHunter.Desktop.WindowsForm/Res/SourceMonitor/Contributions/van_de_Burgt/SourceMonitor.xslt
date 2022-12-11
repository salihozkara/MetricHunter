<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">

    <!-- Created by Edwin vd Burgt. No warranty whatsoever. Use and modify as you wish. -->

    <xsl:variable name="MaxComplexity">8</xsl:variable>
    <xsl:variable name="MaxDepth">6</xsl:variable>

    <xsl:template match="/">
        <html dir="ltr">
            <head>
                <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
                <style type="text/css">
                    body
                    {
                    margin-left: 0px;
                    color: #888888;
                    text-indent: 10px;
                    line-height: 40px;
                    font-family: verdana, arial, helvetica;
                    margin-top: 0px;
                    }

                    div.Links
                    {
                    margin-top: -15px;
                    }

                    H1
                    {
                    font-size: medium;
                    margin-left: 0px;
                    color: #ebebeb;
                    text-indent: 0px;
                    line-height: 40px;
                    border-bottom: #666666 2px groove;
                    background-color: #4384bd;
                    font-family: verdana, arial, helvetica;
                    padding-top: 0em;
                    margin-top: 0px;
                    }

                    H2
                    {
                    font-size: medium;
                    margin-left: 0px;
                    color: #ebebeb;
                    text-indent: 20px;
                    line-height: 20px;
                    background-color: #65a6df;
                    font-family: verdana, arial, helvetica;
                    }

                    A.HeaderLink
                    {
                    color: white;
                    }
                    A.HeaderLink:hover
                    {
                    color: white;
                    }
                    A.HeaderLink:visited
                    {
                    color: white;
                    }
                    A.SmallLink
                    {
                    font-weight: bolder;
                    color: #003399;
                    font-family: verdana, arial, helvetica;
                    }
                    A.SmallLink:visited
                    {
                    font-weight: bolder;
                    color: #003399;
                    font-family: verdana, arial, helvetica;
                    }
                    A.SmallLink:hover
                    {
                    color: red;
                    font-family: verdana, arial, helvetica;
                    }

                    A.ExpandCollapseLink
                    {
                    font-size: xx-small;
                    text-decoration: none;
                    font-weight: bolder;
                    color: #003399;
                    font-family: verdana, arial, helvetica;
                    }
                    A.ExpandCollapseLink:visited
                    {
                    font-size: xx-small;
                    text-decoration: none;
                    font-weight: bolder;
                    color: #003399;
                    font-family: verdana, arial, helvetica;
                    }
                    A.ExpandCollapseLink:hover
                    {
                    font-size: xx-small;
                    text-decoration: none;
                    color: red;
                    font-family: verdana, arial, helvetica;
                    }

                    A.CorpLink
                    {
                    font-size:smaller;
                    font-weight: bolder;
                    color: #003399;
                    font-family: verdana, arial, helvetica;
                    }
                    A.CorpLink:visited
                    {
                    font-size:smaller;
                    font-weight: bolder;
                    color: #003399;
                    font-family: verdana, arial, helvetica;
                    }
                    A.CorpLink:hover
                    {
                    font-size:smaller;
                    color: red;
                    font-family: verdana, arial, helvetica;
                    }

                    TABLE.MeasurementsTable
                    {
                    border: solid 0 black;
                    border-collapse:collapse;
                    margin-left: 15px;
                    margin-top: 5px;
                    margin-bottom: 5px;
                    font-family: verdana, arial, helvetica;
                    }

                    TD.NameDataCell
                    {
                    border: solid 1 grey;
                    border-collapse:collapse;
                    text-align: left;
                    font-size: smaller;
                    font-family: verdana, arial, helvetica;
                    font-weight: bolder;
                    color: black;
                    padding-right: 4px;
                    padding-left: 4px;
                    white-space: nowrap;
                    }

                    TD.DataCell
                    {
                    border: solid 1 grey;
                    border-collapse:collapse;
                    text-align: right;
                    font-size: smaller;
                    font-family: verdana, arial, helvetica;
                    color: black;
                    padding-right: 4px;
                    padding-left: 4px;
                    white-space: nowrap;
                    }

                    TH.HeaderCellHorizontal
                    {
                    border-right: black 1px solid;
                    border-top: black 1px solid;
                    border-left: black 1px solid;
                    border-bottom: black 1px solid;
                    border-collapse: collapse;
                    color: black;
                    background-color: gainsboro;
                    text-align: center;
                    font-size: smaller;
                    font-family: verdana, arial, helvetica;
                    padding-right: 4px;
                    padding-left: 4px;
                    white-space: nowrap;
                    }

                    TH.HeaderCell
                    {
                    border-right: black 1px solid;
                    border-top: black 1px solid;
                    border-left: black 1px solid;
                    border-bottom: black 1px solid;
                    border-collapse: collapse;
                    color: black;
                    background-color: gainsboro;
                    text-align: center;
                    font-size: smaller;
                    font-family: verdana, arial, helvetica;
                    writing-mode: tb-rl;
                    }

                </style>
                <title>
                    <xsl:value-of select="/sourcemonitor_metrics/project/project_name"/> metrics (<xsl:value-of
                        select="/sourcemonitor_metrics/project/project_directory"/>)
                </title>
            </head>
            <body>
                <div class="links" align="right" style="font-size: x-small">
                    <a class="ExpandCollapseLink" href="SourceMonitor-summary.xml">Summary</a>
                    |
                    <a class="ExpandCollapseLink" href="SourceMonitor-details.xml">Details</a>
                    |
                    <a class="ExpandCollapseLink" href="javascript:ToggleDisplay('HideDetails');"><span
                            name="HideDetails" style="display: ''">&#8226;</span>Toggle details
                    </a>
                    |
                    <a class="ExpandCollapseLink" href="javascript:ToggleDisplay('ShowAllMethods');"><span
                            name="ShowAllMethods" style="display: 'none'">&#8226;</span>Toggle all methods
                    </a>
                    |
                    <a class="ExpandCollapseLink" href="javascript:ToggleDisplay('ShowOlderMetrics');"><span
                            name="ShowOlderMetrics" style="display: 'none'">&#8226;</span>Toggle older metrics
                    </a>
                </div>
                <h1>
                    <xsl:value-of select="/sourcemonitor_metrics/project/project_name"/> metrics
                </h1>
                Project directory:
                <xsl:value-of select="/sourcemonitor_metrics/project/project_directory"/>
                <xsl:apply-templates select="//checkpoints/checkpoint">
                    <!-- <xsl:sort select="position()" data-type="number" order="descending"/>
                    <xsl:sort select="@checkpoint_date" order="descending" />-->
                </xsl:apply-templates>

                <script language="javascript">
                    function ToggleDisplay(displayElement)
                    {
                    var elems = document.getElementsByTagName("div");
                    <!--var elems = document.getElementsByName(displayElement);
                    var count = elems.length;-->
                    for (var i=0; i &lt; elems.length; i++)
                    {
                    if (elems[i].name == displayElement)
                    {
                    if (elems[i].style.display == '')
                    {
                    elems[i].style.display = 'none';
                    }
                    else
                    {
                    elems[i].style.display = '';
                    }
                    }
                    }
                    var elems2 = document.getElementsByTagName("span");
                    for (var i=0; i &lt; elems2.length; i++)
                    {
                    if (elems2[i].name == displayElement)
                    {
                    if (elems2[i].style.display == '')
                    {
                    elems2[i].style.display = 'none';
                    }
                    else
                    {
                    elems2[i].style.display = '';
                    }
                    }
                    }

                    }
                </script>

            </body>
        </html>
    </xsl:template>

    <xsl:template match="//checkpoint">
        <xsl:choose>
            <xsl:when test="position() = 1">
                <xsl:call-template name="chkpnt"/>
            </xsl:when>
            <xsl:otherwise>
                <div name="ShowOlderMetrics" style="display: 'none'">
                    <xsl:call-template name="chkpnt"/>
                </div>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>

    <xsl:template name="chkpnt">
        <h2><xsl:value-of select="./@checkpoint_date"/>:
            <xsl:value-of select="./@checkpoint_name"/>
        </h2>
        <table class="MeasurementsTable">
            <tr class="HeaderRow">
                <th class="HeaderCell">File</th>
                <xsl:for-each select="/sourcemonitor_metrics/project/metric_names/metric_name">
                    <th class="HeaderCell">
                        <xsl:value-of select="."/>
                    </th>
                </xsl:for-each>
            </tr>
            <xsl:choose>
                <xsl:when test="count(files/file) &gt; 0">
                    <xsl:apply-templates select="files/file"/>
                </xsl:when>
                <xsl:otherwise>
                    <xsl:call-template name="summary"/>
                </xsl:otherwise>
            </xsl:choose>
        </table>
    </xsl:template>

    <xsl:template match="file" name="summary">
        <tr class="DataRow">
            <td class="NameDataCell">
                <xsl:value-of select="@file_name"/>
            </td>
            <xsl:for-each select="metrics/metric">
                <td class="DataCell">
                    <xsl:choose>
                        <xsl:when test="contains(.,'()')">
                            ...
                        </xsl:when>
                        <xsl:otherwise>
                            <xsl:value-of select="."/>
                        </xsl:otherwise>
                    </xsl:choose>
                </td>
            </xsl:for-each>
        </tr>
        <tr class="DataRow">
            <td class="DataCell"></td>
            <xsl:variable name="ColumnTotal">
                <xsl:value-of select="count(/sourcemonitor_metrics/project/metric_names/metric_name)"/>
            </xsl:variable>
            <td class="NameDataCell" colspan="{$ColumnTotal}">

                <xsl:choose>
                    <xsl:when test="count(method_metrics/method[complexity &gt; $MaxComplexity]) &gt; 0">
                        <div name="HideDetails" style="display: ''">
                            <table class="MeasurementsTable">
                                <tr class="HeaderRow">
                                    <th class="HeaderCellHorizontal">Too complex</th>
                                    <th class="HeaderCellHorizontal">Complexity</th>
                                    <th class="HeaderCellHorizontal">Max. depth</th>
                                    <th class="HeaderCellHorizontal">Statements</th>
                                    <th class="HeaderCellHorizontal">Calls</th>
                                </tr>
                                <xsl:apply-templates select="method_metrics/method[complexity &gt; $MaxComplexity]">
                                    <xsl:sort select="complexity" data-type="number" order="descending"/>
                                </xsl:apply-templates>
                            </table>
                        </div>
                    </xsl:when>
                </xsl:choose>

                <xsl:choose>
                    <xsl:when test="count(method_metrics/method[maximum_depth &gt; $MaxDepth]) &gt; 0">
                        <div name="HideDetails" style="display: ''">
                            <table class="MeasurementsTable">
                                <tr class="HeaderRow">
                                    <th class="HeaderCellHorizontal">Too deep</th>
                                    <th class="HeaderCellHorizontal">Complexity</th>
                                    <th class="HeaderCellHorizontal">Max. depth</th>
                                    <th class="HeaderCellHorizontal">Statements</th>
                                    <th class="HeaderCellHorizontal">Calls</th>
                                </tr>
                                <xsl:apply-templates select="method_metrics/method[maximum_depth &gt; $MaxDepth]">
                                    <xsl:sort select="maximum_depth" data-type="number" order="descending"/>
                                </xsl:apply-templates>
                            </table>
                        </div>
                    </xsl:when>
                </xsl:choose>

                <xsl:choose>
                    <xsl:when test="count(method_metrics/method) &gt; 0">
                        <div name="HideDetails" style="display: ''">
                            <div name="ShowAllMethods" style="display: 'none'">
                                All methods:
                                <table class="MeasurementsTable">
                                    <tr class="HeaderRow">
                                        <th class="HeaderCellHorizontal">All methods (ordered by complexity)</th>
                                        <th class="HeaderCellHorizontal">Complexity</th>
                                        <th class="HeaderCellHorizontal">Max. depth</th>
                                        <th class="HeaderCellHorizontal">Statements</th>
                                        <th class="HeaderCellHorizontal">Calls</th>
                                    </tr>
                                    <xsl:apply-templates select="method_metrics/method">
                                        <xsl:sort select="complexity" data-type="number" order="descending"/>
                                    </xsl:apply-templates>
                                </table>
                            </div>
                        </div>
                    </xsl:when>
                </xsl:choose>

            </td>
        </tr>
    </xsl:template>

    <xsl:template match="method">
        <tr class="DataRow">
            <td class="NameDataCell">
                <xsl:value-of select="@name"/>
            </td>
            <td class="DataCell">
                <xsl:value-of select="complexity"/>
            </td>
            <td class="DataCell">
                <xsl:value-of select="maximum_depth"/>
            </td>
            <td class="DataCell">
                <xsl:value-of select="statements"/>
            </td>
            <td class="DataCell">
                <xsl:value-of select="calls"/>
            </td>
        </tr>
    </xsl:template>

</xsl:stylesheet>
