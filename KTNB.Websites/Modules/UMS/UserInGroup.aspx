<%@ Page Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" Inherits="VPB_PROMOTION.UMS.UMS_UserInGroup" Title="Untitled Page" Codebehind="UserInGroup.aspx.cs" %>
<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" Runat="Server">
    <asp:ScriptManager  runat="server" ID="Script1"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="pan1" UpdateMode="Always">
        <ContentTemplate>

    <table class="table" runat="server" id="SearchBar" style="width:100%">
        <tr class="">
            <td style="width: 74px">
                Tên đăng nhập
            </td>
            <td style="width: 201px">
                
                <asp:TextBox ID="txtLoginName" runat="server" SkinID="TextBox"></asp:TextBox>
                
            </td>
            <td style="width: 94px">
                
                <asp:CheckBox ID="chkSearchALL" runat="server" ForeColor="Blue" 
                    Text="Tìm toàn cục" />
                
            </td>
            <td>
                
                <asp:Button ID="btnSearch" runat="server" SkinID="SearchButton" Text="Button" OnClick="DoSearch" />
                
            </td>
        </tr>
    </table>

    <table class="table" style="width:100%">
        <tr>
            <td>
                <asp:ObjectDataSource runat="server" ID="ObjectDataSource1" SelectMethod="SelectAllUserInGroup" TypeName="VPB_KTNB.Helpers.DataSource">
                    <SelectParameters>
                        <asp:Parameter Name="groupid" Type="String"  />
                        <asp:Parameter Name="loginname" Type="String"  />
                        <asp:Parameter Name="searchall" Type="Boolean"  />
                        <asp:Parameter Name="docspace" Type="String"  />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <C1WebGrid:C1WebGrid runat="server" ID="dataCtrl" Width="100%" 
                  DataSourceID="ObjectDataSource1" 
                  OnItemCreated="dataCtrl_OnItemCreated"
                  OnItemDataBound="dataCtrl_OnItemDataBound">
                        <Columns>
                           <C1WebGrid:C1TemplateColumn Visible="false">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="PK_UserID" Text='<%# DataBinder.Eval(Container.DataItem,"PK_UserID")%>'></asp:Label>
                                </ItemTemplate>
                           </C1WebGrid:C1TemplateColumn>
                           <C1WebGrid:C1TemplateColumn Visible="true" HeaderText="Tên đăng nhập" SortExpression="UserName">
                                <ItemTemplate>
                                    <img src="../../Images/profile_small.gif" border='0' />
                                    <asp:Label runat="server" ID="UserName" Text='<%# DataBinder.Eval(Container.DataItem,"UserName")%>'></asp:Label>
                                </ItemTemplate>
                           </C1WebGrid:C1TemplateColumn>
                           <C1WebGrid:C1BoundColumn DataField="FullName" HeaderText="Họ tên đầy đủ"></C1WebGrid:C1BoundColumn>
                           <C1WebGrid:C1BoundColumn DataField="UserCode" HeaderText="DAO"></C1WebGrid:C1BoundColumn>
                           <C1WebGrid:C1BoundColumn DataField="GroupName" HeaderText="CN/PGD">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                           </C1WebGrid:C1BoundColumn>
                           <C1WebGrid:C1BoundColumn DataField="RoleName" HeaderText="Chức danh">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                           </C1WebGrid:C1BoundColumn>
                           <C1WebGrid:C1BoundColumn DataField="Description" HeaderText="Tên CN/PGD">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                           </C1WebGrid:C1BoundColumn>
                           <C1WebGrid:C1TemplateColumn Visible="true" HeaderText="Lock/Unlock" SortExpression="IsExpired">
                                <ItemTemplate>
                                    <asp:Image runat="server" ID="imgStatus" />
                                    <asp:Label runat="server" Visible="false" ID="lblStatus" Text='<%# DataBinder.Eval(Container.DataItem,"IsExpired")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                           </C1WebGrid:C1TemplateColumn>
                        </Columns>
                </C1WebGrid:C1WebGrid>            
            </td>
        </tr>
    </table>

    <asp:Button ID="AddNew" runat="server" SkinID="InsertButton" Text="Button" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function newuser()
        {
            var query = GetQueryString();
            var docspace = query["docspace"];     
            window.open("NewUser.aspx?&docspace=" + docspace, "fraDetail");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ButtonExtend" Runat="Server">
</asp:Content>