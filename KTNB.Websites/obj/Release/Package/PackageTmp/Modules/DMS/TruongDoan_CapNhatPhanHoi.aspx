<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true"
    CodeBehind="TruongDoan_CapNhatPhanHoi.aspx.cs" Inherits="VPB_KTNB.Modules.DMS.TruongDoan_CapNhatPhanHoi" %>

<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <asp:ScriptManager runat="server" ID="ScriptManager1">
    </asp:ScriptManager>
    <table width="100%">
        <tr>
            <td style="width: 222px">
                Đối tượng kiểm toán
            </td>
            <td>
                <asp:TextBox Enabled="false" ID="ID8_2A4CA2AD_0282_4D57_86AC_D973D281EF54" runat="server"
                    SkinID="TextBoxReadOnly"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">
                Đợt kiểm toán
            </td>
            <td>
                <asp:TextBox Enabled="false" ID="ID8_63A0C4B1_2088_4994_B891_2FF65EB20265" runat="server"
                    SkinID="TextBoxReadOnly"></asp:TextBox>
            </td>
        </tr>
    </table>
    <div id="Div1" runat="server">
        <asp:UpdatePanel runat="server" ID="updatepanel1" UpdateMode="Conditional" OnLoad="updatepanel1_OnLoad">
            <ContentTemplate>
                <asp:ObjectDataSource runat="server" ID="ObjectDataSource1" SelectMethod="getDocumentList"
                    TypeName="DataSource">
                    <SelectParameters>
                        <asp:Parameter Name="DocumentTypeID" Type="String" />
                        <asp:Parameter Name="DocFields" Type="String" />
                        <asp:Parameter Name="PropertyFields" Type="String" />
                        <asp:Parameter Name="Condition" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <table width="100%">
                    <tr class="GridHeader">
                        <td colspan="2">
                            Công việc
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <C1WebGrid:C1WebGrid runat="server" ID="dataCtrl" Width="100%" DataSourceID="ObjectDataSource1"
                                OnItemCreated="dataCtrl_OnItemCreated" OnItemDataBound="dataCtrl_OnItemDataBound"
                                GroupIndent="20" ItemStyle-Height="30px">
                                <Columns>
                                    <C1WebGrid:C1TemplateColumn>
                                        <ItemTemplate>
                                            <asp:Image runat="server" ID="imgCongViec" ImageUrl="~/Images/profile_small.gif"
                                                ToolTip="Chi tiết công việc" Style="cursor: pointer" />
                                            <asp:Image runat="server" ID="ImgEdit" ImageUrl="~/Images/viewdetail.gif" ToolTip="Chi tiết hồ sơ rủi ro"
                                                Style="cursor: pointer" />
                                        </ItemTemplate>
                                        <ItemStyle Width="8%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </C1WebGrid:C1TemplateColumn>
                                    <C1WebGrid:C1TemplateColumn Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="PK_DocumentID" Text='<%# DataBinder.Eval(Container.DataItem,"PK_DocumentID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </C1WebGrid:C1TemplateColumn>
                                    <C1WebGrid:C1BoundColumn HeaderText="Công việc" DataField="Tên công việc">
                                        <ItemStyle Width="20%" HorizontalAlign="Left" VerticalAlign="Top" />
                                    </C1WebGrid:C1BoundColumn>
                                    <C1WebGrid:C1TemplateColumn HeaderText="Bắt đầu">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="NgayBatDau" Text='<%# DataBinder.Eval(Container.DataItem,"Ngày bắt đầu")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                    </C1WebGrid:C1TemplateColumn>
                                    <C1WebGrid:C1TemplateColumn HeaderText="Kết thúc">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="NgayKetThuc" Text='<%# DataBinder.Eval(Container.DataItem,"Ngày kết thúc")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                    </C1WebGrid:C1TemplateColumn>
                                    <C1WebGrid:C1BoundColumn HeaderText="Người thực hiện" DataField="Người thực hiện">
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                    </C1WebGrid:C1BoundColumn>
                                    <C1WebGrid:C1TemplateColumn HeaderText="Trạng thái">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTrangThai" Text='<%# DataBinder.Eval(Container.DataItem,"Status")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </C1WebGrid:C1TemplateColumn>
                                </Columns>
                            </C1WebGrid:C1WebGrid>
                        </td>
                    </tr>
                </table>
                <asp:ObjectDataSource runat="server" ID="ObjectDataSource2" SelectMethod="getListPhatHien"
                    TypeName="DataSource">
                    <SelectParameters>
                        <asp:Parameter Name="TenDotKiemToan" Type="String" />
                        <asp:Parameter Name="DotKiemToan_ID" Type="String" />
                        <asp:Parameter Name="DoanKiemToan_ID" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <table width="100%">
                    <tr class="GridHeader">
                        <td colspan="2">
                            Phát hiện
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <C1WebGrid:C1WebGrid runat="server" ID="dataCtrl1" Width="100%" OnItemCreated="dataCtrl_OnItemCreated"
                                OnItemDataBound="dataCtrl1_ItemDataBound" GroupIndent="20" ItemStyle-Height="30px"
                                DataSourceID="ObjectDataSource2">
                                <Columns>
                                    <C1WebGrid:C1TemplateColumn>
                                        <ItemTemplate>
                                            <asp:Image runat="server" ID="imgEdit" ImageUrl="~/Images/viewdetail.gif" ToolTip="Chi tiết phát hiện"
                                                Style="cursor: pointer" />
                                        </ItemTemplate>
                                        <ItemStyle Width="8%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </C1WebGrid:C1TemplateColumn>
                                    <C1WebGrid:C1TemplateColumn Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="PK_DocumentID" Text='<%# DataBinder.Eval(Container.DataItem,"PK_DocumentID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </C1WebGrid:C1TemplateColumn>
                                    <C1WebGrid:C1TemplateColumn Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="DotKiemToanID" Text='<%# DataBinder.Eval(Container.DataItem,"DotKiemToanID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </C1WebGrid:C1TemplateColumn>
                                    <C1WebGrid:C1TemplateColumn Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="Kieu" Text='<%# DataBinder.Eval(Container.DataItem,"Kieu")%>'></asp:Label>
                                        </ItemTemplate>
                                    </C1WebGrid:C1TemplateColumn>
                                    <C1WebGrid:C1BoundColumn HeaderText="Phát hiện" DataField="PhatHien">
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                    </C1WebGrid:C1BoundColumn>
                                    <C1WebGrid:C1TemplateColumn Visible="true" HeaderText="Loại">
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblLoai" Text='<%# DataBinder.Eval(Container.DataItem,"Kieu")%>'></asp:Label>
                                        </ItemTemplate>
                                    </C1WebGrid:C1TemplateColumn>
                                    <C1WebGrid:C1BoundColumn HeaderText="Mức độ" DataField="MucDo">
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                    </C1WebGrid:C1BoundColumn>
                                    <C1WebGrid:C1BoundColumn HeaderText="Chi tiết" DataField="ChiTiet">
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                    </C1WebGrid:C1BoundColumn>
                                    <C1WebGrid:C1BoundColumn HeaderText="Ảnh hưởng" DataField="AnhHuong">
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                    </C1WebGrid:C1BoundColumn>
                                    <C1WebGrid:C1BoundColumn HeaderText="Ghi chú" DataField="GhiChu">
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                    </C1WebGrid:C1BoundColumn>
                                    <C1WebGrid:C1BoundColumn HeaderText="Khuyến nghị" DataField="KhuyenNghi">
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                    </C1WebGrid:C1BoundColumn>
                                    <C1WebGrid:C1TemplateColumn HeaderText="Trạng thái">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTrangThai" Text='<%# DataBinder.Eval(Container.DataItem,"Status")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </C1WebGrid:C1TemplateColumn>
                                    <C1WebGrid:C1TemplateColumn HeaderText="Phản hồi biên bản">
                                        <ItemTemplate>
                                            <asp:Label Text="Cập nhật" runat="server" CssClass="gachchan" ID="lblTongSoPhanHoi"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </C1WebGrid:C1TemplateColumn>
                                </Columns>
                            </C1WebGrid:C1WebGrid>
                        </td>
                    </tr>
                </table>
                <asp:Button runat="server" ID="btnAction" Text="Xin BLĐ phê duyệt đóng đợt KT" OnClientClick="javascript:UpdateStatus_XinChoBLD(); return false;"
                    Visible="false" />
                <asp:Button runat="server" ID="btnPheDuyet" Text="Phê duyệt đóng đợt KT" OnClientClick="javascript:UpdateStatus_PheDuyet(); return false;"
                    Visible="false" />
                <asp:Button runat="server" ID="btnTuChoi" Text="Từ chối đóng đợt KT" OnClientClick="javascript:UpdateStatus_TuChoi(); return false;"
                    Visible="false" />
                <asp:Button runat="server" ID="btnAction_BBKT" Text="Xin BLĐ phê duyệt xuất biên bản đợt KT"
                    OnClientClick="javascript:UpdateStatus_XinChoBLD_BBKT(); return false;" Visible="false" />
                <asp:Button runat="server" ID="btnPheDuyet_BBKT" Text="Phê duyệt xuất biên bản đợt KT"
                    OnClientClick="javascript:UpdateStatus_PheDuyet_BBKT(); return false;" Visible="false" />
                <asp:Button runat="server" ID="btnTuChoi_BBKT" Text="Từ chối xuất biên bản đợt KT"
                    OnClientClick="javascript:UpdateStatus_TuChoi_BBKT(); return false;" Visible="false" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <style>
        .gachchan
        {
            text-decoration: underline;
            cursor: pointer;
            font-weight: bold;
        }
    </style>
    <script type="text/javascript">
        /*********************************************************/
        $(document).ready(function () {
            //do smt here

        });
        /*********************************************************/
        var doankt = Qry["doankt"]; //getParameterByName("doankt");
        var dotkt = Qry["dotkt"];

        function newform() {
            var doankt = Qry["doankt"]; //getParameterByName("doankt");
            var dotkt = Qry["dotkt"];
            window.location.href = "ChuongTrinhKiemToan_Load.aspx?dotkt=" + dotkt + "&doankt=" + doankt;
        }
        function LoadDocument(DocumentID) {
            var doankt = Qry["doankt"]; //getParameterByName("doankt");
            var dotkt = Qry["dotkt"];
            url = "ChuongTrinhKiemToan_View.aspx?act=loaddoc&doc=" + DocumentID + "&dotkt=" + dotkt + "&doankt=" + doankt;
            window.location.href = url;
        }
        function LapChuongTrinhKiemToan(DocumentID) {
            var url = "../../Controls/Tab/ThanhLapDoanKT_Load.aspx?doc=" + DocumentID + "&act=dotkt" + "&timkiem=1";
            window.location.href = url;
        }

        function LoadDocumentHT(DocumentID) {
            url = "PhatHienHeThong_Load.aspx?act=loaddoc&doc=" + DocumentID + "&dotkt=" + dotkt + "&doankt=" + doankt + "&timkiem=tk";
            window.location.href = url;
        }

        function LoadDocumentVP(DocumentID) {
            url = "PhatHienViPham_Load.aspx?act=loaddoc&doc=" + DocumentID + "&dotkt=" + dotkt + "&doankt=" + doankt + "&timkiem=tk";
            window.location.href = url;
        }

        function LoadDocumentHoSoRR(congviecID, doankt, dotkt) {
            url = "ReViewDotKiemToan_View.aspx?act=loaddoc&doankt=" + doankt + "&dotkt=" + dotkt + "&doc=" + congviecID;
            window.location.href = url;
        }

        function LoadDocumentCongViec(DocumentID) {
            var doankt = Qry["doankt"]; //getParameterByName("doankt");
            var dotkt = Qry["dotkt"];
            url = "CongDuocPhuTrach_ReViewDotKiemToan.aspx?act=loaddoc&doc=" + DocumentID + "&dotkt=" + dotkt + "&doankt=" + doankt;
            window.location.href = url;
        }


        function UpdateStatus_XinChoBLD() {
            //        if(!window.confirm("Bạn có muốn xin BLĐ phê duyệt đóng đợt KT?"))
            //            return false;

            var doankt = Qry["doankt"];
            var dotkt = Qry["dotkt"];

            var url = "ReViewDotKiemToan.aspx";
            var query = "act=checksumuarydotkt";
            query += "&dotkt=" + dotkt + "&doankt=" + doankt;
            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (message) {
                    FinishProcessingForm();
                    if (window.confirm(message + " Bạn có muốn xin BLĐ phê duyệt đóng đợt KT?")) {
                        var doankt = Qry["doankt"];
                        var dotkt = Qry["dotkt"];
                        var url = "ReViewDotKiemToan.aspx";
                        var query = "act=updatestatus";
                        query += "&dotkt=" + dotkt + "&doankt=" + doankt;
                        StartProcessingForm("");
                        $.ajax({
                            type: "POST",
                            url: url,
                            data: query,
                            success: function (message) {
                                FinishProcessingForm();
                                if (message == "1")
                                    alert("Cập nhật trạng thái thành công.");
                                else if (message == "0")
                                    alert("CÓ LỖI XẢY RA.");
                                else
                                    alert(message);
                            }
                        });
                    }
                }
            });
        }

        function UpdateStatus_PheDuyet() {

            var doankt = Qry["doankt"];
            var dotkt = Qry["dotkt"];

            var url = "ReViewDotKiemToan.aspx";
            var query = "act=checksumuarydotkt";
            query += "&dotkt=" + dotkt + "&doankt=" + doankt;
            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (message) {
                    FinishProcessingForm();
                    if (window.confirm(message + " Bạn có phê duyệt đóng đợt KT?")) {
                        var doankt = Qry["doankt"];
                        var dotkt = Qry["dotkt"];
                        var url = "ReViewDotKiemToan.aspx";
                        var query = "act=updatestatus_pheduyet";
                        query += "&dotkt=" + dotkt + "&doankt=" + doankt;
                        StartProcessingForm("");
                        $.ajax({
                            type: "POST",
                            url: url,
                            data: query,
                            success: function (message) {
                                FinishProcessingForm();
                                if (message == "1")
                                    alert("Cập nhật trạng thái thành công.");
                                else if (message == "0")
                                    alert("CÓ LỖI XẢY RA.");
                                else
                                    alert(message);
                            }
                        });
                    }
                }
            });
        }

        function UpdateStatus_TuChoi() {
            if (!window.confirm("Bạn có muốn từ chối đóng đợt KT?"))
                return false;

            var doankt = Qry["doankt"];
            var dotkt = Qry["dotkt"];

            var url = "ReViewDotKiemToan.aspx";
            var query = "act=updatestatus_tuchoi";
            query += "&dotkt=" + dotkt + "&doankt=" + doankt;
            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (message) {
                    FinishProcessingForm();
                    if (message == "1") {
                        alert("Cập nhật trạng thái thành công.");
                    }
                    else if (message == "0") {
                        alert("CÓ LỖI XẢY RA.TRẠNG THÁI CẬP NHẬT PHẢI TUẦN TỰ.");
                    }
                }
            });
        }


        function UpdateStatus_XinChoBLD_BBKT() {
            if (!window.confirm("Bạn có muốn xin BLĐ phê duyệt xuất biên bản KT?"))
                return false;

            var doankt = Qry["doankt"];
            var dotkt = Qry["dotkt"];

            var url = "ReViewDotKiemToan.aspx";
            var query = "act=updatestatus_bbkt";
            query += "&dotkt=" + dotkt + "&doankt=" + doankt;
            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (message) {
                    FinishProcessingForm();
                    if (message == "1") {
                        alert("Cập nhật trạng thái thành công.");
                    }
                    else if (message == "0") {
                        alert("CÓ LỖI XẢY RA.TRẠNG THÁI CẬP NHẬT PHẢI TUẦN TỰ.");
                    }
                    else {
                        alert(message);
                    }
                }
            });
        }

        function UpdateStatus_PheDuyet_BBKT() {
            if (!window.confirm("Bạn có muốn phê duyệt xuất biên bản KT?"))
                return false;

            var doankt = Qry["doankt"];
            var dotkt = Qry["dotkt"];

            var url = "ReViewDotKiemToan.aspx";
            var query = "act=updatestatus_pheduyet_bbkt";
            query += "&dotkt=" + dotkt + "&doankt=" + doankt;
            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (message) {
                    FinishProcessingForm();
                    if (message == "1") {
                        alert("Cập nhật trạng thái thành công.");
                    }
                    else if (message == "0") {
                        alert("Có lỗi xảy ra trong quá trình cập nhật.");
                    }
                }
            });
        }

        function UpdateStatus_TuChoi_BBKT() {
            if (!window.confirm("Bạn có muốn từ chối xuất biên bản KT?"))
                return false;

            var doankt = Qry["doankt"];
            var dotkt = Qry["dotkt"];

            var url = "ReViewDotKiemToan.aspx";
            var query = "act=updatestatus_tuchoi_bbkt";
            query += "&dotkt=" + dotkt + "&doankt=" + doankt;
            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (message) {
                    FinishProcessingForm();
                    if (message == "1") {
                        alert("Cập nhật trạng thái thành công.");
                    }
                    else if (message == "0") {
                        alert("Có lỗi xảy ra trong quá trình cập nhật.");
                    }
                }
            });
        }
        function LoadPagePhanHoiDonVi(phathienid) {
            var cv = 'nguoithuchien';
            //        if (cv == 'nguoithuchien')
            //            url = "PhanHoiDonVi.aspx?act=loaddoc&doc=" + phathienid + "&cv=" + cv + "&congviec_docid=" + congviecID + "&doankt=" + doankt + "&dotkt=" + dotkt;
            //        else
            url = "PhanHoiDonVi.aspx?act=loaddoc&doc=" + phathienid + "&cv=" + cv + "&role=td&dotkt=" + dotkt; // + "&timkiem=1" + "&congviec_docid=" + congviecID + "&doankt=" + doankt + "&dotkt=" + dotkt;
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
