<%@ Page Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="XemLaiDoiTuong.aspx.cs" Inherits="VPB_KTNB.Modules.Processes.KeHoachNam.XemLaiDoiTuong" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <div data-ng-controller="xemCacDoiTuongKiemToanDaChonController" data-ng-init="initFunc()">
        <nav data-ng-include="'/HtmlTemplates/paged-list.html'"></nav>
        <div class="table-responsive">
            <table class="vpb--table table table-striped table-bordered table-hover">
                <thead>
                    <tr>
                        <th>Rank</th>
                        <th>Loại ĐTKT</th>
                        <th>ĐTKT</th>
                        <th>Điểm gốc</th>
                        <th>Điểm sửa đổi</th>
                        <th>Thời gian kt gần nhất</th>
                        <th>Quy mô đtkt</th>
                        <th>Tháng dự kiến KT</th>
                        <th>Tần suất</th>
                        <th>Đợt Kt tiếp 1</th>
                        <th>Đợt Kt tiếp 2</th>
                        <th>Mục tiêu</th>
                        <th>Phạm vi</th>
                        <th>Phòng</th>
                        <th>Trưởng đoàn</th>
                        <th>Manager</th>
                    </tr>
                </thead>
                <tbody>
                    <tr data-ng-repeat="item in data.Items">
                        <td>{{ item.RankText }}</td>
                        <td>{{ item.LoaiDTKTText }}</td>
                        <td>{{ item.DTKTText }}</td>
                        <td>{{ item.GiaTriGoc }}</td>
                        <td>{{ item.DiemQuyDoi }}</td>
                        <td>{{ item.ThoiGianKTGanNhatText }}</td>
                        <td>{{ item.QuyMo }}</td>
                        <td><span data-ng-show="item.ThangDuKienKT">Tháng {{ item.ThangDuKienKT }}</span></td>
                        <td>{{ item.TanSuatText }}</td>
                        <td>{{ item.DotKTTiep1 | date : 'MM/yyyy' }}</td>
                        <td>{{ item.DotKTTiep2 | date : 'MM/yyyy' }}</td>
                        <td>{{ item.MucTieu }}</td>
                        <td>{{ item.PhamVi }}</td>
                        <td>{{ item.PhongText }}</td>
                        <td>{{ item.TruongDoanText }}</td>
                        <td>{{ item.ManagerText }}</td>
                    </tr>
                </tbody>
            </table>
        </div>
        <nav data-ng-include="'/HtmlTemplates/paged-list.html'"></nav>

        <div class="">
            <a class="btn btn-xs btn-success btn-submit" href="<%= ResolveUrl("~/modules/Processes/Kehoachnam/LapKeHoachNam.aspx" + Request.Url.Query) %>">Quay lại</a>
        </div>
    </div>
</asp:Content>
