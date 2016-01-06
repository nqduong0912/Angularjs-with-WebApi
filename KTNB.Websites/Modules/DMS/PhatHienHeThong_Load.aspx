<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true"
    CodeBehind="PhatHienHeThong_Load.aspx.cs" Inherits="VPB_TDHS.Modules.DMS.PhatHienHeThong_Load" %>

<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <asp:ScriptManager runat="server" ID="ScriptManager1">
    </asp:ScriptManager>
    <div id="tblTruongDoan" class="form-horizontal">
        Thêm/Sửa Phát hiện hệ thống
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Đối tượng kiểm toán</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_906AF492_F67D_4DC5_8D83_65D0B485F1B3" runat="server" CssClass="form-control" Enabled="False" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Đợt kiểm toán</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_DF049E21_08B6_4F9C_93F9_FA8376E04AD0" runat="server" CssClass="form-control" Enabled="False" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Công việc</label>
            <div class="col-sm-6">
                <asp:TextBox ID="txtCongViec" runat="server" CssClass="form-control" Enabled="False" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Phát hiện</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_E48E3FFD_FE56_4B14_AC36_2C036873E1CD" runat="server" CssClass="form-control" Enabled="False" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Mức độ</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="ID8_9AD08FC3_5CEE_41E0_9A72_73822CFB2F68" runat="server" CssClass="form-control" Enabled="False" Width="300px"></asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Chi tiết</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_CEED9D99_A89C_4127_9F9A_BD149E0BBC58" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Nguyên nhân</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_3C648C41_DA03_48AB_AAD5_143D9D959C00" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Ảnh hưởng</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_2CD4A9CF_D1AC_4675_87A3_739AF5344AD8" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Khuyến nghị</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_6A1B7B08_BE4E_400A_A6CC_EC7897A89031" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Ghi chú</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_876310DC_68CE_43E2_A040_88D169B59D34" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group" id="trAttachFile">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">File kèm</label>
            <div class="col-sm-6">
                <input type="button" value="Thêm file" onclick="OpenAttachFile();" id="btn-attachfile"
                    class="InsertButton" />
            </div>
        </div>
        <div class="form-group" id="idTrangThai">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Trạng thái</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="DOCSTATUS" runat="server" CssClass="form-control" Width="100px" Enabled="false">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group" id="trAttachFileList">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label"></label>
            <div class="col-sm-6">
                <asp:ObjectDataSource runat="server" ID="ObjectDataSource1" SelectMethod="getAttachFileList"
                    TypeName="VPB_KTNB.Helpers.DataSource">
                    <SelectParameters>
                        <asp:Parameter Name="PhatHienID" Type="string" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:UpdatePanel runat="server" ID="updatepanel1" UpdateMode="Conditional" OnLoad="updatepanel1_OnLoad">
                    <ContentTemplate>
                        <C1WebGrid:C1WebGrid runat="server" ID="dataCtrl" Width="75%" OnItemCreated="dataCtrl_OnItemCreated"
                            OnItemDataBound="dataCtrl_OnItemDataBound" GroupIndent="20" DataSourceID="ObjectDataSource1"
                            ItemStyle-Height="30px">
                            <Columns>
                                <C1WebGrid:C1TemplateColumn>
                                    <ItemTemplate>
                                        <asp:Image runat="server" ID="imgDelete" ImageUrl="~/Images/delete.gif" ToolTip="Xoá file"
                                            Style="cursor: pointer" />
                                    </ItemTemplate>
                                    <ItemStyle Width="8%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                </C1WebGrid:C1TemplateColumn>
                                <C1WebGrid:C1TemplateColumn HeaderText="Tải File">
                                    <ItemTemplate>
                                        <asp:Image runat="server" ID="ImgEdit" ImageUrl="~/Images/downArrow_default.gif" ToolTip="Tải file"
                                            Style="cursor: pointer" />
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                </C1WebGrid:C1TemplateColumn>
                                <C1WebGrid:C1TemplateColumn Visible="false">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="FileID" Text='<%# DataBinder.Eval(Container.DataItem,"FileID")%>'></asp:Label>
                                    </ItemTemplate>
                                </C1WebGrid:C1TemplateColumn>
                                <C1WebGrid:C1TemplateColumn Visible="false">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="FILENAME" Text='<%# DataBinder.Eval(Container.DataItem,"FILENAME")%>'></asp:Label>
                                    </ItemTemplate>
                                </C1WebGrid:C1TemplateColumn>
                                <C1WebGrid:C1TemplateColumn Visible="false">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="FILEPATH" Text='<%# DataBinder.Eval(Container.DataItem,"FILEPATH")%>'></asp:Label>
                                    </ItemTemplate>
                                </C1WebGrid:C1TemplateColumn>
                                <C1WebGrid:C1BoundColumn HeaderText="Tên File" DataField="DisplayFileName">
                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                </C1WebGrid:C1BoundColumn>
                                <C1WebGrid:C1BoundColumn HeaderText="Diễn giải" DataField="DESCRIPTION">
                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                </C1WebGrid:C1BoundColumn>
                            </Columns>
                        </C1WebGrid:C1WebGrid>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="form-group" id="trDaNhanXet">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Đã nhận xét</label>
            <div class="col-sm-6">
                <asp:TextBox ID="DaNhanXetNguoiDuyet1" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control" Enabled="False" Width="300px"></asp:TextBox>(người duyệt)
            </div>
        </div>
         <div class="form-group" id="trNhanXet">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Nhận xét</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_17FE4A62_A92E_4921_A9DA_C7C7110EB8B3" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control" Enabled="False" Width="300px"></asp:TextBox>(người duyệt)
            </div>
        </div>
<%--        <table width="94%">--%>
            <%-- <tr id="trDaNhanXet" runat="server" class="form-group">
                <td class="col-sm-2 col-sm-offset-1 control-label">
                    <label for="inputDescription">Đã nhận xét</label>
                </td>
                <td class="col-sm-6">
                    <div class="col-sm-6">
                        <asp:TextBox ID="DaNhanXetNguoiDuyet1" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control" Enabled="False" Width="300px"></asp:TextBox>(người duyệt)
                    </div>
                </td>

            </tr>--%>
           <%-- <tr id="trNhanXet" runat="server" class="form-group">
                <td class="col-sm-3 col-sm-offset-1 control-label">
                    <label for="inputDescription">Nhận xét</label>
                </td>
                <td class="col-sm-6">
                    <div class="col-sm-6">
                        <asp:TextBox ID="ID8_17FE4A62_A92E_4921_A9DA_C7C7110EB8B3" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control" Width="300px"></asp:TextBox>
                    </div>
                </td>
            </tr>--%>
<%--        </table>--%>
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
            <td colspan="2">Thêm/Sửa Phát hiện hệ thống
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Đối tượng kiểm toán
            </td>
            <td>
                <asp:TextBox Enabled="false" ID="ID8_906AF492_F67D_4DC5_8D83_65D0B485F1B3" runat="server"
                    SkinID="TextBoxReadOnly"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Đợt kiểm toán
            </td>
            <td>
                <asp:TextBox Enabled="false" ID="ID8_DF049E21_08B6_4F9C_93F9_FA8376E04AD0" runat="server"
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
                <asp:TextBox ID="ID8_E48E3FFD_FE56_4B14_AC36_2C036873E1CD" runat="server" SkinID="TextBoxRequired"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Mức độ
            </td>
            <td>
                <asp:DropDownList ID="ID8_9AD08FC3_5CEE_41E0_9A72_73822CFB2F68" runat="server" SkinID="DropDownList">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Chi tiết
            </td>
            <td>
                <asp:TextBox ID="ID8_CEED9D99_A89C_4127_9F9A_BD149E0BBC58" runat="server" SkinID=""></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Nguyên nhân
            </td>
            <td>
                <asp:TextBox ID="ID8_3C648C41_DA03_48AB_AAD5_143D9D959C00" runat="server" SkinID=""></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Ảnh hưởng
            </td>
            <td>
                <asp:TextBox ID="ID8_2CD4A9CF_D1AC_4675_87A3_739AF5344AD8" runat="server" SkinID=""></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Khuyến nghị
            </td>
            <td>
                <asp:TextBox ID="ID8_6A1B7B08_BE4E_400A_A6CC_EC7897A89031" runat="server" SkinID=""></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Ghi chú
            </td>
            <td>
                <asp:TextBox ID="ID8_876310DC_68CE_43E2_A040_88D169B59D34" runat="server" SkinID=""></asp:TextBox>
            </td>
        </tr>
        <tr id="trAttachFile">
            <td style="width: 222px">File kèm
            </td>
            <td>
                <input type="button" value="Thêm file" onclick="OpenAttachFile();" id="btn-attachfile"
                    class="InsertButton" />
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
        <tr id="trAttachFileList">
            <td style="width: 222px"></td>
            <td>
                <asp:ObjectDataSource runat="server" ID="ObjectDataSource1" SelectMethod="getAttachFileList"
                    TypeName="DataSource">
                    <SelectParameters>
                        <asp:Parameter Name="PhatHienID" Type="string" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:UpdatePanel runat="server" ID="updatepanel1" UpdateMode="Conditional" OnLoad="updatepanel1_OnLoad">
                    <ContentTemplate>
                        <C1WebGrid:C1WebGrid runat="server" ID="dataCtrl" Width="75%" OnItemCreated="dataCtrl_OnItemCreated"
                            OnItemDataBound="dataCtrl_OnItemDataBound" GroupIndent="20" DataSourceID="ObjectDataSource1"
                            ItemStyle-Height="30px">
                            <Columns>
                                <C1WebGrid:C1TemplateColumn>
                                    <ItemTemplate>
                                        <asp:Image runat="server" ID="imgDelete" ImageUrl="~/Images/delete.gif" ToolTip="Xoá file"
                                            Style="cursor: pointer" />
                                    </ItemTemplate>
                                    <ItemStyle Width="8%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                </C1WebGrid:C1TemplateColumn>
                                <C1WebGrid:C1TemplateColumn HeaderText="Tải File">
                                    <ItemTemplate>
                                        <asp:Image runat="server" ID="ImgEdit" ImageUrl="~/Images/downArrow_default.gif" ToolTip="Tải file"
                                            Style="cursor: pointer" />
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                </C1WebGrid:C1TemplateColumn>
                                <C1WebGrid:C1TemplateColumn Visible="false">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="FileID" Text='<%# DataBinder.Eval(Container.DataItem,"FileID")%>'></asp:Label>
                                    </ItemTemplate>
                                </C1WebGrid:C1TemplateColumn>
                                <C1WebGrid:C1TemplateColumn Visible="false">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="FILENAME" Text='<%# DataBinder.Eval(Container.DataItem,"FILENAME")%>'></asp:Label>
                                    </ItemTemplate>
                                </C1WebGrid:C1TemplateColumn>
                                <C1WebGrid:C1TemplateColumn Visible="false">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="FILEPATH" Text='<%# DataBinder.Eval(Container.DataItem,"FILEPATH")%>'></asp:Label>
                                    </ItemTemplate>
                                </C1WebGrid:C1TemplateColumn>
                                <C1WebGrid:C1BoundColumn HeaderText="Tên File" DataField="DisplayFileName">
                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                </C1WebGrid:C1BoundColumn>
                                <C1WebGrid:C1BoundColumn HeaderText="Diễn giải" DataField="DESCRIPTION">
                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                </C1WebGrid:C1BoundColumn>
                            </Columns>
                        </C1WebGrid:C1WebGrid>
                    </ContentTemplate>
                </asp:UpdatePanel>
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
                <asp:TextBox Rows="2" Columns="55" Text="" TextMode="MultiLine" ID="ID8_17FE4A62_A92E_4921_A9DA_C7C7110EB8B3"
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
        var MSG_CONFIRM_ADD_CV = "Bạn có muốn tạo công việc này không?";
        var MSG_ALERT_CHOICE_CV = "Hãy tạo một công việc.";

        var MSG_CONFIRM_ADD_TT = "Bạn có muốn đưa thủ tục kiểm toán này vào công việc này không?";
        var MSG_EXIST_TT = "Thủ tục kiểm toán này này đã tồn tại trong công việc này.";
        var MSG_ADD_TT_SUC = "Đưa thủ tục kiểm toán vào công việc thành công.";
        var MSG_CONFIRM_DEL_TT = "Bạn có muốn đưa thủ tục kiểm toán này ra khỏi công việc?";
        var MSG_CONFIRM_SUBMIT = "Bạn có muốn cập nhật?";
        var MSG_CONFIRM_REJECT = "Bạn có muốn từ chối?";

        var MSG_CANNOT_SUBMIT = "Không submit được vì chưa có thủ tục nào";
        var _documentID = Qry["doc"];

        var _documentid = "<%= _documentid %>";
        var _congviec_docid = "<%= _congviec_docid %>";
        var cv = Qry["cv"];
        var doc = Qry["doc"];
        var dotkt = Qry["dotkt"];
        var doankt = Qry["doankt"];
        var congviec_docid = Qry["congviec_docid"];


        var _status_phathien = "<%= _status_phathien %>";
        var isTruongDoan = "<%= _isTruongDoan %>";

        /*********************************************************/
        $(document).ready(function () {
            //do smt here
            if (_documentid == '') {
                $('#trAttachFile').hide();
                $('#trAttachFileList').hide();
            }
            HiddenControlTimKiem();
            $("#idTrangThai").hide();
            HiddenButtonByCongViec();
            ApplyCSS();
            ShowHideNhanXet();
            if (_status_phathien == '4' || _status_phathien == '16') {
                //$("img[id*='imgDelete']").hide();
                $("#btn-attachfile").hide();
            }
            //css
            $('input[name*="btnFINISH"]').css('width', '80px');
        });
        function ShowHideNhanXet() {
            if (cv == 'nguoiduyet') {
                $('#' + '<%=ID8_17FE4A62_A92E_4921_A9DA_C7C7110EB8B3.ClientID %>').show();
            }
            else {

                $('#' + 'trNhanXet').hide();
                $('#' + '<%=ID8_17FE4A62_A92E_4921_A9DA_C7C7110EB8B3.ClientID %>').hide();
            }

        }

        function ApplyCSS() {
            $('input[type="text"]').addClass("textbox");
        }
        function HiddenControlTimKiem() {
            var timkiem = Qry["timkiem"];
            if (timkiem == 'tk') {
                $("input[type*='button']").hide();
                $("input[type*='submit']").hide();

                if (isTruongDoan == 'True')
                    $("#ctl00_btnEDIT").show();
            }
        }

        function HiddenButtonByCongViec() {
            if (cv != 'nguoithuchien') {//nguoiduyet ko dc phep add/remove file
                $("#btn-attachfile").hide();
                $("img[id*='imgDelete']").hide();
            }
        }

        /*********************************************************/
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
        function OpenAttachFile() {
            window.location.href = 'UploadFile.aspx?doc=' + _documentid + "&congviec_docid=" + _congviec_docid + "&cv=" + cv + "&doankt=" + doankt + "&dotkt=" + dotkt;
        }
        function saveNoCheck(documentID, doctypeID) {
            _documentid = documentID;
            savedocumentwithlink(documentID, _congviec_docid, doctypeID, savedoc_success, savedoc_error, "")
        }
        function updateNoCheck(documentID) {
            updatedocument(documentID, update_success, update_error);
        }

        function capnhattrangthaidone(phathienID) {
            if (!window.confirm(MSG_CONFIRM_SUBMIT))
                return false;
            //alert(documentID);
            var url = "PhatHienHeThong_Load.aspx";
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
                    if (data == "1") {
                        alert("Chưa cho phép hoàn tất phát hiện vì các phản hồi chưa đc hoàn tất.");
                        return false;
                    }
                    else if (data == "-1") {
                        alert("Có lỗi xảy ra.Vui lòng liên hệ với quàn trị.");
                        return false;
                    }
                    else if (data == "0") {
                        window.location.href = "DanhSachPhatHien.aspx?act=loaddoc&doc=" + _congviec_docid + "&cv=" + cv + "&doankt=" + doankt + "&dotkt=" + dotkt;
                    }

                }
            });
        }


        function capnhattrangthaipheduyet(phathienID) {
            if (!window.confirm(MSG_CONFIRM_SUBMIT))
                return false;
            var url = "PhatHienHeThong_Load.aspx";
            var query = "act=capnhattrangthaipheduyet";
            query += "&phathienID=" + phathienID + "&cv=" + cv + "&congviec_docid=" + _congviec_docid;
            query += "&doankt=" + doankt;
            query += "&dotkt=" + dotkt;
            var nhanXet = GetSvrCtlValue("ID8_17FE4A62_A92E_4921_A9DA_C7C7110EB8B3");
            nhanXet = nhanXet.trim();
            query += "&nhanxet=" + nhanXet;
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

        function capnhattrangthaituchoi(phathienID) {
            if (!window.confirm(MSG_CONFIRM_REJECT))
                return false;
            var url = "PhatHienHeThong_Load.aspx";
            var query = "act=capnhattrangthaituchoi";
            query += "&phathienID=" + phathienID + "&cv=" + cv + "&congviec_docid=" + _congviec_docid;
            query += "&doankt=" + doankt;
            query += "&dotkt=" + dotkt;
            var nhanXet = GetSvrCtlValue("ID8_17FE4A62_A92E_4921_A9DA_C7C7110EB8B3");
            query += "&nhanxet=" + nhanXet;
            nhanXet = nhanXet.trim();
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
            //thangma cap nhat trang thai dot kiem toan
            //chuyen trang thai dot kiem toan len trang thai Cap nhat phat hien
            var url = "PhatHienHeThong_Load.aspx";
            var query = "act=chuyentrangthaidotkt";
            var dotkt = Qry["dotkt"];
            query += "&dotkt=" + dotkt;
            //            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (ErrorMessage) {
                    var url = "PhatHienHeThong_Load.aspx?doc=" + _documentid + "&act=loaddoc&congviec_docid=" + _congviec_docid + "&cv=" + cv + "&doankt=" + doankt + "&dotkt=" + dotkt;
                    window.location.href = url;
                }
            });
            //end

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
            window.location.href = 'PhatHienHeThong_Load.aspx?act=loaddoc&doc=' + _congviec_docid + "&cv=" + cv + "&doankt=" + doankt + "&dotkt=" + dotkt;
        }
        function delete_error() {
            alert(MSG_DEL_ER);
        }
        function DeleteFile(FileID) {
            if (!window.confirm("Bạn có muốn xóa file?"))
                return false;

            var url = "PhatHienHeThong_Load.aspx";
            var query = "act=deletefile";
            query += "&v=" + FileID;
            query += "&doankt=" + doankt;
            query += "&dotkt=" + dotkt;
            query += "&cv=" + cv;
            query += "&cv=" + _congviec_docid;
            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (data) {
                    FinishProcessingForm();
                    alert("Xóa file thành công.");
                    __doPostBack('<%=updatepanel1.ClientID %>', '');
                    //delete_success();
                }
            });
        }
        function download(filename, filepath) {
            var rootFile = new String("<%=_rootFile %>");
            var http_body = "<%=_http_body %>";
            var fpath = new String(filepath);
            fpath = fpath.toLowerCase().replace(rootFile.toLowerCase(), "");
            var url = http_body + fpath + "/" + filename;
            opendetail(url, "loadfile_phathien");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ButtonExtendBefore" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ButtonExtend" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FormFooter" runat="server">
</asp:Content>
