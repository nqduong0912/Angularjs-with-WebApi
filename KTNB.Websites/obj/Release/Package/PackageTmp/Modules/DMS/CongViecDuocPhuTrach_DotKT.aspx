<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true"
    CodeBehind="CongViecDuocPhuTrach_DotKT.aspx.cs" Inherits="VPB_TDHS.Modules.DMS.CongViecDuocPhuTrach_DotKT" %>

<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <asp:ObjectDataSource runat="server" ID="ObjectDataSource1" SelectMethod="getDotKTByCongViec"
        TypeName="DataSource">
        <SelectParameters>
            <asp:Parameter Name="FormatUserAssign" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <table width="100%">
        <tr>
            <td>
                <C1WebGrid:C1WebGrid runat="server" ID="dataCtrl" Width="100%" DataSourceID="ObjectDataSource1"
                    OnItemCreated="dataCtrl_OnItemCreated" OnItemDataBound="dataCtrl_OnItemDataBound"
                    GroupIndent="20" ItemStyle-Height="30px">
                    <Columns>
                        <C1WebGrid:C1TemplateColumn>
                            <ItemTemplate>
                                        <asp:Image runat="server" ID="imgEdit" ImageUrl="~/Images/viewdetail.gif" ToolTip="Chi tiết đợt kiểm toán"
                                    Style="cursor: pointer" />|
                                    <asp:Image runat="server" ID="imgCongViec" ImageUrl="~/Images/AppCom/my-task.png"
                                        ToolTip="Danh sách công việc được phân công" Style="cursor: pointer" />
                                        <%--|<asp:Image runat="server"
                                            ID="imgRRCL" ImageUrl="~/Images/AppCom/dsuyquyen.gif" ToolTip="Đánh giá rủi ro còn lại"
                                            Style="cursor: pointer" />--%>
                            </ItemTemplate>
                            <ItemStyle Width="10%" HorizontalAlign="Center" VerticalAlign="Top" />
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1TemplateColumn Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="PK_DocumentID" Text='<%# DataBinder.Eval(Container.DataItem,"PK_DocumentID")%>'></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1TemplateColumn Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="DoanKT" Text='<%# DataBinder.Eval(Container.DataItem,"DoanKT")%>'></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Năm" DataField="Năm" RowMerge="Restricted" Visible="false">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <GroupInfo HeaderText="Kế hoạch năm {0}" Position="Header" OutlineMode="StartCollapsed">
                            </GroupInfo>
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Loại đối tượng kiểm toán" DataField="Loại đối tượng kiểm toán"
                            RowMerge="Restricted" Visible="false">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <GroupInfo HeaderText="{0}" Position="Header" OutlineMode="StartCollapsed">
                            </GroupInfo>
                        </C1WebGrid:C1BoundColumn>

                        <C1WebGrid:C1BoundColumn HeaderText="Trưởng đoàn" DataField="TruongDoan">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>

                        <C1WebGrid:C1BoundColumn HeaderText="Tên đợt kiểm toán" DataField="Tên đợt kiểm toán">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Đối tượng kiểm toán" DataField="Đối tượng kiểm toán">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Đơn vị thực hiện" DataField="Đơn vị thực hiện">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Quy mô" DataField="Quy mô đợt kiểm toán">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Thời gian dự kiến" DataField="Thời gian dự kiến kiểm toán">
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
            window.location.href = "DotKiemToanNam_Load.aspx";
        }
        function LoadDocument(DocumentID) {
            url = "DotKiemToanNam_Load.aspx?act=loaddoc&doc=" + DocumentID+"&timkiem=tk";
            window.location.href = url;
        }

        function LoadCongViec(dotkt, doankt) {
            var cv = Qry["cv"];
            if (cv == "nguoithuchien")
                url = "CongViecDuocPhuTrach.aspx?doankt=" + doankt + "&dotkt=" + dotkt + "&cv=nguoithuchien";
            if (cv == "nguoiduyet")
                url = "CongViecDuocPhuTrach.aspx?doankt=" + doankt + "&dotkt=" + dotkt + "&cv=nguoiduyet";
            window.location.href = url;
        }
        function LoadDanhGiaRRCL(dotkt, doankt) {
            //        var cv = Qry["cv"];
            //        if (cv == "nguoithuchien")
            //            url = "CongViecDuocPhuTrach.aspx?doankt=" + doankt + "&dotkt=" + dotkt + "&cv=nguoithuchien";
            //        if (cv == "nguoiduyet")
            //            url = "CongViecDuocPhuTrach.aspx?doankt=" + doankt + "&dotkt=" + dotkt + "&cv=nguoiduyet";
            url = "DanhGiaRuiRoConLai.aspx?doankt=" + doankt + "&dotkt=" + dotkt + "&cv=nguoiduyet";
            window.location.href = url;
        }


        function LapDoanKiemToan(DocumentID) {
            //var url = "DoanKiemToan_Load.aspx?dotkt=" + DocumentID;
            var url = "../../Controls/Tab/ThanhLapDoanKT_Load.aspx?doc=" + DocumentID + "&act=dotkt" + "&timkiem=tk";
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
