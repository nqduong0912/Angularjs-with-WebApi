<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="KeHoachKiemToanNam_Load.aspx.cs" Inherits="VPB_TDHS.Modules.DMS.KeHoachKiemToanNam_Load" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <table width="100%">
    <tr>
        <td style="width: 222px">Năm</td>
        <td>
            <asp:TextBox ID="ID8_3F979CD0_8B2C_4167_A5B2_1016C85D0978" runat="server" 
                SkinID="TextBoxRequired"></asp:TextBox>
        </td>
    </tr>
    
    <tr>
        <td style="width: 222px">Tên kế hoạch năm</td>
        <td>
        <asp:TextBox ID="ID8_D8A29EE8_E564_4DDC_ACCA_BAE990A5FB53" runat="server" 
                SkinID="TextBox"></asp:TextBox>
        </td>
    </tr>
</table>
<script type="text/javascript">
    var _documentid;
    /*********************************************************/
    $(document).ready(function () {
       
    });
    /*********************************************************/
    function preparesavedoc(documentID, doctypeID) {
        var ten = GetSvrCtlValue("ID8_3F979CD0_8B2C_4167_A5B2_1016C85D0978");
        var url = "KeHoachKiemToanNam_Load.aspx";
        var query = "act=checkvalue";
        query += "&p=3F979CD0-8B2C-4167-A5B2-1016C85D0978";
        query += "&v=" + ten;
        StartProcessingForm("");
        $.ajax({
            type: "POST",
            url: url,
            data: query,
            success: function (data) {
                FinishProcessingForm();
                if (data == "0") {
                    var result = savedocument(documentID, doctypeID, savedoc_success, savedoc_error);
                    //alert(result);
                    if (result == false) {
                        return false;
                    }
                    window.location.href = 'DotKiemToanNam_Load.aspx?doclink=' + documentID;
                }
                else {
                    alert(MSG_DATA_ESXIT);
                }
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
    function delete_success() {
        alert(MSG_DEL_OK);
    }
    function delete_error() {
        alert(MSG_DEL_ER);
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ButtonExtendBefore" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ButtonExtend" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FormFooter" runat="server">
</asp:Content>