<%@ Page Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" Inherits="VPB_PROMOTION.UMS.UMS_userInfo" CodeBehind="userInfo.aspx.cs" %>

<asp:Content runat="server" ID="UserInfo" ContentPlaceHolderID="FormContent">
    <div class="form-horizontal">
        Thông tin chi tiết
        <div class="form-group lblCaption" style="display: none">
            <label for="inputDescription" class="control-label" style="display: none">Tên đăng nhập</label>
            <div class="col-sm-6" style="display: none"></div>
        </div>
        <div class="form-group lblCaption">
            <label for="inputDescription" class="control-label" style="display: none">E-mail</label>
            <div class="col-sm-6" style="display: none">
                <asp:TextBox runat="server" ID="Email" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group lblCaption" style="display: none">
            <label for="inputDescription" class="control-label" style="display: none">Số mobile</label>
            <div class="col-sm-6" style="display: none">
                <asp:TextBox runat="server" ID="MobilePhone" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group lblCaption" style="display: none">
            <label for="inputDescription" class="control-label" style="display: none">Ghi chú</label>
            <div class="col-sm-6" style="display: none">
                <asp:TextBox runat="server" ID="Description" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group lblCaption">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Tên đăng nhập</label>
            <div class="col-sm-6">
                <asp:Label runat="server" ID="lblUserName" CssClass="lblCaption control-label" BackColor=""></asp:Label>
            </div>
        </div>
        <div class="form-group lblCaption">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Tên đầy đủ<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox runat="server" ID="FULLNAME" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group lblCaption">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Mã DAO</label>
            <div class="col-sm-6">
                <asp:TextBox runat="server" ID="UserCode" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group lblCaption">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Cấp quản lý<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:DropDownList ID="cboChiNhanh" runat="server" CssClass="form-control" Width="300px">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group lblCaption">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Phòng ban</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="cboPGD" runat="server" CssClass="form-control" Width="300px">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group lblCaption">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Chức danh</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="cboVaiTro" runat="server" CssClass="form-control" Width="300px">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group lblCaption" style="display: none">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Đơn vị đã kiêm nghiệm</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="cboDonViKN" runat="server" CssClass="form-control" Width="300px">
                </asp:DropDownList>
                <input id="btnBoKiemNhiem" class="Button btn btn-info" type="button" value="Bỏ kiêm nhiệm" onclick="BoKiemNhiem()" />
            </div>
        </div>
        <div class="form-group lblCaption" style="display: none">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Trạng thái</label>
            <div class="col-sm-6">
                <asp:CheckBox runat="server" ID="IsExpired" CssClass="lblNormal" Checked />
                Kích hoạt
            </div>
        </div>
    </div>
    <%--<table cellpadding="5" cellspacing="0">
        <!--thong tin chi tiet-->
        <tr class="lblCaption">
            <td colspan="4"><font color="">Thông tin chi tiết</font></td>
        </tr>
        <tr class="lblNormal">
            <td style="display: none;">Tên đăng nhập:</td>
            <td style="display: none;"></td>
            <td style="display: none">E-mail:</td>
            <td style="display: none">
                <asp:TextBox runat="server" ID="Email" SkinID="TextBox"></asp:TextBox></td>
        </tr>
        <tr class="lblNormal">
            <td style="display: none;">Số mobile:</td>
            <td style="display: none;">
                <asp:TextBox runat="server" ID="MobilePhone" SkinID="TextBox"></asp:TextBox></td>
            <td style="display: none">Ghi chú:</td>
            <td style="display: none">
                <asp:TextBox runat="server" ID="Description" SkinID="TextBox" Width='218px'></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 114px" class="lblNormal">&nbsp;Tên đăng nhập:</td>
            <td style="width: 255px">
                <asp:Label runat="server" ID="lblUserName" CssClass="lblCaption" BackColor=""></asp:Label></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td style="width: 114px" class="lblNormal">&nbsp;Tên đầy đủ:</td>
            <td style="width: 255px">
                <asp:TextBox runat="server" ID="FULLNAME" SkinID="TextBoxRequired"></asp:TextBox>
            </td>
            <td></td>
            <td></td>
        </tr>

        <!--phong ban-->
        <tr class="lblNormal">
            <td style="width: 114px">Mã DAO</td>
            <td colspan="3">
                <asp:TextBox runat="server" ID="UserCode" SkinID="TextBox" BackColor="" Width="80px" Style="text-align: center"></asp:TextBox>
            </td>
        </tr>

        <tr class="lblNormal">
            <td style="width: 114px">
                <span style="color: " class="lblNormal">Cấp quản lý</span></td>
            <td colspan="3">
                <asp:DropDownList runat="server" ID="cboChiNhanh" SkinID="DropDownListRequired"
                    BackColor="" Width="318px"
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>

        <!--vai tro-->
        <tr class="lblNormal">
            <td style="width: 114px" class="lblNormal">
                <span style="color: ">Phòng ban</span></td>
            <td colspan="3">
                <asp:DropDownList ID="cboPGD" runat="server" SkinID="DropDownList" BackColor="" Width="315px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr class="lblNormal">
            <td style="width: 114px" class="lblNormal">
                <font color="">Chức danh</font></td>
            <td colspan="3">
                <asp:DropDownList runat="server" ID="cboVaiTro" SkinID="DropDownList" BackColor=""></asp:DropDownList>
            </td>
        </tr>

        <!--trang thai-->
        <tr class="lblNormal" style="display: none">
            <td style="width: 114px" class="lblNormal">Đơn vị đã kiêm nhiệm&nbsp;</td>
            <td colspan="3">
                <asp:DropDownList ID="cboDonViKN" runat="server" SkinID="DropDownList"
                    BackColor="" Width="315px">
                </asp:DropDownList>
                <input id="btnBoKiemNhiem" class="Button" type="button" value="Bỏ kiêm nhiệm" onclick="BoKiemNhiem()" /></td>
        </tr>

        <tr class="lblNormal">
            <td style="width: 114px" class="lblNormal">&nbsp;</td>
            <td colspan="3">&nbsp;</td>
        </tr>

        <tr class="lblNormal" style="display: none">
            <td style="width: 114px">Trạng thái</td>
            <td style="width: 255px">
                <asp:CheckBox runat="server" ID="IsExpired" CssClass="lblNormal" Checked />
                Kích hoạt</td>
            <td></td>
            <td></td>
        </tr>
    </table>--%>
    <script type="text/javascript">
        function BoKiemNhiem() {
            var id = "<%=_userID %>";
            var url = "userInfo.aspx";
            var query = "act=bokiemnhiem";
            query += "&id=" + id;
            query += "&pgdkn=" + GetSvrCtlValue("cboDonViKN");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (errormessage) {
                    window.location.reload(true);
                }
            });
        }
        function KiemNhiem() {
            var id = "<%=_userID %>";
            var url = "userInfo.aspx";
            var query = "act=kiemnhiem";
            query += "&id=" + id;
            query += "&cnkn=" + GetSvrCtlValue("cboChiNhanh");
            query += "&pgdkn=" + GetSvrCtlValue("cboPGD");
            query += "&vtkn=" + GetSvrCtlValue("cboVaiTro");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (errormessage) {
                    if (errormessage == "")
                        window.location.reload(true);
                    else
                        alert(errormessage);
                }
            });
        }
        function getPGD(parentCompanyID) {
            var id = "<%=_userID %>";
            $('#ctl00_FormContent_cboPhongGDKN option').remove();
            var url = "userInfo.aspx";
            var query = "act=getpgd";
            query += "&id=" + id;
            query += "&ctl00$FormContent$cboChiNhanh=" + parentCompanyID;
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (data) {
                    var options = data.split("@FM");
                    for (i = 0; i < options.length; i++) {
                        $('#ctl00_FormContent_cboPhongGDKN').append($(options[i]));
                    }
                }
            });
        }
        function deleteuser(userid, username) {
            if (!window.confirm("Bạn chắc chắn muốn xóa user này ?")) return;

            $("#ctl00_btnEDIT").attr("disabled", true);
            $("#ctl00_btnDELETE").attr("disabled", true);
            var docspace = "<%=_docspace %>";
            var url = "userInfo.aspx";
            var query = "act=delete&id=" + userid + "&uname=" + username + "&docspace=" + docspace;
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (data) {
                    if (data == "deleted") {
                        alert('Người sử dụng đã bị xóa.');
                        //window.open("../MainView/FolderViewer.aspx?op=&docspace=" + docspace, "fraToc");
                    }
                }
            });
        }
        function updateuser() {
            if (!window.confirm("Bạn chắc chắn muốn cập nhật thông tin người sử dụng này ?")) return;

            var id = "<%=_userID %>";
            var uname = "<%=_username %>";
            var fullname = $("#ctl00_FormContent_FULLNAME").val();
            var exp = 1;
            if ($("#ctl00_FormContent_IsExpired").attr("checked"))
                exp = 0;
            var fk_parentgroupid = $("#ctl00_FormContent_cboChiNhanh").val();
            var fk_groupid = $("#ctl00_FormContent_cboPGD").val();
            var fk_roleid = $("#ctl00_FormContent_cboVaiTro").val();
            var uc = $("#ctl00_FormContent_UserCode").val();
            var docspace = "<%=_docspace %>";
            var url = "userInfo.aspx";
            var query = "act=update&id=" + id + "&uname=" + uname + "&ucode=" + uc + "&fullname=" + fullname + "&exp=" + exp + "&fk_groupid=" + fk_groupid + "&fk_parentgroupid=" + fk_parentgroupid + "&fk_roleid=" + fk_roleid + "&docspace=" + docspace;

            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (data) {
                    if (data == "updated") {
                        alert('Thông tin người sử dụng đã được cập nhật.');
                    }
                    else {
                        alert(data);
                    }
                }
            });
        }

        function addmoreRoleGroup() {
            var userid = "<%=_userID %>";
            var url = "AddMoreRoleGroup.aspx?id=" + userid;
            opendetail(url, "AddMoreRoleGroup");
        }
    </script>
</asp:Content>
<asp:Content runat="server" ID="ExtendButton" ContentPlaceHolderID="ButtonExtend">
    <asp:Button runat="server" ID="btnResetPassword" CssClass="button" Text="reset PWD" />
    <asp:Literal runat="server" ID="litKiemNhiem">
        <input type="button" value="Kiêm nhiệm thêm..." class="InsertButton" style="width: auto"
            onclick="addmoreRoleGroup()" />
    </asp:Literal>
</asp:Content>
