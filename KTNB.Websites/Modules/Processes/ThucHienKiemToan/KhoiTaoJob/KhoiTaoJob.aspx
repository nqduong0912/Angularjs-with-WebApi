<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="KhoiTaoJob.aspx.cs" Inherits="VPB_KTNB.Modules.Processes.ThucHienKiemToan.KhoiTaoJob.KhoiTaoJob" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <div data-ng-controller="thongTinDotKiemToanController" data-ng-init="initFunc('<%= Id %>', '<%= LoaiDoiTuongKiemToanId %>')" class="vpb-ctrl">
        <div ng-form="myForm">
            <div ng-switch="currentView">
                <div ng-switch-when="0">
                    <!-- Nav tabs -->
                    <ul class="nav nav-tabs" role="tablist">
                        <li role="presentation" class="active"><a href="#thong-tin-chung-tab" aria-controls="thong-tin-chung-tab" role="tab" data-toggle="tab">Thông tin chung</a></li>
                        <li role="presentation"><a href="#chon-thanh-vien-dot-kiem-toan-tab" aria-controls="chon-thanh-vien-dot-kiem-toan-tab" role="tab" data-toggle="tab">Chọn thành viên đợt kiểm toán</a></li>
                        <li role="presentation"><a href="#chon-mang-nghiep-vu-tab" aria-controls="chon-mang-nghiep-vu-tab" role="tab" data-toggle="tab">Chọn mảng nghiệp vụ</a></li>
                        <li role="presentation"><a href="#phan-cong-thanh-vien-nhap-risk-profile-tab" aria-controls="phan-cong-thanh-vien-nhap-risk-profile-tab" role="tab" data-toggle="tab">Phân công thành viên nhập Risk profile</a></li>
                        <li role="presentation"><a href="#phe-duyet-risk-profile-tab" aria-controls="phe-duyet-risk-profile-tab" role="tab" data-toggle="tab">Phê duyệt Risk profile</a></li>
                    </ul>

                    <!-- Tab panes -->
                    <div class="tab-content">
                        <div role="tabpanel" class="tab-pane active" id="thong-tin-chung-tab">
                            <div ng-include src="'/app/views/thucHienKiemToan/khoiTaoJob/ucJob_1_1.html'"></div>
                        </div>
                        <div role="tabpanel" class="tab-pane" id="chon-thanh-vien-dot-kiem-toan-tab">
                            <div ng-include src="'/app/views/thucHienKiemToan/khoiTaoJob/ucJob_1_2.html'"></div>
                        </div>
                        <div role="tabpanel" class="tab-pane" id="chon-mang-nghiep-vu-tab">
                            <div ng-include src="'/app/views/thucHienKiemToan/khoiTaoJob/ucJob_1_3.html'"></div>
                        </div>
                        <div role="tabpanel" class="tab-pane" id="phan-cong-thanh-vien-nhap-risk-profile-tab">
                            <div ng-include src="'/app/views/thucHienKiemToan/khoiTaoJob/ucJob_1_4.html'"></div>
                        </div>
                        <div role="tabpanel" class="tab-pane" id="phe-duyet-risk-profile-tab">
                            <div ng-include src="'/app/views/thucHienKiemToan/khoiTaoJob/ucJob_1_6.html'"></div>
                        </div>
                    </div>
                </div>

                <div ng-switch-when="1" ng-form="suaMangNghiepVuForm">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <label for="ten-sua-mang-nghiep-vu" class="control-label col-sm-4">Mảng nghiệp vụ <span class="star-red">*</span></label>
                            <div class="col-sm-8">
                                <input ng-model="mangNghiepVuChosen.Ten" id="ten-sua-mang-nghiep-vu" name="ten" type="text" required class="form-control" autofocus />
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-offset-4 col-sm-8">
                                <div class="vpb-btns">
                                    <button type="button" class="btn btn-primary" ng-click="luuSuaMangNghiepVu()">Lưu</button>
                                    <button type="button" class="btn btn-primary" ng-click="huySuaMangNghiepVuFunc()">Hủy</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
