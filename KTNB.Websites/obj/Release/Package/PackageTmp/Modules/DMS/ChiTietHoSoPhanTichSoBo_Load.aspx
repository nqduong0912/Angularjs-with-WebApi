<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="ChiTietHoSoPhanTichSoBo_Load.aspx.cs" Inherits="VPB_TDHS.Modules.DMS.ChiTietHoSoPhanTichSoBo_Load" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <table width="100%">
    <tr>
        <td style="width: 222px">Mảng nghiệp vụ</td>
        <td>
            <asp:DropDownList ID="ID8_AC4286FB_5B38_47C4_93F0_E075F171D8FC" runat="server" 
                SkinID="TextBoxRequired"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td style="width: 222px">Vấn đề quan tâm</td>
        <td>
            <asp:TextBox ID="ID8_CBA39743_AA0E_4965_9201_BE3BB5CF710A" runat="server" 
                SkinID="TextBox"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="width: 222px">Mức độ rủi ro</td>
        <td>
            <asp:DropDownList ID="ID8_05B1B244_C5E3_4199_B523_96A3E131D83D" runat="server" 
                SkinID="TextBox"></asp:DropDownList>
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
        var vandequantam = GetSvrCtlValue("ID8_CBA39743_AA0E_4965_9201_BE3BB5CF710A");
        var url = "ChiTietHoSoPhanTichSoBo_Load.aspx";
        var query = "act=checkvalue";
        query += "&p=CBA39743-AA0E-4965-9201-BE3BB5CF710A";
        query += "&v=" + vandequantam;
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
    function prepareupdatedoc(documentID) {
        var vandequantam = GetSvrCtlValue("ID8_CBA39743_AA0E_4965_9201_BE3BB5CF710A");
        var url = "ChiTietHoSoPhanTichSoBo_Load.aspx.aspx";
        var query = "act=checkvalue";
        query += "&p=CBA39743-AA0E-4965-9201-BE3BB5CF710A";
        query += "&v=" + vandequantam;
        StartProcessingForm("");
        $.ajax({
            type: "POST",
            url: url,
            data: query,
            success: function (data) {
                FinishProcessingForm();
                if (data == "0")
                    updatedocument(documentID, update_success, update_error);
                else
                    alert(MSG_DATA_ESXIT);
            }
        });
    }
    function savedoc_success() {
        alert(MSG_ADD_OK);
//        window.location.href = 'LoaiDoiTuongKiemToan.aspx';
    }
    function savedoc_error() {
        alert(MSG_ADD_ER);
    }
    function update_success() {
        alert(MSG_EDIT_OK);
//        window.location.href = 'LoaiDoiTuongKiemToan.aspx';
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

