<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="QuanLyBoTieuChiKeHoachNam_Input.aspx.cs" Inherits="VPB_KTNB.Modules.Lookup.DanhMuc.KeHoachNam.QuanLyBoTieuChiKeHoachNam_Input" %>
<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<%@ Register Assembly="C1.Web.C1Input.2" Namespace="C1.Web.C1Input" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <asp:ObjectDataSource runat="server" ID="ObjectDataSource1" SelectMethod="getDocumentList"
        TypeName="VPB_KTNB.Helpers.DataSource">
        <SelectParameters>
            <asp:Parameter Name="DocumentTypeID" Type="String" />
            <asp:Parameter Name="DocFields" Type="String" />
            <asp:Parameter Name="PropertyFields" Type="String" />
            <asp:Parameter Name="Condition" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>

    <div class="form-horizontal">
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Năm</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="drpYears" runat="server" CssClass="form-control" Width="300px">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Loại đối tượng kiểm toán</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="drpLoaiDTKT" runat="server" CssClass="form-control" Width="300px">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Bộ tiêu chí năm</label>
            <div class="col-sm-6">
                <asp:TextBox ID="tbBoTieuChi" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>
        
        <div class="form-group">
            <label class="col-sm-5 col-sm-offset-1 control-label">1.Danh sách Tiêu chí chính</label>
            <div class="col-sm-4">
                <asp:Button ID="btnAddTCC" Text="Thêm Tiêu chí chính" runat="server" CssClass="form-control" Width="150px"></asp:Button>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-2">
            </div>
            <div class="col-sm-6">
                <C1WebGrid:C1WebGrid runat="server" ID="dsTCC" Width="100%" OnItemDataBound="dsTCC_ItemDataBound"
                    GroupIndent="20" ItemStyle-Height="30px">
                    <Columns>
                        <C1WebGrid:C1TemplateColumn Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="PK_DocumentID" Text='<%# DataBinder.Eval(Container.DataItem,"PK_DocumentID")%>'></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Tên" DataField="Tên">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1TemplateColumn HeaderText="Tỷ trọng" >
                            <ItemTemplate>
                                <asp:Label ID="TyTrong" runat="server" ></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="8%" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </C1WebGrid:C1TemplateColumn>
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
        <div class="form-group">
            <label class="col-sm-5 col-sm-offset-1 control-label" >2.Công thức cho bộ tiêu chí</label>
        </div>
        <div class="form-group">
            <label for="drpTieuChiChinh" class="col-sm-3 col-sm-offset-1 control-label">Tiêu chí chính</label>
            <div class="col-sm-3">
                <asp:DropDownList ID="drpTieuChiChinh" runat="server" CssClass="form-control" Width="210px">
                </asp:DropDownList>
            </div>
            <div class="col-sm-3">
                <asp:Button ID="btnChooseTCC" Text="Chọn" runat="server" CssClass="form-control" Width="70px"></asp:Button>
            </div>
        </div>
        <div class="form-group">
            <label for="drpTieuChiChinh" class="col-sm-3 col-sm-offset-1 control-label">Trọng Số</label>
            <div class="col-sm-6">
                <asp:TextBox ID="tbTrongSo" runat="server" CssClass="form-control" Width="300px" ReadOnly="true"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
                <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Danh sách toán tử</label>
                <div class="col-sm-1">
                    <asp:Button ID="btnCong" runat="server" Text="+" OnClick="btnCong_Click" CssClass="form-control"/>
                </div>
                <div class="col-sm-1">
                    <asp:Button ID="btnTru" runat="server" Text="-" OnClick="btnTru_Click" CssClass="form-control"/>
                </div>
                <div class="col-sm-1">
                    <asp:Button ID="btnNhan" runat="server" Text="x" OnClick="btnNhan_Click" CssClass="form-control"/>
                </div>
                <div class="col-sm-1">
                    <asp:Button ID="btnChia" runat="server" Text="/" OnClick="btnChia_Click" CssClass="form-control"/>
                </div>
                <div class="col-sm-1">
                    <asp:Button ID="btnMoNgoac" runat="server" Text="(" OnClick="btnMoNgoac_Click" CssClass="form-control"/>
                </div>
                <div class="col-sm-1">
                    <asp:Button ID="btnDongNgoac" runat="server" Text=")" OnClick="btnDongNgoac_Click" CssClass="form-control"/>
                </div>
            </div>
            <div class="form-group">
                <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Công thức</label>
                <div class="col-sm-5">
                    <asp:TextBox ID="tbCongThuc" runat="server" CssClass="form-control" Width="300px" ReadOnly ="true"></asp:TextBox>
                </div>
                <div class="col-sm-2">
                    <asp:Button ID="btnLamLai" Text="Làm lại" AutoPostBack="true" runat="server" CssClass="form-control" Width="80px" OnClick="btnLamLai_Click"></asp:Button>
                </div>
            </div>
    <script type="text/javascript">
        var _documentid;
        /*********************************************************/
        $(document).ready(function () {
            //do smt here
        });
        /*********************************************************/
        function btnAddTCC_Onclick()
        {
            var tenBTC = GetSvrCtlValue("tbBoTieuChi");
            if (tenBTC.length == 0)
            {
                alert("Nhập tên bộ tiêu chí trước khi tiếp tục.");
                return;
            }
            window.location.href = "QuanLyBoTieuChiKeHoachNam_ThemTCC.aspx?btc=" + tenBTC;
        }
        function preparesavedoc(documentID, doctypeID) {
            //var r = confirm(MSG_ADD_QUESTION);
            //if (!r) return false;
            var url = "QuanLyBoTieuChiKeHoachNam_Input.aspx";
            var query = "act=checkvalue";
            var nam = GetSvrCtlValue("drpYears");
            var loaiDTKT = GetSvrCtlValue("drpLoaiDTKT");
            var boTC = GetSvrCtlValue("drpBoTieuChi");
            query += "&loaiDT=" + loaiDTKT;
            query += "&y=" + nam;
            query += "&tc=" + boTC;
            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (data) {
                    FinishProcessingForm();
                    if (data == "0") {
                        savedoc_success();
                    }
                    else
                        alert("Dữ liệu nhập bị trùng lặp. Kiểm tra lại.");
                }
            });
        }

        function prepareupdatedoc(documentID) {
            var url = "QuanLyBoTieuChiKeHoachNam_Input.aspx";
            var query = "act=checkvalueupdate";
            var nam = $("#ctl00_FormContent_ID8_3DCD0CDE_1330_455B_BC55_32C234360CFE").val();
            var loaiDTKT = $("#ctl00_FormContent_ID8_BA3A0E4C_33BC_475B_B547_4580CD602D68").val();
            var boTC = $("#ctl00_FormContent_ID8_FE3C31CC_4A33_4712_86B4_A037D68173E7").val();
            query += "&loaiDT=" + loaiDTKT;
            query += "&y=" + nam;
            query += "&tc=" + boTC;
            query += "&doc=" + documentID;
            StartProcessingForm("");

            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (data) {
                    FinishProcessingForm();
                    if (data == "0")
                        updatedocument(documentID, update_success, update_error);
                    else
                        alert("Dữ liệu nhập bị trùng lặp. Kiểm tra lại.");
                }
            });
        }

        function savedoc_success() {
            alert(MSG_ADD_OK);
            window.location.href = 'QuanLyBoTieuChiKeHoachNam.aspx';
        }
        function savedoc_error() {
            alert(MSG_ADD_ER);
        }
        function update_success() {
            alert(MSG_EDIT_OK);
            window.location.href = 'QuanLyBoTieuChiKeHoachNam.aspx?y=' + GetSvrCtlValue("ID8_3DCD0CDE_1330_455B_BC55_32C234360CFE");
        }
        function update_error() {
            alert(MSG_EDIT_ER);
        }
        function delete_success() {
            alert(MSG_DEL_OK);
            window.location.href = 'QuanLyBoTieuChiKeHoachNam.aspx?y=' + GetSvrCtlValue("ID8_3DCD0CDE_1330_455B_BC55_32C234360CFE");
        }
        function delete_error() {
            alert(MSG_DEL_ER);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ButtonExtendBefore" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ButtonExtend" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FormFooter" runat="server">
</asp:Content>

