<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true"
    CodeBehind="BaoCaoKeHoachKiemToan.aspx.cs" Inherits="VPB_TDHS.Modules.DMS.BaoCaoKeHoachKiemToan" %>

<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <asp:ScriptManager runat="server" ID="ScriptManager1">
    </asp:ScriptManager>
    <table width="60%">
        <tr>
            <td style="width: 222px">
                Đối tượng kiểm toán
            </td>
            <td>
                <asp:TextBox ID="DOCSTATUS" Text="2" Style="display: none;" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtDoiTuongKiemToan" runat="server" SkinID="TextBoxReadOnly"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">
                Đợt kiểm toán
            </td>
            <td>
                <asp:TextBox ID="txtDotKiemToan" runat="server" SkinID="TextBoxReadOnly"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">
                Thông tin báo cáo
            </td>
            <td>
                <input type="checkbox" value="" />Danh sách đoàn kiểm toán
            </td>
            <td>
                <input type="checkbox" value="" />Phân tích chi tiết
            </td>
        </tr>
        <tr>
            <td style="width: 222px">
                
            </td>
            <td>
                <input type="checkbox" value="" />Thông tin đợt kiểm toán
            </td>
            <td>
                <input type="checkbox" value="" />Hồ sơ rủi ro
            </td>
        </tr>
        <tr>
            <td style="width: 222px">
                
            </td>
            <td>
                <input type="checkbox" value="" />Phân tích/Phân công sơ bộ
            </td>
            <td>
                <input type="checkbox" value="" />Chương trình kiểm toán
            </td>
        </tr>
        <tr>
            <td style="width: 222px">
                Định dạng báo cáo
            </td>
            <td>
                <input type="radio" name="report-format" value="male">Excel
            </td>
            <td>
                
            </td>
        </tr>
        <tr>
            <td style="width: 222px">
                
            </td>
            <td>
                <input type="radio" name="report-format" value="male">Word
            </td>
            <td>
                
            </td>
        </tr>
        <tr>
            <td style="width: 222px">
                
            </td>
            <td>
                <input type="radio" name="report-format" value="male">PDF
            </td>
            <td>
                
            </td>
        </tr>
    </table>
    <input id="btn-submit" type="button" value="Submit" onclick="Submit()" class="InsertButton" />
    <script type="text/javascript">
        var _documentid;
        var MSG_CONFIRM_DEL_HOSOSOBO = "Bạn có muốn xóa phân tích này?";
        var MSG_CONFIRM_SUBMIT_HOSOSOBO = "Bạn có muốn submit toàn bộ việc phân tích sơ bộ?";
        MSG_CONFIRM_SUBMIT_HOSOSOBO
        /*********************************************************/
        $(document).ready(function () {
            //do smt here
        });
        /*********************************************************/

        function newform() {
            //            window.location.href = "ChiTietHoSoPhanTichSoBo_Load.aspx";
        }
        function savedoc_success() {
            alert(MSG_ADD_OK);

        }
        function savedoc_error() {
            alert(MSG_ADD_ER);
        }
        function update_success() {
            alert(MSG_EDIT_OK);
            //        window.location.href = 'LoaiDoiTuongKiemToan.aspx';
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
