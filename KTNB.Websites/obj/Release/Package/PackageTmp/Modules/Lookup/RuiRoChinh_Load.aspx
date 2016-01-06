<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="RuiRoChinh_Load.aspx.cs" Inherits="VPB_TDHS.Modules.Lookup.RuiRoChinh_Load" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <table width="100%">
    <tr>
        <td style="width: 222px">Tên</td>
        <td>
            <asp:TextBox ID="ID8_8707977E_31B9_4EA4_B016_4D110C0D9F64" runat="server" 
                SkinID="TextBox"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="width: 222px">Diễn giải</td>
        <td>
            <asp:TextBox ID="ID8_6B55AA99_3C65_45BF_95C1_5121CA7A3614" runat="server" 
                SkinID="TextBox"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="width: 222px">Trạng thái</td>
        <td>
            <asp:DropDownList ID="DOCSTATUS" runat="server" 
                SkinID="DropDownList">
            </asp:DropDownList>
        </td>
    </tr>
</table>
<script type="text/javascript">
    var _documentid;
    /*********************************************************/
    $(document).ready(function () {
        //do smt here
    });
    /*********************************************************/
    function preparesavedoc(documentID, doctypeID) {
        var tenruiro = GetSvrCtlValue("ID8_8707977E_31B9_4EA4_B016_4D110C0D9F64");
        var url = "RuiRoChinh_Load.aspx";
        var query = "act=checkvalue";
        query += "&p=8707977E-31B9-4EA4-B016-4D110C0D9F64";
        query += "&v=" + tenruiro;
        StartProcessingForm("");
        $.ajax({
            type: "POST",
            url: url,
            data: query,
            success: function (data) {
                FinishProcessingForm();
                if (data == "0")
                    savedocument(documentID, doctypeID, savedoc_success, savedoc_error);
                else
                    alert("Dữ liệu nhập bị trùng lặp. Kiểm tra lại.");
            }
        });
    }
    function savedoc_success() {
        alert(MSG_ADD_OK);
    }
    function savedoc_error() {
        alert(MSG_ADD_ER);
    }
    function update_success() {
        alert(MSG_EDIT_OK);
    }
    function update_error() {
        alert(MSG_EDIT_ER);
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ButtonExtendBefore" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ButtonExtend" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FormFooter" runat="server">
</asp:Content>
