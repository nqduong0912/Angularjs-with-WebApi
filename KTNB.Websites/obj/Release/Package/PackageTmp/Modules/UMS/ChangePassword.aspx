<%@ Page Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" Inherits="VPB_PROMOTION.UMS.UMS_ChangePassword" Codebehind="ChangePassword.aspx.cs" %>
<asp:Content runat="server" ID="UserProfile" ContentPlaceHolderID="FormContent">
    <table cellpadding="5" cellspacing="0">
        <tr class="lblNormal">        
            <td>(<font color='red'>*</font>) Mật khẩu cũ</td>
            <td><asp:TextBox runat="server" ID="MatKhauCu" SkinID="TextBoxRequired" TextMode="Password"></asp:TextBox></td>            
        </tr>
        <tr class="lblNormal">
            <td>(<font color='red'>*</font>) Mật khẩu mới</td>
            <td><asp:TextBox runat="server" ID="MatKhauMoi" SkinID="TextBoxRequired" TextMode="Password"></asp:TextBox></td>
        </tr>
        <tr>
        <td>(<font color='red'>*</font>) Nhập lại mật khẩu mới</td>
            <td><asp:TextBox runat="server" ID="ReMatKhauMoi" SkinID="TextBoxRequired" TextMode="Password"></asp:TextBox></td>
        </tr>
        <tr>
            <td><asp:Label runat="server" ID="Mess" CssClass="lblCaption"></asp:Label></td>
            <td><asp:CompareValidator runat="server" ID="cpr" ControlToValidate="ReMatKhauMoi" ControlToCompare="MatKhauMoi" ErrorMessage="Không nhập đúng mật khẩu."></asp:CompareValidator></td>
            
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ID="buttonEx" ContentPlaceHolderID="ButtonExtend">
    <asp:Button runat="server" SkinID="SaveButton" OnClick="changePassword" />
    <input type="button" value="valid form" onclick="validatingform();" style="display:none" />
    <script type="text/javascript">
        function validatingform() {
            $(".TextBoxRequired").each(function() {
                var svalue = $(this).val();
                if (svalue == "") {
                    $(this).attr("style", "background-color:yellow");
                    $(this).focus();
                }
                else {
                    $(this).attr("style", "background-color:white");
                }

            });
        }
    </script>
</asp:Content>
