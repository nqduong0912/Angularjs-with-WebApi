<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="BoTieuChiDanhGiaCLKTV_Load.aspx.cs" Inherits="VPB_TDHS.Modules.Lookup.BoTieuChiDanhGiaCLKTV_Load" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <div class="form-horizontal">
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Phân loại bộ tiêu chí<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:DropDownList ID="ID8_FB2F72B5_27DC_458B_BB16_D7DC09E4C7B2" runat="server" CssClass="form-control" Width="300px">
                    </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Tên<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_02CCDE67_5D27_4DF2_BA7C_FD8B7DDE2D74" runat="server" CssClass="form-control" ></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Diễn giải</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_323CF8C8_06B8_4F57_8DC4_01C70E704051" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Trạng thái</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="DOCSTATUS" runat="server" CssClass="form-control" Width="100px">
                    </asp:DropDownList>
            </div>
        </div>  
    </div>
    <script type="text/javascript">
        var _documentid;
        /*********************************************************/
        $(document).ready(function () {
            //do smt here
        });
        /*********************************************************/
        function preparesavedoc(documentID, doctypeID) {
            var ten = GetSvrCtlValue("ID8_02CCDE67_5D27_4DF2_BA7C_FD8B7DDE2D74");
            var url = "BoTieuChiDanhGiaCLKTV_Load.aspx";
            var query = "act=checkvalue";
            query += "&p=02CCDE67-5D27-4DF2-BA7C-FD8B7DDE2D74";
            query += "&v=" + ten;
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
            var ten = GetSvrCtlValue("ID8_02CCDE67_5D27_4DF2_BA7C_FD8B7DDE2D74");
            var url = "BoTieuChiDanhGiaCLKTV_Load.aspx";
            var query = "act=checkvalueupdate";
            query += "&p=02CCDE67-5D27-4DF2-BA7C-FD8B7DDE2D74";
            query += "&v=" + ten;
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
                        alert(MSG_DATA_ESXIT);
                }
            });
        }

        function adddoc(documentID, doctypeID) {
            var ten = GetSvrCtlValue("ID8_02CCDE67_5D27_4DF2_BA7C_FD8B7DDE2D74");
            var tenbotieuchi = GetSvrCtlValue("ID8_02CCDE67_5D27_4DF2_BA7C_FD8B7DDE2D74");
            var loaitieuchi = GetSvrCtlValue("ID8_FB2F72B5_27DC_458B_BB16_D7DC09E4C7B2");
            var valueactive = $("#ctl00_FormContent_DOCSTATUS").val();

            var url = "BoTieuChiDanhGiaCLKTV_Load.aspx";
            var query = "act=adddoc";
            query += "&p=02CCDE67-5D27-4DF2-BA7C-FD8B7DDE2D74";
            query += "&v=" + ten;
            query += "&tenbotieuchi=" + tenbotieuchi;
            query += "&loaitieuchi=" + loaitieuchi;
            query += "&valueactive=" + valueactive;
            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (data) {
                    FinishProcessingForm();
                    if (data == "0")
                        savedocument(documentID, doctypeID, savedoc_success, savedoc_error);
                    //alert("OK");
                    if (data == "1")
                        alert("Dữ liệu nhập bị trùng lặp. Kiểm tra lại.");
                    if (data == "2")
                        alert("Tại 1 thời điểm, hệ thống chỉ cho phép tồn tại 1 bộ tiêu chí được active ứng với 1 loại tiêu chí.");
                }
            });
        }

        function updatedoc(documentID) {
            var ten = GetSvrCtlValue("ID8_02CCDE67_5D27_4DF2_BA7C_FD8B7DDE2D74");
            var tenbotieuchi = GetSvrCtlValue("ID8_02CCDE67_5D27_4DF2_BA7C_FD8B7DDE2D74");
            var loaitieuchi = GetSvrCtlValue("ID8_FB2F72B5_27DC_458B_BB16_D7DC09E4C7B2");
            var valueactive = $("#ctl00_FormContent_DOCSTATUS").val();

            var url = "BoTieuChiDanhGiaCLKTV_Load.aspx";
            var query = "act=updatedoc";
            query += "&p=02CCDE67-5D27-4DF2-BA7C-FD8B7DDE2D74";
            query += "&v=" + ten;
            query += "&doc=" + documentID;
            query += "&tenbotieuchi=" + tenbotieuchi;
            query += "&loaitieuchi=" + loaitieuchi;
            query += "&valueactive=" + valueactive;
            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (data) {
                    FinishProcessingForm();
                    if (data == "0")
                        //alert("OK");
                        updatedocument(documentID, update_success, update_error);
                    if (data == "1")
                        alert("Dữ liệu nhập bị trùng lặp. Kiểm tra lại.");
                    if (data == "2")
                        alert("Tại 1 thời điểm, hệ thống chỉ cho phép tồn tại 1 bộ tiêu chí được active ứng với 1 loại tiêu chí.");
                }
            });
        }

        function savedoc_success() {
            alert(MSG_ADD_OK);
            window.location.href = "BoTieuChiDanhGiaCLKTV.aspx";
        }
        function savedoc_error() {
            alert(MSG_ADD_ER);
        }
        function update_success() {
            alert(MSG_EDIT_OK);
            window.location.href = "BoTieuChiDanhGiaCLKTV.aspx";
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

