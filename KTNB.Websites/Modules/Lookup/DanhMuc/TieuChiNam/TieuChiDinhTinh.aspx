<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="TieuChiDinhTinh.aspx.cs" Inherits="VPB_KTNB.Modules.Lookup.DanhMuc.TieuChiNam.TieuChiDinhTinh" %>
<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <div class="form-horizontal" style="width:80%">
        <asp:Repeater ID="dataCtrl" runat="server" OnItemDataBound="dataCtrl_ItemDataBound">
            <HeaderTemplate>
                <div class="table-responsive">
                    <table class="vpb--table table table-striped table-bordered table-hover">
                        <thead>
                            <tr>
                                <th>Tên tiêu chí</th>
                                <th>Diễn giải</th>
                                <th>Trạng thái</th>
                                <th>Thao tác</th>
                            </tr>
                        </thead>
                        <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Eval("Tên tiêu chí") %></td>
                    <td><%# Eval("Diễn giải") %></td>
                    <td>
                        <asp:Literal ID="Status" runat="server"></asp:Literal></td>
                    <td class="text-center">
                        <a class="click-icon" href="javascript:;" title="Chi tiết" onclick="LoadDocument('<%# Eval("PK_DocumentID") %>')">
                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                        </a>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody>
                    </table>
                </div>
                <!-- /.table-responsive -->
            </FooterTemplate>
        </asp:Repeater>
        
     </div>

<script type="text/javascript">
    /*********************************************************/
    $(document).ready(function () {
        //do smt here
    });
    /*********************************************************/
    function newform() {
        window.location.href = "TieuChiDinhTinh_Input.aspx?a=8b8656bb-a88d-4cdb-8a37-5dd3965c49dc&curApp=dbb1170f-fb15-4585-9c21-e21c32d4de86&an=";
    }
    function LoadDocument(DocumentID) {
        url = "TieuChiDinhTinh_Input.aspx?a=8b8656bb-a88d-4cdb-8a37-5dd3965c49dc&curApp=dbb1170f-fb15-4585-9c21-e21c32d4de86&an=&act=loaddoc&doc=" + DocumentID;
        window.location.href = url;
    }
</script>
</asp:Content>
