<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="BanDanhGia_Input.aspx.cs" Inherits="VPB_KTNB.Modules.Lookup.DanhMuc.ChucDanh.BanDanhGia_Input" %>
<%@ Register Assembly="C1.Web.C1Input.2" Namespace="C1.Web.C1Input" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <div class="form-horizontal">
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Phòng ban</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="drpPhongBan" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpPhongBan_SelectedIndexChanged" CssClass="form-control" Width="300px">
                    <asp:ListItem Text="" Value=""></asp:ListItem>
                    <asp:ListItem Text="Ban Giám Đốc Khối" Value="20AC286F-FE83-46AD-BD23-776D06CC0CB4"></asp:ListItem>
                    <asp:ListItem Text="Ban Kiểm Soát" Value="BA0735E6-09AE-4691-8010-F58FE09FB53F"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Người sử dụng</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="ID8_2D5C80C0_39AC_478A_A79E_D9F38D705786" runat="server" onchange="drpUserChanged()" CssClass="form-control" Width="300px">
                    </asp:DropDownList>
            </div>

            
        </div>
        <div class="form-group" visible="false">
            <asp:TextBox ID="ID8_AF348562_CD57_4F06_B97A_044B6E25C8CC" Visible="false" runat="server" CssClass="form-control" Width="300px" ></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Trọng số</label>
            <div class="col-sm-6">
                <cc1:C1WebNumericEdit ID="ID6_38F9D072_1864_43C1_92FC_8DDABEDC8DEB" runat="server"
                    SkinID="C1WebNumeric" Width="20%" Font-Size="Small" DecimalPlaces="0" Culture="en-US"
                    ThousandsSeparator="false" Value="1" SmartInputMode="false" MaxValue="1000" MinValue="0"
                    UpDownAlign="None" Height="34px" CssClass="form-control">
                </cc1:C1WebNumericEdit>
            </div>
        </div>
        
    </div>
 
    <script type="text/javascript">
        var _documentid;
        /*********************************************************/
        $(document).ready(function () {
            //do smt here
        });
        function drpUserChanged() {
            
            var name = GetSvrCtlText("ID8_2D5C80C0_39AC_478A_A79E_D9F38D705786");
            //document.getElementById("ID8_AF348562_CD57_4F06_B97A_044B6E25C8CC").value = "aaa";
            alert(name);
            SetSvrCtlValue("ID8_AF348562_CD57_4F06_B97A_044B6E25C8CC", name);
        }
        
        /*********************************************************/
        function preparesavedoc(documentID, doctypeID) {
            var trongso = GetSvrCtlValue("ID6_38F9D072_1864_43C1_92FC_8DDABEDC8DEB");
            if (parseInt(trongso) > 100)
            {
                alert("Giá trị trọng số không lớn hơn 100.");
                return;
            }
            var id = GetSvrCtlValue("ID8_2D5C80C0_39AC_478A_A79E_D9F38D705786");
            var url = "BanDanhGia_Input.aspx";
            var query = "act=checkvalue";
            query += "&p=2D5C80C0-39AC-478A-A79E-D9F38D705786";
            query += "&v=" + id;

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
            var id = GetSvrCtlValue("ID8_2D5C80C0_39AC_478A_A79E_D9F38D705786");
            var url = "BanDanhGia_Input.aspx";
            var query = "act=checkvalueupdate";
            
            query += "&p=2D5C80C0-39AC-478A-A79E-D9F38D705786";
            query += "&v=" + id;
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
            window.location.href = 'BanDanhGia.aspx';
        }
        function savedoc_error() {
            alert(MSG_ADD_ER);
        }
        function update_success() {
            alert(MSG_EDIT_OK);
            window.location.href = 'BanDanhGia.aspx';
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

