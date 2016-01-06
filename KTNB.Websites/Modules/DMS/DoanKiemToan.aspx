<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="DoanKiemToan.aspx.cs" Inherits="VPB_TDHS.Modules.DMS.DoanKiemToan" %>
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
<table width="50%">
    <tr>
        <td>
            <C1WebGrid:C1WebGrid runat="server" ID="dataCtrl" Width="100%" DataSourceID="ObjectDataSource1"
                    OnItemCreated="dataCtrl_OnItemCreated" OnItemDataBound="dataCtrl_OnItemDataBound"
                    GroupIndent="20" ItemStyle-Height="30px" >
                    <Columns>
                        <C1WebGrid:C1TemplateColumn>
                            <ItemTemplate>
                                <asp:Image runat="server" ID="imgEdit" ImageUrl="~/Images/viewdetail.gif" ToolTip="Chi tiết"
                                    Style="cursor: pointer" />
                            </ItemTemplate>
                            <ItemStyle Width="20%" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1TemplateColumn>
                            <ItemTemplate>
                                <asp:Image runat="server" ID="imgMappingMangNV" ImageUrl="~/Images/preferences.gif" ToolTip="Chọn mảng nghiệp vụ để thực hiện"
                                    Style="cursor: pointer" />
                            </ItemTemplate>
                            <ItemStyle Width="20%" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </C1WebGrid:C1TemplateColumn>
                        
                        <C1WebGrid:C1TemplateColumn Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="PK_DocumentID" Text='<%# DataBinder.Eval(Container.DataItem,"PK_DocumentID")%>'></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>
                        
                         <C1WebGrid:C1BoundColumn HeaderText="Tên trưởng đoàn" DataField="Name">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>

                           <C1WebGrid:C1TemplateColumn HeaderText="Số lượng thành viên">
                           <ItemStyle  HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="SLThanhVien"></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>

                    </Columns>
            </C1WebGrid:C1WebGrid>
        </td>
    </tr>
</table>
<script type="text/javascript">
    var timkiem = Qry["timkiem"]
    /*********************************************************/
    $(document).ready(function () {
        //do smt here
        //HiddenButton();
        HiddenControlTimKiem();
    });
    /*********************************************************/

   
    function newform() {
        var rowCount = $("img[id*='imgEdit']").length;
        if (rowCount >= 1) {
            alert("Một đợt kiểm toán chỉ cho phép tồn tại một đoàn kiểm toán");
            return false;
        }
        
        var dotkt = Qry["dotkt"]; //getParameterByName("dotkt");
        window.location.href = "DoanKiemToan_Load.aspx?dotkt="+dotkt;
    }

    function HiddenControlTimKiem() {
        var timkiem = Qry["timkiem"];
        if (timkiem == 'tk') {
            $("input[type*='button']").hide();
            $("input[type*='submit']").hide();
        }
    }

    function LoadDocument(DocumentID,truongdoan) {
        var dotkt = Qry["dotkt"];
        url = "DoanKiemToan_View.aspx?act=loaddoc&doc=" + DocumentID + "&truongdoan=" + truongdoan + "&dotkt=" + dotkt + "&timkiem=" + timkiem; 
        window.location.href = url;
    }
    function LoadMappingPage(DocumentID, truongdoan) {
        var dotkt = Qry["dotkt"];
        url = "DotKiemToan_MangNghiepVu.aspx?act=loaddoc&doc=" + DocumentID + "&truongdoan=" + truongdoan + "&dotkt=" + dotkt+"&timkiem="+timkiem;
        window.location.href = url;
    }
    function getParameterByName(name) 
    {
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
