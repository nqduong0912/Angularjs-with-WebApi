<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true"
    CodeBehind="DanhGiaRuiRoConLai.aspx.cs" Inherits="VPB_TDHS.Modules.DMS.DanhGiaRuiRoConLai" %>

<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <asp:HiddenField ID="sortDirection" Value="asc" runat="server" />

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
                 <asp:TextBox ID="txtDoiTuongKiemToan" runat="server" CssClass="form-control" Enabled="False" Width="300px"></asp:TextBox>
             </div>
         </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Đợt kiểm toán</label>
            <div class="col-sm-6">
                <asp:TextBox ID="txtDotKiemToan" runat="server" CssClass="form-control" Enabled="False" Width="300px"></asp:TextBox>
            </div>
        </div>
    </div>
    <%--<table width="100%">
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
    </table>--%>
    <table width="100%">
        <tr class="GridHeader">
            <td>Các hồ sơ rủi ro
            </td>
        </tr>
        <tr>
            <td>

                <contenttemplate>
                        <C1WebGrid:C1WebGrid OnSortingCommand="dataCtrl_OnSorting" DataSourceID="ObjectDataSource1" runat="server" ID="dataCtrl" Width="100%" OnItemCreated="dataCtrl_OnItemCreated"
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
                                <C1WebGrid:C1BoundColumn HeaderText="Mảng nghiệp vụ" DataField="mang_nghiep_vu">
                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                </C1WebGrid:C1BoundColumn>
                                <C1WebGrid:C1BoundColumn HeaderText="Mục tiêu kiểm soát" DataField="muc_tieu_kiem_soat">
                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                </C1WebGrid:C1BoundColumn>
                                <C1WebGrid:C1BoundColumn HeaderText="Rủi ro" DataField="rui_ro">
                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                </C1WebGrid:C1BoundColumn>
                                <C1WebGrid:C1TemplateColumn Visible="false" HeaderText="Xác xuất">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblRuiRoID" Text='<%# DataBinder.Eval(Container.DataItem,"ruiroid")%>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Top" />
                                </C1WebGrid:C1TemplateColumn>
                                <%--<C1WebGrid:C1TemplateColumn Visible="true" HeaderText="Kiểm soát">
                                    <ItemTemplate>
                                        <asp:TreeView runat="server" ID="treeViewKiemSoat" ExpandDepth="0">
                                        </asp:TreeView>
                                    </ItemTemplate>
                                    <ItemStyle Width="15%" HorizontalAlign="Left" VerticalAlign="Top" />
                                </C1WebGrid:C1TemplateColumn>--%>
                                <C1WebGrid:C1TemplateColumn Visible="true" HeaderText="Kiểm soát/Thủ tục">
                                    <ItemTemplate>
                                        <asp:TreeView runat="server" ID="treeViewThuTuc" ExpandDepth="0" Width="300px">
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
                                <C1WebGrid:C1TemplateColumn HeaderText="Điểm kiểm soát">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblDiemKiemSoat" Text='<%# DataBinder.Eval(Container.DataItem,"rui_ro_con_lai")%>'
                                            Style="cursor: pointer; text-decoration: underline" />
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Top" />
                                </C1WebGrid:C1TemplateColumn>
                                <C1WebGrid:C1TemplateColumn HeaderText="RR còn lại">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblRRCL" Style="cursor: pointer; text-decoration: underline" />
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Top" />
                                </C1WebGrid:C1TemplateColumn>
                                <C1WebGrid:C1TemplateColumn Visible="false" HeaderText="Submitted">
                                    <ItemTemplate>
                                        <asp:Image runat="server" ID="imgSubmit" ImageUrl="~/Images/check.gif" ToolTip="Đã Submit" />
                                    </ItemTemplate>
                                    <ItemStyle Width="8%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                </C1WebGrid:C1TemplateColumn>
                                <C1WebGrid:C1TemplateColumn Visible="false" HeaderText="">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblStatus" Text='<%# DataBinder.Eval(Container.DataItem,"STATUS")%>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Top" />
                                </C1WebGrid:C1TemplateColumn>
                            </Columns>
                        </C1WebGrid:C1WebGrid>
                    </contenttemplate>

            </td>
        </tr>
    </table>
    <script type="text/javascript">
        var _documentid;
        var dotkt = Qry["dotkt"];
        if (!dotkt)
            dotkt = '<%=_dotkt%>';
        var doankt = Qry["doankt"];
        var nhomkt = '<%=_nhomkt%>';
        var MSG_CONFIRM_SUBMIT_HOSOSOBO = "Bạn có muốn submit toàn bộ việc phân tích sơ bộ?";
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
                success: function (ErrorMessage) {
                    FinishProcessingForm();
                    var url = "LapHoSoRuiRo.aspx?act=loaddoc&doc=" + Qry["doc"];
                    window.location.href = url;
                }
            });
        }
        function LoadDocument(hosoRuiRoID, type) {
            url = "DanhGiaXacSuat.aspx?nhomkt=" + nhomkt + "&type=" + type + "&act=loaddoc&doc=" + hosoRuiRoID;
            window.location.href = url;
        }
        function LoadTinhDiemKiemSoat(hosoRuiRoID, ruiroid) {

            url = "TinhDiemKiemSoat.aspx?hosoruiroid=" + hosoRuiRoID + "&ruiroid=" + ruiroid + "&dotkt=" + dotkt + "&nhomkt=" + nhomkt;
            window.location.href = url;
        }
        function newform() {
            //            window.location.href = "ChiTietHoSoPhanTichSoBo_Load.aspx";
        }
        function savedoc_success() {
            alert(MSG_ADD_OK);
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
