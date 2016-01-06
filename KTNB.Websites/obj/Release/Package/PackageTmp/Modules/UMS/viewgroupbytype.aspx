<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" Inherits="VPB_PROMOTION.UMS.Modules_UMS_viewgroupbytype" Codebehind="viewgroupbytype.aspx.cs" %>
<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="frmContent" ContentPlaceHolderID="FormContent" Runat="Server">
    <script src="../../Javascript/getquerystring.js"></script>
    <asp:ScriptManager runat="server" ID="script1"></asp:ScriptManager>
        <asp:UpdatePanel runat="server" ID="pn1" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
        </Triggers>
        <ContentTemplate>
    <table class="table" style="width:100%">
        <tr>
            <td class="lblNormal" align="left" style="width: 88px" valign="top">
                TT hỗ trợ
            </td>
            <td align="left" style="width: 360px" valign="top">
                <asp:Label ID="lblCompany" CssClass="lblCaption" runat="server" Text="Label"></asp:Label>
            </td>
            <td align="right" valign="top">Thêm
                <asp:DropDownList ID="cboCompany" runat="server" SkinID="DropDownList">
                </asp:DropDownList>
&nbsp;<asp:Button ID="btnSave" runat="server" SkinID="SaveButton" Text="Button" OnClick="addCompany2TTHTro" />
            </td>
        </tr>
        <tr>
            <td class="lblNormal" align="left" colspan="3" valign="top">
                <hr size="1" />
            </td>
        </tr>
        <tr>
            <td class="lblNormal" align="left" colspan="3" 
                style="font-weight: bold; color: #009900" valign="top">
                Danh sách</td>
        </tr>
    </table>
    


    <!--grid-->
    <asp:SqlDataSource runat="server" ID="SqlDataSource1" DataSourceMode="DataSet"></asp:SqlDataSource>
    <table class="table" style="width:100%">
        <tr style="width:100%">
            <td style="width:100%">
                <C1WebGrid:C1WebGrid runat="server" ID="dataCtrl" 
                Width="100%"
                DataSourceID="SqlDataSource1"
                OnItemCreated="dataCtrl_OnItemCreated"
                OnItemDataBound="dataCtrl_OnItemDataBound">
                
                <Columns>
                    <C1WebGrid:C1TemplateColumn HeaderText="Tên Chi nhánh/Phòng GD" Visible="true" SortExpression="Description">
                         <ItemTemplate>
                            <asp:HyperLink runat="server" ID="btnRemove" Text="remove..." style="cursor:hand;"></asp:HyperLink>
                            <img border="0" src="../../Images/AppCom/Group.png" />
                            <asp:Label runat="server" ID="company" Text='<%# DataBinder.Eval(Container.DataItem,"Description")%>'></asp:Label>
                        </ItemTemplate>
                    </C1WebGrid:C1TemplateColumn>
                    
                    <C1WebGrid:C1TemplateColumn HeaderText="PK_GroupID" Visible="false">
                         <ItemTemplate>
                            <asp:Label runat="server" ID="RowID" Text='<%# DataBinder.Eval(Container.DataItem,"PK_GroupID")%>'></asp:Label>
                        </ItemTemplate>
                    </C1WebGrid:C1TemplateColumn>
                    
                    <C1WebGrid:C1TemplateColumn HeaderText="Mã Chi nhánh/Phòng GD" Visible="true" SortExpression="Name">
                         <ItemTemplate>
                            <asp:Label runat="server" ID="Name" Text='<%# DataBinder.Eval(Container.DataItem,"Name")%>'></asp:Label>
                            
                        </ItemTemplate>
                    </C1WebGrid:C1TemplateColumn>
                </Columns>
                
                
                </C1WebGrid:C1WebGrid>
            </td>
        </tr>
    </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function addcompany() {
            if (!confirm("Đồng ý thêm chi nhánh này vào TT Hỗ Trợ ?"))
                return false;
            return true;
        }
        function removeCompany(groupid) {
            if (!confirm("Đồng ý loại bỏ Chi nhánh này khỏ TT Hỗ Trợ ?"))
                return false;

            var Qry = GetQueryString();
            var url = "viewgroupbytype.aspx?type=" + Qry["type"] + "&groupid=" + Qry["groupid"] + "&removegroup=" + groupid;
            window.location = url;
            return true;
        }
    </script>
    
    <!--demo here-->
    
    <%--<hr />
    
    <asp:ObjectDataSource runat="server" ID="ObjectDataSource1" SelectMethod="SelectAllUser" TypeName="DataSource"></asp:ObjectDataSource>
    
    <C1WebGrid:C1WebGrid runat="server" ID="dataCtrl_2" Width="100%" DataSourceID="ObjectDataSource1" OnItemCreated="dataCtrl_OnItemCreated">
        <Columns>
            <C1WebGrid:C1BoundColumn DataField="Name" HeaderText="Name"></C1WebGrid:C1BoundColumn>
            <C1WebGrid:C1BoundColumn DataField="FullName" HeaderText="FullName"></C1WebGrid:C1BoundColumn>
        </Columns>
    </C1WebGrid:C1WebGrid>--%>
    
    
</asp:Content>
<asp:Content ID="frmB1" ContentPlaceHolderID="ButtonExtendBefore" Runat="Server">
</asp:Content>
<asp:Content ID="frmB2" ContentPlaceHolderID="ButtonExtend" Runat="Server">
</asp:Content>

