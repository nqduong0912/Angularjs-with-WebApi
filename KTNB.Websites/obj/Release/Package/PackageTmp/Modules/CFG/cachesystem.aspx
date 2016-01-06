<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" Inherits="VPB_PROMOTION.CFG.Modules_CFG_cachesystem" Codebehind="cachesystem.aspx.cs" %>
<%@ OutputCache NoStore="true" Duration="1" VaryByParam="None" %>
<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="frmContent" ContentPlaceHolderID="FormContent" Runat="Server">
<table style="width:100%; height:60%; background:black; color:White">
    <tr >
        <td style="color:White; width: 120px;">System cache objects</td>
        <td style="color:White; width: 203px;">
            <asp:Label runat="server" ID="lblCacheObjects" CssClass="lblCaption" ForeColor="Yellow"></asp:Label>
        </td>
        <td style="color:White">
            <asp:Button ID="btnResetCache" runat="server" SkinID="DeleteButton" 
                Text="Reset" OnClick="DeleSysCache" />
        </td>
    </tr>
    <tr >
        <td style="color:White" align="left" colspan="3" valign="top">
            <hr size="1" />
        </td>
    </tr>
    <tr >
        <td colspan="3" style="color:White; width:100%;">
            <asp:SqlDataSource runat="server" ID="SqlDataSource1" DataSourceMode="DataSet"></asp:SqlDataSource>
             <C1WebGrid:C1WebGrid runat="server" ID="dataCtrl" 
                Width="100%"
                DataSourceID="SqlDataSource1"
                OnItemCreated="dataCtrl_OnItemCreated"
                OnItemDataBound="dataCtrl_OnItemDataBound">
                <Columns>
                    <C1WebGrid:C1TemplateColumn HeaderText="ID#" SortExpression="PK_CACHEID">
                        <ItemTemplate>
                            <asp:HyperLink runat="server" ID="btnRemove" Text="delete..." style="cursor:hand;"></asp:HyperLink>
                            <asp:Label runat="server" ID="lblCACHEID" Text='<%# DataBinder.Eval(Container.DataItem,"PK_CACHEID")%>'></asp:Label>
                        </ItemTemplate>
                    </C1WebGrid:C1TemplateColumn>
                    <C1WebGrid:C1BoundColumn DataField="CREATEDDATETIME" HeaderText="CREATED" SortExpression="CREATEDDATETIME"></C1WebGrid:C1BoundColumn>
                    <C1WebGrid:C1BoundColumn DataField="EXPIREMINUTES" HeaderText="EXP" SortExpression="EXPIREMINUTES"></C1WebGrid:C1BoundColumn>
                </Columns>
            </C1WebGrid:C1WebGrid>
        </td>
    </tr>
</table>
<script type="text/javascript">
    function resetcache() {
        if (!window.confirm("Đồng ý reset cache ứng dụng ?"))
            return false;
        else
            return true;
    }
</script>
</asp:Content>
<asp:Content ID="frmB1" ContentPlaceHolderID="ButtonExtendBefore" Runat="Server">
</asp:Content>
<asp:Content ID="frmB2" ContentPlaceHolderID="ButtonExtend" Runat="Server">
</asp:Content>