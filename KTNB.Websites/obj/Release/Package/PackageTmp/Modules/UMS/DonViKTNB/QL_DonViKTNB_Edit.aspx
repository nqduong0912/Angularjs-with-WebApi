﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="QL_DonViKTNB_Edit.aspx.cs" Inherits="VPB_KTNB.Modules.UMS.DonViKTNB.QL_DonViKTNB_Input" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <div data-ng-controller="donViKiemToanNoiBoEditController" data-ng-init="initFunc()">
        <div data-ng-form="myForm">
            <div class="form-horizontal">
                <div class="form-group">
                    <div class="col-sm-3 col-sm-offset-1 col-xs-4">
                        <label for="inputDescription" class="control-label">Mã đơn vị<span class="star-red">*</span></label>
                    </div>
                    <div class="col-sm-6">
                        <input name="donViId" type="text" data-ng-model="item.MaDonVi" class="form-control" required maxlength="20" />
                        <div data-ng-show="myForm.donViId.$dirty">
                            <span data-ng-show="myForm.donViId.$error.required" class="text-danger">Vui lòng nhập Mã đơn vị</span>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-3 col-sm-offset-1 col-xs-4">
                        <label for="inputDescription" class="control-label">Tên đơn vị<span class="star-red">*</span></label>
                    </div>
                    <div class="col-sm-6">
                        <input name="tenDonVi" type="text" class="form-control" required data-ng-model="item.Ten" maxlength="70" />
                        <div data-ng-show="myForm.tenDonVi.$dirty">
                            <span data-ng-show="myForm.tenDonVi.$error.required" class="text-danger">Vui lòng nhập Tên đơn vị</span>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-3 col-sm-offset-1 col-xs-4">
                        <label for="inputDescription" class="control-label">Trưởng phòng<span class="star-red">*</span></label>
                    </div>
                    <div class="col-sm-6">
                        <select class="form-control" data-ng-model="item.MaTruongPhong" name="truongPhong" data-ng-options="u.PK_UserID as u.Name for u in dsTruongPhong" required></select>
                        <div data-ng-show="myForm.truongPhong.$dirty">
                            <span data-ng-show="myForm.truongPhong.$error.required" class="text-danger">Vui lòng chọn trưởng phòng</span>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-3 col-sm-offset-1 col-xs-4">
                        <label for="inputDescription" class="control-label">Nguồn lực<span class="star-red">*</span></label>
                    </div>
                    <div class="col-sm-6">
                        <input name="nguonLuc" type="text" class="form-control" required data-ng-model="item.NguonLuc" />
                        <div data-ng-show="myForm.nguonLuc.$dirty">
                            <span data-ng-show="myForm.nguonLuc.$error.required" class="text-danger">Vui lòng nhập nguồn lực của phòng ban</span>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-3 col-sm-offset-1 col-xs-4">
                        <label for="inputDescription" class="control-label">Trạng thái<span class="star-red">*</span></label>
                    </div>
                    <div class="col-sm-6">
                        <select class="form-control" name="trangThai" style="width: 100px" data-ng-model="item.TrangThai" data-ng-options="t.value as t.text for t in dsTrangThai"></select>
                    </div>
                </div>
            </div>
            <button type="button" class="btn btn-xs btn-success btn-submit" data-ng-click="saveFunc()">Lưu</button>
        </div>
    </div>
</asp:Content>

