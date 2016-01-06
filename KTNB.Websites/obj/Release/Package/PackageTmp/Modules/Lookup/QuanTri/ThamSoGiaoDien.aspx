<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="ThamSoGiaoDien.aspx.cs" Inherits="VPB_KTNB.Modules.Lookup.QuanTri.ThamSoGiaoDien" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <div class="form-horizontal">
        <div class="form-group">
            <label class="col-sm-3 col-sm-offset-1 control-label">Màu rủi ro cao<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox id="clHigh" runat="server" name="border-color" CssClass="pick-a-color form-control"/>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-3 col-sm-offset-1 control-label">Màu rủi ro trung bình<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox id="clMedium" runat="server" name="border-color" CssClass="pick-a-color form-control"/>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-3 col-sm-offset-1 control-label">Màu rủi ro thấp<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox id="clLow" runat="server" name="border-color" CssClass="pick-a-color form-control"/>
            </div>
        </div>
    </div>
    
    <script type="text/javascript">
        var _documentid;
        /*********************************************************/
        $(document).ready(function () {
            $(".pick-a-color").pickAColor({
                showSpectrum: true,
                showSavedColors: true,
                saveColorsPerElement: true,
                fadeMenuToggle: true,
                showAdvanced: true,
                showBasicColors: true,
                showHexInput: true,
                allowBlank: true,
                inlineDropdown: true
            });
        });
        /*********************************************************/        
       
        function prepareupdatedoc(documentID) {
            var highColor = GetSvrCtlValue("clHigh");
            var mediumColor = GetSvrCtlValue("clMedium");
            var lowColor = GetSvrCtlValue("clLow");
            if (highColor.length == 0) {
                alert("Chọn màu rủi ro cao.");
                return false;
            }
            if (mediumColor.length == 0) {
                alert("Chọn màu rủi ro trung bình.");
                return false;
            }
            if (lowColor.length == 0) {
                alert("Chọn màu rủi ro thấp.");
                return false;
            }
            var r = confirm(MSG_EIDT_QUESTION);
            if (!r) return false;
            var url = "ThamSoGiaoDien.aspx";
            var query = "act=checkvalueupdate";
            query += "&highColor=" + highColor;
            query += "&mediumColor=" + mediumColor;
            query += "&lowColor=" + lowColor;
            StartProcessingForm("");

            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (data) {
                    FinishProcessingForm();
                    if (data == "0")
                    {
                        alert(MSG_EDIT_OK);
                    }
                    else
                        alert("Dữ liệu nhập bị trùng lặp. Kiểm tra lại.");
                }
            });
        }
    </script>
</asp:Content>