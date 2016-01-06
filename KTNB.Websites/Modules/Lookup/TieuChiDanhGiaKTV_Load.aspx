<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="TieuChiDanhGiaKTV_Load.aspx.cs" Inherits="VPB_TDHS.Modules.Lookup.TieuChiDanhGiaKTV_Load" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <div class="form-horizontal">
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Tên tiêu chí đánh giá<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_1AC09A4B_EC8F_4FEB_9908_FFC06D294E25" runat="server" CssClass="form-control" ></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Loại tiêu chí đánh giá</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="ID8_170AE424_36DC_4B5C_A88B_188FA9947EEA" runat="server" CssClass="form-control" Width="300px">
                    </asp:DropDownList>
            </div>
        </div>
         <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Diễn giải</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_D7C67D2B_0E3F_4024_8DC0_6387AE7E6A3E" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control"></asp:TextBox>
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
            var ten = GetSvrCtlValue("ID8_1AC09A4B_EC8F_4FEB_9908_FFC06D294E25");
            var url = "TieuChiDanhGiaKTV_Load.aspx";
            var query = "act=checkvalue";
            query += "&p=1AC09A4B-EC8F-4FEB-9908-FFC06D294E25";
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
            var ten = GetSvrCtlValue("ID8_1AC09A4B_EC8F_4FEB_9908_FFC06D294E25");
            var url = "TieuChiDanhGiaKTV_Load.aspx";
            var query = "act=checkvalueupdate";
            query += "&p=1AC09A4B-EC8F-4FEB-9908-FFC06D294E25";
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
            window.location.href = 'TieuChiDanhGiaKTV.aspx';
        }
        function savedoc_error() {
            alert(MSG_ADD_ER);
        }
        function update_success() {
            alert(MSG_EDIT_OK);
            window.location.href = 'TieuChiDanhGiaKTV.aspx';
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
