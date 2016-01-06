<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="BaoCaoChiTiet_SoBo.aspx.cs" Inherits="VPB_TDHS.Modules.DMS.BaoCaoChiTiet_SoBo" %>
<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <asp:ScriptManager runat="server" ID="ScriptManager1">
    </asp:ScriptManager>
    <asp:HiddenField ID="hiddenMucDoPhatHien" runat="server" />
     <asp:HiddenField ID="hdDanhSachPhatHien" runat="server" />
     <asp:ObjectDataSource runat="server" ID="ObjectDataSource1" SelectMethod="GetMangNghiepVuByDotKT"
        TypeName="DataSource">
        <SelectParameters>
            <asp:Parameter Name="DotKT" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <table width="100%">
         <tr>
            <td style="width: 15%">
                Đợt kiểm toán
            </td>
            <td style="width:25%">
                <asp:DropDownList ID="ddlDotKiemToan" CssClass="DropDownList" runat="server"  Width="200px">
                </asp:DropDownList>
                 
            </td>
           <td >
                <asp:Button runat="server" ID="btnView" Text="Xem" CssClass="SearchButton" />
           </td>
        </tr>
        <tr>
            <td >
                Đối tượng kiểm toán
            </td>
            <td>
                <asp:TextBox ID="txtDoiTuongKiemToan" runat="server" SkinID="TextBoxReadOnly"></asp:TextBox>
            </td>
            <td></td>
        </tr>
        <tr>
            <td  >
                Đơn vị kiểm toán
            </td>
            <td>
                <asp:TextBox ID="txtDonViKiemToan" runat="server" SkinID="TextBoxReadOnly"></asp:TextBox>
            </td>
             <td></td>
        </tr>
        
       <tr>
            <td style="width: 15%">
               Định dạng
            </td>
            <td  style="width:25%">
                <input type="radio" checked=checked name="report-format" value="DOC"/>DOC
                <input type="radio" name="report-format" value="PDF"/>PDF
                <input type="radio" name="report-format" value="XLS"/>XLS
            </td>
            <td>
             <input id="btn-baocao" style="width: 100px;" type="button" class="PrintButton" onclick="XuatBaoCao();"
                    value="Xuất báo cáo" />
            </td>
        </tr>
    </table>

    <table width="100%">
       
       <%-- <tr>
            <td style="width: 222px;vertical-align:top">
               Mảng nghiệp vụ
            </td>
            <td>
                 <asp:UpdatePanel runat="server" ID="updatepanel1" UpdateMode="Conditional" OnLoad="updatepanel1_OnLoad">
                <ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td>
                                <C1WebGrid:C1WebGrid runat="server" ID="dataCtrl" Width="100%" OnItemCreated="dataCtrl_OnItemCreated"
                                    OnItemDataBound="dataCtrl_OnItemDataBound" GroupIndent="20" ItemStyle-Height="30px" DataSourceID="ObjectDataSource1">
                                    <Columns>
                                        <C1WebGrid:C1TemplateColumn Visible="false">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="hoso" Text='<%# DataBinder.Eval(Container.DataItem,"PK_DocumentID")%>'></asp:Label>
                                            </ItemTemplate>
                                        </C1WebGrid:C1TemplateColumn>
                                        <C1WebGrid:C1BoundColumn HeaderText="Tên mảng nghiệp vụ" DataField="Tên mảng nghiệp vụ">
                                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        </C1WebGrid:C1BoundColumn>
                                    </Columns>
                                </C1WebGrid:C1WebGrid>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
    </asp:UpdatePanel>

            </td>
            <td>
            </td>
        </tr>--%>
        <tr>
             <td  colspan="2" style="vertical-align: top;">
                Sửa phát hiện
            </td>
        </tr>

         <tr>
          
            <td colspan="2">
               <%-- <table class="fixed" id="sua-phat-hien">
                    <col width="100px" />
                    <col width="300px" />
                </table>--%>
                  <div id="data-container">
                </div>
            </td>

        </tr>
    </table>

    
    <asp:HiddenField ID="hd_IDDotKT" runat="server" />
    <asp:HiddenField ID="hd_Message" runat="server" />
<%--
    <asp:Button runat="server" ID="btnReport" Text="Báo cáo" SkinID="PrintButton" OnClick="btnReport_Click" />--%>
      <style>
         .title-mangnghiepvu
        {
            font-weight:bold;
            }
        table.fixed
        {
            
            border: 1px solid black;
            border-collapse: collapse;
            width:90%;
            margin-left: 3px;
            margin-right: 3px;
        }
        table.fixed td
        {
            overflow: hidden;
            border: 1px solid black;
            font-size:13px;
        }
        table.fixed th
        {
            background-color: #008081;
            color: White;
            font-size:13px;
        }
        textarea
        {
            width: 250px !important;
            font-size:13px !important;;
        }
        .span-tenmangnghiepvu
        {
        cursor:pointer;
        }
        .textarea1
        {
            width: 150px !important;
            font-size:13px !important;
        }
    </style>

    <script type="text/javascript">
        var _documentid;
        var MSG_CONFIRM_DEL_HOSOSOBO = "Bạn có muốn xóa phân tích này?";
        var MSG_CONFIRM_SUBMIT_HOSOSOBO = "Bạn có muốn submit toàn bộ việc phân tích sơ bộ?";


        var clientIDbtnSave = '<%=_btnSave.ClientID %>';
        var _action = Qry["act"];

        var jsonMucDoPhatHien = '<%=hiddenMucDoPhatHien.Value %>';
        var objJsonMucDoPhatHien = JSON.parse(jsonMucDoPhatHien);
        /*********************************************************/
        $(document).ready(function () {
            //do smt here
            //SetInfoDotKT();

            $("#btn-baocao").attr('disabled', 'disabled');
            $("#btn-baocao").css('opacity', '0.5');

            $('#check-all').live('click', function () {
                if ($(this).is(":checked")) {
                    $('input[chk="phathien"]').attr("checked", true);
                }
                else {
                    $('input[chk="phathien"]').attr("checked", false);
                }
            });

            $('.chk-mangnghiepvu').live('click', function () {
                var checked = $(this).is(':checked');
                var mangnghiepvuid = $(this).val();
                if (checked)
                    $('.chk-phathien[mangnghiepvuid="' + mangnghiepvuid + '"]').attr('checked', true);
                else
                    $('.chk-phathien[mangnghiepvuid="' + mangnghiepvuid + '"]').attr('checked', false);
            });
            $('.chk-phathien').live('click', function () {
                var checked = $(this).is(':checked');
                var mangnghiepvuid = $(this).attr('mangnghiepvuid');
                if (checked)
                    $('.chk-mangnghiepvu[value="' + mangnghiepvuid + '"]').attr('checked', true);
                else {
                    var count = $('.chk-phathien[mangnghiepvuid="' + mangnghiepvuid + '"]:checked').length;
                    if (count == 0)
                        $('.chk-mangnghiepvu[value="' + mangnghiepvuid + '"]').attr('checked', false);
                }
            });
            $('.span-tenmangnghiepvu').live('click', function () {
                $(this).parent().parent().find('table').first().slideToggle("slow", function () {
                    // Animation complete.
                });
            });
        });
        /*********************************************************/
        function XuatBaoCao_1() {
            var id_dotkt = $("#" + "<%=ddlDotKiemToan.ClientID %>").val();
            if ((id_dotkt == '') || (id_dotkt == null) || (id_dotkt == 'undefined') ) {
                return false;
            }
            if (id_dotkt == '-1') {
                alert("Vui lòng chọn đợt kiểm toán để xuất báo cáo.");
                return false;
            }

            var phatHienList = [];
            $('input[chk="phathien"]:checked:enabled').each(function () {
                var currentCheckBox = $(this);
                var PhatHienID = currentCheckBox.closest('tr').attr('phathienid');
                //var TenPhatHien = currentCheckBox.closest('tr').find('td[cell=\"tenphathien\"]').text();
                var TenPhatHien = currentCheckBox.closest('tr').find('td[cell=\"tenphathien\"]').find('textarea').val();
                var MucDo = currentCheckBox.closest('tr').find('td[cell=\"mucdo\"]').find('select').val();
                var DanChieu = currentCheckBox.closest('tr').find('td[cell=\"danchieu\"]').find('textarea').val();
                var NguyenNhan = currentCheckBox.closest('tr').find('td[cell=\"nguyennhan\"]').find('textarea') == null
                    ? "" : currentCheckBox.closest('tr').find('td[cell=\"nguyennhan\"]').find('textarea').val();
                var AnhHuong = currentCheckBox.closest('tr').find('td[cell=\"anhhuong\"]').find('textarea') == null 
                    ? "" : currentCheckBox.closest('tr').find('td[cell=\"anhhuong\"]').find('textarea').val();
                var KhuyenNghi = currentCheckBox.closest('tr').find('td[cell=\"khuyennghi\"]').find('textarea').val();

                //init object phathien
                var phathienObj = {};
                phathienObj.PhatHienID = PhatHienID;
                phathienObj.TenPhatHien = TenPhatHien;
                phathienObj.MucDo = MucDo;
                phathienObj.DanChieu = DanChieu;
                phathienObj.NguyenNhan = NguyenNhan;
                phathienObj.AnhHuong = AnhHuong;
                phathienObj.KhuyenNghi = KhuyenNghi;

                phatHienList.push(phathienObj);
            });
            if (phatHienList.length == 0) {
                alert('Bạn chưa chọn phát hiện để xuất báo cáo');
                return false;
            }



            var selectedVal = "";
            var selected = $("input[type='radio'][name='report-format']:checked");
            if (selected.length > 0) {
                selectedVal = selected.val();
            }
          
            
            //alert(_action);
            //ajax de lay gia tri
            var jsonBaoCaoData = JSON.stringify(phatHienList);
            var url = "BaoCaoChiTiet_SoBo.aspx";
            var query = "act_report=xuatbaocao&dotkt=" + id_dotkt + "&act=" + _action;
            query += "&report_format=" + selectedVal;
            query += "&jsonbaocaodata=" + jsonBaoCaoData;
            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (message) {
                    FinishProcessingForm();
                    if (message == "0") {
                        return false;
                    }
                    if (message.indexOf("-1") > 0)
                        alert(message);
                    else
                        window.open(message);
                }
            });
        }

        function XuatBaoCao() {
            var id_dotkt = $("#" + "<%=ddlDotKiemToan.ClientID %>").val();
            if ((id_dotkt == '') || (id_dotkt == null) || (id_dotkt == 'undefined')) {
                return false;
            }
            if (id_dotkt == '-1') {
                alert("Vui lòng chọn đợt kiểm toán để xuất báo cáo.");
                return false;
            }
            var MangNghiepVuList = [];
            $('.chk-mangnghiepvu:checked:enabled').each(function () {
                var currentMangNghiepVuCheckBox = $(this);
                //build MangNghiepVu object
                var mangNghiepVuObj = {};
                mangNghiepVuObj.PhatHienBienBanList = [];
                mangNghiepVuObj.MangNghiepVuID = currentMangNghiepVuCheckBox.val();
                mangNghiepVuObj.TenMangNghiepVu = currentMangNghiepVuCheckBox.attr('tenmangnghiepvu');

                $('.chk-phathien[mangnghiepvuid="' + mangNghiepVuObj.MangNghiepVuID + '"]:checked:enabled').each(function () {
                    var currentCheckBox = $(this);
                    var PhatHienID = currentCheckBox.closest('tr').attr('phathienid');
                    //var TenPhatHien = currentCheckBox.closest('tr').find('td[cell=\"tenphathien\"]').find('input').val();
                    var TenPhatHien = currentCheckBox.closest('tr').find('td[cell=\"tenphathien\"]').find('textarea').val();
                    var MucDo = currentCheckBox.closest('tr').find('td[cell=\"mucdo\"]').find('select').val();
                    var DanChieu = currentCheckBox.closest('tr').find('td[cell=\"danchieu\"]').find('textarea').val();
                    var NguyenNhan = currentCheckBox.closest('tr').find('td[cell=\"nguyennhan\"]').find('textarea').val();
                    var AnhHuong = currentCheckBox.closest('tr').find('td[cell=\"anhhuong\"]').find('textarea') == null ? "" :
                    currentCheckBox.closest('tr').find('td[cell=\"anhhuong\"]').find('textarea').val();
                    var KhuyenNghi = currentCheckBox.closest('tr').find('td[cell=\"khuyennghi\"]').find('textarea') == null ? "" : 
                     currentCheckBox.closest('tr').find('td[cell=\"khuyennghi\"]').find('textarea').val();

                    //init object phathien
                    var phathienObj = {};
                    phathienObj.PhatHienID = PhatHienID;
                    phathienObj.TenPhatHien = TenPhatHien;
                    phathienObj.MucDo = MucDo;
                    phathienObj.DanChieu = DanChieu;
                    phathienObj.AnhHuong = AnhHuong;
                    phathienObj.NguyenNhan = NguyenNhan;
                    phathienObj.KhuyenNghi = KhuyenNghi;
                    mangNghiepVuObj.PhatHienBienBanList.push(phathienObj);
                });
                MangNghiepVuList.push(mangNghiepVuObj);

            });
            if (MangNghiepVuList.length > 0) {
                var selectedVal = "";
                var selected = $("input[type='radio'][name='report-format']:checked");
                if (selected.length > 0) {
                    selectedVal = selected.val();
                }

                var jsonBaoCaoData = JSON.stringify(MangNghiepVuList);
                var url = "BaoCaoChiTiet_SoBo.aspx";
                var query = "act_report=xuatbaocao&dotkt=" + id_dotkt + "&act=" + _action;
                query += "&report_format=" + selectedVal;
                query += "&jsonbaocaodata=" + jsonBaoCaoData;
                StartProcessingForm("");
                $.ajax({
                    type: "POST",
                    url: url,
                    data: query,
                    success: function (message) {
                        FinishProcessingForm();
                        if (message == "0") {
                            return false;
                        }
                        if (message.indexOf("-1") >= 0){
                            alert("Error: " + message);
                            return false;
                        }
                        else {
                            window.open(message);
                        }

                    }
                });
            }
            else {
                alert('Bạn chưa chọn phát hiện để xuất báo cáo');
            }
        
        }

        function XemBaoCao() {
            var id_dotkt = $("#" + "<%=ddlDotKiemToan.ClientID %>").val();
            if ((id_dotkt == '') || (id_dotkt == null) || (id_dotkt == 'undefined')) {
                return false;
            }
            if (id_dotkt == '-1') {
                $("#btn-baocao").attr('disabled', 'disabled');
                $("#btn-baocao").css('opacity', '0.5');
                $("#" + "<%=hd_Message.ClientID %>").val("");
                $('#' + '<%=txtDoiTuongKiemToan.ClientID %>').val("");
                $('#' + '<%=txtDonViKiemToan.ClientID %>').val("");
                $('#data-container').empty();
            }
            else {
                $("#btn-baocao").removeAttr('disabled');
                $("#btn-baocao").css('opacity', '1');
            }

            $("#" + "<%=hd_IDDotKT.ClientID %>").val(id_dotkt);
            //ajax de lay gia tri
            var url = "BaoCaoChiTiet_SoBo.aspx";
            var query = "act_report=baocao&dotkt=" + id_dotkt + "&act=" + _action;
            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (message) {
                    FinishProcessingForm();

                    //lay ra message;
                    var doituong_dotkt = message.split("#")[3];
                    var donvi_dotkt = message.split("#")[4];

                    //update cac textbox lien quan dot kiem toan 

                    $("#" + "<%=hd_Message.ClientID %>").val(message);
                    $('#' + '<%=txtDoiTuongKiemToan.ClientID %>').val(doituong_dotkt);
                    $('#' + '<%=txtDonViKiemToan.ClientID %>').val(donvi_dotkt);
                    //update panel
                    //doPostBack
                    var data = message.split("#")[5];
                    if (data == '' || data == null) {
                        $('#sua-phat-hien').empty();
                        return false;
                    }
                    var mangNghiepVuList = JSON.parse(data);
                    $('#data-container').empty();
                    for (var i = 0; i < mangNghiepVuList.length; i++) {
                        var strDivMangNghiepVu = '<div mangnghiepvuid=\"' + mangNghiepVuList[i].MangNghiepVuID + '\" />';
                        var jDivMangNghiepVu = $(strDivMangNghiepVu);
                        var jtablePhatHien = buildTablePhatHien(mangNghiepVuList[i].MangNghiepVuID, mangNghiepVuList[i].PhatHienBienBanList);

                        //var DivMangNghiepVuTitle = '<div class=\"title-mangnghiepvu\" ><input tenmangnghiepvu=\"' + mangNghiepVuList[i].TenMangNghiepVu + '\" value=\"' + mangNghiepVuList[i].MangNghiepVuID + '\" class=\"chk-mangnghiepvu\" type=\"checkbox\"  ><span id=\"' + mangNghiepVuList[i].MangNghiepVuID + '\">' + mangNghiepVuList[i].TenMangNghiepVu + '</span></div>';
                        var DivMangNghiepVuTitle = '<div class=\"title-mangnghiepvu\" ><input tenmangnghiepvu=\"' + mangNghiepVuList[i].TenMangNghiepVu + '\" value=\"' + mangNghiepVuList[i].MangNghiepVuID + '\" class=\"chk-mangnghiepvu\" type=\"checkbox\"  ><span class=\"span-tenmangnghiepvu\" id=\"' + mangNghiepVuList[i].MangNghiepVuID + '\">' + mangNghiepVuList[i].TenMangNghiepVu + '</span></div>';
                        jDivMangNghiepVu.append(DivMangNghiepVuTitle);
                        jDivMangNghiepVu.append(jtablePhatHien);
                        //                                jtablePhatHien.toggle();
                        $('#data-container').append(jDivMangNghiepVu);
                    }


                    //render grid here
                    //                    var phatHienList = JSON.parse(data);
                    //                    $('#sua-phat-hien').empty();

                    //                    if (_action == "chitiet")
                    //                        $('#sua-phat-hien').append("<tr><th><input id=\"check-all\" type=\"checkbox\" /> </th><th >Phát hiện</th><th>Mức độ</th><th >Dẫn chiếu</th><th >Nguyên nhân</th><th >Ảnh hưởng</th><th >Khuyến nghị</th></tr>");
                    //                    if (_action == "sobo")
                    //                        $('#sua-phat-hien').append("<tr><th><input id=\"check-all\" type=\"checkbox\" /> </th><th >Phát hiện</th><th>Mức độ</th><th >Dẫn chiếu</th><th >Nguyên nhân</th></tr>");
                    //                    var tr;
                    //                    for (var i = 0; i < phatHienList.length; i++) {
                    //                        tr = $('<tr/>');
                    //                        tr.attr('phathienid', phatHienList[i].PhatHienID);
                    //                        var dropdownMucDo = buildDropDownMucDoPhatHien(phatHienList[i].MucDo);

                    //                        tr.append("<td style=\"width: 3%;\" ><input style=\"margin-left:10px;\" chk=\"phathien\" type=\"checkbox\" ></td>");
                    //                        if(_action == "chitiet")
                    //                            tr.append("<td style=\"width: 13%;\" cell=\"tenphathien\" ><textarea rows=\"4\" cols=\"40\">" + phatHienList[i].TenPhatHien + "</textarea></td>");
                    //                        if (_action == "sobo")
                    //                            tr.append("<td style=\"width: 13%;\" cell=\"tenphathien\" ><textarea rows=\"4\" cols=\"40\" class='textarea1'>" + phatHienList[i].TenPhatHien + "</textarea></td>");
                    //                        //tr.append("<td style=\"width: 20%;\" cell=\"tenphathien\" >" + phatHienList[i].TenPhatHien + "</td>");
                    //                        tr.append("<td style=\"width: 10%;\" cell=\"mucdo\" >" + dropdownMucDo + "</td>");
                    //                        if (_action == "chitiet") {
                    //                            tr.append("<td style=\"width: 13%;\" cell=\"danchieu\" ><textarea rows=\"4\" cols=\"40\">" + phatHienList[i].DanChieu + "</textarea></td>");
                    //                            tr.append("<td style=\"width: 13%;\" cell=\"nguyennhan\" ><textarea rows=\"4\" cols=\"40\">" + phatHienList[i].NguyenNhan + "</textarea></td>");
                    //                            tr.append("<td style='width: 13%;' cell=\"anhhuong\" ><textarea rows=\"4\" cols=\"40\">" + phatHienList[i].AnhHuong + "</textarea></td>");
                    //                            tr.append("<td style='width: 13%;' cell=\"khuyennghi\" ><textarea rows=\"4\" cols=\"40\">" + phatHienList[i].KhuyenNghi + "</textarea></td>");
                    //                        }
                    //                        if (_action == "sobo") {
                    //                            tr.append("<td style=\"width: 25%;\" cell=\"danchieu\" ><textarea rows=\"4\" cols=\"40\" class='textarea1'>" + phatHienList[i].DanChieu + "</textarea></td>");
                    //                            tr.append("<td style=\"width: 25%;\" cell=\"nguyennhan\" ><textarea rows=\"4\" cols=\"40\" class='textarea1'>" + phatHienList[i].NguyenNhan + "</textarea></td>");
                    //                        }
                    //                        $('#sua-phat-hien').append(tr);

                    //                    }

                }
            });
        
        }

        function buildDropDownMucDoPhatHien(selected) {
            var str = '<select class=\"DropDownList\">';
            var attrSelected = '';
            for (var i = 0; i < objJsonMucDoPhatHien.length; i++) {
                if (objJsonMucDoPhatHien[i].key == selected) {
                    attrSelected = 'selected';
                }
                else
                    attrSelected = '';
                str += '<option ' + attrSelected + ' value=\"' + objJsonMucDoPhatHien[i].key + '\" >' + objJsonMucDoPhatHien[i].value + '</option>';
            }
            str += '</select>';
            return str;
        }

        function buildTablePhatHien(mangnghiepvuid, phatHienList) {
            var table = $('<table/>');
            table.addClass('fixed');
            if(_action =="chitiet")
                table.append("<tr><th></th><th >Phát hiện</th><th>Mức độ</th><th >Dẫn chiếu</th><th >Nguyên nhân</th><th >Ảnh hưởng</th><th >Khuyến nghị</th><th >Trạng thái</th></tr>");
            if (_action == "sobo")
                table.append("<tr><th></th><th >Phát hiện</th><th>Mức độ</th><th >Dẫn chiếu</th><th >Nguyên nhân</th><th >Trạng thái</th></tr>");
            table.attr('mangnghiepvu', mangnghiepvuid)
            var tr;
            for (var i = 0; i < phatHienList.length; i++) {
                tr = $('<tr/>');
                tr.attr('phathienid', phatHienList[i].PhatHienID);
                var dropdownMucDo = buildDropDownMucDoPhatHien(phatHienList[i].MucDo);

                tr.append("<td style=\"width: 5%;\" ><input style=\"margin-left:10px;\" mangnghiepvuid=\"" + mangnghiepvuid + "\" class=\"chk-phathien\" chk=\"phathien\" type=\"checkbox\" ></td>");
                //tr.append("<td style=\"width: 15%;\" cell=\"tenphathien\" ><input type =\"textbox\" value=\"" + phatHienList[i].TenPhatHien + "\" ></td>");
                tr.append("<td style=\"width: 15%;\" cell=\"tenphathien\" ><textarea rows=\"4\" cols=\"50\" class='textarea1'>" + phatHienList[i].TenPhatHien + "</textarea></td>");
                tr.append("<td style='width: 15%;vertical-align:top;' cell=\"mucdo\" >" + dropdownMucDo + "</td>");
                if (_action == "chitiet") {
                    tr.append("<td style=\"width: 15%;\" cell=\"danchieu\" ><textarea rows=\"4\" cols=\"50\" class='textarea1'>" + phatHienList[i].DanChieu + "</textarea></td>");
                    tr.append("<td style=\"width: 15%;\" cell=\"nguyennhan\" ><textarea rows=\"4\" cols=\"50\" class='textarea1'>" + phatHienList[i].NguyenNhan + "</textarea></td>");
                }
                else {
                    tr.append("<td style=\"width: 35%;\" cell=\"danchieu\" ><textarea rows=\"4\" cols=\"50\" >" + phatHienList[i].DanChieu + "</textarea></td>");
                    tr.append("<td style=\"width: 30%;\" cell=\"nguyennhan\" ><textarea rows=\"4\" cols=\"50\">" + phatHienList[i].NguyenNhan + "</textarea></td>");
                }
                if (_action == "chitiet") {
                    tr.append("<td style=\"width: 15%;\" cell=\"anhhuong\" ><textarea rows=\"4\" cols=\"50\" class='textarea1'>" + phatHienList[i].AnhHuong + "</textarea></td>");
                    tr.append("<td style=\"width: 15%;\" cell=\"khuyennghi\" ><textarea rows=\"4\" cols=\"50\" class='textarea1'>" + phatHienList[i].KhuyenNghi + "</textarea></td>");
                }
                tr.append("<td style='width: 10%;vertical-align:top;' cell=\"trangthai\" >" + phatHienList[i].TrangThai + "</td>");
                table.append(tr);
            }
            return table;
        }


        function newform() {
           
        }
        function savedoc_success() {
            alert(MSG_ADD_OK);

        }
        function savedoc_error() {
            alert(MSG_ADD_ER);
        }
        function update_success() {
            alert(MSG_EDIT_OK);
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

