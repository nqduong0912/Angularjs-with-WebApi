<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" Inherits="VPB_PROMOTION.CFG.Modules_CFG_apppermission" Codebehind="apppermission.aspx.cs" %>
<asp:Content ID="frmContent" ContentPlaceHolderID="FormContent" Runat="Server">
<script src="../../Javascript/BackEndProcess.js"></script>
<table style="width:100%">
    <tr>
        <td align="left" valign="top" style="width:50%" class="lblCaption">
            Vai trò
            <asp:ListBox runat="server" ID="lstRole" SkinID="ListBoxRole" Width="100%" Height="170px"></asp:ListBox>
        </td>
        <td align="left" valign="top" style="width:50%">
            <div id="rolename" class="lblCaption"></div>
            <table style="width:100%; height:100%" cellpadding="1" cellspacing="1">
                <tr class="lblNormal" align="center">
                    <td>
                        <img src="../../Images/Form/up.gif" border='0' />Upload</td>
                    <td>
                        <img src="../../Images/Form/down.gif" border='0' />Download</td>
                    <td>
                        <img src="../../Images/delete.gif" border='0' />Delete</td>
                </tr>
                <tr id="permission" style="background-color:Olive" align="center">
                    <td><input type="checkbox" id="chkUpload"></td>
                    <td><input type="checkbox" id="chkDownload"></td>
                    <td><input type="checkbox" id="chkDelete"></td>
                </tr>
                <tr>
                    <td colspan="3" align="right"> 
                        <input type="button" id="btnSetPermission" value="Gán quyền" class="button" disabled onclick="setrole()" />
                        <img id='wait' src="../../Images/indicator.gif" border='0' style='display:none' />
                    </td>
                </tr>
                <tr>
                    <td colspan="3" align="left"><hr size='1' /></td>
                </tr>
                <tr>
                    <td colspan="3" align="left" class='lblCaption' style="color:Blue">Đã phân quyền tới</td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:GridView runat="server" ID="grvRole" AlternatingRowStyle-BackColor="AntiqueWhite" ShowHeader="false">
                            <Columns>
                                <asp:TemplateField HeaderText="Đã phân quyền tới">
                                    <ItemTemplate>
                                        <img src="../../Images/UserInfo.gif"/>
                                        <asp:Label runat="server" ID="l1" Text='<%# DataBinder.Eval(Container.DataItem,"RoleName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="l2" Text='<%# DataBinder.Eval(Container.DataItem,"sRight")%>'></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="left">
            <input type="button" id="btnEnabled" class="button" value="Cho phép" style="width:100px;display:none" onclick="enableroleoncomponent();" />
            <input type="button" id="btnDisabled" class="button" value="Không cho phép" style="width:100px;display:none" onclick="disableroleoncomponent();" />
            
        </td>
    </tr>
</table>

<script type="text/javascript">
    $(document).ready(function() {
        var T24REPORT_APP = "E3D9948B-8DD9-49B3-8CCB-A6343399C62B";
        var applicationid = "<%=_applicationid %>";
        if (applicationid == T24REPORT_APP) $("#chkUpload").attr("disabled", "false");
    });

</script>
</asp:Content>
<asp:Content ID="frmB1" ContentPlaceHolderID="ButtonExtendBefore" Runat="Server">
</asp:Content>
<asp:Content ID="frmB2" ContentPlaceHolderID="ButtonExtend" Runat="Server">
</asp:Content>

