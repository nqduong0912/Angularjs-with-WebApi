<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="DotKiemToan.aspx.cs" Inherits="VPB_KTNB.Modules.Processes.ThucHienKiemToan.KhoiTaoJob.DotKiemToan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <div class="form-horizontal" data-ng-controller="dotKiemToanController" data-ng-init="initFunc()">
        <table class="vpb-table-filter">
            <colgroup>
                <col />
                <col style="width: 200px;" />
            </colgroup>
            <tr>
                <td>Trạng thái</td>
                <td>
                    <select data-ng-model="trangThai" data-ng-options="d.value as d.name for d in dsTrangThai" data-ng-change="searchFunc(trangThai)" class="form-control"></select>
                </td>
            </tr>
        </table>
        <div>
            <nav data-ng-include="'/HtmlTemplates/paged-list.html'"></nav>
            <div class="table-responsive">
                <table class="vpb--table table table-striped table-bordered table-hover">
                    <thead>
                        <tr>
                            <th>Tên đợt KT</th>
                            <th>ĐTKT</th>
                            <th>Trong kế hoạch</th>
                            <th>Đơn vị liên quan</th>
                            <th>Tháng dự kiến</th>
                            <th>Trạng thái ĐKT</th>
                            <th>Lập kế hoạch</th>
                            <th>Thực địa</th>
                            <th>Báo cáo</th>
                            <th>Thao tác</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr data-ng-repeat="item in data.Items">
                            <td>{{ item.DotKT }}</td>
                            <td>{{ item.DTKTText }}</td>
                            <td>
                                <span data-ng-show="item.TrongKeHoach">Trong kế hoạch</span>
                                <span data-ng-show="!item.TrongKeHoach">Ngoài kế hoạch</span>
                            </td>
                            <td>{{ item.DonViLienQuan }}</td>
                            <td>{{ item.ThangDuKienKT }}</td>
                            <td>
                                <span data-ng-show="item.TrangThai == 0">Chưa thực hiện</span>
                                <span data-ng-show="item.TrangThai == 1">Đã duyệt</span>
                            </td>
                            <td>{{ item.LapKHStart }}</td>
                            <td>{{ item.ThucDiaStart }}</td>
                            <td>{{ item.BaoCaoStart }}</td>
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
            <%--<a href="#" class="btn btn-xs btn-success btn-block btn-submit" data-ng-click="addNew()">Thêm mới</a>--%>
        </div>
    </div>
    <script type="text/javascript">
        /*********************************************************/
        $(document).ready(function () {
            //do smt here
        });
        /*********************************************************/
        function newform() {
            window.location.href = "DotKiemToan_Load.aspx?a=8b8656bb-a88d-4cdb-8a37-5dd3965c49dc&curApp=dbb1170f-fb15-4585-9c21-e21c32d4de86&an=";
        }
        function LoadDocument(DocumentID) {
            url = "DotKiemToan_Load.aspx?a=8b8656bb-a88d-4cdb-8a37-5dd3965c49dc&curApp=dbb1170f-fb15-4585-9c21-e21c32d4de86&an=&act=loaddoc&doc=" + DocumentID;
            window.location.href = url;
        }
    </script>
</asp:Content>