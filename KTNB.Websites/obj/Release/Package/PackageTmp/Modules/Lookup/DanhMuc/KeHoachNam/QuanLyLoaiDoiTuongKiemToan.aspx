<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="QuanLyLoaiDoiTuongKiemToan.aspx.cs" Inherits="VPB_KTNB.Modules.Lookup.DanhMuc.KeHoachNam.QuanLyLoaiDoiTuongKiemToan" %>

<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <div class="form-horizontal">
        <div class="form-group">
            <label class="col-sm-1 col-sm-offset-1 control-label">Năm<span class="star-red">*</span></label>
            <div class="col-sm-2">
                <asp:DropDownList ID="drpYears" runat="server" AutoPostBack="true" Height="30px" OnSelectedIndexChanged="drpYear_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
       
            <label class="col-sm-1 col-sm-offset-1 control-label">Đơn vị<span class="star-red">*</span></label>
            <div class="col-sm-5">
                <asp:DropDownList ID="drpDonVi" runat="server" AutoPostBack="true" Height="30px" OnSelectedIndexChanged="drpYear_SelectedIndexChanged"> 
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <C1WebGrid:C1WebGrid runat="server" ID="dataCtrl" Width="100%" OnItemDataBound="dataCtrl_OnItemDataBound"
                GroupIndent="20" ItemStyle-Height="30px">
                <Columns>
                    <C1WebGrid:C1TemplateColumn Visible="false">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="Id" Text='<%# DataBinder.Eval(Container.DataItem,"Id")%>'></asp:Label>
                        </ItemTemplate>
                    </C1WebGrid:C1TemplateColumn>
                    <C1WebGrid:C1TemplateColumn Visible="false">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="SourceId" Text='<%# DataBinder.Eval(Container.DataItem,"SourceId")%>'></asp:Label>
                        </ItemTemplate>
                    </C1WebGrid:C1TemplateColumn>
                    <C1WebGrid:C1BoundColumn HeaderText="Năm" DataField="Nam">
                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                    </C1WebGrid:C1BoundColumn>
                    <C1WebGrid:C1BoundColumn HeaderText="Phòng ban" DataField="Phongban">
                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                    </C1WebGrid:C1BoundColumn>
                    <C1WebGrid:C1BoundColumn HeaderText="Loại ĐTKT" DataField="Ten">
                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                    </C1WebGrid:C1BoundColumn>
                    <C1WebGrid:C1BoundColumn HeaderText="Diễn giải" DataField="Diengiai">
                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                    </C1WebGrid:C1BoundColumn>
                    <C1WebGrid:C1TemplateColumn>
                        <ItemTemplate>
                            <asp:Image runat="server" ID="imgDelete" ImageUrl="~/Images/delete.gif" ToolTip="Xóa"
                                Style="cursor: pointer" />
                        </ItemTemplate>
                        <ItemStyle Width="8%" HorizontalAlign="Center" VerticalAlign="Middle" />
                    </C1WebGrid:C1TemplateColumn>
                </Columns>
            </C1WebGrid:C1WebGrid>
        </div>
    </div>
    <script type="text/javascript">
        /*********************************************************/
        $(document).ready(function () {
            //do smt here
        });
        /*********************************************************/
        function newform() {
            var strOptions = $("#ctl00_FormContent_drpYears").val();
            window.location.href = "QuanLyLoaiDoiTuongKiemToan_Input.aspx?y=" + GetSvrCtlValue("drpYears") + "&dv=" + GetSvrCtlValue("drpDonVi");

        }
        function DeleteDocument(id) {
            var r = confirm(MSG_DEL_QUESTION);
            if (!r) return false;
            $.ajax({
                type: "POST",
                url: "QuanLyLoaiDoiTuongKiemToan.aspx/DeleteEntity",
                data: JSON.stringify({ dtktId: id }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function () {
                    delete_success();
                }
            });
        }
        function delete_success() {
            alert(MSG_DEL_OK);
            window.location.href = 'QuanLyLoaiDoiTuongKiemToan.aspx?y=' + GetSvrCtlValue("drpYears") + "&dv=" + GetSvrCtlValue("drpDonVi");;
        }
    </script>
</asp:Content>
