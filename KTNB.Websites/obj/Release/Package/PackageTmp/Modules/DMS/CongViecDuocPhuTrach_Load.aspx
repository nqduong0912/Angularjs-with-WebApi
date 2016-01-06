<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true"
    CodeBehind="CongViecDuocPhuTrach_Load.aspx.cs" Inherits="VPB_TDHS.Modules.DMS.CongViecDuocPhuTrach_Load" %>

<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <asp:ScriptManager runat="server" ID="ScriptManager1">
    </asp:ScriptManager>
    <%--<div class="form-horizontal">
        Thông tin công việc
         <div class="form-group">
             <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Công việc</label>
             <div class="col-sm-6">
                 <asp:TextBox ID="ID8_470105E3_B810_4982_A8EF_74E367441EBD" runat="server" CssClass="form-control" Enabled="False" Width="300px"></asp:TextBox>
             </div>
             <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Công việc</label>
             <div class="col-sm-6">
                 <asp:TreeView ID="treeViewThuTucKT" runat="server" EnableTheming="true" ExpandDepth="0"
                     EnableClientScript="true" PopulateNodesFromClient="true" PopulateOnDemand="true"
                     Width="400px">
                 </asp:TreeView>
             </div>
         </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Bắt đầu</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID3_3E7AE62F_3CF2_4239_A1A0_079CEDD7E057" runat="server" CssClass="form-control" Enabled="False" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Kết thúc</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID3_07A2647B_5E37_4DC8_8D8C_7C457F9D2B1B" runat="server" CssClass="form-control" Enabled="False" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Người thực hiện</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_7DCBBD86_FD24_4415_8EDB_531559D663F2" runat="server" CssClass="form-control" Enabled="False" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Người duyệt</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_C983F892_1A81_4F52_98C3_DAAD729A99E8" runat="server" CssClass="form-control" Enabled="False" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Người duyệt 2</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_B9D5B3D6_6118_4821_94F3_AB03B060FC8C" runat="server" CssClass="form-control" Enabled="False" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group" id="idTrangThai">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Trạng thái</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="DOCSTATUS" runat="server" CssClass="form-control" Width="300px" Enabled="False">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group" id="trDaNhanXet">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Đã nhận xét</label>
            <div class="col-sm-6">
                <asp:TextBox ID="DaNhanXetNguoiDuyet1" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-sm-6">
                <asp:TextBox ID="DaNhanXetNguoiDuyet2" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group" clientidmode="trNhanXet"  runat="server">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Nhận xét</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_AE50F25A_A04E_4541_8266_CE8C5033E0E5" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Lịch sử từ chối</label>
            <div class="col-sm-6">
                <C1WebGrid:C1WebGrid runat="server" ID="gridTuChoi" Width="75%" GroupIndent="20"
                    ItemStyle-Height="30px">
                    <Columns>
                        <C1WebGrid:C1BoundColumn HeaderText="Lý do từ chối" DataField="LyDo">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Người từ chối" DataField="NguoiNhanXet">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Ngày từ chối" DataField="NguoiNhanXet">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                    </Columns>
                </C1WebGrid:C1WebGrid>
            </div>
        </div>
    </div>--%>
    <table width="100%" id="tblTruongDoan">
        <tr class="GridHeader">
            <td colspan="2">Thông tin công việc
            </td>
        </tr>
        <tr>
            <td style="width: 222px">
                <label for="inputName" class="col-sm-offset-1 control-label">Công việc</label>
            </td>
            <td>
                <div class="col-sm-6">
                    <asp:TextBox ID="ID8_470105E3_B810_4982_A8EF_74E367441EBD" runat="server" CssClass="form-control" Enabled="False" Width="300px"></asp:TextBox>
                </div>
            </td>
        </tr>
        <tr>
            <td style="width: 222px; vertical-align: top">Thủ tục kiểm toán
            </td>
            <td>
                <asp:TreeView ID="treeViewThuTucKT" runat="server" EnableTheming="true" ExpandDepth="0"
                    EnableClientScript="true" PopulateNodesFromClient="true" PopulateOnDemand="true"
                    Width="400px">
                </asp:TreeView>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">
                <label for="inputName" class="col-sm-offset-1 control-label">Bắt đầu</label>
            </td>
            <td>
                <div class="col-sm-6">
                    <asp:TextBox ID="ID3_3E7AE62F_3CF2_4239_A1A0_079CEDD7E057" runat="server" CssClass="form-control" Enabled="False" Width="300px"></asp:TextBox>
                </div>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">
                <label for="inputName" class="col-sm-offset-1 control-label">Kết thúc</label>
            </td>
            <td>
                <div class="col-sm-6">
                    <asp:TextBox ID="ID3_07A2647B_5E37_4DC8_8D8C_7C457F9D2B1B" runat="server" CssClass="form-control" Enabled="False" Width="300px"></asp:TextBox>
                </div>
            </td>
        </tr>
        <tr>
            <td style="width: 222px; position: fixed">
                <label for="inputName" class="col-sm-offset-1 control-label">Người thực hiện</label>
            </td>
            <td>
                <div class="col-sm-6">
                    <asp:TextBox ID="ID8_7DCBBD86_FD24_4415_8EDB_531559D663F2" runat="server" CssClass="form-control" Enabled="False" Width="300px"></asp:TextBox>
                </div>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">
                <label for="inputName" class="col-sm-offset-1 control-label">Người duyệt</label>
            </td>
            <td>
                <div class="col-sm-6">
                    <asp:TextBox ID="ID8_C983F892_1A81_4F52_98C3_DAAD729A99E8" runat="server" CssClass="form-control" Enabled="False" Width="300px"></asp:TextBox>
                </div>
            </td>
        </tr>
        <%--<tr>
            <td style="width: 222px">Người duyệt 2
            </td>
            <td>
                <asp:TextBox ID="ID8_B9D5B3D6_6118_4821_94F3_AB03B060FC8C" runat="server" Enabled="false"
                    SkinID="TextBoxReadOnly"></asp:TextBox>
            </td>
        </tr>--%>
        <tr id="idTrangThai">
            <td style="width: 222px">
                <label for="inputName" class="col-sm-offset-1 control-label">Trạng thái</label>
            </td>
            <td>
                <div class="col-sm-6">
                    <asp:DropDownList ID="DOCSTATUS" runat="server" Enabled="false" CssClass="form-control" Width="300px">
                    </asp:DropDownList>
                </div>
            </td>
        </tr>
        <tr id="trDaNhanXet" runat="server">
            <td style="width: 222px">
                <label for="inputName" class="col-sm-offset-1 control-label">Đã nhận xét</label>
            </td>
            <td>
                <div class="col-sm-6">
                    <asp:TextBox ID="DaNhanXetNguoiDuyet1" Rows="5" runat="server" TextMode="MultiLine" CssClass="form-control" Enabled="False" Width="300px"></asp:TextBox>
                </div>
                <br />
                <%--<asp:TextBox Rows="2" Columns="55" Text="" Enabled="false" TextMode="MultiLine" ID="DaNhanXetNguoiDuyet2"
                    runat="server" SkinID="TextBoxReadOnly"></asp:TextBox>(người duyệt 2)--%>
            </td>
        </tr>
        <tr id="trNhanXet" runat="server">
            <td style="width: 222px"><label for="inputName" class="col-sm-offset-1 control-label">Nhận xét</label>
            </td>
            <td>
                 <div class="col-sm-6">
                    <asp:TextBox ID="ID8_AE50F25A_A04E_4541_8266_CE8C5033E0E5" Rows="5" runat="server" TextMode="MultiLine" CssClass="form-control" Enabled="False" Width="300px"></asp:TextBox>
                </div>
            </td>
        </tr>
        <tr>
            <td style="width: 222px; vertical-align: top;">Lịch sử từ chối
            </td>
            <td>
                <C1WebGrid:C1WebGrid runat="server" ID="gridTuChoi" Width="75%" GroupIndent="20"
                    ItemStyle-Height="30px">
                    <Columns>
                        <C1WebGrid:C1BoundColumn HeaderText="Lý do từ chối" DataField="LyDo">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Người từ chối" DataField="NguoiNhanXet">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Ngày từ chối" DataField="NguoiNhanXet">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                    </Columns>
                </C1WebGrid:C1WebGrid>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="idCongViec" runat="server" />
    <script type="text/javascript">
        /*********************************************************/
        var ctl_username;
        var ctl_username_thanhvien;
        var MSG_CONFIRM_ADD_CV = "Bạn có muốn tạo công việc này không?";
        var MSG_ALERT_CHOICE_CV = "Hãy tạo một công việc.";

        var MSG_CONFIRM_ADD_TT = "Bạn có muốn đưa thủ tục kiểm toán này vào công việc này không?";
        var MSG_EXIST_TT = "Thủ tục kiểm toán này này đã tồn tại trong công việc này.";
        var MSG_ADD_TT_SUC = "Đưa thủ tục kiểm toán vào công việc thành công.";
        var MSG_CONFIRM_DEL_TT = "Bạn có muốn đưa thủ tục kiểm toán này ra khỏi công việc?";
        var MSG_CONFIRM_SUBMIT = "Bạn có muốn cập nhật?";
        var MSG_CONFIRM_REJECT = "Bạn có muốn từ chối?";

        var MSG_CANNOT_SUBMIT = "Không submit được vì chưa có thủ tục nào";
        var _documentID = Qry["doc"];
        var cv = Qry["cv"];
        //thangma
        var clientIDbtnSave = '<%=_btnSave.ClientID %>';

        $(document).ready(function () {
            //HiddenButtonSave();
            $("#idTrangThai").hide();
            ShowHideNhanXet();
            //css
            $('input[name*="btnSAVE"]').css('width', '80px');
        });
        function ShowHideNhanXet() {
            if (cv == 'nguoiduyet') {
                $('#' + '<%=ID8_AE50F25A_A04E_4541_8266_CE8C5033E0E5.ClientID %>').show();
            }
            else {

                $('#' + '<%=trNhanXet.ClientID %>').hide();
                $('#' + '<%=ID8_AE50F25A_A04E_4541_8266_CE8C5033E0E5.ClientID %>').hide();
            }

        }
        function HiddenButtonSave() {
            var btnSave = $("#ctl00_btnSAVE");
            var items = $('#ctl00_FormContent_DOCSTATUS option').size();
            if (items == 0)
                btnSave.hide();
        }

        function CapNhatTrangThai() {
            var cv = Qry["cv"];
            var doankt = Qry["doankt"];
            var dotkt = Qry["dotkt"];
            if (!window.confirm(MSG_CONFIRM_SUBMIT))
                return false;
            var url = "CongViecDuocPhuTrach_Load.aspx";
            var query = "act=capnhattrangthai";
            var trangthai = GetSvrCtlValue("DOCSTATUS");
            query += "&doc=" + _documentID + "&trangthai=" + trangthai + "&cv=" + cv + "&doankt=" + doankt + "&dotkt=" + dotkt;
            var nhanXet = GetSvrCtlValue("ID8_AE50F25A_A04E_4541_8266_CE8C5033E0E5");
            query += "&nhanxet=" + nhanXet;
            $('#<%=idCongViec.ClientID %>').val(_documentID);
            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (result) {
                    FinishProcessingForm();
                    if (result == "0") {
                        alert("Công việc này chưa được phép phê duyệt vì các phát hiện tương ứng chưa đc phê duyệt.");
                        return false;
                    }
                    else if (result == "2") {
                        alert("Công việc này chưa được phép hoàn tất vì các phát hiện tương ứng chưa đc hoàn tất hoặc phê duyệt.");
                        return false;
                    }
                    else {
                        window.location.href = "CongViecDuocPhuTrach.aspx?doankt=" + doankt + "&dotkt=" + dotkt + "&cv=" + cv;
                    }
                }
            });
        }

        function CapNhatTrangThaiTuChoi() {
            var cv = Qry["cv"];
            var doankt = Qry["doankt"];
            var dotkt = Qry["dotkt"];
            if (!window.confirm(MSG_CONFIRM_REJECT))
                return false;
            var trangthai = GetSvrCtlValue("DOCSTATUS");
            var url = "CongViecDuocPhuTrach_Load.aspx";
            var query = "act=capnhattrangthaituchoi";
            query += "&doc=" + _documentID + "&cv=" + cv + "&doankt=" + doankt + "&dotkt=" + dotkt;
            var nhanXet = GetSvrCtlValue("ID8_AE50F25A_A04E_4541_8266_CE8C5033E0E5");
            query += "&nhanxet=" + nhanXet;
            nhanXet = nhanXet.trim();
            if (nhanXet.length == 0) {
                alert('Bạn chưa nhập nhận xét');
                return false;
            }

            $('#<%=idCongViec.ClientID %>').val(_documentID);
            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (message) {
                    FinishProcessingForm();
                    window.location.href = "CongViecDuocPhuTrach.aspx?doankt=" + doankt + "&dotkt=" + dotkt + "&cv=" + cv;
                }
            });
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ButtonExtendBefore" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ButtonExtend" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FormFooter" runat="server">
</asp:Content>
