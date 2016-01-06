<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="LapKeHoachNam.aspx.cs" Inherits="VPB_KTNB.Modules.Processes.KeHoachNam.LapKeHoachNam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <div class="" data-ng-controller="lapKeHoachNamController" data-ng-init="initFunc()">
        <div>
            <div class="row">
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="nguon-luc-nam">Nguồn lực năm</label>
                        <input id="nguon-luc-nam" type="text" data-ng-model="nguonLucNam" class="form-control" readonly />
                    </div>
                </div>
                <div class="col-md-offset-1 col-md-3">
                    <div class="form-group">
                        <label for="nguon-luc-can-thiet">Nguồn lực cần thiết</label>
                        <input id="nguon-luc-can-thiet" type="text" data-ng-model="nguonLucCanThiet" class="form-control" readonly />
                    </div>
                </div>
                <div class="col-md-offset-1 col-md-3">
                    <div class="form-group">
                        <label for="nguon-luc-con-lai">Nguồn lực còn lại</label>
                        <input id="nguon-luc-con-lai" type="text" data-ng-model="nguonLucConLai" class="form-control" readonly />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <div class="form-inline">
                        <div class="form-group">
                            <label for="year">Năm</label>
                            <select id="year" data-ng-model="currentYear" data-ng-options="nam as nam for nam in dsNam" data-ng-change="changeYearFunc(currentYear)" class="form-control" style="width: 100px;" autofocus></select>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <h4>1. Danh sách ĐTKT trên 3 năm chưa từng được kiểm toán</h4>
                    <div class="form-inline">
                        <div class="form-group">
                            <label for="loai-dtkt-1">Loại đối tượng kiểm toán</label>
                            <select id="loai-dtkt-1" data-ng-model="loaiDTKT1" data-ng-options="d.Id as d.Ten for d in dsLoaiDTKT" class="form-control" style="width: 200px;"></select>
                            <input type="text" data-ng-model="tenDTKT1" class="form-control" placeholder="Tên ĐTKT" style="width: 250px;" />
                            <button type="button" data-ng-click="search1Func(loaiDTKT1, tenDTKT1)" class="btn color-green btn-xs">Search</button>
                        </div>
                    </div>
                    <br />
                    <div id="grid-1">
                        <nav data-ng-include src="'/HtmlTemplates/paged-list.html'"></nav>
                        <div class="table-responsive">
                            <table class="vpb--table table table-striped table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>Rank</th>
                                        <th>Loại ĐTKT</th>
                                        <th>ĐTKT</th>
                                        <th>Điểm gốc</th>
                                        <th>Điểm sửa đổi</th>
                                        <th>Thời gian kiểm toán gần nhất</th>
                                        <th>Quy mô đtkt</th>
                                        <th>Tháng dự kiến KT</th>
                                        <th>Tần suất</th>
                                        <th>Đợt Kt tiếp 1</th>
                                        <th>Đợt kt tiếp 2</th>
                                        <th>Mục tiêu</th>
                                        <th>Phạm vi</th>
                                        <th>Phòng</th>
                                        <th>Trưởng đoàn</th>
                                        <th>Manager</th>
                                        <th>&nbsp;</th>
                                    </tr>
                                </thead>
                                <tbody data-ng-repeat="item in data.Items">
                                    <tr data-ng-hide="item.EditMode">
                                        <td>{{ item.RankText }}</td>
                                        <td>{{ item.LoaiDTKTText }}</td>
                                        <td>{{ item.DTKTText }}</td>
                                        <td>{{ item.GiaTriGoc }}</td>
                                        <td>{{ item.DiemQuyDoi }}</td>
                                        <td>{{ item.ThoiGianKTGanNhatText }}</td>
                                        <td>{{ item.QuyMo }}</td>
                                        <td>
                                            <span data-ng-show="item.ThangDuKienKT">Tháng {{ item.ThangDuKienKT }}</span>
                                        </td>
                                        <td>{{ item.TanSuatText }}</td>
                                        <td>{{ item.DotKTTiep1 | date : 'MM/yyyy' }}</td>
                                        <td>{{ item.DotKTTiep2 | date : 'MM/yyyy' }}</td>
                                        <td>{{ item.MucTieu }}</td>
                                        <td>{{ item.PhamVi }}</td>
                                        <td>{{ item.PhongText }}</td>
                                        <td>{{ item.TruongDoanText }}</td>
                                        <td>{{ item.ManagerText }}</td>
                                        <td>
                                            <a href="#" title="Sửa" data-ng-click="editFunc(item, index)" data-ng-show="trangThaiNam.TrangThaiKeHoachNam == 0 || trangThaiNam.TrangThaiKeHoachNam == 1"><i class="fa fa-pencil-square-o"></i></a>
                                        </td>
                                    </tr>
                                    <tr data-ng-show="item.EditMode">
                                        <td>{{ item.RankText }}</td>
                                        <td>{{ item.LoaiDTKTText }}</td>
                                        <td>{{ item.DTKTText }}</td>
                                        <td>{{ item.GiaTriGoc }}</td>
                                        <td>
                                            <input type="number" min="0" max="100" data-ng-model="item.DiemQuyDoi" class="form-control" style="width: 70px;" />
                                        </td>
                                        <td>
                                            <div class="input-group">
                                                <input data-ng-model="item.ThoiGianKTGanNhatText" class="form-control vpb-datepicker-js" style="width: 100px;" />
                                                <label class="input-group-addon"><i class="fa fa-calendar"></i></label>
                                            </div>
                                        </td>
                                        <td>
                                            <select data-ng-model="item.QuyMoId" data-ng-options="q.Id as q.Ten for q in dsQuyMo" data-ng-change="changeQuyMoFunc(item)" class="form-control" style="width: 150px;"></select>
                                        </td>
                                        <td>
                                            <select data-ng-model="item.ThangDuKienKT" data-ng-options="month.value as month.name for month in listMonthOfYear" class="form-control" style="width: 150px;"></select>
                                        </td>
                                        <td>
                                            <select data-ng-model="item.TanSuat" data-ng-options="ts.SourceId as ts.Name for ts in dsTanSuat" data-ng-change="changeTanSuatFunc(item)" class="form-control" style="width: 150px;"></select>
                                        </td>
                                        <td>{{ item.DotKTTiep1 | date : 'MM/yyyy' }}</td>
                                        <td>{{ item.DotKTTiep2 | date : 'MM/yyyy' }}</td>
                                        <td>
                                            <input type="text" data-ng-model="item.MucTieu" class="form-control" style="width: 150px;" /></td>
                                        <td>
                                            <input type="text" data-ng-model="item.PhamVi" class="form-control" style="width: 150px;" /></td>
                                        <td>
                                            <select data-ng-model="item.Phong" data-ng-options="p.SourceId as p.Ten for p in rooms" data-ng-change="changePhongFunc(item)" class="form-control" style="width: 150px;"></select>
                                        </td>
                                        <td>
                                            <select data-ng-model="item.TruongDoan" data-ng-options="u.PK_UserID as u.FullName for u in users" data-ng-change="changeLeaderFunc(item)" class="form-control" style="width: 150px;"></select>
                                        </td>
                                        <td>
                                            <select data-ng-model="item.Manager" data-ng-options="u.PK_UserID as u.FullName for u in users" data-ng-change="changeManagerFunc(item)" class="form-control" style="width: 150px;"></select>
                                        </td>
                                        <td>
                                            <a href="#" title="Lưu" data-ng-click="saveFunc(item, $index)"><i class="fa fa-floppy-o"></i></a>
                                            <a href="#" title="Hủy" data-ng-click="cancelEditFunc(item, $index)"><i class="fa fa-times"></i></a>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <nav data-ng-include="'/HtmlTemplates/paged-list.html'"></nav>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <h4>2. Danh sách ĐTKT đã được kiểm toán trong 3 năm và được sắp xếp</h4>
                    <div class="form-inline">
                        <div class="form-group">
                            <label for="loai-dtkt-1">Loại đối tượng kiểm toán</label>
                            <select id="loai-dtkt-1" data-ng-model="loaiDTKT2" data-ng-options="d.Id as d.Ten for d in dsLoaiDTKT" class="form-control" style="width: 200px;"></select>
                            <input type="text" data-ng-model="tenDTKT2" class="form-control" placeholder="Tên ĐTKT" style="width: 250px;" />
                            <button type="button" data-ng-click="search2Func(loaiDTKT2, tenDTKT2)" class="btn color-green btn-xs">Search</button>
                        </div>
                    </div>
                    <br />
                    <div id="grid-2">
                        <nav data-ng-include src="'/HtmlTemplates/paged-list-2.html'"></nav>
                        <div class="table-responsive">
                            <table class="vpb--table table table-striped table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>Rank</th>
                                        <th>Loại ĐTKT</th>
                                        <th>ĐTKT</th>
                                        <th>Điểm gốc</th>
                                        <th>Điểm sửa đổi</th>
                                        <th>Thời gian kiểm toán gần nhất</th>
                                        <th>Quy mô đtkt</th>
                                        <th>Tháng dự kiến KT</th>
                                        <th>Tần suất</th>
                                        <th>Đợt Kt tiếp 1</th>
                                        <th>Đợt kt tiếp 2</th>
                                        <th>Mục tiêu</th>
                                        <th>Phạm vi</th>
                                        <th>Phòng</th>
                                        <th>Trưởng đoàn</th>
                                        <th>Manager</th>
                                        <th>&nbsp;</th>
                                    </tr>
                                </thead>
                                <tbody data-ng-repeat="item in data2.Items">
                                    <tr data-ng-hide="item.EditMode">
                                        <td>{{ item.RankText }}</td>
                                        <td>{{ item.LoaiDTKTText }}</td>
                                        <td>{{ item.DTKTText }}</td>
                                        <td>{{ item.GiaTriGoc }}</td>
                                        <td>{{ item.DiemQuyDoi }}</td>
                                        <td>{{ item.ThoiGianKTGanNhatText }}</td>
                                        <td>{{ item.QuyMo }}</td>
                                        <td>
                                            <span data-ng-show="item.ThangDuKienKT">Tháng {{ item.ThangDuKienKT }}</span>
                                        </td>
                                        <td>{{ item.TanSuatText }}</td>
                                        <td>{{ item.DotKTTiep1 | date : 'MM/yyyy' }}</td>
                                        <td>{{ item.DotKTTiep2 | date : 'MM/yyyy' }}</td>
                                        <td>{{ item.MucTieu }}</td>
                                        <td>{{ item.PhamVi }}</td>
                                        <td>{{ item.PhongText }}</td>
                                        <td>{{ item.TruongDoanText }}</td>
                                        <td>{{ item.ManagerText }}</td>
                                        <td>
                                            <a href="#" title="Sửa" data-ng-click="editFunc(item, index)" data-ng-show="trangThaiNam.TrangThaiKeHoachNam == 0 || trangThaiNam.TrangThaiKeHoachNam == 1"><i class="fa fa-pencil-square-o"></i></a>
                                        </td>
                                    </tr>
                                    <tr data-ng-show="item.EditMode">
                                        <td>{{ item.RankText }}</td>
                                        <td>{{ item.LoaiDTKTText }}</td>
                                        <td>{{ item.DTKTText }}</td>
                                        <td>{{ item.GiaTriGoc }}</td>
                                        <td>
                                            <input type="number" min="0" max="100" data-ng-model="item.DiemQuyDoi" class="form-control" style="width: 70px;" />
                                        </td>
                                        <td>
                                            <div class="input-group">
                                                <input data-ng-model="item.ThoiGianKTGanNhatText" class="form-control vpb-datepicker-js" style="width: 100px;" />
                                                <label class="input-group-addon"><i class="fa fa-calendar"></i></label>
                                            </div>
                                        </td>
                                        <td>
                                            <select data-ng-model="item.QuyMoId" data-ng-options="q.Id as q.Ten for q in dsQuyMo" data-ng-change="changeQuyMoFunc(item)" class="form-control" style="width: 150px;"></select>
                                        </td>
                                        <td>
                                            <select data-ng-model="item.ThangDuKienKT" data-ng-options="month.value as month.name for month in listMonthOfYear" class="form-control" style="width: 150px;"></select>
                                        </td>
                                        <td>
                                            <select data-ng-model="item.TanSuat" data-ng-options="ts.SourceId as ts.Name for ts in dsTanSuat" data-ng-change="changeTanSuatFunc(item)" class="form-control" style="width: 150px;"></select>
                                        </td>
                                        <td>{{ item.DotKTTiep1 | date : 'MM/yyyy' }}</td>
                                        <td>{{ item.DotKTTiep2 | date : 'MM/yyyy' }}</td>
                                        <td>
                                            <input type="text" data-ng-model="item.MucTieu" class="form-control" style="width: 150px;" /></td>
                                        <td>
                                            <input type="text" data-ng-model="item.PhamVi" class="form-control" style="width: 150px;" /></td>
                                        <td>
                                            <select data-ng-model="item.Phong" data-ng-options="p.SourceId as p.Ten for p in rooms" data-ng-change="changePhongFunc(item)" class="form-control" style="width: 150px;"></select>
                                        </td>
                                        <td>
                                            <select data-ng-model="item.TruongDoan" data-ng-options="u.PK_UserID as u.FullName for u in users" data-ng-change="changeLeaderFunc(item)" class="form-control" style="width: 150px;"></select>
                                        </td>
                                        <td>
                                            <select data-ng-model="item.Manager" data-ng-options="u.PK_UserID as u.FullName for u in users" data-ng-change="changeManagerFunc(item)" class="form-control" style="width: 150px;"></select>
                                        </td>
                                        <td>
                                            <a href="#" title="Lưu" data-ng-click="save2Func(item, $index)"><i class="fa fa-floppy-o"></i></a>
                                            <a href="#" title="Hủy" data-ng-click="cancelEdit2Func(item, $index)"><i class="fa fa-times"></i></a>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <nav data-ng-include="'/HtmlTemplates/paged-list-2.html'"></nav>
                    </div>
                </div>
            </div>
            <div class="command-btns">
                <a href="<%= ResolveUrl("~/modules/Processes/Kehoachnam/XemLaiDoiTuong.aspx" + Request.Url.Query) %>" class="btn btn-xs btn-success btn-submit">Xem các ĐTKT đã chọn</a>
                <button type="button" data-ng-click="xuatExcelFunc()" data-ng-disabled="true" class="btn btn-xs btn-success btn-submit">Xuất Excel</button>
                <button type="button" data-ng-click="xinDuyetFunc()" data-ng-disabled="trangThaiNam.TrangThaiKeHoachNam > 0" class="btn btn-xs btn-success btn-submit">Xin duyệt</button>
            </div>
        </div>
    </div>
</asp:Content>
