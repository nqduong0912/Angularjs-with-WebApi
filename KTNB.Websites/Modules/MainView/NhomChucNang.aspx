<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="NhomChucNang.aspx.cs" Inherits="VPB_KTNB.Modules.MainView.NhomChucNang" %>
<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">

<table width="80%">
    <tr>
        <td>
            <C1WebGrid:C1WebGrid runat="server" ID="dataCtrl" Width="100%" 
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
                                <asp:Label runat="server" ID="PK_ROLEID" Text='<%# DataBinder.Eval(Container.DataItem,"PK_ROLEID")%>'></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Tên" DataField="NAME">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Diễn giải" DataField="DESCRIPTION">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
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
        window.location.href = "NhomChucNang_Input.aspx";
    }
    function LoadDocument(DocumentID) {
        url = "NhomChucNang_Input.aspx?act=loaddoc&doc=" + DocumentID;
        window.location.href = url;
    }
</script>
</asp:Content>
