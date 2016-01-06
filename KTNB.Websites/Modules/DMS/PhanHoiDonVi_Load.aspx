<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true"
    CodeBehind="PhanHoiDonVi_Load.aspx.cs" Inherits="VPB_TDHS.Modules.DMS.PhanHoiDonVi_Load"%>

<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <asp:ScriptManager runat="server" ID="ScriptManager1">
    </asp:ScriptManager>
    <div class="form-horizontal" id="tblTruongDoan">
        Thêm/Sửa Thông tin phản hồi
        <div class="form-group">
             <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Đối tượng kiểm toán</label>
             <div class="col-sm-6">
                 <asp:TextBox ID="txtDoiTuongKiemToan" runat="server" CssClass="form-control" Enabled="False" Width="300px"></asp:TextBox>
             </div>
         </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Đợt kiểm toán</label>
            <div class="col-sm-6">
                <asp:TextBox ID="txtDotKiemToan" runat="server" CssClass="form-control" Enabled="False" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Công việc</label>
            <div class="col-sm-6">
                <asp:TextBox ID="txtCongViec" runat="server" CssClass="form-control" Enabled="False" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Phát hiện<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="txtPhatHien" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Nội dung</label>
            <div class="col-sm-6">
                <asp:TextBox ID="DOCDESCRIPTION" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group" id="idTrangThai">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Trạng thái</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="DOCSTATUS" runat="server" CssClass="form-control" Width="100px">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group" id="trDaNhanXet">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Đã nhận xét</label>
            <div class="col-sm-6">
                <asp:TextBox Rows="5" Columns="55" Text="" Enabled="false" TextMode="MultiLine" ID="DaNhanXetNguoiDuyet1"
                    runat="server" CssClass="form-control"></asp:TextBox>(người duyệt)
            </div>
        </div>
        <table></table>
        <div class="form-group" id="trNhanXet">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Nhận xét</label>
            <div class="col-sm-6">
                <asp:TextBox Rows="5" Columns="55" Text="" Enabled="false" TextMode="MultiLine" ID="DOCNAME"
                    runat="server" CssClass="form-control"></asp:TextBox>(người duyệt)
            </div>
        </div>
        <div class="form-group" id="trLichSu">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Lịch sử từ chối</label>
            <div class="col-sm-6">
                <C1WebGrid:C1WebGrid runat="server" ID="gridTuChoi" Width="75%" GroupIndent="20"
                    ItemStyle-Height="30px">
                    <Columns>
                        <C1WebGrid:C1BoundColumn HeaderText="Lý do từ chối" DataField="LyDo">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Người từ chối" DataField="NguoiNhanXet">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Ngày từ chối" DataField="NgayNhanXet">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                    </Columns>
                </C1WebGrid:C1WebGrid>
            </div>
        </div>
    </div>
    <%--<table width="100%" id="tblTruongDoan">
        <tr class="GridHeader">
            <td colspan="2">Thêm/Sửa Thông tin phản hồi
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Đối tượng kiểm toán
            </td>
            <td>
                <asp:TextBox Enabled="false" ID="txtDoiTuongKiemToan" runat="server" SkinID="TextBoxReadOnly"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Đợt kiểm toán
            </td>
            <td>
                <asp:TextBox Enabled="false" ID="txtDotKiemToan" runat="server" SkinID="TextBoxReadOnly"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Công việc
            </td>
            <td>
                <asp:TextBox Enabled="false" ID="txtCongViec" runat="server" SkinID="TextBoxReadOnly"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Phát hiện
            </td>
            <td>
                <asp:TextBox Enabled="false" SkinID="TextBoxReadOnly" ID="txtPhatHien" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Nội dung
            </td>
            <td>
                <asp:TextBox Columns="50" Rows="5" TextMode="multiline" ID="DOCDESCRIPTION" runat="server"
                    SkinID="TextBoxRequired"></asp:TextBox>
            </td>
        </tr>
        <tr id="idTrangThai">
            <td style="width: 222px">Trạng thái
            </td>
            <td>
                <asp:DropDownList ID="DOCSTATUS" runat="server" SkinID="DropDownList" Enabled="false">
                </asp:DropDownList>
            </td>
        </tr>
        <tr id="trDaNhanXet" runat="server">
            <td style="width: 222px">Đã Nhận xét
            </td>
            <td>
                <asp:TextBox Rows="2" Columns="55" Text="" Enabled="false" TextMode="MultiLine" ID="DaNhanXetNguoiDuyet1"
                    runat="server" SkinID="TextBoxReadOnly"></asp:TextBox>(người duyệt)
                <br />
            </td>
        </tr>
        <tr id="trNhanXet" runat="server">
            <td style="width: 222px">Nhận xét
            </td>
            <td>
                <asp:TextBox Rows="2" Columns="55" Text="" TextMode="MultiLine" ID="DOCNAME" runat="server"
                    SkinID=""></asp:TextBox>
            </td>
        </tr>
        <tr id="trLichSu" runat="server">
            <td style="width: 222px; vertical-align: top;">Lịch sử từ chối
            </td>
            <td>
                <C1WebGrid:C1WebGrid runat="server" ID="gridTuChoi" Width="75%" GroupIndent="20"
                    ItemStyle-Height="30px">
                    <Columns>
                        <C1WebGrid:C1BoundColumn HeaderText="Lý do từ chối" DataField="LyDo">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Người từ chối" DataField="NguoiNhanXet">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Ngày từ chối" DataField="NgayNhanXet">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                    </Columns>
                </C1WebGrid:C1WebGrid>
            </td>
        </tr>
    </table>--%>
    <script type="text/javascript">
        var MSG_CONFIRM_ADD_CV = "Bạn có muốn tạo công việc này không?";
        var MSG_ALERT_CHOICE_CV = "Hãy tạo một công việc.";

        var MSG_CONFIRM_ADD_TT = "Bạn có muốn đưa thủ tục kiểm toán này vào công việc này không?";
        var MSG_EXIST_TT = "Thủ tục kiểm toán này này đã tồn tại trong công việc này.";
        var MSG_ADD_TT_SUC = "Đưa thủ tục kiểm toán vào công việc thành công.";
        var MSG_CONFIRM_DEL_TT = "Bạn có muốn đưa thủ tục kiểm toán này ra khỏi công việc?";
        var MSG_CONFIRM_SUBMIT = "Bạn có muốn cập nhật?";
        var MSG_CONFIRM_REJECT = "Bạn có muốn từ chối?";

        var MSG_CANNOT_SUBMIT = "Không submit được vì chưa có thủ tục nào";
        var MSG_CHECK_EMPTY = 'BẠN VUI LÒNG NHẬP ĐẦY ĐỦ THÔNG TIN VÀO CÁC TRƯỜNG ĐƯỢC YÊU CẦU.';
        MSG_CHECK_EMPTY += '\n---------------------------------------------------------------------------------------------------';

        var _documentid = "<%= _documentid %>"; //phanhoiID
        var _phathienid = "<%= _phathienid %>";
        var cv = Qry["cv"];
        var congviec_docid = Qry["congviec_docid"];
        var dotkt = Qry["dotkt"];
        var doankt = Qry["doankt"];
        var role = Qry["role"];
        //var documentID = Qry["doc"];//phathienID
        /*********************************************************/
        $(document).ready(function () {
            //do smt here
            $("#idTrangThai").hide();
            if (_documentid == '') {
                $('#trAttachFile').hide();
                $('#trAttachFileList').hide();
            }
            ShowHideNhanXet();
            //css
            $('input[name*="btnFINISH"]').css('width', '80px');
            if (role == 'td') {
                $('input[name*="btnFINISH"]').hide();
                $('#trDaNhanXet').hide();
                $('#trLichSu').hide();
            }
                
        });
        function ShowHideNhanXet() {
            if (cv == 'nguoiduyet') {
                $('#' + '<%=DOCNAME.ClientID %>').show();
            }
            else {

                $('#trNhanXet').hide();
                $('#' + '<%=DOCNAME.ClientID %>').hide();
            }

        }
        /*********************************************************/
        function preparesavedoc(documentID, doctypeID) {
            var ten = GetSvrCtlValue("ID8_E48E3FFD_FE56_4B14_AC36_2C036873E1CD");
            var url = "PhatHienHeThong_Load.aspx";
            var query = "act=checkvalue";
            query += "&p=E48E3FFD-FE56-4B14-AC36-2C036873E1CD";
            query += "&v=" + ten;
            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (data) {
                    FinishProcessingForm();
                    if (data == "0")
                        savedocumentwithlink(documentID, _congviec_docid, doctypeID, savedoc_success, savedoc_error, "")
                    else
                        alert(MSG_DATA_ESXIT);
                }
            });
        }
        function OpenAttachFile() {
            window.location.href = 'UploadFile.aspx?doc=' + _documentid + "&congviec_docid=" + congviec_docid + "&cv=" + cv + "&doankt=" + doankt + "&dotkt=" + dotkt;
        }
        function saveNoCheck(documentID, doctypeID) {

            savedocumentwithlink(documentID, _phathienid, doctypeID, savedoc_success, savedoc_error, "")
        }
        function updateNoCheck(documentID) {
            var noidung = GetSvrCtlValue("DOCDESCRIPTION");
            if (noidung == null || noidung == '') {
                alert(MSG_CHECK_EMPTY);
                return false;
            }
            else {
                var url = "PhanHoiDonVi_Load.aspx";
                var query = "act=updatenoidung";
                query += "&doc=" + _documentid + "&noidung=" + GetSvrCtlValue("DOCDESCRIPTION");
                query += "&congviec_docid=" + congviec_docid;
                query += "&doankt=" + doankt;
                query += "&dotkt=" + dotkt;
                StartProcessingForm("");
                $.ajax({
                    type: "POST",
                    url: url,
                    data: query,
                    success: update_success
                });
            }
        }
        function prepareupdatedoc(documentID) {
            var ten = GetSvrCtlValue("ID8_4138B099_7CBD_443E_A12B_9A1FF5D1E08F");
            var url = "KiemSoat_Load.aspx";
            var query = "act=checkvalueupdate";
            query += "&p=4138B099-7CBD-443E-A12B-9A1FF5D1E08F";
            query += "&v=" + ten;
            query += "&doc=" + documentID;
            query += "&doankt=" + doankt;
            query += "&dotkt=" + dotkt;
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
            if (role == 'td') {
                //chuyển trạng thái đợt lên: Cập nhật phản hồi
                var url = "PhanHoiDonVi_Load.aspx";
                var query = "act=capnhattrangthaidot";
                query += "&dotkt=" + dotkt;
                StartProcessingForm("");
                $.ajax({
                    type: "POST",
                    url: url,
                    data: query,
                    success: function (data) {
                        FinishProcessingForm();
                        window.location.href = 'PhanHoiDonVi.aspx?act=loaddoc&doc=' + _phathienid + "&cv=" + cv + "&congviec_docid=" + congviec_docid + "&doankt=" + doankt + "&dotkt=" + dotkt;
                    }
                });
            }
            else {
                window.location.href = 'PhanHoiDonVi.aspx?act=loaddoc&doc=' + _phathienid + "&cv=" + cv + "&congviec_docid=" + congviec_docid + "&doankt=" + doankt + "&dotkt=" + dotkt;
            }

        }
        function savedoc_error() {
            alert(MSG_ADD_ER);
        }
        function update_success() {
            alert(MSG_EDIT_OK);
            window.location.href = 'PhanHoiDonVi.aspx?act=loaddoc&doc=' + _phathienid + "&cv=" + cv + "&congviec_docid=" + congviec_docid + "&doankt=" + doankt + "&dotkt=" + dotkt;
        }
        function update_error() {
            alert(MSG_EDIT_ER);
        }
        function delete_success() {
            alert(MSG_DEL_OK);
            window.location.href = 'PhanHoiDonVi.aspx?act=loaddoc&doc=' + _phathienid + "&cv=" + cv + "&congviec_docid=" + congviec_docid + "&doankt=" + doankt + "&dotkt=" + dotkt;
        }
        function delete_error() {
            alert(MSG_DEL_ER);
        }
        function DeleteFile(FileID) {
            var url = "PhatHienHeThong_Load.aspx";
            var query = "act=deletefile";
            query += "&congviec_docid=" + congviec_docid;
            query += "&v=" + FileID;
            query += "&doankt=" + doankt;
            query += "&dotkt=" + dotkt;
            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (data) {
                    FinishProcessingForm();
                    delete_success();
                }
            });
        }
        function capnhattrangthaidone(phanhoiID) {
            if (!window.confirm(MSG_CONFIRM_SUBMIT))
                return false;
            //alert(documentID);
            var url = "PhanHoiDonVi_Load.aspx";
            var query = "act=capnhattrangthaidone";
            query += "&doc=" + phanhoiID + "&cv=" + cv + "&phathienid=" + _phathienid;
            query += "&congviec_docid=" + congviec_docid;
            query += "&doankt=" + doankt;
            query += "&dotkt=" + dotkt;
            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (data) {
                    FinishProcessingForm();
                    window.location.href = "PhanHoiDonVi.aspx?act=loaddoc&doc=" + _phathienid + "&cv=" + cv + "&congviec_docid=" + congviec_docid + "&doankt=" + doankt + "&dotkt=" + dotkt;
                }
            });
        }


        function capnhattrangthaipheduyet(phanhoiID) {
            if (!window.confirm(MSG_CONFIRM_SUBMIT))
                return false;
            var url = "PhanHoiDonVi_Load.aspx";
            var query = "act=capnhattrangthaipheduyet";
            query += "&doc=" + phanhoiID + "&cv=" + cv + "&phathienid=" + _phathienid;
            query += "&congviec_docid=" + congviec_docid;
            query += "&doankt=" + doankt;
            query += "&dotkt=" + dotkt;
            var nhanXet = GetSvrCtlValue("DOCNAME");
            query += "&nhanxet=" + nhanXet;
            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (data) {
                    FinishProcessingForm();
                    window.location.href = "PhanHoiDonVi.aspx?act=loaddoc&doc=" + _phathienid + "&cv=" + cv + "&congviec_docid=" + congviec_docid + "&doankt=" + doankt + "&dotkt=" + dotkt;
                }
            });
        }

        function capnhattrangthaituchoi(phanhoiID) {
            if (!window.confirm(MSG_CONFIRM_REJECT))
                return false;
            var url = "PhanHoiDonVi_Load.aspx";
            var query = "act=capnhattrangthaituchoi";
            query += "&doc=" + phanhoiID + "&cv=" + cv + "&phathienid=" + _phathienid;
            query += "&congviec_docid=" + congviec_docid;
            query += "&doankt=" + doankt;
            query += "&dotkt=" + dotkt;
            var nhanXet = GetSvrCtlValue("DOCNAME");
            query += "&nhanxet=" + nhanXet;
            nhanXet = nhanXet.trim();
            if (nhanXet.length == 0) {
                alert('Bạn chưa nhập nhận xét');
                return false;
            }
            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (data) {
                    FinishProcessingForm();
                    window.location.href = "PhanHoiDonVi.aspx?act=loaddoc&doc=" + _phathienid + "&cv=" + cv + "&congviec_docid=" + congviec_docid + "&doankt=" + doankt + "&dotkt=" + dotkt;
                }
            });
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ButtonExtendBefore" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ButtonExtend" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FormFooter" runat="server">
</asp:Content>
