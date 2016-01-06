<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="QuymoLoaiDTKT.aspx.cs" Inherits="VPB_KTNB.Modules.Lookup.DanhMuc.ChucDanh.QuymoLoaiDTKT" %>

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
    <table width="80%">
        <tr>
            <td>Năm</td>
            <td>
                <asp:DropDownList ID="drl_Year" runat="server"
                    SkinID="DropDownListRequired">
                </asp:DropDownList>
            </td>
            <td>Loại đối tượng kiểm toán</td>
            <td>
                <asp:DropDownList ID="drl_LDTKT" runat="server"
                    SkinID="DropDownListRequired">
                </asp:DropDownList>
            </td>
            <td>Quy mô</td>
            <td>
                <asp:DropDownList ID="drl_QM" runat="server"
                    SkinID="DropDownListRequired">
                </asp:DropDownList>
            </td>
            <td>
                Nguồn lực
            </td>
            <td>
                <asp:TextBox ID="txtResource" runat="server"></asp:TextBox>
            </td>

        </tr>
    </table>
    <table width="80%" style="margin-top: 20px">
        
        <tr>
            <td>
                <C1WebGrid:C1WebGrid runat="server" ID="dataCtrl" Width="100%" DataSourceID="ObjectDataSource1"
                    OnItemCreated="dataCtrl_OnItemCreated" OnItemDataBound="dataCtrl_OnItemDataBound"
                    GroupIndent="20" ItemStyle-Height="30px">
                    <Columns>
                        <C1WebGrid:C1TemplateColumn Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="PK_DocumentID" Text='<%# DataBinder.Eval(Container.DataItem,"PK_DocumentID")%>'></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1TemplateColumn HeaderText="STT" Visible="true">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="STT" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Năm" DataField="Năm">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Loại đối tượng Kiểm toán" DataField="Loại đối tượng Kiểm toán">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Quy mô" DataField="Quy mô">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Nguồn lực" DataField="Nguồn lực">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1TemplateColumn Visible="true" HeaderText="Trạng thái">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="Status" Text='<%# DataBinder.Eval(Container.DataItem,"Status")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="14%" HorizontalAlign="Right" VerticalAlign="Middle" />
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1TemplateColumn HeaderText="Hành động">
                            <ItemTemplate>
                                <asp:Image runat="server" ID="imgDel" ImageUrl="~/Images/delete.gif" ToolTip="Chi tiết"
                                    Style="cursor: pointer" />
                            </ItemTemplate>
                            <ItemStyle Width="8%" HorizontalAlign="Center" VerticalAlign="Middle" />
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
            window.location.href = "QuymoLoaiDTKT_Input.aspx";
        }
        function LoadDocument(DocumentID) {
            url = "QuymoLoaiDTKT_Input.aspx?act=loaddoc&doc=" + DocumentID;
            window.location.href = url;
        }
        function deleteLoaiDTKT(pk_documentID) {
            if (!window.confirm("Bạn có muốn xóa Quy mô loại đối tượng kiểm toán?"))
                return false;
                var url = "QuymoLoaiDTKT.aspx";
                var query = "act=xoaLoaiDTKT";
                query += "&doc=" + pk_documentID;
                StartProcessingForm("");
                $.ajax({
                    type: "POST",
                    url: url,
                    data: query,
                    success: function (errormessage) {
                        FinishProcessingForm();
                        if (errormessage == "") {
                            delete_success();
                            window.location.reload(true);
                        }
                        else
                            alert(errormessage);
                    }
                });
        }
        function delete_success() {
            alert(MSG_DEL_OK);
        }
        function delete_error() {
            alert(MSG_DEL_ER);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ButtonExtendBefore" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ButtonExtend" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FormFooter" runat="server">
</asp:Content>
