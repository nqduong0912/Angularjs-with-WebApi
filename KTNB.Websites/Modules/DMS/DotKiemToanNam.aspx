<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="DotKiemToanNam.aspx.cs" Inherits="VPB_TDHS.Modules.DMS.DotKiemToanNam" %>
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
    <table width="100%">
        <tr>
            <td class="form-group">                
                <label class="col-sm-2  control-label" style="line-height: 34px;height: 34px;">Năm</label>
                <asp:DropDownList ID="drpYear" runat="server" CssClass="form-control" Width="150px" AutoPostBack="true" OnSelectedIndexChanged="drpYear_SelectedIndexChanged">
                    <asp:ListItem Text="2015" Value="2015" Enabled="true"></asp:ListItem>
                    <asp:ListItem Text="2014" Value="2014"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr><td>&nbsp;</td></tr>
        <tr>
            <td>
                <c1webgrid:c1webgrid runat="server" ID="dataCtrl" Width="100%" DataSourceID="ObjectDataSource1"
                    OnItemCreated="dataCtrl_OnItemCreated" OnItemDataBound="dataCtrl_OnItemDataBound"
                    GroupIndent="20" ItemStyle-Height="30px">
                    <Columns>
                        <C1WebGrid:C1TemplateColumn>
                            <ItemTemplate>
                                <a class="click-icon" onclick="LapDoanKiemToan('<%# DataBinder.Eval(Container.DataItem,"PK_DocumentID")%>');" title="Đoàn kiểm toán">
                                    <span class="group-file"></span>
                                </a>
                                &nbsp;
                                <a class="click-icon" onclick="LoadDocument('<%# DataBinder.Eval(Container.DataItem,"PK_DocumentID")%>');" title="Chi tiết">
                                    <span class="edit-file"></span>
                                </a>
                            </ItemTemplate>
                            <ItemStyle Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </C1WebGrid:C1TemplateColumn>

                        <C1WebGrid:C1TemplateColumn Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="PK_DocumentID" Text='<%# DataBinder.Eval(Container.DataItem,"PK_DocumentID")%>'></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>

                        <C1WebGrid:C1BoundColumn HeaderText="Năm" DataField="Năm">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Loại đối tượng kiểm toán" DataField="Loại đối tượng kiểm toán">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>

                        <C1WebGrid:C1BoundColumn HeaderText="Tên đợt kiểm toán" DataField="Tên đợt kiểm toán">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>

                         <C1WebGrid:C1BoundColumn HeaderText="Đối tượng kiểm toán" DataField="Đối tượng kiểm toán">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>

                         <C1WebGrid:C1BoundColumn HeaderText="Đơn vị thực hiện" DataField="Đơn vị thực hiện">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>

                         <C1WebGrid:C1BoundColumn HeaderText="Quy mô" DataField="Quy mô đợt kiểm toán">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>

                         <C1WebGrid:C1BoundColumn HeaderText="Thời gian dự kiến" DataField="Thời gian dự kiến kiểm toán">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>

                       <C1WebGrid:C1TemplateColumn HeaderText="Trạng thái">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="Status" Text='<%# DataBinder.Eval(Container.DataItem,"Status")%>'></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>

                    </Columns>
                </c1webgrid:c1webgrid>
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
        window.location.href = "DotKiemToanNam_Load.aspx";
    }
    function LoadDocument(DocumentID) {
        url = "DotKiemToanNam_Load.aspx?act=loaddoc&doc=" + DocumentID;
        window.location.href = url;
    }
    function LapDoanKiemToan(DocumentID) {
        //var url = "DoanKiemToan_Load.aspx?dotkt=" + DocumentID;
        var url = "../../Controls/Tab/ThanhLapDoanKT_Load.aspx?doc=" + DocumentID + "&act=dotkt";
        window.location.href = url;
        //opendetail(url, "LapDoanKT");
    }

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ButtonExtendBefore" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ButtonExtend" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FormFooter" runat="server">
</asp:Content>
