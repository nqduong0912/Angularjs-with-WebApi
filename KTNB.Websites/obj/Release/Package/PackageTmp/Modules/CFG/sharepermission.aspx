<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" Inherits="VPB_PROMOTION.CFG.Modules_CFG_sharepermission" Codebehind="sharepermission.aspx.cs" %>
<asp:Content ID="frmContent" ContentPlaceHolderID="FormContent" Runat="Server">
<script src="../../Javascript/BackEndProcess.js"></script>
<asp:ScriptManager runat="server" ID="scrp1">
</asp:ScriptManager>
<asp:UpdatePanel runat="server" ID="pan" UpdateMode="Always">
    <ContentTemplate>
<table width="100%" class="table">
    <tr>
        <td class="lblNormal" style="width: 142px">(<font color='red'>*</font>) Ứng dụng được ủy quyền</td>
        <td class="lblCaption"><asp:Label runat="server" ID="lblComponentName"></asp:Label></td>
    </tr>
    <tr>
        <td colspan="2">
            <hr size="1" />
        </td>
    </tr>
    <tr>
        <td class="lblNormal" style="width: 142px">(<font color='red'>*</font>) Tại Chi 
            nhánh/PGD</td>
        <td class="lblCaption">
            <asp:DropDownList ID="cboCompany" runat="server" SkinID="DropDownList" AutoPostBack="true">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="lblNormal" style="width: 142px">(<font color='red'>*</font>) Vai trò được 
            ủy quyền</td>
        <td class="lblCaption">
            <asp:DropDownList ID="cboRole" runat="server" AutoPostBack="true" SkinID="DropDownList">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="lblNormal" style="width: 142px">(<font color='red'>*</font>) Người được 
            ủy quyền</td>
        <td class="lblCaption">
            <asp:DropDownList ID="cboUser" runat="server" SkinID="DropDownList">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <hr size="1" />
        </td>
    </tr>
    <tr>
        <td class="lblNormal" style="width: 142px" valign="top">(<font color='red'>*</font>) Ủy quyền</td>
        <td class="lblCaption" valign="top">
             <table align="left" cellpadding="1" cellspacing="1">
                <tr>
                    <td class="lblNormal" style="width:180px">
                        <img src="../../Images/Form/up.gif" />Upload</td>
                    
                    <td class="lblNormal" style="width:180px">
                        <img src="../../Images/Form/down.gif" />Download</td> 
                
                </tr>
                 <tr style="background-color:Olive">
                     <td class="lblNormal" align="left" style="width:180px">
                         <asp:CheckBox ID="chkUpload" runat="server" />
                     </td>
                     
                     <td class="lblNormal" align="left" style="width:180px">
                         <asp:CheckBox ID="chkDownload" runat="server" />
                     </td>
                 </tr>
             </table>
        </td>
    </tr>
    <tr>
        <td class="lblNormal" colspan="2" valign="top">
            <hr size="1" />
        </td>
    </tr>
    
</table>
</ContentTemplate>
</asp:UpdatePanel>

<table>
<tr>
        <td class="lblNormal" style="width: 142px" valign="top">
            Ngày hết hạn</td>
        <td class="lblNormal" valign="top">
            <asp:TextBox ID="txtFinishedDate" runat="server" SkinID="TextBoxDate" Width="65px"></asp:TextBox>
		        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
		        <input type="checkbox" id="nolimit" />Không thời hạn
        </td>
    </tr>
</table>
<script type="text/javascript">
//    $(document).ready(function() {
//        var T24REPORT_APP = "E3D9948B-8DD9-49B3-8CCB-A6343399C62B";
//        var applicationid = "<%=_applicationid %>";
//        if (applicationid == T24REPORT_APP) $("#ctl00_FormContent_chkUpload").attr("disabled", "false");
//    });
</script>
</asp:Content>
<asp:Content ID="frmB1" ContentPlaceHolderID="ButtonExtendBefore" Runat="Server">
</asp:Content>
<asp:Content ID="frmB2" ContentPlaceHolderID="ButtonExtend" Runat="Server">
</asp:Content>

