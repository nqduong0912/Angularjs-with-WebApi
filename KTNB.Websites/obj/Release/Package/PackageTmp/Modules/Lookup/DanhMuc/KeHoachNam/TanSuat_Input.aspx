<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="TanSuat_Input.aspx.cs" Inherits="VPB_KTNB.Modules.Lookup.DanhMuc.KeHoachNam.TanSuat_Input" %>
<%@ Register Assembly="C1.Web.C1Input.2" Namespace="C1.Web.C1Input" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <div class="form-horizontal">
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Tên tần suất <span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_10F06128_E9E7_4FCB_8188_9A61911893DE" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>
       
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Thời gian tương ứng <span class="star-red">*</span></label>
            <div class="col-sm-1">
                <cc1:C1WebNumericEdit ID="ID6_A11FB5E1_3DAC_4DDA_950F_9A976E43BF3F" runat="server"
                    SkinID="C1WebNumeric" Width="100%" Font-Size="Small" DecimalPlaces="0" Culture="en-US"
                    ThousandsSeparator="false" Value="1" SmartInputMode="false" MaxValue="100" MinValue="0"
                    UpDownAlign="None" Height="34px" CssClass="form-control">
                </cc1:C1WebNumericEdit>
                
            </div>
            <div class="col-sm-1 col-sm-offset-1 control-label">
                <label>tháng</label>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        var _documentid;
        /*********************************************************/
        $(document).ready(function () {
            //do smt here
        });
        /*********************************************************/
        function preparesavedoc(documentID, doctypeID) {
            var ten = GetSvrCtlValue("ID8_10F06128_E9E7_4FCB_8188_9A61911893DE");
            var thoigian = GetSvrCtlValue("ID6_A11FB5E1_3DAC_4DDA_950F_9A976E43BF3F");
            if (ten.trim().length == 0) {
                alert("Nhập tên tần suất.");
                FocusSvrCtl("ID8_10F06128_E9E7_4FCB_8188_9A61911893DE");
                return false;
            }
            if (thoigian.trim().length == 0) {
                alert("Nhập thời gian tần suất.");
                FocusSvrCtl("ID6_A11FB5E1_3DAC_4DDA_950F_9A976E43BF3F");
                return false;
            }
            if (parseInt(thoigian.trim()) <= 0 ) {
                alert("Thời gian phải là số nguyên dương.");
                FocusSvrCtl("ID6_A11FB5E1_3DAC_4DDA_950F_9A976E43BF3F");
                return false;
            }
            var url = "TanSuat_Input.aspx";
            var query = "act=checkvalue";
            query += "&p=10F06128-E9E7-4FCB-8188-9A61911893DE";
            query += "&v=" + ten;

            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (data) {
                    FinishProcessingForm();
                    if (data == "0") {
                        savedocument(documentID, doctypeID, savedoc_success, savedoc_error);
                    }
                    else
                        alert("Dữ liệu nhập bị trùng lặp. Kiểm tra lại.");
                }
            });
        }

        function prepareupdatedoc(documentID) {
            var ten = GetSvrCtlValue("ID8_10F06128_E9E7_4FCB_8188_9A61911893DE");
            var thoigian = GetSvrCtlValue("ID6_A11FB5E1_3DAC_4DDA_950F_9A976E43BF3F");
            if (ten.trim().length == 0) {
                alert("Nhập tên tần suất.");
                FocusSvrCtl("ID8_10F06128_E9E7_4FCB_8188_9A61911893DE");
                return false;
            }
            if (thoigian.trim().length == 0) {
                alert("Nhập thời gian tần suất.");
                FocusSvrCtl("ID6_A11FB5E1_3DAC_4DDA_950F_9A976E43BF3F");
                return false;
            }
            var url = "TanSuat_Input.aspx";
            var query = "act=checkvalueupdate";
            query += "&p=10F06128-E9E7-4FCB-8188-9A61911893DE";
            query += "&v=" + ten;
            query += "&doc=" + documentID;
            StartProcessingForm("");

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

        function savedoc_success() {
            alert(MSG_ADD_OK);
            window.location.href = 'TanSuat.aspx?a=dbb1170f-fb15-4585-9c21-e21c32d4de86';
        }
        function savedoc_error() {
            alert(MSG_ADD_ER);
        }
        function update_success() {
            alert(MSG_EDIT_OK);
            window.location.href = 'TanSuat.aspx?a=dbb1170f-fb15-4585-9c21-e21c32d4de86';
        }
        function update_error() {
            alert(MSG_EDIT_ER);
        }
        function delete_success() {
            alert(MSG_DEL_OK);
            window.location.href = 'TanSuat.aspx?a=dbb1170f-fb15-4585-9c21-e21c32d4de86';
        }
        function delete_error() {
            alert(MSG_DEL_ER);
        }
    </script>
</asp:Content>

