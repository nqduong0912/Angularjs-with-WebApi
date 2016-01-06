<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="NghiepVuViPham_Input.aspx.cs" Inherits="VPB_KTNB.Modules.Lookup.DanhMuc.ViPham.NghiepVuViPham_Input" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <div class="form-horizontal">
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Tên<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_21D3AA58_E232_4906_AE6E_CE516D908910" runat="server" CssClass="form-control" ></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Diễn giải</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_569D7AEC_742E_4918_B1F6_282FC393DF93" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
    </div>
<%--    <table width="100%">
        <tr>
            <td style="width: 222px">Tên</td>
            <td>
                <asp:TextBox ID="ID8_21D3AA58_E232_4906_AE6E_CE516D908910" runat="server"
                    SkinID="TextBoxRequired"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Diễn giải</td>
            <td>
                <asp:TextBox ID="ID8_569D7AEC_742E_4918_B1F6_282FC393DF93" runat="server"
                    SkinID="TextBox"></asp:TextBox>
            </td>
        </tr>

    </table>--%>
    <script type="text/javascript">
        var _documentid;
        /*********************************************************/
        $(document).ready(function () {
            //do smt here
        });
        /*********************************************************/
        function preparesavedoc(documentID, doctypeID) {
            var ten = GetSvrCtlValue("ID8_21D3AA58_E232_4906_AE6E_CE516D908910");
            var diengiai = GetSvrCtlValue("ID8_569D7AEC_742E_4918_B1F6_282FC393DF93");
            var url = "NghiepVuViPham_Input.aspx";
            var query = "act=checkvalue";
            //var valueactive = $("#ctl00_FormContent_DOCSTATUS").val();
            query += "&p=21D3AA58-E232-4906-AE6E-CE516D908910";
            query += "&v=" + ten;
            query += "&diengiai=" + diengiai;
            //query += "&valueactive=" + valueactive;

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
                        alert("Dữ liệu nhập bị trùng lặp. Kiểm tra lại.");
                }
            });
        }

        function prepareupdatedoc(documentID) {
            var ten = GetSvrCtlValue("ID8_21D3AA58_E232_4906_AE6E_CE516D908910");
            var diengiai = GetSvrCtlValue("ID8_569D7AEC_742E_4918_B1F6_282FC393DF93");
            var url = "NghiepVuViPham_Input.aspx";
            var query = "act=checkvalueupdate";
            //var valueactive = $("#ctl00_FormContent_DOCSTATUS").val();
            query += "&p=21D3AA58-E232-4906-AE6E-CE516D908910";
            query += "&v=" + ten;
            query += "&diengiai=" + diengiai;
            //query += "&valueactive=" + valueactive;
            query += "&doc=" + documentID;
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
            window.location.href = 'NghiepVuViPham.aspx';
        }
        function savedoc_error() {
            alert(MSG_ADD_ER);
        }
        function update_success() {
            alert(MSG_EDIT_OK);
            window.location.href = 'NghiepVuViPham.aspx';
        }
        function update_error() {
            alert(MSG_EDIT_ER);
        }
        function delete_success() {
            alert(MSG_DEL_OK);
            window.location.href = 'NghiepVuViPham.aspx';
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
