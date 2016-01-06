<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="HinhThucXuLyLuatLaoDong_Input.aspx.cs" Inherits="VPB_KTNB.Modules.Lookup.DanhMuc.HinhThucXuLy.HinhThucXuLyLuatLaoDong_Input" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <div class="form-horizontal">
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Hình thức xử lý<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_BFB564E4_2E62_4755_AB4C_26391E21BB43" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Diễn giải</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_36DD6FCE_361D_4643_B04E_46CA161971A0" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
    </div>
    <%--  <table width="100%">
        <tr>
            <td style="width: 222px">Hình thức xử lý</td>
            <td>
                <asp:TextBox ID="ID8_BFB564E4_2E62_4755_AB4C_26391E21BB43" runat="server"
                    SkinID="TextBoxRequired"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Diễn giải</td>
            <td>
                <asp:TextBox ID="ID8_36DD6FCE_361D_4643_B04E_46CA161971A0" runat="server"
                    SkinID="TextBox"></asp:TextBox>
            </td>
        </tr>

    </table>--%>
    <script type="text/javascript">
        var _documentid;
        /*********************************************************/
        $(document).ready(function () {
            //do smt here
        });
        /*********************************************************/
        function preparesavedoc(documentID, doctypeID) {
            var ten = GetSvrCtlValue("ID8_BFB564E4_2E62_4755_AB4C_26391E21BB43");
            var diengiai = GetSvrCtlValue("ID8_36DD6FCE_361D_4643_B04E_46CA161971A0");
            var url = "HinhThucXuLyLuatLaoDong_Input.aspx";
            var query = "act=checkvalue";
            query += "&p=BFB564E4-2E62-4755-AB4C-26391E21BB43";
            query += "&v=" + ten;
            query += "&diengiai=" + diengiai;

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
            var ten = GetSvrCtlValue("ID8_BFB564E4_2E62_4755_AB4C_26391E21BB43");
            var diengiai = GetSvrCtlValue("ID8_36DD6FCE_361D_4643_B04E_46CA161971A0");
            var url = "HinhThucXuLyLuatLaoDong_Input.aspx";
            var query = "act=checkvalueupdate";
            query += "&p=BFB564E4-2E62-4755-AB4C-26391E21BB43";
            query += "&v=" + ten;
            query += "&diengiai=" + diengiai;
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
            window.location.href = 'HinhThucXuLyLuatLaoDong.aspx';
        }
        function savedoc_error() {
            alert(MSG_ADD_ER);
        }
        function update_success() {
            alert(MSG_EDIT_OK);
            window.location.href = 'HinhThucXuLyLuatLaoDong.aspx';
        }
        function update_error() {
            alert(MSG_EDIT_ER);
        }
        function delete_success() {
            alert(MSG_DEL_OK);
            window.location.href = 'HinhThucXuLyLuatLaoDong.aspx';
        }
        function delete_error() {
            alert(MSG_DEL_ER);
        }
    </script>
</asp:Content>
