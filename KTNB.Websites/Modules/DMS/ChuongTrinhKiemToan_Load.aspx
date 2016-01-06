<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true"
    CodeBehind="ChuongTrinhKiemToan_Load.aspx.cs" Inherits="VPB_TDHS.Modules.DMS.ChuongTrinhKiemToan_Load" %>

<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <asp:ScriptManager runat="server" ID="ScriptManager1">
    </asp:ScriptManager>
    <div class="form-horizontal">
        <div class="GridHeader">Thông tin công việc</div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Công việc<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_470105E3_B810_4982_A8EF_74E367441EBD" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputYear" class="col-sm-3 col-sm-offset-1 control-label">Bắt đầu<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID3_3E7AE62F_3CF2_4239_A1A0_079CEDD7E057" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputYear" class="col-sm-3 col-sm-offset-1 control-label">Kết thúc<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID3_07A2647B_5E37_4DC8_8D8C_7C457F9D2B1B" runat="server" CssClass="form-control datepicker" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Người thực hiện</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="ID8_7DCBBD86_FD24_4415_8EDB_531559D663F2" runat="server" CssClass="form-control" Width="300px">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Người duyệt</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="ID8_C983F892_1A81_4F52_98C3_DAAD729A99E8" runat="server" CssClass="form-control" Width="300px">
                </asp:DropDownList>
                <asp:Button runat="server" CssClass="InsertButton" Text="Đồng ý" ID="btnDongy" />
            </div>
        </div>
        <div class="form-group" style="display: none">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Người duyệt 2</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="ID8_B9D5B3D6_6118_4821_94F3_AB03B060FC8C" runat="server" CssClass="form-control" Width="300px">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group" style="display: none">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Trạng thái</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="DOCSTATUS" Enabled="false" runat="server" CssClass="form-control" Width="100px">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group" id="trNhanXet">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Nhận xét</label>
            <div class="col-sm-6">
                 <asp:TextBox Rows="2" Columns="55" Text="" TextMode="MultiLine" ID="ID8_AE50F25A_A04E_4541_8266_CE8C5033E0E5"
                    runat="server" CssClass="form-control"></asp:TextBox>
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
                <asp:TextBox ID="" runat="server" SkinID="TextBoxRequired"></asp:TextBox>
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
        <tr id="trNhanXet" runat="server">
            <td style="width: 222px">Nhận xét
            </td>
            <td>
                <asp:TextBox Rows="2" Columns="55" Text="" TextMode="MultiLine" ID="ID8_AE50F25A_A04E_4541_8266_CE8C5033E0E5"
                    runat="server" SkinID=""></asp:TextBox>
            </td>
        </tr>
    </table>--%>
    <asp:ObjectDataSource runat="server" ID="ObjectDataSource1" SelectMethod="getListThuTucByCongViec"
        TypeName="VPB_KTNB.Helpers.DataSource">
        <SelectParameters>
            <asp:Parameter Name="DocumentID" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <%--style="display:none"--%>
    <div id="tblThanhVien" style="display: none;">
        <asp:UpdatePanel runat="server" ID="updatepanel1" UpdateMode="Conditional" OnLoad="updatepanel1_OnLoad">
            <ContentTemplate>
                <div class="form-horizontal">
                    Danh sách thủ tục kiểm toán
                    <div class="form-group">
                        <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Thủ tục kiểm toán</label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlThuTuc" runat="server" CssClass="form-control" Width="300px">
                            </asp:DropDownList>
                            <asp:Button runat="server" Text="Thêm thủ tục" ID="btnThem" CssClass="InsertButton" />
                        </div>
                    </div>
                </div>
                <table runat="server" width="100%">
                    <%--<tr class="GridHeader">
                        <td colspan="2">Danh sách thủ tục kiểm toán
                        </td>
                    </tr>
                    <tr>
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
    </div>
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
        var dotkt = Qry["dotkt"];
        //thangma
        var clientIDbtnSave = '<%=_btnSave.ClientID %>';

        $(document).ready(function () {
            //DateTime Picker
            //bat dau
            $("#<%=ID3_3E7AE62F_3CF2_4239_A1A0_079CEDD7E057.ClientID%>").datepicker({
                showOn: 'both',
                buttonImage: "../../Images/calendar.png",
                buttonImageOnly: true,

            });
            
            $("#<%=ID3_3E7AE62F_3CF2_4239_A1A0_079CEDD7E057.ClientID%>").datepicker('setDate', new Date());
            $("#<%=ID3_3E7AE62F_3CF2_4239_A1A0_079CEDD7E057.ClientID%>").datepicker('option', 'dateFormat', 'dd/mm/yy');

            //ket thuc
            $("#<%=ID3_07A2647B_5E37_4DC8_8D8C_7C457F9D2B1B.ClientID%>").datepicker({
                showOn: 'both',
                buttonImage: "../../Images/calendar.png",
                buttonImageOnly: true,
            });
            $("#<%=ID3_07A2647B_5E37_4DC8_8D8C_7C457F9D2B1B.ClientID%>").datepicker('setDate', new Date());
            $("#<%=ID3_07A2647B_5E37_4DC8_8D8C_7C457F9D2B1B.ClientID%>").datepicker('option', 'dateFormat', 'dd/mm/yy');

            ctl_ddlThuTuc = '<%=ddlThuTuc.ClientID%>';

            initCtlEvents();
            //thangma
            if ((_documentID == '') || (_documentID == null) || (_documentID == 'undefined')) {
                $('#' + clientIDbtnSave).hide();
            }
            $('#' + '<%=ID8_AE50F25A_A04E_4541_8266_CE8C5033E0E5.ClientID %>').hide();
            //thangma end
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
        function Submit() {
            if (!window.confirm(MSG_CONFIRM_SUBMIT))
                return false;
            var thanhvien = $("#ctl00_FormContent_ID8_7DCBBD86_FD24_4415_8EDB_531559D663F2").val();

            var url = "ChuongTrinhKiemToan_Load.aspx";
            var dotkt = Qry['dotkt'];
            var query = "act=submit";
            query += "&doc=" + _documentID + "&dotkt=" + dotkt;
            $('#<%=idCongViec.ClientID %>').val(_documentID);
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
        function hideshow() {
            var obj = $("#tblThanhVien");
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
            var query = "act=getfullnamethanhvien&user=" + username_thanhvien + "&doankt=" + doankt;
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

        function ThemCongViec(doctypeID) {
            //nguoi duyet 1 va nguoi duyet 2 ko the trung nhau
            var ngthuchien = GetSvrCtlValue("ID8_7DCBBD86_FD24_4415_8EDB_531559D663F2");
            var ngduyet1 = GetSvrCtlValue("ID8_C983F892_1A81_4F52_98C3_DAAD729A99E8");
            //var ngduyet2 = GetSvrCtlValue("ID8_B9D5B3D6_6118_4821_94F3_AB03B060FC8C");
            if ((ngthuchien == '') || (ngthuchien == null) || (ngthuchien == 'undefined')
            || (ngduyet1 == '') || (ngduyet1 == null) || (ngduyet1 == 'undefined')) {
                alert("BẠN VUI LÒNG NHẬP ĐẦY ĐỦ THÔNG TIN VÀO CÁC TRƯỜNG ĐƯỢC YÊU CẦU.");
                return false;
            }

            if (ngthuchien == ngduyet1) {
                alert("Người thực hiện, Người duyệt phải khác nhau.");
                return false;
            }

            var isDate = CompareDate();
            if (isDate == false) {
                alert("Ngày bắt đầu phải nhỏ hơn ngày kết thúc.");
                return false;
            }

            if (!window.confirm(MSG_DO_QUESTION))
                return false;

            if ((_documentID == '') || (_documentID == null) || (_documentID == 'undefined')) {
                AddDocument(doctypeID);
            } else {
                UpdateDocument(_documentID);
            }

        }

        function UpdateDocument(documentID) {
            //updatedocument(documentID, update_success, update_error, "Bạn có muốn cập nhật công việc này?");
            updatedocument(documentID, update_success, update_error, "");
        }

        function AddDocument(doctypeID) {
            var doankt = Qry["doankt"];
            var dotkt = Qry["dotkt"];
            //kiem tra da coPTRR tai dotkt nay chua
            var url = "ChuongTrinhKiemToan_Load.aspx";
            var query = "act=checkPTRR&doankt=" + doankt + "&dotkt=" + dotkt;
            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (fullname) {
                    FinishProcessingForm();
                    if (fullname == "1") {
                        _documentID = NewGuid();
                        //var o = savedocumentwithlink(_documentID, doankt, doctypeID, savedoc_success, savedoc_error, MSG_CONFIRM_ADD_CV);
                        var o = savedocumentwithlink(_documentID, doankt, doctypeID, savedoc_success, savedoc_error, "");
                        if (o == false) {
                            return false;
                        }
                        $("#ctl00_FormContent_ID8_7DCBBD86_FD24_4415_8EDB_531559D663F2").attr('disabled', 'disabled'); //nguoithuchien
                    }
                    else if (fullname == "0") {
                        alert("Không thêm được công việc vì đoàn kiểm toán chưa đc lập hồ sơ rủi ro.");
                    }
                }
            });

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
            hideshow();
            $('#' + clientIDbtnSave).show();
            //thangma
            //chuyen trang thai dot kiem toan len trang thai Lập chương trình kiểm toán
            //            var url = "ChuongTrinhKiemToan_Load.aspx";
            //            var query = "act=chuyentrangthaidotkt";
            //            var dotkt = Qry["dotkt"];
            //            query += "&dotkt=" + dotkt;
            //            //            StartProcessingForm("");
            //            $.ajax({
            //                type: "POST",
            //                url: url,
            //                data: query,
            //                success: function (ErrorMessage) {

            //                }
            //            });
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

        function xoathutuc(rowId, documentID, fk_doclinkID, thutuc) {
            if (!window.confirm(MSG_CONFIRM_DEL_TT))
                return false;
            var url = "ChuongTrinhKiemToan_Load.aspx";
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
                        //$("#" + rowId).hide();
                        //var obj_ddl = $("#ctl00_FormContent_ddlThuTuc");
                        //obj_ddl.append('<option  value="' + documentID + '">' + thutuc + '</option>');
                        __doPostBack('<%=updatepanel1.ClientID %>', '');
                    }
                    else
                        alert(errormessage);
                }
            });

        }

        function ThemThuTuc() {
            //var thanhvien = $("#UserIDThanhVien").html().split('#')[1];
            var lenopts = document.getElementById("ctl00_FormContent_ddlThuTuc").length;
            var thutuc = $("#ctl00_FormContent_ddlThuTuc").val();
            var thanhvien = $("#ctl00_FormContent_ID8_7DCBBD86_FD24_4415_8EDB_531559D663F2").val();
            if ((thutuc == '') || (thutuc == null) || (thutuc == 'undefined') || (thutuc == '-1') || lenopts == 0) {
                alert("Chọn một thủ tục kiểm toán.");
                return false;
            }

            if ((_documentID == '') || (_documentID == null) || (_documentID == 'undefined')) {
                alert(MSG_ALERT_CHOICE_CV);
                return false;
            }

            if (!window.confirm("Bạn muốn thêm thủ tục kiểm toán này vào công việc?"))
                return false;

            var url = "ChuongTrinhKiemToan_Load.aspx";
            var query = "act=themthutuc&doc=" + _documentID + "&thutuc=" + thutuc + "&dotkt=" + dotkt;
            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (fullname) {
                    FinishProcessingForm();
                    if (fullname == "2") {
                        //alert(MSG_ADD_TT_SUC);
                        //var val_ddl = $("#ctl00_FormContent_ddlThuTuc").val();
                        //$("#ctl00_FormContent_ddlThuTuc option[value='" + val_ddl + "']").remove();
                        $('#<%=idCongViec.ClientID %>').val(_documentID);
                        $('#<%=idThanhVien.ClientID %>').val(thanhvien);
                        __doPostBack('<%=updatepanel1.ClientID %>', '');
                    }
                    else if (fullname == "1") {
                        alert(MSG_EXIST_TT);
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
            var url = "ChuongTrinhKiemToan_Load.aspx";
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

        

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ButtonExtendBefore" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ButtonExtend" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FormFooter" runat="server">
</asp:Content>
