<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="DuyetKeHoachNam.aspx.cs" Inherits="VPB_KTNB.Modules.Processes.KeHoachNam.DuyetKeHoachNam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <div data-ng-controller="duyetKeHoachNamController" data-ng-init="initFunc('<%= vpb.app.business.ktnb.Definition.UMS.GROUPS.KTNB1 %>', '<%= vpb.app.business.ktnb.Definition.UMS.GROUPS.BKS %>')">
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
                            <a href="#" title="Sửa" data-ng-click="editFunc(item, index)" data-ng-show="roles.isBanGDKhoi"><i class="fa fa-pencil-square-o"></i></a>
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

        <br />
        <div class="vpb-btns">
            <button type="button" data-ng-click="duyetFunc()" data-ng-disabled="!(data.Items.length > 0) || !((roles.isBanGDKhoi && trangThaiNam.TrangThaiKeHoachNam == 1) || (roles.isBanKS && trangThaiNam.TrangThaiKeHoachNam == 2))" class="btn btn-xs btn-success btn-submit">Duyệt</button>
            <button type="button" data-ng-click="tuChoiFunc()" data-ng-disabled="!(data.Items.length > 0) || !((roles.isBanGDKhoi && trangThaiNam.TrangThaiKeHoachNam == 1) || (roles.isBanKS && trangThaiNam.TrangThaiKeHoachNam == 2))" class="btn btn-xs btn-success btn-submit">Từ chối</button>
            <button type="button" data-ng-click="xuatExcelFunc()" data-ng-disabled="true" class="btn btn-xs btn-success btn-submit">Xuất Excel</button>
        </div>
    </div>
</asp:Content>
