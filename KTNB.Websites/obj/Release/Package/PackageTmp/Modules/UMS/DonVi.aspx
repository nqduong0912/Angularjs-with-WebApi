<%@ Page Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" Inherits="VPB_PROMOTION.UMS.UMS_DonVi" CodeBehind="DonVi.aspx.cs" %>

<%@ OutputCache Duration="3600" Location="Client" VaryByParam="id; docspace" %>

<asp:Content runat="server" ID="NewUser" ContentPlaceHolderID="FormContent">
    <div class="form-horizontal">
        Thông tin chi tiết
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label" id="lblMaCN">Mã đơn vị<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="FULLNAME" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:CheckBox ID="chkTrungTamHT" runat="server" Visible="False"></asp:CheckBox>
            </div>
            <asp:CheckBox ID="chkPGD" runat="server" Text="Phòng giao dịch" Visible="False"/>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label" >Tên gợi nhớ<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="Mnemonic" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label" >Tên đầy đủ<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="DESCRIPTION" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Cấp quản lý</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="cboDonVi" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group" style="display: none">
            Trạng thái
            <div class="col-sm-offset-4 col-sm-2">
                <asp:CheckBox runat="server" ID="IsExpired" Checked />
                <label for="ex2-a">Kích hoạt</label>
            </div>
        </div>
    </div>
<%--    <table style="width: 100%">
        <!--thong tin chi tiet-->
        <tr class="GridHeader">
            <td colspan="4">Thông tin chi tiết</td>
        </tr>
        <tr class="lblNormal">
            <td style="width: 132px" align="left" id="lblMaCN">Mã đơn vị</td>
            <td>
                <asp:TextBox runat="server" ID="FULLNAME" SkinID="TextBoxRequired" ReadOnly="true"></asp:TextBox>
                <asp:CheckBox runat="server" ID="chkTrungTamHT" Visible="false" />
            </td>
            <td></td>
            <td style="width: 113px; display: none">
                <asp:CheckBox ID="chkPGD" runat="server" Text="Phòng giao dịch" /></td>
        </tr>

        <tr class="lblNormal">
            <td align="left" style="width: 132px">Tên gợi nhớ</td>
            <td>
                <asp:TextBox runat="server" ID="Mnemonic" SkinID="TextBoxRequired"></asp:TextBox></td>
            <td>&nbsp;</td>
            <td style="width: 113px">&nbsp;</td>
        </tr>
        <tr class="lblNormal">
            <td align="left" style="width: 132px">Tên đầy đủ</td>
            <td>
                <asp:TextBox runat="server" ID="DESCRIPTION" SkinID="TextBoxRequired" Style="width: 94%"></asp:TextBox></td>
            <td></td>
            <td style="width: 113px"></td>
        </tr>
        <tr class="lblNormal">
            <td align="left" style="width: 132px">Cấp quản lý</td>
            <td>
                <asp:DropDownList ID="cboDonVi" runat="server" SkinID="DropDownList" Width="75%">
                </asp:DropDownList></td>
            <td></td>
            <td style="width: 113px"></td>
        </tr>
        <tr class="lblCaption" style="display: none">
            <td colspan="4">Trạng thái</td>
        </tr>
        <tr class="lblNormal" style="display: none">
            <td style="width: 132px"></td>
            <td>
                <asp:CheckBox runat="server" ID="IsExpired" Checked />Kích hoạt</td>
            <td></td>
            <td style="width: 113px"></td>
        </tr>
    </table>--%>
    <script type="text/javascript">
        var QRY = GetQueryString();
        $(document).ready(function () {

        });
        function deletegroup(obj) {
            if (!window.confirm('Việc xóa CN/PGD sẽ ảnh hưởng đến hoạt động của CN/PGD này trên hệ thống.\n Bạn thực sự muốn xóa CN/PGD này ?')) return false;
            $("#" + obj.id).attr("disabled", true);
            $("#ctl00_btnEDIT").attr("disabled", true);
            var groupid = $("#ctl00_FormContent_FULLNAME").val();
            var action = "DEL";
            var query = "act=" + action + "&id=" + QRY["id"] + "&cocode=" + QRY["name"];
            var url = "DonVi.aspx";
            var docspace = "<%=_docspace %>";
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (data) {
                    alert(data);
                    parent["fraToc"].location.reload();
                }
            });
        }
        function updategroup() {

            var TT_HOTRO = 1;
            var CHI_NHANH = 3;
            var PHONG_GD = 5;

            if (!window.confirm('Bạn đồng ý cập nhật CN/PGD này ?')) return false;
            var cocode = $("#ctl00_FormContent_FULLNAME").val();
            var mnemonic = $("#ctl00_FormContent_Mnemonic").val();
            var parentgroup = $("#ctl00_FormContent_cboDonVi").val();
            var des = $("#ctl00_FormContent_DESCRIPTION").val();
            var isexp = 0;
            var grouptype = 0;
            if ($("#ctl00_FormContent_chkTrungTamHT").attr("checked"))
                grouptype = TT_HOTRO;
            else if (parentgroup != "")
                grouptype = PHONG_GD;
            else if (parentgroup == "")
                grouptype = CHI_NHANH;

            isexp = 0;
            var action = "EDIT";
            var query = "id=" + QRY["id"] + "&parentgroup=" + parentgroup + "&act=" + action + "&cocode=" + cocode + "&mnemonic=" + mnemonic + "&des=" + des + "&exp=" + isexp + "&type=" + grouptype;
            var url = "DonVi.aspx";
            var docspace = "<%=_docspace %>";
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (data) {
                    alert(data);
                    parent["fraToc"].location.reload();
                }
            });
        }
        function openurl(url) {
            opendetail(url, 'grouptype');
        }
        function toogleCNQuanLy() {
            if ($("#ctl00_FormContent_chkTrungTamHT").attr("checked")) {
                $("#ctl00_FormContent_cboDonVi").attr("selectedIndex", 0);
                $("#ctl00_FormContent_cboDonVi").attr("disabled", true);
            }
            else {
                $("#ctl00_FormContent_cboDonVi").attr("disabled", false);
            }
        }
        $(document).ready(function () {
            if ($("#ctl00_FormContent_chkTrungTamHT").attr("checked")) {
                $("#ctl00_FormContent_cboDonVi").attr("disabled", true);
            }
        })
    </script>
</asp:Content>
