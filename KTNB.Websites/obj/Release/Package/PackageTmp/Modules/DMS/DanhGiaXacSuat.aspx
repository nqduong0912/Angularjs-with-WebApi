<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true"
    CodeBehind="DanhGiaXacSuat.aspx.cs" Inherits="VPB_TDHS.Modules.DMS.DanhGiaXacSuat" %>

<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <asp:ScriptManager runat="server" ID="ScriptManager1">
    </asp:ScriptManager>
    <table width="100%">
        <tr class="GridHeader">
            <td colspan="2">
                Thông tin đánh giá xác suất/ảnh hưởng
            </td>
        </tr>
        <tr>
            <td>Thông tin đánh giá xác suất</td>
            <td>Thông tin đánh giá ảnh hưởng</td>
        </tr>
        <tr>
            <td style="width:50%" valign="top">
                <C1WebGrid:C1WebGrid runat="server" ID="dataCtrl" Width="100%" OnItemCreated="dataCtrl_OnItemCreated"
                    OnItemDataBound="dataCtrl_OnItemDataBound" GroupIndent="20" ItemStyle-Height="30px"
                    AllowPaging="false">
                    <Columns>
                        <C1WebGrid:C1TemplateColumn Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="PK_DocumentID" Text='<%# DataBinder.Eval(Container.DataItem,"PK_DocumentID")%>'></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Tên tiêu chí" DataField="Tên tiêu chí">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="60%" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1TemplateColumn Visible="true" HeaderText="Điểm đánh giá">
                            <ItemTemplate>
                                <asp:DropDownList Width="150px" SkinID="DropDownList" runat="server" ID="ddlTieuChi_xs">
                                </asp:DropDownList>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>
                    </Columns>
                </C1WebGrid:C1WebGrid>
            </td>
      
           <td style="width:50%" valign="top">
                <C1WebGrid:C1WebGrid runat="server" ID="dataCtrl_ah" Width="100%" 
                    GroupIndent="20" ItemStyle-Height="30px"
                    AllowPaging="false" onitemdatabound="dataCtrl_ah_ItemDataBound">
                    <Columns>
                        <C1WebGrid:C1TemplateColumn Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="PK_DocumentID" Text='<%# DataBinder.Eval(Container.DataItem,"PK_DocumentID")%>'></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Tên tiêu chí" DataField="Tên tiêu chí">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="60%" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1TemplateColumn Visible="true" HeaderText="Điểm đánh giá">
                            <ItemTemplate>
                                <asp:DropDownList Width="150px" SkinID="DropDownList" runat="server" ID="ddlTieuChi_ah">
                                </asp:DropDownList>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>
                    </Columns>
                </C1WebGrid:C1WebGrid>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        /*********************************************************/

        $(document).ready(function () {


        });

        function savedoc_success() {

        }
        function savedoc_error() {
            alert(MSG_ADD_ER);
        }
        function update_success() {
            alert(MSG_EDIT_OK);
            var nhomkt = Qry["nhomkt"];

            var dotkt = Qry["dotkt"];
            var doankt = Qry["doankt"];
            var timkiem = Qry["timkiem"];

            var url = "LapHoSoRuiRo.aspx?act=loaddoc&doc=" + nhomkt;
            if (timkiem == 'tk')
                url = "ReViewDotKiemToan_View.aspx?act=loaddoc&doc=" + nhomkt+"&doankt="+doankt+"&dotkt="+dotkt;
            window.location.href = url;
        }
        function update_error() {
            alert(MSG_EDIT_ER);
        }
        function delete_success() {
        }
        function delete_error() {
            alert(MSG_DEL_ER);
        }

        function updatedocument(type) {
            if (type == 'xs') {
                danhgiaxacsuat(type);
            }
            else if (type == 'ah') {
                danhgiaanhuong(type);
            }
        }

        function updatedocumentxs_ah() {
            danhgiaxs_ah();
        }

        function danhgiaxacsuat(type) {
            var documentID = Qry["doc"];
            var objs = $("select[id*='ddlTieuChi']");
            var count = 0;
            var sum = 0;
            objs.each(function () {
                if ($(this).val() != "0") {
                    count++;
                    sum += parseInt($(this).val());
                }
            });
            if (count == 0) {
                alert("Chọn tiêu chí để đánh giá xác suất.");
                return false;
            }

            if (!window.confirm("Bạn có muốn đánh giá xác suất?"))
                return false;
            var value = Math.round(sum / count);
            var url = "DanhGiaXacSuat.aspx";
            var query = "act=danhgiaxacsuat";
            query += "&value=" + value;
            query += "&type=" + type;
            query += "&doc=" + documentID;
            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (ErrorMessage) {
                    FinishProcessingForm();
                    
                    if (ErrorMessage == "") {
                        update_success();
                    }
                    else
                        alert("Error");
                }
            });
        }


        function danhgiaanhuong(type) {
            var documentID = Qry["doc"];
            var objs = $("select[id*='ddlTieuChi']");
            var count = 0;
            var sum = 0;
            objs.each(function () {
                if ($(this).val() != "0") {
                    count++;
                    sum += parseInt($(this).val());
                }
            });
            if (count == 0) {
                alert("Chọn tiêu chí để đánh giá ảnh hưởng.")
                return false;
            }
            if (!window.confirm("Bạn có muốn đánh giá ảnh hưởng?"))
                return false;

            var value = Math.round(sum / count);
            var url = "DanhGiaXacSuat.aspx";
            var query = "act=danhgiaanhhuong";
            query += "&value=" + value;
            query += "&type=" + type;
            query += "&doc=" + documentID;
            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (ErrorMessage) {
                    FinishProcessingForm();
                    
                    if (ErrorMessage == "") {
                        update_success();
                    }
                    else
                        alert("Error");
                }
            });
        }


        function danhgiaxs_ah() {
            var documentID = Qry["doc"];
            var objs_xs = $("select[id*='ddlTieuChi_xs']");
            var objs_ah = $("select[id*='ddlTieuChi_ah']");
            var diem_xs = '';
            var diem_ah = '';
            var count_xs = 0;
            var sum_xs = 0;

            var arr_xs = [];
            var arr_ah = [];

           
          
            //xacsuat
            objs_xs.each(function () {
                if ($(this).val() != "0") {
                    count_xs++;
                    sum_xs += parseInt($(this).val());
                    var obj_diem_xs = {};
                    obj_diem_xs.Diem = $(this).val();
                    obj_diem_xs.Ten = $(this).parent().prev().text().trim();
                    arr_xs.push(obj_diem_xs);
                }
            });
           

           //anhhuong
            var count_ah = 0;
            var sum_ah = 0;
            objs_ah.each(function () {
                if ($(this).val() != "0") {
                    count_ah++;
                    sum_ah += parseInt($(this).val());
                    var obj_diem_ah = {};
                    obj_diem_ah.Diem = $(this).val();
                    obj_diem_ah.Ten = $(this).parent().prev().text().trim();
                    arr_ah.push(obj_diem_ah);
                }
            });

            if (count_xs == 0 || count_ah == 0) {
                alert("Chọn tiêu chí để đánh giá ảnh hưởng/xác suất.")
                return false;
            }
            if (!window.confirm("Bạn có muốn đánh giá ảnh hưởng/xác suất?"))
                return false;

            var value_xs = Math.round(sum_xs / count_xs);
            var value_ah = Math.round(sum_ah / count_ah);

            var json_diem_xs = JSON.stringify(arr_xs);
            var json_diem_ah = JSON.stringify(arr_ah);
            //alert(json_diem_xs);
            var url = "DanhGiaXacSuat.aspx";
            var query = "act=danhgiaxs_ah";
            query += "&value_xs=" + value_xs;
            query += "&value_ah=" + value_ah;
            query += "&doc=" + documentID;
            query += "&json_diem_xs=" + json_diem_xs;
            query += "&json_diem_ah=" + json_diem_ah;
            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (ErrorMessage) {
                    FinishProcessingForm();

                    if (ErrorMessage == "") {
                        update_success();
                    }
                    else
                        alert("Error");
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
