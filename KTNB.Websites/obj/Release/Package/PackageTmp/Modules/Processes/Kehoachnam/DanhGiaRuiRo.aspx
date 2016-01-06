<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="DanhGiaRuiRo.aspx.cs" Inherits="VPB_KTNB.Modules.Processes.KeHoachNam.DanhGiaRuiRo" %>

<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">    
    <div class="form-horizontal">
        <div class="form-group">
            <label for="inputName" class="col-sm-3 control-label">Năm</label>
            <div class="col-sm-3">
                <asp:DropDownList ID="drpYears" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpYear_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
            <label for="inputName" class="col-sm-3 control-label">Loại đối tượng kiểm toán</label>
            <div class="col-sm-3">
                <asp:DropDownList ID="drpLoaiDTKT" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpYear_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 control-label">Đối tượng kiểm toán</label>
            <div class="col-sm-3">
                <asp:DropDownList ID="DropDownList1" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpYear_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
            <label for="inputName" class="col-sm-3 control-label">Bộ tiêu chí được chọn</label>
            <div class="col-sm-3">
                <asp:DropDownList ID="DropDownList2" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpYear_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
        </div>
        
        <div class="form-group">
            <label for="inputName" class="col-sm-3 control-label">Thời gian kiểm toán gần nhất</label>
            <div class="col-sm-3">
                <input type="text" class="form-control hasDatepicker" id="datepicker">
                <img class="ui-datepicker-trigger" src="/Images/ktnb/calendar.png" alt="Thời gian kiểm toán gần nhất" title="Thời gian kiểm toán gần nhất">
            </div>
            <div class="col-sm-6">
                 <asp:Button runat="server" ID="btnSearch" CssClass="btn color-green btn-xs" Text="Đánh giá rủi ro" />
            </div>
        </div>
    </div>    
    <br />
    <table width="100%">
        <tr>
            <td>            
                <C1WebGrid:C1WebGrid runat="server" ID="dataCtrl" Width="100%" 
                    OnItemCreated="dataCtrl_OnItemCreated" OnItemDataBound="dataCtrl_OnItemDataBound"
                    GroupIndent="20" ItemStyle-Height="30px" OnItemCommand="dataCtrl_ItemCommand">
                    <Columns>
                        <C1WebGrid:C1TemplateColumn Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="PK_ID" Text='<%# DataBinder.Eval(Container.DataItem,"Id")%>'></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Năm" DataField="Nam">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Loại đối tượng kiểm toán" DataField="LoaiDTKT">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Bộ tiêu chí năm" DataField="BoTieuChiNam">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1TemplateColumn Visible="true" HeaderText="Trạng thái">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lbStatus" Text='<%# DataBinder.Eval(Container.DataItem,"TrangThai")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="14%" HorizontalAlign="Right" VerticalAlign="Middle" />
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1TemplateColumn >
                            <ItemTemplate>
                                <asp:RadioButton ID="RowSelector" runat="server" CssClass="cssrbt" GroupName="rdb" Checked='<%# DataBinder.Eval(Container.DataItem,"IsActive")%>'></asp:RadioButton>
                            </ItemTemplate>
                            <ItemStyle Width="14%" HorizontalAlign="Right" VerticalAlign="Middle" />
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Ngày cập nhật" DataField="NgayCapNhat" DataFormatString="{0:dd/MM/yyyy}">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1TemplateColumn HeaderText="Hành động">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lbTrinhduyet" Text="Trình Duyệt" Visible ="false" onclick="lbTrinhduyet_Click" />
                            </ItemTemplate>
                            <ItemStyle Width="12%" HorizontalAlign="Center" VerticalAlign="Middle" />
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
        $(function () {
            $(".cssrbt").each(function () {
                $(this).find(":radio").attr("name", "rdb");
            });
        })
        function Active(id)
        {
            alert(id);
        }
        function newform() {
            var strOptions = $("#ctl00_FormContent_drpYears").val();
            window.location.href = "QuanLyBoTieuChiKeHoachNam_Input.aspx?y=" + strOptions;

        }
        function LoadDocument(DocumentID) {
            url = "QuanLyBoTieuChiKeHoachNam_Input.aspx?act=loaddoc&doc=" + DocumentID;
            window.location.href = url;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ButtonExtendBefore" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ButtonExtend" runat="server">    
    <asp:Button runat="server" ID="btnImport" Text="Import File đánh giá" CssClass="btn btn-xs btn-success btn-block  btn-submit" Style="width: 148px !important;" />
    <asp:Button runat="server" ID="btnExport" Text="Export File đánh giá" CssClass="btn btn-xs btn-success btn-block  btn-submit" Style="width: 148px !important;" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FormFooter" runat="server">
</asp:Content>
