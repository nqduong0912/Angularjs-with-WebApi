<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="LoaiTieuChiDanhGiaKTV_Load.aspx.cs" Inherits="VPB_TDHS.Modules.Lookup.LoaiTieuChiDanhGiaKTV_Load" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <div class="form-horizontal">
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Tên loại tiêu chí đánh giá<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_186FFBD8_8695_4304_B2E9_C6BC951B60CC" runat="server" CssClass="form-control" ></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Diễn giải</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_4FBE11C9_2610_45E2_BE28_6E53276DFB91" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control"></asp:TextBox>
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
            var ten = GetSvrCtlValue("ID8_186FFBD8_8695_4304_B2E9_C6BC951B60CC");
            var url = "LoaiTieuChiDanhGiaKTV_Load.aspx";
            var query = "act=checkvalue";
            query += "&p=186FFBD8-8695-4304-B2E9-C6BC951B60CC";
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
                        alert(MSG_DATA_ESXIT);
                }
            });
        }

        function prepareupdatedoc(documentID) {
            var ten = GetSvrCtlValue("ID8_186FFBD8_8695_4304_B2E9_C6BC951B60CC");
            var url = "LoaiTieuChiDanhGiaKTV_Load.aspx";
            var query = "act=checkvalueupdate";
            query += "&p=186FFBD8-8695-4304-B2E9-C6BC951B60CC";
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
            window.location.href = 'LoaiTieuChiDanhGiaKTV.aspx';
        }
        function savedoc_error() {
            alert(MSG_ADD_ER);
        }
        function update_success() {
            alert(MSG_EDIT_OK);
            window.location.href = 'LoaiTieuChiDanhGiaKTV.aspx';
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
