<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="TieuChiChinh_Input.aspx.cs" Inherits="VPB_KTNB.Modules.Lookup.DanhMuc.TieuChiNam.TieuChiChinh_Input" %>

<%@ Register Assembly="C1.Web.C1Input.2" Namespace="C1.Web.C1Input" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">   
    <div class="form-horizontal">
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Năm</label>
            <div class="col-sm-8">
                <asp:TextBox ID="lblYear" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Loại đối tượng kiểm toán</label>
            <div class="col-sm-8">
                <asp:TextBox ID="lblLDTKT" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Bộ tiêu chí năm</label>
            <div class="col-sm-8">
                <asp:TextBox ID="txtBotieuchi" runat="server" Enabled="false" CssClass="form-control "></asp:TextBox>
                <asp:HiddenField ID="ID9_3952C713_E304_443D_9CC7_C55D51408A5F" runat="server" />
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Tên tiêu chí chính<span class="star-red">(*)</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_67E8EBEA_3C55_4EF4_9F4F_90D457DEB495" runat="server" CssClass="form-control TextBoxRequired"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Diễn giải</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_346D63E4_2747_4DF7_81F3_4A95CAEF9E13" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control" ></asp:TextBox>
            </div>
        </div>

        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Tỷ trọng</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID6_2E3ECB61_E8C2_433A_8FF9_8B19BB8204CE" MaxLength="3" runat="server" Style="float: left" Width="100" CssClass="form-control TextBoxPercent"></asp:TextBox>
                <label class="control-label col-sm-1">&nbsp;%</label>
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
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Danh sách tiêu chí thành phần</label>
            <div class="col-sm-8">
                <a href="javascript:;" onclick="return CheckFormValid();" class="btn color-green btn-xs" style="width: 172px; float: right">Thêm tiêu chí thành phần</a>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">&nbsp;</label>
            <div class="col-sm-8">
                <asp:Repeater ID="rptTCC" runat="server" OnItemDataBound="rptTCC_ItemDataBound" OnItemCommand="rptTCC_ItemCommand">
                    <HeaderTemplate>
                        <div class="table-responsive">
                            <table class="table table-striped table-bordered table-hover">
                                <thead class="color-green">
                                    <tr>
                                        <th>Tiêu chí thành phần</th>
                                        <th>Trọng số</th>
                                        <th>Trạng thái</th>
                                        <th>Thao tác</th>
                                    </tr>
                                </thead>
                                <tbody>
                    </HeaderTemplate>
                    <FooterTemplate>
                        </tbody>
                            </table>
                        </div>
                        <!-- /.table-responsive -->
                    </FooterTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("Ten") %></td>
                            <td><%# Eval("Tytrong") %>&nbsp;%</td>
                            <td>
                                <asp:Literal ID="Status" runat="server"></asp:Literal></td>
                            <td class="text-center">
                                <a class="click-icon" href="javascript:;" title="Chi tiết" onclick="LoadDocument('<%# Eval("PK_DocumentID") %>')">
                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                </a>
                                <asp:LinkButton ID="btnActive" Visible="false" ToolTip="Active" CommandName="active" CommandArgument='<%# Eval("PK_DocumentID") %>' runat="server" CssClass="click-icon-out">
                                    <span class="glyphicon glyphicon-upload" aria-hidden="true"></span>
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnInactive" Visible="false" ToolTip="Inactive" CommandName="inactive" CommandArgument='<%# Eval("PK_DocumentID") %>' runat="server" CssClass="click-icon-out">
                                    <span class="glyphicon glyphicon-download" aria-hidden="true"></span>
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
        <div class="form-group<%= isUpdate %>">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Công thức tiêu chí chính</label>
            <div class="col-sm-8">
                &nbsp;
            </div>
        </div>
        <div class="form-group<%= isUpdate %>">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Tiêu chí thành phần</label>
            <div class="col-sm-8">
                <asp:DropDownList ID="drpTCC" runat="server" CssClass="form-control" Style="width: 300px; float: left;">
                </asp:DropDownList>
                <a href="javascript:;" onclick="Add2Formula();" class="btn color-green btn-xs" style="width: 160px; margin: 0 0 0 10px !important; float: left;">Thêm vào công thức</a>
            </div>
        </div>
        <div class="form-group<%= isUpdate %>">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Trọng số</label>
            <div class="col-sm-8">
                <asp:TextBox ID="txtTytrong" runat="server" Style="float: left" Width="100" CssClass="form-control"></asp:TextBox>
                <label class="control-label col-sm-1">&nbsp;%</label>
            </div>
        </div>
        <div class="form-group<%= isUpdate %>">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Toán tử</label>
            <div class="col-sm-8">
                <div class="col-sm-2" style="padding-left: 0 !important;">
                    <a href="javascript:;" onclick="Add2FormulaWithOperator(' + ');" class="col-sm-12 btn color-green btn-xs">+</a>
                </div>
                <div class="col-sm-2" style="padding-left: 0 !important;">
                    <a href="javascript:;" onclick="Add2FormulaWithOperator(' - ');" class="col-sm-12 btn color-green btn-xs">-</a>
                </div>
                <div class="col-sm-2" style="padding-left: 0 !important;">
                    <a href="javascript:;" onclick="Add2FormulaWithOperator(' x ');" class="col-sm-12 btn color-green btn-xs">x</a>
                </div>
                <div class="col-sm-2" style="padding-left: 0 !important;">
                    <a href="javascript:;" onclick="Add2FormulaWithOperator(' : ');" class="col-sm-12 btn color-green btn-xs">:</a>
                </div>
                <div class="col-sm-2" style="padding-left: 0 !important;">
                    <a href="javascript:;" onclick="Add2FormulaWithOperator('(');" class="col-sm-12 btn color-green btn-xs">(</a>
                </div>
                <div class="col-sm-2" style="padding-left: 0 !important;">
                    <a href="javascript:;" onclick="Add2FormulaWithOperator(')');" class="col-sm-12 btn color-green btn-xs">)</a>
                </div>
            </div>
        </div>
        <div class="form-group<%= isUpdate %>">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Công thức</label>
            <div class="col-sm-8">
                <asp:HiddenField ID="ID8_D2ABD1F4_CE31_4424_93DD_C217ECA2A835" runat="server" />
                <div id="txtFormula" class="col-sm-8" style="float: left; min-height: 34px; border: 1px solid #48B061; line-height: 34px;"></div>
                <a href="javascript:;" onclick="Clear2Formula();" class="col-sm-3 btn color-green btn-xs" style="margin: 0 0 0 10px !important; float: left;">Làm lại</a>
            </div>
        </div>
    </div>
    <asp:ScriptManager ID='ScriptManager1' runat='server' EnablePageMethods='true' />
    <script type="text/javascript">
        /*********************************************************/
        var lstTTC = [];
        $(document).ready(function () {
            $('#<%= drpTCC.ClientID %>').on('change', function () {
                $("#<%= txtTytrong.ClientID %>").val(this.value == "0" ? "" : this.value);
            });
            $("#txtFormula").text($("#<%= ID8_D2ABD1F4_CE31_4424_93DD_C217ECA2A835.ClientID %>").val());
            var res, regexp = /\{TC\d\}/g;
            while (res = regexp.exec($("#txtFormula").text())) {
                lstTTC.push(eval(res[0].replace("{TC", "").replace("}", "")));
            }
            console.log(lstTTC);
        });

        function Add2Formula() {
            var index = $("#<%= drpTCC.ClientID %> option:selected").index();
            if (index > 0) {

                if ($.inArray(index, lstTTC) < 0) {
                    var _curFormula = $.trim($("#txtFormula").text());
                    var lastChar = _curFormula.slice(-1);
                    if (lastChar == '}' || lastChar == ')')
                        alert("Hãy chọn toán tử trước khi chọn tiêu chí chính!");
                    else {
                        var tytrong = $("#<%= txtTytrong.ClientID %>").val();
                        var _formula = $("#txtFormula");
                        _formula.append(tytrong + "%" + "{TC" + index + "}");
                        lstTTC.push(index);
                    }
                }
                else
                    alert("Đã có tiêu chí trong công thức!");

            }
            else
                alert("Hãy chọn tiêu chí thành phần!");
        }
        function Add2FormulaWithOperator(operator) {
            var _curFormula = $.trim($("#txtFormula").text());
            var lastChar = _curFormula.slice(-1);
            var _formula = $("#txtFormula");
            if (lastChar == '+' || lastChar == '-' || lastChar == 'x' || lastChar == ':' || lastChar == '(') {
                if (operator != '(')
                    alert("Hai toán tử không thể đặt cạnh nhau!");
                else
                    _formula.append(operator);
            }
            else if (lastChar == ')') {
                if (operator == '(')
                    alert("Hai toán tử không thể đặt cạnh nhau!");
                else
                    _formula.append(operator);
            }
            else {
                _formula.append(operator);
            }
        }

        function Clear2Formula() {
            //var formular = $("#<%= ID8_D2ABD1F4_CE31_4424_93DD_C217ECA2A835.ClientID %>").val();
            //Xoa trang du lieu
            lstTTC = [];
            $("#txtFormula").text(' ');
        }
        function CheckFormValid() {
            if ('<%= _documentid %>'.length > 0)
                addTieuchi_success();
            else {
                var tenbotieuchinam = $("#<%= ID8_67E8EBEA_3C55_4EF4_9F4F_90D457DEB495.ClientID %>").val();
                if (tenbotieuchinam.length == 0) {
                    alert("Nhập tên tiêu chí chính trước khi thêm mới tiêu chí thành phần!");
                    $("#<%= ID8_67E8EBEA_3C55_4EF4_9F4F_90D457DEB495.ClientID %>").focus();
                    return false;
                }
                else {
                    //Thêm vào library
                    var url = "TieuChiChinh_Input.aspx";
                    var query = "act=checkvalue";
                    query += "&p=67E8EBEA-3C55-4EF4-9F4F-90D457DEB495";
                    query += "&v=" + tenbotieuchinam;
                    var documentID = '<%= NewDocumentId %>';
                    var doctypeID = '<%= _doctypeid %>';
                    StartProcessingForm("");
                    $.ajax({
                        type: "POST",
                        url: url,
                        data: query,
                        success: function (data) {
                            FinishProcessingForm();
                            if (data == "0") {
                                $('#<%= DOCSTATUS.ClientID %> > option:eq(1)').prop('selected', true);
                                savedocumentwithoutconfirm(documentID, doctypeID, addTieuchi_success, savedoc_error);
                            }
                            else
                                alert("Dữ liệu nhập bị trùng lặp. Kiểm tra lại.");
                        }
                    });
                    return true;
                }
            }
        }
        /*********************************************************/
        function adddoc(documentID, doctypeID) {
            var tenbotieuchinam = $("#<%= ID8_67E8EBEA_3C55_4EF4_9F4F_90D457DEB495.ClientID %>").val();
            if (tenbotieuchinam.length == 0) {
                alert("Nhập tên tiêu chí chính");
                $("#<%= ID8_67E8EBEA_3C55_4EF4_9F4F_90D457DEB495.ClientID %>").focus();
                return false;
            }
            var url = "TieuChiChinh_Input.aspx";
            var query = "act=checkvalue";
            query += "&p=67E8EBEA-3C55-4EF4-9F4F-90D457DEB495";
            query += "&v=" + tenbotieuchinam;
            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (data) {
                    FinishProcessingForm();
                    if (data == "0") {
                        $('#<%= DOCSTATUS.ClientID %> > option:eq(1)').prop('selected', true);
                        savedocument(documentID, doctypeID, savedoc_success, savedoc_error, "Tiêu chí chính đang thêm mới không có tiêu chí thành phần. Có tiếp tục?");
                    }
                    else
                        alert("Dữ liệu nhập bị trùng lặp. Kiểm tra lại.");
                }
            });
        }

        function updatedoc(documentID) {
            var tenbotieuchinam = $("#<%= ID8_67E8EBEA_3C55_4EF4_9F4F_90D457DEB495.ClientID %>").val();
            if (tenbotieuchinam.length == 0) {
                alert("Nhập tên tiêu chí chính");
                $("#<%= ID8_67E8EBEA_3C55_4EF4_9F4F_90D457DEB495.ClientID %>").focus();
                return false;
            }
            //Check tổng số tiêu chí thành phần
            var totalTCC = $('#<%= drpTCC.ClientID %> option').length - 1;
            //Nếu không có tiêu chí thành phần
            if (totalTCC == 0) {
                if (!window.confirm("Tiêu chí chính đang thêm mới không có tiêu chí thành phần. Có tiếp tục?"))
                    return false;
                $('#<%= DOCSTATUS.ClientID %> > option:eq(1)').prop('selected', true);
            }
            //Check công thức đã đủ tiêu chí chính ?
            if (totalTCC != lstTTC.length) {
                if (!window.confirm("Công thức tiêu chí chính đang không đủ tiêu chí thành phần. Có tiếp tục?"))
                    return false;
                $('#<%= DOCSTATUS.ClientID %> > option:eq(1)').prop('selected', true);
            }
            //Check tổng trọng số của tiêu chí Active có = 100% ?
            var sumTCCpercent = 0; //parseInt($('#<%= drpTCC.ClientID %>').val());
            $("#<%= drpTCC.ClientID %> option").each(function () {
                sumTCCpercent = parseInt(sumTCCpercent) + parseInt($(this).val());
            });
            if (sumTCCpercent != 100) {
                if (!window.confirm("Tổng trọng số tiêu chí chính đang không bằng 100%. Có tiếp tục?"))
                    return false;
                $('#<%= DOCSTATUS.ClientID %> > option:eq(1)').prop('selected', true);
            }
            $("#<%= ID8_D2ABD1F4_CE31_4424_93DD_C217ECA2A835.ClientID %>").val($("#txtFormula").text());
            var url = "TieuChiChinh_Input.aspx";
            var query = "act=checkvalueupdate";
            query += "&p=67E8EBEA-3C55-4EF4-9F4F-90D457DEB495";
            query += "&v=" + tenbotieuchinam;
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
                    else
                        alert("Dữ liệu nhập bị trùng lặp. Kiểm tra lại.");
                }
            });
        }

        function LoadDocument(DocumentID) {
            var year = $("#<%= lblYear.ClientID %>").val();
            var ldtkt = encodeURI($("#<%= lblLDTKT.ClientID %>").val());
            url = "TieuChiThanhPhan_Input.aspx?a=<%= appID %>&curApp=<%= curAppID %>&tcc=<%= NewDocumentId %>&y=" + year + "&l=" + ldtkt + "&act=loaddoc&doc=" + DocumentID;
            window.location.href = url;
        }
        function addTieuchi_success() {
            //Thêm vào workspace
            var year = $("#<%= lblYear.ClientID %>").val();
            var ldtkt = encodeURI($("#<%= lblLDTKT.ClientID %>").val());
            window.location.href = "TieuChiThanhPhan_Input.aspx?a=<%= appID %>&curApp=<%= curAppID %>&tcc=<%= NewDocumentId %>&y=" + year + "&l=" + ldtkt;
        }
        function savedoc_success() {
            alert(MSG_ADD_OK);
            var botieuchi = $("#<%= ID9_3952C713_E304_443D_9CC7_C55D51408A5F.ClientID %>").val();
            window.location.href = "BoTieuChiNam_Input.aspx?a=<%= appID %>&curApp=<%= curAppID %>&act=loaddoc&doc=" + botieuchi;
        }
        function savedoc_error() {
            alert(MSG_ADD_ER);
        }
        function update_success() {
            alert(MSG_EDIT_OK);
            var botieuchi = $("#<%= ID9_3952C713_E304_443D_9CC7_C55D51408A5F.ClientID %>").val();
            window.location.href = "BoTieuChiNam_Input.aspx?a=<%= appID %>&curApp=<%= curAppID %>>&act=loaddoc&doc=" + botieuchi;
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

