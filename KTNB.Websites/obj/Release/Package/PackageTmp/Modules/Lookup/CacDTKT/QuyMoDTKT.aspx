<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="QuyMoDTKT.aspx.cs" Inherits="VPB_KTNB.Modules.Lookup.CacDTKT.QuyMoDTKT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <div data-ng-controller="quyMoDoiTuongKiemToanController" data-ng-init="initFunc()">

        <div class="form-inline" style="margin-bottom: 20px">
            <div class="form-group">
                <label for="inputName" class="control-label">Năm</label>
                <select class="form-control" data-ng-model="Nam" data-ng-options="nam for nam in DanhSachNam" data-ng-change="namChangeFunc()"></select>
            </div>
            <div class="form-group">
                <label for="inputName" class="control-label">Loại DTKT</label>
                <select class="form-control" data-ng-model="LoaiDoiTuongKiemToan" data-ng-options="loaidoituongkiemtoan for loaidoituongkiemtoan in DanhSachLoaiDoiTuongKiemToan" data-ng-change="loaiDoiTuongKiemToanChangeFunc()"></select>
            </div>
            <button type="button" class="btn btn-xs btn-success btn-submit" data-ng-click="searchFunc()">Search</button>
        </div>
        <nav data-ng-include src="'/HtmlTemplates/paged-list.html'"></nav>
        <div class="table-responsive">
            <table class="vpb--table table table-striped table-bordered table-hover">
                <thead class="color-green">
                    <tr>
                        <th style="display: none">ID</th>
                        <th>Năm</th>
                        <th>Loại đtkt</th>
                        <th>Bộ quy mô</th>
                        <th>On/Off</th>
                        <th>Hành động</th>
                    </tr>
                </thead>
                <tbody>
                    <tr data-ng-repeat="item in data.Items">
                        <td>{{item.Nam}}
                        </td>
                        <td>{{item.LoaiDTKT}}
                        </td>
                        <td>{{item.Ten}}
                        </td>
                        <td>
                            <input type="checkbox" data-ng-model="item.Trangthai" data-ng-click="editSatusFunc(item)" />
                        </td>
                        <td>
                            <a title="Sửa" data-ng-click="editFunc(item.Id)"><i class="glyphicon glyphicon-search"></i></a>
                            <a title="Copy" data-ng-click="copyFunc(item.Id)"><i class="glyphicon glyphicon-copy"></i></a>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <nav data-ng-include src="'/HtmlTemplates/paged-list.html'"></nav>
        <button type="button" class="btn btn-xs btn-success btn-submit" data-ng-click="newFormFunc()">Thêm mới</button>
    </div>
</asp:Content>
