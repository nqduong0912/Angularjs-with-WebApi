<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="QLChucDanh_Input.aspx.cs" Inherits="VPB_KTNB.Modules.Lookup.DanhMuc.ChucNang.QLChucDanh_Input" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <div class="form-horizontal">
        <div class="form-group">
            <div class="col-sm-3 col-sm-offset-1 col-xs-4">
                <label for="inputDescription" class="control-label">Tên chức danh<span class="star-red"></span>*</label>
            </div>
            
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_9FBBDFF6_C6DA_4E94_90A8_16B5D190495D" runat="server" CssClass="form-control">
                </asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-3 col-sm-offset-1 col-xs-4">
                <label for="inputName" class="control-label">Diễn giải</label>
            </div>
            
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_565F1B87_DB7B_4231_BBC8_7074BBFC079D" runat="server" TextMode="MultiLine" Rows="5"  CssClass="form-control">
                </asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-3 col-sm-offset-1 col-xs-4">
                <label for="inputName" class="control-label">Trạng thái</label>
            </div>
            
            <div class="col-sm-6">
                <asp:DropDownList ID="DOCSTATUS" runat="server" CssClass="form-control" Width="100px">
                    </asp:DropDownList>
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
            var ten = GetSvrCtlValue("ID8_9FBBDFF6_C6DA_4E94_90A8_16B5D190495D");
            var diengiai = GetSvrCtlValue("ID8_565F1B87_DB7B_4231_BBC8_7074BBFC079D");
            var url = "QLChucDanh_Input.aspx";
            var query = "act=checkvalue";
            query += "&p=9FBBDFF6-C6DA-4E94-90A8-16B5D190495D";
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
            var ten = GetSvrCtlValue("ID8_9FBBDFF6_C6DA_4E94_90A8_16B5D190495D");
            var diengiai = GetSvrCtlValue("ID8_565F1B87_DB7B_4231_BBC8_7074BBFC079D");
            var url = "QLChucDanh_Input.aspx";
            var query = "act=checkvalueupdate";
            query += "&p=9FBBDFF6-C6DA-4E94-90A8-16B5D190495D";
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
            window.location.href = 'QLChucDanh.aspx';
        }
        function savedoc_error() {
            alert(MSG_ADD_ER);
        }
        function update_success() {
            alert(MSG_EDIT_OK);
            window.location.href = 'QLChucDanh.aspx';
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
