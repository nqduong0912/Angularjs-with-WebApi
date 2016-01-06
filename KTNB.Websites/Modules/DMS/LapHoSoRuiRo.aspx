<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true"
    CodeBehind="LapHoSoRuiRo.aspx.cs" Inherits="VPB_TDHS.Modules.DMS.LapHoSoRuiRo" %>

<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <asp:HiddenField ID="sortDirection" Value="asc" runat="server" />
    <asp:ScriptManager runat="server" ID="ScriptManager1">
    </asp:ScriptManager>
    <asp:ObjectDataSource runat="server" ID="ObjectDataSource1" SelectMethod="getListHoSoPhanTichChiTiet"
        TypeName="VPB_KTNB.Helpers.DataSource">
        <SelectParameters>
            <asp:Parameter Name="NhomKiemToan" Type="string" />
            <asp:Parameter Name="sort" Type="string" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <div class="form-horizontal">
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Đối tượng kiểm toán</label>
            <div class="col-sm-6">
                <fieldset disabled>
                    <asp:TextBox ID="txtDoiTuongKiemToan" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                </fieldset>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Đợt kiểm toán</label>
            <div class="col-sm-6">
                <fieldset disabled>
                    <asp:TextBox ID="txtDotKiemToan" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                </fieldset>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Số rủi ro đã được đánh giá</label>
            <div class="col-sm-6">
                <fieldset disabled>
                    <asp:TextBox ID="txtSoRuiRoDaDanhGia" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                </fieldset>
            </div>
        </div>
    </div>
    <%--    <table width="100%">
        <tr>
            <td style="width: 222px">Đối tượng kiểm toán
            </td>
            <td>
                <asp:TextBox ID="txtDoiTuongKiemToan" runat="server" SkinID="TextBoxReadOnly"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Đợt kiểm toán
            </td>
            <td>
                <asp:TextBox ID="txtDotKiemToan" runat="server" SkinID="TextBoxReadOnly"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Số rủi ro đã được đánh giá
            </td>
            <td>
                <asp:TextBox ID="txtSoRuiRoDaDanhGia" runat="server" SkinID="TextBoxReadOnly"></asp:TextBox>
            </td>
        </tr>
    </table>--%>
    <table width="100%">
        <tr class="GridHeader">
            <td>Các phân tích sơ bộ
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel runat="server" ID="updatepanel2" UpdateMode="Conditional">
                    <ContentTemplate>
                        <C1WebGrid:C1WebGrid runat="server" ID="GridVanDeQuanTam" Width="100%" OnItemCreated="dataCtrl_OnItemCreated"
                            OnItemDataBound="GridVanDeQuanTam_OnItemDataBound" GroupIndent="20" ItemStyle-Height="30px">
                            <Columns>

                                <C1WebGrid:C1TemplateColumn Visible="false">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="mangnv" Text='<%# DataBinder.Eval(Container.DataItem,"mangnv")%>'></asp:Label>
                                        <asp:HiddenField runat="server" ID="hiddenTenMangNghiepVu" Value='<%# DataBinder.Eval(Container.DataItem,"ten_mang_nghiep_vu")%>' />
                                    </ItemTemplate>
                                </C1WebGrid:C1TemplateColumn>
                                <C1WebGrid:C1BoundColumn HeaderText="Mảng nghiệp vụ" DataField="ten_mang_nghiep_vu">
                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                </C1WebGrid:C1BoundColumn>
                                <C1WebGrid:C1TemplateColumn HeaderText="Vấn đề quan tâm">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblVanDeQuanTam" Text='' />
                                    </ItemTemplate>
                                    <ItemStyle Width="50%" HorizontalAlign="Left" VerticalAlign="Top" />
                                </C1WebGrid:C1TemplateColumn>
                                <C1WebGrid:C1TemplateColumn HeaderText="Submitted">
                                    <ItemTemplate>
                                        <asp:Image runat="server" ID="imgSubmit" ImageUrl="~/Images/check.gif" ToolTip="Đã Submit" />
                                    </ItemTemplate>
                                    <ItemStyle Width="8%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                </C1WebGrid:C1TemplateColumn>
                            </Columns>
                        </C1WebGrid:C1WebGrid>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr class="GridHeader">
            <td>Các hồ sơ rủi ro
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel runat="server" ID="updatepanel1" UpdateMode="Conditional">
                    <ContentTemplate>
                        <C1WebGrid:C1WebGrid runat="server" ID="dataCtrl" Width="100%" OnSortingCommand="dataCtrl_OnSorting" DataSourceID="ObjectDataSource1" OnItemCreated="dataCtrl_OnItemCreated"
                            OnItemDataBound="dataCtrl_OnItemDataBound" GroupIndent="20" ItemStyle-Height="30px">
                            <Columns>
                                <%--<C1WebGrid:C1TemplateColumn>
                                    <ItemTemplate>
                                        <asp:Image runat="server" ID="imgDelete" ImageUrl="~/Images/delete.gif" ToolTip="Xoá vấn đề quan tâm"
                                            Style="cursor: pointer" />
                                    </ItemTemplate>
                                    <ItemStyle Width="8%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                </C1WebGrid:C1TemplateColumn>--%>
                                <C1WebGrid:C1TemplateColumn Visible="false">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="hoso" Text='<%# DataBinder.Eval(Container.DataItem,"hoso")%>'></asp:Label>
                                    </ItemTemplate>
                                </C1WebGrid:C1TemplateColumn>
                                <C1WebGrid:C1BoundColumn SortExpression="mang_nghiep_vu" HeaderText="Mảng nghiệp vụ" DataField="mang_nghiep_vu">
                                    <ItemStyle Width="10%" VerticalAlign="Top" HorizontalAlign="Left" />
                                </C1WebGrid:C1BoundColumn>
                                <C1WebGrid:C1BoundColumn HeaderText="Mục tiêu kiểm soát" DataField="muc_tieu_kiem_soat">
                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                </C1WebGrid:C1BoundColumn>
                                <C1WebGrid:C1BoundColumn SortExpression="rui_ro" HeaderText="Rủi ro" DataField="rui_ro">
                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                </C1WebGrid:C1BoundColumn>
                                <%--<C1WebGrid:C1TemplateColumn Visible="true" HeaderText="Kiểm soát">
                                    <ItemTemplate>
                                        <asp:TreeView runat="server" ID="treeViewKiemSoat" ExpandDepth="0">
                                        </asp:TreeView>
                                    </ItemTemplate>
                                    <ItemStyle Width="15%" HorizontalAlign="Left" VerticalAlign="Top" />
                                </C1WebGrid:C1TemplateColumn>--%>

                                <C1WebGrid:C1TemplateColumn Visible="true" HeaderText="Kiểm soát/Thủ tục">
                                    <ItemTemplate>
                                        <asp:TreeView runat="server" ID="treeViewThuTuc" ExpandDepth="0" Width="350px">
                                        </asp:TreeView>
                                    </ItemTemplate>
                                    <ItemStyle Width="15%" HorizontalAlign="Left" VerticalAlign="Top" />
                                </C1WebGrid:C1TemplateColumn>

                                <C1WebGrid:C1TemplateColumn HeaderText="Xác xuất">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCommandXacSuat" Text='<%# DataBinder.Eval(Container.DataItem,"xac_suat")%>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Top" />
                                </C1WebGrid:C1TemplateColumn>
                                <C1WebGrid:C1TemplateColumn HeaderText="Ảnh hưởng">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCommandAnhHuong" Text='<%# DataBinder.Eval(Container.DataItem,"anh_huong")%>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Top" />
                                </C1WebGrid:C1TemplateColumn>
                                <C1WebGrid:C1TemplateColumn HeaderText="RR cố hữu">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCommandRRCoHuu" Style="cursor: pointer; text-decoration: underline" />
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Top" />
                                </C1WebGrid:C1TemplateColumn>
                                <C1WebGrid:C1TemplateColumn HeaderText="Submitted">
                                    <ItemTemplate>
                                        <asp:Image runat="server" ID="imgSubmit" ImageUrl="~/Images/check.gif" ToolTip="Đã Submit" />
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                </C1WebGrid:C1TemplateColumn>
                                <C1WebGrid:C1TemplateColumn Visible="false" HeaderText="">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblStatus" Text='<%# DataBinder.Eval(Container.DataItem,"STATUS")%>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Top" />
                                </C1WebGrid:C1TemplateColumn>
                            </Columns>
                        </C1WebGrid:C1WebGrid>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        var _documentid;
        var nhomkt = '<%=_nhomkt%>';
        var MSG_CONFIRM_SUBMIT_HOSOSOBO = "Bạn có muốn submit toàn bộ việc phân tích chi tiết?";
        /*********************************************************/
        $(document).ready(function () {
            //do smt here
        });
        /*********************************************************/
        function ThemVDQuanTam(doctypeID) {
            var _chitietHoSo = NewGuid();

            savedocumentwithlink(_chitietHoSo, nhomkt, doctypeID, savedoc_success, savedoc_error, "")
        }
        function Submit() {
            if (!window.confirm(MSG_CONFIRM_SUBMIT_HOSOSOBO))
                return false;
            var url = "LapHoSoRuiRo.aspx";
            var query = "act=submit";
            query += "&doc=" + nhomkt;
            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (message) {
                    FinishProcessingForm();
                    if (message == 'notsubmit')
                        alert('Không submit thành công vì chưa đánh giá xác suất & ảnh hưởng');
                    else {
                        alert('Submit thành công');
                        var url = "LapHoSoRuiRo.aspx?act=loaddoc&doc=" + Qry["doc"];
                        window.location.href = url;
                    }
                }
            });
        }
        function LoadDocument(hosoRuiRoID, type) {
            url = "DanhGiaXacSuat.aspx?nhomkt=" + nhomkt + "&type=" + type + "&act=loaddoc&doc=" + hosoRuiRoID;
            window.location.href = url;
        }
        function newform() {
            //            window.location.href = "ChiTietHoSoPhanTichSoBo_Load.aspx";
        }
        function savedoc_success() {
            alert(MSG_ADD_OK);

            __doPostBack('<%=updatepanel1.ClientID %>', '');
            //        window.location.href = 'LoaiDoiTuongKiemToan.aspx';
        }
        function savedoc_error() {
            alert(MSG_ADD_ER);
        }
        function update_success() {
            alert(MSG_EDIT_OK);
            //        window.location.href = 'LoaiDoiTuongKiemToan.aspx';
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
        function deleteChiTietPhanTich(hosoChiTiet) {
            var nhomkt = '<%=_nhomkt%>';
            var url = "LapHoSoPhanTichSoBo.aspx";
            var query = "act=xoachitietphantich";
            query += "&doc=" + nhomkt;
            query += "&hosochitiet=" + hosoChiTiet;
            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (errormessage) {
                    FinishProcessingForm();
                    if (errormessage == "") {
                        window.location.reload(true);
                    }
                    else
                        alert(errormessage);
                }
            });

        }
    </script>
    <style>
        ul {
            list-style-type: square;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ButtonExtendBefore" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ButtonExtend" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FormFooter" runat="server">
</asp:Content>
