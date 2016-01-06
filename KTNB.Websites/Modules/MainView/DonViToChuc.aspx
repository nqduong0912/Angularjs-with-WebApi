<%@ Page Language="C#" AutoEventWireup="true" Inherits="VPB_PROMOTION.MainView.MainView_DonViToChuc" Codebehind="DonViToChuc.aspx.cs" Buffer="false"%>
<%@ OutputCache Duration="1" NoStore="true" VaryByParam="None"%>
<!DOCTYPE html>

<html lang="vi">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
    <title>Untitled Page</title>
    <script type="text/javascript" src="../../Javascript/jquery-1.4.2.min.js"></script>
</head>
<body onload="folderload()">
    <img alt="" id="loading" src="../../Images/indicator_big.gif" />
    <form id="frmFolderView" runat="server" style="display:none">
        <div>
            <asp:ScriptManager runat="server" ID="scrMgn_DonViToChuc"></asp:ScriptManager>
            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                <ContentTemplate>
                    <asp:TreeView runat="server" ID="treeFolder" ShowLines="true" RootNodeStyle-ImageUrl="~/Images/thumuc.gif" RootNodeStyle-ForeColor="#0E602F">
                        <SelectedNodeStyle BackColor="LightBlue" BorderColor="#0E602F" BorderStyle="Solid" BorderWidth="1" />
                        <LeafNodeStyle ForeColor="Black" />
                    </asp:TreeView>                
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:Literal runat="server" ID="ctxstatus"></asp:Literal>
        <script type="text/javascript">
            //function gopage(url, target) {
            //    window.open(url, target);
            //}
            function folderload() {
                $("#frmFolderView").show();
                $("#loading").hide();
            }
     </script>
    </form>
</body>
</html>
