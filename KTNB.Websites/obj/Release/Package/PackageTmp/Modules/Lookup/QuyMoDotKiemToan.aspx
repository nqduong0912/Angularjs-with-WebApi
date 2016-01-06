<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="QuyMoDotKiemToan.aspx.cs" Inherits="VPB_TDHS.Modules.Lookup.QuyMoDotKiemToan" %>
<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
<asp:ObjectDataSource runat="server" ID="ObjectDataSource1" SelectMethod="getDocumentList"
    TypeName="DataSource">
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
            <C1WebGrid:C1WebGrid runat="server" ID="dataCtrl" Width="100%" DataSourceID="ObjectDataSource1"
                    OnItemCreated="dataCtrl_OnItemCreated" OnItemDataBound="dataCtrl_OnItemDataBound"
                    GroupIndent="20" ItemStyle-Height="30px">
                    <Columns>
                        <C1WebGrid:C1TemplateColumn>
                            <ItemTemplate>
                                <asp:Image runat="server" ID="imgEdit" ImageUrl="~/Images/viewdetail.gif" ToolTip="Chi tiết"
                                    Style="cursor: pointer" />
                            </ItemTemplate>
                            <ItemStyle Width="8%" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1TemplateColumn Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="PK_DocumentID" Text='<%# DataBinder.Eval(Container.DataItem,"PK_DocumentID")%>'></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Quy mô" DataField="Tên quy mô">
                            <ItemStyle Width="8%" VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Diễn giải" DataField="Diễn giải">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>

                        <%--<C1WebGrid:C1BoundColumn HeaderText="Số người" DataField="Số người">
                            <ItemStyle Width="8%" VerticalAlign="Top" HorizontalAlign="Right" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Số ngày công" DataField="Số ngày công">
                            <ItemStyle Width="8%" VerticalAlign="Top" HorizontalAlign="Right" />
                        </C1WebGrid:C1BoundColumn>--%>

                         <C1WebGrid:C1TemplateColumn HeaderText = "Số người">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="SoNguoi" Text='<%# DataBinder.Eval(Container.DataItem,"Số người")%>'></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>

                         <C1WebGrid:C1TemplateColumn HeaderText="Số ngày công">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="SoNgayCong" Text='<%# DataBinder.Eval(Container.DataItem,"Số ngày công")%>'></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>

                        <C1WebGrid:C1TemplateColumn Visible="true" HeaderText="Trạng thái">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="Status" Text='<%# DataBinder.Eval(Container.DataItem,"Status")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="8%" HorizontalAlign="Right" VerticalAlign="Middle" />
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
        window.location.href = "QuyMoDotKiemToan_Load.aspx";
    }
    function LoadDocument(DocumentID) {
        url = "QuyMoDotKiemToan_Load.aspx?act=loaddoc&doc=" + DocumentID;
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
