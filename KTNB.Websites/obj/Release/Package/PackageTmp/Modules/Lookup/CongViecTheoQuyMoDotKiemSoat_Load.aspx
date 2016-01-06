<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true"
    CodeBehind="CongViecTheoQuyMoDotKiemSoat_Load.aspx.cs" Inherits="VPB_TDHS.Modules.Lookup.CongViecTheoQuyMoDotKiemSoat_Load" %>

<%@ Register Assembly="C1.Web.C1Input.2" Namespace="C1.Web.C1Input" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <table width="100%">
        <tr>
            <td style="width: 222px">
                Quy mô đợt kiểm toán
            </td>
            <td>
                <asp:DropDownList ID="ID8_D0C2F31F_A7AA_4164_941C_451AFB331A46" runat="server" SkinID="DropDownListRequired">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">
                Tên
            </td>
            <td>
                <asp:TextBox ID="ID8_E3045993_F205_47E3_900B_E78BCF4EC411" runat="server" SkinID="TextBoxRequired"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">
                Số lượng trưởng đoàn
            </td>
            <td>
                <cc1:C1WebNumericEdit ID="ID6_0B2F41A9_0F2A_4FD1_B116_3FF605CB71E4" runat="server"
                    SkinID="C1WebNumeric" Width="6%"  Font-Size="Small" DecimalPlaces="0"
                    Culture="en-US" ThousandsSeparator="false" Value="1" SmartInputMode="false" MaxValue="100"
                    MinValue="0" UpDownAlign="None">
                </cc1:C1WebNumericEdit>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">
                Số ngày(Trưởng đoàn)
            </td>
            <td>
                <cc1:C1WebNumericEdit ID="ID6_5AC88958_61BE_403E_AFA2_5EAB6AE2C3F7" runat="server"
                    SkinID="C1WebNumeric" Width="6%"  Font-Size="Small" DecimalPlaces="0"
                    Culture="en-US" ThousandsSeparator="false" Value="1" SmartInputMode="false" MaxValue="100"
                    MinValue="0" UpDownAlign="None">
                </cc1:C1WebNumericEdit>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">
                Số lượng nhân viên
            </td>
            <td>
                <cc1:C1WebNumericEdit ID="ID6_0DAE6861_DD7B_458D_B257_76F38A840B3B" runat="server"
                    SkinID="C1WebNumeric" Width="6%" Text="2013" Font-Size="Small" DecimalPlaces="0"
                    Culture="en-US" ThousandsSeparator="false" Value="1" SmartInputMode="false" MaxValue="100"
                    MinValue="1" UpDownAlign="None">
                </cc1:C1WebNumericEdit>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">
                Số ngày (Nhân viên)
            </td>
            <td>
                <cc1:C1WebNumericEdit ID="ID6_A5AF2CEF_FDDE_430D_A90E_7E6B5CB3FDD9" runat="server"
                    SkinID="C1WebNumeric" Width="6%" Text="2013" Font-Size="Small" DecimalPlaces="0"
                    Culture="en-US" ThousandsSeparator="false" Value="1" SmartInputMode="false" MaxValue="100"
                    MinValue="1" UpDownAlign="None">
                </cc1:C1WebNumericEdit>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">
                Số lượng Lãnh đạo Khối
            </td>
            <td>
                <cc1:C1WebNumericEdit ID="ID6_BD07C2F0_3130_4623_BB3A_A16B05A33D9A" runat="server"
                    SkinID="C1WebNumeric" Width="6%" Text="2013" Font-Size="Small" DecimalPlaces="0"
                    Culture="en-US" ThousandsSeparator="false" Value="1" SmartInputMode="false" MaxValue="100"
                    MinValue="1" UpDownAlign="None">
                </cc1:C1WebNumericEdit>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">
                Số ngày (Lãnh đạo Khối)
            </td>
            <td>
                <cc1:C1WebNumericEdit ID="ID6_60555BB9_EE34_485E_878D_D2B7BF154005" runat="server"
                    SkinID="C1WebNumeric" Width="6%" Text="2013" Font-Size="Small" DecimalPlaces="0"
                    Culture="en-US" ThousandsSeparator="false" Value="1" SmartInputMode="false" MaxValue="100"
                    MinValue="1" UpDownAlign="None">
                </cc1:C1WebNumericEdit>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">
                Diễn giải
            </td>
            <td>
                <asp:TextBox ID="ID8_5BE0D294_3F17_4E99_AAA9_80FE0DC7AF4C" runat="server" SkinID="TextBox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">
                Trạng thái
            </td>
            <td>
                <asp:DropDownList ID="DOCSTATUS" runat="server" SkinID="DropDownListRequired">
                </asp:DropDownList>
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
            var ten = GetSvrCtlValue("ID8_E3045993_F205_47E3_900B_E78BCF4EC411");
            var url = "CongViecTheoQuyMoDotKiemSoat_Load.aspx";
            var query = "act=checkvalue";
            query += "&p=E3045993-F205-47E3-900B-E78BCF4EC411";
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
