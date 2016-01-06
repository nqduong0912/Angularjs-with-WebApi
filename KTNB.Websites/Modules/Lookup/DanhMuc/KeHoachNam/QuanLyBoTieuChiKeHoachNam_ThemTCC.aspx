<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="QuanLyBoTieuChiKeHoachNam_ThemTCC.aspx.cs" Inherits="VPB_KTNB.Modules.Lookup.DanhMuc.KeHoachNam.QuanLyBoTieuChiKeHoachNam_ThemTCC" %>
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
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Tên bộ tiêu chí năm</label>
            <div class="col-sm-6">
                <asp:TextBox ID="tbBoTieuChi" runat="server" CssClass="form-control" Width="300px" Enabled="false"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Tên tiêu chí chính</label>
            <div class="col-sm-6">
                <asp:TextBox ID="tbTieuChiChinh" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Tỷ trọng</label>
            <div class="col-sm-6">
                <cc1:C1WebNumericEdit ID="ID6_2E3ECB61_E8C2_433A_8FF9_8B19BB8204CE" runat="server"
                    SkinID="C1WebNumeric" Width="10%" Font-Size="Small" DecimalPlaces="0" Culture="en-US"
                    ThousandsSeparator="false" Value="1" SmartInputMode="false" MaxValue="100" MinValue="0"
                    UpDownAlign="None" Height="34px" CssClass="form-control">
                </cc1:C1WebNumericEdit>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Diễn giải</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_346D63E4_2747_4DF7_81F3_4A95CAEF9E13" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-5 col-sm-offset-1 control-label">1.Danh sách Tiêu chí thành phần</label>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Tên tiêu chí thành phần</label>
            <div class="col-sm-6">
                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Tỷ trọng</label>
            <div class="col-sm-6">
                <cc1:C1WebNumericEdit ID="C1WebNumericEdit1" runat="server"
                    SkinID="C1WebNumeric" Width="10%" Font-Size="Small" DecimalPlaces="0" Culture="en-US"
                    ThousandsSeparator="false" Value="1" SmartInputMode="false" MaxValue="100" MinValue="0"
                    UpDownAlign="None" Height="34px" CssClass="form-control">
                </cc1:C1WebNumericEdit>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Diễn giải</label>
            <div class="col-sm-6">
                <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Loại tiêu chí</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="drpLoaiTC" runat="server" AutoPostBack="true" CssClass="form-control" Width="300px" OnSelectedIndexChanged="drpLoaiTC_SelectedIndexChanged">
                    <asp:ListItem Text ="" Value="" Enabled="false"/>
                    <asp:ListItem Text ="Định tính" Value="1" />
                    <asp:ListItem Text ="Định lượng" Value="0" />
                </asp:DropDownList>
            </div>
        </div>

        <asp:Panel ID ="pnTest" runat="server">
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Diễn giải</label>
        </div>
            </asp:Panel>
        
    </div>
    <script type="text/javascript">
        var _documentid;
        /*********************************************************/
        $(document).ready(function () {
            //do smt here
        });
        /*********************************************************/

        

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