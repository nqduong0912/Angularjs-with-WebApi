<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="ReViewDotKiemToan.aspx.cs" Inherits="VPB_TDHS.Modules.DMS.ReViewDotKiemToan" %>

<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <asp:ScriptManager runat="server" ID="ScriptManager1"></asp:ScriptManager>
    <div class="form-horizontal">
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Đối tượng kiểm toán</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_2A4CA2AD_0282_4D57_86AC_D973D281EF54" runat="server" Enabled="False" Width="300px" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Đợt kiểm toán</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_63A0C4B1_2088_4994_B891_2FF65EB20265" runat="server" Enabled="False" Width="300px" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
    </div>

    <%--<table width="100%">
        <tr>
            <td style="width: 222px">Đối tượng kiểm toán</td>
            <td>
                <asp:TextBox Enabled="false" ID="ID8_2A4CA2AD_0282_4D57_86AC_D973D281EF54" runat="server"
                    SkinID="TextBoxReadOnly"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Đợt kiểm toán</td>
            <td>
                <asp:TextBox Enabled="false" ID="ID8_63A0C4B1_2088_4994_B891_2FF65EB20265" runat="server"
                    SkinID="TextBoxReadOnly"></asp:TextBox>
            </td>
        </tr>

    </table>--%>
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
                        <td colspan="2">Công việc</td>
                    </tr>
                    <tr>
                        <td>
                            <C1WebGrid:C1WebGrid runat="server" ID="dataCtrl" Width="100%" DataSourceID="ObjectDataSource1"
                                OnItemCreated="dataCtrl_OnItemCreated" OnItemDataBound="dataCtrl_OnItemDataBound"
                                GroupIndent="20" ItemStyle-Height="30px">
                                <Columns>
                                    <C1WebGrid:C1TemplateColumn>
                                        <ItemTemplate>
                                            <asp:Image runat="server" ID="imgCongViec" ImageUrl="~/Images/profile_small.gif" ToolTip="Chi tiết công việc"
                                                Style="cursor: pointer" />
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
                        <td colspan="2">Phát hiện</td>
                    </tr>

                    <tr>
                        <td>
                            <C1WebGrid:C1WebGrid runat="server" ID="dataCtrl1" Width="100%"
                                OnItemCreated="dataCtrl_OnItemCreated" OnItemDataBound="dataCtrl1_ItemDataBound"
                                GroupIndent="20" ItemStyle-Height="30px" DataSourceID="ObjectDataSource2">
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
                                </Columns>
                            </C1WebGrid:C1WebGrid>
                        </td>
                    </tr>


                </table>
                <div class="form-horizontal" id="tbGiaHan" visible="false">
                    <div class="form-group">
                        <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label"></label>
                        <div class="col-sm-8">
                            <asp:CheckBox runat="server" ID="chkXinGiaHanThoiGian" Text="Xin gia hạn thời gian kết thúc đợt kiểm toán" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Thời gian gia hạn</label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txtThoiGian" runat="server" Enabled="False" Width="300px" CssClass="form-control"></asp:TextBox>
                            <asp:Label runat="server" ID="lbl1" ForeColor="Red" Text="*" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Lý do gia hạn</label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txtLyDo" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <%--<table runat="server" visible="false" id="tbGiaHan" width="80%">
                    <tr>
                        <td colspan="2">
                            <asp:CheckBox runat="server" ID="chkXinGiaHanThoiGian" Text="Xin gia hạn thời gian kết thúc đợt kiểm toán" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 200px">Thời gian gia hạn
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtThoiGian" Width="247px" /><asp:Label runat="server" ID="lbl1" ForeColor="Red" Text="*" />
                        </td>
                    </tr>
                    <tr>
                        <td>Lý do gia hạn
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtLyDo" TextMode="MultiLine" Width="250px" />
                        </td>
                    </tr>
                </table>--%>

                <asp:Button runat="server" ID="btnAction" Text="Xin BLĐ phê duyệt đóng đợt KT" OnClientClick="javascript:UpdateStatus_XinChoBLD(); return false;" Visible="false" />
                <asp:Button runat="server" ID="btnPheDuyet" Text="Phê duyệt đóng đợt KT" OnClientClick="javascript:UpdateStatus_PheDuyet(); return false;" Visible="false" />
                <asp:Button runat="server" ID="btnTuChoi" Text="Từ chối đóng đợt KT" OnClientClick="javascript:UpdateStatus_TuChoi(); return false;" Visible="false" />

                <asp:Button runat="server" ID="btnAction_BBKT" Text="Xin BLĐ phê duyệt xuất biên bản đợt KT" OnClientClick="javascript:UpdateStatus_XinChoBLD_BBKT(); return false;" Visible="false" />
                <asp:Button runat="server" ID="btnPheDuyet_BBKT" Text="Phê duyệt xuất biên bản đợt KT" OnClientClick="javascript:UpdateStatus_PheDuyet_BBKT(); return false;" Visible="false" />
                <asp:Button runat="server" ID="btnTuChoi_BBKT" Text="Từ chối xuất biên bản đợt KT" OnClientClick="javascript:UpdateStatus_TuChoi_BBKT(); return false;" Visible="false" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <script type="text/javascript">
        /*********************************************************/
        $(document).ready(function () {
            //do smt here
        });
        /*********************************************************/
        var doankt = Qry["doankt"]; //getParameterByName("doankt");
        var dotkt = Qry["dotkt"];
        var active = Qry["act"];
        $(document).on("pageload", function () {
            if (active == "td_bbkt")
                $('#tbGiaHan').show();
        });

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


        //    function UpdateStatus_XinChoBLD() {
        //        var doankt = Qry["doankt"];
        //        var dotkt = Qry["dotkt"];

        //        var url = "ReViewDotKiemToan.aspx";
        //        var query = "act=checksumuarydotkt";
        //        query += "&dotkt=" + dotkt + "&doankt=" + doankt;
        //        StartProcessingForm("");
        //        $.ajax({
        //            type: "POST",
        //            url: url,
        //            data: query,
        //            success: function (message) {
        //                FinishProcessingForm();
        //                if (window.confirm(message+ " Bạn có muốn xin BLĐ phê duyệt đóng đợt KT?")) {
        //                    var doankt = Qry["doankt"];
        //                    var dotkt = Qry["dotkt"];
        //                    var url = "ReViewDotKiemToan.aspx";
        //                    var query = "act=updatestatus";
        //                    query += "&dotkt=" + dotkt + "&doankt=" + doankt;
        //                    StartProcessingForm("");
        //                    $.ajax({
        //                        type: "POST",
        //                        url: url,
        //                        data: query,
        //                        success: function (message) {
        //                            FinishProcessingForm();
        //                            if (message == "1") 
        //                                alert("Cập nhật trạng thái thành công.");
        //                            else if (message == "0") 
        //                                alert("CÓ LỖI XẢY RA.");
        //                            else 
        //                                alert(message);
        //                        }
        //                    });
        //                }
        //            }
        //        });
        //    }


        function UpdateStatus_XinChoBLD() {
            if (!window.confirm("Bạn có muốn xin BLĐ phê duyệt đóng đợt KT?"))
                return false;
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

        //    function UpdateStatus_PheDuyet() {

        //        var doankt = Qry["doankt"];
        //        var dotkt = Qry["dotkt"];

        //        var url = "ReViewDotKiemToan.aspx";
        //        var query = "act=checksumuarydotkt";
        //        query += "&dotkt=" + dotkt + "&doankt=" + doankt;
        //        StartProcessingForm("");
        //        $.ajax({
        //            type: "POST",
        //            url: url,
        //            data: query,
        //            success: function (message) {
        //                FinishProcessingForm();
        //                if (window.confirm(message+" Bạn có phê duyệt đóng đợt KT?")) {
        //                    var doankt = Qry["doankt"];
        //                    var dotkt = Qry["dotkt"];
        //                    var url = "ReViewDotKiemToan.aspx";
        //                    var query = "act=updatestatus_pheduyet";
        //                    query += "&dotkt=" + dotkt + "&doankt=" + doankt;
        //                    StartProcessingForm("");
        //                    $.ajax({
        //                        type: "POST",
        //                        url: url,
        //                        data: query,
        //                        success: function (message) {
        //                            FinishProcessingForm();
        //                            if (message == "1")
        //                                alert("Cập nhật trạng thái thành công.");
        //                            else if (message == "0")
        //                                alert("CÓ LỖI XẢY RA.");
        //                            else
        //                                alert(message);
        //                        }
        //                    });
        //                }
        //            }
        //        });
        //    }

        function UpdateStatus_PheDuyet() {
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

            //var chkXinDuyet = $("#ctl00_FormContent_chkXinPheDuyetKetThuc");
            var chkGiaHan = $("#ctl00_FormContent_chkXinGiaHanThoiGian");
            var txtThoiGian = $("#ctl00_FormContent_txtThoiGian");
            var txtLyDo = $("#ctl00_FormContent_txtLyDo");

            //        if (chkXinDuyet.attr('checked') == false && chkGiaHan.attr('checked') == false) {
            //            alert("Chọn 1 loại xin phê duyệt");
            //            return false;
            //        }

            var v_thoigian = '';
            var v_lydo = '';
            if (chkGiaHan.attr('checked') == true) {
                v_thoigian = txtThoiGian.val();
                v_lydo = txtLyDo.val();
                if (v_thoigian.length == 0) {
                    alert("NHẬP THÔNG TIN THỜI GIAN GIA HẠN");
                    return false;
                }
            }

            if (!window.confirm("Bạn có muốn xin BLĐ phê duyệt xuất biên bản KT?"))
                return false;

            var doankt = Qry["doankt"];
            var dotkt = Qry["dotkt"];

            var url = "ReViewDotKiemToan.aspx";
            var query = "act=updatestatus_bbkt";
            query += "&dotkt=" + dotkt + "&doankt=" + doankt + "&v_thoigian=" + v_thoigian + "&v_lydo=" + v_lydo;
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
                    else {
                        alert(message);
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



    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ButtonExtendBefore" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ButtonExtend" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FormFooter" runat="server">
</asp:Content>
