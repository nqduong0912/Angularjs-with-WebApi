<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true"
    CodeBehind="DSDoanKiemToanDuocPhuTrach.aspx.cs" Inherits="VPB_TDHS.Modules.DMS.DSDoanKiemToanDuocPhuTrach" %>

<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
<asp:ObjectDataSource runat="server" ID="ObjectDataSource1" SelectMethod="getList"
    TypeName="VPB_KTNB.Helpers.DataSource">
    <SelectParameters>
        <asp:Parameter Name="Status" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>


    <table width="100%">
        <tr>
            <td>
                <C1WebGrid:C1WebGrid runat="server" ID="dataCtrl" Width="100%" OnItemCreated="dataCtrl_OnItemCreated"
                    OnItemDataBound="dataCtrl_OnItemDataBound" GroupIndent="20" ItemStyle-Height="30px" DataSourceID="ObjectDataSource1">
                    <Columns>
                        <C1WebGrid:C1TemplateColumn>
                            <ItemTemplate>
                              <asp:Image runat="server" ID="imgDoanKT" ImageUrl="~/Images/People.gif" ToolTip="Đoàn kiểm toán"
                                    Style="cursor: pointer" />

                                <asp:Image runat="server" ID="imgEdit" ImageUrl="~/Images/cus.gif" ToolTip="Chi tiết"
                                    Style="cursor: pointer" />
                            </ItemTemplate>
                            <ItemStyle Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1TemplateColumn Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="PK_DocumentID" Text='<%# DataBinder.Eval(Container.DataItem,"DoanKiemToanID")%>'></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1TemplateColumn Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="DotKiemToanID" Text='<%# DataBinder.Eval(Container.DataItem,"DotKiemToanID")%>'></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Năm" DataField="nam" RowMerge="Restricted" Visible="false">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <GroupInfo HeaderText="Kế hoạch năm {0}" Position="Header" OutlineMode="StartCollapsed"></GroupInfo>
                        </C1WebGrid:C1BoundColumn>

                          <C1WebGrid:C1BoundColumn HeaderText="Loại đối tượng kiểm toán" DataField="loai_doi_tuong" RowMerge="Restricted" Visible="false">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <GroupInfo HeaderText="{0}" Position="Header" OutlineMode="StartCollapsed"></GroupInfo>
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1TemplateColumn Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="TruongDoan" Text='<%# DataBinder.Eval(Container.DataItem,"Name")%>'></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Trưởng đoàn" DataField="Name">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Đợt kiểm toán" DataField="ten_dot_kiem_toan"
                            RowMerge="Restricted" Visible="false">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <GroupInfo HeaderText="Đợt: {0}" Position="Header" OutlineMode="StartCollapsed">
                            </GroupInfo>
                        </C1WebGrid:C1BoundColumn>
                         <C1WebGrid:C1BoundColumn HeaderText="Đối tượng kiểm toán" DataField="doi_tuong_kiem_toan">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>

                         <C1WebGrid:C1BoundColumn HeaderText="Đơn vị thực hiện" DataField="don_vi_thuc_hien">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>

                         <C1WebGrid:C1BoundColumn HeaderText="Quy mô" DataField="quy_mo">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>

                         <C1WebGrid:C1BoundColumn HeaderText="Thời gian dự kiến" DataField="thoi_gian_du_kien">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>

                          <C1WebGrid:C1TemplateColumn HeaderText="Trạng thái">
                              <ItemTemplate>
                                <asp:Label runat="server" ID="lblTrangThai" Text='<%# DataBinder.Eval(Container.DataItem,"Status")%>'></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>
                    </Columns>
                </C1WebGrid:C1WebGrid>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        /*********************************************************/
        $(document).ready(function () {
            //do smt here
        });
        /*********************************************************/
        function newform() {
            window.location.href = "LoaiDoiTuongKiemToan_Load.aspx";
        }
        function LoadDocument(DocumentID, truongdoan, DotKiemToanID) {
            url = "../../Controls/Tab/ThanhLapDoanKT_Load.aspx?act=doankt&doc=" + DocumentID + "&truongdoan=" + truongdoan + "&dotkt=" + DotKiemToanID;
            window.location.href = url;
        }
        function LoadDocumentDotKT(DocumentID) {
            url = "DotKiemToanNam_Load.aspx?act=loaddoc&doc=" + DocumentID;
            window.location.href = url;
        }

        function LoadDocumentSeeDotKT(DocumentID) {
            url = "DotKiemToanNam_Load.aspx?act=loaddoc&doc=" + DocumentID + "&timkiem=tk";
            window.location.href = url;
        }

        function LoadDocumentReViewDotKT(doankt, dotkt) {
            url = "ReviewDotKiemToan.aspx?act=td&doankt=" + doankt + "&dotkt=" + dotkt;
            window.location.href = url;
        }

        function LoadDocumentChuongTrinhKiemToan(doankt, truongdoan, dotkt) {
            //url = "../../Controls/Tab/ThanhLapDoanKT_Load.aspx?act=ctkt&doankt=" + doankt + "&truongdoan=" + truongdoan + "&dotkt=" + dotkt;
            url = "ChuongTrinhKiemToan.aspx?act=ctkt&doankt=" + doankt + "&truongdoan=" + truongdoan + "&dotkt=" + dotkt;
            window.location.href = url;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ButtonExtendBefore" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ButtonExtend" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FormFooter" runat="server">
</asp:Content>
