<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="DoiTuongKiemToan_Load.aspx.cs" Inherits="VPB_TDHS.Modules.Lookup.DoiTuongKiemToan_Load" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <div class="form-horizontal">
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Loại đối tượng kiểm toán</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="ID8_94F402C1_CCC3_4A93_8D9F_2D24BDB8EE2C" runat="server" CssClass="form-control" Width="300px">
                    </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Tên đối tượng kiểm toán<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_F4018BE5_84AD_4FE2_B3AF_5BC3F5981CEA" runat="server" CssClass="form-control"  Width="300px"></asp:TextBox>
            </div>
        </div>
        
         <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Diễn giải</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_18BCCAAE_F688_4D50_98C1_0F1EE034AEA0" TextMode="MultiLine" runat="server" CssClass="form-control"  Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Trạng thái</label>
            <div class="col-sm-6">
                <asp:DropDownList  ID="DOCSTATUS" runat="server" CssClass="form-control" Width="100px">
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
        var ten = GetSvrCtlValue("ID8_F4018BE5_84AD_4FE2_B3AF_5BC3F5981CEA");
        if (ten.trim().length == 0)
        {
            alert("Nhâp tên đối tượng kiểm toán.");
            return;
        }
        var url = "LoaiDoiTuongKiemToan_Load.aspx";
        var query = "act=checkvalue";
        query += "&p=F4018BE5-84AD-4FE2-B3AF-5BC3F5981CEA";
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
    function prepareupdatedoc(documentID) {
        var ten = GetSvrCtlValue("ID8_F4018BE5_84AD_4FE2_B3AF_5BC3F5981CEA");
        if (ten.trim().length == 0) {
            alert("Nhâp tên đối tượng kiểm toán.");
            return;
        }
        var url = "DoiTuongKiemToan_Load.aspx";
        var query = "act=checkvalueupdate";
        query += "&p=F4018BE5-84AD-4FE2-B3AF-5BC3F5981CEA";
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
        window.location.href = "DoiTuongKiemToan.aspx?a=dbb1170f-fb15-4585-9c21-e21c32d4de86";
    }
    function savedoc_error() {
        alert(MSG_ADD_ER);
    }
    function update_success() {
        alert(MSG_EDIT_OK);
        window.location.href = "DoiTuongKiemToan.aspx?a=dbb1170f-fb15-4585-9c21-e21c32d4de86";
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
