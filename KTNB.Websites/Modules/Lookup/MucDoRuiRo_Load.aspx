<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true"
    CodeBehind="MucDoRuiRo_Load.aspx.cs" Inherits="VPB_TDHS.Modules.Lookup.MucDoRuiRo_Load" %>

<%@ Register Assembly="C1.Web.C1Input.2" Namespace="C1.Web.C1Input" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <div class="form-horizontal">
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Tên mức độ rủi ro<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_F92EA020_7FB3_41CA_A233_8CDFE91D0C77" runat="server" CssClass="form-control" ></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Diễn giải</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_7B451BD1_204F_42A1_82B8_37FB9CC3710D" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Điểm rủi ro (Chặn trên)<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <cc1:C1WebNumericEdit ID="ID6_6EED4C06_6D40_4926_BD2B_4685CF47C40C" runat="server"
                    SkinID="C1WebNumeric" Width="10%" Font-Size="Small" DecimalPlaces="0" Culture="en-US"
                    ThousandsSeparator="false" Value="1" SmartInputMode="false" MaxValue="1000" MinValue="0"
                    UpDownAlign="None" CssClass="form-control" Height="35px" BorderColor="#48B061">
                </cc1:C1WebNumericEdit>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Điểm rủi ro (Chặn dưới)<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <cc1:C1WebNumericEdit ID="ID6_1F9EA2E5_16B2_4080_B907_B195D4A540D6" runat="server"
                    SkinID="C1WebNumeric" Width="10%" Font-Size="Small" DecimalPlaces="0" Culture="en-US"
                    ThousandsSeparator="false" Value="1" SmartInputMode="false" MaxValue="1000" MinValue="0"
                    UpDownAlign="None" CssClass="form-control" Height="35px" BorderColor="#48B061">
                </cc1:C1WebNumericEdit>
            </div>
        </div>
         <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Tần suất kiểm toán<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <cc1:C1WebNumericEdit ID="ID6_5D0BE3C9_5C9D_4C16_922E_6A1CA752A15C" runat="server"
                    SkinID="C1WebNumeric" Width="10%" Font-Size="Small" DecimalPlaces="0" Culture="en-US"
                    ThousandsSeparator="false" Value="1" SmartInputMode="false" MaxValue="1000" MinValue="0"
                    UpDownAlign="None" CssClass="form-control" Height="35px" BorderColor="#48B061">
                </cc1:C1WebNumericEdit>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Trạng thái</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="DOCSTATUS" runat="server" CssClass="form-control" Width="100px">
                    </asp:DropDownList>
            </div>
        </div> 
    </div>
<%--    <table width="100%">
        <%--<tr>
            <td style="width: 222px">Điểm rủi ro (Chặn trên)
            </td>
            <td>
                <cc1:C1WebNumericEdit ID="ID6_6EED4C06_6D40_4926_BD2B_4685CF47C40C" runat="server"
                    SkinID="C1WebNumeric" Width="6%" Font-Size="Small" DecimalPlaces="0" Culture="en-US"
                    ThousandsSeparator="false" Value="1" SmartInputMode="false" MaxValue="1000" MinValue="0"
                    UpDownAlign="None">
                </cc1:C1WebNumericEdit>
            </td>
        </tr>--%>
        <%--<tr>
            <td style="width: 222px">Điểm rủi ro (Chặn dưới)
            </td>
            <td>
                <cc1:C1WebNumericEdit ID="ID6_1F9EA2E5_16B2_4080_B907_B195D4A540D6" runat="server"
                    SkinID="C1WebNumeric" Width="6%" Font-Size="Small" DecimalPlaces="0" Culture="en-US"
                    ThousandsSeparator="false" Value="1" SmartInputMode="false" MaxValue="1000" MinValue="0"
                    UpDownAlign="None">
                </cc1:C1WebNumericEdit>

            </td>
        </tr>--%>
        <%--<tr>
            <td style="width: 222px">Tần suất kiểm toán
            </td>
            <td>
                <cc1:C1WebNumericEdit ID="ID6_5D0BE3C9_5C9D_4C16_922E_6A1CA752A15C" runat="server"
                    SkinID="C1WebNumeric" Width="6%" Font-Size="Small" DecimalPlaces="0" Culture="en-US"
                    ThousandsSeparator="false" Value="1" SmartInputMode="false" MaxValue="100" MinValue="0"
                    UpDownAlign="None">
                </cc1:C1WebNumericEdit>
            </td>
        </tr>--%>
        <%--<tr>
            <td style="width: 222px">Trạng thái
            </td>
            <td>
                <asp:DropDownList ID="DOCSTATUS" runat="server" SkinID="DropDownListRequired">
                </asp:DropDownList>
            </td>
        </tr>--%>
    <%--</table>--%>
    <script type="text/javascript">
        var _documentid;
        /*********************************************************/
        $(document).ready(function () {
            //do smt here
        });
        /*********************************************************/
        function preparesavedoc(documentID, doctypeID) {
            var ten = GetSvrCtlValue("ID8_F92EA020_7FB3_41CA_A233_8CDFE91D0C77");
            var url = "MucDoRuiRo_Load.aspx";
            var query = "act=checkvalue";
            query += "&p=F92EA020-7FB3-41CA-A233-8CDFE91D0C77";
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

        function prepareupdatedoc(documentID) {
            var ten = GetSvrCtlValue("ID8_F92EA020_7FB3_41CA_A233_8CDFE91D0C77");
            var url = "MucDoRuiRo_Load.aspx";
            var query = "act=checkvalueupdate";
            query += "&p=F92EA020-7FB3-41CA-A233-8CDFE91D0C77";
            query += "&v=" + ten;
            StartProcessingForm("");
            query += "&doc=" + documentID;
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (data) {
                    FinishProcessingForm();
                    if (data == "0")
                        updatedocument(documentID, update_success, update_error);
                    else
                        alert("Dữ liệu nhập bị trùng lặp. Kiểm tra lại.");
                }
            });
        }


        function adddoc(documentID, doctypeID) {
            var diemchantren = GetSvrCtlValue("ID6_6EED4C06_6D40_4926_BD2B_4685CF47C40C");
            var diemchanduoi = GetSvrCtlValue("ID6_1F9EA2E5_16B2_4080_B907_B195D4A540D6");
            if (parseInt(diemchantren) <= parseInt(diemchanduoi)) {
                alert('Điểm rủi ro(chặn trên) phải lớn hơn điểm rủi ro (chặn dưới)');
                return false;
            }

            var ten = GetSvrCtlValue("ID8_F92EA020_7FB3_41CA_A233_8CDFE91D0C77");
            var url = "MucDoRuiRo_Load.aspx";
            var query = "act=adddoc";
            query += "&p=F92EA020-7FB3-41CA-A233-8CDFE91D0C77";
            query += "&v=" + ten;
            query += "&diemchantren=" + diemchantren;
            query += "&diemchanduoi=" + diemchanduoi;
            query += "&tenruiro=" + ten;

            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (data) {
                    FinishProcessingForm();
                    if (data == "1")
                        savedocument(documentID, doctypeID, savedoc_success, savedoc_error);
                    else if (data == "0")
                        alert("Điểm rủi ro (chặn trên) hoặc (chặn dưới) không thỏa mãn. Kiểm tra lại.");
                    else
                        alert("Dữ liệu nhập bị trùng lặp. Kiểm tra lại.");
                }
            });
        }

        function updatedoc(documentID) {
            var diemchantren = GetSvrCtlValue("ID6_6EED4C06_6D40_4926_BD2B_4685CF47C40C");
            var diemchanduoi = GetSvrCtlValue("ID6_1F9EA2E5_16B2_4080_B907_B195D4A540D6");
            if (parseInt(diemchantren) <= parseInt(diemchanduoi)) {
                alert('Điểm rủi ro(chặn trên) phải lớn hơn điểm rủi ro (chặn dưới)');
                return false;
            }


            var ten = GetSvrCtlValue("ID8_F92EA020_7FB3_41CA_A233_8CDFE91D0C77");
            var url = "MucDoRuiRo_Load.aspx";
            var query = "act=updatedoc";
            query += "&p=F92EA020-7FB3-41CA-A233-8CDFE91D0C77";
            query += "&v=" + ten;
            query += "&diemchantren=" + diemchantren;
            query += "&diemchanduoi=" + diemchanduoi;
            query += "&tenruiro=" + ten;
            query += "&doc=" + documentID;
            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (data) {
                    FinishProcessingForm();
                    if (data == "1")
                        updatedocument(documentID, update_success, update_error);
                    else if (data == "0")
                        alert("Điểm rủi ro (chặn trên) hoặc (chặn dưới) không thỏa mãn. Kiểm tra lại.");
                    else
                        alert("Dữ liệu nhập bị trùng lặp. Kiểm tra lại.");
                }
            });
        }

        function savedoc_success() {
            alert(MSG_ADD_OK);
            window.location.href = "MucDoRuiRo.aspx";
        }
        function savedoc_error() {
            alert(MSG_ADD_ER);
        }
        function update_success() {
            alert(MSG_EDIT_OK);
            window.location.href = "MucDoRuiRo.aspx";
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
