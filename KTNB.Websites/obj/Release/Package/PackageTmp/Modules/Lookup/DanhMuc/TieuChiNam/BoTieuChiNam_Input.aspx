<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="BoTieuChiNam_Input.aspx.cs" Inherits="VPB_KTNB.Modules.Lookup.DanhMuc.TieuChiNam.BoTieuChiNam_Input" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <div class="form-horizontal">   
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Năm</label>
            <div class="col-sm-8">
                <asp:DropDownList ID="drpYears" runat="server" CssClass="form-control" Width="100px">
                    <asp:ListItem Text ="Active" Value="4" />
                    <asp:ListItem Text ="Inactive" Value="2" />
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Loại đối tượng kiểm toán</label>
            <div class="col-sm-8">                
                <asp:DropDownList ID="drpLoaidoituongkiemtoan" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>
        </div>    
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Tên bộ tiêu chí năm<span class="star-red">(*)</span></label>
            <div class="col-sm-8">
                <asp:TextBox ID="ID8_078EF917_FC13_41CC_88CB_EFC5B8C47773" runat="server" CssClass="form-control TextBoxRequired"></asp:TextBox>
            </div>
        </div>
        
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Diễn giải</label>
            <div class="col-sm-8">
                <asp:TextBox ID="ID8_82190182_4CF8_4A2C_A080_E5E942254A6C" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Trạng thái<span class="star-red"></span></label>
            <div class="col-sm-8">
                <asp:DropDownList ID="DOCSTATUS" runat="server" CssClass="form-control" Width="100px">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Danh sách tiêu chí chính</label>
            <div class="col-sm-8">
                <a href="javascript:;" onclick="return CheckFormValid();" class="btn color-green btn-xs" style="width:152px;float:right">Thêm tiêu chí chính</a>
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
                                    <th>Tiêu chí chính</th>
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
                            <td><asp:Literal ID="Status" runat="server"></asp:Literal></td>                           
                            <td class="text-center">
                                <a class="click-icon" href="javascript:;" title="Chi tiết" onclick="LoadDocument('<%# Eval("PK_DocumentID") %>')">
                                    <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                </a>
                                <asp:LinkButton CssClass="click-icon-out" ID="btnInactive" ToolTip="Inactive" Visible="false" CommandName="inactive" CommandArgument='<%# Eval("PK_DocumentID") %>' runat="server">
                                    <span class="glyphicon glyphicon-download" aria-hidden="true"></span>
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>                   
            </div>            
        </div>
            <div class="form-group<%= isUpdate %>">
                <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Công thức bộ tiêu chí</label>
                <div class="col-sm-8">
                    &nbsp;
                </div>
            </div>
            <div class="form-group<%= isUpdate %>">
                <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Tiêu chí chính</label>
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
                    <asp:HiddenField ID="ID8_DC218153_9C9E_4907_B291_D8540F9DD909" runat="server" />
                    <div id="txtFormula" class="col-sm-8"  style="float: left;min-height: 34px; border: 1px solid #48B061;line-height: 34px;">zxcv zc vz czxcvz cxvzxcv xcv zcxvzc vzcx vzc vzvcz z z zcv zcvzcv zcvzcvzc vzcvz vc</div>
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
            $("#txtFormula").text($("#<%= ID8_DC218153_9C9E_4907_B291_D8540F9DD909.ClientID %>").val());
            var res, regexp = /\{TC\d\}/g;
            while (res = regexp.exec($("#txtFormula").text())) {
                lstTTC.push(eval(res[0].replace("{TC", "").replace("}", "")));
            }
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
                alert("Hãy chọn tiêu chí chính!");
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
            else if (lastChar == ')')
            {
                if (operator == '(')
                    alert("Hai toán tử không thể đặt cạnh nhau!");
                else
                    _formula.append(operator);
            }
            else {                
                _formula.append(operator);
            }
        }        
        
        function Clear2Formula()
        {
            //var formular = $("#<%= ID8_DC218153_9C9E_4907_B291_D8540F9DD909.ClientID %>").val();
            //Xoa trang du lieu
            lstTTC = [];
            $("#txtFormula").text(' ');
        }
        function CheckFormValid() {
            if ('<%= _documentid %>'.length > 0)
                addTieuchi_success();
            else {
                var tenbotieuchinam = $("#<%= ID8_078EF917_FC13_41CC_88CB_EFC5B8C47773.ClientID %>").val();
                if (tenbotieuchinam.length == 0) {
                    alert("Nhập tên bộ tiêu chí trước khi thêm mới tiêu chí chính!");
                    $("#<%= ID8_078EF917_FC13_41CC_88CB_EFC5B8C47773.ClientID %>").focus();
                    return false;
                }
                else {
                    //Thêm vào library
                    var url = "BoTieuChiNam_Input.aspx";
                    var query = "act=checkvalue";
                    query += "&p=078EF917-FC13-41CC-88CB-EFC5B8C47773";
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
            var tenbotieuchinam = $("#<%= ID8_078EF917_FC13_41CC_88CB_EFC5B8C47773.ClientID %>").val();
            if (tenbotieuchinam.length == 0) {
                alert("Nhập tên bộ tiêu chí trước khi thêm mới tiêu chí chính!");
                $("#<%= ID8_078EF917_FC13_41CC_88CB_EFC5B8C47773.ClientID %>").focus();
                    return false;
                }
            var url = "BoTieuChiNam_Input.aspx";
            var query = "act=checkvalue";
            query += "&p=078EF917-FC13-41CC-88CB-EFC5B8C47773";
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
                        savedocument(documentID, doctypeID, savedoc_success, savedoc_error, "Bộ tiêu chí đang thêm mới không có tiêu chí chính. Có tiếp tục?");
                    }
                    else
                        alert("Dữ liệu nhập bị trùng lặp. Kiểm tra lại.");
                }
            });
        }

        function updatedoc(documentID) {
            var tenbotieuchinam = GetSvrCtlValue("ID8_078EF917_FC13_41CC_88CB_EFC5B8C47773");
            if (tenbotieuchinam.length == 0) {
                alert("Nhập tên bộ tiêu chí.");
                FocusSvrCtl("ID8_078EF917_FC13_41CC_88CB_EFC5B8C47773");
                return false;
            }
            //Check tổng số tiêu chí chính
            var totalTCC = $('#<%= drpTCC.ClientID %> option').length - 1;
            //Nếu không có tiêu chí chính
            if (totalTCC == 0)
            {
                if (!window.confirm("Bộ tiêu chí đang thêm mới không có tiêu chí chính. Có tiếp tục?"))
                    return false;
                $('#<%= DOCSTATUS.ClientID %> > option:eq(1)').prop('selected', true); 
            }
            //Check công thức đã đủ tiêu chí chính ?
            if (totalTCC != lstTTC.length)
            {
                if (!window.confirm("Công thức bộ tiêu chí đang không đủ tiêu chí chính. Có tiếp tục?"))
                    return false;
                $('#<%= DOCSTATUS.ClientID %> > option:eq(1)').prop('selected', true); 
            }
            //Check tổng trọng số của tiêu chí Active có = 100% ?
            var sumTCCpercent = 0;
            $("#<%= drpTCC.ClientID %> option").each(function () {
                sumTCCpercent = parseInt(sumTCCpercent) + parseInt($(this).val());
            });
            if (sumTCCpercent != 100)
            {
                if (!window.confirm("Tổng trọng số bộ tiêu chí đang không bằng 100%. Có tiếp tục?"))
                    return false;
                $('#<%= DOCSTATUS.ClientID %> > option:eq(1)').prop('selected', true); 
            }
            $("#<%= ID8_DC218153_9C9E_4907_B291_D8540F9DD909.ClientID %>").val($("#txtFormula").text());
            var url = "BoTieuChiNam_Input.aspx";
            var query = "act=checkvalueupdate";
            query += "&p=078EF917-FC13-41CC-88CB-EFC5B8C47773";
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
            var year = $("#<%= drpYears.ClientID %>").val();
            var ldtkt = encodeURI($("#<%= drpLoaidoituongkiemtoan.ClientID %> option:selected").text());
            url = "TieuChiChinh_Input.aspx?a=<%= appID %>&curApp=<%= curAppID %>&y=" + year + "&l=" + ldtkt + "&act=loaddoc&doc=" + DocumentID;
            window.location.href = url;
        }
        function addTieuchi_2Workspace(callbackfunction_onsuccess)
        {
            var year = $("#<%= drpYears.ClientID %>").val();
            var ldtkt = encodeURI($("#<%= drpLoaidoituongkiemtoan.ClientID %> option:selected").val());
            post_url = "savedocworkspace.do";
            post_data = ParsingForm();
            post_data += "&year=" + year;
            post_data += "&ldtkt=" + ldtkt;
            post_data += "&btc=<%= NewDocumentId %>";
            post_data = post_data.replace("undefined", "");
            while (post_data.lastIndexOf(pre_id_formcontrol) != -1)
                post_data = post_data.replace(pre_id_formcontrol, "");
            //Check xem nếu bộ tiêu chí Active thì Insert/Update vào WorkSpace
            post_data += "&act=IOU_Botieuchi";
            $.ajax({
                type: "POST",
                url: post_url,
                data: post_data,
                success: function (msg) {
                    var ErrorMessage = new String(msg);
                    if (ErrorMessage == "") {
                        if (callbackfunction_onsuccess != "") {
                            var f = callbackfunction_onsuccess;
                            f();
                        }
                    }
                    else {
                        update_error();
                    }
                }
            });
        }
        function addTieuchi_success()
        {
            addTieuchi_2Workspace(addTieuchi_finish);            
        }
        function addTieuchi_finish()
        {
            var year = $("#<%= drpYears.ClientID %>").val();
            var ldtkt = encodeURI($("#<%= drpLoaidoituongkiemtoan.ClientID %> option:selected").text());
            window.location.href = "TieuChiChinh_Input.aspx?a=<%= appID %>&curApp=<%= curAppID %>&btc=<%= NewDocumentId %>&y=" + year + "&l=" + ldtkt;
        }
        function savedoc_success() {
            addTieuchi_2Workspace(savedoc_finish);
        }
        function savedoc_finish() {
            alert(MSG_ADD_OK);
            window.location.href = "/modules/Processes/Kehoachnam/ThietLapBoTieuChi.aspx?a=0c2a2496-b6d9-492e-9fb2-a29537f7d32f&curApp=0c2a2496-b6d9-492e-9fb2-a29537f7d32f";
        }
        function savedoc_error() {
            alert(MSG_ADD_ER);
        }
        function update_success() {
            alert(MSG_EDIT_OK);
            window.location.href = "/modules/Processes/Kehoachnam/ThietLapBoTieuChi.aspx?a=0c2a2496-b6d9-492e-9fb2-a29537f7d32f&curApp=0c2a2496-b6d9-492e-9fb2-a29537f7d32f";
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
