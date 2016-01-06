<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="DotKiemToan.aspx.cs" Inherits="VPB_TDHS.Modules.DMS.DotKiemToan" %>
<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
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
            <C1WebGrid:C1WebGrid runat="server" ID="dataCtrl" Width="100%" DataSourceID="ObjectDataSource1"
                    OnItemCreated="dataCtrl_OnItemCreated" OnItemDataBound="dataCtrl_OnItemDataBound"
                    GroupIndent="20" ItemStyle-Height="30px">
                    <Columns>
                        <C1WebGrid:C1TemplateColumn>
                            <ItemTemplate>
                                <asp:Image runat="server" ID="imgDoanKT" ImageUrl="~/Images/People.gif" ToolTip="Đoàn kiểm toán"
                                    Style="cursor: pointer" /> (<asp:Label runat="server" ID="Persons"></asp:Label>) | 
                                <asp:Image runat="server" ID="imgEdit" ImageUrl="~/Images/viewdetail.gif" ToolTip="Chi tiết"
                                    Style="cursor: pointer" />
                            </ItemTemplate>
                            <ItemStyle Width="20%" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1TemplateColumn Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="PK_DocumentID" Text='<%# DataBinder.Eval(Container.DataItem,"PK_DocumentID")%>'></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Năm" DataField="Năm" RowMerge="Restricted" Visible="false">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <GroupInfo HeaderText="{0}" Position="Header" OutlineMode="StartCollapsed"></GroupInfo>
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Loại đối tượng kiểm toán" DataField="Loại đối tượng kiểm toán" RowMerge="Restricted" Visible="false">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <GroupInfo HeaderText="{0}" Position="Header" OutlineMode="StartCollapsed"></GroupInfo>
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Đối tượng kiểm toán" DataField="Đối tượng kiểm toán">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Quy mô" DataField="Quy mô đợt kiểm toán">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Thời gian" DataField="Thời gian dự kiến kiểm toán">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>

                          <C1WebGrid:C1BoundColumn HeaderText="Mục tiêu" DataField="Mục tiêu">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>


                         <C1WebGrid:C1BoundColumn HeaderText="Phạm vi" DataField="Phạm vi">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                    </Columns>
            </C1WebGrid:C1WebGrid>
        </td>
    </tr>
</table>
<script type="text/javascript">
    /*********************************************************/
    var timkiem = Qry["timkiem"];
    $(document).ready(function () {
        //do smt here
        HiddenControlTimKiem();
    });

    /*********************************************************/
    function newform() {
        window.location.href = "DotKiemToan_Load.aspx";
    }
    function LoadDocument(DocumentID) {
        url = "DotKiemToan_Load.aspx?act=loaddoc&doc=" + DocumentID;
        opendetail(url, "DotKiemToan_Load");
    }
    function LapDoanKiemToan(DocumentID) {
        //var url = "DoanKiemToan_Load.aspx?dotkt=" + DocumentID;
        var url = "../../Controls/Tab/tab.aspx?doc=" + DocumentID + "&act=dotkt"+"&timkiem="+timkiem;
        opendetail(url, "LapDoanKT");
    }

    function HiddenControlTimKiem() {
        var timkiem = Qry["timkiem"];
        if (timkiem == 'tk') {
            $("input[type*='button']").hide();
            $("input[type*='submit']").hide();
        }
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ButtonExtendBefore" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ButtonExtend" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FormFooter" runat="server">
</asp:Content>
