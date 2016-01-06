<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="QLNhansuKTNB_Input.aspx.cs" Inherits="VPB_KTNB.Modules.Lookup.DanhMuc.ChucDanh.QLNhansuKTNB_Input" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <div class="form-horizontal">
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Tên đăng nhập<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_F9B2BEB9_B3F4_4878_B718_547915007D0D" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Tên đầy đủ<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_6CF20676_81A6_4766_9854_9FD8EE4F6B26" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Ngày sinh</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID3_02B2A5FC_6E59_4917_83EE_3A26DA31D59C" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Trình độ học vấn</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="ID8_385CD97C_1A72_4AF1_9472_53EE5EB272BE" runat="server" CssClass="form-control" Width="300px">
                    <asp:ListItem>Cao học</asp:ListItem>
                    <asp:ListItem>Đại học</asp:ListItem>
                    <asp:ListItem>Cao đẳng</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Phòng ban</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="ID8_558DD2DC_841B_4368_A426_B84F01079086" runat="server" CssClass="form-control" Width="300px">
                    </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Chức danh</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="ID8_936D771B_D880_4A1C_8D6E_42C571C2853C" runat="server" CssClass="form-control" Width="300px">
                    </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Ngày vào ngân hàng</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID3_D7E84FA3_B6A9_40D0_B504_291E7F11C3C1" runat="server" CssClass="form-control" ></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Mô tả</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_74DC98E4_62C9_45C3_A6B1_0BF380DCB9F1" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control"></asp:TextBox>
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
    <script type="text/javascript">
        var _documentid;
        /*********************************************************/
        $(document).ready(function () {
            //do smt here
        });
        /*********************************************************/
        function preparesavedoc(documentID, doctypeID) {
            var ten = GetSvrCtlValue("ID8_F9B2BEB9_B3F4_4878_B718_547915007D0D");
            var url = "QLNhansuKTNB_Input.aspx";
            var query = "act=checkvalue";
            query += "&p=F9B2BEB9-B3F4-4878-B718-547915007D0D";
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
            var ten = GetSvrCtlValue("ID8_F9B2BEB9_B3F4_4878_B718_547915007D0D");
            var url = "QLNhansuKTNB_Input.aspx";
            var query = "act=checkvalueupdate";
            query += "&p=F9B2BEB9-B3F4-4878-B718-547915007D0D";
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
            window.location.href = 'QLNhansuKTNB.aspx';
        }
        function savedoc_error() {
            alert(MSG_ADD_ER);
        }
        function update_success() {
            alert(MSG_EDIT_OK);
            window.location.href = 'QLNhansuKTNB.aspx';
        }
        function update_error() {
            alert(MSG_EDIT_ER);
        }
        function delete_success() {
            alert(MSG_DEL_OK);
            window.location.href = 'QLNhansuKTNB.aspx';
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
