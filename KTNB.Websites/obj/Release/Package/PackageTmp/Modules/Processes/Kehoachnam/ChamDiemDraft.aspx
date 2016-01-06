<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="ChamDiemDraft.aspx.cs" Inherits="VPB_KTNB.Modules.Processes.KeHoachNam.ChamDiemDraft" %>

<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
     <div class="form-horizontal">
        <div class="form-group">
            <label for="inputName" class="col-sm-3 control-label">Năm</label>
            <div class="col-sm-3">
                <asp:DropDownList ID="drpYears" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpYear_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
           <div class="col-sm-6">
               <asp:Button runat="server" ID="btnSearch" CssClass="btn color-green btn-xs" Text="Sửa đổi điểm" />
           </div>
        </div>
        <div class="form-group">
             <label for="inputName" class="col-sm-3 control-label">Loại đối tượng kiểm toán</label>
            <div class="col-sm-3">
                <asp:DropDownList ID="drpLoaiDTKT" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpYear_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
            <label for="inputName" class="col-sm-3 control-label">Đối tượng kiểm toán</label>
            <div class="col-sm-3">
                <asp:DropDownList ID="DropDownList1" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpYear_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
        </div>      
    </div>  
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
                    GroupIndent="20" ItemStyle-Height="30px" OnItemCommand="dataCtrl_ItemCommand">
                    <Columns>                    
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
                        <C1WebGrid:C1TemplateColumn >
                            <ItemTemplate>
                                <asp:RadioButton ID="RowSelector" runat="server" CssClass="cssrbt" GroupName="rdb" Checked='<%# DataBinder.Eval(Container.DataItem,"IsActive")%>'></asp:RadioButton>
                                <%--<asp:HiddenField ID="HiddenID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"Id")%>' />--%>
                            </ItemTemplate>
                            <ItemStyle Width="14%" HorizontalAlign="Right" VerticalAlign="Middle" />
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Ngày cập nhật" DataField="NgayCapNhat" DataFormatString="{0:dd/MM/yyyy}">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        
                        <%--<C1WebGrid:C1ButtonColumn Text="Active" runat="server" CommandName="changeStatus"></C1WebGrid:C1ButtonColumn>--%>
                        
                        <C1WebGrid:C1TemplateColumn HeaderText="Hành động">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lbTrinhduyet" Text="Trình Duyệt" Visible ="false" onclick="lbTrinhduyet_Click" />
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
            alert(id);
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
