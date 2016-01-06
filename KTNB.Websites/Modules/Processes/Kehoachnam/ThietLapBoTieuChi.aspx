<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="ThietLapBoTieuChi.aspx.cs" Inherits="VPB_KTNB.Modules.Processes.KeHoachNam.ThietLapBoTieuChi" %>

<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
     <div class="form-horizontal">
        <div class="form-group">
            <label for="inputName" class="col-sm-1 control-label">Năm</label>
            <div class="col-sm-3">
                <asp:DropDownList ID="drpYears" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpYear_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
            <label for="inputName" class="col-sm-3 control-label">Loại đối tượng kiểm toán</label>
            <div class="col-sm-3">
                <asp:DropDownList ID="drpLoaiDTKT" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpYear_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
           <div class="col-sm-2">
               <a href="javascript:;" onclick="newform();" class="btn btn-default" style="float: right">Thêm bộ tiêu chí</a>               
           </div>
        </div>             
    </div>  
    <br />
     <asp:Repeater ID="rptBoTieuChi" runat="server" OnItemDataBound="rptBoTieuChi_ItemDataBound" OnItemCommand="rptBoTieuChi_ItemCommand">
        <HeaderTemplate>
            <div class="table-responsive">
            <table id="tblBotieuchi" class="table table-striped table-bordered table-hover">
                <thead class="color-green">
                    <tr>
                        <th>Năm</th>
                        <th>Loại đối tượng kiểm toán</th>
                        <th>Tên bộ tiêu chí</th>
                        <th>Trạng thái</th>
                        <th>On/Off</th>
                        <th>Ngày cập nhật</th>
                        <th>Hành động</th>
                    </tr>
                </thead>
                <tbody>
        </HeaderTemplate>
        <FooterTemplate>
                </tbody>
                </table>
            </div>
            <!-- /.table-responsive -->
        </FooterTemplate>
        <ItemTemplate>
            <tr>
                <td><%# Eval("Nam") %></td>
                <td class="_tenloaiDTKT"><%# Eval("LDTKT") %></td>
                <td><%# Eval("TenBTC") %></td>
                <td><asp:Literal ID="lblStatus" runat="server"></asp:Literal></td>
                <td>
                    <asp:LinkButton ID="btnActiveBTC" Visible="false" ToolTip="Sử dụng bộ tiêu chí" CommandName="onbtc" OnClientClick='<%# string.Format("javascript:return ActiveBTC(\"{0}\", \"{1}\")", Eval("BTC"), Eval("TenBTC"))%>' CommandArgument='<%# Eval("BTC") %>' runat="server" CssClass="click-icon">
                        <span class="glyphicon glyphicon-log-in" aria-hidden="true"></span>
                    </asp:LinkButton>
                    <asp:Label ID="lblIsActive" runat="server" Visible="false" class="glyphicon glyphicon-ok" aria-hidden="true"></asp:Label>
                </td>
                <td><asp:Literal ID="lblUpdatedate" runat="server"></asp:Literal></td>
                <td class="text-left">
                    <asp:LinkButton ID="btnMark" Visible="false" ToolTip="Chấm điểm draft bộ tiêu chí" CommandName="onbtc" OnClientClick='<%# string.Format("javascript:return ActiveBTC(\"{0}\", \"{1}\")", Eval("BTC"), Eval("TenBTC"))%>' CommandArgument='<%# Eval("BTC") %>' runat="server" CssClass="click-icon">
                        <span class="glyphicon glyphicon-list" aria-hidden="true"></span>
                    </asp:LinkButton>
                    <asp:LinkButton ID="btnCopy" Visible="false" ToolTip="Copy bộ tiêu chí" CommandName="onbtc" OnClientClick='<%# string.Format("javascript:return ActiveBTC(\"{0}\", \"{1}\")", Eval("BTC"), Eval("TenBTC"))%>' CommandArgument='<%# Eval("BTC") %>' runat="server" CssClass="click-icon">
                        <span class="glyphicon glyphicon-save" aria-hidden="true"></span>
                    </asp:LinkButton>
                    <asp:LinkButton ID="btnSend" Visible="false" ToolTip="Trình duyệt" CommandName="onbtc" OnClientClick='<%# string.Format("javascript:return ActiveBTC(\"{0}\", \"{1}\")", Eval("BTC"), Eval("TenBTC"))%>' CommandArgument='<%# Eval("BTC") %>' runat="server" CssClass="click-icon">
                        <span class="glyphicon glyphicon-send" aria-hidden="true"></span>
                    </asp:LinkButton>
                    <a class="click-icon" href="javascript:;" title="Chi tiết" onclick="LoadDocument('<%# Eval("BTC") %>')">
                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                    </a>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
    <script type="text/javascript">
        /*********************************************************/
        $(document).ready(function () {
            //do smt here
            $.each($("._tenloaiDTKT"), function (key, value) {
                var guid_ldtkt = $(this).text();
                $(this).text($("#<%= drpLoaiDTKT.ClientID %> option[value='" + guid_ldtkt + "']").text());
            });
            
        });
        /*********************************************************/
        function ActiveBTC(btc, ten)
        {
            if (!window.confirm("Bạn có chắc chắn sử dụng bộ tiêu chí '" + ten + "' cho năm " + $("#<%= drpYears.ClientID %>").val()))
                return false;
        }
        function newform() {
            var strOptions = $("#<%= drpYears.ClientID %>").val();
            var ldtkt = encodeURI($("#<%= drpLoaiDTKT.ClientID %> option:selected").val());
            window.location.href = "/modules/lookup/DanhMuc/TieuChiNam/BoTieuChiNam_Input.aspx?a=696c6aa4-1b7a-4f38-a155-801f0ca720fa&curApp=dbb1170f-fb15-4585-9c21-e21c32d4de86&y=" + strOptions + "&l=" + ldtkt;

        }
        function LoadDocument(DocumentID) {
            var strOptions = $("#<%= drpYears.ClientID %>").val();
            var ldtkt = encodeURI($("#<%= drpLoaiDTKT.ClientID %> option:selected").val());
            url = "/modules/lookup/DanhMuc/TieuChiNam/BoTieuChiNam_Input.aspx?a=696c6aa4-1b7a-4f38-a155-801f0ca720fa&curApp=dbb1170f-fb15-4585-9c21-e21c32d4de86&act=loaddoc&&doc=" + DocumentID + "&y=" + strOptions + "&l=" + ldtkt;
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
