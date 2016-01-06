<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="DotKiemToan.aspx.cs" Inherits="VPB_KTNB.Modules.Processes.ThucHienKiemToan.KhoiTaoJob.DotKiemToan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <div class="form-horizontal" data-ng-controller="dotKiemToanController" data-ng-init="initFunc()">
        <div ng-include src="'/app/views/thucHienKiemToan/khoiTaoJob/ucJob_1.html'"></div>
    </div>
</asp:Content>
