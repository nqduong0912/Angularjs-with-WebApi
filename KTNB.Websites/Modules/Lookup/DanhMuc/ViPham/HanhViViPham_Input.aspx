<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="HanhViViPham_Input.aspx.cs" Inherits="VPB_KTNB.Modules.Lookup.DanhMuc.ViPham.HanhViViPham_Input" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <div class="form-horizontal">

        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Tên hành vi<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_273EA18C_C3FD_4C7D_A9A6_BEDB6C7BE70A" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Diễn giải</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_B9AEBA83_3618_4D64_8848_D38B1792CBAE" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Nhóm hành vi vi phạm</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="ID8_67A48D08_05D6_4F52_B155_84B9284A978D" runat="server" CssClass="form-control" Width="300px">
                </asp:DropDownList>
            </div>
        </div>
    </div>
    <%--<table width="100%">
        <tr>
            <td style="width: 222px">Tên hành vi</td>
            <td>
                <asp:TextBox ID="ID8_273EA18C_C3FD_4C7D_A9A6_BEDB6C7BE70A" runat="server"
                    SkinID="TextBoxRequired"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Diễn giải</td>
            <td>
                <asp:TextBox ID="ID8_B9AEBA83_3618_4D64_8848_D38B1792CBAE" runat="server"
                    SkinID="TextBox"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td style="width: 222px">Nhóm hành vi vi phạm
            </td>
            <td>
                <asp:DropDownList ID="ID8_67A48D08_05D6_4F52_B155_84B9284A978D" runat="server" SkinID="DropDownListRequired">
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
            var ten = GetSvrCtlValue("ID8_273EA18C_C3FD_4C7D_A9A6_BEDB6C7BE70A");
            var diengiai = GetSvrCtlValue("ID8_B9AEBA83_3618_4D64_8848_D38B1792CBAE");
            var url = "HanhViViPham_Input.aspx";
            var query = "act=checkvalue";
            query += "&p=273EA18C-C3FD-4C7D-A9A6-BEDB6C7BE70A";
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
            var ten = GetSvrCtlValue("ID8_273EA18C_C3FD_4C7D_A9A6_BEDB6C7BE70A");
            var diengiai = GetSvrCtlValue("ID8_B9AEBA83_3618_4D64_8848_D38B1792CBAE");
            var url = "HanhViViPham_Input.aspx";
            var query = "act=checkvalueupdate";
            query += "&p=273EA18C-C3FD-4C7D-A9A6-BEDB6C7BE70A";
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
            window.location.href = 'HanhViViPham.aspx';
        }
        function savedoc_error() {
            alert(MSG_ADD_ER);
        }
        function update_success() {
            alert(MSG_EDIT_OK);
            window.location.href = 'HanhViViPham.aspx';
        }
        function update_error() {
            alert(MSG_EDIT_ER);
        }
        function delete_success() {
            alert(MSG_DEL_OK);
            window.location.href = 'HanhViViPham.aspx';
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

