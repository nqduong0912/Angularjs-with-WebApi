<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="NhapRiskProfile.aspx.cs" Inherits="VPB_KTNB.Modules.Processes.ThucHienKiemToan.KhoiTaoJob.NhapRiskProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <div ng-controller="nhapRiskProfileController" ng-init="initFunc()">
        <div ng-include src="'/app/views/thucHienKiemToan/khoiTaoJob/ucJob_1_5.html'"></div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ButtonExtendBefore" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ButtonExtend" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FormFooter" runat="server">
</asp:Content>
