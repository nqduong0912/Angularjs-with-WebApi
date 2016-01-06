<%@ Page Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" Inherits="VPB_PROMOTION.UMS.UMS_Profile" Title="Untitled Page" Codebehind="Profile.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" Runat="Server">
    <script type="text/javascript">
        var winname;
        function do_changepwd()
        {
            var url = "ChangePassword.aspx";
            if((!winname)||(winname.closed))
                winname=window.open(url,'info','height=450px,width=800px,top=0,left=0,resizable=yes,status=no');
            else
                winname.focus();
        }
        function do_changeinfo()
        {
            var url = "userInfo.aspx?id=" + "<%=_objUserContext.UserID%>";
            if((!winname)||(winname.closed))
                winname=window.open(url,'info','height=450px,width=800px,top=0,left=0,resizable=yes,status=no');
            else
                winname.focus();
        }
    </script>
    <table>
        <tr>
            <td align="right"><img src="../Images/UserInfo.gif"></td>
            <td align="left"><asp:HyperLink runat="server" ID="changeInfo" CssClass="lblNormal" style="cursor:hand">Thay đổi thông tin cá nhân</asp:HyperLink></td>
        </tr>
        
        <tr>
            <td align="right"><img src="../Images/ChangePwd.gif"></td>
            <td align="left"><asp:HyperLink runat="server" ID="changePwd" CssClass="lblNormal" style="cursor:hand">Thay đổi mật khẩu</asp:HyperLink></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ButtonExtend" Runat="Server">
</asp:Content>

