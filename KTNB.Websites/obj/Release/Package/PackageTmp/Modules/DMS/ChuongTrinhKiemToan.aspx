<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="ChuongTrinhKiemToan.aspx.cs" Inherits="VPB_TDHS.Modules.DMS.ChuongTrinhKiemToan" %>

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
    <div class="form-horizontal">
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Đối tượng kiểm toán</label>
            <div class="col-sm-6">
                <fieldset disabled>
                    <asp:TextBox ID="ID8_2A4CA2AD_0282_4D57_86AC_D973D281EF54" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                </fieldset>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Đợt kiểm toán</label>
            <div class="col-sm-6">
                <fieldset disabled>
                    <asp:TextBox ID="ID8_63A0C4B1_2088_4994_B891_2FF65EB20265" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                </fieldset>
            </div>
        </div>
    </div>
<%--    <table width="100%">
        <tr>
            <td style="width: 222px">Đối tượng kiểm toán</td>
            <td>
                <asp:TextBox Enabled="false" ID="ID8_2A4CA2AD_0282_4D57_86AC_D973D281EF54" runat="server"
                    SkinID="TextBoxReadOnly"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Đợt kiểm toán</td>
            <td>
                <asp:TextBox Enabled="false" ID="ID8_63A0C4B1_2088_4994_B891_2FF65EB20265" runat="server"
                    SkinID="TextBoxReadOnly"></asp:TextBox>
            </td>
        </tr>

    </table>--%>

    <table width="100%">
        <tr>
            <td>
                <asp:UpdatePanel runat="server" ID="updatepanel1" UpdateMode="Conditional" OnLoad="updatepanel1_OnLoad">
                    <ContentTemplate>

                        <C1WebGrid:C1WebGrid runat="server" ID="dataCtrl" Width="100%" DataSourceID="ObjectDataSource1"
                            OnItemCreated="dataCtrl_OnItemCreated" OnItemDataBound="dataCtrl_OnItemDataBound"
                            GroupIndent="20" ItemStyle-Height="30px">
                            <Columns>
                                <C1WebGrid:C1TemplateColumn>
                                    <ItemTemplate>
                                        <asp:Image runat="server" ID="ImgEdit" ImageUrl="~/Images/viewdetail.gif" ToolTip="Sửa chương trình kiểm toán"
                                            Style="cursor: pointer" />
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                </C1WebGrid:C1TemplateColumn>


                                <C1WebGrid:C1TemplateColumn Visible="false">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="PK_DocumentID" Text='<%# DataBinder.Eval(Container.DataItem,"PK_DocumentID")%>'></asp:Label>
                                    </ItemTemplate>
                                </C1WebGrid:C1TemplateColumn>

                                <C1WebGrid:C1BoundColumn HeaderText="Công việc" DataField="Tên công việc">
                                    <ItemStyle Width="20%" HorizontalAlign="Left" VerticalAlign="Top" />
                                </C1WebGrid:C1BoundColumn>

                                <C1WebGrid:C1TemplateColumn Visible="true" HeaderText="Thủ tục kiểm toán">
                                    <ItemTemplate>
                                        <asp:TreeView runat="server" ID="treeViewThuTuc" Enabled="false" ExpandDepth="0" Width="300px">
                                        </asp:TreeView>
                                    </ItemTemplate>
                                    <ItemStyle Width="20%" HorizontalAlign="Left" VerticalAlign="Top" />
                                </C1WebGrid:C1TemplateColumn>

                                <C1WebGrid:C1BoundColumn HeaderText="Người thực hiện" DataField="Người thực hiện">
                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                </C1WebGrid:C1BoundColumn>

                                <C1WebGrid:C1BoundColumn HeaderText="Người duyệt" DataField="Người duyệt 1">
                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                </C1WebGrid:C1BoundColumn>
                                <%--
                         <C1WebGrid:C1BoundColumn HeaderText="Người duyệt 2" DataField="Người duyệt 2">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>--%>

                                <C1WebGrid:C1TemplateColumn HeaderText="Bắt đầu">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="NgayBatDau" Text='<%# DataBinder.Eval(Container.DataItem,"Ngày bắt đầu")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                </C1WebGrid:C1TemplateColumn>

                                <C1WebGrid:C1TemplateColumn HeaderText="Kết thúc">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="NgayKetThuc" Text='<%# DataBinder.Eval(Container.DataItem,"Ngày kết thúc")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
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
            var doankt = Qry["doankt"]; //getParameterByName("doankt");
            var dotkt = Qry["dotkt"];
            window.location.href = "ChuongTrinhKiemToan_Load.aspx?dotkt=" + dotkt + "&doankt=" + doankt;
        }
        function LoadDocument(DocumentID) {
            var doankt = Qry["doankt"]; //getParameterByName("doankt");
            var dotkt = Qry["dotkt"];
            url = "ChuongTrinhKiemToan_View.aspx?act=loaddoc&doc=" + DocumentID + "&dotkt=" + dotkt + "&doankt=" + doankt;
            window.location.href = url;
        }
        function LapChuongTrinhKiemToan(DocumentID) {
            var url = "../../Controls/Tab/ThanhLapDoanKT_Load.aspx?doc=" + DocumentID + "&act=dotkt" + "&timkiem=1";
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
