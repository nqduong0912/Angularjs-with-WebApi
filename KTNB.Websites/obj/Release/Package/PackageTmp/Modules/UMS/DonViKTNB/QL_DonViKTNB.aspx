<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="QL_DonViKTNB.aspx.cs" Inherits="VPB_KTNB.Modules.UMS.DonViKTNB.QL_DonViKTNB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <div data-ng-controller="donViKiemToanNoiBoController" data-ng-init="initFunc()">
        <nav data-ng-include src="'/HtmlTemplates/paged-list.html'"></nav>
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
                        <td>{{item.TrangThai}}
                        </td>
                        <td>
                            {{item.TruongPhong}}
                        </td>
                        <td>
                            {{item.NguonLuc}}
                        </td>
                        <td>
                            <a title="Sửa" data-ng-click="editFunc(item.Id)"><i class="glyphicon glyphicon-search"></i></a>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <nav data-ng-include src="'/HtmlTemplates/paged-list.html'"></nav>
        <button type="button" class="btn btn-xs btn-success btn-submit" data-ng-click="newFormFunc()">Thêm mới</button>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ButtonExtendBefore" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ButtonExtend" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FormFooter" runat="server">
</asp:Content>
