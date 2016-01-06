<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="NhomKiemToan.aspx.cs" Inherits="VPB_TDHS.Modules.DMS.NhomKiemToan" %>
<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
 <asp:ScriptManager runat="server" ID="ScriptManager1"></asp:ScriptManager>
<asp:ObjectDataSource runat="server" ID="ObjectDataSource1" SelectMethod="getDocumentList"
    TypeName="DataSource">
    <SelectParameters>
        <asp:Parameter Name="DocumentTypeID" Type="String" />
        <asp:Parameter Name="DocFields" Type="String" />
        <asp:Parameter Name="PropertyFields" Type="String" />
        <asp:Parameter Name="Condition" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>


<asp:ObjectDataSource runat="server" ID="ObjectDataSource2" SelectMethod="getListMangNghiepVu"
    TypeName="DataSource">
    <SelectParameters>
        <asp:Parameter Name="DoanKT" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>
<div runat="server" id="divDoanKT" >
 <asp:UpdatePanel runat="server" ID="updatepanel1" UpdateMode="Conditional" OnLoad="updatepanel1_OnLoad">
 <ContentTemplate>
<table width="70%">
    <tr>
        <td colspan="2" class="GridHeader">Thông tin theo nhóm kiểm toán</td>
    </tr>
    <tr>
        <td>
            <C1WebGrid:C1WebGrid runat="server" ID="data_Ctrl1" Width="100%" 
                    OnItemCreated="dataCtrl_OnItemCreated" OnItemDataBound="dataCtrl_OnItemDataBound"
                    GroupIndent="20" ItemStyle-Height="30px" DataSourceID="ObjectDataSource1" >
                    <Columns>

                        <C1WebGrid:C1TemplateColumn>
                            <ItemTemplate>
                                <asp:Image runat="server" ID="imgEdit" ImageUrl="~/Images/viewdetail.gif" ToolTip="Chi tiết"
                                    Style="cursor: pointer" />
                            </ItemTemplate>
                            <ItemStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </C1WebGrid:C1TemplateColumn>
                        
                        <C1WebGrid:C1TemplateColumn Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="PK_DocumentID" Text='<%# DataBinder.Eval(Container.DataItem,"PK_DocumentID")%>'></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>
                        
                         <C1WebGrid:C1BoundColumn HeaderText="Tên trưởng nhóm" DataField="Name">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>

                         <C1WebGrid:C1TemplateColumn HeaderText="Số lượng thành viên">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="SLThanhVien"></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>

                         <C1WebGrid:C1TemplateColumn HeaderText="Số lượng mảng nghiệp vụ">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="SLMangNghiepVu"></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>
                    </Columns>
            </C1WebGrid:C1WebGrid>
        </td>
    </tr>


</table>

<table width="70%">
     <tr>
        <td colspan="2" class="GridHeader">Thông tin theo mảng nghiệp vụ</td>
    </tr>
    
     <tr>
        <td>
            <C1WebGrid:C1WebGrid runat="server" ID="dataCtrl" Width="100%" 
                    GroupIndent="20" ItemStyle-Height="30px" 
                OnItemDataBound="dataCtrl_ItemDataBound" DataSourceID="ObjectDataSource2">
                    <Columns>

                        <C1WebGrid:C1TemplateColumn>
                            <ItemTemplate>
                                <asp:Image runat="server" ID="imgEdit" ImageUrl="~/Images/viewdetail.gif" ToolTip="Chi tiết"
                                    Style="cursor: pointer" />
                            </ItemTemplate>
                            <ItemStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </C1WebGrid:C1TemplateColumn>
                        
                        <C1WebGrid:C1TemplateColumn Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="NhomKiemToan_ID" Text='<%# DataBinder.Eval(Container.DataItem,"NhomKiemToan_ID")%>'></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>
                        
                        <C1WebGrid:C1BoundColumn HeaderText="Mảng nghiệp vụ" DataField="Tên mảng nghiệp vụ">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>

                         <C1WebGrid:C1BoundColumn HeaderText="Tên trưởng nhóm" DataField="NhomKiemToan">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>


                         <C1WebGrid:C1BoundColumn HeaderText="Thành viên" DataField="ThanhVien">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>

                          <C1WebGrid:C1TemplateColumn HeaderText="Submit">
                          <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemTemplate>
                                        <asp:Image runat="server" Visible="false" ID="imgSubmit" ImageUrl="/Images/check.gif"></asp:Image>
                                    </ItemTemplate>
                         </C1WebGrid:C1TemplateColumn>
                    </Columns>
            </C1WebGrid:C1WebGrid>
        </td>
    </tr>

</table>
 </ContentTemplate>
 </asp:UpdatePanel>
</div>
<script type="text/javascript">
    /*********************************************************/
    $(document).ready(function () {
        //do smt here
    });
    /*********************************************************/
    function newform() {
        var doankt = Qry["doankt"]; //getParameterByName("doankt");
        var dotkt = Qry["dotkt"];
        window.location.href = "NhomKiemToan_Load.aspx?doankt=" + doankt + "&dotkt=" + dotkt;
    }
    function LoadDocument(DocumentID, truongnhom) {
        var dotkt = Qry["dotkt"];
        var doankt = Qry["doankt"];
        url = "NhomKiemToan_View.aspx?act=loaddoc&doc=" + DocumentID + "&truongnhom=" + truongnhom+"&dotkt="+dotkt+"&doankt="+doankt;
        window.location.href = url;
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
