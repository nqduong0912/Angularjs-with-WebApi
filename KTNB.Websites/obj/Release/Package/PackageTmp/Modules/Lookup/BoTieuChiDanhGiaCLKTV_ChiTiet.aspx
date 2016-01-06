<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true"
    CodeBehind="BoTieuChiDanhGiaCLKTV_ChiTiet.aspx.cs" Inherits="VPB_TDHS.Modules.Lookup.BoTieuChiDanhGiaCLKTV_ChiTiet" %>

<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <div class="form-horizontal">
        <div class="GridHeader">Bộ tiêu chí</div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Tên bộ tiêu chí</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_02CCDE67_5D27_4DF2_BA7C_FD8B7DDE2D74" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Loại tiêu chí</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="LoaiTieuChi" runat="server" CssClass="form-control" Width="300px">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Các tiêu chí</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="DropDownTieuChi" runat="server" CssClass="form-control" Width="300px">
                </asp:DropDownList>
            </div>
            <div class="col-sm-offset-4 col-sm-2">
                <input id="btn-add-tieuchi" type="button" value="Thêm" onclick="bosungTieuChi()" class="form-control btn InsertButton" />
            </div>
        </div>
    </div>
    <%--<table width="100%">
        <tr class="GridHeader">
            <td colspan="2">Bộ tiêu chí
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Tên bộ tiêu chí
            </td>
            <td>
                <asp:TextBox ID="ID8_02CCDE67_5D27_4DF2_BA7C_FD8B7DDE2D74" Enabled="false" runat="server"
                    SkinID="TextBoxReadOnly"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Loại tiêu chí
            </td>
            <td>
                <asp:DropDownList runat="server" SkinID="DropDownList" ID="LoaiTieuChi">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Các tiêu chí
            </td>
            <td>
                <asp:DropDownList runat="server" SkinID="DropDownList" ID="DropDownTieuChi">
                </asp:DropDownList>
                 <%--<asp:Button runat="server" ID="btnAddTieuChi" Text="Thêm" 
                    onclick="btnAddTieuChi_Click" />--%>
    <%--<input id="btn-add-tieuchi" type="button" value="Thêm" onclick="bosungTieuChi()" class="InsertButton" />
            </td>
        </tr>
    </table>--%>
    <asp:ObjectDataSource runat="server" ID="ObjectDataSource1" SelectMethod="getDocumentList"
        TypeName="DataSource">
        <SelectParameters>
            <asp:Parameter Name="DocumentTypeID" Type="String" />
            <asp:Parameter Name="DocFields" Type="String" />
            <asp:Parameter Name="PropertyFields" Type="String" />
            <asp:Parameter Name="Condition" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <table width="50%">
        <tr>
            <td>
                <C1WebGrid:C1WebGrid runat="server" ID="dataCtrl" Width="100%" DataSourceID="ObjectDataSource1"
                    OnItemCreated="dataCtrl_OnItemCreated" OnItemDataBound="dataCtrl_OnItemDataBound"
                    GroupIndent="20" ItemStyle-Height="30px">
                    <Columns>
                        <C1WebGrid:C1TemplateColumn>
                            <ItemTemplate>
                                <asp:Image runat="server" ID="imgDelete" ImageUrl="~/Images/delete.gif" ToolTip="Xoá tiêu chí"
                                    Style="cursor: pointer" />
                            </ItemTemplate>
                            <ItemStyle Width="8%" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1TemplateColumn Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="PK_DocumentID" Text='<%# DataBinder.Eval(Container.DataItem,"PK_DocumentID")%>'></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Tên tiêu chí đánh giá" DataField="Tên tiêu chí đánh giá">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                    </Columns>
                </C1WebGrid:C1WebGrid>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        var _documentid;
        /*********************************************************/
        $(document).ready(function () {
            //do smt here

            $('#' + '<%=LoaiTieuChi.ClientID %>').change(function () {
                //post ajax to re populate dropdown
                var botieuchi = "<%=_botieuchi_documentid %>";
                var loaiTieuChiID = $(this).val();
                var url = "BoTieuChiDanhGiaCLKTV_ChiTiet.aspx";
                var query = "act=reloadtieuchi";

                query += "&loaitieuchi=" + loaiTieuChiID;
                query += "&doc=" + botieuchi;

                $.ajax({
                    type: "POST",
                    url: url,
                    data: query,
                    success: function (data) {

                        var $selectTieuChi = $('#' + '<%=DropDownTieuChi.ClientID %>');
                        $selectTieuChi.empty();
                        jsonObj = JSON.parse(data);

                        var i = 0;
                        for (i = 0; i < jsonObj.length; i++) {
                            $selectTieuChi.append('<option value=' + jsonObj[i].PK_DocumentID + '>' + jsonObj[i]['Tên tiêu chí đánh giá'] + '</option>');
                        }
                    }
                });
            })
        });
            function bosungTieuChi() {
                var botieuchi = "<%=_botieuchi_documentid %>";

                var tieuchi = GetSvrCtlValue("DropDownTieuChi");
                if (tieuchi != '') {
                    var url = "BoTieuChiDanhGiaCLKTV_ChiTiet.aspx";
                    var query = "act=bosungtieuchi";
                    query += "&doc=" + botieuchi;
                    query += "&tieuchi=" + tieuchi;
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
                else
                    alert('Không có tiêu chí để bổ xung!');
            }

            /*********************************************************/

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
            function deleteTieuChi(pk_documentID) {
                var botieuchi = "<%=_botieuchi_documentid %>";
            var tieuchi = pk_documentID;
            var url = "BoTieuChiDanhGiaCLKTV_ChiTiet.aspx";
            var query = "act=xoatieuchi";
            query += "&doc=" + botieuchi;
            query += "&tieuchi=" + tieuchi;
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
