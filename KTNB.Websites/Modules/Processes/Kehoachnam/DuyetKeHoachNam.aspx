<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="DuyetKeHoachNam.aspx.cs" Inherits="VPB_KTNB.Modules.Processes.KeHoachNam.DuyetKeHoachNam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <div data-ng-controller="duyetKeHoachNamController" data-ng-init="initFunc('<%= vpb.app.business.ktnb.Definition.UMS.GROUPS.KTNB1 %>', '<%= vpb.app.business.ktnb.Definition.UMS.GROUPS.BKS %>')" class="vpb-ctrl">
        <div class="list-filter">
            <table class="vpb-table-filter">
                <colgroup>
                    <col />
                    <col style="width: 120px;" />
                    <col />
                    <col style="width: 120px;" />
                    <col />
                    <col style="width: 140px;" />
                </colgroup>
                <tr>
                    <td>Năm</td>
                    <td>
                        <select data-ng-model="currentYear" data-ng-options="nam as nam for nam in dsNam" data-ng-change="changeYear(currentYear)" class="form-control"></select>
                    </td>
                    <td>Loại đối tượng kiểm toán</td>
                    <td>
                        <select data-ng-model="currentLoaiDTKT" data-ng-options="d.Id as d.Ten for d in dsLoaiDTKT" data-ng-change="changeLoaiDTKT(currentLoaiDTKT)" class="form-control"></select>
                    </td>
                    <td>Trạng thái</td>
                    <td>
                        <input type="text" data-ng-model="trangThaiNam.TrangThaiKeHoachNamText" class="form-control" readonly />
                    </td>
                </tr>
                <tr>
                    <td>Nguồn lực năm</td>
                    <td>
                        <input id="nguon-luc-nam" type="text" data-ng-model="nguonLucNam" class="form-control" readonly />
                    </td>
                    <td>Nguồn lực cần thiết</td>
                    <td>
                        <input id="nguon-luc-can-thiet" type="text" data-ng-model="nguonLucCanThiet" class="form-control" readonly />
                    </td>
                    <td>Nguồn lực còn lại</td>
                    <td>
                        <input id="nguon-luc-con-lai" type="text" data-ng-model="nguonLucConLai" class="form-control" readonly />
                    </td>
                </tr>
            </table>
            <br />
        </div>
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
                <tbody data-ng-repeat="item in data.Items">
                    <tr>
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
                                <a href="#" data-ng-click="editFunc(item, index)" data-ng-show="roles.isBanGDKhoi" class="click-icon" title="Sửa"><i class="fa fa-edit"></i></a>
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

        <br />
        <div class="vpb-btns">
            <button type="button" data-ng-click="duyetFunc()" data-ng-disabled="!(data.Items.length > 0) || !((roles.isBanGDKhoi && trangThaiNam.TrangThaiKeHoachNam == 1) || (roles.isBanKS && trangThaiNam.TrangThaiKeHoachNam == 2))" class="btn btn-primary">Duyệt</button>
            <button type="button" data-ng-click="tuChoiFunc()" data-ng-disabled="!(data.Items.length > 0) || !((roles.isBanGDKhoi && trangThaiNam.TrangThaiKeHoachNam == 1) || (roles.isBanKS && trangThaiNam.TrangThaiKeHoachNam == 2))" class="btn btn-primary">Từ chối</button>
            <button type="button" data-ng-click="xuatExcelFunc()" data-ng-disabled="true" class="btn btn-primary">Xuất Excel</button>
        </div>
    </div>
</asp:Content>
