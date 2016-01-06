<%@ Page Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" Inherits="VPB_PROMOTION.UMS.UMS_roleinfo" Codebehind="roleinfo.aspx.cs" %>
<asp:Content runat="server" ID="NewUser" ContentPlaceHolderID="FormContent">
    <table cellpadding="5" cellspacing="0">
        <!--thong tin chi tiet-->
        <tr class="lblCaption">
            <td colspan="4">Thông tin chi tiết</td>
        </tr>
        <tr class="lblNormal">
            <td>Tên đầy đủ:</td>
            <td><asp:TextBox runat="server" ID="FULLNAME" SkinID="TextBox"></asp:TextBox></td>
            <td>Ghi chú:</td>
            <td><asp:TextBox runat="server" ID="DESCRIPTION" SkinID="TextBox"></asp:TextBox></td>
        </tr>
        
        <!--trang thai-->
        <tr class="lblCaption">
            <td colspan="4">Trạng thái</td>
        </tr>
        <tr class="lblNormal">
            <td></td>
            <td><asp:CheckBox runat="server" ID="IsExpired" Checked/>Kích hoạt</td>
            <td></td>
            <td></td>
        </tr>
    </table>
</asp:Content>
