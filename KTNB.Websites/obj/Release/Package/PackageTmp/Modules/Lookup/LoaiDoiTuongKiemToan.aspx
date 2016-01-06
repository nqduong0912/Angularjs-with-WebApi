<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="LoaiDoiTuongKiemToan.aspx.cs" Inherits="VPB_TDHS.Modules.Lookup.LoaiDoiTuongKiemToan" %>

<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <div data-ng-controller="loaiDoiTuongKiemToanController" data-ng-init="initFunc()">
        <nav data-ng-include="'/HtmlTemplates/paged-list.html'"></nav>
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
        <nav data-ng-include="'/HtmlTemplates/paged-list.html'"></nav>
        <br />
        <a href="#" class="btn btn-xs btn-success btn-block btn-submit" data-ng-click="addNew()">Thêm mới</a>
    </div>
</asp:Content>
