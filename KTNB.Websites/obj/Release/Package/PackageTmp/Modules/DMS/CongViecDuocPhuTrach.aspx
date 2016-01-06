<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true"
    CodeBehind="CongViecDuocPhuTrach.aspx.cs" Inherits="VPB_TDHS.Modules.DMS.CongViecDuocPhuTrach" %>

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
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Đối tượng kiểm toán</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_2A4CA2AD_0282_4D57_86AC_D973D281EF54" runat="server" CssClass="form-control" Enabled="False" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Đợt kiểm toán</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_63A0C4B1_2088_4994_B891_2FF65EB20265" runat="server" CssClass="form-control" Enabled="False" Width="300px"></asp:TextBox>
            </div>
        </div>
    </div>
    <%--<table width="100%">
        <tr>
            <td style="width: 222px">Đối tượng kiểm toán
            </td>
            <td>
                <asp:TextBox Enabled="false" ID="ID8_2A4CA2AD_0282_4D57_86AC_D973D281EF54" runat="server"
                    SkinID="TextBoxReadOnly"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Đợt kiểm toán
            </td>
            <td>
                <asp:TextBox Enabled="false" ID="ID8_63A0C4B1_2088_4994_B891_2FF65EB20265" runat="server"
                    SkinID="TextBoxReadOnly"></asp:TextBox>
            </td>
        </tr>
    </table>--%>
    <table width="100%">
        <tr>
            <td>
                <C1WebGrid:C1WebGrid runat="server" ID="dataCtrl" Width="100%" DataSourceID="ObjectDataSource1"
                    OnItemCreated="dataCtrl_OnItemCreated" OnItemDataBound="dataCtrl_OnItemDataBound"
                    GroupIndent="20" ItemStyle-Height="30px">
                    <Columns>
                        <C1WebGrid:C1TemplateColumn HeaderText="Chi tiết">
                            <ItemTemplate>
                                <asp:Image runat="server" ID="ImgEdit" ImageUrl="~/Images/viewdetail.gif" ToolTip="Chi tiết công việc"
                                    Style="cursor: pointer" />
                            </ItemTemplate>
                            <ItemStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1TemplateColumn Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="PK_DocumentID" Text='<%# DataBinder.Eval(Container.DataItem,"PK_DocumentID")%>'></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Công việc" DataField="Tên công việc">
                            <ItemStyle Width="30%" HorizontalAlign="Left" VerticalAlign="Top" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1TemplateColumn HeaderText="Bắt đầu">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="NgayBatDau" Text='<%# DataBinder.Eval(Container.DataItem,"Ngày bắt đầu")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="5%" VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1TemplateColumn HeaderText="Kết thúc">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="NgayKetThuc" Text='<%# DataBinder.Eval(Container.DataItem,"Ngày kết thúc")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="5%" VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1TemplateColumn HeaderText="Trạng thái">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblTrangThai" Text='<%# DataBinder.Eval(Container.DataItem,"STATUS")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1TemplateColumn HeaderText="Phát hiện">
                            <ItemTemplate>

                                <asp:Label runat="server" CssClass="gachchan" ID="lblTongSoPhatHien"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1TemplateColumn HeaderText="RR chưa đánh giá KS">
                            <ItemTemplate>

                                <asp:Label runat="server" ID="lblRR"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1TemplateColumn HeaderText="Ghi chú">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblGhiChu"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1TemplateColumn HeaderText="Số lần từ chối">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblSoLanTuChoi" Text='<%# DataBinder.Eval(Container.DataItem,"Nhận xét")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </C1WebGrid:C1TemplateColumn>
                    </Columns>
                </C1WebGrid:C1WebGrid>
            </td>
        </tr>
    </table>
    <style>
        .gachchan {
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
        var DocumentID =
        function newform() {
            var doankt = Qry["doankt"]; //getParameterByName("doankt");
            var dotkt = Qry["dotkt"];
            window.location.href = "ChuongTrinhKiemToan_Load.aspx?dotkt=" + dotkt + "&doankt=" + doankt;
        }
        function LoadDocument(DocumentID) {
            var cv = Qry["cv"]; //getParameterByName("doankt");
            var doankt = Qry["doankt"]; //getParameterByName("doankt");
            var dotkt = Qry["dotkt"];
            url = "CongViecDuocPhuTrach_Load.aspx?act=loaddoc&doc=" + DocumentID + "&dotkt=" + dotkt + "&doankt=" + doankt + "&cv=" + cv;
            window.location.href = url;
        }
        function LoadPageDanhSachPhatHien(DocumentID) {
            var cv = Qry["cv"];
            var doankt = Qry["doankt"]; //getParameterByName("doankt");
            var dotkt = Qry["dotkt"];

            if (cv == 'nguoithuchien')
                url = "DanhSachPhatHien.aspx?act=loaddoc&doc=" + DocumentID + "&cv=" + cv + "&doankt=" + doankt + "&dotkt=" + dotkt;
            else
                url = "DanhSachPhatHien.aspx?act=loaddoc&doc=" + DocumentID + "&cv=" + cv + "&doankt=" + doankt + "&dotkt=" + dotkt;
            window.location.href = url;
        }
        function LapChuongTrinhKiemToan(DocumentID) {
            var url = "../../Controls/Tab/ThanhLapDoanKT_Load.aspx?doc=" + DocumentID + "&act=dotkt";
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
