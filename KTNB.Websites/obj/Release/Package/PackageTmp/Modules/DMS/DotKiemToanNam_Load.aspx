<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="DotKiemToanNam_Load.aspx.cs" Inherits="VPB_TDHS.Modules.DMS.DotKiemToanNam_Load" %>

<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <div class="form-horizontal">
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Năm</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="ID8_23FB0D9B_6A02_44B0_A1AA_CEFA895CD9C1" runat="server" CssClass="form-control" Width="100px">
                    </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Tên đợt kiểm toán<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_63A0C4B1_2088_4994_B891_2FF65EB20265" runat="server" CssClass="form-control" ></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Loại đối tượng kiểm toán</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="ID8_737694DF_DE17_4FF9_AE15_70E197C83593" runat="server" CssClass="form-control" Width="300px">
                    </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Đối tượng kiểm toán</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="ID8_2A4CA2AD_0282_4D57_86AC_D973D281EF54" runat="server" CssClass="form-control" Width="300px">
                    </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Đơn vị thực hiện</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="ID8_A8A3EDBA_F569_4C06_8A57_045FBFED55FD" runat="server" CssClass="form-control" Width="300px">
                    </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Quy mô đợt kiểm toán</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="ID8_06991CFA_158B_4460_8B7E_EC010C14DFE4" runat="server" CssClass="form-control" Width="300px">
                    </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Thời gian thực hiện</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="ID8_B23A439D_95D3_466E_8AD1_CDDAC260CB27" runat="server" CssClass="form-control" Width="150px">
                    </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Mục tiêu<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_7D31CEB6_69CC_4F8E_9BE6_38762D7C30C6" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Phạm vi<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_46C1BE6C_68F3_47B9_B0FB_DEC78831AEFF" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group" id="trTrangThai" visible="false" runat="server">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Trạng thái đợt kiểm toán</label>
            <div class="col-sm-6" style="height: 34px; line-height: 34px">
                <asp:Label ID="lblTrangThai" runat="server" Width="80%" ></asp:Label>
            </div>
        </div>   
    </div>
    <%--<table width="100%">

        <%-- <tr class="GridHeader2">
            <td colspan="2">thông tin kế hoạch năm</td>
        </tr>--%>

       <%-- <tr>
            <td style="width: 222px">Năm</td>
            <td>
                <asp:DropDownList ID="ID8_23FB0D9B_6A02_44B0_A1AA_CEFA895CD9C1" runat="server"
                    SkinID="DropDownListRequired" Width="10%">
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td style="width: 222px">Tên đợt kiểm toán</td>
            <td>
                <asp:TextBox ID="ID8_63A0C4B1_2088_4994_B891_2FF65EB20265" runat="server"
                    SkinID="TextBoxRequired" Width="80%"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td style="width: 222px">Loại đối tượng kiểm toán</td>
            <td>
                <asp:DropDownList ID="ID8_737694DF_DE17_4FF9_AE15_70E197C83593" runat="server"
                    SkinID="DropDownList" Width="20%">
                </asp:DropDownList>
            </td>
        </tr>


        <tr>
            <td style="width: 222px">Đối tượng kiểm toán</td>
            <td>
                <asp:DropDownList ID="ID8_2A4CA2AD_0282_4D57_86AC_D973D281EF54" runat="server"
                    SkinID="DropDownList" Width="20%">
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td style="width: 222px">Đơn vị thực hiện</td>
            <td>
                <asp:DropDownList ID="ID8_A8A3EDBA_F569_4C06_8A57_045FBFED55FD" runat="server"
                    SkinID="DropDownList" Width="20%">
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td style="width: 222px">Quy mô đợt kiểm toán</td>
            <td>
                <asp:DropDownList ID="ID8_06991CFA_158B_4460_8B7E_EC010C14DFE4" runat="server"
                    SkinID="DropDownList" Width="20%">
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td style="width: 222px">Thời gian thực hiện</td>
            <td>
                <asp:DropDownList ID="ID8_B23A439D_95D3_466E_8AD1_CDDAC260CB27" runat="server"
                    SkinID="DropDownList" Style="width: 10%">
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td style="width: 222px">Mục tiêu</td>
            <td>
                <asp:TextBox ID="ID8_7D31CEB6_69CC_4F8E_9BE6_38762D7C30C6" runat="server"
                    SkinID="TextBoxRequired" Rows="4" TextMode="MultiLine" Width="80%"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td style="width: 222px">Phạm vi</td>
            <td>
                <asp:TextBox ID="ID8_46C1BE6C_68F3_47B9_B0FB_DEC78831AEFF" runat="server"
                    SkinID="TextBoxRequired" Rows="2" TextMode="MultiLine" Width="80%"></asp:TextBox>
            </td>
        </tr>

        <tr runat="server" id="trTrangThai" visible="false">
            <td style="width: 222px">Trạng thái đợt kiểm toán</td>
            <td>
                <asp:Label ID="lblTrangThai" runat="server" Width="80%"></asp:Label>
            </td>
        </tr>

    </table>--%>
    <asp:ScriptManager runat="server" ID="ScriptManager1"></asp:ScriptManager>
    <table id="tbAttachFileList" width="100%" runat="server">
        <tr id="trAttachFile">
            <td style="width: 222px"></td>
            <td>
                <%--<input type="button" value="Thêm file" onclick="OpenAttachFile();" id="btn-attachfile" />--%>
                <asp:Button runat="server" Text="Thêm file" CssClass="InsertButton" ID="btnFile" OnClientClick="javascript:OpenAttachFile(); return false;" Visible="false" />
            </td>
        </tr>

        <tr>
            <td style="width: 222px; vertical-align: top">File đính kèm
            </td>
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
                            OnItemDataBound="dataCtrl_OnItemDataBound" PageSize="10" DataSourceID="ObjectDataSource1"
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

                                <C1WebGrid:C1BoundColumn HeaderText="Diễn giải" DataField="Description">
                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                </C1WebGrid:C1BoundColumn>

                            </Columns>
                        </C1WebGrid:C1WebGrid>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>


    <script type="text/javascript">
        var _documentid;
        _documentid = Qry["doc"];
        /*********************************************************/
        $(document).ready(function () {
            //do smt here
            $("#ctl00_FormContent_ID8_737694DF_DE17_4FF9_AE15_70E197C83593").change(function () {
                var value = $(this).val();
                BuildDDLDoiTuongKT(value);
            }).change();
            HiddenControlTimKiem();
        });

        function HiddenControlTimKiem() {
            var timkiem = Qry["timkiem"];
            if (timkiem == 'tk') {
                $("input[type$='button']").hide();
                $("input[type$='submit']").hide();
                //$("img[id*='imgDelete']").hide();
            }
        }

        /*********************************************************/
        /*Insert 2 table
            + T_TYPE_DOC_PROPERTY
            + T_DOCLINK
        */
        function preparesavedoc(documentID, doctypeID) {
            var phamvi = GetSvrCtlValue("ID8_46C1BE6C_68F3_47B9_B0FB_DEC78831AEFF");
            var muctieu = GetSvrCtlValue("ID8_7D31CEB6_69CC_4F8E_9BE6_38762D7C30C6");
            if (muctieu.length > 500) {
                alert("Mục tiêu không cho phép nhập quá 500 ký tự");
                return false;
            }

            if (phamvi.length > 500) {
                alert("Phạm vi không cho phép nhập quá 500 ký tự");
                return false;
            }


            var ten = GetSvrCtlValue("ID8_63A0C4B1_2088_4994_B891_2FF65EB20265");
            var url = "DotKiemToanNam_Load.aspx";
            var query = "act=checkvalue";
            query += "&p=63A0C4B1-2088-4994-B891-2FF65EB20265";
            query += "&v=" + ten;
            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (data) {
                    FinishProcessingForm();
                    if (data == "0") {
                        //alert("documentID" + documentID);
                        savedocument(documentID, doctypeID, savedoc_success, savedoc_error, null);
                        //savedocument(documentID, doctypeID, savedoc_success, savedoc_error);
                        //window.location.reload(true);
                    }
                    else {
                        alert(MSG_DATA_ESXIT);
                    }
                }
            });
        }

        function prepareupdatedoc(documentID) {
            var phamvi = GetSvrCtlValue("ID8_46C1BE6C_68F3_47B9_B0FB_DEC78831AEFF");
            var muctieu = GetSvrCtlValue("ID8_7D31CEB6_69CC_4F8E_9BE6_38762D7C30C6");
            if (muctieu.length > 500) {
                alert("Mục tiêu không cho phép nhập quá 500 ký tự");
                return false;
            }

            if (phamvi.length > 500) {
                alert("Phạm vi không cho phép nhập quá 500 ký tự");
                return false;
            }

            var ten = GetSvrCtlValue("ID8_63A0C4B1_2088_4994_B891_2FF65EB20265");
            var url = "DotKiemToanNam_Load.aspx";
            var query = "act=checkvalueupdate";
            query += "&p=63A0C4B1-2088-4994-B891-2FF65EB20265";
            query += "&v=" + ten;
            query += "&doc=" + documentID;
            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (data) {
                    FinishProcessingForm();
                    if (data == "0") {
                        updatedocument(documentID, update_success, update_error);
                    }
                    else {
                        alert(MSG_DATA_ESXIT);
                    }
                }
            });
        }


        function BuildDDLDoiTuongKT(value) {
            var url = "DotKiemToanNam_Load.aspx";
            var query = "act=BuildDDLDoiTuongKT";
            query += "&value=" + value;
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (data) {

                    var obj = $("#ctl00_FormContent_ID8_2A4CA2AD_0282_4D57_86AC_D973D281EF54");
                    obj.empty();
                    var str;
                    var a = data.split("||");
                    var str = '';
                    for (var i = 0; i < a.length; i++) {
                        if (a[i].trim().length > 0) {
                            obj.append('<option selected="selected" value="' + a[i] + '">' + a[i] + '</option>');
                        }
                    }
                }
            });
        }

        function OpenAttachFile() {
            _documentid = Qry["doc"];
            window.location.href = 'UploadFile.aspx?doc=' + _documentid + "&act=attachfile";
        }

        function download(filename, filepath) {
            var rootFile = new String("<%=_rootFile %>");
        var http_body = "<%=_http_body %>";
        var fpath = new String(filepath);
        fpath = fpath.toLowerCase().replace(rootFile.toLowerCase(), "");
        var url = http_body + fpath + "/" + filename;
        opendetail(url, "loadfile_phathien");
    }

    function DeleteFile(FileID) {
        if (!window.confirm("Bạn có muốn xóa file?"))
            return false;

        var url = "DotKiemToanNam_Load.aspx";
        var query = "act=deletefile";
        query += "&v=" + FileID;
        query += "&doc=" + _documentid;
        StartProcessingForm("");
        $.ajax({
            type: "POST",
            url: url,
            data: query,
            success: function (data) {
                FinishProcessingForm();
                delete_success();
                __doPostBack('<%=updatepanel1.ClientID %>', '');
            }
        });
    }

    function savedoc_success() {
        alert(MSG_ADD_OK);
        window.location.href = "DotKiemToanNam.aspx";
    }
    function savedoc_error() {
        alert(MSG_ADD_ER);
    }
    function update_success() {
        alert(MSG_EDIT_OK);
        window.location.href = "DotKiemToanNam.aspx";
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
