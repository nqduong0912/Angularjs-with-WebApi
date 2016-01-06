<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="DonViKiemtoan_Manager.aspx.cs" Inherits="VPB_KTNB.Modules.Admin.DonViKiemtoan_Manager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <div class="form-horizontal">
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Các đơn vị kiểm toán</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="cboChiNhanh" runat="server" Width="300px"  CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cboChiNhanh_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-4 col-sm-2">
                <asp:Button runat="server" ID="btnAddnew" Text="Thêm mới" SkinID="InsertButton" />
            </div>

        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ButtonExtendBefore" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ButtonExtend" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FormFooter" runat="server">
</asp:Content>
