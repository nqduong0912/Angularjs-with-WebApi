<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="TieuChiThanhPhan_Input.aspx.cs" Inherits="VPB_KTNB.Modules.Lookup.DanhMuc.TieuChiNam.TieuChiThanhPhan_Input" %>
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
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Bộ tiêu chí</label>
            <div class="col-sm-8">
                <asp:TextBox ID="txtBotieuchi" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                <asp:HiddenField ID="ID9_26C864F5_7ED3_44ED_AB0B_3C99EF198EFB" runat="server" />
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Tiêu chí chính</label>
            <div class="col-sm-8">                
                <asp:TextBox ID="txtTieuchichinh" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                <asp:HiddenField ID="ID9_2B01A211_9DDA_4F5F_A252_0751ED63D2B3" runat="server" />
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Tiêu chí thành phần<span class="star-red">*</span></label>
            <div class="col-sm-8">
                <asp:TextBox ID="ID8_23FDFFE4_8F68_437E_B777_7DC2B88B9EB6" runat="server" CssClass="form-control TextBoxRequired"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Diễn giải</label>
            <div class="col-sm-8">
                <asp:TextBox ID="ID8_3031A7F5_9793_45AB_897E_AC05C60B299A" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Tỷ trọng</label>
            <div class="col-sm-8">
                <asp:TextBox ID="ID6_F5D740A0_342D_40D4_96EA_74C5DB35AE46" runat="server" MaxLength="3" style="float:left" Width="100" CssClass="form-control TextBoxPercent"></asp:TextBox>
                <label class="control-label col-sm-1">&nbsp;%</label>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Trạng thái</label>
            <div class="col-sm-8">
                <asp:DropDownList ID="DOCSTATUS" runat="server" CssClass="form-control" Width="100px">
                </asp:DropDownList>
            </div>
        </div>   
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Loại tiêu chí</label>
            <div class="col-sm-8">
                <asp:DropDownList ID="ID6_359B4356_DA02_4D60_870D_44508E638682" runat="server" CssClass="form-control">
                    <asp:ListItem Text ="Định tính" Value="0" />
                    <asp:ListItem Text ="Định lượng" Value="1" />
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group dinhtinh">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Loại tiêu chí định tính</label>
            <div class="col-sm-8">
                <asp:DropDownList ID="ID9_D51797C1_4558_4237_A184_610F7C10D1C4" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group dinhluong">                 
            <label class="col-sm-3 col-sm-offset-1 control-label">Min</label>
            <div class="col-sm-3">
                <asp:TextBox ID="txtMin" runat="server" CssClass="form-control TextBoxPercent"></asp:TextBox>
            </div>
            <label class="col-sm-1 control-label" style="padding-left: 0 !important;">Max</label>
            <div class="col-sm-4">
                <asp:TextBox ID="txtMax" runat="server" CssClass="form-control TextBoxPercent"></asp:TextBox>
            </div>
        </div>
        <div class="form-group dinhluong">
            <label for="inputDescription" class="col-sm-4 control-label">&nbsp;</label>
            <div class="col-sm-8">
                <div class="panel-form">
                    <div class="panel-heading">
                        <span class="title-form">Cấu hình điểm quy đổi</span>
                        <asp:HiddenField ID="ID8_56B0851F_537B_4281_AD36_95909AB7FD67" runat="server" />
                    </div>
                    <div class="panel-body">
                        <div class="form-horizontal">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-sm-3 control-label" style="padding-right: 15px !important;">Từ</label>
                                    <label class="col-sm-3 control-label" style="padding-right: 15px !important;">Đến</label>
                                    <label class="col-sm-3 control-label" style="padding-right: 15px !important;">Điểm quy đổi</label>
                                    <label class="col-sm-3 control-label" style="padding-right: 15px !important;">&nbsp;</label>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control TextBoxPercent"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtTo" runat="server" CssClass="form-control TextBoxPercent"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtConvert" runat="server" CssClass="form-control TextBoxPercent"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-3">
                                        <a href="javascript:;" onclick="return ThemMoiDinhLuong();" class="col-sm-12 btn color-green btn-xs">Thêm</a>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="table-responsive">
                                        <table class="table table-striped table-bordered table-hover" id="tblDonvi">
                                            <thead class="color-green">
                                                <tr>
                                                    <th>Từ</th>
                                                    <th>Đến</th>
                                                    <th>Điểm quy đổi</th>
                                                    <th>Thao tác</th>
                                                </tr>
                                            </thead>
                                            <tbody></tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    <script type="text/javascript">
        var _documentid;
        /*********************************************************/
        $(document).ready(function () {
            //do smt here
            
            var idxLoaitieuchi = $("#<%= ID6_359B4356_DA02_4D60_870D_44508E638682.ClientID %> option:selected").index();
            if (idxLoaitieuchi == 0) {
                $(".dinhtinh").show();
                $(".dinhluong").hide();
            }
            else {
                $(".dinhtinh").hide();
                $(".dinhluong").show();
            }
            $('#<%= ID6_359B4356_DA02_4D60_870D_44508E638682.ClientID %>').on('change', function () {
                if (this.value == "0") {
                    $(".dinhtinh").show("slow");
                    $(".dinhluong").hide();
                }
                else {
                    $(".dinhtinh").hide();
                    $(".dinhluong").show("slow");
                }
            });
            LoadDinhLuong();
        });
        var lstDonvi = [];
        function LoadDinhLuong()
        {
            //{ "Config": { "min": min, "max": max }, "ListDonvi": lstDonvi };
            var objDonvi = jQuery.parseJSON($("#<%= ID8_56B0851F_537B_4281_AD36_95909AB7FD67.ClientID %>").val());
            lstDonvi = objDonvi.ListDonvi;
            $("#<%= txtMin.ClientID %>").val(objDonvi.Config.min);
            $("#<%= txtMax.ClientID %>").val(objDonvi.Config.max);
            for (var i = 0; i < lstDonvi.length; i++) {
                //{ "from": min, "to": max, "convert": _convert };
                var min = lstDonvi[i].from;
                var max = lstDonvi[i].to;
                var _convert = lstDonvi[i].convert;
                var tr_id = 'tr_' + min + '_' + max + '_' + _convert;
                var html = '<tr id="' + tr_id + '">';
                html += '       <td>' + min + '</td>';
                html += '       <td>' + max + '</td>';
                html += '       <td>' + _convert + '</td>';
                html += '       <td>';
                html += '           <a class="click-icon" onclick="removeDonvi(\'' + tr_id + '\')"><span class="delete-file"></span></a>';
                html += '       </td>';
                html += '</tr>';
                $("#tblDonvi tbody").append(html);
            }
        }
        function ThemMoiDinhLuong()
        {
            if ($("#<%= txtFrom.ClientID %>").val().length == 0) {
                alert("Giá trị định lượng từ khoảng không được để trống!");
                return false;
            }
            else if ($("#<%= txtTo.ClientID %>").val().length == 0) {
                alert("Giá trị định lượng đến khoảng không được để trống!");
                return false;
            }
            else if ($("#<%= txtConvert.ClientID %>").val().length == 0) {
                alert("Giá trị quy đổi không được để trống!");
                return false;
            }
            var min = parseInt($("#<%= txtFrom.ClientID %>").val());
            var max = parseInt($("#<%= txtTo.ClientID %>").val());
            var _convert = parseInt($("#<%= txtConvert.ClientID %>").val());
            if (max <= min) {
                alert("Giá trị định lượng từ khoảng phải nhỏ hơn đến khoảng!");
                return false;
            }
            //Thêm vào mảng
            var donvi = { "from": min, "to": max, "convert": _convert };
            //Check xem đã tồn tại chưa.
            var filterDonvi = $.grep(lstDonvi, function (obj) { return obj.from == min && obj.to == max && obj.convert == _convert });
            if (filterDonvi.length > 0)
            {
                alert("Giá trị đã tồn tại!");
                return false;
            }            
            lstDonvi.push(donvi);
            //console.log(JSON.stringify(lstDonvi));
            //Append và table
            var tr_id = 'tr_' + min + '_' + max + '_' + _convert;
            var html = '<tr id="' + tr_id + '">';
            html += '       <td>' + min + '</td>';
            html += '       <td>' + max + '</td>';
            html += '       <td>' + _convert + '</td>';
            html += '       <td>';
            html += '           <a class="click-icon" onclick="removeDonvi(\'' + tr_id + '\')"><span class="delete-file"></span></a>';
            html += '       </td>';
            html += '</tr>';
            $("#tblDonvi tbody").append(html);
        }
        function removeDonvi(tr_min)
        {
            $("#" + tr_min).remove();
            var min = tr_min.split('_')[1];
            var max = tr_min.split('_')[2];
            var _convert = tr_min.split('_')[3];
            var filterDonvi = $.grep(lstDonvi, function (obj) { return obj.from != min && obj.to != max && obj.convert != _convert });
            lstDonvi = filterDonvi;
        }
        /*********************************************************/
        function preparesavedoc(documentID, doctypeID) {
            var ten = GetSvrCtlValue("ID8_23FDFFE4_8F68_437E_B777_7DC2B88B9EB6");
            var diengiai = GetSvrCtlValue("ID8_3031A7F5_9793_45AB_897E_AC05C60B299A");
            var tytrong = GetSvrCtlValue("ID6_F5D740A0_342D_40D4_96EA_74C5DB35AE46");
            var url = "TieuChiThanhPhan_Input.aspx";
            var query = "act=checkvalue";
            var valueactive = $("#ctl00_FormContent_DOCSTATUS").val();
            if ($("#<%= ID6_359B4356_DA02_4D60_870D_44508E638682.ClientID %> option:selected").index() == 1) {
                if ($("#<%= txtMin.ClientID %>").val().length == 0) {
                    alert("Giá trị Min không được để trống!");
                    return false;
                }
                else if ($("#<%= txtMax.ClientID %>").val().length == 0) {
                    alert("Giá trị Min không được để trống!");
                    return false;
                }
                var min = parseInt($("#<%= txtMin.ClientID %>").val());
                var max = parseInt($("#<%= txtMax.ClientID %>").val());
                if (min >= max) {
                    alert("Giá trị Max phải lớn hơn giá trị Min!");
                    return false;
                }
                var objDinhLuong = { "Config": { "min": min, "max": max }, "ListDonvi": lstDonvi };
                $("#<%= ID8_56B0851F_537B_4281_AD36_95909AB7FD67.ClientID %>").val(JSON.stringify(objDinhLuong));
            }
            else
                $("#<%= ID8_56B0851F_537B_4281_AD36_95909AB7FD67.ClientID %>").val('');

            query += "&p=23FDFFE4-8F68-437E-B777-7DC2B88B9EB6";
            query += "&v=" + ten;
            query += "&diengiai=" + diengiai;
            query += "&tytrong=" + tytrong;
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
            var ten = GetSvrCtlValue("ID8_23FDFFE4_8F68_437E_B777_7DC2B88B9EB6");
            var diengiai = GetSvrCtlValue("ID8_3031A7F5_9793_45AB_897E_AC05C60B299A");
            var tytrong = GetSvrCtlValue("ID6_F5D740A0_342D_40D4_96EA_74C5DB35AE46");
            var url = "TieuChiThanhPhan_Input.aspx";
            var query = "act=checkvalueupdate";
            var valueactive = $("#ctl00_FormContent_DOCSTATUS").val();

            if ($("#<%= ID6_359B4356_DA02_4D60_870D_44508E638682.ClientID %> option:selected").index() == 1) {
                if ($("#<%= txtMin.ClientID %>").val().length == 0) {
                    alert("Giá trị Min không được để trống!");
                    return false;
                }
                else if ($("#<%= txtMax.ClientID %>").val().length == 0) {
                    alert("Giá trị Min không được để trống!");
                    return false;
                }
                var min = parseInt($("#<%= txtMin.ClientID %>").val());
                var max = parseInt($("#<%= txtMax.ClientID %>").val());
                if (min >= max) {
                    alert("Giá trị Max phải lớn hơn giá trị Min!");
                    return false;
                }
                var objDinhLuong = { "Config": { "min": min, "max": max }, "ListDonvi": lstDonvi };
                $("#<%= ID8_56B0851F_537B_4281_AD36_95909AB7FD67.ClientID %>").val(JSON.stringify(objDinhLuong));
            }
            else
                $("#<%= ID8_56B0851F_537B_4281_AD36_95909AB7FD67.ClientID %>").val('');

            query += "&p=23FDFFE4-8F68-437E-B777-7DC2B88B9EB6";
            query += "&v=" + ten;
            query += "&diengiai=" + diengiai;
            query += "&tytrong=" + tytrong;
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
            var year = $("#<%= lblYear.ClientID %>").val();
            var ldtkt = encodeURI($("#<%= lblLDTKT.ClientID %>").val());
            url = "TieuChiChinh_Input.aspx?a=<%= appID %>&curApp=<%= curAppID %>&y=" + year + "&l=" + ldtkt + "&act=loaddoc&doc=<%= _tcc %>";
            window.location.href = url;
        }
        function savedoc_error() {
            alert(MSG_ADD_ER);
        }
        function update_success() {
            alert(MSG_EDIT_OK);
            var year = $("#<%= lblYear.ClientID %>").val();
            var ldtkt = encodeURI($("#<%= lblLDTKT.ClientID %>").val());
            url = "TieuChiChinh_Input.aspx?a=<%= appID %>&curApp=<%= curAppID %>&y=" + year + "&l=" + ldtkt + "&act=loaddoc&doc=<%= _tcc %>";
            window.location.href = url;
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

