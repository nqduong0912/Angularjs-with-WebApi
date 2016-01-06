<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="QLDonViKTNB_Input.aspx.cs" Inherits="VPB_KTNB.Modules.UMS.DonViKTNB.QLDonViKTNB_Input" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <div class="form-horizontal">
        <div class="form-group">
            <div class="col-sm-3 col-sm-offset-1 col-xs-4">
                <label for="inputDescription" class="control-label">Mã đơn vị<span class="star-red">*</span></label>
            </div>
            <div class="col-sm-6">
                <asp:TextBox ID="txtMaDV" runat="server" CssClass="form-control" Width="300px">
                </asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-3 col-sm-offset-1 col-xs-4">
                <label for="inputDescription" class="control-label">Tên đơn vị<span class="star-red">*</span></label>
            </div>
            <div class="col-sm-6">
                <asp:TextBox ID="txtTenDV" runat="server" CssClass="form-control" Width="300px">
                </asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-3 col-sm-offset-1 col-xs-4">
                <label for="inputDescription" class="control-label">Trưởng phòng<span class="star-red">*</span></label>
            </div>
            <div class="col-sm-6">
                <asp:DropDownList ID="drpTP" runat="server" CssClass="form-control" Width="300px">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-3 col-sm-offset-1 col-xs-4">
                <label for="inputDescription" class="control-label">Nguồn lực năm (man/day)<span class="star-red">*</span></label>
            </div>
            <div class="col-sm-6">
                <asp:TextBox ID="txtNL" runat="server" CssClass="form-control" Width="300px">
                </asp:TextBox>
            </div>
        </div>
         <div class="form-group">
            <div class="col-sm-3 col-sm-offset-1 col-xs-4">
                <label for="inputDescription" class="control-label">Trạng thái</label>
            </div>
            <div class="col-sm-6">
                <asp:DropDownList ID="drpTT" runat="server" CssClass="form-control" Width="300px">
                </asp:DropDownList>
            </div>
        </div>
    </div>
    <div class="text-center">
        <asp:Button runat="server" ID="btnSave" CssClass="btn btn-xs btn-success btn-submit" Text="Lưu" OnClick="btnSave_OnClick" />
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ButtonExtendBefore" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ButtonExtend" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FormFooter" runat="server">
</asp:Content>
