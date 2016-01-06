<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" Inherits="VPB_PROMOTION.CFG.Modules_CFG_newapp" Codebehind="newapp.aspx.cs" %>
<%@ OutputCache Duration="1" NoStore="true" VaryByParam="None"%>
<asp:Content ID="frmContent" ContentPlaceHolderID="FormContent" Runat="Server">
<asp:ScriptManager runat="server" ID="scrp1"></asp:ScriptManager>
<asp:UpdatePanel runat="server" ID="pan" UpdateMode="Always">
    <ContentTemplate>
<table style="width:100%">
    <tr>
        <td class="lblNormal" style="width: 112px">(<font color='red'>*</font>) Nhóm ứng dụng</td>
        <td style="width: 416px">
            <asp:DropDownList ID="cboApplication" runat="server" SkinID="DropDownList" AutoPostBack="true">
            </asp:DropDownList>
        </td>
        <td rowspan="10" align="left" valign="top" class="lblNormal">
        </td>
    </tr>
    <tr>
        <td class="lblNormal" style="width: 112px">(<font color='red'>*</font>) Tên ứng dụng</td>
        <td style="width: 416px">
            <asp:TextBox ID="txtComponentName" runat="server" SkinID="TextBox"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="lblNormal" style="width: 112px">Ghi chú</td>
        <td style="width: 416px">
            <asp:TextBox ID="txtComponentDes" runat="server" SkinID="TextBox" Width="300px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="lblNormal" style="width: 112px;display:none" valign="top">Vai trò</td>
        <td style="width: 416px; display:none">
        <asp:GridView runat="server" ID="grvRoles" AlternatingRowStyle-BackColor="AntiqueWhite" ShowHeader="false">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <input type="checkbox" checked value='<%# DataBinder.Eval(Container.DataItem,"FK_RoleID")%>' />
                            <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Name")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>
<script type="text/javascript">
    function verifycomponentname(obj) {
        var componentname = obj.value;
        if (componentname == "") return;
        var query = "act=verifyname&comp=" + componentname;
        var url = "newapp.aspx";
        $.ajax({
            type: "POST",
            url: url,
            data: query,
            success: function(data) {
                if (data == "duplicated") {
                    alert('Tên ứng dụng này đã có. Bạn hãy nhập tên khác.');
                    obj.focus();
                }
            }
        });
    }
</script>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
<asp:Content ID="frmB1" ContentPlaceHolderID="ButtonExtendBefore" Runat="Server">
</asp:Content>
<asp:Content ID="frmB2" ContentPlaceHolderID="ButtonExtend" Runat="Server">
</asp:Content>

