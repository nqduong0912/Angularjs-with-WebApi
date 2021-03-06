﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="QuyMoDTKT_Input.aspx.cs" Inherits="VPB_KTNB.Modules.Lookup.CacDTKT.QuyMoDTKT_Input" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <div data-ng-controller="quyMoDoiTuongKiemToanInputController" data-ng-init="initFunc()" class="vpb-ctrl">
        <div class="form-group">1.Thông tin bộ quy mô</div>
        <div class="form-horizontal">
            <div class="form-group">
                <div class="col-sm-3 col-sm-offset-1 col-xs-4">
                    <label for="nam-qmdtkti" class="control-label">Năm</label>
                </div>
                <div class="col-sm-6">
                    <select id="nam-qmdtkti" class="form-control" data-ng-model="Nam" data-ng-options="nam for nam in DanhSachNam" data-ng-change="namChangeFunc()"></select>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-3 col-sm-offset-1 col-xs-4">
                    <label for="loai-dtkt-qmdtkti" class="control-label">Loại DTKT</label>
                </div>
                <div class="col-sm-6">
                    <select id="loai-dtkt-qmdtkti" class="form-control" data-ng-model="LoaiDoiTuongKiemToan" data-ng-options="loaidoituongkiemtoan for loaidoituongkiemtoan in DanhSachLoaiDoiTuongKiemToan" data-ng-change="loaiDoiTuongKiemToanChangeFunc()"></select>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-3 col-sm-offset-1 col-xs-4">
                    <label for="ten-bo-quy-mo-qmdtkti" class="control-label">Tên bộ quy mô<span class="star-red">*</span></label>
                </div>
                <div class="col-sm-6">
                    <input id="ten-bo-quy-mo-qmdtkti" type="text" class="form-control" required data-ng-model="tenBoQuyMo" />
                </div>
            </div>
        </div>
        <div class="form-group">2. Danh mục quy mô thuộc bộ quy mô</div>
        <div class="form-inline col-sm-3 col-sm-offset-1 col-xs-4" style="margin-bottom: 20px">
            <div class="form-group">
                <label class="control-label" for="ten-quy-mo-qmdtkti">Quy mô <span class="star-red">*</span></label>
                <input id="ten-quy-mo-qmdtkti" type="text" class="form-control" data-ng-model="tenQuyMo" />
            </div>
            <div class="form-group" style="margin-left: 30px">
                <label class="control-label" for="nguon-luc-qmdtkti">Nguồn lực<span class="star-red">*</span></label>
                <input id="nguon-luc-qmdtkti" type="text" class="form-control" data-ng-model="nguonLuc" />
                <a class="btn btn-primary-element" data-ng-click="insertQuyMoFunc()">Thêm</a>
            </div>

        </div>

        <div class="body-table">
            <div class="table-responsive">
                <table class="table table-striped table-bordered table-hover">
                    <thead class="color-green">
                        <tr>
                            <th>Quy mô</th>
                            <th>Nguồn lực</th>
                            <th>Hành động</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr data-ng-repeat="quyMo in DanhSachQuyMo">
                            <td>{{quyMo.Ten}}
                            </td>
                            <td>{{quyMo.NguonLuc}}
                            </td>
                            <td>
                                <a ><span class="glyphicon glyphicon-remove" data-ng-click="deleteQuyMoFunc()"></span></a>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>

        <div class="text-center">
            <button type="button" class="btn btn-primary" data-ng-click="insertFunc()">Lưu</button>
            <button type="button" class="btn btn-primary" data-ng-click="backFunc()">Quay lại</button>
        </div>
    </div>
</asp:Content>
