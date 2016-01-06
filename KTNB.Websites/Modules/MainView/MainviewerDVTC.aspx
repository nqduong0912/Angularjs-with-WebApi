<%@ Page Language="C#" AutoEventWireup="true" Inherits="VPB_PROMOTION.MainviewerDVTC.MainviewerDVTC_Mainviewer" Codebehind="MainviewerDVTC.aspx.cs" %>
<%@ OutputCache Duration="1" Location="None" VaryByParam="None"%>
<!DOCTYPE html>
<html lang="vi">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
    <title>VPBANK</title>
    <script type="text/javascript">
        <!--
	    // -----------------------------------------------------------
	    // Client-side BrowserData constructor
	    // Populated using data from server-side oBD object to avoid redundancy
        // -------------------------------------------f----------------
        window.status = "";
	    function BrowserData()
	    {
		    this.userAgent = "Mozilla/4.0 (compatible; MSIE 5.01; Windows NT 5.0)";
		    this.browser = "MSIE";
		    this.majorVer = 5;
		    this.minorVer = "01";
		    this.betaVer = "0";
		    this.platform = "NT";
		    this.doesDHTML = true;
		    this.doesActiveX = true;
		    this.doesUserData = true;

	    }
	    var oBD = new BrowserData();

        //-->
    </script>

    <script type="text/javascript">
        <!--
        // -------------------------------------------------------
        // These two variables are also used by PutUserData() and GetUserData()
        // -------------------------------------------------------

        var iPaneWidth = 250;
        var glossOff = false;

        var path = top.location.pathname.substring(0,top.location.pathname.lastIndexOf("/") + 1);

        function save()
        {
            if( "object" == typeof( fsTop ) )
            {
                fsTop.setAttribute( "fraPaneBar" , document.frames[ "fraPaneBar" ].location );
                fsTop.setAttribute( "fraTopic" , document.frames[ "fraTopic" ].location.pathname );
            }
        }

        function load()
        {
            if( "object" == typeof( fsTop ) && fsTop.XMLDocument )
            {
                document.frames["fraPaneBar"].location =  fsTop.getAttribute( "fraPaneBar" );
                document.frames["fraToc"].location =  "toc.asp?URL=" + fsTop.getAttribute( "fraTopic" );
                document.frames["fraTopic"].location =  fsTop.getAttribute( "fraTopic" );
            }
        }

        //-->
    </script>
</head>
<form>
<frameset marginwidth="0" marginheight="0" leftmargin="0" topmargin="0" border="0" frameborder="no" scrolling="yes" noresize="true" name="fraMain" id="fraMain">
    <frameset id="fsTop" name="fsTop" style="behavior: url('#default#savefavorite')" marginwidth="0" marginheight="0" leftmargin="0" topmargin="0" framespacing="0" border="1" rows="0,*" onsave="save();" onload="load();">
	    <frame marginwidth="0" marginheight="0" leftmargin="0" topmargin="0" border="0" frameborder="no" scrolling="no" noresize="false" src="" name="fraToolbar" id="fraToolbar" runat="server" />
	    <frameset id="TCfs" marginwidth="0" marginheight="0" leftmargin="0" topmargin="0" framespacing="0" border="1" frameborder="1" cols="270,*">
		    <frame style="border-right: #0E602F 1px solid; border-top: #0E602F 0px solid" marginwidth="0" marginheight="0" leftmargin="0" topmargin="0" border="0" frameborder="0" scrolling="auto" src="" name="fraToc" runat="server" id="fraToc" />
		    <frame style="border-left: #0E602F 0px groove; border-top: #0E602F 0px solid" border="0" frameborder="0" scrolling="yes" src="../../Wellcome.html" runat="server" name="fraTopic" id="fraTopic"  bordercolor="#0E602F" />
	</frameset>
</frameset>
</form>
</html>


