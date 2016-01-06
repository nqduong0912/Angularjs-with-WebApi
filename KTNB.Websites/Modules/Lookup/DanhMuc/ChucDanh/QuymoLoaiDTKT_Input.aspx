<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="QuymoLoaiDTKT_Input.aspx.cs" Inherits="VPB_KTNB.Modules.Lookup.DanhMuc.ChucDanh.QuymoLoaiDTKT_Input" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <div class="form-horizontal">
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Năm</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="ID8_3661C77F_A22A_440D_8D32_FE472A117505" runat="server" CssClass="form-control" Width="300px">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Loại đối tượng kiểm toán</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="ID8_48163DD4_9F4B_4AC4_A1B9_A25BBB1F0EA2" runat="server" CssClass="form-control" Width="300px">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Quy mô</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="ID8_A3AFE5F3_3412_4418_854C_BD17EFAFD6BE" runat="server" CssClass="form-control" Width="300px">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Nguồn lực<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_396C3AD1_2349_4CBA_94E7_108EE6B9AE6D" runat="server" CssClass="form-control"></asp:TextBox>
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
        <tr>
            <td style="width: 222px">Năm</td>
            <td>
                <asp:DropDownList ID="ID8_3661C77F_A22A_440D_8D32_FE472A117505" runat="server"
                    SkinID="DropDownListRequired">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Loại đối tượng kiểm toán</td>
            <td>
                <asp:DropDownList ID="ID8_48163DD4_9F4B_4AC4_A1B9_A25BBB1F0EA2" runat="server"
                    SkinID="DropDownListRequired">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Quy mô</td>
            <td>
                <asp:DropDownList ID="ID8_A3AFE5F3_3412_4418_854C_BD17EFAFD6BE" runat="server"
                    SkinID="DropDownListRequired">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Nguồn lực</td>
            <td>
                <asp:TextBox ID="ID8_396C3AD1_2349_4CBA_94E7_108EE6B9AE6D" runat="server"
                    SkinID="TextBox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Trạng thái</td>
            <td>
                <asp:DropDownList ID="DOCSTATUS" runat="server"
                    SkinID="DropDownListRequired">
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
            var ten = GetSvrCtlValue("ID8_396C3AD1_2349_4CBA_94E7_108EE6B9AE6D");
            var url = "QuymoLoaiDTKT_Input.aspx";
            var query = "act=checkvalue";
            query += "&p=396C3AD1-2349-4CBA-94E7-108EE6B9AE6D";
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
                        alert(MSG_DATA_ESXIT);
                }
            });
        }

        function prepareupdatedoc(documentID) {
            var ten = GetSvrCtlValue("ID8_396C3AD1_2349_4CBA_94E7_108EE6B9AE6D");
            var url = "QuymoLoaiDTKT_Input.aspx";
            var query = "act=checkvalueupdate";
            query += "&p=396C3AD1-2349-4CBA-94E7-108EE6B9AE6D";
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
                        alert(MSG_DATA_ESXIT);
                }
            });
        }

        function savedoc_success() {
            alert(MSG_ADD_OK);
            window.location.href = 'QuymoLoaiDTKT.aspx';
        }
        function savedoc_error() {
            alert(MSG_ADD_ER);
        }
        function update_success() {
            alert(MSG_EDIT_OK);
            window.location.href = 'QuymoLoaiDTKT.aspx';
        }
        function update_error() {
            alert(MSG_EDIT_ER);
        }
        function delete_success() {
            alert(MSG_DEL_OK);
            window.location.href = 'QuymoLoaiDTKT.aspx';
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
