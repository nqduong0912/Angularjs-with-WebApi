﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true"
    CodeBehind="DanhSachPhatHien.aspx.cs" Inherits="VPB_TDHS.Modules.DMS.DanhSachPhatHien" %>

<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <asp:ScriptManager runat="server" ID="ScriptManager1">
    </asp:ScriptManager>
    <div class="form-horizontal" id="tblTruongDoan">
        Danh sách phát hiện
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
    </div>
    <%--<table width="100%" id="tblTruongDoan">
        <tr class="GridHeader">
            <td colspan="2">Danh sách phát hiện
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
    </table>--%>
    <asp:ObjectDataSource runat="server" ID="ObjectDataSource1" SelectMethod="getListPhatHien"
        TypeName="VPB_KTNB.Helpers.DataSource">
        <SelectParameters>
            <asp:Parameter Name="congviecID" Type="string" />
            <asp:Parameter Name="cv" Type="string" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <table width="100%" id="tblThanhVien">
        <tr>
            <td colspan="2">
                <asp:HiddenField ID="idCongViec" runat="server" />
                <asp:HiddenField ID="hiddenIsCotheThemPhatHien" runat="server" />
                <C1WebGrid:C1WebGrid runat="server" ID="dataCtrl" Width="75%" OnItemCreated="dataCtrl_OnItemCreated"
                    OnItemDataBound="dataCtrl_OnItemDataBound" GroupIndent="20" DataSourceID="ObjectDataSource1"
                    ItemStyle-Height="30px">
                    <Columns>
                        <C1WebGrid:C1TemplateColumn>
                            <ItemTemplate>
                                <asp:Image runat="server" ID="imgDelete" ImageUrl="~/Images/delete.gif" ToolTip="Xóa"
                                    Style="cursor: pointer" />
                            </ItemTemplate>
                            <ItemStyle Width="8%" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1TemplateColumn HeaderText="Cập nhật">
                            <ItemTemplate>
                                <asp:Image runat="server" ID="ImgEdit" ImageUrl="~/Images/viewdetail.gif" ToolTip="Chi tiết phát hiện"
                                    Style="cursor: pointer" />
                            </ItemTemplate>
                            <ItemStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1TemplateColumn HeaderText="Loại Phát hiện">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblLoaiPhatHienText" Text='<%# DataBinder.Eval(Container.DataItem,"phathienid")%>'></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1TemplateColumn HeaderText="Trạng thái">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="Status" Text='<%# DataBinder.Eval(Container.DataItem,"STATUS")%>'></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1TemplateColumn Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="phathienid" Text='<%# DataBinder.Eval(Container.DataItem,"phathienid")%>'></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1TemplateColumn Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblLoaiPhatHien" Text='<%# DataBinder.Eval(Container.DataItem,"loai")%>'></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Phát hiện" DataField="phat_hien">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Mức độ" DataField="muc_do">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Chi tiết" DataField="chi_tiet">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Nguyên nhân" DataField="nguyen_nhan">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Ảnh hưởng" DataField="anh_huong">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Khuyến nghị" DataField="khuyen_nghi">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Ghi chú" DataField="ghi_chu">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1BoundColumn Visible="false" HeaderText="Nhận xét" DataField="nhan_xet">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1TemplateColumn HeaderText="Phản hồi của ĐV">
                            <ItemTemplate>
                                <asp:Label runat="server" CssClass="gachchan" ID="lblTongSoPhanHoi"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1TemplateColumn HeaderText="Nhận xét">
                            <ItemTemplate>
                                <asp:Image runat="server" ID="imgNhanXet" ImageUrl="~/Images/nhanxet2.png" ToolTip=""
                                    Style="cursor: pointer" />
                            </ItemTemplate>
                            <ItemStyle Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </C1WebGrid:C1TemplateColumn>
                    </Columns>
                </C1WebGrid:C1WebGrid>
            </td>
        </tr>
    </table>
    <table runat="server" id="tbThemPhatHien">
        <tr>
            <td>
                <input type="button" value="Thêm phát hiện hệ thống" onclick="NewDocumentHeThong();"
                    id="btn-themphathienhethong" />
                <input type="button" value="Thêm phát hiện vi phạm" onclick="NewDocumentViPham();"
                    id="btn-themphathienvipham" />
            </td>
        </tr>
    </table>

    <style>
        .gachchan {
            text-decoration: underline;
            cursor: pointer;
            font-weight: bold;
        }
    </style>
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
        var MSG_CONFIRM_SUBMIT = "Bạn có muốn cập nhật?";
        var MSG_CANNOT_SUBMIT = "Không submit được vì chưa có thủ tục nào";
        var MSG_CONFIRM_DEL_TT = "Bạn có muốn xóa phát hiện này?";
        var congviecID = Qry["doc"];
        var cv = Qry["cv"];
        var doankt = Qry["doankt"];
        var dotkt = Qry["dotkt"];

        $(document).ready(function () {
            HiddenButton();

            //            var isCotheThem = "<%= hiddenIsCotheThemPhatHien.Value %>"
            //            if (isCotheThem == 'False') {
            //                //disable button Them Phat Hien
            //                $('#btn-themphathienhethong').attr('disabled', 'disabled');
            //                $('#btn-themphathienvipham').attr('disabled', 'disabled');
            //            }
        });

        function HiddenButton() {
            var timkiem = Qry["timkiem"];
            if (timkiem == '1') {
                $("input[type$='button']").hide();
                $("input[type$='submit']").hide();
                $("img[id*='ImgEdit']").hide();
                $("img[id*='imgDelete']").hide();
            }
            if (cv != "nguoithuchien") {
                $("input[type$='button']").hide();
                $("input[type$='submit']").hide();
            }
        }

        function CapNhatTrangThai() {
            if (!window.confirm(MSG_CONFIRM_SUBMIT))
                return false;
            var url = "CongViecDuocPhuTrach_Load.aspx";
            var query = "act=capnhattrangthai";
            var trangthai = GetSvrCtlValue("ID8_A0979435_7846_4421_97FE_4A54CF890381");
            query += "&doc=" + _documentID + "&trangthai=" + trangthai;

            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (message) {
                    FinishProcessingForm();
                    //                //submit thanh cong
                    //                if (message == '1') {

                    //                    $('#' + clientIDbtnSave).hide();
                    //                }
                    //                //submit khong thanh cong
                    //                else if (message == '0') {
                    //                    alert(MSG_CANNOT_SUBMIT);
                    //                }

                }
            });
        }
        function xoaPhatHien(phathienid) {
            if (!window.confirm(MSG_CONFIRM_DEL_TT))
                return false;
            var url = "DanhSachPhatHien.aspx";
            var query = "act=xoaphathien";
            query += "&doc=" + congviecID + "&phathienid=" + phathienid;
            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (errormessage) {
                    FinishProcessingForm();
                    if (errormessage == "2") {
                        url = "DanhSachPhatHien.aspx?act=loaddoc&doc=" + congviecID + "&cv=" + cv + "&doankt=" + doankt + "&dotkt=" + dotkt;
                        window.location.href = url;
                    }
                    else
                        alert(errormessage);
                }
            });

        }
        function LoadPagePhanHoiDonVi(phathienid) {

            if (cv == 'nguoithuchien')
                url = "PhanHoiDonVi.aspx?act=loaddoc&doc=" + phathienid + "&cv=" + cv + "&congviec_docid=" + congviecID + "&doankt=" + doankt + "&dotkt=" + dotkt;
            else
                url = "PhanHoiDonVi.aspx?act=loaddoc&doc=" + phathienid + "&cv=" + cv + "&timkiem=1" + "&congviec_docid=" + congviecID + "&doankt=" + doankt + "&dotkt=" + dotkt;
            window.location.href = url;
        }
        function NewDocumentHeThong() {
            url = "PhatHienHeThong_Load.aspx?act=loaddoc&congviec_docid=" + congviecID + "&cv=" + cv + "&doankt=" + doankt + "&dotkt=" + dotkt;
            window.location.href = url;
        }
        function LoadDocumentHeThong(phathienid) {
            url = "PhatHienHeThong_Load.aspx?doc=" + phathienid + "&act=loaddoc&congviec_docid=" + congviecID + "&cv=" + cv + "&doankt=" + doankt + "&dotkt=" + dotkt;
            window.location.href = url;
        }
        function LoadDocumentViPham(phathienid) {
            url = "PhatHienViPham_Load.aspx?doc=" + phathienid + "&act=loaddoc&congviec_docid=" + congviecID + "&cv=" + cv + "&doankt=" + doankt + "&dotkt=" + dotkt;
            window.location.href = url;
        }
        function NewDocumentViPham() {
            url = "PhatHienViPham_Load.aspx?act=loaddoc&congviec_docid=" + congviecID + "&cv=" + cv + "&doankt=" + doankt + "&dotkt=" + dotkt;
            window.location.href = url;
        }
        function showPos(event) {
            var el, x, y;
            el = document.getElementById('divTreeView');
            if (window.event) {
                x = window.event.clientX + document.documentElement.scrollLeft
+ document.body.scrollLeft;
                y = window.event.clientY + document.documentElement.scrollTop +
+document.body.scrollTop;
            }
            else {
                x = event.clientX + window.scrollX;
                y = event.clientY + window.scrollY;
            }
            x -= 2; y -= 2;
            y = y + 15
            el.style.left = x + "px";
            el.style.top = y + "px";
            el.style.display = "block";
            //document.getElementById('PopUpText').innerHTML = text;
            el.show();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ButtonExtendBefore" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ButtonExtend" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FormFooter" runat="server">
</asp:Content>
