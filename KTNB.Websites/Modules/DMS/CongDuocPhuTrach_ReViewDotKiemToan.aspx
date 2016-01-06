<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="CongDuocPhuTrach_ReViewDotKiemToan.aspx.cs" Inherits="VPB_TDHS.Modules.DMS.CongDuocPhuTrach_ReViewDotKiemToan" %>

<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <asp:ScriptManager runat="server" ID="ScriptManager1">
    </asp:ScriptManager>
    <div class="form-horizontal" id="tblTruongDoan">
        Thông tin công việc
         <div class="form-group">
             <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Công việc</label>
             <div class="col-sm-6">
                 <asp:TextBox ID="ID8_470105E3_B810_4982_A8EF_74E367441EBD" runat="server" Enabled="False" Width="300px" CssClass="form-control"></asp:TextBox>
             </div>
         </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Thủ tục kiểm toán</label>
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
                 <asp:TextBox ID="ID3_3E7AE62F_3CF2_4239_A1A0_079CEDD7E057" runat="server" Enabled="False" Width="300px" CssClass="form-control"></asp:TextBox>
             </div>
         </div>
        <div class="form-group">
             <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Kết thúc</label>
             <div class="col-sm-6">
                 <asp:TextBox ID="ID3_07A2647B_5E37_4DC8_8D8C_7C457F9D2B1B" runat="server" Enabled="False" Width="300px" CssClass="form-control"></asp:TextBox>
             </div>
         </div>
        <div class="form-group">
             <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Người thực hiện</label>
             <div class="col-sm-6">
                 <asp:TextBox ID="ID8_7DCBBD86_FD24_4415_8EDB_531559D663F2" runat="server" Enabled="False" Width="300px" CssClass="form-control"></asp:TextBox>
             </div>
         </div>
        <div class="form-group">
             <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Người duyệt</label>
             <div class="col-sm-6">
                 <asp:TextBox ID="ID8_C983F892_1A81_4F52_98C3_DAAD729A99E8" runat="server" Enabled="False" Width="300px" CssClass="form-control"></asp:TextBox>
             </div>
         </div>
        <div class="form-group">
             <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Trạng thái</label>
             <div class="col-sm-6">
                 <asp:TextBox ID="txtStatus" runat="server" Enabled="False" Width="300px" CssClass="form-control"></asp:TextBox>
             </div>
         </div>
    </div>
    <%--<table width="100%" id="tblTruongDoan">
        <tr class="GridHeader">
            <td colspan="2">Thông tin công việc
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Công việc
            </td>
            <td>
                <asp:TextBox ID="ID8_470105E3_B810_4982_A8EF_74E367441EBD" runat="server" Enabled="false"
                    SkinID="TextBoxReadOnly"></asp:TextBox>
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
            <td style="width: 222px">Bắt đầu
            </td>
            <td>
                <asp:TextBox ID="ID3_3E7AE62F_3CF2_4239_A1A0_079CEDD7E057" runat="server" Enabled="false"
                    SkinID="TextBoxReadOnly"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Kết thúc
            </td>
            <td>
                <asp:TextBox ID="ID3_07A2647B_5E37_4DC8_8D8C_7C457F9D2B1B" runat="server" Enabled="false"
                    SkinID="TextBoxReadOnly"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px; position: fixed">Người thực hiện
            </td>
            <td>
                <asp:TextBox ID="ID8_7DCBBD86_FD24_4415_8EDB_531559D663F2" runat="server" Enabled="false"
                    SkinID="TextBoxReadOnly"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Người duyệt
            </td>
            <td>
                <asp:TextBox ID="ID8_C983F892_1A81_4F52_98C3_DAAD729A99E8" runat="server" Enabled="false"
                    SkinID="TextBoxReadOnly"></asp:TextBox>
            </td>
        </tr>


        <tr>
            <td style="width: 222px">Trạng thái
            </td>
            <td>
                <asp:TextBox ID="txtStatus" runat="server" SkinID="TextBoxReadOnly">
                </asp:TextBox>
            </td>
        </tr>
    </table>--%>
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
        $(document).ready(function () {

        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ButtonExtendBefore" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ButtonExtend" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FormFooter" runat="server">
</asp:Content>
