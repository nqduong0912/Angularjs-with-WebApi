<%@ Page Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true"
    CodeBehind="AddMoreRoleGroup.aspx.cs" Inherits="AddMoreRoleGroup" Title="Untitled Page" %>

<asp:Content runat="server" ID="UserInfo" ContentPlaceHolderID="FormContent">
    <div class="form-horizontal">
        Thông tin chi tiết
        <div class="form-group lblCaption">
            <label for="inputDescription" class="control-label" style="display: none">Mã người sử dụng</label>
            <div class="col-sm-6" style="display: none">
                <asp:TextBox runat="server" ID="UserCode" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
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
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Tên đăng nhập<span class="star-red">*</span></label>
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
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Chi nhánh<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:DropDownList ID="cboChiNhanh" runat="server" CssClass="form-control" Width="300px" BackColor=""
                    OnSelectedIndexChanged="cboChiNhanh_SelectedIndexChanged"
                    AutoPostBack="True">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group lblCaption">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Phòng giao dịch</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="cboPGD" runat="server" CssClass="form-control" Width="300px">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group lblCaption">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Vai trò</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="cboVaiTro" runat="server" CssClass="form-control" Width="300px">
                </asp:DropDownList>
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
            <td colspan="4">
                <font color="">Thông tin chi tiết</font>
            </td>
        </tr>
        <tr class="lblNormal" style="display: none">
            <td style="width: 114px">Mã người sử dụng:
            </td>
            <td style="width: 255px">
                <asp:TextBox runat="server" ID="UserCode" SkinID="TextBox" BackColor="" Width="80px"
                    Style="text-align: center"></asp:TextBox>
            </td>
        </tr>
        <tr class="lblNormal">
            <td style="display: none;">Tên đăng nhập:
            </td>
            <td style="display: none;"></td>
            <td style="display: none">E-mail:
            </td>
            <td style="display: none">
                <asp:TextBox runat="server" ID="Email" SkinID="TextBox"></asp:TextBox>
            </td>
        </tr>
        <tr class="lblNormal">
            <td style="display: none;">Số mobile:
            </td>
            <td style="display: none;">
                <asp:TextBox runat="server" ID="MobilePhone" SkinID="TextBox"></asp:TextBox>
            </td>
            <td style="display: none">Ghi chú:
            </td>
            <td style="display: none">
                <asp:TextBox runat="server" ID="Description" SkinID="TextBox" Width='218px'></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 114px" class="lblNormal">(<font color='red'>*</font>) Tên đăng nhập:
            </td>
            <td style="width: 255px">
                <asp:Label runat="server" ID="lblUserName" CssClass="lblCaption" BackColor=""></asp:Label>
            </td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td style="width: 114px" class="lblNormal">(<font color='red'>*</font>) Tên đầy đủ:
            </td>
            <td style="width: 255px">
                <asp:TextBox runat="server" ID="FULLNAME" SkinID="TextBox"></asp:TextBox>
            </td>
            <td></td>
            <td></td>
        </tr>
        <!--phong ban-->
        <tr class="lblCaption">
            <td colspan="4">
                <font color=""></font>
            </td>
        </tr>
        <tr class="lblNormal">
            <td style="width: 114px">
                <span style="color: " class="lblNormal">(<font color='red'>*</font>) Chi nhánh</span>
            </td>
            <td colspan="3">
                <asp:DropDownList runat="server" ID="cboChiNhanh" SkinID="DropDownList" BackColor=""
                    Width="318px" OnSelectedIndexChanged="cboChiNhanh_SelectedIndexChanged"
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <!--vai tro-->
        <tr class="lblNormal">
            <td style="width: 114px" class="lblNormal">
                <span style="color: ">Phòng GD</span>
            </td>
            <td colspan="3">
                <asp:DropDownList ID="cboPGD" runat="server" SkinID="DropDownList" BackColor=""
                    Width="315px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr class="lblNormal">
            <td colspan="4" class="lblNormal">
                <font color="">(<font color='red'>*</font>) Vai trò</font>
            </td>
        </tr>
        <tr class="lblNormal">
            <td style="width: 114px"></td>
            <td style="width: 255px">
                <asp:DropDownList runat="server" ID="cboVaiTro" SkinID="DropDownList" BackColor="">
                </asp:DropDownList>
            </td>
            <td></td>
            <td></td>
        </tr>
        <!--trang thai-->
        <tr class="lblNormal" style="display: none">
            <td colspan="4" class="lblNormal">Trạng thái
            </td>
        </tr>
        <tr class="lblNormal" style="display: none">
            <td style="width: 114px"></td>
            <td style="width: 255px">
                <asp:CheckBox runat="server" ID="IsExpired" CssClass="lblNormal" Checked />Kích
                hoạt
            </td>
            <td></td>
            <td></td>
        </tr>
    </table>--%>

    <script type="text/javascript">

        // ADD haitx 
        // add more role
        function addmoreRole() {
            var id = "<%=_userID %>";
            var fk_roleid = $("#ctl00_FormContent_cboVaiTro").val();
            var fk_groupid = $("#ctl00_FormContent_cboPGD").val();
            var fk_parentgroupid = $("#ctl00_FormContent_cboChiNhanh").val();
            var url = "AddMoreRoleGroup.aspx"
            var query = "act=update&id=" + id + "&fk_parentgroupid=" + fk_parentgroupid + "&fk_roleid=" + fk_roleid + "&fk_groupid=" + fk_groupid;

            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (data) {
                    if (data == "updated") {
                        alert('Quyền kiêm nhiệm đã được thêm.');
                        window.opener = null;
                        window.close(true);
                    }
                    else if (data = "error") {
                        alert('Quyền này đã được kiêm nhiệm rồi ');
                        window.location.reload(true);
                    }
                }
            });
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ButtonExtendBefore" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ButtonExtend" runat="server">
    <br />
    <table style="width: 100%" class="table">
        <tr>
            <td align="left">
                <asp:GridView runat="server" ID="DataRole" DataKeyNames="GroupID,RoleID" OnRowDataBound="delImg">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Image runat="server" ID="imgDel" ImageUrl="~/Images/delete.gif" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="GroupName" HeaderText="chi nhánh" />
                        <asp:BoundField DataField="RoleName" HeaderText="Vai trò" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>

    <script type="text/javascript">
        // delete Role
        function deleteMoreRole(userid, groupid, roleid) {
            if (!confirm('Bạn có chắc muốn xóa quyền kiêm nhiệm này không'))
                return false
            var url = "AddMoreRoleGroup.aspx"
            var query = "act=delete&id=" + userid + "&roleid=" + roleid + "&groupid=" + groupid;

            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (data) {
                    if (data == "deleted") {
                        alert('Quyền kiêm nhiệm đã được xóa.');
                        window.location.reload(true);
                    }
                }
            });
        }
    </script>

</asp:Content>
