<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="NhomHanhViViPham_Input.aspx.cs" Inherits="VPB_KTNB.Modules.Lookup.DanhMuc.ViPham.NhomHanhViViPham_Input" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <div class="form-horizontal">
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Quy trình /Activity /Sản phẩm<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_4DCBD2E6_4C65_4FE3_839C_C54C79AADB74" runat="server" CssClass="form-control" ></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Diễn giải</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_222D4F00_4E85_4CA6_B052_3287FCFE0FA4" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Mảng nghiệp vụ</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="ID8_D94F69C8_D205_4FB3_9305_593B67B70C10" runat="server" CssClass="form-control" Width="300px">
                    </asp:DropDownList>
            </div>
        </div>
    </div>
    <%--<table width="100%">
        <tr>
            <td style="width: 222px">Tên nhóm</td>
            <td>
                <asp:TextBox ID="ID8_4DCBD2E6_4C65_4FE3_839C_C54C79AADB74" runat="server"
                    SkinID="TextBoxRequired"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Diễn giải</td>
            <td>
                <asp:TextBox ID="ID8_222D4F00_4E85_4CA6_B052_3287FCFE0FA4" runat="server"
                    SkinID="TextBox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Nghiệp vụ vi phạm
            </td>
            <td>
                <asp:DropDownList ID="ID8_D94F69C8_D205_4FB3_9305_593B67B70C10" runat="server" SkinID="DropDownListRequired">
                </asp:DropDownList>
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
            var ten = GetSvrCtlValue("ID8_4DCBD2E6_4C65_4FE3_839C_C54C79AADB74");
            var diengiai = GetSvrCtlValue("ID8_222D4F00_4E85_4CA6_B052_3287FCFE0FA4");
            var url = "NhomHanhViViPham_Input.aspx";
            var query = "act=checkvalue";
            query += "&p=4DCBD2E6-4C65-4FE3-839C-C54C79AADB74";
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
            var ten = GetSvrCtlValue("ID8_4DCBD2E6_4C65_4FE3_839C_C54C79AADB74");
            var diengiai = GetSvrCtlValue("ID8_222D4F00_4E85_4CA6_B052_3287FCFE0FA4");
            var url = "NhomHanhViViPham_Input.aspx";
            var query = "act=checkvalueupdate";
            query += "&p=4DCBD2E6-4C65-4FE3-839C-C54C79AADB74";
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
            window.location.href = 'NhomHanhViViPham.aspx';
        }
        function savedoc_error() {
            alert(MSG_ADD_ER);
        }
        function update_success() {
            alert(MSG_EDIT_OK);
            window.location.href = 'NhomHanhViViPham.aspx';
        }
        function update_error() {
            alert(MSG_EDIT_ER);
        }
        function delete_success() {
            alert(MSG_DEL_OK);
            window.location.href = 'NhomHanhViViPham.aspx';
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
