<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true"
    CodeBehind="MangNghiepVu_Load.aspx.cs" Inherits="VPB_TDHS.Modules.Lookup.MangNghiepVu_Load" %>

<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">

    <asp:ObjectDataSource runat="server" ID="ObjectDataSource1" SelectMethod="getDocumentList"
        TypeName="VPB_KTNB.Helpers.DataSource">

        <SelectParameters>
            <asp:Parameter Name="DocumentTypeID" Type="String" />
            <asp:Parameter Name="DocFields" Type="String" />
            <asp:Parameter Name="PropertyFields" Type="String" />
            <asp:Parameter Name="Condition" Type="String" />
        </SelectParameters>

    </asp:ObjectDataSource>
    <div class="form-horizontal">
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Tên mảng nghiệp vụ<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_144DFC1C_5D45_4FE7_8772_CD573CEFD04F" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <%--Hidden Field--%>
        <div class="form-group" style="display: none">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Loại đối tượng kiểm toán</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="ID8_B65E8D96_F253_40AB_A645_D210E09DA504" runat="server"></asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Diễn giải</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_E577E063_C972_4D5D_8B55_38B8737C7D03" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Trạng thái</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="DOCSTATUS" runat="server" CssClass="form-control" Width="100px">
                </asp:DropDownList>
            </div>
        </div>
    </div>
    <%--    <table width="100%">
        <tr>
            <td style="width: 222px">
                Tên mảng nghiệp vụ
            </td>
            <td>
                <asp:TextBox ID="ID8_144DFC1C_5D45_4FE7_8772_CD573CEFD04F" runat="server" SkinID="TextBoxRequired"></asp:TextBox>
            </td>
        </tr>
        <tr runat="server" ID="trLoaiDoiTuong">
            <td style="width: 222px">
                Loại đối tượng kiểm toán
            </td>
            <td>
                
            </td>
        </tr>
        <asp:HiddenField Value="Empty" ID="ID8_B65E8D96_F253_40AB_A645_D210E09DA504" runat="server" >
                </asp:HiddenField>
        <tr>
            <td style="width: 222px">
                Diễn giải
            </td>
            <td>
                <asp:TextBox ID="ID8_E577E063_C972_4D5D_8B55_38B8737C7D03" runat="server" SkinID="TextBox"></asp:TextBox>
            </td>
        </tr>
        
        <tr>
            <td style="width: 222px">
                Trạng thái
            </td>
            <td>
                <asp:DropDownList ID="DOCSTATUS" runat="server" SkinID="DropDownList">
                </asp:DropDownList>
            </td>
        </tr>
        
    </table>--%>
    <table width="70%">
        <tr>
            <td>
                <C1WebGrid:C1WebGrid runat="server" ID="dataCtrl" Width="100%" OnItemCreated="dataCtrl_OnItemCreated"
                    OnItemDataBound="dataCtrl_OnItemDataBound" GroupIndent="20" ItemStyle-Height="30px" DataSourceID="ObjectDataSource1">
                    <Columns>
                        <C1WebGrid:C1TemplateColumn>
                            <ItemTemplate>
                                <asp:Image runat="server" ID="imgEdit" ImageUrl="~/Images/viewdetail.gif" ToolTip="Chi tiết"
                                    Style="cursor: pointer" />
                            </ItemTemplate>
                            <ItemStyle Width="8%" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1TemplateColumn Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="PK_DocumentID" Text='<%# DataBinder.Eval(Container.DataItem,"PK_DocumentID")%>'></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Tên mục tiêu kiểm soát" DataField="Tên Mục tiêu kiểm soát">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1BoundColumn HeaderText="Diễn giải" DataField="Diễn Giải">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>
                        <C1WebGrid:C1TemplateColumn Visible="true" HeaderText="Trạng thái">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="Status" Text='<%# DataBinder.Eval(Container.DataItem,"Status")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="14%" HorizontalAlign="Right" VerticalAlign="Middle" />
                        </C1WebGrid:C1TemplateColumn>
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
        /*********************************************************/
        function preparesavedoc(documentID, doctypeID) {
            var ten = GetSvrCtlValue("ID8_144DFC1C_5D45_4FE7_8772_CD573CEFD04F");
            //            var loaidoituong = GetSvrCtlValue("ID8_B65E8D96_F253_40AB_A645_D210E09DA504");
            var url = "MangNghiepVu_Load.aspx";
            var query = "act=checkvalue";
            query += "&p=144DFC1C-5D45-4FE7-8772-CD573CEFD04F";
            query += "&v=" + ten;
            //            query += "&ploaidoituong=B65E8D96-F253-40AB-A645-D210E09DA504";
            //            query += "&vloaidoituong=" + loaidoituong;
            query += "&doc=" + documentID;
            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (data) {
                    FinishProcessingForm();
                    if (data == "0")
                        savedocument(documentID, doctypeID, savedoc_success, savedoc_error);
                    else
                        alert(MSG_DATA_ESXIT);
                }
            });
        }


        function prepareupdatedoc(documentID) {
            var ten = GetSvrCtlValue("ID8_144DFC1C_5D45_4FE7_8772_CD573CEFD04F");
            //            var loaidoituong = GetSvrCtlValue("ID8_B65E8D96_F253_40AB_A645_D210E09DA504");
            var url = "MangNghiepVu_Load.aspx";
            var query = "act=checkvalueupdate";
            query += "&p=144DFC1C-5D45-4FE7-8772-CD573CEFD04F";
            query += "&v=" + ten;
            //            query += "&ploaidoituong=B65E8D96-F253-40AB-A645-D210E09DA504";
            //            query += "&vloaidoituong=" + loaidoituong;
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
                        alert(MSG_DATA_ESXIT);
                }
            });
        }
        function LoadDocument(DocumentID) {
            url = "MucTieuKiemSoat_Load.aspx?act=loaddoc&doc=" + DocumentID;
            window.location.href = url;
        }
        function savedoc_success() {
            alert(MSG_ADD_OK);
            window.location.href = 'MangNghiepVu.aspx';

        }
        function savedoc_error() {
            alert(MSG_ADD_ER);
        }
        function update_success() {
            alert(MSG_EDIT_OK);
            window.location.href = 'MangNghiepVu.aspx';

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
