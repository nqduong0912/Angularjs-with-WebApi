<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" Inherits="VPB_PROMOTION.CFG.Modules_CFG_auditlog" Codebehind="auditlog.aspx.cs" %>
<%@ OutputCache Duration="1" NoStore="true" VaryByParam="None" %>
<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="frmContent" ContentPlaceHolderID="FormContent" Runat="Server">

<asp:ScriptManager runat="server" ID="scr4"></asp:ScriptManager>

<table style="width:100%">
    <tr>
        <td class="lblCaption" style="width: 165px">
            Ngày nhật ký <br />
            <asp:TextBox ID="txtValueDate" runat="server" SkinID="TextBoxDate" ></asp:TextBox>
        </td>
        <td class="lblCaption" style="width: 190px">
            Sự kiện<br />
            <asp:DropDownList ID="cboEvents" runat="server" SkinID="DropDownList"></asp:DropDownList>
        </td>
        <td class="lblCaption" style="width: 335px">
            Chi nhánh/Phòng GD<br />
            <asp:DropDownList ID="cboCompany" runat="server" SkinID="DropDownList">
            </asp:DropDownList>
        </td>
        <td class="lblNormal" style="width: 448px">
            <br />
            <asp:Button ID="btnView" runat="server" SkinID="DownloadButton" Text="Xem" />
            <asp:Button ID="btnClose" runat="server" SkinID="CloseWindowButton" />
        </td>
        <td align="right"><asp:Label runat="server" ID="lblRecordCount" CssClass="lblCaption"></asp:Label></td>
    </tr>
</table>

<asp:UpdatePanel runat="server" ID="pn" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnView" EventName="Click" />
    </Triggers>
    <ContentTemplate>
<asp:SqlDataSource runat="server" ID="SqlDataSource1" DataSourceMode="DataSet"></asp:SqlDataSource>
<table style="width:100%">
    <tr>
        <td>
            <C1WebGrid:C1WebGrid runat="server" ID="dataCtrl" 
                Width="100%"
                DataSourceID="SqlDataSource1"
                OnItemCreated="dataCtrl_OnItemCreated"
                OnItemDataBound="dataCtrl_OnItemDataBound">
                <Columns>
                    <C1WebGrid:C1TemplateColumn HeaderText="Chi nhánh/Phòng GD" SortExpression="groupfullname">
                         <ItemTemplate>
                            <img src="../../Images/AppCom/Group.png" border='0' />
                            <asp:Label runat="server" ID="company" Text='<%# DataBinder.Eval(Container.DataItem,"groupfullname")%>'></asp:Label>
                        </ItemTemplate>
                    </C1WebGrid:C1TemplateColumn>
    
                    <C1WebGrid:C1TemplateColumn HeaderText="Tài khoản" SortExpression="username">
                        <ItemTemplate>
                            <img src="../../Images/profile_small.gif" border='0' />
                            <asp:Label runat="server" ID="acc" Text='<%# DataBinder.Eval(Container.DataItem,"username")%>'></asp:Label>
                        </ItemTemplate>
                    </C1WebGrid:C1TemplateColumn>
                    
                    <C1WebGrid:C1TemplateColumn HeaderText="OperationType" Visible="false">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblOperationType" Text='<%# DataBinder.Eval(Container.DataItem,"OperationType")%>'></asp:Label>
                        </ItemTemplate>
                    </C1WebGrid:C1TemplateColumn>
                    
                    <C1WebGrid:C1TemplateColumn HeaderText="Sự kiện" SortExpression="AdditionalData1">
                        <ItemTemplate>
                            <asp:Image runat="server" ID="imgEvent" BorderWidth="0" Visible="false" />
                            <asp:Label runat="server" ID="lblAdditionalData1" Text='<%# DataBinder.Eval(Container.DataItem,"AdditionalData1")%>'></asp:Label>
                        </ItemTemplate>
                    </C1WebGrid:C1TemplateColumn>
                    
                    <C1WebGrid:C1BoundColumn DataField="AdditionalData2" HeaderText="Nội dung" SortExpression="AdditionalData2"></C1WebGrid:C1BoundColumn>
                    
                    <C1WebGrid:C1TemplateColumn HeaderText="Ngày" SortExpression="operationdatetime">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblOperationdatetime" ForeColor="brown" Text='<%# DataBinder.Eval(Container.DataItem,"operationdatetime")%>'></asp:Label>
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
    function closeme() {
        window.opener = null;
        window.close(true);
    }
</script>
</asp:Content>
<asp:Content ID="frmB1" ContentPlaceHolderID="ButtonExtendBefore" Runat="Server">
</asp:Content>
<asp:Content ID="grmB2" ContentPlaceHolderID="ButtonExtend" Runat="Server">
</asp:Content>

