<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="PhanLoaiBoTieuChi_Input.aspx.cs" Inherits="VPB_KTNB.Modules.Lookup.DanhMuc.PhanLoaiBoTieuChi_Input" %>
<%@ Register Assembly="C1.Web.C1Input.2" Namespace="C1.Web.C1Input" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <table width="100%">
    <tr>
        <td style="width: 222px">Năm</td>
        <td>
            <asp:TextBox ID="ID8_DDFF81BA_1A46_426F_85D1_69C68C7F3BA3" runat="server" Enabled="false"></asp:TextBox>
        </td>
    </tr>

    <tr>
        <td style="width: 222px">
            Loại bộ tiêu chí
        </td>
        <td>
            <asp:DropDownList ID="ID8_51CB5CBB_9D95_423C_BF08_0FA673E6CD34" runat="server" SkinID="DropDownListRequired">
            </asp:DropDownList>
        </td>
    </tr>
    <tr >
        <td style="width: 222px" id ="statusLine">Trạng thái</td>
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
        var url = "PhanLoaiBoTieuChi_Input.aspx";
        var query = "act=checkvalue";
        var name = $("#ctl00_FormContent_ID8_51CB5CBB_9D95_423C_BF08_0FA673E6CD34").val();
        var valueactive = "2";
        var countActive = '<%=_count%>'; 
        if (countActive == 0) valueactive = "4";
        query += "&valueactive=" + valueactive;
        query += "&p=" + name;
        query += "&y=" + $("#ctl00_FormContent_ID8_DDFF81BA_1A46_426F_85D1_69C68C7F3BA3").val();
        StartProcessingForm("");
        $.ajax({
            type: "POST",
            url: url,
            data: query,
            success: function (data) {
                FinishProcessingForm();
                if (data == "0") {
                    savedocument(documentID, doctypeID, savedoc_success, savedoc_error);
                }
                else
                    alert("Loại bộ tiêu chí " + name+" đã tồn tại.");
            }
        });
    }

    function prepareupdatedoc(documentID) {
        var url = "PhanLoaiBoTieuChi_Input.aspx";
        var query = "act=checkvalueupdate";
        var valueactive = $("#ctl00_FormContent_DOCSTATUS").val();
        query += "&valueactive=" + valueactive;
        query += "&doc=" + documentID;
        query += "&y=" + $("#ctl00_FormContent_ID8_DDFF81BA_1A46_426F_85D1_69C68C7F3BA3").val();
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
                    alert("Dữ liệu nhập bị trùng lặp. Kiểm tra lại.");
            }
        });
    }

    function savedoc_success() {
        alert(MSG_ADD_OK);
        window.location.href = 'PhanLoaiBoTieuChi.aspx?y=' + GetSvrCtlValue("ID8_DDFF81BA_1A46_426F_85D1_69C68C7F3BA3");
    }
    function savedoc_error() {
        alert(MSG_ADD_ER);
    }
    function update_success() {
        alert(MSG_EDIT_OK);
        window.location.href = 'PhanLoaiBoTieuChi.aspx?y=' + GetSvrCtlValue("ID8_DDFF81BA_1A46_426F_85D1_69C68C7F3BA3");
    }
    function update_error() {
        alert(MSG_EDIT_ER);
    }
    function delete_success() {
        alert(MSG_DEL_OK);
        window.location.href = 'PhanLoaiBoTieuChi.aspx?y=' + GetSvrCtlValue("ID8_DDFF81BA_1A46_426F_85D1_69C68C7F3BA3");
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

