<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="BoTieuChiDanhGiaCLKTV_LoaiTieuChi.aspx.cs" Inherits="VPB_TDHS.Modules.Lookup.BoTieuChiDanhGiaCLKTV_LoaiTieuChi" %>

<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <div class="form-horizontal">
        <div class="GridHeader2">Bộ tiêu chí</div>
        <div class="form-group" style="margin-top: 20px">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Tên bộ tiêu chí</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_02CCDE67_5D27_4DF2_BA7C_FD8B7DDE2D74" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Loại tiêu chí</label>
            <div class="col-sm-6">
                <asp:DropDownList runat="server" ID="DropDownLoaiTieuChi"
                    OnSelectedIndexChanged="DropDownLoaiTieuChi_SelectedIndexChanged" CssClass="form-control" Width="200px">
                </asp:DropDownList>
            </div>
            <div class="col-sm-offset-4 col-sm-2">
                <input class="form-control InsertButton" id="btn-add-loaitieuchi" type="button" value="Thêm" onclick="bosungLoaiTieuChi()" />
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Tỷ trọng</label>
            <div class="col-sm-6">
                <asp:TextBox ID="TyTrong" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
    </div>
    <%--<table width="100%">
        <tr class="GridHeader2">
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
                <asp:DropDownList runat="server" SkinID="DropDownList" ID="DropDownLoaiTieuChi"
                    OnSelectedIndexChanged="DropDownLoaiTieuChi_SelectedIndexChanged">
                </asp:DropDownList>
                <%-- <asp:Button runat="server" ID="btnAddTieuChi" Text="Thêm" 
                    onclick="btnAddTieuChi_Click" />--%>
    <%--  <input id="btn-add-loaitieuchi" type="button" value="Thêm" onclick="bosungLoaiTieuChi()" class="InsertButton" />
            </td>
        </tr>
        <tr>
            <td style="width: 222px">Tỷ trọng
            </td>
            <td>
                <asp:TextBox runat="server" SkinID="TextBox" ID="TyTrong">
                </asp:TextBox>
                <%-- <asp:Button runat="server" ID="btnAddTieuChi" Text="Thêm" 
                    onclick="btnAddTieuChi_Click" />--%>

    <%-- </td>
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
                <C1WebGrid:C1WebGrid runat="server" ID="dataCtrl" Width="100%"
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
                                <asp:Label runat="server" ID="LoaiTieuChiID" Text='<%# DataBinder.Eval(Container.DataItem,"LoaiTieuChiID")%>'></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>

                        <C1WebGrid:C1BoundColumn HeaderText="Tên loại tiêu chí đánh giá" DataField="loai_tieu_chi">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Tỷ trọng" DataField="TYTRONG">
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
        });
        function bosungLoaiTieuChi() {
            var botieuchi = "<%=_botieuchi_documentid %>";
            var loaitieuchi = GetSvrCtlValue("DropDownLoaiTieuChi");
            var tytrong = GetSvrCtlValue("TyTrong");
            var url = "BoTieuChiDanhGiaCLKTV_LoaiTieuChi.aspx";
            var query = "act=bosungloaitieuchi";
            query += "&doc=" + botieuchi;
            query += "&loaitieuchi=" + loaitieuchi;
            query += "&tytrong=" + tytrong;
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
        function deleteLoaiTieuChi(LoaiTieuChiID) {
            var botieuchi = "<%=_botieuchi_documentid %>";
            var loaitieuchi = LoaiTieuChiID;
            var url = "BoTieuChiDanhGiaCLKTV_LoaiTieuChi.aspx";
            var query = "act=xoaloaitieuchi";
            query += "&doc=" + botieuchi;
            query += "&loaitieuchi=" + loaitieuchi;
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

