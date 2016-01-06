<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="QLQuytrinhActivitySanpham_Input.aspx.cs" Inherits="VPB_KTNB.Modules.Lookup.DanhMuc.ChucDanh.QLQuytrinhActivitySanpham_Input" %>

<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <div class="form-horizontal">
        <div class="form-group">
             <div class="col-sm-3 col-sm-offset-1 col-xs-4">
                <label for="inputName" class="control-label">Mảng nghiệp vụ</label>
            </div>
            <div class="col-sm-6">
                <asp:DropDownList ID="ID8_1A265085_0B1B_4FE5_B7AF_51EEF89B6474" runat="server" CssClass="form-control" Width="300px">
                    </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
             <div class="col-sm-3 col-sm-offset-1 col-xs-4">
                <label for="inputName" class="control-label">Quy trình /Activity /Sản phẩm<span class="star-red">*</span></label>
            </div>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_DA131AD9_234F_43CE_BBED_ED408543A23E" runat="server" CssClass="form-control" ></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
             <div class="col-sm-3 col-sm-offset-1 col-xs-4">
                <label for="inputName" class="control-label">Diễn giải</label>
            </div>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_4E6F4C58_F130_451B_9779_3434753A53BA" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control"></asp:TextBox>
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
            
            var ten = GetSvrCtlValue("ID8_DA131AD9_234F_43CE_BBED_ED408543A23E");
            var url = "QLQuytrinhActivitySanpham_Input.aspx";
            var query = "act=checkvalue";
            query += "&p=DA131AD9-234F-43CE-BBED-ED408543A23E";
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
            var ten = GetSvrCtlValue("ID8_DA131AD9_234F_43CE_BBED_ED408543A23E");
            var url = "QLQuytrinhActivitySanpham_Input.aspx";
            var query = "act=checkvalueupdate";
            query += "&p=DA131AD9-234F-43CE-BBED-ED408543A23E";
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
            window.location.href = 'QLQuytrinhActivitySanpham.aspx';
        }
        function savedoc_error() {
            alert(MSG_ADD_ER);
        }
        function update_success() {
            alert(MSG_EDIT_OK);
            window.location.href = 'QLQuytrinhActivitySanpham.aspx';
        }
        function update_error() {
            alert(MSG_EDIT_ER);
        }
        function delete_success() {
            alert(MSG_DEL_OK);
            window.location.href = 'QLQuytrinhActivitySanpham.aspx';
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
