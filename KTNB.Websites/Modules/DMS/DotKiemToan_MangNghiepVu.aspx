<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true"
    CodeBehind="DotKiemToan_MangNghiepVu.aspx.cs" Inherits="VPB_TDHS.Modules.DMS.DotKiemToan_MangNghiepVu" %>

<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <table width="100%" id="tblTruongDoan">
        <tr class="GridHeader">
            <td colspan="2">
                Trưởng đoàn kiểm toán
            </td>
        </tr>
        <tr>
            <td style="width: 222px">
                Tên trưởng đoàn
            </td>
            <td>
                <asp:Label ID="DOCNAME" runat="server" />
            </td>
        </tr>
        <tr id="tr-trangthai" style="display: none;">
            <td>
                Trạng thái
            </td>
            <td id="td-trangthai">
                Chưa Submit
            </td>
        </tr>
    </table>
    <asp:ObjectDataSource runat="server" ID="ObjectDataSource1" SelectMethod="getDocumentList"
        TypeName="VPB_KTNB.Helpers.DataSource">
        <SelectParameters>
            <asp:Parameter Name="DocumentTypeID" Type="String" />
            <asp:Parameter Name="DocFields" Type="String" />
            <asp:Parameter Name="PropertyFields" Type="String" />
            <asp:Parameter Name="Condition" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <table width="70%">
        <tr>
            <td>
                <C1WebGrid:C1WebGrid runat="server" ID="GridPhamViMangNghiepVu" DataSourceID="ObjectDataSource1"
                    Width="100%" OnItemDataBound="GridPhamViMangNghiepVu_OnItemDataBound" GroupIndent="20"
                    ItemStyle-Height="30px">
                    <Columns>
                        <%-- <C1WebGrid:C1TemplateColumn>
                            <ItemTemplate>
                                <asp:Image runat="server" ID="imgEdit" ImageUrl="~/Images/viewdetail.gif" ToolTip="Chi tiết"
                                    Style="cursor: pointer" />
                            </ItemTemplate>
                            <ItemStyle Width="8%" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </C1WebGrid:C1TemplateColumn>--%>
                        <C1WebGrid:C1TemplateColumn Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="PK_DocumentID" Text='<%# DataBinder.Eval(Container.DataItem,"PK_DocumentID")%>'></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Mảng nghiệp vụ" DataField="Tên mảng nghiệp vụ">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1TemplateColumn>
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="chkMangNghiepVu" />
                            </ItemTemplate>
                            <ItemStyle Width="8%" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </C1WebGrid:C1TemplateColumn>
                    </Columns>
                </C1WebGrid:C1WebGrid>
            </td>
        </tr>
    </table>
    <%--<input id="btn-add-mangnghiepvu" type="button" value="Save" onclick="SaveMangNghiepVu()"
        class="InsertButton" />--%>
    <script type="text/javascript">
        /*********************************************************/
        var MSG_CONFIRM_ADD_TRUONGDOAN = "Bạn có muốn đưa thành viên này làm trưởng đoàn?";
        var MSG_ALERT_CHOICE_TRUONGDOAN = "Hãy chọn một thành viên làm trưởng đoàn kiểm toán.";

        var MSG_CONFIRM_ADD_THANHVIEN = "Bạn có muốn đưa thành viên này vào đoàn kiểm toán không?";
        var MSG_EXIST_THANHVIEN = "Thành viên này đã tồn tại trong đoàn kiểm toán này.";
        var MSG_ADD_THANHVIEN_SUC = "Đưa thành viên vào đoàn kiểm toán thành công.";
        var MSG_CONFIRM_DEL_THANHVIEN = "Bạn có muốn đưa thành viên này ra khỏi đoàn kiểm toán?";
        var MSG_UPDATE_MANGNGHIEPVU = "Cập nhật mảng nghiệp vụ thành công";

        var ctl_username;
        var ctl_username_thanhvien;
        $(document).ready(function () {
            initCtlEvents();
            HiddenControlTimKiem();
        });

        function HiddenControlTimKiem() {
            var timkiem = Qry["timkiem"];
            if (timkiem == 'tk') {
                $("img[id*='imgDelete']").hide();
                $("img[id*='imgEdit']").hide();
                $("input[type*='button']").hide();
                $("input[type*='submit']").hide();
            }
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
            $("#FullName").attr("innerHTML", "");
            var url = "DoanKiemToan_Load.aspx";
            var query = "act=getfullname&user=" + username + "&dotkt=" + dotkt; ;
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
                        $("#" + ctl_username_thanhvien).focus();
                    }
                }
            });
        }



        function SaveMangNghiepVu() {
           
            var dsmangnghiepvu = [];
            var strMangNghiepVu = '';
            $('[id*="chkMangNghiepVu"]').each(function () {
                //                if (this.checked && $(this).attr('disabled') == false) {
                //                    dsmangnghiepvu.push($(this).parent().attr('mangnghiepvuid'));
                //                }
                if (this.checked) {
                    dsmangnghiepvu.push($(this).parent().attr('mangnghiepvuid'));
                }
            })
            if (dsmangnghiepvu.length == 0) {
                alert("Phải chọn ít nhất một mảng nghiệp vụ.");
                return false;
            }

            dsmangnghiepvu.forEach(function (item) {
                strMangNghiepVu += item + '__';
            });
            var url = "DotKiemToan_MangNghiepVu.aspx";
            var doan = Qry["doc"];
            var dotkt = Qry["dotkt"];
            var query = "doc=" + doan + "&act=mappingmangnghiepvu&dsmangnghiepvu=" + strMangNghiepVu + "&dotkt=" + dotkt;
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (data) {
                    //                    if (fullname == "2") {
                    //                        alert(MSG_ADD_THANHVIEN_SUC);


                    //                    }
                    //                    else if (fullname == "1") {
                    //                        alert(MSG_EXIST_THANHVIEN);
                    //                    }
                    alert(MSG_UPDATE_MANGNGHIEPVU);
                    window.location.href = "DoanKiemToan.aspx?dotkt=" + Qry["dotkt"];
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
