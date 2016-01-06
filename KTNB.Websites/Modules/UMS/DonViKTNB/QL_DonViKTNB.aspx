<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="QL_DonViKTNB.aspx.cs" Inherits="VPB_KTNB.Modules.UMS.DonViKTNB.QL_DonViKTNB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <div data-ng-controller="donViKiemToanNoiBoController" data-ng-init="initFunc()" class="vpb-ctrl">
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
                <thead class="color-green">
                    <tr>
                        <th>Mã đơn vị</th>
                        <th>Tên đơn vị</th>
                        <th>Trạng thái</th>
                        <th>Trưởng phòng</th>
                        <th>Nguồn lực năm</th>
                        <th>Hành động</th>
                    </tr>
                </thead>
                <tbody>
                    <tr data-ng-repeat="item in data.Items">
                        <td>{{item.MaDonVi}}
                        </td>
                        <td>{{item.Ten}}
                        </td>
                        <td>{{item.TrangThaiText}}
                        </td>
                        <td>{{item.TruongPhong}}
                        </td>
                        <td>{{item.NguonLuc}}
                        </td>
                        <td>
                            <a title="Sửa" data-ng-click="editFunc(item.Id)"><i class="glyphicon glyphicon-search"></i></a>
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
        <button type="button" class="btn btn-primary" data-ng-click="newFormFunc()">Thêm mới</button>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ButtonExtendBefore" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ButtonExtend" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FormFooter" runat="server">
</asp:Content>
