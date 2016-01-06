<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="ThemMoiLoaiBoTieuChiNam.aspx.cs" Inherits="VPB_KTNB.Modules.Lookup.DanhMuc.TieuChiNam.ThemMoiLoaiBoTieuChiNam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <div class="form-horizontal">
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Tên loại bộ tiêu chí năm<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_14BD172E_E172_48CD_9D16_2387FB1ACB72" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Diễn giải</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_B2C19A7A_2848_470D_93D7_E49F0D2CE13D" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control"></asp:TextBox>
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


        function adddoc(documentID, doctypeID) {
            var tenloaibotieuchinam = GetSvrCtlValue("ID8_14BD172E_E172_48CD_9D16_2387FB1ACB72");
            var diengiai = GetSvrCtlValue("ID8_B2C19A7A_2848_470D_93D7_E49F0D2CE13D");
            var valueactive = $("#ctl00_FormContent_DOCSTATUS").val();

            var url = "ThemMoiLoaiBoTieuChiNam.aspx";
            var query = "act=checkvalue";
            query += "&p=14BD172E-E172-48CD-9D16-2387FB1ACB72";
            query += "&v=" + tenloaibotieuchinam;
            query += "&diengiai=" + diengiai;
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
                        alert("Tại 1 thời điểm, hệ thống chỉ cho phép tồn tại 1 bộ tiêu chí năm được active");
                }
            });
        }

        function updatedoc(documentID) {
            var tenloaibotieuchinam = GetSvrCtlValue("ID8_14BD172E_E172_48CD_9D16_2387FB1ACB72");
            var diengiai = GetSvrCtlValue("ID8_B2C19A7A_2848_470D_93D7_E49F0D2CE13D");
            var valueactive = $("#ctl00_FormContent_DOCSTATUS").val();

            var url = "ThemMoiLoaiBoTieuChiNam.aspx";
            var query = "act=checkvalueupdate";
            query += "&p=14BD172E-E172-48CD-9D16-2387FB1ACB72";
            query += "&p=14BD172E-E172-48CD-9D16-2387FB1ACB72";
            query += "&v=" + tenloaibotieuchinam;
            query += "&diengiai=" + diengiai;
            query += "&valueactive=" + valueactive;
            query += "&doc=" + documentID;
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
            window.location.href = "PhanLoaiBoTieuChiNam.aspx";
        }
        function savedoc_error() {
            alert(MSG_ADD_ER);
        }
        function update_success() {
            alert(MSG_EDIT_OK);
            window.location.href = "PhanLoaiBoTieuChiNam.aspx";
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
