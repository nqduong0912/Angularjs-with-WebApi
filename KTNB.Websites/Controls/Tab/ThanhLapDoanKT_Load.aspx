<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ThanhLapDoanKT_Load.aspx.cs" Inherits="VPB_TDHS.Controls.Tab.ThanhLapDoanKT_Load" %>
<%@ OutputCache Duration="1" NoStore="true" VaryByParam="None" %>
<!DOCTYPE html>

<html lang="vi">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
    <title>VPBANK</title>
    <link rel="stylesheet" href="tab.css" TYPE="text/css" MEDIA="screen" />
    <script type="text/javascript">

        /* Optional: Temporarily hide the "tabber" class so it does not "flash"
        on the page as plain HTML. After tabber runs, the class is changed
        to "tabberlive" and it will appear.
        */
        document.write('<style type="text/css">.tabber{display:none;}<\/style>');

        var tabberOptions = {

            /* Optional: instead of letting tabber run during the onload event,
            we'll start it up manually. This can be useful because the onload
            even runs after all the images have finished loading, and we can
            run tabber at the bottom of our page to start it up faster. See the
            bottom of this page for more info. Note: this variable must be set
            BEFORE you include tabber.js.
            */
            'manualStartup': true,

            /* Optional: code to run after each tabber object has initialized */

            //  'onLoad': function(argsObj) {
            //    /* Display an alert only after tab2 */
            //    if (argsObj.tabber.id == 'tab2') {
            //     alert('Finished loading tab2!');
            //    }
            //  },

            /* Optional: code to run when the user clicks a tab. If this
            function returns boolean false then the tab will not be changed
            (the click is canceled). If you do not return a value or return
            something that is not boolean false, */

            //  'onClick': function(argsObj) {

            //    var t = argsObj.tabber; /* Tabber object */
            //    var id = t.id; /* ID of the main tabber DIV */
            //    var i = argsObj.index; /* Which tab was clicked (0 is the first tab) */
            //    var e = argsObj.event; /* Event object */

            //   if (id == 'tab2') {
            //     return confirm('Swtich to '+t.tabs[i].headingText+'?\nEvent type: '+e.type);
            //    }
            //  },

            /* Optional: set an ID for each tab navigation link */
            'addLinkId': true

        };

</script>

    <!-- Load the tabber code -->
    <script type="text/javascript" src="tab.js"></script>
</head>
<body onload="tabinit()">
    <div id="tabloading" style="height:auto;width:auto;position:absolute;top:42%;margin-left:42%">
         <img alt="loading..." id="" src="../../Images/indicator_big.gif" style="width:auto; height:auto" />       
    </div>
    <form id="frmTabs" runat="server" style="display:none">
        <table width="100%">
            <thead align="center">
                <tr id="trNavigation" class="lblTinVan2">
                    <td>
                        <asp:Literal runat="server" ID="litNavigationOnTab"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:ImageButton runat="server" ID="btnRefresh" ImageUrl="~/Images/Set3/18/refresh.png" Visible="false" ToolTip="refresh" />
                    </td>
                </tr>
            </thead>
        </table>
        
        <asp:Literal ID="litTab" runat="server" EnableViewState="false"></asp:Literal>
        <script type="text/javascript" src="../../Javascript/jquery-1.4.2.min.js"></script>
        <script type="text/javascript">
            function tabinit() {
                $("#tabloading").hide();
                $("#frmTabs").show();
            }
        </script>
    </form>
</body>
</html>
