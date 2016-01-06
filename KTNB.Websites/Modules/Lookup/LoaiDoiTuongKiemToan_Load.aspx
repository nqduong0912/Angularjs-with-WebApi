<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true"
    CodeBehind="LoaiDoiTuongKiemToan_Load.aspx.cs" Inherits="VPB_TDHS.Modules.Lookup.LoaiDoiTuongKiemToan_Load" %>

<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <div class="form-horizontal">
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Tên<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_410D55A8_48D8_4EED_894D_836E24E1E36D" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Diễn giải</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_563054B4_4305_4D2D_AE75_8B54F80F56EB" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control"></asp:TextBox>
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
            var ten = GetSvrCtlValue("ID8_410D55A8_48D8_4EED_894D_836E24E1E36D");
            if (ten.trim().length == 0)
            {
                alert("Nhập tên loại ĐTKT.");
                return;
            }
            var parentgroup = $("#ctl00_FormContent_cboBranchCodes").val();
            var url = "LoaiDoiTuongKiemToan_Load.aspx";
            var query = "act=checkvalue";
            query += "&p=410D55A8-48D8-4EED-894D-836E24E1E36D";
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
            var ten = GetSvrCtlValue("ID8_410D55A8_48D8_4EED_894D_836E24E1E36D");
            if (ten.trim().length == 0) {
                alert("Nhập tên loại ĐTKT.");
                return;
            }
            var url = "LoaiDoiTuongKiemToan_Load.aspx";
            var query = "act=checkvalueupdate";
            query += "&p=410D55A8-48D8-4EED-894D-836E24E1E36D";
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

        function savedoc_success() {
            alert(MSG_ADD_OK);
            window.location.href = 'LoaiDoiTuongKiemToan.aspx?a=dbb1170f-fb15-4585-9c21-e21c32d4de86';
        }
        function savedoc_error() {
            alert(MSG_ADD_ER);
        }
        function update_success() {
            alert(MSG_EDIT_OK);
            window.location.href = 'LoaiDoiTuongKiemToan.aspx?a=dbb1170f-fb15-4585-9c21-e21c32d4de86';
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
