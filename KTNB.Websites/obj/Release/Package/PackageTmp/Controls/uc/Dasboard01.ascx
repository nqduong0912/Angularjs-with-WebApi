<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Dasboard01.ascx.cs" Inherits="VPB_CE.Controls.uc.Dasboard01" %>
<script src="../../Javascript/jquery-1.4.2.min.js"></script>
<script src="../../Javascript/getquerystring.js"></script>
<style type="text/css">
    .style1
    {
        width: 98px;
    }
</style>
<table width="100%">
    <tr>
        <td class="style1">Tài khoản</td>
        <td>
            <asp:TextBox ID="TextBox1" runat="server" SkinID="TextBox"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="style1">CIF </td>
        <td>
            <asp:TextBox ID="TextBox2" runat="server" SkinID="TextBox"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="style1">CMTND</td>
        <td>
            <asp:TextBox ID="TextBox3" runat="server" SkinID="TextBox"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="style1">Số tel</td>
        <td>
            <asp:TextBox ID="TextBox4" runat="server" SkinID="TextBox"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="style1">Địa chỉ cư trú</td>
        <td>
            <asp:TextBox ID="TextBox5" runat="server" SkinID="TextBox"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="style1">&nbsp;</td>
        <td>
            <asp:Button ID="Button1" runat="server" Text="Submit" />
        </td>
    </tr>
</table>
<script type="text/javascript">
    function submitform() {
        if (!confirm("Are you sure to submit ?"))
            return false;
        var tk = $("#ctl00_FormContent_WebPartManager1_gwplblDashboard01_ucDashboard_TextBox1").val();
        var url = "DocumentOfGroup.aspx";
        var query = "acc=" + tk;
        query += "&act=check";
        $.ajax({
            type: "POST",
            url: url,
            data: query,
            success: function(data) {
                alert(data);
            }
        });
    }
</script>