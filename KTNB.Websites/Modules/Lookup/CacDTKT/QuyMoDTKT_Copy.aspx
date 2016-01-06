<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="QuyMoDTKT_Copy.aspx.cs" Inherits="VPB_KTNB.Modules.Lookup.CacDTKT.QuyMoDTKT_Copy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <div data-ng-controller="quyMoDoiTuongKiemToanCopyController" data-ng-init="initFunc('<%= _idboquymo %>')" class="vpb-ctrl">
        <div class="form-horizontal">
            <div class="form-group">
                <div class="col-sm-3 col-sm-offset-1 col-xs-4">
                    <label for="tu-loai-dtkt-qmdtktc" class="control-label">Từ loại ĐTKT</label>
                </div>
                <div class="col-sm-6">
                    <input id="tu-loai-dtkt-qmdtktc" type="text" class="form-control" disabled data-ng-model="item.LoaiDTKT" />
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-3 col-sm-offset-1 col-xs-4">
                    <label for="ten-cu-bo-quy-mo-qmdtktc" class="control-label">Tên cũ bộ quy mô</label>
                </div>
                <div class="col-sm-6">
                    <input id="ten-cu-bo-quy-mo-qmdtktc" type="text" class="form-control" disabled data-ng-model="item.Ten" />
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-3 col-sm-offset-1 col-xs-4">
                    <label for="copy-toi-loai-dtkt-qmdtktc" class="control-label">Copy tới Loại ĐTKT</label>
                </div>
                <div class="col-sm-6">
                    <select id="copy-toi-loai-dtkt-qmdtktc" class="form-control" data-ng-model="newItem.LoaiDTKT" data-ng-options="loaiDoiTuongKiemToan for loaiDoiTuongKiemToan in DanhSachLoaiDoiTuongKiemToan"></select>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-3 col-sm-offset-1 col-xs-4">
                    <label for="ten-moi-bo-quy-mo-qmdtktc" class="control-label">Tên mới bộ quy mô<span class="star-red">*</span></label>
                </div>
                <div class="col-sm-6">
                    <input id="ten-moi-bo-quy-mo-qmdtktc" type="text" class="form-control" data-ng-model="newItem.Ten" />
                </div>
            </div>
        </div>
        <div class="text-center">
            <button type="button" class="btn btn-primary" data-ng-click="copyFunc()">Copy</button>
            <button type="button" class="btn btn-primary" data-ng-click="backFunc()">Quay lại</button>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ButtonExtendBefore" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ButtonExtend" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FormFooter" runat="server">
</asp:Content>
