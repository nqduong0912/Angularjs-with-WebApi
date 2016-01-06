<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true"
    CodeBehind="NhomKiemToanDoToiPhuTrach.aspx.cs" Inherits="VPB_TDHS.Modules.DMS.NhomKiemToanDoToiPhuTrach" %>

<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
   <asp:ScriptManager runat="server" ID="ScriptManager1"></asp:ScriptManager>
<asp:HiddenField ID="sortDirection" Value="asc" runat="server" />
    <table width="100%">
        <tr>
            <td style="width: 222px">
                <asp:Label ID="lblActor" runat="server"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtActor" runat="server" SkinID="TextBoxReadOnly"></asp:TextBox>
            </td>
        </tr>
    </table>
    <asp:ObjectDataSource runat="server"  ID="ObjectDataSource1" SelectMethod="getNhomKiemToanInfo"
        TypeName="VPB_KTNB.Helpers.DataSource">
        <SelectParameters>
            <asp:Parameter Name="DocTypeNhomKT" Type="string" />
            <asp:Parameter Name="role" Type="string" />
            <asp:Parameter Name="currentUserID" Type="string" />
            <asp:Parameter Name="currentUserName" Type="string" />
            <asp:Parameter Name="task" Type="string" />
            <asp:Parameter Name="sort" Type="string" />
        </SelectParameters>
    </asp:ObjectDataSource>
<asp:UpdatePanel runat="server" ID="updatepanel1" UpdateMode="Conditional" OnLoad="updatepanel1_OnLoad">
<ContentTemplate>

    <table width="100%">
        <tr>
            <td>
                <C1WebGrid:C1WebGrid runat="server" ID="dataCtrl" Width="100%" OnItemCreated="dataCtrl_OnItemCreated"
                    DataSourceID="ObjectDataSource1" OnSortingCommand="dataCtrl_OnSorting"  AllowSorting=true OnItemDataBound="dataCtrl_OnItemDataBound" GroupIndent="20"
                    ItemStyle-Height="30px">
                    <Columns>
                        <C1WebGrid:C1TemplateColumn>
                            <ItemTemplate>
                                <asp:Image runat="server" ID="imgEdit" ImageUrl="~/Images/viewdetail.gif" ToolTip="Chi tiết đợt kiểm toán"
                                    Style="cursor: pointer" />
                            </ItemTemplate>
                            <ItemStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Top" />
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1TemplateColumn>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblCommand" Style="cursor: pointer; text-decoration: underline" />
                                <%--<asp:Button ID="btnPhanTich" runat="server" CausesValidation="false" 
                                    Text="Phân tích"  />--%>
                            </ItemTemplate>
                            <ItemStyle Width="10%" HorizontalAlign="Center" VerticalAlign="Top" />
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1BoundColumn SortExpression="ten_dot_kiem_toan" HeaderText="Đợt kiểm toán" DataField="ten_dot_kiem_toan">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1BoundColumn SortExpression="doi_tuong_kiem_toan" HeaderText="Đối tượng kiểm toán" DataField="doi_tuong_kiem_toan">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1TemplateColumn Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="PK_DocumentID" Text='<%# DataBinder.Eval(Container.DataItem,"PK_DocumentID")%>'></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1TemplateColumn>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblMangNghiepVu"></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1TemplateColumn Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="dotkt" Text='<%# DataBinder.Eval(Container.DataItem,"dotkt")%>'></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>
                        <%--<C1WebGrid:C1BoundColumn HeaderText="Tên trưởng nhóm" DataField="Name">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>--%>
                        <%-- <C1WebGrid:C1TemplateColumn HeaderText="SL thành viên theo mảng nghiệp vụ">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="SLThanhVienTheoMNV"></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>--%>
                        <%--<C1WebGrid:C1TemplateColumn HeaderText="SL thành viên">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="SLThanhVien"></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1TemplateColumn HeaderText="SL mảng nghiệp vụ">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="SLMangNghiepVu"></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>--%>
                    </Columns>
                </C1WebGrid:C1WebGrid>
            </td>
        </tr>
    </table>

 </ContentTemplate>
 </asp:UpdatePanel>
    <style>
        table.fixed
        {
            table-layout: fixed;
        }
        table.fixed td
        {
            overflow: hidden;
        }
        th
        {
            background-color: Teal;
            color: White;
        }
    </style>
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
        function LoadDocument(DocumentID, role, truongnhom, task) {
            if (role == 'tv' && task == '')
                url = "LapHoSoRuiRo.aspx?act=loaddoc&doc=" + DocumentID;
            else if (role == 'tn' && task == '')
                url = "LapHoSoPhanTichSoBo.aspx?act=loaddoc&doc=" + DocumentID;
            else if (role == 'tv' && task == 'rrcl')
                url = "DanhGiaRuiRoConLai.aspx?doc=" + DocumentID;

            window.location.href = url;
        }

        function LoadDocumentDotKT(DocumentID) {
            url = "DotKiemToanNam_Load.aspx?act=loaddoc&doc=" + DocumentID + "&timkiem=tk";
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
