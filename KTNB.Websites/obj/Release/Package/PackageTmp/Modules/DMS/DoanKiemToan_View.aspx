<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true"
    CodeBehind="DoanKiemToan_View.aspx.cs" Inherits="VPB_TDHS.Modules.DMS.DoanKiemToan_View" %>

<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <asp:ScriptManager runat="server" ID="ScriptManager1">
    </asp:ScriptManager>
    <div class="form-horizontal">
        Trưởng đoàn kiểm toán
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Tên trưởng đoàn<span class="star-red">*</span></label>
            <div class="col-sm-6 ">
                <asp:TextBox ID="DOCNAME" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                <img id="imgCheckUser" alt="Kiểm tra" src="../../Images/checkcif.png" style="width: 16px; height: 16px; cursor: pointer"
                    title="Kiểm tra" onclick="checkUser_click()" />
                <asp:Button runat="server" Text="Cập nhật" ID="btnDongy" class="InsertButton" />
            </div>
        </div>
        <div class="col-sm-3 col-sm-offset-4">
            <label id="FullName" style="font-style: italic"></label>
        </div>
        <div style="clear: both"></div>
        <div class="form-group">
            <label for="td-trangthai" class="col-sm-3 col-sm-offset-1 control-label">Trạng thái</label>
            <label id="td-trangthai" class="col-sm-1 control-label" style="font-style: italic"></label>
        </div>
    </div>
    <%--    <table width="100%" id="tblTruongDoan">
        <tr class="GridHeader">
            <td colspan="2">Trưởng đoàn kiểm toán
            </td>
        </tr>--%>
    <%-- <tr>
            <td style="width: 222px">
                Tên trưởng đoàn
            </td>
            <td>
                <asp:Label ID="DOCNAME" runat="server" />
            </td>
        </tr>--%>
    <%--<tr>
            <td style="width: 222px">Tên trưởng đoàn
            </td>
            <td>
                <asp:TextBox ID="DOCNAME" runat="server" SkinID="TextBoxRequired"></asp:TextBox>
                <img id="imgCheckUser" alt="Kiểm tra" src="../../Images/checkcif.png" style="width: 16px; height: 16px; cursor: pointer"
                    title="Kiểm tra" onclick="checkUser_click()" />
                <asp:Button runat="server" Text="Cập nhật" ID="btnDongy" class="InsertButton" />
            </td>
        </tr>--%>

    <%--<tr>
            <td></td>
            <td id="FullName" style="font-style: italic"></td>
        </tr>--%>

    <%--<tr id="tr-trangthai">
            <td style="width: 222px">Trạng thái
            </td>
            <td id="td-trangthai">Chưa Submit
            </td>
        </tr>--%>

    <%--</table>--%>
    <asp:ObjectDataSource runat="server" ID="ObjectDataSource1" SelectMethod="getListThanhVien"
        TypeName="DataSource">
        <SelectParameters>
            <asp:Parameter Name="Status" Type="String" />
            <asp:Parameter Name="TruongDoan" Type="String" />
            <asp:Parameter Name="TruongNhom" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <div id="tblThanhVien" class="form-horizontal">
        Danh sách thành viên đoàn kiểm toán
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Thành viên<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="THANHVIEN" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                <img alt="Kiểm tra" src="../../Images/checkcif.png" style="width: 16px; height: 16px; cursor: pointer"
                    title="Kiểm tra" onclick="checkUserThanhVien_click()" />
                <asp:Button ID="btnThem" runat="server" Text="Thêm" class="InsertButton" />
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-4 col-sm-2">
                <label id="FullNameThanhVien" class="control-label" style="font-style: italic"></label>
                <label id="UserIDThanhVien" class="control-label" style="font-style: italic; display: none"></label>
            </div>
        </div>
        <div class="form-group">
            <asp:UpdatePanel runat="server" ID="updatepanel1" UpdateMode="Conditional" OnLoad="updatepanel1_OnLoad">
                <ContentTemplate>
                    <asp:HiddenField ID="idTruongDoan" runat="server" />
                    <C1WebGrid:C1WebGrid runat="server" ID="dataCtrl" Width="75%" OnItemCreated="dataCtrl_OnItemCreated"
                        OnItemDataBound="dataCtrl_OnItemDataBound" GroupIndent="20" ItemStyle-Height="30px" DataSourceID="ObjectDataSource1">
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
        </div>
    </div>
    <%--<table width="70%" id="tblThanhVien">
        <tr class="GridHeader">
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
        </tr>
        <tr>
            <td colspan="2">
                <asp:UpdatePanel runat="server" ID="updatepanel1" UpdateMode="Conditional" OnLoad="updatepanel1_OnLoad">
                    <ContentTemplate>
                        <asp:HiddenField ID="idTruongDoan" runat="server" />
                        <C1WebGrid:C1WebGrid runat="server" ID="dataCtrl" Width="75%" OnItemCreated="dataCtrl_OnItemCreated"
                            OnItemDataBound="dataCtrl_OnItemDataBound" GroupIndent="20" ItemStyle-Height="30px" DataSourceID="ObjectDataSource1">
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
    </table>--%>
    <%--<input id="btn-submit" type="button" value="Submit" onclick="Submit()" class="InsertButton" />--%>
    <script type="text/javascript">
        /*********************************************************/
        var MSG_CONFIRM_ADD_TRUONGDOAN = "Bạn có muốn đưa thành viên này làm trưởng đoàn?";
        var MSG_ALERT_CHOICE_TRUONGDOAN = "Hãy chọn một thành viên làm trưởng đoàn kiểm toán.";

        var MSG_CONFIRM_ADD_THANHVIEN = "Bạn có muốn đưa thành viên này vào đoàn kiểm toán không?";
        var MSG_EXIST_THANHVIEN = "Thành viên này đã tồn tại trong đoàn kiểm toán này.";
        var MSG_ADD_THANHVIEN_SUC = "Đưa thành viên vào đoàn kiểm toán thành công.";
        var MSG_CONFIRM_DEL_THANHVIEN = "Bạn có muốn đưa thành viên này ra khỏi đoàn kiểm toán?";
        var MSG_CONFIRM_SUBMIT = "Bạn có muốn submit?";
        var MSG_CANNOT_SUBMIT = "Chưa có thành viên trong đoàn nên không thể submit!";
        var MSG_USER_NOTEXIST = "Mã thành viên không đúng.";
        var ctl_username;
        var ctl_username_thanhvien;

        //thangma
        var clientIDbtnSave = '<%=_btnSave.ClientID %>';

        $(document).ready(function () {
            var truongdoan = Qry["truongdoan"];
            $("#FullName").html(truongdoan);
            ctl_username = '<%=DOCNAME.ClientID %>';
            ctl_username_thanhvien = '<%=THANHVIEN.ClientID %>';
            initCtlEvents();


            //thangma --doc status
            var docstatus = '<%=_docstatus %>';
            if (docstatus == '2') {
                //                $('#btn-submit').show();
                $('#' + clientIDbtnSave).show();
                $('#td-trangthai').html('Chưa Submit');

            }
            else if (docstatus == '4') {
                //                $('#btn-submit').hide();
                $('#' + clientIDbtnSave).hide();
                $('#td-trangthai').html('Đã Submit');
                $('#td-trangthai').css({ "color": "Red" });
                $("#" + ctl_username).attr('readonly', 'readonly');
            }

            HiddenControlTimKiem();
        });

        function HiddenControlTimKiem() {
            var timkiem = Qry["timkiem"];
            if (timkiem == 'tk') {
                $("img[id*='imgDelete']").hide();
                $("img[id*='imgEdit']").hide();
                $("input[type*='button']").hide();
                $("input[type*='submit']").hide();
                $("#" + ctl_username).attr('readonly', 'readonly');
                $("#" + ctl_username_thanhvien).attr('readonly', 'readonly');
            }
        }

        function updatedocumentdoankiemtoan(documentID) {
            //alert(documentID);
            var truongdoan = $("#FullName").html();
            if ((truongdoan == '') || (truongdoan == null) || (truongdoan == 'undefined'
            || truongdoan == MSG_USER_NOTEXIST)) {
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

            //updatedocument(documentID, update_success, update_error, "Bạn có muốn cập nhật đoàn kiểm toán này?");
        }

        function hideshow() {
            var obj = $("#tblThanhVien");
            $("#ctl00_FormContent_DOCNAME").attr("disabled", "disabled");
            $("#ctl00_FormContent_btnDongy").attr("disabled", "disabled");
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
            $("#FullName").html("");
            var url = "DoanKiemToan_Load.aspx";
            var query = "act=getfullname&user=" + username + "&dotkt=" + dotkt;;
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (fullname) {
                    if (fullname != "") {
                        $("#FullName").html(fullname);
                    }
                    else {
                        $("#" + ctl_username).val("");
                        $("#" + ctl_username).focus();
                        $("#FullName").html(MSG_USER_NOTEXIST);
                    }
                }
            });
        }

        function getfullnamethanhvien(username_thanhvien) {
            var dotkt = Qry["dotkt"];
            $("#FullNameThanhVien").html("");
            $("#UserIDThanhVien").html("");
            var url = "DoanKiemToan_Load.aspx";
            var query = "act=getfullnamethanhvien&user=" + username_thanhvien + "&dotkt=" + dotkt;
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (fullname) {
                    if (fullname != "") {
                        $("#FullNameThanhVien").html(fullname.split('#')[0]);
                        $("#UserIDThanhVien").html(fullname);
                    }
                    else {
                        $("#" + ctl_username_thanhvien).val("");
                        $("#" + ctl_username_thanhvien).focus();
                        $("#FullNameThanhVien").html(MSG_USER_NOTEXIST);
                    }
                }
            });
        }

        function ThemTruongDoan(doctypeID) {
            _documentID = NewGuid();
            var dotkt = Qry["dotkt"];
            var thanhvien = $("#FullName").html();
            if ((thanhvien == '') || (thanhvien == null) || (thanhvien == 'undefined') ||
            (thanhvien == MSG_USER_NOTEXIST)) {
                alert(MSG_ALERT_CHOICE_TRUONGDOAN);
                return false;
            }
            savedocumentwithlink(_documentID, dotkt, doctypeID, savedoc_success, savedoc_error, MSG_CONFIRM_ADD_TRUONGDOAN);
        }


        function ThemThanhVien() {
            var truongdoan = Qry["doc"];
            var thanhvien = $("#UserIDThanhVien").html().split('#')[1];
            if ((thanhvien == '') || (thanhvien == null) || (thanhvien == 'undefined'))
                return false;
            if (!window.confirm("Bạn muốn thêm thành viên này vào đoàn kiểm toán?"))
                return false;
            var url = "DoanKiemToan_View.aspx";
            var query = "act=themthanhvien&user=" + thanhvien + "&truongdoan=" + truongdoan;
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (fullname) {
                    if (fullname == "2") {
                        //alert(MSG_ADD_THANHVIEN_SUC);
                        __doPostBack('<%=updatepanel1.ClientID %>', '');
                    $("#" + ctl_username_thanhvien).val("");
                    $("#FullNameThanhVien").attr("innerHTML", "");
                    $("#UserIDThanhVien").attr("innerHTML", "");
                }
                else if (fullname == "1") {
                    alert(MSG_EXIST_THANHVIEN);
                    $("#" + ctl_username_thanhvien).val("");
                    $("#FullNameThanhVien").html("");
                    $("#UserIDThanhVien").html("");
                }
                }
            });
    }

    function SaveMangNghiepVu() {
        var dsmangnghiepvu = [];
        var strMangNghiepVu = '';
        $('[id*="chkMangNghiepVu"]').each(function () {
            if (this.checked) {
                dsmangnghiepvu.push($(this).parent().attr('mangnghiepvuid'));
            }
        })
        dsmangnghiepvu.forEach(function (item) {
            strMangNghiepVu += item + '__';
        });
        var url = "DoanKiemToan_View.aspx";
        var doan = Qry["doc"];
        var query = "doc=" + doan + "&act=mappingmangnghiepvu&dsmangnghiepvu=" + strMangNghiepVu;
        $.ajax({
            type: "POST",
            url: url,
            data: query,
            success: function (data) {
                //                    if (fullname == "2") {
                //                        alert(MSG_ADD_THANHVIEN_SUC);
                //                        __doPostBack('<%=updatepanel1.ClientID %>', '');

                //                    }
                //                    else if (fullname == "1") {
                //                        alert(MSG_EXIST_THANHVIEN);
                //                    }

            }
        });
    }
        function Submit() {
            if (!window.confirm(MSG_CONFIRM_SUBMIT))
                return false;
            var url = "DoanKiemToan_View.aspx";
            var dotkt = Qry["dotkt"];
            var query = "act=submit";
            query += "&doc=" + Qry["doc"] + "&dotkt=" + dotkt;

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
                        $('#td-trangthai').css({ "color": "Red" });
                        //                    $('#btn-submit').hide();
                        __doPostBack('<%=updatepanel1.ClientID %>', '');
                        //alert("successfull");
                        $('#' + clientIDbtnSave).hide();
                        $("#" + ctl_username).attr('readonly', 'readonly');
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
            // __doPostBack('<%=updatepanel1.ClientID %>', '');
            hideshow();
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

        function xoathanhvien(rowId, documentID, linkID) {
            if (!window.confirm(MSG_CONFIRM_DEL_THANHVIEN))
                return false;
            var url = "DoanKiemToan_View.aspx";
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
