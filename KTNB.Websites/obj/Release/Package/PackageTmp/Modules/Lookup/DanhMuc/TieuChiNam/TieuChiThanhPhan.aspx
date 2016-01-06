<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="TieuChiThanhPhan.aspx.cs" Inherits="VPB_KTNB.Modules.Lookup.DanhMuc.TieuChiNam.TieuChiThanhPhan" %>
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
    <asp:Repeater ID="rptDanhmuc" runat="server" OnItemDataBound="rptDanhmuc_ItemDataBound">
        <HeaderTemplate>
            <div class="table-responsive">
            <table class="table table-striped table-bordered table-hover">
                <thead class="color-green">
                    <tr>
                        <th>Tên</th>
                        <th>Diễn giải</th>
                        <th>Tỷ trọng</th>
                        <th>Loại tiêu chí</th>
                        <th>Tiêu chí chính</th>
                        <th>Trạng thái</th>
                        <th>Hành động</th>
                    </tr>
                </thead>
                <tbody>
        </HeaderTemplate>
        <FooterTemplate>
                </tbody>
                </table>
            </div>
            <!-- /.table-responsive -->
        </FooterTemplate>
        <ItemTemplate>
            <tr>
                <td><%# Eval("Ten") %></td>
                <td><%# Eval("Diengiai") %></td>
                <td><%# Eval("Tytrong") %>%</td>
                <td><asp:Literal ID="Loaitieuchi" runat="server"></asp:Literal></td>
                <td><%# Eval("Tieuchichinh") %></td>
                <td><asp:Literal ID="Status" runat="server"></asp:Literal></td>
                <td class="text-center click-icon">
                    <a class="click-icon" href="javascript:;" title="Chi tiết" onclick="LoadDocument('<%# Eval("PK_DocumentID") %>')">
                        <span class="edit-file"></span>
                    </a>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>    
<script type="text/javascript">
    /*********************************************************/
    $(document).ready(function () {
        //do smt here
    });
    /*********************************************************/
    function newform() {
        window.location.href = "TieuChiThanhPhan_Input.aspx?a=<%= appID %>&curApp=<%= curAppID %>";
    }
    function LoadDocument(DocumentID) {
        url = "TieuChiThanhPhan_Input.aspx?a=<%= appID %>&curApp=<%= curAppID %>&act=loaddoc&doc=" + DocumentID;
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


