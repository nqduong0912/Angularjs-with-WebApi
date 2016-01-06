<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" Inherits="VPB_PROMOTION.CFG.Modules_CFG_cachefolder" Codebehind="cachefolder.aspx.cs" %>
<%@ OutputCache NoStore="true" Duration="1" VaryByParam="None" %>
<asp:Content ID="frmContent" ContentPlaceHolderID="FormContent" Runat="Server">
<table style="width:75%; height:60%; background:black; color:White">
    <tr >
        <td style="color:White; width: 112px;">Download folder</td>
        <td style="color:White; width: 203px;">
            <asp:Label runat="server" ID="lblFilesDown" CssClass="lblCaption" ForeColor="Yellow"></asp:Label>
        </td>
        <td style="color:White">
            <asp:Button ID="btnResetDownloadFolder" runat="server" SkinID="DeleteButton" 
                Text="Reset" OnClick="ResetDownload" />
        </td>
    </tr>
    <tr >
        <td style="color:White" align="left" colspan="3" valign="top">
            <hr size="1" />
        </td>
    </tr>
    <tr >
        <td style="color:White; width: 112px;">Upload folder</td>
        <td style="color:White; width: 203px;">
            <asp:Label runat="server" ID="lblFilesUp" CssClass="lblCaption" ForeColor="Yellow" ></asp:Label>
        </td>
        <td style="color:White">
            <asp:Button ID="btnResetUploadFolder" runat="server" SkinID="DeleteButton" 
                Text="Reset" OnClick="ResetUpload" />
        </td>
    </tr>
    <tr >
        <td style="color:White" align="left" colspan="3" valign="top">
            <hr size="1" />
        </td>
    </tr>
    <tr >
        <td style="color:White; width: 112px;">&nbsp;</td>
        <td style="color:White; width: 203px;">&nbsp;</td>
        <td style="color:White">&nbsp;</td>
    </tr>
</table>
<script type="text/javascript">
    function resetdownloadfolder() {
        if (!confirm("Bạn đồng ý reset ?"))
            return false;
        return true;
    }
    function resetuploadfolder() {
        if (!confirm("Bạn đồng ý reset ?"))
            return false;
        return true;
    }
</script>
</asp:Content>
<asp:Content ID="frmB1" ContentPlaceHolderID="ButtonExtendBefore" Runat="Server">
</asp:Content>
<asp:Content ID="frmB2" ContentPlaceHolderID="ButtonExtend" Runat="Server">
</asp:Content>

