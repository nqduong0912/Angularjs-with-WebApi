<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="TieuChiDinhTinh_Input.aspx.cs" Inherits="VPB_KTNB.Modules.Lookup.DanhMuc.TieuChiNam.TieuChiDinhTinh_Input" %>

<%@ Register Assembly="C1.Web.C1Input.2" Namespace="C1.Web.C1Input" TagPrefix="cc1" %>
<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <div class="form-horizontal">
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Tên tiêu chí<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_05A093C7_A1C3_486C_BEF2_A590D203111B" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>

        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Diễn giải</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_E628D09D_1337_4103_9B41_915339783731" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control"></asp:TextBox>
            </div>
        </div>

        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Trạng thái</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="DOCSTATUS" runat="server" CssClass="form-control" Width="100px">
                </asp:DropDownList>
            </div>
        </div>
        
        <div class="form-group">
            <asp:TextBox ID="ID8_1EB1E332_0620_4AD1_A87B_D672E7D04BC3" runat="server"></asp:TextBox>
        </div>
        <asp:Panel ID="pnDinhTinh" GroupingText="Các giá trị định tính" runat="server" Visible="true">
            <div class="form-group">
                <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Giá trị định tính<span class="star-red">*</span></label>
                <div class="col-sm-6">
                    <asp:TextBox ID="tbGTDinhTinh" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Điểm quy đổi</label>
                <div class="col-sm-6">
                    <cc1:C1WebNumericEdit ID="tbDiemDinhTinh" runat="server"
                        SkinID="C1WebNumeric" Width="15%" Font-Size="Small" DecimalPlaces="0" Culture="en-US"
                        ThousandsSeparator="false" Value="1" SmartInputMode="false" MinValue="0"
                        UpDownAlign="None" Height="34px" CssClass="form-control">
                    </cc1:C1WebNumericEdit>
                </div>
            </div>
            
            <div class="form-group">
                <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label"></label>
                <div class="col-sm-3">
                    <asp:Button ID="btnCapNhatDinhTinh" Text="Cập nhật giá trị" runat="server" CssClass="form-control" Width="150px" OnClick="btnCapNhatDinhTinh_Click"></asp:Button>
                </div>
                <asp:Label ID="tbDinhTinh" runat="server" />
            </div>
            <div class="form-group">
                <label class="col-sm-2 col-sm-offset-1 control-label"></label>
                <div class="col-sm-7">
                    <asp:Repeater runat="server" ID="dsTPDinhTinh" OnItemCommand="dsTPDinhTinh_ItemCommand">
                        <HeaderTemplate>
                            <table class="table table-striped table-bordered table-hover">
                                <thead class="color-green">
                                    <tr>
                                        <th>Giá trị định tính</th>
                                        <th>Điểm quy đổi</th>
                                        <th>Hành động</th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <FooterTemplate>
                            </tbody>
                         </table>
                        </FooterTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("Ten") %></td>
                                <td><%# Eval("Diem") %></td>
                                <td class="text-center click-icon">
                                    <asp:LinkButton runat="server" CssClass="btn-delete" ID="btnDeleteDT" OnClientClick="if (!confirm('Bạn chắc chắn muốn xóa?')) return false;" CommandName="delete" CommandArgument='<%# Eval("Ten") %>'><span class="delete-file"></span></asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            
        </asp:Panel>
        
        <asp:HiddenField ID="hdfDanhSach" runat="server" />
    </div>

    <script type="text/javascript">
        var _documentid;
        /*********************************************************/
        $(document).ready(function () {
            //do smt here
        });
        /*********************************************************/
        function preparesavedoc(documentID, doctypeID) {
            var ten = GetSvrCtlValue("ID8_05A093C7_A1C3_486C_BEF2_A590D203111B");
            var gtDinhTinh = GetSvrCtlValue("ID8_1EB1E332_0620_4AD1_A87B_D672E7D04BC3");
            alert(gtDinhTinh);
            if (ten.trim().length == 0)
            {
                alert("Nhập tên tiêu chí.");
                return;
            }
            if (gtDinhTinh.trim().length < 5) {
                alert("Nhập ít nhất 1 giá trị định tính.");
                return;
            }
            var diengiai = GetSvrCtlValue("ID8_E628D09D_1337_4103_9B41_915339783731");
            var diem = GetSvrCtlValue("ID6_79C46A92_E0D4_4571_8AB3_9DA995B97BD9");
            var url = "TieuChiDinhTinh_Input.aspx";
            var query = "act=checkvalue";
            var valueactive = $("#ctl00_FormContent_DOCSTATUS").val();
            query += "&p=05A093C7-A1C3-486C-BEF2-A590D203111B";
            query += "&v=" + ten;
            query += "&diengiai=" + diengiai;
            query += "&diem=" + diem;
            query += "&valueactive=" + valueactive;

            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (data) {
                    FinishProcessingForm();
                    if (data == "0") {
                        savedocument(documentID, doctypeID, savedoc_success, savedoc_error);
                    }
                    else
                        alert("Dữ liệu nhập bị trùng lặp. Kiểm tra lại.");
                }
            });
        }

        function prepareupdatedoc(documentID) {
            var ten = GetSvrCtlValue("ID8_05A093C7_A1C3_486C_BEF2_A590D203111B");
            var gtDinhTinh = GetSvrCtlValue("ID8_1EB1E332_0620_4AD1_A87B_D672E7D04BC3");
            if (ten.trim().length == 0){
                alert("Nhập tên tiêu chí.");
                return;
            }
            if (gtDinhTinh.trim().length < 5) {
                alert("Nhập ít nhất 1 giá trị định tính.");
                return;
            }
            var diengiai = GetSvrCtlValue("ID8_E628D09D_1337_4103_9B41_915339783731");
            var diem = GetSvrCtlValue("ID6_79C46A92_E0D4_4571_8AB3_9DA995B97BD9");
            var url = "TieuChiDinhTinh_Input.aspx";
            var query = "act=checkvalueupdate";
            var valueactive = $("#ctl00_FormContent_DOCSTATUS").val();
            query += "&p=05A093C7-A1C3-486C-BEF2-A590D203111B";
            query += "&v=" + ten;
            query += "&diengiai=" + diengiai;
            query += "&diem=" + diem;
            query += "&valueactive=" + valueactive;
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
            window.location.href = 'TieuChiDinhTinh.aspx?a=dbb1170f-fb15-4585-9c21-e21c32d4de86';
        }
        function savedoc_error() {
            alert(MSG_ADD_ER);
        }
        function update_success() {
            alert(MSG_EDIT_OK);
            window.location.href = 'TieuChiDinhTinh.aspx?a=dbb1170f-fb15-4585-9c21-e21c32d4de86';
        }
        function update_error() {
            alert(MSG_EDIT_ER);
        }
        function delete_success() {
            alert(MSG_DEL_OK);
            window.location.href = 'TieuChiDinhTinh.aspx?a=dbb1170f-fb15-4585-9c21-e21c32d4de86';
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
