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
                            <label for="loai-dtkt-2">Loại đối tượng kiểm toán</label>
                            <select id="loai-dtkt-2" data-ng-model="loaiDTKT1" data-ng-options="d.Id as d.Ten for d in dsLoaiDTKT" class="form-control" style="width: 200px;"></select>
                            <input type="text" data-ng-model="tenDTKT1" class="form-control" placeholder="Tên ĐTKT" style="width: 250px;" />
                            <button type="button" data-ng-click="search1Func(loaiDTKT1, tenDTKT1)" class="btn btn-default">Search</button>
                        </div>
                    </div>
                    <br />
                    <div id="grid-1">
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
                                <tbody>
                                    <tr data-ng-repeat="item in data.Items">
                                        <td>
                                            <div>{{ item.RankText }}</div>
                                        </td>
                                        <td>
                                            <div>{{ item.LoaiDTKTText }}</div>
                                        </td>
                                        <td>
                                            <div>{{ item.DTKTText }}</div>
                                        </td>
                                        <td>
                                            <div>{{ item.GiaTriGoc }}</div>
                                        </td>
                                        <td>
                                            <div data-ng-hide="item.EditMode">{{ item.DiemQuyDoi }}</div>
                                            <div data-ng-show="item.EditMode">
                                                <input type="number" min="0" max="100" data-ng-model="item.DiemQuyDoi" class="form-control" style="width: 70px;" />
                                            </div>
                                        </td>
                                        <td>
                                            <div data-ng-hide="item.EditMode">{{ item.ThoiGianKTGanNhatText }}</div>
                                            <div data-ng-show="item.EditMode">
                                                <div class="input-group">
                                                    <input data-ng-model="item.ThoiGianKTGanNhatText" class="form-control vpb-datepicker-js" style="width: 100px;" />
                                                    <label class="input-group-addon"><i class="fa fa-calendar"></i></label>
                                                </div>
                                            </div>
                                        </td>
                                        <td>
                                            <div data-ng-hide="item.EditMode">{{ item.QuyMo }}</div>
                                            <div data-ng-show="item.EditMode">
                                                <select data-ng-model="item.QuyMoId" data-ng-options="q.Id as q.Ten for q in dsQuyMo" data-ng-change="changeQuyMoFunc(item)" class="form-control" style="width: 150px;"></select>
                                            </div>
                                        </td>
                                        <td>
                                            <div data-ng-hide="item.EditMode"><span data-ng-show="item.ThangDuKienKT">Tháng {{ item.ThangDuKienKT }}</span></div>
                                            <div data-ng-show="item.EditMode">
                                                <select data-ng-model="item.ThangDuKienKT" data-ng-options="month.value as month.name for month in listMonthOfYear" class="form-control" style="width: 150px;"></select>
                                            </div>
                                        </td>
                                        <td>
                                            <div data-ng-hide="item.EditMode">{{ item.TanSuatText }}</div>
                                            <div data-ng-show="item.EditMode">
                                                <select data-ng-model="item.TanSuat" data-ng-options="ts.SourceId as ts.Name for ts in dsTanSuat" data-ng-change="changeTanSuatFunc(item)" class="form-control" style="width: 150px;"></select>
                                            </div>
                                        </td>
                                        <td>
                                            <div>{{ item.DotKTTiep1 | date : 'MM/yyyy' }}</div>
                                        </td>
                                        <td>
                                            <div>{{ item.DotKTTiep2 | date : 'MM/yyyy' }}</div>
                                        </td>
                                        <td>
                                            <div data-ng-hide="item.EditMode">{{ item.MucTieu }}</div>
                                            <div data-ng-show="item.EditMode">
                                                <input type="text" data-ng-model="item.MucTieu" class="form-control" style="width: 150px;" />
                                            </div>
                                        </td>
                                        <td>
                                            <div data-ng-hide="item.EditMode">{{ item.PhamVi }}</div>
                                            <div data-ng-show="item.EditMode">
                                                <input type="text" data-ng-model="item.PhamVi" class="form-control" style="width: 150px;" />
                                            </div>
                                        </td>
                                        <td>
                                            <div data-ng-hide="item.EditMode">{{ item.PhongText }}</div>
                                            <div data-ng-show="item.EditMode">
                                                <select data-ng-model="item.Phong" data-ng-options="p.SourceId as p.Ten for p in rooms" data-ng-change="changePhongFunc(item)" class="form-control" style="width: 150px;"></select>
                                            </div>
                                        </td>
                                        <td>
                                            <div data-ng-hide="item.EditMode">{{ item.TruongDoanText }}</div>
                                            <div data-ng-show="item.EditMode">
                                                <select data-ng-model="item.TruongDoan" data-ng-options="u.PK_UserID as u.FullName for u in users" data-ng-change="changeLeaderFunc(item)" class="form-control" style="width: 150px;"></select>
                                            </div>
                                        </td>
                                        <td>
                                            <div data-ng-hide="item.EditMode">{{ item.ManagerText }}</div>
                                            <div data-ng-show="item.EditMode">
                                                <select data-ng-model="item.Manager" data-ng-options="u.PK_UserID as u.FullName for u in users" data-ng-change="changeManagerFunc(item)" class="form-control" style="width: 150px;"></select>
                                            </div>
                                        </td>
                                        <td>
                                            <div data-ng-hide="item.EditMode">
                                                <a href="#" data-ng-click="editFunc(item, index)" data-ng-show="trangThaiNam.TrangThaiKeHoachNam == 0 || trangThaiNam.TrangThaiKeHoachNam == 1" class="click-icon" title="Sửa"><i class="fa fa-edit"></i></a>
                                            </div>
                                            <div data-ng-show="item.EditMode">
                                                <a href="#" data-ng-click="saveFunc(item, $index)" class="click-icon" title="Lưu"><i class="fa fa-save"></i></a>
                                                <a href="#" data-ng-click="cancelEditFunc(item, $index)" class="click-icon" title="Hủy"><i class="fa fa-close"></i></a>
                                            </div>
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
                            <button type="button" data-ng-click="search2Func(loaiDTKT2, tenDTKT2)" class="btn btn-default">Search</button>
                        </div>
                    </div>
                    <br />
                    <div id="grid-2">
                        <div data-ng-show="data2.TotalPages > 0" class="vpb--pagination clearfix">
                            <div class="pull-left">
                                <span>Trang {{ data2.CurrentPage }}/{{ data2.TotalPages }}</span>
                            </div>
                            <div class="pull-right">
                                <pagination total-items="data2.TotalItems" ng-model="data2.CurrentPage" max-size="data2.NumberOfLinks" ng-change="changePage2Func(data2.CurrentPage)" class="pagination-sm" boundary-links="true" rotate="false" num-pages="numPages" previous-text="&lsaquo;" next-text="&rsaquo;" first-text="&laquo;" last-text="&raquo;"></pagination>
                            </div>
                        </div>
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
                                <tbody>
                                    <tr data-ng-repeat="item in data2.Items">
                                        <td>
                                            <div>{{ item.RankText }}</div>
                                        </td>
                                        <td>
                                            <div>{{ item.LoaiDTKTText }}</div>
                                        </td>
                                        <td>
                                            <div>{{ item.DTKTText }}</div>
                                        </td>
                                        <td>
                                            <div>{{ item.GiaTriGoc }}</div>
                                        </td>
                                        <td>
                                            <div data-ng-hide="item.EditMode">{{ item.DiemQuyDoi }}</div>
                                            <div data-ng-show="item.EditMode">
                                                <input type="number" min="0" max="100" data-ng-model="item.DiemQuyDoi" class="form-control" style="width: 70px;" />
                                            </div>
                                        </td>
                                        <td>
                                            <div data-ng-hide="item.EditMode">{{ item.ThoiGianKTGanNhatText }}</div>
                                            <div data-ng-show="item.EditMode">
                                                <div class="input-group">
                                                    <input data-ng-model="item.ThoiGianKTGanNhatText" class="form-control vpb-datepicker-js" style="width: 100px;" />
                                                    <label class="input-group-addon"><i class="fa fa-calendar"></i></label>
                                                </div>
                                            </div>
                                        </td>
                                        <td>
                                            <div data-ng-hide="item.EditMode">{{ item.QuyMo }}</div>
                                            <div data-ng-show="item.EditMode">
                                                <select data-ng-model="item.QuyMoId" data-ng-options="q.Id as q.Ten for q in dsQuyMo" data-ng-change="changeQuyMoFunc(item)" class="form-control" style="width: 150px;"></select>
                                            </div>
                                        </td>
                                        <td>
                                            <div data-ng-hide="item.EditMode"><span data-ng-show="item.ThangDuKienKT">Tháng {{ item.ThangDuKienKT }}</span></div>
                                            <div data-ng-show="item.EditMode">
                                                <select data-ng-model="item.ThangDuKienKT" data-ng-options="month.value as month.name for month in listMonthOfYear" class="form-control" style="width: 150px;"></select>
                                            </div>
                                        </td>
                                        <td>
                                            <div data-ng-hide="item.EditMode">{{ item.TanSuatText }}</div>
                                            <div data-ng-show="item.EditMode">
                                                <select data-ng-model="item.TanSuat" data-ng-options="ts.SourceId as ts.Name for ts in dsTanSuat" data-ng-change="changeTanSuatFunc(item)" class="form-control" style="width: 150px;"></select>
                                            </div>
                                        </td>
                                        <td>
                                            <div>{{ item.DotKTTiep1 | date : 'MM/yyyy' }}</div>
                                        </td>
                                        <td>
                                            <div>{{ item.DotKTTiep2 | date : 'MM/yyyy' }}</div>
                                        </td>
                                        <td>
                                            <div data-ng-hide="item.EditMode">{{ item.MucTieu }}</div>
                                            <div data-ng-show="item.EditMode">
                                                <input type="text" data-ng-model="item.MucTieu" class="form-control" style="width: 150px;" />
                                            </div>
                                        </td>
                                        <td>
                                            <div data-ng-hide="item.EditMode">{{ item.PhamVi }}</div>
                                            <div data-ng-show="item.EditMode">
                                                <input type="text" data-ng-model="item.PhamVi" class="form-control" style="width: 150px;" />
                                            </div>
                                        </td>
                                        <td>
                                            <div data-ng-hide="item.EditMode">{{ item.PhongText }}</div>
                                            <div data-ng-show="item.EditMode">
                                                <select data-ng-model="item.Phong" data-ng-options="p.SourceId as p.Ten for p in rooms" data-ng-change="changePhongFunc(item)" class="form-control" style="width: 150px;"></select>
                                            </div>
                                        </td>
                                        <td>
                                            <div data-ng-hide="item.EditMode">{{ item.TruongDoanText }}</div>
                                            <div data-ng-show="item.EditMode">
                                                <select data-ng-model="item.TruongDoan" data-ng-options="u.PK_UserID as u.FullName for u in users" data-ng-change="changeLeaderFunc(item)" class="form-control" style="width: 150px;"></select>
                                            </div>
                                        </td>
                                        <td>
                                            <div data-ng-hide="item.EditMode">{{ item.ManagerText }}</div>
                                            <div data-ng-show="item.EditMode">
                                                <select data-ng-model="item.Manager" data-ng-options="u.PK_UserID as u.FullName for u in users" data-ng-change="changeManagerFunc(item)" class="form-control" style="width: 150px;"></select>
                                            </div>
                                        </td>
                                        <td>
                                            <div data-ng-hide="item.EditMode">
                                                <a href="#" data-ng-click="editFunc(item, index)" data-ng-show="trangThaiNam.TrangThaiKeHoachNam == 0 || trangThaiNam.TrangThaiKeHoachNam == 1" class="click-icon" title="Sửa"><i class="fa fa-edit"></i></a>
                                            </div>
                                            <div data-ng-show="item.EditMode">
                                                <a href="#" data-ng-click="save2Func(item, $index)" title="Lưu" class="click-icon"><i class="fa fa-save"></i></a>
                                                <a href="#" data-ng-click="cancelEdit2Func(item, $index)" title="Hủy" class="click-icon"><i class="fa fa-close"></i></a>
                                            </div>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div data-ng-show="data2.TotalPages > 0" class="vpb--pagination clearfix">
                            <div class="pull-left">
                                <span>Trang {{ data2.CurrentPage }}/{{ data2.TotalPages }}</span>
                            </div>
                            <div class="pull-right">
                                <pagination total-items="data2.TotalItems" ng-model="data2.CurrentPage" max-size="data2.NumberOfLinks" ng-change="changePage2Func(data2.CurrentPage)" class="pagination-sm" boundary-links="true" rotate="false" num-pages="numPages" previous-text="&lsaquo;" next-text="&rsaquo;" first-text="&laquo;" last-text="&raquo;"></pagination>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="command-btns">
                <a href="<%= ResolveUrl("~/modules/Processes/Kehoachnam/XemLaiDoiTuong.aspx" + Request.Url.Query) %>" class="btn btn-primary">Xem các ĐTKT đã chọn</a>
                <button type="button" data-ng-click="xuatExcelFunc()" data-ng-disabled="true" class="btn btn-primary">Xuất Excel</button>
                <button type="button" data-ng-click="xinDuyetFunc()" data-ng-disabled="trangThaiNam.TrangThaiKeHoachNam > 0" class="btn btn-primary">Xin duyệt</button>
            </div>
        </div>
    </div>
</asp:Content>
