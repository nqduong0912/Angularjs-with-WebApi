<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="SendBroadCastMessage.aspx.cs" Inherits="VPB_PROMOTION.Modules.UMS.SendBroadCastMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <table style="width:100%">
    <tr class="GridHeader">
        <td colspan="2">Soạn Thông báo</td>
    </tr>
    <tr>
        <td style="width: 114px" valign="top">Nội dung</td>
        <td valign="top">
            <asp:TextBox ID="txtMessage" runat="server" Rows="5" Width="90%" 
                SkinID="TextBox" TextMode="MultiLine"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="width: 114px">Nhóm đối tượng&nbsp;</td>
        <td>
            <asp:DropDownList ID="DropDownList1" runat="server" SkinID="DropDownList">
                <asp:ListItem Text="Role" Value="Role"></asp:ListItem>
                <asp:ListItem Text="Group" Value="Group"></asp:ListItem>
                <asp:ListItem Text="Private" Value="Private"></asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="btnRef" runat="server" CssClass="Button" Text="..." />
        </td>
    </tr>
    <tr>
        <td style="width: 114px">&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ButtonExtendBefore" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ButtonExtend" runat="server">
</asp:Content>
