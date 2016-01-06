<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" Inherits="VPB_PROMOTION.CFG.Modules_CFG_appcustom" Codebehind="appcustom.aspx.cs" %>
<asp:Content ID="frmContent" ContentPlaceHolderID="FormContent" Runat="Server">
<asp:ScriptManager runat="server" ID="sc1"></asp:ScriptManager>
<asp:UpdatePanel runat="server" ID="up" UpdateMode="Always">
    <ContentTemplate>
<table class="table" style="width:100%">
    <tr>
        <td class="lblNormal" style="width: 171px">Ứng dụng</td>
        <td>
            <asp:Label runat="server" ID="lblComponentName" CssClass="lblCaption"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="lblNormal" colspan="2">
            <hr align="left" size="1" />
        </td>
    </tr>
    <tr>
        <td class="lblNormal" style="width: 171px">(<font color='red'>*</font>) Với vai trò...</td>
        <td>
            <asp:DropDownList ID="cboRole" runat="server" SkinID="DropDownList" AutoPostBack="true">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="lblNormal" colspan="2">
            <hr align="left" size="1" />
        </td>
    </tr>
    <tr>
        <td class="lblNormal" style="width: 171px">...Chọn CN/PGD để loại trừ...</td>
        <td>
            <asp:DropDownList ID="cboGroupUnMapped" runat="server" SkinID="DropDownList">
            </asp:DropDownList>
            <asp:Button ID="btnLoaiTru" runat="server" SkinID="SaveButton" Text="Button" OnClick="LoaiTruNhom" />
        </td>
    </tr>
    <tr>
        <td class="lblNormal" colspan="2">
            <hr align="left" size="1" />
        </td>
    </tr>
    <tr>
        <td class="lblNormal" style="width: 171px">...Hoặc chọn CN/PGD để thêm vào</td>
        <td>
            <asp:DropDownList ID="cboGroupMapped" runat="server" SkinID="DropDownList">
            </asp:DropDownList>
            <asp:Button ID="btnThemVao" runat="server" SkinID="SaveButton" Text="Button" OnClick="ThemNhom" />
        </td>
    </tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
<asp:Content ID="frmB1" ContentPlaceHolderID="ButtonExtendBefore" Runat="Server">
</asp:Content>
<asp:Content ID="frmB2" ContentPlaceHolderID="ButtonExtend" Runat="Server">
</asp:Content>

