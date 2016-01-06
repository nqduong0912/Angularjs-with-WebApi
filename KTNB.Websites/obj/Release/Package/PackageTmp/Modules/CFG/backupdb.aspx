<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" Inherits="VPB_PROMOTION.CFG.Modules_CFG_backupdb" Codebehind="backupdb.aspx.cs" %>
<%@ OutputCache Duration="1" Location="Client" VaryByParam="None"%>
<asp:Content ID="frmContent" ContentPlaceHolderID="FormContent" Runat="Server">
<table style="width:75%; height:60%; background:black;">
    <tr>
        <td class="lblNormal" style="color:Yellow; font-weight:bold; width:2%" 
            valign="top" align="left" rowspan="2">
            <table style="width:100%; background:black; color:White">
                <tr>
                    <td valign="top">
                        &nbsp;</td>
                </tr>
            </table>
        </td>
        <td class="lblNormal" style="color:Yellow; font-weight:bold; width:70%" valign="top" align="left">Tiến trình sao lưu </td>
        <td class="lblNormal" style="color:White; width:70%" valign="top" align="left">&nbsp;</td>
    </tr>
    <tr>
        <!--Danh muc sao luu-->
        
        <!--Tien trinh sao luu-->
        <td class="lblNormal" style="color:White; width:70%" valign="top" align="left">
            <table style="width:100%; background:black; color:White">
                <tr>
                    <td align="left" class="lblNormal" style="color:White" valign="top">File
                        <asp:TextBox ID="txtFile" runat="server" SkinID="TextBox" Width="80%" ReadOnly="true" style="background-color:Black; color:White"></asp:TextBox>
                    </td>
                    
                </tr>
                <tr>
                    <td align="left" class="lblNormal" style="color:White">
                        <asp:Label runat="server" ID="lblStatus" CssClass="lblCaption" ForeColor="Yellow"></asp:Label>
                    &nbsp;</td>
                </tr>
                <tr>
                    <td align="left" class="lblNormal" style="color:White" valign="bottom">
                        <asp:Button ID="btnBackUp" runat="server" SkinID="Resolve" Text="Sao lưu" OnClick="BackUp" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<script type="text/javascript">
    function backup() {
        if ($("#ctl00_FormContent_txtFile").val() == "") {
            alert("Bạn chưa đặt tên file sao lưu !");
            $("#ctl00_FormContent_txtFile").focus();
            return false;
        }
        
        if (!window.confirm("Đồng ý sao lưu ?"))
            return false;

        var filename=GetValue("ctl00_FormContent_txtFile");
        var url = "backup.do";
        var query="todisk=" + filename;
        
        StartProcessingForm("");
        DisableControl("ctl00_FormContent_btnBackUp");
        $.ajax({
            type: "POST",
            url: url,
            data: query,
            success: function(msg){
                FinishProcessingForm();
                if(msg=="::")
                    alert('Backup successfully.');
                EnableControl("ctl00_FormContent_btnBackUp");
            }
        });
        
        
        
        return false;
    }
</script>
</asp:Content>
<asp:Content ID="frmB1" ContentPlaceHolderID="ButtonExtendBefore" Runat="Server">
</asp:Content>
<asp:Content ID="frmB2" ContentPlaceHolderID="ButtonExtend" Runat="Server">
</asp:Content>

