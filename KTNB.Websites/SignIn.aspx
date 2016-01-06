<%@ Page Language="C#" AutoEventWireup="true" Inherits="SignIn" Title="" CodeBehind="SignIn.aspx.cs" EnableViewStateMac="true" %>
<%@ OutputCache Duration="14400" VaryByParam="None" %>
<!DOCTYPE html>

<html lang="vi">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
    <title>SignIn</title>    
     <webopt:bundlereference runat="server" path="~/Content/css" />
    <link rel="shortcut icon" href="~/Images/Icon/favicon.ico" type="image/x-icon" />
</head>
<body id="page-login">
    <form id="form1" runat="server">
        <div class="cl-banner">
            <img runat="server" class="img-responsive" src="~/Images/banner.jpg" />
            <div class="text-banner navbar-header navbar-right col-md-7 col-md-offset-5">
                <span>Phần mềm hỗ trợ hoạt động kiểm toán nội bộ</span>
            </div>
            <div class="line"></div>
        </div>

        <!-- <div id="wrapper"> -->
        <div class="container">
            <div class="row">
                <div class="col-md-5 col-sm-5">
                    <div class="login-panel">
                        <div class="login-heading">
                            <h2 class="login-title">Đăng nhập hệ thống VPBank</h2>
                        </div>
                        <div class="body-form">
                            <div role="form">
                                <div class="form-group has-success has-feedback">
                                    <label class="control-label" for="<%= UserName.ClientID %>">Tên đăng nhập:</label>
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="fa fa-user fa-lg"></i></span>
                                        <asp:TextBox ID="UserName" runat="server" MaxLength="100" CssClass="form-control" />
                                    </div>
                                    <asp:RequiredFieldValidator ID="rfvUserNameRequired" runat="server" Visible="true" ControlToValidate="UserName" Font-Bold="true" ErrorMessage="?" />
                                </div>
                                <div class="form-group has-success has-feedback">
                                    <label class="control-label" for="<%= Password.ClientID %>">Mật khẩu:</label>
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="fa fa-lock fa-lg"></i></span>
                                        <asp:TextBox ID="Password" runat="server" TextMode="Password" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Visible="true" ControlToValidate="Password" Font-Bold="true" ErrorMessage="?" />
                                </div>
                                <div class="btn-login">
                                    <asp:Button runat="server" ID="LoginButton" CssClass="btn btn-primary btn-block" Text="Đăng nhập" />
                                    <asp:Label runat="server" ID="lblLoginFail" CssClass="lblTinVan2" EnableViewState="false"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-offset-2 col-md-5 col-sm-offset-2 col-sm-5">
                    <img runat="server" class="img-right img-responsive" src="~/Images/icon-home.png" alt="Icon" />
                </div>
            </div>
        </div>
        <div class="cl-footer">
            <div class="row">
                <div class="col-lg-12 img-footer">
                    <img runat="server" class="img-responsive" src="~/Images/img-footer.png" alt="Footer" />
                    <div class="footer-content">
                        <p>Ngân hàng Việt Nam Thịnh vượng VPBank © 2012.</p>
                        <p>Trụ sở chính: 72 Trần Hưng Đạo, Hoàn Kiếm, Hà Nội. Điện thoại: 043.9288869. Fax: 043.9288867. Email: customercare@vpb.com.vn</p>
                        <p>SWIFT code: VPBKVNVX. Sơ đồ website | Chính sách bảo mật | Webmail</p>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <script type="text/javascript" src="<%= ResolveUrl("~/Scripts/jquery-1.11.3.min.js") %>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("~/Scripts/jquery.blockUI.js") %>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("~/Javascript/BackEndProcess.js") %>"></script>
    <script type="text/javascript">
        jQuery("#<%= UserName.ClientID  %>").focus();
    </script>
</body>
</html>
