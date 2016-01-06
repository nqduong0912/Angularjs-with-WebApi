<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true"
    CodeBehind="TruongDoan_XinDuyet.aspx.cs" Inherits="VPB_TDHS.Modules.DMS.TruongDoan_XinDuyet" %>

<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <asp:ScriptManager runat="server" ID="ScriptManager1">
    </asp:ScriptManager>
    <asp:ObjectDataSource runat="server" ID="ObjectDataSource1" SelectMethod="getList"
        TypeName="DataSource">
        <SelectParameters>
            <asp:Parameter Name="Status" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <table width="100%">
        <tr>
            <td>
                <asp:UpdatePanel runat="server" ID="updatepanel1" UpdateMode="Conditional" OnLoad="updatepanel1_OnLoad">
                    <ContentTemplate>
                        <C1WebGrid:C1WebGrid runat="server" ID="dataCtrl" Width="100%" OnItemCreated="dataCtrl_OnItemCreated"
                            OnItemDataBound="dataCtrl_OnItemDataBound" GroupIndent="20" ItemStyle-Height="30px"
                            DataSourceID="ObjectDataSource1">
                            <Columns>
                                <C1WebGrid:C1TemplateColumn>
                                    <ItemTemplate>
                                        <asp:Image runat="server" ID="imgDoanKT" ImageUrl="~/Images/People.gif" ToolTip="Đoàn kiểm toán"
                                            Style="cursor: pointer" />
                                        <asp:Image runat="server" ID="imgEdit" ImageUrl="~/Images/cus.gif" ToolTip="Chi tiết"
                                            Style="cursor: pointer" />
                                        <asp:Image runat="server" Visible="false" ID="imgViewHSRR" ImageUrl="~/Images/monitor.gif"
                                            ToolTip="Chi tiết" Style="cursor: pointer" />
                                    </ItemTemplate>
                                    <ItemStyle Width="15%" HorizontalAlign="Center" VerticalAlign="Middle" />
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
                                    <GroupInfo HeaderText="Kế hoạch năm {0}" Position="Header" OutlineMode="StartCollapsed">
                                    </GroupInfo>
                                </C1WebGrid:C1BoundColumn>
                                <C1WebGrid:C1BoundColumn HeaderText="Loại đối tượng kiểm toán" DataField="loai_doi_tuong"
                                    RowMerge="Restricted" Visible="false">
                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                    <GroupInfo HeaderText="{0}" Position="Header" OutlineMode="StartCollapsed">
                                    </GroupInfo>
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
                    </ContentTemplate>
                </asp:UpdatePanel>
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
        function LoadDocumentViewHSRR(DocumentID) {
            url = "TruongDoan_ViewHSRR.aspx?act=loaddoc&dotkt=" + DocumentID;
            window.location.href = url;
        }

        function LoadDocumentReViewDotKT(doankt, dotkt) {
            url = "ReviewDotKiemToan.aspx?act=loaddoc&doankt=" + doankt + "&dotkt=" + dotkt;
            window.location.href = url;
        }
        function LoadDocumentCapNhatPhanHoiBB(doankt, dotkt) {
            url = "TruongDoan_CapNhatPhanHoi.aspx?act=loaddoc&doankt=" + doankt + "&dotkt=" + dotkt;
            window.location.href = url;
        }
        function LoadDocumentChuongTrinhKiemToan(doankt, truongdoan, dotkt) {
            //url = "../../Controls/Tab/ThanhLapDoanKT_Load.aspx?act=ctkt&doankt=" + doankt + "&truongdoan=" + truongdoan + "&dotkt=" + dotkt;
            url = "ChuongTrinhKiemToan.aspx?act=ctkt&doankt=" + doankt + "&truongdoan=" + truongdoan + "&dotkt=" + dotkt;
            window.location.href = url;
        }
        function KiemTraPTRRHetChua(DotKiemToanID) {
            if (!window.confirm('Xin ban lãnh đạo duyệt hồ sơ rủi ro?'))
                return false;
            var url = "TruongDoan_XinDuyet.aspx";
            var dotkt = DotKiemToanID;
            var query = "act=KiemTraPTRRHetChua";
            query += "&dotkt=" + dotkt;
            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (message) {
                    FinishProcessingForm();
                    if (message == 'True')
                        XinDuyet(DotKiemToanID);
                    else {
                        if (window.confirm('Đợt kiểm toán chưa phân tích hết rủi ro, Bạn có muốn tiếp tục trình Ban lãnh đạo duyệt?')) {
                            XinDuyet(DotKiemToanID);
                        }
                        else
                            return false;
                    }
                }
            });
        }
        function XinDuyet(DotKiemToanID) {
            //            if (!window.confirm('Xin ban lãnh đạo duyệt hồ sơ rủi ro?'))
            //                return false;
            var url = "TruongDoan_XinDuyet.aspx";
            var dotkt = DotKiemToanID;
            var query = "act=XinDuyetHSRR";
            query += "&dotkt=" + dotkt;
            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (message) {
                    alert(message);
                    FinishProcessingForm();
                    __doPostBack('<%=updatepanel1.ClientID %>', '');
                }
            });
        }
        function XinDuyet_XuatBBKT(doankt, dotkt) {
            url = "ReviewDotKiemToan.aspx?act=td_bbkt&doankt=" + doankt + "&dotkt=" + dotkt;
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
