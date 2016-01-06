<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="QuanLyBoTieuChiKeHoachNam.aspx.cs" Inherits="VPB_KTNB.Modules.Lookup.DanhMuc.KeHoachNam.QuanLyBoTieuChiKeHoachNam" %>

<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <%--<asp:ObjectDataSource runat="server" ID="ObjectDataSource1" SelectMethod="getDocumentList"
        TypeName="DataSource">
        <SelectParameters>
            <asp:Parameter Name="DocumentTypeID" Type="String" />
            <asp:Parameter Name="DocFields" Type="String" />
            <asp:Parameter Name="PropertyFields" Type="String" />
            <asp:Parameter Name="Condition" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>--%>
    <br />
    <table width="100%">
        <tr>
            <td>Năm&nbsp;
                <asp:DropDownList ID="drpYears" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpYear_SelectedIndexChanged">

                </asp:DropDownList>
            </td>
            <td>Loại đối tượng kiểm toán&nbsp;
                <asp:DropDownList ID="drpLoaiDTKT" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpYear_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <br />
    <table width="100%">
        <tr>
            <td>
                <%--<asp:Repeater runat="server" ID="test" DataSourceID="ObjectDataSource1" OnItemDataBound="test_ItemDataBound" OnItemCommand="test_ItemCommand">
                    <HeaderTemplate>
                        <table>
                            <thead>
                                <th>test</th>
                            </thead>                        
                    </HeaderTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:ImageButton runat="server" CommandName="changeStatus" ID="imgEdit" ImageUrl="~/Images/viewdetail.gif" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"PK_DocumentID")%>' />    
                            </td>
                        </tr>                        
                    </ItemTemplate>
                </asp:Repeater>--%>
                <C1WebGrid:C1WebGrid runat="server" ID="dataCtrl" Width="100%" 
                    OnItemCreated="dataCtrl_OnItemCreated" OnItemDataBound="dataCtrl_OnItemDataBound"
                    GroupIndent="20" ItemStyle-Height="30px">
                    <Columns>
                        <%--<C1WebGrid:C1TemplateColumn>                            
                            <EditItemTemplate>
                            <asp:ImageButton runat="server" CommandName="changeStatus" ID="imgEdit" ImageUrl="~/Images/viewdetail.gif" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"PK_DocumentID")%>' />    
                            </EditItemTemplate>
                            <ItemStyle Width="8%" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </C1WebGrid:C1TemplateColumn>--%>
                        
                        <C1WebGrid:C1TemplateColumn Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="PK_ID" Text='<%# DataBinder.Eval(Container.DataItem,"Id")%>'></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Năm" DataField="Nam">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Loại đối tượng kiểm toán" DataField="LoaiDTKT">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Bộ tiêu chí năm" DataField="BoTieuChiNam">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1TemplateColumn Visible="true" HeaderText="Trạng thái">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lbStatus" Text='<%# DataBinder.Eval(Container.DataItem,"TrangThai")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="14%" HorizontalAlign="Right" VerticalAlign="Middle" />
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1TemplateColumn HeaderText="On/Off">
                            <ItemTemplate>
                                <asp:RadioButton ID="RowSelector" runat="server" Visible="false" CssClass="cssrbt" GroupName="rdb" Checked='<%# DataBinder.Eval(Container.DataItem,"IsActive")%>'></asp:RadioButton>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Ngày cập nhật" DataField="NgayCapNhat" DataFormatString="{0:dd/MM/yyyy}">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        
                        <%--<C1WebGrid:C1ButtonColumn Text="Active" runat="server" CommandName="changeStatus"></C1WebGrid:C1ButtonColumn>--%>
                        
                        <C1WebGrid:C1TemplateColumn>
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lbChamdiem" Text="Chấm điểm draft"/>
                            </ItemTemplate>
                            <ItemStyle Width="15%" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1TemplateColumn>
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lvCopyBTC" Text="Copy BTC" />
                            </ItemTemplate>
                            <ItemStyle Width="12%" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1TemplateColumn>
                            <ItemTemplate>
                                <a><asp:Label runat="server" ID="lbTrinhduyet"  Text="Trình Duyệt" Visible ="false"/></a>
                            </ItemTemplate>
                            <ItemStyle Width="12%" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </C1WebGrid:C1TemplateColumn>

                        <%--<C1WebGrid:C1TemplateColumn>
                            <ItemTemplate>
                                <asp:Image runat="server" ID="imgEdit" ImageUrl="~/Images/viewdetail.gif" ToolTip="Chi tiết"
                                    Style="cursor: pointer" />
                            </ItemTemplate>
                            <ItemStyle Width="8%" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </C1WebGrid:C1TemplateColumn>--%>
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
        $(function () {
            $(".cssrbt").each(function () {
                $(this).find(":radio").attr("name", "rdb");
            });
        })
        function Active(id)
        {
            $.ajax({
                type: "POST",
                url: "QuanLyBoTieuChiKeHoachNam.aspx/ActiveBTC",
                data: JSON.stringify({ tcnId : id }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function () {
                    
                }
            });
        }
        function Trinhduyet(id) {
            var r = confirm("Bạn có chắc trình duyệt bộ tiêu chí này?");
            if (!r) return;
            if (r) {
                $.ajax({
                    type: "POST",
                    url: "QuanLyBoTieuChiKeHoachNam.aspx/Trinhduyet",
                    data: JSON.stringify({ tcnId: id }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function () {
                        window.location.href = "QuanLyBoTieuChiKeHoachNam.aspx";
                    }
                });
            }
            
        }
        function newform() {
            var strOptions = $("#ctl00_FormContent_drpYears").val();
            window.location.href = "QuanLyBoTieuChiKeHoachNam_Input.aspx?y=" + strOptions;

        }
        function LoadDocument(DocumentID) {
            url = "QuanLyBoTieuChiKeHoachNam_Input.aspx?act=loaddoc&doc=" + DocumentID;
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
