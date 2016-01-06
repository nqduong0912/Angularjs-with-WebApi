<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true"
    CodeBehind="PhatHienViPham_Load.aspx.cs" Inherits="VPB_TDHS.Modules.DMS.PhatHienViPham_Load" %>

<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <asp:ScriptManager runat="server" ID="ScriptManager1">
    </asp:ScriptManager>
    <div class="form-horizontal">
        Thêm/Sửa Phát hiện vi phạm
         <div class="form-group">
             <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Đối tượng kiểm toán</label>
             <div class="col-sm-6">
                 <asp:TextBox ID="ID8_7222867E_EC9C_46FF_BC71_20873B7EBD37" runat="server" CssClass="form-control" Enabled="False" Width="300px"></asp:TextBox>
             </div>
         </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Đợt kiểm toán</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_1C98F56A_9CFE_410E_882C_7AB370D99A78" runat="server" CssClass="form-control" Enabled="False" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Công việc</label>
            <div class="col-sm-6">
                <asp:TextBox ID="txtCongViec" runat="server" CssClass="form-control" Enabled="False" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Phát hiện<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_D5637BAF_54F3_4724_9E3B_8543EB93509A" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Mức độ</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="ID8_A4C22F5B_2271_46D8_AD8D_B88B7399317A" runat="server" CssClass="form-control" Width="300px">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Chi tiết</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_10A26FD0_3309_4E1E_B83B_AC55E8194C2F" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Nguyên nhân</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_0480690F_6373_4AC3_BA8C_FF61AA8763B7" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Ảnh hưởng</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_5D74DB08_2EFD_47DC_8970_0C015F89F05D" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Khuyến nghị</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_06271BD7_653E_4EF4_826F_991A408A2074" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Ghi chú</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_483F188D_3325_4EAB_8A9E_24B187EA5BE1" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">STK-HĐ vi phạm</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_CF188F52_E26C_4FA4_B3EA_566E64D68E35" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Tên KH-Tên TK</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_28F7AE8F_FAA5_4548_A761_E5ABB99A3FDC" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Loại tiền</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_85561BD3_7EA6_4078_8D9E_92555EE9D5D4" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Ngày phát sinh</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_9964AFC3_D7D3_4EAE_9E97_A06491986376" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Giá trị giao dịch</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_0823B6FB_A376_4922_A3D2_F9DFE428F132" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Giá trị tổn thất</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_5DBE8B67_B15B_44B0_B338_BFF9A5893E38" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Lỗi vi phạm</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_BCB66023_B645_4ACE_9BDA_739E36D2EA91" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Dẫn chiếu quy định vi phạm</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_D4E3708C_A637_4737_95DD_5BE0791CF5AB" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Mức độ rủi ro của vi phạm</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_DE9FFB27_2B1D_448B_9F47_BFC8BA38E980" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Vấn đề hệ thống có liên quan</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_EC4C86B7_B065_43D0_B6DE_42A3429A7B3C" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Cá nhân - Tập thể vi phạm</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_384E8A87_E6C5_46B2_B855_1EE0C954255B" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Người kiểm soát liên quan</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_A75D10D9_AC94_41B8_9BDC_0B5DFBB25ECC" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Dẫn chiếu điều khoản xử lý vi phạm</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_D1FA20D3_1754_4FE9_B05E_BE912E977F34" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Dẫn chiếu hình thức xử lý vi phạm</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_01B25AC2_2EAA_4779_A8AD_AFC45EE8C0AB" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Phản hồi của đơn vị</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_246A4FCC_FC59_4900_8ACF_9DBB2323E14A" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Kiến nghị của KTNB</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_1D5CAE34_E7C1_4539_9EA2_0F7C40454DA8" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Nội dung khắc phục</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_CDD9A94A_43F3_4289_835C_32634683D866" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group" id="idTrangThai">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Trạng thái</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="DOCSTATUS" runat="server" CssClass="form-control" Width="100px">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group" id="trDaNhanXet">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Đã nhận xét</label>
            <div class="col-sm-6">
                <asp:TextBox Rows="5" Columns="55" Text="" Enabled="false" TextMode="MultiLine" ID="DaNhanXetNguoiDuyet1"
                    runat="server" CssClass="form-control"></asp:TextBox>(người duyệt)
            </div>
        </div>
        <div class="form-group" id="trNhanXet">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Nhận xét</label>
            <div class="col-sm-6">
                <asp:TextBox Rows="5" Columns="55" Text="" Enabled="false" TextMode="MultiLine" ID="ID8_75AF3ED2_78F6_4DAE_BB20_157920BAC865"
                    runat="server" CssClass="form-control"></asp:TextBox>(người duyệt)
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Lịch sử từ chối</label>
            <div class="col-sm-6">
                <C1WebGrid:C1WebGrid runat="server" ID="gridTuChoi" Width="75%" GroupIndent="20"
                    ItemStyle-Height="30px">
                    <Columns>
                        <C1WebGrid:C1BoundColumn HeaderText="Lý do từ chối" DataField="LyDo">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Người từ chối" DataField="NguoiNhanXet">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Ngày từ chối" DataField="NgayNhanXet">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                    </Columns>
                </C1WebGrid:C1WebGrid>
            </div>
        </div>
    </div>
    <%--<table width="100%" id="tblTruongDoan">
        <tr class="GridHeader">
            <td colspan="2">Thêm/Sửa Phát hiện vi phạm 
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Đối tượng kiểm toán
            </td>
            <td>
                <asp:TextBox Enabled="false" ID="ID8_7222867E_EC9C_46FF_BC71_20873B7EBD37" runat="server"
                    SkinID="TextBoxReadOnly"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Đợt kiểm toán
            </td>
            <td>
                <asp:TextBox Enabled="false" ID="ID8_1C98F56A_9CFE_410E_882C_7AB370D99A78" runat="server"
                    SkinID="TextBoxReadOnly"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Công việc
            </td>
            <td>
                <asp:TextBox Enabled="false" ID="txtCongViec" runat="server" SkinID="TextBoxReadOnly"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Phát hiện
            </td>
            <td>
                <asp:TextBox ID="ID8_D5637BAF_54F3_4724_9E3B_8543EB93509A" runat="server" SkinID="TextBoxRequired"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Mức độ
            </td>
            <td>
                <asp:DropDownList ID="ID8_A4C22F5B_2271_46D8_AD8D_B88B7399317A" runat="server" SkinID="DropDownList">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Chi tiết
            </td>
            <td>
                <asp:TextBox ID="ID8_10A26FD0_3309_4E1E_B83B_AC55E8194C2F" runat="server" SkinID=""></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Nguyên nhân
            </td>
            <td>
                <asp:TextBox ID="ID8_0480690F_6373_4AC3_BA8C_FF61AA8763B7" runat="server" SkinID=""></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Ảnh hưởng
            </td>
            <td>
                <asp:TextBox ID="ID8_5D74DB08_2EFD_47DC_8970_0C015F89F05D" runat="server" SkinID=""></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Khuyến nghị
            </td>
            <td>
                <asp:TextBox ID="ID8_06271BD7_653E_4EF4_826F_991A408A2074" runat="server" SkinID=""></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Ghi chú
            </td>
            <td>
                <asp:TextBox ID="ID8_483F188D_3325_4EAB_8A9E_24B187EA5BE1" runat="server" SkinID=""></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">STK-HĐ vi phạm
            </td>
            <td>
                <asp:TextBox ID="ID8_CF188F52_E26C_4FA4_B3EA_566E64D68E35" runat="server" SkinID=""></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Tên KH-Tên TK
            </td>
            <td>
                <asp:TextBox ID="ID8_28F7AE8F_FAA5_4548_A761_E5ABB99A3FDC" runat="server" SkinID=""></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Loại tiền
            </td>
            <td>
                <asp:TextBox ID="ID8_85561BD3_7EA6_4078_8D9E_92555EE9D5D4" runat="server" SkinID=""></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Ngày phát sinh
            </td>
            <td>
                <asp:TextBox ID="ID8_9964AFC3_D7D3_4EAE_9E97_A06491986376" runat="server" SkinID=""></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Giá trị giao dịch
            </td>
            <td>
                <asp:TextBox ID="ID8_0823B6FB_A376_4922_A3D2_F9DFE428F132" runat="server" SkinID=""></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Giá trị tổn thất
            </td>
            <td>
                <asp:TextBox ID="ID8_5DBE8B67_B15B_44B0_B338_BFF9A5893E38" runat="server" SkinID=""></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Lỗi vi phạm
            </td>
            <td>
                <asp:TextBox ID="ID8_BCB66023_B645_4ACE_9BDA_739E36D2EA91" runat="server" SkinID=""></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Dẫn chiếu qui định vi phạm
            </td>
            <td>
                <asp:TextBox ID="ID8_D4E3708C_A637_4737_95DD_5BE0791CF5AB" runat="server" SkinID=""></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Mức độ rủi ro của vi phạm
            </td>
            <td>
                <asp:TextBox ID="ID8_DE9FFB27_2B1D_448B_9F47_BFC8BA38E980" runat="server" SkinID=""></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Vấn đề hệ thống có liên quan
            </td>
            <td>
                <asp:TextBox ID="ID8_EC4C86B7_B065_43D0_B6DE_42A3429A7B3C" runat="server" SkinID=""></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Cá nhân-Tập thể vi phạm
            </td>
            <td>
                <asp:TextBox ID="ID8_384E8A87_E6C5_46B2_B855_1EE0C954255B" runat="server" SkinID=""></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Người kiểm soát liên quan
            </td>
            <td>
                <asp:TextBox ID="ID8_A75D10D9_AC94_41B8_9BDC_0B5DFBB25ECC" runat="server" SkinID=""></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Dẫn chiếu điều khoản xử lý vi phạm
            </td>
            <td>
                <asp:TextBox ID="ID8_D1FA20D3_1754_4FE9_B05E_BE912E977F34" runat="server" SkinID=""></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Dẫn chiếu hình thức xử lý vi phạm
            </td>
            <td>
                <asp:TextBox ID="ID8_01B25AC2_2EAA_4779_A8AD_AFC45EE8C0AB" runat="server" SkinID=""></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Phản hồi của đơn vị
            </td>
            <td>
                <asp:TextBox ID="ID8_246A4FCC_FC59_4900_8ACF_9DBB2323E14A" runat="server" SkinID=""></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Kiến nghị của KTNB
            </td>
            <td>
                <asp:TextBox ID="ID8_1D5CAE34_E7C1_4539_9EA2_0F7C40454DA8" runat="server" SkinID=""></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Nội dung khắc phục
            </td>
            <td>
                <asp:TextBox ID="ID8_CDD9A94A_43F3_4289_835C_32634683D866" runat="server" SkinID=""></asp:TextBox>
            </td>
        </tr>
        <tr id="idTrangThai">
            <td style="width: 222px">Trạng thái
            </td>
            <td>
                <asp:DropDownList ID="DOCSTATUS" runat="server" SkinID="DropDownList" Enabled="false">
                </asp:DropDownList>
            </td>
        </tr>
        <tr id="trDaNhanXet" runat="server">
            <td style="width: 222px">Đã Nhận xét
            </td>
            <td>
                <asp:TextBox Rows="2" Columns="55" Text="" Enabled="false" TextMode="MultiLine" ID="DaNhanXetNguoiDuyet1"
                    runat="server" SkinID="TextBoxReadOnly"></asp:TextBox>(người duyệt)
                <br />
            </td>
        </tr>
        <tr id="trNhanXet" runat="server">
            <td style="width: 222px">Nhận xét
            </td>
            <td>
                <asp:TextBox Rows="2" Columns="55" Text="" TextMode="MultiLine" ID="ID8_75AF3ED2_78F6_4DAE_BB20_157920BAC865"
                    runat="server" SkinID=""></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px; vertical-align: top;">Lịch sử từ chối
            </td>
            <td>
                <C1WebGrid:C1WebGrid runat="server" ID="gridTuChoi" Width="75%" GroupIndent="20"
                    ItemStyle-Height="30px">
                    <Columns>
                        <C1WebGrid:C1BoundColumn HeaderText="Lý do từ chối" DataField="LyDo">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Người từ chối" DataField="NguoiNhanXet">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Ngày từ chối" DataField="NgayNhanXet">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                    </Columns>
                </C1WebGrid:C1WebGrid>
            </td>
        </tr>
    </table>--%>
    <style>
        .textbox {
            width: 50%;
        }
    </style>
    <script type="text/javascript">
        var _documentid;
        var _congviec_docid = "<%= _congviec_docid %>";
        var cv = Qry["cv"];
        var doankt = Qry["doankt"];
        var dotkt = Qry["dotkt"];
        var isTruongDoan = "<%= _isTruongDoan %>";
        /*********************************************************/
        var MSG_CONFIRM_ADD_CV = "Bạn có muốn tạo công việc này không?";
        var MSG_ALERT_CHOICE_CV = "Hãy tạo một công việc.";

        var MSG_CONFIRM_ADD_TT = "Bạn có muốn đưa thủ tục kiểm toán này vào công việc này không?";
        var MSG_EXIST_TT = "Thủ tục kiểm toán này này đã tồn tại trong công việc này.";
        var MSG_ADD_TT_SUC = "Đưa thủ tục kiểm toán vào công việc thành công.";
        var MSG_CONFIRM_DEL_TT = "Bạn có muốn đưa thủ tục kiểm toán này ra khỏi công việc?";
        var MSG_CONFIRM_SUBMIT = "Bạn có muốn cập nhật?";
        var MSG_CONFIRM_REJECT = "Bạn có muốn từ chối?";

        var MSG_CANNOT_SUBMIT = "Không submit được vì chưa có thủ tục nào";

        $(document).ready(function () {
            //do smt here
            ApplyCSS();
            $("#idTrangThai").hide();
            HiddenControlTimKiem();
            ShowHideNhanXet();
            //css
            $('input[name*="btnFINISH"]').css('width', '80px');
        });

        function HiddenControlTimKiem() {
            var timkiem = Qry["timkiem"];
            if (timkiem == 'tk') {
                $("img[id*='imgDelete']").hide();
                $("img[id*='imgEdit']").hide();
                $("input[type*='button']").hide();
                $("input[type*='submit']").hide();
                if (isTruongDoan == 'True') {
                    $("#ctl00_btnEDIT").show();
                }

            }
        }
        function ShowHideNhanXet() {
            if (cv == 'nguoiduyet') {
                $('#' + '<%=ID8_75AF3ED2_78F6_4DAE_BB20_157920BAC865.ClientID %>').show();
            }
            else {

                $('#trNhanXet').hide();
                $('#' + '<%=ID8_75AF3ED2_78F6_4DAE_BB20_157920BAC865.ClientID %>').hide();
            }

        }
        /*********************************************************/
        function ApplyCSS() {
            $('input[type="text"]').addClass("textbox");
        }
        function preparesavedoc(documentID, doctypeID) {
            var ten = GetSvrCtlValue("ID8_E48E3FFD_FE56_4B14_AC36_2C036873E1CD");
            var url = "PhatHienHeThong_Load.aspx";
            var query = "act=checkvalue";
            query += "&p=E48E3FFD-FE56-4B14-AC36-2C036873E1CD";
            query += "&v=" + ten;
            query += "&doankt=" + doankt;
            query += "&dotkt=" + dotkt;
            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (data) {
                    FinishProcessingForm();
                    if (data == "0")
                        savedocumentwithlink(documentID, _congviec_docid, doctypeID, savedoc_success, savedoc_error, "")
                    else
                        alert(MSG_DATA_ESXIT);
                }
            });
        }
        function saveNoCheck(documentID, doctypeID) {
            savedocumentwithlink(documentID, _congviec_docid, doctypeID, savedoc_success, savedoc_error, "")
        }
        function updateNoCheck(documentID) {
            updatedocument(documentID, update_success, update_error);
        }

        function capnhattrangthaidone(phathienID) {
            if (!window.confirm(MSG_CONFIRM_SUBMIT))
                return false;
            //alert(documentID);
            var url = "PhatHienViPham_Load.aspx";
            var query = "act=capnhattrangthaidone";
            query += "&phathienID=" + phathienID + "&cv=" + cv + "&congviec_docid=" + _congviec_docid;
            query += "&doankt=" + doankt;
            query += "&dotkt=" + dotkt;
            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (data) {
                    FinishProcessingForm();
                    if (data == "1")
                        alert("Chưa cho phép hoàn tất phát hiện vì các phản hồi chưa đc hoàn tất.");
                    else if (data == "-1")
                        alert("Có lỗi xảy ra.Vui lòng liên hệ với quản trị.");
                    else if (data == "0")
                        window.location.href = "DanhSachPhatHien.aspx?act=loaddoc&doc=" + _congviec_docid + "&cv=" + cv + "&doankt=" + doankt + "&dotkt=" + dotkt;
                }
            });
        }


        function capnhattrangthaipheduyet(phathienID) {
            if (!window.confirm(MSG_CONFIRM_SUBMIT))
                return false;
            var url = "PhatHienViPham_Load.aspx";
            var query = "act=capnhattrangthaipheduyet";
            query += "&phathienID=" + phathienID + "&cv=" + cv + "&congviec_docid=" + _congviec_docid;
            query += "&doankt=" + doankt;
            query += "&dotkt=" + dotkt;
            var nhanXet = GetSvrCtlValue("ID8_75AF3ED2_78F6_4DAE_BB20_157920BAC865");
            nhanXet = nhanXet.trim();
            query += "&nhanxet=" + nhanXet;
            if (nhanXet.length == 0) {
                alert('Bạn chưa nhập nhận xét');
                return false;
            }
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (data) {
                    FinishProcessingForm();
                    window.location.href = "DanhSachPhatHien.aspx?act=loaddoc&doc=" + _congviec_docid + "&cv=" + cv + "&doankt=" + doankt + "&dotkt=" + dotkt;
                }
            });
        }

        function capnhattrangthaituchoi(phathienID) {
            if (!window.confirm(MSG_CONFIRM_REJECT))
                return false;
            var url = "PhatHienViPham_Load.aspx";
            var query = "act=capnhattrangthaituchoi";
            query += "&phathienID=" + phathienID + "&cv=" + cv + "&congviec_docid=" + _congviec_docid;
            query += "&doankt=" + doankt;
            query += "&dotkt=" + dotkt;
            var nhanXet = GetSvrCtlValue("ID8_75AF3ED2_78F6_4DAE_BB20_157920BAC865");
            nhanXet = nhanXet.trim();
            query += "&nhanxet=" + nhanXet;
            if (nhanXet.length == 0) {
                alert('Bạn chưa nhập nhận xét');
                return false;
            }
            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (data) {
                    FinishProcessingForm();
                    window.location.href = "DanhSachPhatHien.aspx?act=loaddoc&doc=" + _congviec_docid + "&cv=" + cv + "&doankt=" + doankt + "&dotkt=" + dotkt;
                }
            });
        }

        function prepareupdatedoc(documentID) {
            var ten = GetSvrCtlValue("ID8_4138B099_7CBD_443E_A12B_9A1FF5D1E08F");
            var url = "KiemSoat_Load.aspx";
            var query = "act=checkvalueupdate";
            query += "&p=4138B099-7CBD-443E-A12B-9A1FF5D1E08F";
            query += "&v=" + ten;
            query += "&doc=" + documentID;
            query += "&doankt=" + doankt;
            query += "&dotkt=" + dotkt;
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
                        alert(MSG_DATA_ESXIT);
                }
            });
        }

        function savedoc_success() {
            alert(MSG_ADD_OK);
            window.location.href = 'DanhSachPhatHien.aspx?act=loaddoc&doc=' + _congviec_docid + "&cv=" + cv + "&doankt=" + doankt + "&dotkt=" + dotkt;
        }
        function savedoc_error() {
            alert(MSG_ADD_ER);
        }
        function update_success() {
            alert(MSG_EDIT_OK);
            window.location.href = 'DanhSachPhatHien.aspx?act=loaddoc&doc=' + _congviec_docid + "&cv=" + cv + "&doankt=" + doankt + "&dotkt=" + dotkt;
        }
        function update_error() {
            alert(MSG_EDIT_ER);
        }
        function delete_success() {
            alert(MSG_DEL_OK);
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
