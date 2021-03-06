﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true"
    CodeBehind="DSNhomKiemToanDuocPhuTrach.aspx.cs" Inherits="VPB_TDHS.Modules.DMS.DSNhomKiemToanDuocPhuTrach" %>

<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
<asp:ObjectDataSource runat="server" ID="ObjectDataSource1" SelectMethod="getList"
    TypeName="VPB_KTNB.Helpers.DataSource">
    <SelectParameters>
        <asp:Parameter Name="Status" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>


    <table width="50%">
        <tr>
            <td>
                <C1WebGrid:C1WebGrid runat="server" ID="dataCtrl" Width="100%" OnItemCreated="dataCtrl_OnItemCreated"
                    OnItemDataBound="dataCtrl_OnItemDataBound" GroupIndent="20" ItemStyle-Height="30px" 
DataSourceID="ObjectDataSource1">
                    <Columns>
                        <C1WebGrid:C1BoundColumn HeaderText="Đợt kiểm toán" DataField="ten_dot_kiem_toan"
                            RowMerge="Restricted" Visible="false">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <GroupInfo HeaderText="{0}" Position="Header" OutlineMode="StartCollapsed">
                            </GroupInfo>
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1TemplateColumn>
                            <ItemTemplate>
                                <asp:Image runat="server" ID="imgEdit" ImageUrl="~/Images/viewdetail.gif" ToolTip="Chi tiết"
                                    Style="cursor: pointer" />
                            </ItemTemplate>
                            <ItemStyle Width="8%" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </C1WebGrid:C1TemplateColumn>
                        <%--<C1WebGrid:C1TemplateColumn>
                            <ItemTemplate>
                                <asp:Image runat="server" ID="imgLapHoSoPhanTichSoBo" ImageUrl="~/Images/preferences.gif" ToolTip="Lập hồ sơ phân tích sơ bộ"
                                    Style="cursor: pointer" />
                            </ItemTemplate>
                            <ItemStyle Width="8%" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </C1WebGrid:C1TemplateColumn>--%>
                        <C1WebGrid:C1TemplateColumn Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="DotKiemToanID" Text='<%# DataBinder.Eval(Container.DataItem,"DotKiemToanID")%>'></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1TemplateColumn Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="DoanKiemToanID" Text='<%# DataBinder.Eval(Container.DataItem,"DoanKiemToanID")%>'></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1TemplateColumn Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="NhomKiemToanID" Text='<%# DataBinder.Eval(Container.DataItem,"NhomKiemToanID")%>'></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1TemplateColumn Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="TruongNhom" Text='<%# DataBinder.Eval(Container.DataItem,"TruongNhom")%>'></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Đoàn" DataField="TenDoan">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Trưởng đoàn" DataField="TruongDoan">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Nhóm" DataField="TenNhom">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Trưởng nhóm" DataField="TruongNhom">
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
            window.location.href = "LapHoSoPhanTichSoBo.aspx";
        }
        function LoadDocument(DotKiemToanID,DoanKiemToanID,NhomKiemToanID, truongnhom) {
            //            url = "../../Controls/Tab/ThanhLapDoanKT_Load.aspx?act=doankt&doc=" + DocumentID + "&truongdoan=" + truongdoan + "&dotkt=" + DotKiemToanID;
            url = "NhomKiemToan_View.aspx?act=loaddoc&doc=" + NhomKiemToanID + "&truongnhom=" + truongnhom + "&dotkt=" + DotKiemToanID + "&doankt=" + DoanKiemToanID;
            window.location.href = url;
        }
//        function LoadLapHSPTSBPage(DotKiemToanID, DoanKiemToanID, NhomKiemToanID, truongnhom) {
//            //            url = "../../Controls/Tab/ThanhLapDoanKT_Load.aspx?act=doankt&doc=" + DocumentID + "&truongdoan=" + truongdoan + "&dotkt=" + DotKiemToanID;
//            url = "NhomKiemToan_View.aspx?act=loaddoc&doc=" + NhomKiemToanID + "&truongnhom=" + truongnhom + "&dotkt=" + DotKiemToanID + "&doankt=" + DoanKiemToanID;
//            window.location.href = url;
//        }
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ButtonExtendBefore" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ButtonExtend" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FormFooter" runat="server">
</asp:Content>
