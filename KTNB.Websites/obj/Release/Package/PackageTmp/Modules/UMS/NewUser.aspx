<%@ Page Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" Inherits="VPB_PROMOTION.UMS.UMS_NewUser" CodeBehind="NewUser.aspx.cs" %>

<asp:Content runat="server" ID="NewUser" ContentPlaceHolderID="FormContent">
    <div class="form-horizontal">
        Thông tin chi tiết
        <!--thong tin chi tiet-->
        <div class="form-group" style="display: none">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Tên đăng nhập<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="FULLNAME_X" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group" style="display: none">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">E-mail<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="Email" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group" style="display: none">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Số mobile<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="MobilePhone" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Tên đăng nhập<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="UserName" runat="server" CssClass="form-control"></asp:TextBox>
                <img src="../../Images/indicator.gif" style="display: none" id="wait" />
            </div>
        </div>
        <div class="form-group" style="display: none">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Mật khẩu<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="Password" runat="server" CssClass="form-control"></asp:TextBox>
                (<asp:CheckBox runat="server" ID="PasswordDefault" Checked />Mặc định)
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Tên đầy đủ<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="FULLNAME" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputYear" class="col-sm-3 col-sm-offset-1 control-label">Ngày sinh</label>
            <div class="col-sm-6">
                <asp:TextBox ID="txtBirthDate" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputYear" class="col-sm-3 col-sm-offset-1 control-label">Ngày vào công ty</label>
            <div class="col-sm-6">
                <asp:TextBox ID="txtJoinDate" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Trình độ học vấn</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="EduLevel" runat="server" CssClass="form-control" Width="300px">
                    <asp:ListItem>Cao học</asp:ListItem>
                    <asp:ListItem>Đại học</asp:ListItem>
                    <asp:ListItem>Cao đẳng</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Mã DAO</label>
            <div class="col-sm-6">
                <asp:TextBox ID="UserCode" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Cấp quản lý<span class="star-red"></span></label>
            <div class="col-sm-6">
                <asp:DropDownList ID="cboChiNhanh" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Phòng ban<span class="star-red"></span></label>
            <div class="col-sm-6">
                <asp:DropDownList ID="cboPGD" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Vai trò<span class="star-red"></span></label>
            <div class="col-sm-6">
                <asp:DropDownList ID="cboVaiTro" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group" >
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Mô tả<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="Description" runat="server" CssClass="form-control"></asp:TextBox>
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
    <%--<table style="width: 100%">
        <!--thong tin chi tiet-->
        <tr class="GridHeader">
            <td colspan="4"><font color=''>Thông tin chi tiết</font></td>
        </tr>
        <tr class="lblNormal">
            <td style="display: none; width: 91px;">Tên đăng nhập:</td>
            <td style="display: none;">
                <asp:TextBox runat="server" ID="FULLNAME_X" SkinID="TextBoxRequired"></asp:TextBox></td>
            <td style="display: none;">E-mail:</td>
            <td style="display: none;">
                <asp:TextBox runat="server" ID="Email" SkinID="TextBox"></asp:TextBox></td>
        </tr>
        <tr class="lblNormal">
            <td style="display: none; width: 91px;">Số mobile:</td>
            <td style="display: none">
                <asp:TextBox runat="server" ID="MobilePhone" SkinID="TextBox"></asp:TextBox></td>
            <td style="display: none">Ghi chú:</td>
            <td style="display: none">
                <asp:TextBox runat="server" ID="Description" SkinID="TextBox"></asp:TextBox></td>
        </tr>

        <!--thong tin dang nhap-->
        <tr class="lblCaption" style='display: none'>
            <td colspan="4"><font color=''>Thông tin đăng nhập</font></td>
        </tr>
        <tr class="lblNormal">
            <td style="width: 91px" class="lblNormal">Tên đăng nhập</td>
            <td>
                <asp:TextBox runat="server" ID="UserName" SkinID="TextBoxRequired" BackColor=""></asp:TextBox>
                <img src="../../Images/indicator.gif" id="busy" style="display: none" />
                <label id="err"></label>
            </td>
            <td style="display: none;" class="lblNormal">Mật khẩu:</td>
            <td style="display: none;">
                <asp:TextBox runat="server" ID="Password" SkinID="TextBox" TextMode="Password" BackColor="" Text="123456" Width="130px"></asp:TextBox>
                (<asp:CheckBox runat="server" ID="PasswordDefault" Checked />Mặc định)
            </td>
        </tr>
        <tr class="lblNormal">
            <td style="width: 91px" class="lblNormal">Tên đầy đủ</td>
            <td>
                <asp:TextBox runat="server" ID="FULLNAME" SkinID="TextBoxRequired" BackColor=""></asp:TextBox>
            </td>
            <td colspan="2"></td>
        </tr>
        <!--phong ban-->
        <tr class="lblNormal">
            <td style="width: 91px" class="lblNormal">Mã DAO</td>
            <td colspan="3">
                <asp:TextBox runat="server" ID="UserCode" SkinID="TextBox" Width="80px"></asp:TextBox>
            </td>
        </tr>

        <tr class="lblNormal">
            <td style="width: 91px" class="lblNormal">
                <span style="color: ">Cấp quản lý</span></td>
            <td colspan="3">
                <asp:DropDownList runat="server" ID="cboChiNhanh"
                    SkinID="DropDownListRequired"
                    AutoPostBack="True"
                    Width="342px">
                </asp:DropDownList>
                <span style="color: #a52a2a"></span>
            </td>
        </tr>

        <!--vai tro-->
        <tr class="lblNormal">
            <td style="width: 91px">
                <span style="color: " class="lblNormal">Đơn vị</span></td>
            <td colspan="3">
                <asp:DropDownList ID="cboPGD" SkinID="DropDownList" runat="server" BackColor="" Width="343px">
                </asp:DropDownList></td>
        </tr>
        <tr class="lblNormal">
            <td style="width: 91px">Vai trò nhiệm vụ</td>
            <td colspan="3">
                <asp:DropDownList runat="server" ID="cboVaiTro" SkinID="DropDownListRequired" BackColor=""></asp:DropDownList>
            </td>
        </tr>

        <!--trang thai-->
        <tr class="lblNormal" style="display: none">
            <td style="width: 91px"><font color=''>Trạng thái</font></td>
            <td>
                <asp:CheckBox runat="server" ID="IsExpired" Checked />Kích hoạt</td>
            <td></td>
            <td></td>
        </tr>
    </table>--%>
    <script type="text/javascript">
        $("#<%=txtBirthDate.ClientID%>").datepicker({
            showOn: 'both',
            buttonImage: "../../Images/calendar.png",
            buttonImageOnly: true,

        });
        $("#<%=txtJoinDate.ClientID%>").datepicker({
            showOn: 'both',
            buttonImage: "../../Images/calendar.png",
            buttonImageOnly: true,

        });
        function checkAccountName(obj, accName) {
            if (accName == "") {
                obj.focus();
                return false;
            }
            $("#busy").show();
            var url = "checkaccount.aspx";
            var cond = "u=" + accName;
            $.ajax({
                type: "POST",
                url: url,
                data: cond,
                success: function (data) {
                    $("#busy").hide();
                    if (data == "exists") {
                        alert("Tên đăng nhập này đã được cấp phép.");
                        obj.focus();
                        return false;
                    }
                }
            });
        }
        function createuser() {
            var query = "";
            query = parsingform();

            if (query == undefined) return;
            if (query == "") return;
            var docspace = "<%=_docspace %>";
            var url = "NewUser.aspx";
            //alert(query); return;
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (data) {
                    alert(data);
                    //                    var url = "../../Modules/MainView/FolderViewer.aspx?op=&docspace=" + docspace;
                    //                    window.open(url, 'fraToc');
                }
            });
        }
        function parsingform() {
            var uname = $("#ctl00_FormContent_UserName").val();
            if (uname == "") {
                $("#ctl00_FormContent_UserName").focus();
                return;
            }
            var fullname = $("#ctl00_FormContent_FULLNAME").val();
            if (fullname == "") {
                $("#ctl00_FormContent_FULLNAME").focus();
                return;
            }
            var cn = $("#ctl00_FormContent_cboChiNhanh").val();
            if (cn == "") {
                $("#ctl00_FormContent_cboChiNhanh").focus();
                return;
            }
            var birthDate = $("#<%=txtBirthDate.ClientID%>").val();
            if (birthDate == "") {
                $("#<%=txtBirthDate.ClientID%>").focus();
                return;
            }
            var joinDate = $("#<%=txtJoinDate.ClientID%>").val();
            if (joinDate == "") {
                $("#<%=txtJoinDate.ClientID%>").focus();
                return;
            }
            var description = $("#<%=Description.ClientID%>").val();
            if (description == "") {
                $("#<%=Description.ClientID%>").focus();
                return;
            }
            var edu = $("#<%=EduLevel.ClientID%>").val();
            if (joinDate == "") {
                $("#<%=EduLevel.ClientID%>").focus();
                return;
            }
            
            var pgd = $("#ctl00_FormContent_cboPGD").val();
            if (pgd == null) pgd = "ALL";
            var vt = $("#ctl00_FormContent_cboVaiTro").val();
            var exp = 1;
            if ($("#ctl00_FormContent_IsExpired").attr("checked"))
                exp = 0;
            var uc = $("#ctl00_FormContent_UserCode").val();
            var docspace = "<%=_docspace %>";
            var query = "act=new&fn=&em=&mb=&un=" + uname + "&uc=" + uc + "&des=" + description + "&edu=" + edu + "&joinDate=" + joinDate + "&birthDate=" + birthDate + "&pwd=&cn=" + cn + "&pgd=" + pgd + "&vt=" + vt + "&exp=" + exp + "&fn=" + fullname + "&docspace=" + docspace;
            return query;
        }
    </script>
</asp:Content>
<asp:Content runat="server" ID="ExtendButton" ContentPlaceHolderID="ButtonExtend">
</asp:Content>
