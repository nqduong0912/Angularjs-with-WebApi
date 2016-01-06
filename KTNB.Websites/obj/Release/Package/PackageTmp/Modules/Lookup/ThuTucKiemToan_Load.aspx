<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="ThuTucKiemToan_Load.aspx.cs" Inherits="VPB_TDHS.Modules.Lookup.ThuTucKiemToan_Load" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <div class="form-horizontal">
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Tên kiểm soát</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="ID8_4B3FCE9D_5A7E_4DD6_893B_912086B225B2" runat="server" CssClass="form-control" Width="300px">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Tên<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_98F450F3_824E_4BBD_9F9D_6C9845FD8186" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Diễn giải</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_2D17E66A_B9EB_4051_ABB4_01CBA54512DD" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control"></asp:TextBox>
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
    <%--<table width="100%">
        <tr>
            <td style="width: 222px">Tên kiểm soát</td>
            <td>
                <asp:DropDownList ID="ID8_4B3FCE9D_5A7E_4DD6_893B_912086B225B2" runat="server"
                    SkinID="DropDownListRequired" Width="500px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Tên</td>
            <td>
                <asp:TextBox ID="ID8_98F450F3_824E_4BBD_9F9D_6C9845FD8186" runat="server"
                    SkinID="TextBoxRequired"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Diễn giải</td>
            <td>
                <asp:TextBox ID="ID8_2D17E66A_B9EB_4051_ABB4_01CBA54512DD" runat="server"
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
            var ten = GetSvrCtlValue("ID8_98F450F3_824E_4BBD_9F9D_6C9845FD8186");
            var url = "ThuTucKiemToan_Load.aspx";
            var query = "act=checkvalue";
            query += "&p=98F450F3-824E-4BBD-9F9D-6C9845FD8186";
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

        /*********************************************************/
        function prepareupdatedoc(documentID) {
            var ten = GetSvrCtlValue("ID8_98F450F3_824E_4BBD_9F9D_6C9845FD8186");
            var url = "ThuTucKiemToan_Load.aspx";
            var query = "act=checkvalueupdate";
            query += "&p=98F450F3-824E-4BBD-9F9D-6C9845FD8186";
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
            window.location.href = 'ThuTucKiemToan.aspx';
        }
        function savedoc_error() {
            alert(MSG_ADD_ER);
        }
        function update_success() {
            alert(MSG_EDIT_OK);
            window.location.href = 'ThuTucKiemToan.aspx';
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
