<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="QuyMoDTKT_Edit.aspx.cs" Inherits="VPB_KTNB.Modules.Lookup.CacDTKT.QuyMoDTKT_Edit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <div data-ng-controller="quyMoDoiTuongKiemToanEditController" data-ng-init="initFunc('<%= _id %>')" class="vpb-ctrl">
        <div class="form-group">1.Thông tin bộ quy mô</div>
        <div class="form-horizontal">
            <div class="form-group">
                <div class="col-sm-3 col-sm-offset-1 col-xs-4">
                    <label for="nam-qmdtkte" class="control-label">Năm</label>
                </div>
                <div class="col-sm-6">
                    <select id="nam-qmdtkte" class="form-control" data-ng-model="item.Nam" data-ng-options="nam as nam for nam in DanhSachNam" data-ng-change="namChangeFunc()"></select>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-3 col-sm-offset-1 col-xs-4">
                    <label for="loai-dtkt-qmdtkte" class="control-label">Loại DTKT</label>
                </div>
                <div class="col-sm-6">
                    <select id="loai-dtkt-qmdtkte" class="form-control" data-ng-model="item.LoaiDTKT" data-ng-options="loaidoituongkiemtoan for loaidoituongkiemtoan in DanhSachLoaiDoiTuongKiemToan" data-ng-change="loaiDoiTuongKiemToanChangeFunc()"></select>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-3 col-sm-offset-1 col-xs-4">
                    <label for="ten-bo-quy-mo-qmdtkte" class="control-label">Tên bộ quy mô<span class="star-red">*</span></label>
                </div>
                <div class="col-sm-6">
                    <input id="ten-bo-quy-mo-qmdtkte" type="text" class="form-control" required data-ng-model="item.Ten" />
                </div>
            </div>
        </div>
        <div class="form-group">2. Danh mục quy mô thuộc bộ quy mô</div>
        <div class="form-inline" style="margin-bottom: 20px">
            <div class="form-group">
                <label class="control-label" for="quy-mo-qmdtkte">Quy mô <span class="star-red">*</span></label>
                <input id="quy-mo-qmdtkte" type="text" class="form-control" data-ng-model="tenQuyMo" />
            </div>
            <div class="form-group" style="margin-left: 30px">
                <label class="control-label" for="nguon-luc-qmdtkte">Nguồn lực<span class="star-red">*</span></label>
                <input id="nguon-luc-qmdtkte" type="text" class="form-control" data-ng-model="nguonLuc" />
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
                        <tr data-ng-repeat="quyMo in item.LstQuyMo" data-ng-model="item.LstQuyMo">
                            <td>{{quyMo.Ten}}
                            </td>
                            <td>{{quyMo.NguonLuc}}
                            </td>
                            <td>
                                <a ><span class="glyphicon glyphicon-remove" data-ng-click="deleteQuyMoFunc(quyMo)"></span></a>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>

        <div class="text-center">
            <button type="button" class="btn btn-primary" data-ng-click="updateFunc()">Lưu</button>
            <button type="button" class="btn btn-primary" data-ng-click="backFunc()">Quay lại</button>
        </div>
    </div>
</asp:Content>
