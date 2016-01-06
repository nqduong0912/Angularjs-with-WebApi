<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true"
    CodeBehind="DoanKiemToan_Load.aspx.cs" Inherits="VPB_TDHS.Modules.DMS.DoanKiemToan_Load" %>

<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <asp:ScriptManager runat="server" ID="ScriptManager1">
    </asp:ScriptManager>
    <div class="form-horizontal">
        Trưởng đoàn kiểm toán
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Tên trưởng đoàn<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="DOCSTATUS" Text="2" Style="display: none;" runat="server"></asp:TextBox>
                <asp:TextBox ID="DOCNAME" runat="server" CssClass="form-control"></asp:TextBox>
                <img id="imgCheckUser" alt="Kiểm tra" src="../../Images/checkcif.png" style="width: 16px; height: 16px; cursor: pointer"
                    title="Kiểm tra" onclick="checkUser_click()" />
                <asp:Button runat="server" Text="Đồng ý" ID="btnDongy" class="InsertButton" />
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-4 col-sm-2">
                <label id="FullName"></label>
            </div>
        </div>
        <div class="form-group" id="tr-trangthai" style="display: none;">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Trạng thái</label>
            <div class="col-sm-6">
                <label id="td-trangthai" for="inputName" class="control-label">Chưa submit</label>
            </div>
        </div>
    </div>
    <%--<table width="100%" id="tblTruongDoan">
        <tr class="GridHeader">
            <td colspan="2">Trưởng đoàn kiểm toán
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Tên trưởng đoàn
            </td>
            <td>
                <asp:TextBox ID="DOCSTATUS" Text="2" Style="display: none;" runat="server"></asp:TextBox>
                <asp:TextBox ID="DOCNAME" runat="server" SkinID="TextBoxRequired"></asp:TextBox>
                <img id="imgCheckUser" alt="Kiểm tra" src="../../Images/checkcif.png" style="width: 16px; height: 16px; cursor: pointer"
                    title="Kiểm tra" onclick="checkUser_click()" />
                <asp:Button runat="server" Text="Đồng ý" ID="btnDongy" class="InsertButton" />
            </td>
        </tr>
        <tr id="tr-trangthai" style="display: none;">
            <td>Trạng thái
            </td>
            <td id="td-trangthai">Chưa Submit
            </td>
        </tr>
        <tr>
            <td></td>
            <td id="FullName" style="font-style: italic"></td>
        </tr>
    </table>--%>
    <asp:ObjectDataSource runat="server" ID="ObjectDataSource1" SelectMethod="getListThanhVien"
        TypeName="VPB_KTNB.Helpers.DataSource">
        <SelectParameters>
            <asp:Parameter Name="Status" Type="String" />
            <asp:Parameter Name="TruongDoan" Type="String" />
            <asp:Parameter Name="TruongNhom" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>

    <%--        <tr class="GridHeader">
            <td colspan="2">Danh sách thành viên đoàn kiểm toán
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Thành viên
            </td>
            <td>
                <asp:TextBox ID="THANHVIEN" runat="server" SkinID="TextBox"></asp:TextBox>
                <img alt="Kiểm tra" src="../../Images/checkcif.png" style="width: 16px; height: 16px; cursor: pointer"
                    title="Kiểm tra" onclick="checkUserThanhVien_click()" />
                <asp:Button ID="btnThem" runat="server" Text="Thêm" class="InsertButton" />
            </td>
        </tr>
        <tr>
            <td></td>
            <td id="FullNameThanhVien" style="font-style: italic"></td>
            <td id="UserIDThanhVien" style="font-style: italic; display: none"></td>
        </tr>--%>
    <div id="tblthanhvien" style="display: none">
        <div class="form-horizontal">
            Danh sách thành viên đoàn kiểm toán
                <div class="form-group">
                    <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Thành viên</label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="THANHVIEN" runat="server" CssClass="form-control"></asp:TextBox>
                        <img alt="Kiểm tra" src="../../Images/checkcif.png" style="width: 16px; height: 16px; cursor: pointer"
                            title="Kiểm tra" onclick="checkUserThanhVien_click()" />
                        <asp:Button ID="btnThem" runat="server" Text="Thêm" class="InsertButton" />
                    </div>
                </div>
            <div class="form-group">
                <div class="col-sm-offset-4 col-sm-2">
                    <label id="FullNameThanhVien" class="control-label" style="font-style: italic"></label>
                    <label id="UserIDThanhVien" style="font-style: italic; display: none"></label>

                </div>
            </div>
        <table width="70%">
            <tr>
                <td colspan="2">
                    <asp:UpdatePanel runat="server" ID="updatepanel1" UpdateMode="Conditional" OnLoad="updatepanel1_OnLoad">
                        <ContentTemplate>
                            <asp:HiddenField ID="idTruongDoan" runat="server" />
                            <C1WebGrid:C1WebGrid runat="server" ID="dataCtrl" Width="75%" OnItemCreated="dataCtrl_OnItemCreated"
                                OnItemDataBound="dataCtrl_OnItemDataBound" GroupIndent="20" ItemStyle-Height="30px"
                                DataSourceID="ObjectDataSource1">
                                <Columns>
                                    <C1WebGrid:C1TemplateColumn>
                                        <ItemTemplate>
                                            <asp:Image runat="server" ID="imgDelete" ImageUrl="~/Images/delete.gif" ToolTip="Xóa"
                                                Style="cursor: pointer" />
                                        </ItemTemplate>
                                        <ItemStyle Width="8%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </C1WebGrid:C1TemplateColumn>
                                    <C1WebGrid:C1TemplateColumn Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="PK_UserID" Text='<%# DataBinder.Eval(Container.DataItem,"PK_UserID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </C1WebGrid:C1TemplateColumn>
                                    <C1WebGrid:C1TemplateColumn Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="FK_DOCLINKID" Text='<%# DataBinder.Eval(Container.DataItem,"FK_DOCLINKID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </C1WebGrid:C1TemplateColumn>
                                    <C1WebGrid:C1BoundColumn HeaderText="Tên thành viên" DataField="UserName">
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                    </C1WebGrid:C1BoundColumn>
                                    <C1WebGrid:C1BoundColumn HeaderText="Tên đầy đủ" DataField="FullName">
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                    </C1WebGrid:C1BoundColumn>
                                    <C1WebGrid:C1BoundColumn HeaderText="Phòng ban" DataField="GroupName">
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="30%" />
                                    </C1WebGrid:C1BoundColumn>
                                </Columns>
                            </C1WebGrid:C1WebGrid>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>


    <%--<input id="btn-submit" type="button" value="Submit" onclick="Submit()" class="InsertButton" />--%>
    <script type="text/javascript">
        /*********************************************************/
        var ctl_username;
        var ctl_username_thanhvien;
        var MSG_CONFIRM_ADD_TRUONGDOAN = "Bạn có muốn đưa thành viên này làm trưởng đoàn?";
        var MSG_ALERT_CHOICE_TRUONGDOAN = "Hãy chọn một thành viên làm trưởng đoàn kiểm toán.";
        var MSG_EXIST_THANHVIEN = "Thành viên này đã tồn tại trong đoàn kiểm toán này.";
        var MSG_CONFIRM_ADD_THANHVIEN = "Bạn có muốn đưa thành viên này vào đoàn kiểm toán không?";
        var MSG_ADD_THANHVIEN_SUC = "Đưa thành viên vào đoàn kiểm toán thành công.";
        var MSG_CONFIRM_DEL_THANHVIEN = "Bạn có muốn đưa thành viên này ra khỏi đoàn kiểm toán?";
        var MSG_CONFIRM_SUBMIT = "Bạn có muốn submit?";
        var MSG_CANNOT_SUBMIT = "Chưa có thành viên trong đoàn nên không thể submit!";
        var MSG_USER_NOTEXIST = "Mã thành viên không đúng.";

        //thangma
        var clientIDbtnSave = '<%=_btnSave.ClientID %>';

        $(document).ready(function () {
            ctl_username = '<%=DOCNAME.ClientID %>';
            ctl_username_thanhvien = '<%=THANHVIEN.ClientID %>';
            initCtlEvents();
            //thangma
            if ((_documentID == '') || (_documentID == null) || (_documentID == 'undefined')) {
                //                $('#btn-submit').hide();
                $('#' + clientIDbtnSave).hide();
            }

        });
        function hideshow() {
            var obj = $("#tblThanhVien");
            $("#ctl00_FormContent_DOCNAME").attr("disabled", "disabled");
            //$("#ctl00_FormContent_btnDongy").attr("disabled", "disabled");
            $("#ctl00_FormContent_btnDongy").hide();
            $("#imgCheckUser").attr('disabled', 'true');
            obj.show();

        }

        function initCtlEvents() {
            $("#" + ctl_username).blur(function () {
                var username = $(this).val();
                getfullname(username);
            });
            $("#" + ctl_username_thanhvien).blur(function () {
                var username_thanhvien = $(this).val();
                getfullnamethanhvien(username_thanhvien);
            });
        }
        function getfullname(username) {

            var dotkt = Qry["dotkt"];
            $("#FullName").attr("innerHTML", "");
            var url = "DoanKiemToan_Load.aspx";
            var query = "act=getfullname&user=" + username + "&dotkt=" + dotkt;
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (fullname) {
                    if (fullname != "") {
                        $("#FullName").text(fullname);
                    }
                    else {
                        $("#" + ctl_username).val("");
                        $("#FullName").text(MSG_USER_NOTEXIST);
                    }
                }
            });
        }

        function getfullnamethanhvien(username_thanhvien) {
            var dotkt = Qry["dotkt"];
            $("#FullNameThanhVien").attr("innerHTML", "");
            $("#UserIDThanhVien").attr("innerHTML", "");
            var url = "DoanKiemToan_Load.aspx";
            var query = "act=getfullnamethanhvien&user=" + username_thanhvien + "&dotkt=" + dotkt;
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (fullname) {
                    if (fullname != "") {
                        $("#FullNameThanhVien").attr("innerHTML", fullname.split('#')[0]);
                        $("#UserIDThanhVien").attr("innerHTML", fullname);
                    }
                    else {
                        $("#" + ctl_username_thanhvien).val("");
                        $("#FullNameThanhVien").attr("innerHTML", MSG_USER_NOTEXIST);
                    }
                }
            });
        }

        function ThemTruongDoan(doctypeID) {
            var dotkt = Qry["dotkt"];
            var thanhvien = $("#FullName").html();
            if ((thanhvien == '') || (thanhvien == null) || (thanhvien == 'undefined')
            || (thanhvien == MSG_USER_NOTEXIST)) {
                alert(MSG_ALERT_CHOICE_TRUONGDOAN);
                return false;
            }
            if ((_documentID == '') || (_documentID == null) || (_documentID == 'undefined')) {
                _documentID = NewGuid();
                savedocumentwithlink(_documentID, dotkt, doctypeID, savedoc_success, savedoc_error, MSG_CONFIRM_ADD_TRUONGDOAN);
                var obj = $("#tblThanhVien");
                obj.show();
            }
            else {
                updatedocumentdoankiemtoan(_documentID);
                $("#" + ctl_username).focus();
            }
        }

        function updatedocumentdoankiemtoan(documentID) {
            var truongdoan = $("#FullName").html();
            if ((truongdoan == '') || (truongdoan == null) || (truongdoan == 'undefined')) {
                alert(MSG_ALERT_CHOICE_TRUONGDOAN);
                return false;
            }
            var truongdoan1 = $("#" + ctl_username).val();
            if (!window.confirm('Bạn có muốn cập nhật đoàn kiểm toán này?'))
                return false;
            var url = "DoanKiemToan_View.aspx";
            var query = "act=capnhatdoankiemtoan&user=" + truongdoan1 + "&doc=" + documentID;
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (fullname) {
                    if (fullname != "") {
                        update_success();
                    }
                    else {
                        update_error();
                    }
                }
            });

        }


        function Submit() {
            if (!window.confirm(MSG_CONFIRM_SUBMIT))
                return false;
            var url = "DoanKiemToan_Load.aspx";
            var dotkt = Qry["dotkt"];
            var query = "act=submit";
            query += "&doc=" + _documentID + "&dotkt=" + dotkt;
            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (message) {
                    FinishProcessingForm();
                    //submit thanh cong
                    if (message == '1') {
                        $('#td-trangthai').html('Đã Submit');
                        $('#td-trangthai').css({ "color": "Red", "display": "block" });
                        $('#' + clientIDbtnSave).hide();
                        $("#" + ctl_username).attr('readonly', 'readonly');
                        $("#ctl00_FormContent_btnDongy").hide();
                        $("#imgCheckUser").attr('disabled', 'true');

                        __doPostBack('<%=updatepanel1.ClientID %>', '');

                    }
                        //submit khong thanh cong
                    else if (message == '0') {
                        alert(MSG_CANNOT_SUBMIT);
                    }
                    else if (message == '2') {
                        alert('Chưa mapping Đợt kiểm toán với các mảng nghiệp vụ');
                    }
                }
            });
        }

        function ThemThanhVien() {
            var thanhvien = $("#UserIDThanhVien").html().split('#')[1];
            if ((thanhvien == '') || (thanhvien == null) || (thanhvien == 'undefined'))
                return false;
            if ((_documentID == '') || (_documentID == null) || (_documentID == 'undefined')) {
                alert(MSG_ALERT_CHOICE_TRUONGDOAN);
                return false;
            }

            if (!window.confirm("Bạn muốn thêm thành viên này vào đoàn kiểm toán?"))
                return false;

            var url = "DoanKiemToan_Load.aspx";
            var query = "act=themthanhvien&user=" + thanhvien + "&truongdoan=" + _documentID;
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (fullname) {
                    if (fullname == "2") {
                        //alert(MSG_ADD_THANHVIEN_SUC);
                        $('#<%=idTruongDoan.ClientID %>').val(_documentID);
                        __doPostBack('<%=updatepanel1.ClientID %>', '');
                        $("#" + ctl_username_thanhvien).val("");
                        $("#FullNameThanhVien").attr("innerHTML", "");
                        $("#UserIDThanhVien").attr("innerHTML", "");

                    }
                    else if (fullname == "1") {
                        alert(MSG_EXIST_THANHVIEN);
                        $("#" + ctl_username_thanhvien).val("");
                        $("#FullNameThanhVien").attr("innerHTML", "");
                        $("#UserIDThanhVien").attr("innerHTML", "");
                    }
                }
            });
        }


        function checkUser_click() {
            var username = $("#" + ctl_username).val();
            getfullname(username);
        }

        function checkUserThanhVien_click() {
            var username = $("#" + ctl_username_thanhvien).val();
            getfullnamethanhvien(username);
        }

        var _documentID = '';
        var _thanhvien = '';

        function savedoc_success() {
            $('#' + clientIDbtnSave).show();
            $('#tr-trangthai').show();
            var dotkt = Qry["dotkt"];
            var url = "DoanKiemToan_Load.aspx";
            var query = "act=chuyentrangthaidot&dotkt=" + dotkt + "&truongdoan=" + _documentID;
            //chuyen trang thai dotkt & add default mang nghiep vu
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (fullname) {
                }
            });
        }
        function savedoc_error() {
            alert(MSG_ADD_ER);
        }
        function update_success() {
            alert(MSG_EDIT_OK);
        }
        function update_error() {
            alert(MSG_EDIT_ER);
        }
        function delete_success() {
        }
        function delete_error() {
            alert(MSG_DEL_ER);
        }
        function DeleteDocument(rowId, documentID) {
            deletedocument(documentID, delete_success, delete_error, '');
            $("#" + rowId).hide();
        }

        function CloseWindow() {
            window.location.href = "DoanKiemToan.aspx";
        }

        function xoathanhvien(rowId, documentID, linkID) {
            if (!window.confirm(MSG_CONFIRM_DEL_THANHVIEN))
                return false;
            var url = "DoanKiemToan_Load.aspx";
            var query = "act=xoathanhvien";
            query += "&doc=" + documentID + "&truongdoan=" + linkID;
            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (errormessage) {
                    FinishProcessingForm();
                    if (errormessage == "2") {
                        $("#" + rowId).hide();
                        //alert(_documentID);
                        //alert("Xoá thành công.");
                        //$('#<%=idTruongDoan.ClientID %>').val(_documentID);
                        //__doPostBack('<%=updatepanel1.ClientID %>', '');
                    }
                    else
                        alert(errormessage);
                }
            });

        }

        function getParameterByName(name) {
            name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
            var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
            return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ButtonExtendBefore" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ButtonExtend" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FormFooter" runat="server">
</asp:Content>
