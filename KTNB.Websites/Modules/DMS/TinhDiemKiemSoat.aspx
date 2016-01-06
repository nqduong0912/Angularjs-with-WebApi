<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true"
    CodeBehind="TinhDiemKiemSoat.aspx.cs" Inherits="VPB_TDHS.Modules.DMS.TinhDiemKiemSoat" %>

<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <asp:ScriptManager runat="server" ID="ScriptManager1">
    </asp:ScriptManager>
    <table width="100%">
        <tr class="GridHeader">
            <td>
                Các kiểm soát
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel runat="server" ID="updatepanel1" UpdateMode="Conditional" OnLoad="updatepanel1_OnLoad">
                    <ContentTemplate>
                        <C1WebGrid:C1WebGrid runat="server" ID="dataCtrl" Width="100%" OnItemCreated="dataCtrl_OnItemCreated"
                            OnItemDataBound="dataCtrl_OnItemDataBound" GroupIndent="20" ItemStyle-Height="30px">
                            <Columns>
                                <C1WebGrid:C1BoundColumn HeaderText="Tên kiểm soát" DataField="Tên kiểm soát">
                                    <ItemStyle Width="50%" VerticalAlign="Top" HorizontalAlign="Left" />
                                </C1WebGrid:C1BoundColumn>
                                <C1WebGrid:C1TemplateColumn Visible="false" HeaderText="">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblKiemSoatID" Text='<%# DataBinder.Eval(Container.DataItem,"PK_DocumentID")%>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="50%" HorizontalAlign="Center" VerticalAlign="Top" />
                                </C1WebGrid:C1TemplateColumn>
                                <C1WebGrid:C1TemplateColumn Visible="true" HeaderText="">
                                    <ItemTemplate>
                                        <%-- <input type="radio" name="sex" value="1">1
                                        <input type="radio" name="sex" value="2">2--%>
                                        <asp:Label runat="server" ID="lblFormatRadio" />
                                    </ItemTemplate>
                                    <ItemStyle Width="50%" HorizontalAlign="Center" VerticalAlign="Top" />
                                </C1WebGrid:C1TemplateColumn>
                            </Columns>
                        </C1WebGrid:C1WebGrid>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        /*********************************************************/
        var dotkt = Qry["dotkt"];
        var doankt = Qry["doankt"];
        var nhomkt = Qry["nhomkt"];
        $(document).ready(function () {


        });

        function savedoc_success() {

        }
        function savedoc_error() {
            alert(MSG_ADD_ER);
        }
        function update_success() {
            alert(MSG_EDIT_OK);
            var dotkt = Qry["dotkt"];
            var doankt = Qry["doankt"];
            var timkiem = Qry["timkiem"];

            var url = "DanhGiaRuiRoConLai.aspx?doc=" + nhomkt;
            if (timkiem == 'tk')
                url = "ReViewDotKiemToan_View.aspx?act=loaddoc&doc=" + nhomkt + "&doankt=" + doankt + "&dotkt=" + dotkt;
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

        function TinhDiem() {
            var total = 0;
            var count = 0;
            $('input[type=radio]:checked').each(function () {
                var diem = parseInt($(this).val());
                total += diem;
                count++;
            });

            var result = Math.round(total / count);
            var hosoruiroid = Qry["hosoruiroid"];

            if (count == 0) {
                alert("Bạn chưa chọn đánh giá!");
                return false;
            }

            if (!window.confirm("Bạn có muốn đánh giá ?"))
                return false;
            var value = Math.round(total / count);
            
            var url = "TinhDiemKiemSoat.aspx";
            var query = "act=tinhdiem";
            query += "&value=" + value;
            query += "&hosoruiroid=" + hosoruiroid;
            query += "&dotkt=" + dotkt;
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

            var count_xs = 0;
            var sum_xs = 0;
            //xacsuat
            objs_xs.each(function () {
                if ($(this).val() != "0") {
                    count_xs++;
                    sum_xs += parseInt($(this).val());
                }
            });

            //anhhuong
            var count_ah = 0;
            var sum_ah = 0;
            objs_ah.each(function () {
                if ($(this).val() != "0") {
                    count_ah++;
                    sum_ah += parseInt($(this).val());
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


            var url = "DanhGiaXacSuat.aspx";
            var query = "act=danhgiaxs_ah";
            query += "&value_xs=" + value_xs;
            query += "&value_ah=" + value_ah;
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

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ButtonExtendBefore" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ButtonExtend" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FormFooter" runat="server">
</asp:Content>
