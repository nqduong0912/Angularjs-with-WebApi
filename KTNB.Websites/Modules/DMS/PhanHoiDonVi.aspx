<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true"
    CodeBehind="PhanHoiDonVi.aspx.cs" Inherits="VPB_TDHS.Modules.DMS.PhanHoiDonVi" %>

<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <asp:ScriptManager runat="server" ID="ScriptManager1">
    </asp:ScriptManager>
    <div class="form-horizontal" id="tblTruongDoan">
        Danh sách phản hồi
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
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Phát hiện</label>
            <div class="col-sm-6">
                <asp:TextBox ID="txtPhatHien" runat="server" CssClass="form-control" Enabled="False" Width="300px"></asp:TextBox>
            </div>
        </div>
    </div>
    <%--<table width="100%" id="tblTruongDoan">
        <tr class="GridHeader">
            <td colspan="2">Danh sách phản hồi
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
                <asp:TextBox Enabled="false" ID="txtPhatHien" runat="server" SkinID="TextBoxReadOnly"></asp:TextBox>
            </td>
        </tr>
    </table>--%>
    <asp:ObjectDataSource runat="server" ID="ObjectDataSource1" SelectMethod="getListPhanHoi"
        TypeName="VPB_KTNB.Helpers.DataSource">
        <SelectParameters>
            <asp:Parameter Name="phatHienID" Type="string" />
            <asp:Parameter Name="UserType" Type="string" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <table width="100%" id="tblThanhVien">
        <tr>
            <td colspan="2">
                <asp:HiddenField ID="idCongViec" runat="server" />
                <C1WebGrid:C1WebGrid runat="server" DataSourceID="ObjectDataSource1" ID="dataCtrl"
                    Width="75%" OnItemCreated="dataCtrl_OnItemCreated" OnItemDataBound="dataCtrl_OnItemDataBound"
                    GroupIndent="20" ItemStyle-Height="30px">
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
                                <asp:Image runat="server" ID="ImgEdit" ImageUrl="~/Images/viewdetail.gif" ToolTip="Chi tiết phản hồi"
                                    Style="cursor: pointer" />
                            </ItemTemplate>
                            <ItemStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1TemplateColumn Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="phanhoiid" Text='<%# DataBinder.Eval(Container.DataItem,"phanhoiid")%>'></asp:Label>
                                <asp:Label runat="server" ID="lblNguoiNhap" Text='<%# DataBinder.Eval(Container.DataItem,"nguoi_nhap")%>'></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Ngày giờ" DataField="ngay_gio">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="15%" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1TemplateColumn HeaderText="Trạng thái">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="Status" Text='<%# DataBinder.Eval(Container.DataItem,"STATUS")%>'></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Người nhập" DataField="nguoi_nhap">
                            <ItemStyle VerticalAlign="Top" Width="15%" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1TemplateColumn HeaderText="Vai trò" Visible="true">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblRole"></asp:Label>

                            </ItemTemplate>
                            <ItemStyle VerticalAlign="Top" Width="15%" HorizontalAlign="Left" />
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Nội Dung" DataField="NoiDung">
                            <ItemStyle VerticalAlign="Top" Width="50%" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>

                    </Columns>
                </C1WebGrid:C1WebGrid>
            </td>
        </tr>
    </table>
    <input type="button" value="Thêm thông tin phản hồi" onclick="NewDocumentPhanHoi();"
        id="btn-themphathien" />
    <style>
        .gachchan {
            text-decoration: underline;
            cursor: pointer;
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
        var phathienid = Qry["doc"];
        var cv = Qry["cv"];
        var dotkt = Qry["dotkt"];
        var doankt = Qry["doankt"];
        var congviec_docid = Qry["congviec_docid"];
        var role = Qry["role"];
        //thangma

        //var imgTreeView = imgTreeView.ClientID;


        $(document).ready(function () {
            HiddenButtonByCongViec();
            var isCotheThem = "<%= _IsCoTheThemPhanHoi %>"
            if (isCotheThem == 'False') {
                //disable button Thêm Phản hồi
                $('#btn-themphathien').attr('disabled', 'disabled');
            }
        });

        function HiddenButton() {
            var timkiem = Qry["timkiem"];
            if (timkiem == '1') {
                $("input[type$='button']").hide();
                $("input[type$='submit']").hide();
                $("img[id*='ImgEdit']").hide();
                $("img[id*='imgDelete']").hide();
            }
        }

        function HiddenButtonByCongViec() {
            if (cv != 'nguoithuchien') {
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
            query += "&doc=" + _documentID + "&trangthai=" + trangthai + "&doankt=" + doankt + "&dotkt=" + dotkt;

            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (message) {
                    FinishProcessingForm();
                }
            });
        }
        function xoaPhanHoi(phanhoiid) {
            if (!window.confirm(MSG_CONFIRM_DEL_TT))
                return false;
            var url = "PhanHoiDonVi.aspx";
            var query = "act=xoaphanhoi";
            query += "&doc=" + phathienid + "&phanhoiid=" + phanhoiid;
            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (errormessage) {
                    FinishProcessingForm();
                    if (errormessage == "2") {
                        url = "PhanHoiDonVi.aspx?act=loaddoc&doc=" + phathienid + "&cv=" + cv + "&doankt=" + doankt + "&dotkt=" + dotkt;
                        window.location.href = url;
                    }
                    else
                        alert(errormessage);
                }
            });

        }
        function LoadPagePhanHoiDonVi(phathienid) {
            url = "PhanHoiDonVi.aspx?act=loaddoc&doc=" + phathienid + "&cv=" + cv + "&congviec_docid=" + congviec_docid + "&doankt=" + doankt + "&dotkt=" + dotkt;
            window.location.href = url;
        }
        function NewDocumentPhanHoi() {
            url = "PhanHoiDonVi_Load.aspx?phathienid=" + phathienid + "&cv=" + cv + "&congviec_docid=" + congviec_docid + "&doankt=" + doankt + "&dotkt=" + dotkt + "&role=" + role;
            window.location.href = url;
        }
        function LoadDocumentPhanHoi(phanhoiid) {
            url = "PhanHoiDonVi_Load.aspx?doc=" + phanhoiid + "&act=loaddoc" + "&cv=" + cv + "&phathienid=" + phathienid + "&congviec_docid=" + congviec_docid + "&doankt=" + doankt + "&dotkt=" + dotkt + "&role=" + role;
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
