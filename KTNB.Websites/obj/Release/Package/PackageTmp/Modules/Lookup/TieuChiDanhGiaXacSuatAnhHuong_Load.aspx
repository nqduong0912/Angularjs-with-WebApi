<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="TieuChiDanhGiaXacSuatAnhHuong_Load.aspx.cs" Inherits="VPB_TDHS.Modules.Lookup.TieuChiDanhGiaXacSuatAnhHuong_Load" %>

<%@ Register Assembly="C1.Web.C1Input.2" Namespace="C1.Web.C1Input" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <div class="form-horizontal">
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Tên tiêu chí<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_5C64F7E2_116A_43A1_A3AE_333213E15B4E" runat="server" CssClass="form-control" ></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Nhóm tiêu chí đánh giá</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="ID8_31C0688F_D708_4741_941C_1C031D9B6CAE" runat="server" CssClass="form-control" Width="300px">
                    </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Diễn giải</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_6667604E_1FB6_48D3_957F_22A8B5681C70" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Điểm 1<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_B07C032D_8B99_405C_BB8C_C4BB8AD88B31" runat="server" CssClass="form-control" ></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Điểm 2<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_6DCC915B_C3A8_4F26_A8A5_A3FC2FED9BDD" runat="server" CssClass="form-control" ></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Điểm 3<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_E17E9406_5264_442D_B512_C888253D6A8B" runat="server" CssClass="form-control" ></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Điểm 4<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_B320D0B7_BEA3_472A_8557_A28B3EAF5DC3" runat="server" CssClass="form-control" ></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Điểm 5<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_CFE67746_AF75_4893_BEF5_8AEFE9C1438B" runat="server" CssClass="form-control" ></asp:TextBox>
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
            var doc = NewGuid();
            var ten = GetSvrCtlValue("ID8_5C64F7E2_116A_43A1_A3AE_333213E15B4E");
            var nhomtieuchi = GetSvrCtlValue("ID8_31C0688F_D708_4741_941C_1C031D9B6CAE");
            var url = "TieuChiDanhGiaXacSuatAnhHuong_Load.aspx";
            var query = "act=checkvalue";
            query += "&p=5C64F7E2-116A-43A1-A3AE-333213E15B4E";
            query += "&v=" + ten;
            //        query += "&pnhomtieuchi=31C0688F-D708-4741-941C-1C031D9B6CAE";
            //        query += "&vnhomtieuchi=" + nhomtieuchi;
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
            var ten = GetSvrCtlValue("ID8_5C64F7E2_116A_43A1_A3AE_333213E15B4E");
            var nhomtieuchi = GetSvrCtlValue("ID8_31C0688F_D708_4741_941C_1C031D9B6CAE");
            var url = "TieuChiDanhGiaXacSuatAnhHuong_Load.aspx";
            var query = "act=checkvalueupdate";
            query += "&p=5C64F7E2-116A-43A1-A3AE-333213E15B4E";
            query += "&v=" + ten;
            query += "&pnhomtieuchi=31C0688F-D708-4741-941C-1C031D9B6CAE";
            query += "&vnhomtieuchi=" + nhomtieuchi;
            query += "&doc=" + documentID;
            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (data) {
                    FinishProcessingForm();
                    //alert(data);
                    if (data == "0")
                        updatedocument(documentID, update_success, update_error);
                    else
                        alert(MSG_DATA_ESXIT);
                }
            });
        }


        function adddoc(documentID, doctypeID) {
            var doc = NewGuid();
            var tentieuchi = GetSvrCtlValue("ID8_5C64F7E2_116A_43A1_A3AE_333213E15B4E");
            var nhomtieuchi = GetSvrCtlValue("ID8_31C0688F_D708_4741_941C_1C031D9B6CAE");
            var url = "TieuChiDanhGiaXacSuatAnhHuong_Load.aspx";
            var query = "act=adddoc";
            query += "&tentieuchi=" + tentieuchi;
            query += "&nhomtieuchi=" + nhomtieuchi;
            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (IsExist) {
                    FinishProcessingForm();
                    if (IsExist == "0")
                        savedocument(documentID, doctypeID, savedoc_success, savedoc_error);
                    else if (IsExist == "1")
                        alert(MSG_DATA_ESXIT);
                }
            });
        }


        function updatedoc(documentID) {
            var tentieuchi = GetSvrCtlValue("ID8_5C64F7E2_116A_43A1_A3AE_333213E15B4E");
            var nhomtieuchi = GetSvrCtlValue("ID8_31C0688F_D708_4741_941C_1C031D9B6CAE");
            var url = "TieuChiDanhGiaXacSuatAnhHuong_Load.aspx";
            var query = "act=updatedoc";
            query += "&tentieuchi=" + tentieuchi;
            query += "&nhomtieuchi=" + nhomtieuchi;
            query += "&doc=" + documentID;
            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (fullname) {
                    FinishProcessingForm();
                    //alert(data);
                    if (fullname == "1")
                        updatedocument(documentID, update_success, update_error);
                    else if (fullname == "0")
                        alert(MSG_DATA_ESXIT);
                }
            });
        }


        function savedoc_success() {
            alert(MSG_ADD_OK);
            window.location.href = 'TieuChiDanhGiaXacSuatAnhHuong.aspx';
        }
        function savedoc_error() {
            alert(MSG_ADD_ER);
        }
        function update_success() {
            alert(MSG_EDIT_OK);
            window.location.href = 'TieuChiDanhGiaXacSuatAnhHuong.aspx';
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

