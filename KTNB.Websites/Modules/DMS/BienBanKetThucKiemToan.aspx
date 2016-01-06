<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true"
    CodeBehind="BienBanKetThucKiemToan.aspx.cs" Inherits="VPB_TDHS.Modules.DMS.BienBanKetThucKiemToan" %>

<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <asp:ScriptManager runat="server" ID="ScriptManager1">
    </asp:ScriptManager>
    <asp:HiddenField ID="hiddenMucDoPhatHien" runat="server" />
    <div class="form-horizontal">
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Đợt kiểm toán</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="ddlDotKiemToan" runat="server" CssClass="form-control" Width="300px">
                </asp:DropDownList>
                <input type="button" style="width: 100px;" class="btn btn-default" onclick="XemThongTinDot();" value="Xem" />
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Đối tượng kiểm toán</label>
            <div class="col-sm-6">
                <asp:TextBox ID="txtDoiTuongKiemToan" runat="server" Enabled="False" Width="300px" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Đơn vị kiểm toán</label>
            <div class="col-sm-6">
                <asp:TextBox ID="txtDonViKiemToan" runat="server" Enabled="False" Width="300px" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
    </div>
    <table width="100%">
        <%--<tr>
            <td style="width: 222px">Đợt kiểm toán
            </td>
            <td style="padding-left: 3px; width: 40%;">
                <asp:DropDownList ID="ddlDotKiemToan" Width="100%" runat="server" SkinID="DropDownList">
                </asp:DropDownList>
            </td>
            <td>
                <input type="button" style="width: 100px; background-image: url('~/Images/search_ico_8.gif');"
                    class="SearchButton" onclick="XemThongTinDot();" value="Xem" />
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Đối tượng kiểm toán
            </td>
            <td style="padding-left: 3px;">
                <asp:TextBox ID="txtDoiTuongKiemToan" Width="100%" runat="server" SkinID="TextBoxReadOnly"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Đơn vị kiểm toán
            </td>
            <td style="padding-left: 3px;">
                <asp:TextBox ID="txtDonViKiemToan" Width="100%" runat="server" SkinID="TextBoxReadOnly"></asp:TextBox>
            </td>
        </tr>--%>
        <tr id="trMangNghiepvu" style="display: none;">
            <td style="width: 222px; vertical-align: top;">Mảng nghiệp vụ
            </td>
            <td>
                <asp:UpdatePanel runat="server" ID="updatepanel1" UpdateMode="Conditional" OnLoad="updatepanel1_OnLoad">
                    <ContentTemplate>
                        <asp:HiddenField ID="hiddenDotID" runat="server" />
                        <table width="100%">
                            <tr>
                                <td>
                                    <C1WebGrid:C1WebGrid runat="server" ID="dataCtrl" Width="100%" OnItemCreated="dataCtrl_OnItemCreated"
                                        OnItemDataBound="dataCtrl_OnItemDataBound" GroupIndent="20" ItemStyle-Height="30px">
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
        </tr>
        <tr>
            <td style="width: 222px">Định dạng
            </td>
            <td>
                <label><input type="radio" checked="checked" name="report-format" value="DOC"> DOC</label>
                <label><input type="radio" name="report-format" value="PDF"> PDF</label>
                <label><input type="radio" name="report-format" value="XLS"> XLS</label>
            </td>
            <td>
                <input id="btn-baocao" style="width: 100px;" type="button" class="btn btn-primary" onclick="BaoCao();" value="Xuất báo cáo" />
            </td>
        </tr>
    </table>
    <div id="data-container">
    </div>
    <%--<input id="btn-submit" type="button" value="Submit" onclick="Submit()" class="InsertButton" />--%>
    <asp:Button runat="server" Visible="false" ID="btnReport" Text="Báo cáo" SkinID="btn btn-primary" OnClick="btnReport_Click" />
    <style type="text/css">
        .title-mangnghiepvu {
            font-weight: bold;
        }

        table.fixed {
            border: 1px solid black;
            border-collapse: collapse;
            width: 90%;
            margin-left: 3px;
            margin-right: 3px;
        }

            table.fixed td {
                overflow: hidden;
                border: 1px solid black;
                font-size: 13px;
            }

            table.fixed th {
                background-color: #008081;
                color: White;
                font-size: 13px;
            }

        textarea {
            width: 250px !important;
            font-size: 13px !important;
        }

        .span-tenmangnghiepvu {
            cursor: pointer;
        }
    </style>
    <script type="text/javascript">
        var _documentid;
        var MSG_CONFIRM_DEL_HOSOSOBO = "Bạn có muốn xóa phân tích này?";
        var MSG_CONFIRM_BAOCAO = "Bạn có muốn tạo báo cáo?";
        var clientIDbtnSave = '<%=_btnSave.ClientID %>';
        var jsonMucDoPhatHien = '<%=hiddenMucDoPhatHien.Value %>';
        var objJsonMucDoPhatHien = JSON.parse(jsonMucDoPhatHien);
        /*********************************************************/
        $(document).ready(function () {
            //do smt here
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

        function XemThongTinDot() {
            var dotkt = $("#" + "<%=ddlDotKiemToan.ClientID %>").val();
            if (dotkt == 'select') {
                alert('Bạn chưa chọn đợt kiểm toán!');
                $('#' + '<%=txtDoiTuongKiemToan.ClientID %>').val('');
                $('#' + '<%=txtDonViKiemToan.ClientID %>').val('');
                $("#" + "<%=hiddenDotID.ClientID %>").val('');
                $("#btn-baocao").css('opacity', '0.5');
                $("#btn-baocao").attr('disabled', 'disabled');
                __doPostBack('<%=updatepanel1.ClientID %>', '');
                return false;
            }
            else {
                $("#btn-baocao").removeAttr('disabled');
                $("#btn-baocao").css('opacity', '1');
                //render grid để sửa thông tin phát hiện trước khi xuất báo cáo
                var url = "BienBanKetThucKiemToan.aspx";
                var query = "act=getdatamodel";
                var dotkt = $('#' + '<%=ddlDotKiemToan.ClientID %>').val();
                query += "&dotkt=" + dotkt;
                StartProcessingForm("");
                $.ajax({
                    type: "POST",
                    url: url,
                    data: query,
                    success: function (data) {
                        FinishProcessingForm();
                        if (data != '0') {
                            //render grid here
                            $('#data-container').empty();
                            var mangNghiepVuList = JSON.parse(data);
                            for (var i = 0; i < mangNghiepVuList.length; i++) {
                                var strDivMangNghiepVu = '<div mangnghiepvuid=\"' + mangNghiepVuList[i].MangNghiepVuID + '\" />';
                                var jDivMangNghiepVu = $(strDivMangNghiepVu);
                                var jtablePhatHien = buildTablePhatHien(mangNghiepVuList[i].MangNghiepVuID, mangNghiepVuList[i].PhatHienBienBanList);

                                var DivMangNghiepVuTitle = '<div class=\"title-mangnghiepvu\" ><input tenmangnghiepvu=\"' + mangNghiepVuList[i].TenMangNghiepVu + '\" value=\"' + mangNghiepVuList[i].MangNghiepVuID + '\" class=\"chk-mangnghiepvu\" type=\"checkbox\"  ><span class=\"span-tenmangnghiepvu\" id=\"' + mangNghiepVuList[i].MangNghiepVuID + '\">' + mangNghiepVuList[i].TenMangNghiepVu + '</span></div>';
                                jDivMangNghiepVu.append(DivMangNghiepVuTitle);
                                jDivMangNghiepVu.append(jtablePhatHien);
                                //add to container
                                $('#data-container').append(jDivMangNghiepVu);
                                var count = 0;
                                $('.fixed').each(function () {
                                    if (count == 0)
                                        $(this).show();
                                    else
                                        $(this).hide();
                                    count++;
                                });
                            }
                        }
                        else {
                            $('#sua-phat-hien').empty();
                            $('#data-container').empty();
                        }
                    }
                });
            }
            //                $('#btn-baocao').show();
            $("#" + "<%=hiddenDotID.ClientID %>").val(dotkt);
            __doPostBack('<%=updatepanel1.ClientID %>', '');
            //update cac textbox lien quan dot kiem toan 
            var doiTuongKiemToan = $('option[value="' + dotkt + '"]').first().attr('doi-tuong-kiem-toan');
            var donViThucHien = $('option[value="' + dotkt + '"]').first().attr('don-vi-thuc-hien');
            $('#' + '<%=txtDoiTuongKiemToan.ClientID %>').val(doiTuongKiemToan);
            $('#' + '<%=txtDonViKiemToan.ClientID %>').val(donViThucHien);
        }
        function buildTablePhatHien(mangnghiepvuid, phatHienList) {
            //            $('#sua-phat-hien').empty();
            //            $('#sua-phat-hien').append("<tr><th><input id=\"check-all\" type=\"checkbox\" /> </th><th >Phát hiện</th><th>Mức độ</th><th >Dẫn chiếu</th><th >Ý kiến đơn vị</th></tr>");
            var table = $('<table/>');
            table.addClass('fixed');
            table.append("<tr><th></th><th >Phát hiện</th><th>Mức độ</th><th >Dẫn chiếu</th><th >Ý kiến đơn vị</th><th >Trạng thái</th></tr>");
            table.attr('mangnghiepvu', mangnghiepvuid)
            var tr;
            for (var i = 0; i < phatHienList.length; i++) {
                tr = $('<tr/>');
                tr.attr('phathienid', phatHienList[i].PhatHienID);
                var dropdownMucDo = buildDropDownMucDoPhatHien(phatHienList[i].MucDo);

                tr.append("<td style=\"width: 5%;\" ><input style=\"margin-left:10px;\" mangnghiepvuid=\"" + mangnghiepvuid + "\" class=\"chk-phathien\" chk=\"phathien\" type=\"checkbox\" ></td>");
                tr.append("<td style=\"width: 15%;\" cell=\"tenphathien\" ><textarea rows=\"4\" cols=\"50\">" + phatHienList[i].TenPhatHien + " </textarea></td>");
                tr.append("<td style=\"width: 15%;\" cell=\"mucdo\" >" + dropdownMucDo + "</td>");
                tr.append("<td style=\"width: 35%;\" cell=\"danchieu\" ><textarea rows=\"4\" cols=\"50\">" + phatHienList[i].DanChieu + "</textarea></td>");
                tr.append("<td style=\"width: 30%;\" cell=\"ykiendonvi\" ><textarea rows=\"4\" cols=\"50\">" + phatHienList[i].PhanHoiDonVi + "</textarea></td>");
                tr.append("<td style=\"width: 10%;\" cell=\"trangthai\" >" + phatHienList[i].TrangThai + "</td>");
                table.append(tr);
            }
            return table;
        }
        function buildDropDownMucDoPhatHien(selected) {
            var str = '<select  class=\"DropDownList\">';
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
        function BaoCao() {
            if (!window.confirm(MSG_CONFIRM_BAOCAO))
                return false;
            //lay cac mang nghiep vu va phat hien duoc tick tren grid
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
                    var TenPhatHien = currentCheckBox.closest('tr').find('td[cell=\"tenphathien\"]').find('textarea').val();
                    var MucDo = currentCheckBox.closest('tr').find('td[cell=\"mucdo\"]').find('select').val();
                    var DanChieu = currentCheckBox.closest('tr').find('td[cell=\"danchieu\"]').find('textarea').val();
                    var PhanHoiDonVi = currentCheckBox.closest('tr').find('td[cell=\"ykiendonvi\"]').find('textarea').val();

                    //init object phathien
                    var phathienObj = {};
                    phathienObj.PhatHienID = PhatHienID;
                    phathienObj.TenPhatHien = TenPhatHien;
                    phathienObj.MucDo = MucDo;
                    phathienObj.DanChieu = DanChieu;
                    phathienObj.PhanHoiDonVi = PhanHoiDonVi;
                    mangNghiepVuObj.PhatHienBienBanList.push(phathienObj);
                });
                MangNghiepVuList.push(mangNghiepVuObj);

            });
            if (MangNghiepVuList.length > 0) {
                var jsonBaoCaoData = JSON.stringify(MangNghiepVuList);
                var url = "BienBanKetThucKiemToan.aspx";
                var query = "act=baocao";
                var dotkt = $('#' + '<%=ddlDotKiemToan.ClientID %>').val();
                query += "&dotkt=" + dotkt;
                query += "&jsonbaocaodata=" + jsonBaoCaoData;
                var selectedVal = "";
                var selected = $("input[type='radio'][name='report-format']:checked");
                if (selected.length > 0) {
                    selectedVal = selected.val();
                }
                query += "&reportformat=" + selectedVal;
                StartProcessingForm("");
                $.ajax({
                    type: "POST",
                    url: url,
                    data: query,
                    success: function (ErrorMessage) {
                        FinishProcessingForm();
                        if (ErrorMessage.indexOf("-1") >= 0) {
                            alert("Error: " + ErrorMessage);
                            return false;
                        }
                        if (ErrorMessage == '0')
                            alert('Đợt kiểm toán chưa thể xuất báo cáo!');
                        else
                            window.open(ErrorMessage);
                    }
                });
            }
            else {
                alert('Bạn chưa chọn phát hiện để xuất báo cáo');
            }
        }
        function newform() {
            //            window.location.href = "ChiTietHoSoPhanTichSoBo_Load.aspx";
        }
        function savedoc_success() {
            alert(MSG_ADD_OK);

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

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ButtonExtendBefore" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ButtonExtend" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FormFooter" runat="server">
</asp:Content>
