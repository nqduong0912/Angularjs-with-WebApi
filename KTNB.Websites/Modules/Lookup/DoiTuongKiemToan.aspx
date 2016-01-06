<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="DoiTuongKiemToan.aspx.cs" Inherits="VPB_TDHS.Modules.Lookup.DoiTuongKiemToan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <asp:ObjectDataSource runat="server" ID="ObjectDataSource1" SelectMethod="getDocumentList"
        TypeName="VPB_KTNB.Helpers.DataSource">
        <SelectParameters>
            <asp:Parameter Name="DocumentTypeID" Type="String" />
            <asp:Parameter Name="DocFields" Type="String" />
            <asp:Parameter Name="PropertyFields" Type="String" />
            <asp:Parameter Name="Condition" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <div class="form-horizontal" data-ng-controller="doiTuongKiemToanController" data-ng-init="initFunc()">
        <table class="vpb-table-filter">
            <colgroup>
                <col />
                <col style="width: 200px;" />
                <col />
                <col style="width: 100px;" />
            </colgroup>
            <tr>
                <td>Loại đối tượng kiểm toán</td>
                <td>
                    <select data-ng-model="loaiDTKT" data-ng-options="d.PK_DocumentID as d.Ten for d in dsLoaiDTKT" data-ng-change="changeLoaiDTKT(loaiDTKT)" class="form-control"></select>
                </td>
                <td>
                    <input type="text" data-ng-model="tenDTKT" class="form-control" placeholder="Tên ĐTKT" /></td>
                <td>
                    <button type="button" data-ng-click="searchFunc(loaiDTKT, tenDTKT)" class="form-control" style="width: 100px">Tìm kiếm</button>
                </td>

            </tr>
        </table>
        <div>
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
                            <th>Tên đối tượng kiểm toán</th>
                            <th>Diễn giải</th>
                            <th>Loại đối tượng kiểm toán</th>
                            <th>Trạng thái</th>
                            <th>Thao tác</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr data-ng-repeat="item in data.Items">
                            <td>{{ item.Ten }}</td>
                            <td>{{ item.Diengiai }}</td>
                            <td>{{ item.TenLDTKT }}</td>
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
    </div>
    <script type="text/javascript">
        function newform() {
            window.location.href = "DoiTuongKiemToan_Load.aspx?a=8b8656bb-a88d-4cdb-8a37-5dd3965c49dc&curApp=dbb1170f-fb15-4585-9c21-e21c32d4de86&an=";
        }
        function LoadDocument(DocumentID) {
            var url = "DoiTuongKiemToan_Load.aspx?a=8b8656bb-a88d-4cdb-8a37-5dd3965c49dc&curApp=dbb1170f-fb15-4585-9c21-e21c32d4de86&an=&act=loaddoc&doc=" + DocumentID;
            window.location.href = url;
        }
    </script>
</asp:Content>
