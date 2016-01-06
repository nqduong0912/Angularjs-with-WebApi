<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true"
    CodeBehind="LapHoSoPhanTichSoBo.aspx.cs" Inherits="VPB_TDHS.Modules.DMS.LapHoSoPhanTichSoBo" %>

<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <asp:ScriptManager runat="server" ID="ScriptManager1">
    </asp:ScriptManager>

    <asp:ObjectDataSource runat="server" ID="ObjectDataSource1" SelectMethod="getListHoSoPhanTichSoBo"
        TypeName="VPB_KTNB.Helpers.DataSource">
        <SelectParameters>
            <asp:Parameter Name="NhomKiemToan" Type="string" />
        </SelectParameters>
    </asp:ObjectDataSource>

    <div class="form-horizontal">
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Đối tượng kiểm toán</label>
            <div class="col-sm-6">
                <asp:TextBox ID="DOCSTATUS" Text="2" Style="display: none;" runat="server"></asp:TextBox>
                <fieldset disabled>
                    <asp:TextBox ID="txtDoiTuongKiemToan" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                </fieldset>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Đợt kiểm toán</label>
            <div class="col-sm-6">
                <fieldset disabled>
                    <asp:TextBox ID="txtDotKiemToan" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                </fieldset>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Mảng nghiệp vụ</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="ID8_AC4286FB_5B38_47C4_93F0_E075F171D8FC" runat="server" CssClass="form-control" Width="300px">
                    </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Vấn đề quan tâm<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_CBA39743_AA0E_4965_9201_BE3BB5CF710A" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Mức độ rủi ro</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="ID8_05B1B244_C5E3_4199_B523_96A3E131D83D" runat="server" CssClass="form-control" Width="300px">
                    </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label"></label>
            <div class="col-sm-6">
                <asp:Button runat="server" Text="Thêm" ID="btnThemVDQuanTam" class="InsertButton form-control"/>
            </div>
        </div>
    </div>
<%--    <table width="100%">
        <tr>
            <td style="width: 222px">Đối tượng kiểm toán
            </td>
            <td>
                <asp:TextBox ID="DOCSTATUS" Text="2" Style="display: none;" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtDoiTuongKiemToan" runat="server" SkinID="TextBoxReadOnly"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Đợt kiểm toán
            </td>
            <td>
                <asp:TextBox ID="txtDotKiemToan" runat="server" SkinID="TextBoxReadOnly"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Mảng nghiệp vụ
            </td>
            <td>
                <asp:DropDownList ID="ID8_AC4286FB_5B38_47C4_93F0_E075F171D8FC" runat="server" SkinID="DropDownList">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Vấn đề quan tâm
            </td>
            <td>
                <asp:TextBox ID="ID8_CBA39743_AA0E_4965_9201_BE3BB5CF710A" runat="server" SkinID="TextBoxRequired"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Mức độ rủi ro
            </td>
            <td>
                <asp:DropDownList ID="ID8_05B1B244_C5E3_4199_B523_96A3E131D83D" runat="server" SkinID="DropDownList">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 222px"></td>
            <td>
                <asp:Button runat="server" Text="Thêm" ID="btnThemVDQuanTam" class="InsertButton" />
            </td>
        </tr>
    </table>--%>
    <table width="100%">
        <tr>
            <td>
                <asp:UpdatePanel runat="server" ID="updatepanel1" UpdateMode="Conditional" OnLoad="updatepanel1_OnLoad">
                    <ContentTemplate>
                        <%--<asp:HiddenField ID="hiddenNhomKT" runat="server" />--%>
                        <C1WebGrid:C1WebGrid runat="server" ID="dataCtrl" Width="100%" OnItemCreated="dataCtrl_OnItemCreated"
                            OnItemDataBound="dataCtrl_OnItemDataBound" GroupIndent="20" ItemStyle-Height="30px" DataSourceID="ObjectDataSource1">
                            <Columns>
                                <C1WebGrid:C1TemplateColumn>
                                    <ItemTemplate>
                                        <asp:Image runat="server" ID="imgDelete" ImageUrl="~/Images/delete.gif" ToolTip="Xoá vấn đề quan tâm"
                                            Style="cursor: pointer" />
                                    </ItemTemplate>
                                    <ItemStyle Width="8%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                </C1WebGrid:C1TemplateColumn>
                                <C1WebGrid:C1TemplateColumn Visible="false">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="hoso" Text='<%# DataBinder.Eval(Container.DataItem,"hoso")%>'></asp:Label>
                                    </ItemTemplate>
                                </C1WebGrid:C1TemplateColumn>
                                <C1WebGrid:C1BoundColumn HeaderText="Mảng nghiệp vụ" DataField="mang_nghiep_vu">
                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                </C1WebGrid:C1BoundColumn>
                                <C1WebGrid:C1BoundColumn HeaderText="Vấn đề quan tâm" DataField="van_de_quan_tam">
                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                </C1WebGrid:C1BoundColumn>
                                <C1WebGrid:C1BoundColumn HeaderText="Mức độ rủi ro" DataField="muc_do_rui_ro">
                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                </C1WebGrid:C1BoundColumn>
                                <C1WebGrid:C1TemplateColumn HeaderText="Submitted">
                                    <ItemTemplate>
                                        <asp:Image runat="server" ID="imgSubmit" ImageUrl="~/Images/check.gif" ToolTip="Đã Submit" />
                                    </ItemTemplate>
                                    <ItemStyle Width="8%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                </C1WebGrid:C1TemplateColumn>
                            </Columns>
                        </C1WebGrid:C1WebGrid>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <%--<input id="btn-submit" type="button" value="Submit" onclick="Submit()" class="InsertButton" />--%>
    <script type="text/javascript">
        var _documentid;
        var MSG_CONFIRM_DEL_HOSOSOBO = "Bạn có muốn xóa phân tích này?";
        var MSG_CONFIRM_SUBMIT_HOSOSOBO = "Bạn có muốn submit toàn bộ việc phân tích sơ bộ?";


        var clientIDbtnSave = '<%=_btnSave.ClientID %>';
        /*********************************************************/
        $(document).ready(function () {
            //do smt here
        });
        /*********************************************************/
        function ThemVDQuanTam(doctypeID) {
            var _chitietHoSo = NewGuid();
            var nhomkt = '<%=_nhomkt%>';
            savedocumentwithlink(_chitietHoSo, nhomkt, doctypeID, savedoc_success, savedoc_error, "Bạn có chắc chắn muốn thêm mới vấn đề quan tâm?")
        }
        function Submit() {
            if (!window.confirm(MSG_CONFIRM_SUBMIT_HOSOSOBO))
                return false;
            var url = "LapHoSoPhanTichSoBo.aspx";
            var query = "act=submit";
            var nhomkt = '<%=_nhomkt%>';
            query += "&doc=" + nhomkt;
            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (ErrorMessage) {
                    FinishProcessingForm();
                    var url = "LapHoSoPhanTichSoBo.aspx?act=loaddoc&doc=" + Qry["doc"];
                    window.location.href = url;
                    //                    if (ErrorMessage == "") {
                    //                        update_success();
                    //                    }
                    //                    else
                    //                        alert("Error");
                }
            });
        }
        function newform() {
            //            window.location.href = "ChiTietHoSoPhanTichSoBo_Load.aspx";
        }
        function savedoc_success() {
            //alert(MSG_ADD_OK);
            $('#' + '<%=ID8_CBA39743_AA0E_4965_9201_BE3BB5CF710A.ClientID %>').val('');

            __doPostBack('<%=updatepanel1.ClientID %>', '');
            //chuyen trang thai dot kiem toan len trang thai Phan tich so bo
            var url = "LapHoSoPhanTichSoBo.aspx";
            var query = "act=chuyentrangthaidotkt";
            var nhomkt = '<%=_nhomkt%>';
            query += "&doc=" + nhomkt;
            //            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (ErrorMessage) {
                    //                    FinishProcessingForm();
                    //                    var url = "LapHoSoPhanTichSoBo.aspx?act=loaddoc&doc=" + Qry["doc"];
                    //                    window.location.href = url;
                    //                    if (ErrorMessage == "") {
                    //                        update_success();
                    //                    }
                    //                    else
                    //                        alert("Error");
                }
            });
        }
        function savedoc_error() {
            alert(MSG_ADD_ER);
        }
        function update_success() {
            alert(MSG_EDIT_OK);
            //        window.location.href = 'LoaiDoiTuongKiemToan.aspx';
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
        function deleteChiTietPhanTich(hosoChiTiet) {
            if (!window.confirm(MSG_CONFIRM_DEL_HOSOSOBO))
                return false;
            var nhomkt = '<%=_nhomkt%>';
            var url = "LapHoSoPhanTichSoBo.aspx";
            var query = "act=xoachitietphantich";
            query += "&doc=" + nhomkt;
            query += "&hosochitiet=" + hosoChiTiet;
            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (errormessage) {
                    FinishProcessingForm();
                    if (errormessage == "") {
                        window.location.reload(true);
                    }
                    else
                        alert(errormessage);
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
