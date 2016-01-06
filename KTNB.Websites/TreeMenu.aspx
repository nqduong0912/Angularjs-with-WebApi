<%@ Page Language="C#" AutoEventWireup="true" Inherits="TreeMenu" Codebehind="TreeMenu.aspx.cs" %>
<!DOCTYPE html>
<html lang="vi">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
    <title>Untitled Page</title>
    <link href="Controls/Xmenu/Xmenu.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="Javascript/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="Controls/Xmenu/Xmenu.js"></script>
    <script type="text/javascript">
    var itemclick;
    function selectingItem(obj) {
        //itemclick = obj.id;
        $("ul.nav-second-level li a").attr("style", "color:#48b061;");
        $(obj).find("a").attr("style", "color:#E61C29;");
        //$("#" + obj.id).attr("style", "color:Blue;");
    }
    </script>
</head>

<body style="background-color: white">
    <img id="loading" src="Images/indicator_big.gif" alt="loading!" />
    <script type="text/javascript">
        $(document).ready(function() {
            $("#loading").hide();
        });
    ;
    </script>
    <form runat="server" id="main_menu">
        <div class="col-md-3 col-sm-12 sidebar">
            <asp:Literal runat="server" ID="boxmenu"></asp:Literal>
        </div>        
    </form>
</body>
</html>
