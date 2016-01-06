<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true"
    CodeBehind="ChuongTrinhKiemToan_View.aspx.cs" Inherits="VPB_TDHS.Modules.DMS.ChuongTrinhKiemToan_View" %>

<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <asp:ScriptManager runat="server" ID="ScriptManager1">
    </asp:ScriptManager>
    <div class="form-horizontal" id="tblTruongDoan">
        Thông tin công việc
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Công việc<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_470105E3_B810_4982_A8EF_74E367441EBD" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Bắt đầu<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID3_3E7AE62F_3CF2_4239_A1A0_079CEDD7E057" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Kết thúc<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID3_07A2647B_5E37_4DC8_8D8C_7C457F9D2B1B" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Người thực hiện<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:DropDownList ID="ID8_7DCBBD86_FD24_4415_8EDB_531559D663F2" runat="server" CssClass="form-control" Width="300px">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Người duyệt<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:DropDownList ID="ID8_C983F892_1A81_4F52_98C3_DAAD729A99E8" runat="server" CssClass="form-control" Width="300px">
                </asp:DropDownList>
                <asp:Button runat="server" CssClass="InsertButton" Text="Đồng ý" ID="btnDongy" />
            </div>
        </div>
        <div class="form-group" style="display: none">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Người duyệt 2<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:DropDownList ID="ID8_B9D5B3D6_6118_4821_94F3_AB03B060FC8C" runat="server" CssClass="form-control" Width="300px">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group" style="display: none">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Trạng thái</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="DOCSTATUS" runat="server" CssClass="form-control" Width="100px">
                </asp:DropDownList>
            </div>
        </div>
    </div>
    <%--<table width="100%" id="tblTruongDoan">
        <tr class="GridHeader">
            <td colspan="2">Thông tin công việc
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Công việc
            </td>
            <td>
                <asp:TextBox ID="ID8_470105E3_B810_4982_A8EF_74E367441EBD" runat="server" SkinID="TextBoxRequired"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Bắt đầu
            </td>
            <td>
                <asp:TextBox ID="ID3_3E7AE62F_3CF2_4239_A1A0_079CEDD7E057" runat="server" SkinID="TextBoxDateRequired"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Kết thúc
            </td>
            <td>
                <asp:TextBox ID="ID3_07A2647B_5E37_4DC8_8D8C_7C457F9D2B1B" runat="server" SkinID="TextBoxDateRequired"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Người thực hiện
            </td>
            <td>
                <asp:DropDownList Width="200px" ID="ID8_7DCBBD86_FD24_4415_8EDB_531559D663F2" runat="server"
                    SkinID="DropDownListRequired">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Người duyệt
            </td>
            <td>
                <asp:DropDownList Width="200px" ID="ID8_C983F892_1A81_4F52_98C3_DAAD729A99E8" runat="server"
                    SkinID="DropDownListRequired">
                </asp:DropDownList>
                <asp:Button runat="server" CssClass="InsertButton" Text="Đồng ý" ID="btnDongy" />
            </td>
        </tr>
        <tr style="display: none">
            <td style="width: 222px">Người duyệt 2
            </td>
            <td>
                <asp:DropDownList Width="200px" ID="ID8_B9D5B3D6_6118_4821_94F3_AB03B060FC8C" runat="server"
                    SkinID="DropDownList">
                </asp:DropDownList>
            </td>
        </tr>
        <tr runat="server" visible="false">
            <td style="width: 222px">Trạng thái
            </td>
            <td>
                <asp:DropDownList Width="200px" Enabled="false" ID="DOCSTATUS" runat="server" SkinID="DropDownList">
                </asp:DropDownList>

            </td>
        </tr>
    </table>--%>
    <asp:ObjectDataSource runat="server" ID="ObjectDataSource1" SelectMethod="getListThuTucByCongViec"
        TypeName="DataSource">
        <SelectParameters>
            <asp:Parameter Name="DocumentID" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <table width="100%" id="tblThanhVien">
        <tr class="GridHeader">
            <td colspan="2">Danh sách thủ tục kiểm toán
            </td>
        </tr>
    </table>
    <asp:UpdatePanel runat="server" ID="updatepanel1" UpdateMode="Conditional" OnLoad="updatepanel1_OnLoad">
        <ContentTemplate>
            <div class="form-horizontal">
                <div class="form-group">
                    <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Thủ tục kiểm toán</label>
                    <div class="col-sm-6">
                        <asp:DropDownList ID="ddlThuTuc" runat="server" CssClass="form-control" Width="300px">
                        </asp:DropDownList>
                        <asp:Button runat="server" Text="Thêm thủ tục" ID="btnThem" CssClass="InsertButton" />
                    </div>
                </div>
            </div>
            <table width="100%" runat="server">
               <%-- <tr>
                    <td style="width: 222px">Thủ tục kiểm toán
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlThuTuc" runat="server" SkinID="DropDownList" Width="200px">
                        </asp:DropDownList>
                        <asp:Button runat="server" Text="Thêm thủ tục" ID="btnThem" CssClass="InsertButton" />
                    </td>
                </tr>--%>
                <tr>
                    <td colspan="2">
                        <asp:HiddenField ID="idCongViec" runat="server" />
                        <asp:HiddenField ID="idThanhVien" runat="server" />
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
                                        <asp:Label runat="server" ID="PK_DocumentID" Text='<%# DataBinder.Eval(Container.DataItem,"PK_DocumentID")%>'></asp:Label>
                                    </ItemTemplate>
                                </C1WebGrid:C1TemplateColumn>
                                <C1WebGrid:C1TemplateColumn Visible="false">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="FK_DOCLINKID" Text='<%# DataBinder.Eval(Container.DataItem,"FK_DOCLINKID")%>'></asp:Label>
                                    </ItemTemplate>
                                </C1WebGrid:C1TemplateColumn>
                                <C1WebGrid:C1BoundColumn HeaderText="Thủ tục kiểm toán" DataField="Tên thủ tục kiểm toán">
                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                </C1WebGrid:C1BoundColumn>
                            </Columns>
                        </C1WebGrid:C1WebGrid>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        /*********************************************************/
        var ctl_username;
        var ctl_username_thanhvien;
        var MSG_CONFIRM_ADD_CV = "Bạn có muốn tạo công việc này không?";
        var MSG_ALERT_CHOICE_CV = "Hãy tạo một công việc.";

        var MSG_CONFIRM_ADD_TT = "Bạn có muốn đưa thủ tục kiểm toán này vào công việc này không?";
        var MSG_EXIST_TT = "Thủ tục kiểm toán này này đã tồn tại trong công việc này.";
        var MSG_ADD_TT_SUC = "Đưa thủ tục kiểm toán vào công việc thành công.";
        var MSG_CONFIRM_DEL_TT = "Bạn có muốn đưa thủ tục kiểm toán này ra khỏi công việc?";
        var MSG_CONFIRM_SUBMIT = "Bạn có muốn submit?";
        var MSG_CANNOT_SUBMIT = "Không submit được vì chưa có thủ tục nào";

        //thangma
        var clientIDbtnSave = '<%=_btnSave.ClientID %>';
        _documentID = Qry["doc"];
        var dotkt = Qry["dotkt"];
        $(document).ready(function () {
            initCtlEvents();

            //thangma --doc status
            var docstatus = '<%=_docstatus %>';
            if (docstatus == '0') {
                $('#' + clientIDbtnSave).show();
                $('#td-trangthai').html('Chưa Submit');

            }
            else if (docstatus > 0) {
                $('#' + clientIDbtnSave).hide();
                $('#td-trangthai').html('Đã Submit');
                $('#td-trangthai').css({ "color": "Red" });
                $("#ctl00_btnDELETE").hide();
                $("img[id*='imgDelete']").hide()
            }

            //        $("#divTreeView").hide();
            //        $("#" + imgTreeView).click(function (event) {
            //            if ($("#divTreeView").is(':hidden')) {
            //                $("#divTreeView").show();
            //                showPos(event);
            //            }
            //            else {
            //                $("#divTreeView").hide();
            //            }
            //        });

            $("#ctl00_FormContent_ID8_7DCBBD86_FD24_4415_8EDB_531559D663F2").change(function () {
                var thanhvien = $("#ctl00_FormContent_ID8_7DCBBD86_FD24_4415_8EDB_531559D663F2").val();
                var doankt = Qry["doankt"];
                var dotkt = Qry["dotkt"];

                var url = "ChuongTrinhKiemToan_Load.aspx";
                var query = "act=loadthutuc";
                query += "&doc=" + _documentID + "&dotkt=" + dotkt + "&doankt=" + doankt + "&thanhvien=" + thanhvien;
                $('#<%=idCongViec.ClientID %>').val(_documentID);
                StartProcessingForm("");
                $.ajax({
                    type: "POST",
                    url: url,
                    data: query,
                    success: function (message) {
                        FinishProcessingForm();

                        var obj_thuthuc = $("#ctl00_FormContent_ddlThuTuc");
                        if ((message == '') || (message == null) || (message == 'undefined')) {
                            obj_thuthuc.empty();
                            return false;
                        }
                        obj_thuthuc.empty(); //clear
                        var a = message.split("#");
                        for (var i = 0; i < a.length; i++) {
                            if (a[i].trim().length > 0) {
                                var idthutuc = a[i].split("||")[1];
                                var tenthutuc = a[i].split("||")[0];
                                if (idthutuc == '-1')
                                    obj_thuthuc.append('<option  value="' + idthutuc + '">' + tenthutuc + '</option>');
                                else
                                    obj_thuthuc.append('<option  value="' + idthutuc + '">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;' + tenthutuc + '</option>');
                            }
                        }
                    }
                });
            });
            //css
            $('input[name*="btnThem"]').css('width', '100px');
        });


        function hideshow() {
            var obj = $("#tblThanhVien");
            //$("#ctl00_FormContent_DOCNAME").attr("disabled", "disabled");
            $("#ctl00_FormContent_btnDongy").attr("disabled", "disabled");
            //$("#imgCheckUser").attr('disabled', 'true');
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
            var doankt = Qry["doankt"];
            $("#FullName").attr("innerHTML", "");
            var url = "NhomKiemToan_Load.aspx";
            var query = "act=getfullname&user=" + username + "&doankt=" + doankt;
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (fullname) {
                    if (fullname != "") {
                        $("#FullName").attr("innerHTML", fullname);
                    }
                    else {
                        $("#" + ctl_username).val("");
                        $("#" + ctl_username).focus();
                    }
                }
            });
        }

        function getfullnamethanhvien(username_thanhvien) {
            var doankt = Qry["doankt"];
            $("#FullNameThanhVien").attr("innerHTML", "");
            $("#UserIDThanhVien").attr("innerHTML", "");
            var url = "NhomKiemToan_Load.aspx";
            var query = "act=getfullnamethanhvien&user=" + username_thanhvien + "&doankt=" + doankt;;
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
                        $("#" + ctl_username_thanhvien).focus();
                    }
                }
            });
        }
        function Submit() {
            var thanhvien = $("#ctl00_FormContent_ID8_7DCBBD86_FD24_4415_8EDB_531559D663F2").val();
            if (!window.confirm(MSG_CONFIRM_SUBMIT))
                return false;
            var url = "ChuongTrinhKiemToan_View.aspx";
            var dotkt = Qry['dotkt'];
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
                        $('#<%=idThanhVien.ClientID %>').val(thanhvien);
                        __doPostBack('<%=updatepanel1.ClientID %>', '');
                        $('#' + clientIDbtnSave).hide();
                        $("#ctl00_FormContent_btnDongy").hide();
                        $("#ctl00_btnDELETE").hide();
                        $('#ctl00_FormContent_DOCSTATUS').val('2');
                    }
                        //submit khong thanh cong
                    else if (message == '0') {
                        alert(MSG_CANNOT_SUBMIT);
                    }

                }
            });
        }



        function ThemThuTucTreeView() {
            var inputs = $("form input:checkbox")
            var count = 0;
            var valueNode = '';
            inputs.each(function () {
                if ($(this).is(':checked')) {
                    count++;
                    valueNode = valueNode + '$' + $(this).val();
                }
            });
            if ((count == '') || (count == null) || (count == 'undefined') || (count == 0) || (valueNode == '')) {
                alert("Chọn một thủ tục kiểm toán.");
                return false;
            }

            if (!window.confirm("Bạn muốn thêm thủ tục kiểm toán này vào công việc?"))
                return false;

            var documentID = Qry["doc"];
            var url = "ChuongTrinhKiemToan_View.aspx";
            var query = "act=themthutuctreeview&doc=" + documentID + "&thutuc=" + valueNode;
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (fullname) {
                    if (fullname == "2") {
                        //alert(MSG_ADD_TT_SUC);
                        var val_ddl = $("#ctl00_FormContent_ddlThuTuc").val();
                        $("#ctl00_FormContent_ddlThuTuc option[value='" + val_ddl + "']").remove();
                        //$('#<%=idCongViec.ClientID %>').val(_documentID);
                        __doPostBack('<%=updatepanel1.ClientID %>', '');
                    }
                    else if (fullname == "1") {
                        alert(MSG_EXIST_TT);
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

        function updatedocumentcongviec(documentID) {
            var ngthuchien = GetSvrCtlValue("ID8_7DCBBD86_FD24_4415_8EDB_531559D663F2");
            var ngduyet1 = GetSvrCtlValue("ID8_C983F892_1A81_4F52_98C3_DAAD729A99E8");
            //var ngduyet2 = GetSvrCtlValue("ID8_B9D5B3D6_6118_4821_94F3_AB03B060FC8C");

            if (ngthuchien == ngduyet1) {
                alert("Người thực hiện, Người duyệt phải khác nhau.");
                return false;
            }
            var isDate = CompareDate();
            if (isDate == false) {
                alert("Ngày bắt đầu phải nhỏ hơn ngày kết thúc.");
                return false;
            }
            updatedocument(documentID, update_success, update_error, "Bạn có muốn cập nhật công việc này?");
        }

        function CompareDate() {
            var ngaybatdau = GetSvrCtlValue("ID3_3E7AE62F_3CF2_4239_A1A0_079CEDD7E057");
            var ngayketthuc = GetSvrCtlValue("ID3_07A2647B_5E37_4DC8_8D8C_7C457F9D2B1B");
            var day_ngaybatdau = ngaybatdau.split('/')[0];
            var month_ngaybatdau = ngaybatdau.split('/')[1];
            var year_ngaybatdau = ngaybatdau.split('/')[2];

            var day_ngayketthuc = ngayketthuc.split('/')[0];
            var month_ngayketthuc = ngayketthuc.split('/')[1];
            var year_ngayketthuc = ngayketthuc.split('/')[2];


            if (parseInt(year_ngayketthuc) > parseInt(year_ngaybatdau)) {
                return true;
            }
            if (parseInt(year_ngayketthuc) == parseInt(year_ngaybatdau)) {
                if (parseInt(month_ngayketthuc) > parseInt(month_ngaybatdau)) {
                    return true;
                }
                if (parseInt(month_ngayketthuc) == parseInt(month_ngaybatdau)) {
                    if (parseInt(day_ngayketthuc) < parseInt(day_ngaybatdau))
                        return false;
                    else
                        return true;
                }
                else {
                    return false;
                }
            }
            else {
                return false;
            }

            return true;
        }


        function deletedocumentcongviec(documentID) {
            deletedocument(documentID, delete_success, delete_error, "Bạn có muốn xóa công việc này?");
        }

        function savedoc_success() {
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
            var doankt = Qry["doankt"];
            var dotkt = Qry["dotkt"];
            window.location.href = "ChuongTrinhKiemToan.aspx?doankt=" + doankt + "&dotkt=" + dotkt;

        }
        function delete_error() {
            alert(MSG_DEL_ER);
        }
        function DeleteDocument(rowId, documentID) {
            deletedocument(documentID, delete_success, delete_error, '');
            $("#" + rowId).hide();
        }

        function xoathutuc(rowId, documentID, fk_doclinkID, thutuc) {
            if (!window.confirm(MSG_CONFIRM_DEL_TT))
                return false;
            var url = "ChuongTrinhKiemToan_View.aspx";
            var query = "act=xoathutuc";
            query += "&doc=" + documentID + "&fk_doclinkID=" + fk_doclinkID;
            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (errormessage) {
                    FinishProcessingForm();
                    if (errormessage == "2") {
                        __doPostBack('<%=updatepanel1.ClientID %>', '');
                    }
                    else
                        alert(errormessage);
                }
            });

        }

        function ThemThuTuc() {
            var lenopts = document.getElementById("ctl00_FormContent_ddlThuTuc").length;
            var thutuc = $("#ctl00_FormContent_ddlThuTuc").val();
            var thanhvien = $("#ctl00_FormContent_ID8_7DCBBD86_FD24_4415_8EDB_531559D663F2").val();
            if ((thutuc == '') || (thutuc == null) || (thutuc == 'undefined') || lenopts == 0 || (thutuc == '-1')) {
                alert("Chọn một thủ tục kiểm toán.");
                return false;
            }
            if (!window.confirm("Bạn muốn thêm thủ tục kiểm toán này vào công việc?"))
                return false;

            var documentID = Qry["doc"];
            var url = "ChuongTrinhKiemToan_View.aspx";
            var query = "act=themthutuc&doc=" + documentID + "&thutuc=" + thutuc + "&dotkt=" + dotkt;
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (fullname) {
                    if (fullname == "2") {
                        $('#<%=idThanhVien.ClientID %>').val(thanhvien);
                        __doPostBack('<%=updatepanel1.ClientID %>', '');
                    }
                    else if (fullname == "1") {
                        alert(MSG_EXIST_TT);
                    }
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
