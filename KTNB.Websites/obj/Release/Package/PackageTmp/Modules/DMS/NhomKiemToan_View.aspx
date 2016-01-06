<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true"
    CodeBehind="NhomKiemToan_View.aspx.cs" Inherits="VPB_TDHS.Modules.DMS.NhomKiemToan_View" %>

<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <asp:ScriptManager runat="server" ID="ScriptManager1">
    </asp:ScriptManager>
    <table width="100%" id="tblTruongDoan">
        <tr class="GridHeader">
            <td colspan="2">
                Trưởng nhóm kiểm toán
            </td>
        </tr>
        <%--  <tr>
            <td style="width: 222px">Tên trưởng nhóm</td>
           <td>
                <asp:Label ID="DOCNAME" runat="server" />
            </td>
        </tr>--%>
        <tr>
            <td style="width: 222px">
                Tên trưởng nhóm
            </td>
            <td>
                <asp:TextBox ID="DOCNAME" runat="server" SkinID="TextBoxRequired"></asp:TextBox>
                <img id="imgCheckUser" alt="Kiểm tra" src="../../Images/checkcif.png" style="width: 16px;
                    height: 16px; cursor: pointer" title="Kiểm tra" onclick="checkUser_click()" />
                <asp:Button runat="server" Text="Cập nhật" ID="btnDongy" class="InsertButton" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td id="FullName" style="font-style: italic">
            </td>
        </tr>
        <tr id="tr-trangthai">
            <td style="width: 222px">
                Trạng thái
            </td>
            <td id="td-trangthai">
                Chưa Submit
            </td>
        </tr>
    </table>
    <asp:ObjectDataSource runat="server" ID="ObjectDataSource1" SelectMethod="getListThanhVien"
        TypeName="DataSource">
        <SelectParameters>
            <asp:Parameter Name="Status" Type="String" />
            <asp:Parameter Name="TruongDoan" Type="String" />
            <asp:Parameter Name="TruongNhom" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <table width="100%" id="tblThanhVien">
        <tr class="GridHeader">
            <td colspan="2">
                Danh sách thành viên nhóm kiểm toán
            </td>
        </tr>
        <tr>
            <td style="width: 222px">
                Thành viên
            </td>
            <td>
                <asp:TextBox ID="THANHVIEN" runat="server" SkinID="TextBox"></asp:TextBox>
                <img alt="Kiểm tra" src="../../Images/checkcif.png" style="width: 16px; height: 16px;
                    cursor: pointer" title="Kiểm tra" onclick="checkUserThanhVien_click()" />
                <asp:Button ID="btnThem" runat="server" Text="Thêm" class="InsertButton" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td id="FullNameThanhVien" style="font-style: italic">
            </td>
            <td id="UserIDThanhVien" style="font-style: italic; display: none">
            </td>
        </tr>
        <tr>
            <td style="width: 222px">
                Mảng nghiệp vụ
            </td>
            <td>
                <asp:DropDownList ID="ddlMangNghiepVu" runat="server" SkinID="DropDownList">
                </asp:DropDownList>
            </td>
        </tr>
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
                                <C1WebGrid:C1TemplateColumn Visible="false">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="MangNghiepVuID" Text='<%# DataBinder.Eval(Container.DataItem,"MangNghiepVuID")%>'></asp:Label>
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
                                <C1WebGrid:C1BoundColumn HeaderText="Mảng nghiệp vụ" DataField="TenMangNghiepVu">
                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="30%" />
                                </C1WebGrid:C1BoundColumn>
                                <C1WebGrid:C1TemplateColumn Visible="false">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="PK_LINKID" Text='<%# DataBinder.Eval(Container.DataItem,"PK_LINKID")%>'></asp:Label>
                                    </ItemTemplate>
                                </C1WebGrid:C1TemplateColumn>
                            </Columns>
                        </C1WebGrid:C1WebGrid>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <%--<input id="btn-submit" type="button" value="Submit" onclick="Submit()" class="InsertButton" />--%>
    <script type="text/javascript">
        /*********************************************************/
        var MSG_CONFIRM_ADD_TRUONGDOAN = "Bạn có muốn đưa thành viên này làm trưởng nhóm?";
        var MSG_ALERT_CHOICE_TRUONGDOAN = "Hãy chọn một thành viên làm trưởng nhóm kiểm toán.";

        var MSG_CONFIRM_ADD_THANHVIEN = "Bạn có muốn đưa thành viên này vào đoàn nhóm toán không?";
        var MSG_EXIST_THANHVIEN = "Mảng nghiệp vụ đã tồn tại trong đợt kiểm toán.";
        var MSG_ADD_THANHVIEN_SUC = "Đưa thành viên vào nhóm kiểm toán thành công.";
        var MSG_CONFIRM_DEL_THANHVIEN = "Bạn có muốn đưa thành viên này ra khỏi nhóm kiểm toán?";
        var MSG_CONFIRM_SUBMIT = "Bạn có muốn submit?";
        var MSG_CANNOT_SUBMIT = "Chưa có thành viên trong đoàn nên không thể submit!";
        var MSG_USER_NOTEXIST = "Mã thành viên không đúng.";

        var ctl_username;
        var ctl_username_thanhvien;

        //thangma
        var clientIDbtnSave = '<%=_btnSave.ClientID %>';

        $(document).ready(function () {
            var truongnhom = Qry["truongnhom"];
            $("#FullName").html(truongnhom);
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
        });
        function hideshow() {
            var obj = $("#tblThanhVien");
            $("#ctl00_FormContent_DOCNAME").attr("disabled", "disabled");
            $("#ctl00_FormContent_btnDongy").attr("disabled", "disabled");
            $("#imgCheckUser").attr('disabled', 'true');
            obj.show();

        }

        function updatedocumentnhomkiemtoan(documentID) {
            //alert(documentID);
            var truongnhom = $("#FullName").html();
            if ((truongnhom == '') || (truongnhom == null) || (truongnhom == 'undefined')
            || (truongnhom == MSG_USER_NOTEXIST)) {
                alert(MSG_ALERT_CHOICE_TRUONGDOAN);
                return false;
            }
            var truongnhom1 = $("#" + ctl_username).val();
            if (!window.confirm('Bạn có muốn cập nhật nhóm kiểm toán này?'))
                return false;
            var url = "NhomKiemToan_View.aspx";
            var query = "act=capnhatnhomkiemtoan&user=" + truongnhom1 + "&doc=" + documentID;
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
            var doankt = Qry["doankt"];
            $("#FullName").html("");
            var url = "NhomKiemToan_View.aspx";
            var query = "act=getfullname&user=" + username + "&doankt=" + doankt;
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
            var doankt = Qry["doankt"];
            $("#FullNameThanhVien").html("");
            $("#UserIDThanhVien").html("");
            var url = "NhomKiemToan_View.aspx";
            var query = "act=getfullnamethanhvien&user=" + username_thanhvien + "&doankt=" + doankt;
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
        function Submit() {
            var items = $('#ctl00_FormContent_ddlMangNghiepVu > option').length;
            if (items > 0) {
                alert("Không cho phép submit vì các mảng nghiệp vụ chưa được gán hết.");
                return false;
            }

            if (!window.confirm(MSG_CONFIRM_SUBMIT))
                return false;
            var url = "NhomKiemToan_View.aspx";
            var query = "act=submit";
            var dotkt = Qry["dotkt"];
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
                        $("#" + ctl_username).attr('readonly', 'readonly');
                        $('#' + clientIDbtnSave).hide();
                        __doPostBack('<%=updatepanel1.ClientID %>', '');
                    }
                    //submit khong thanh cong
                    else {
                        alert(MSG_CANNOT_SUBMIT);
                    }

                }
            });
        }
        function ThemTruongDoan(doctypeID) {

            var dotkt = Qry["dotkt"];
            var thanhvien = $("#FullName").html();
            if ((thanhvien == '') || (thanhvien == null) || (thanhvien == 'undefined')
            || thanhvien == MSG_USER_NOTEXIST) {
                alert(MSG_ALERT_CHOICE_TRUONGDOAN);
                return false;
            }
            _documentID = NewGuid();
            savedocumentwithlink(_documentID, dotkt, doctypeID, savedoc_success, savedoc_error, MSG_CONFIRM_ADD_TRUONGDOAN);
        }


        function ThemThanhVien() {
            var documentID = Qry["doc"];
            var doankt = Qry["doankt"];

            var thanhvien = $("#UserIDThanhVien").html().split('#')[1];
            var lenopts = document.getElementById("ctl00_FormContent_ddlMangNghiepVu").length;
            //alert(thanhvien + "   " + lenopts);
            if ((thanhvien == '') || (thanhvien == null) || (thanhvien == 'undefined') || lenopts == 0) {
                alert("Chọn một thành viên và mảng nghiệp vụ tương ứng");
                return false;
            }
            if (!window.confirm("Bạn muốn gán mảng nghiệp vụ này?"))
                return false;

            var mangnghiepvu = $("#ctl00_FormContent_ddlMangNghiepVu").val();
            var url = "NhomKiemToan_View.aspx";
            var query = "act=themthanhvien&user=" + thanhvien + "&mangnghiepvu=" + mangnghiepvu + "&doc=" + documentID + "&doankt=" + doankt;
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (fullname) {
                    if (fullname == "2") {
                        //alert(MSG_ADD_THANHVIEN_SUC);
                        var val_ddl = $("#ctl00_FormContent_ddlMangNghiepVu").val();
                        $("#ctl00_FormContent_ddlMangNghiepVu option[value='" + val_ddl + "']").remove();
                        $("#" + ctl_username_thanhvien).val("");
                        $("#FullNameThanhVien").html("");
                        $("#UserIDThanhVien").html("");
                        __doPostBack('<%=updatepanel1.ClientID %>', '');

                    }
                    else if (fullname == "1") {
                        $("#" + ctl_username_thanhvien).val("");
                        $("#FullNameThanhVien").html("");
                        $("#UserIDThanhVien").html("");
                        alert(MSG_EXIST_THANHVIEN);
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

        function xoathanhvien(rowId, documentID, linkID, mangnghiepvuID, mangnghiepvu, pk_linkID) {
            if (!window.confirm(MSG_CONFIRM_DEL_THANHVIEN))
                return false;
            var doankt = Qry["doankt"];
            var url = "NhomKiemToan_View.aspx";
            var query = "act=xoathanhvien";
            query += "&doc=" + documentID + "&truongnhom=" + linkID + "&doankt=" + doankt + "&mangnghiepvuID=" + mangnghiepvuID + "&pk_linkID=" + pk_linkID;
            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (errormessage) {
                    FinishProcessingForm();
                    if (errormessage == "2") {
                        $("#" + rowId).hide();
                        var obj_ddl = $("#ctl00_FormContent_ddlMangNghiepVu");
                        obj_ddl.append('<option  value="' + mangnghiepvuID + '">' + mangnghiepvu + '</option>');
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
