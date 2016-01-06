<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="DotKiemToan_Load.aspx.cs" Inherits="VPB_TDHS.Modules.DMS.DotKiemToan_Load" %>
<%@ Register Assembly="C1.Web.C1Input.2" Namespace="C1.Web.C1Input" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <table width="100%">
    <tr>
        <td style="width: 222px">Năm</td>
        <td>
            <cc1:C1WebNumericEdit ID="ID8_23FB0D9B_6A02_44B0_A1AA_CEFA895CD9C1" 
                runat="server" SkinID="C1WebNumeric" Width="6%" Text="2013" 
                Font-Size="Small" DecimalPlaces="0" Culture="en-US"  ThousandsSeparator="false" 
                Value="2013" SmartInputMode="false" MaxValue="2020" MinValue="2013" UpDownAlign="None"></cc1:C1WebNumericEdit>
        </td>
    </tr>
    <tr>
        <td style="width: 222px">Tên đợt kiểm toán</td>
        <td>
            <asp:TextBox ID="ID8_63A0C4B1_2088_4994_B891_2FF65EB20265" runat="server" 
                SkinID="TextBoxRequired" style="width:90%"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="width: 222px">Loại đối tượng kiểm toán</td>
        <td>
            <asp:DropDownList ID="ID8_737694DF_DE17_4FF9_AE15_70E197C83593" runat="server" 
                SkinID="DropDownList">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td style="width: 222px">Đối tượng kiểm toán</td>
        <td>
            <asp:DropDownList ID="ID8_2A4CA2AD_0282_4D57_86AC_D973D281EF54" runat="server" 
                SkinID="DropDownList">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td style="width: 222px">Quy mô</td>
        <td>
            <asp:DropDownList ID="ID8_06991CFA_158B_4460_8B7E_EC010C14DFE4" runat="server" 
                SkinID="DropDownList">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td style="width: 222px">Thời gian</td>
        <td>
            <asp:TextBox ID="ID8_B23A439D_95D3_466E_8AD1_CDDAC260CB27" runat="server" 
                SkinID="TextBox" style="width:15%"></asp:TextBox>
        </td>
    </tr>
    
     <tr>
        <td style="width: 222px">Mục tiêu</td>
        <td>
            <asp:TextBox ID="ID8_7D31CEB6_69CC_4F8E_9BE6_38762D7C30C6" runat="server" 
                SkinID="TextBox" style="width:15%"></asp:TextBox>
        </td>
    </tr>

     <tr>
        <td style="width: 222px">Phạm vi</td>
        <td>
            <asp:TextBox ID="ID8_46C1BE6C_68F3_47B9_B0FB_DEC78831AEFF" runat="server" 
                SkinID="TextBox" style="width:15%"></asp:TextBox>
        </td>
    </tr>

</table>
<script type="text/javascript">
    var _documentid;
    /*********************************************************/
    $(document).ready(function () {
        //do smt here
    });
    /*********************************************************/
    function preparesavedoc(documentID, doctypeID) {
        var ten = GetSvrCtlValue("ID8_F4018BE5_84AD_4FE2_B3AF_5BC3F5981CEA");
        var url = "DotKiemToan_Load.aspx";
        var query = "act=checkvalue";
        query += "&p=F4018BE5-84AD-4FE2-B3AF-5BC3F5981CEA";
        query += "&v=" + ten;
        StartProcessingForm("");
        $.ajax({
            type: "POST",
            url: url,
            data: query,
            success: function (data) {
                FinishProcessingForm();
                if (data == "0")
                    savedocument(documentID, doctypeID, savedoc_success, savedoc_error);
                else
                    alert("Dữ liệu nhập bị trùng lặp. Kiểm tra lại.");
            }
        });
    }
    function savedoc_success() {
        alert(MSG_ADD_OK);
    }
    function savedoc_error() {
        alert(MSG_ADD_ER);
    }
    function update_success() {
        alert(MSG_EDIT_OK);
    }
    function update_error() {
        alert(MSG_EDIT_ER);
    }
    function delete_success() {
        alert(MSG_DEL_OK);
    }
    function delete_error() {
        alert(MSG_DEL_ER);
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ButtonExtendBefore" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ButtonExtend" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FormFooter" runat="server">
</asp:Content>
