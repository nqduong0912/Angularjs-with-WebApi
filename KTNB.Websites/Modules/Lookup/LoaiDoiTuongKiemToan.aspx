<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="LoaiDoiTuongKiemToan.aspx.cs" Inherits="VPB_TDHS.Modules.Lookup.LoaiDoiTuongKiemToan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <div data-ng-controller="loaiDoiTuongKiemToanController" data-ng-init="initFunc()" class="vpb-ctrl">
        <div data-ng-show="data.TotalPages > 0" class="vpb--pagination clearfix">
            <div class="pull-left">
                <span>Trang {{ data.CurrentPage }}/{{ data.TotalPages }}</span>
            </div>
            <div class="pull-right">
                <pagination total-items="data.TotalItems" ng-model="data.CurrentPage" max-size="data.NumberOfLinks" ng-change="changePageFunc(data.CurrentPage)" class="pagination-sm" boundary-links="true" rotate="false" num-pages="numPages" previous-text="&lsaquo;" next-text="&rsaquo;" first-text="&laquo;" last-text="&raquo;"></pagination>
            </div>
        </div>
        <div class="table-responsive">
            <table class="vpb--table table table-striped table-bordered table-hover">
                <thead>
                    <tr>
                        <th>Tên loại đối tượng kiểm toán</th>
                        <th>Diễn giải</th>
                        <th>Số lượng ĐTKT</th>
                        <th>Trạng thái</th>
                        <th>Thao tác</th>
                    </tr>
                </thead>
                <tbody>
                    <tr data-ng-repeat="item in data.Items">
                        <td>{{ item.Ten }}</td>
                        <td>{{ item.Diengiai }}</td>
                        <td>
                            <a href="#" data-ng-click="forwardFunc()">{{ item.SoLuongDTKT }}</a>
                        </td>
                        <td>
                            <span data-ng-show="item.Status == 2">Inactive</span>
                            <span data-ng-show="item.Status == 4">Active</span>
                        </td>
                        <td>
                            <a class="click-icon" href="#" title="Chi tiết" data-ng-click="loadDocumentFunc(item.PK_DocumentID)">
                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                            </a>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div data-ng-show="data.TotalPages > 0" class="vpb--pagination clearfix">
            <div class="pull-left">
                <span>Trang {{ data.CurrentPage }}/{{ data.TotalPages }}</span>
            </div>
            <div class="pull-right">
                <pagination total-items="data.TotalItems" ng-model="data.CurrentPage" max-size="data.NumberOfLinks" ng-change="changePageFunc(data.CurrentPage)" class="pagination-sm" boundary-links="true" rotate="false" num-pages="numPages" previous-text="&lsaquo;" next-text="&rsaquo;" first-text="&laquo;" last-text="&raquo;"></pagination>
            </div>
        </div>
        <br />
        <a href="#" class="btn btn-primary" data-ng-click="addNew()">Thêm mới</a>
    </div>
</asp:Content>
