<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="HinhThucXuLyQuyDinhNoiBo_Input.aspx.cs" Inherits="VPB_KTNB.Modules.Lookup.DanhMuc.HinhThucXuLy.HinhThucXuLyQuyDinhNoiBo_Input" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <div class="form-horizontal">
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Hình thức xử lý<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_44A8E83C_8C52_44AC_BA97_F2EFF16F6DF6" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Diễn giải</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_6F3C2CCE_2C94_40A9_A28D_048BDC514134" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
    </div>
    <%--    <table width="100%">
        <tr>
            <td style="width: 222px">Hình thức xử lý</td>
            <td>
                <asp:TextBox ID="ID8_44A8E83C_8C52_44AC_BA97_F2EFF16F6DF6" runat="server"
                    SkinID="TextBoxRequired"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Diễn giải</td>
            <td>
                <asp:TextBox ID="ID8_6F3C2CCE_2C94_40A9_A28D_048BDC514134" runat="server"
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
            var ten = GetSvrCtlValue("ID8_44A8E83C_8C52_44AC_BA97_F2EFF16F6DF6");
            var diengiai = GetSvrCtlValue("ID8_6F3C2CCE_2C94_40A9_A28D_048BDC514134");
            var url = "HinhThucXuLyQuyDinhNoiBo_Input.aspx";
            var query = "act=checkvalue";
            query += "&p=44A8E83C-8C52-44AC-BA97-F2EFF16F6DF6";
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
            var ten = GetSvrCtlValue("ID8_44A8E83C_8C52_44AC_BA97_F2EFF16F6DF6");
            var diengiai = GetSvrCtlValue("ID8_6F3C2CCE_2C94_40A9_A28D_048BDC514134");
            var url = "HinhThucXuLyQuyDinhNoiBo_Input.aspx";
            var query = "act=checkvalueupdate";
            query += "&p=44A8E83C-8C52-44AC-BA97-F2EFF16F6DF6";
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
            window.location.href = 'HinhThucXuLyQuyDinhNoiBo.aspx';
        }
        function savedoc_error() {
            alert(MSG_ADD_ER);
        }
        function update_success() {
            alert(MSG_EDIT_OK);
            window.location.href = 'HinhThucXuLyQuyDinhNoiBo.aspx';
        }
        function update_error() {
            alert(MSG_EDIT_ER);
        }
        function delete_success() {
            alert(MSG_DEL_OK);
            window.location.href = 'HinhThucXuLyQuyDinhNoiBo.aspx';
        }
        function delete_error() {
            alert(MSG_DEL_ER);
        }
    </script>
</asp:Content>
