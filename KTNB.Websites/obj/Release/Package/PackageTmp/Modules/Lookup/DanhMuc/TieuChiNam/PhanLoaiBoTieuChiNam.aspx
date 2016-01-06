﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="PhanLoaiBoTieuChiNam.aspx.cs" Inherits="VPB_KTNB.Modules.Lookup.DanhMuc.TieuChiNam.PhanLoaiBoTieuChiNam" %>
<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <asp:ObjectDataSource runat="server" ID="ObjectDataSource1" SelectMethod="getDocumentList"
    TypeName="DataSource">
    <SelectParameters>
        <asp:Parameter Name="DocumentTypeID" Type="String" />
        <asp:Parameter Name="DocFields" Type="String" />
        <asp:Parameter Name="PropertyFields" Type="String" />
        <asp:Parameter Name="Condition" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>
    <div class="form-horizontal">
        <div class="form-group">
            <label class="col-sm-1 col-sm-offset-1 control-label">Năm</label>
            <div class="col-sm-2">
                <asp:DropDownList ID="drpYear" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpYear_SelectedIndexChanged">
                    <asp:ListItem Text="2015" Value="2015" Enabled="true"></asp:ListItem>
                    <asp:ListItem Text="2014" Value="2014"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <label class="col-sm-3 col-sm-offset-1 control-label">Loại đối tượng kiểm toán</label>
            <div class="col-sm-4">
                <asp:DropDownList ID="drpLoaiDTKT" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpLoaiDTKT_SelectedIndexChanged">
                    <asp:ListItem Text="2015" Value="2015" Enabled="true"></asp:ListItem>
                    <asp:ListItem Text="2014" Value="2014"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
    </div>
    <div class="body-table">
                        <div class="table-responsive">
                            <table class="table table-striped table-bordered table-hover">
                                <thead class="color-green">
                                    <tr>
                                        <th>STT</th>
                                        <th>Tên bộ tiêu chí</th>
                                        <th>Trạng thái</th>
                                        <th>On/Off</th>
                                        <th>Ngày cập nhật</th>
                                        <th>Hành động</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>1</td>
                                        <td>110000</td>
                                        <td>Phòng ABC</td>
                                        <td>Người đẹp</td>
                                        <td>XXXXXXXXXXXXXXXXXXX</td>
                                       <td class="text-center click-icon"><a><span class="add-file"></span></a></td>
                                    </tr>
                                    <tr>
                                        <td>2</td>
                                        <td>110000</td>
                                        <td>Phòng XXX</td>
                                        <td>Người đẹp</td>
                                        <td>XXXXXXXXXXXXXXXXXXX</td>
                                        <td class="text-center click-icon"><a><span class="cancel-file"></span></a></td>
                                    </tr>
                                    <tr>
                                        <td>3</td>
                                        <td>110000</td>
                                        <td>Phòng ABC</td>
                                        <td>Người đẹp</td>
                                        <td>XXXXXXXXXXXXXXXXXXX</td>
                                        <td class="text-center click-icon"><a><span class="edit-file"></span></a></td>
                                    </tr>
                                    <tr>
                                        <td>4</td>
                                        <td>110000</td>
                                        <td>Phòng XXX</td>
                                        <td>Người đẹp</td>
                                        <td>XXXXXXXXXXXXXXXXXXX</td>
                                        <td class="text-center click-icon"><a><span class="suss-file"></span></a></td>
                                    </tr>
                                    <tr>
                                        <td>5</td>
                                        <td>110000</td>
                                        <td>Phòng ABC</td>
                                        <td>Người đẹp</td>
                                        <td>XXXXXXXXXXXXXXXXXXX</td>
                                        <td class="text-center click-icon"><a><span class="delete-file"></span></a></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <!-- /.table-responsive -->
                    </div>
<table width="70%">
    <tr>
        <td>
            <C1WebGrid:C1WebGrid runat="server" ID="dataCtrl" Width="100%" DataSourceID="ObjectDataSource1"
                    OnItemCreated="dataCtrl_OnItemCreated" OnItemDataBound="dataCtrl_OnItemDataBound"
                    GroupIndent="20" ItemStyle-Height="30px">
                    <Columns>
                        <C1WebGrid:C1TemplateColumn>
                            <ItemTemplate>
                                <asp:Image runat="server" ID="imgEdit" ImageUrl="~/Images/viewdetail.gif" ToolTip="Chi tiết"
                                    Style="cursor: pointer" />
                            </ItemTemplate>
                            <ItemStyle Width="8%" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1TemplateColumn Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="PK_DocumentID" Text='<%# DataBinder.Eval(Container.DataItem,"PK_DocumentID")%>'></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Tên phân loại bộ tiêu chí năm" DataField="Tên phân loại bộ tiêu chí năm">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Diễn giải" DataField="Diễn Giải">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1TemplateColumn Visible="true" HeaderText="Trạng thái">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="Status" Text='<%# DataBinder.Eval(Container.DataItem,"Status")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="8%" HorizontalAlign="Right" VerticalAlign="Middle" />
                        </C1WebGrid:C1TemplateColumn>
                    </Columns>
            </C1WebGrid:C1WebGrid>
        </td>
    </tr>
</table>

<script type="text/javascript">
    /*********************************************************/
    $(document).ready(function () {
        //do smt here
    });
    /*********************************************************/
    function newform() {
        window.location.href = "ThemMoiLoaiBoTieuChiNam.aspx";
    }
    function LoadDocument(DocumentID) {
        url = "ThemMoiLoaiBoTieuChiNam.aspx?act=loaddoc&doc=" + DocumentID;
        window.location.href = url;
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ButtonExtendBefore" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ButtonExtend" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FormFooter" runat="server">
</asp:Content>
