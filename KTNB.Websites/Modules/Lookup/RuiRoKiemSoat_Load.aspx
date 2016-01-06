<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="RuiRoKiemSoat_Load.aspx.cs" Inherits="VPB_TDHS.Modules.Lookup.RuiRoKiemSoat_Load" %>

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
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Mục tiêu kiểm soát</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="ID8_236D789E_8EE2_4ACC_B960_1C47144ACF90" runat="server" CssClass="form-control" Width="300px">
                    </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Tên rủi ro kiểm soát<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_33686451_E1A7_4F11_9D8D_A0D697D0D5B6" runat="server" CssClass="form-control" ></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Diễn giải</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_A697DFE7_58FD_4FF3_87FA_FBD630B631FF" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control"></asp:TextBox>
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

    <%--<table width="100%">

        <tr>
            <td style="width: 222px">Mục tiêu kiểm soát</td>
            <td>
                <asp:DropDownList ID="ID8_236D789E_8EE2_4ACC_B960_1C47144ACF90" runat="server"
                    SkinID="DropDownListRequired" Width="300px">
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td style="width: 222px">Tên rủi ro kiểm soát</td>
            <td>
                <asp:TextBox ID="ID8_33686451_E1A7_4F11_9D8D_A0D697D0D5B6" runat="server"
                    SkinID="TextBoxRequired" Width="80%"></asp:TextBox>
            </td>
        </tr>



        <tr>
            <td style="width: 222px">Diễn giải</td>
            <td>
                <asp:TextBox ID="ID8_A697DFE7_58FD_4FF3_87FA_FBD630B631FF" runat="server"
                    SkinID="TextBox"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td style="width: 222px">Trạng thái</td>
            <td>
                <asp:DropDownList ID="DOCSTATUS" runat="server"
                    SkinID="DropDownList">
                </asp:DropDownList>
            </td>
        </tr>
    </table>--%>

    <table width="70%">
        <tr>
            <td>
                <C1WebGrid:C1WebGrid runat="server" ID="dataCtrl" Width="100%"
                    OnItemCreated="dataCtrl_OnItemCreated" OnItemDataBound="dataCtrl_OnItemDataBound"
                    GroupIndent="20" ItemStyle-Height="30px" DataSourceID="ObjectDataSource1">
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


                        <C1WebGrid:C1BoundColumn HeaderText="Tên kiểm soát" DataField="Tên kiểm soát">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>


                        <C1WebGrid:C1BoundColumn HeaderText="Diễn giải" DataField="Diễn giải">
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
            $('#ctl00_FormContent_ID8_236D789E_8EE2_4ACC_B960_1C47144ACF90 option').each(function () {
                var value = $(this).val();
                $(this).attr('title', value);

            });


        });
        /*********************************************************/
        function preparesavedoc(documentID, doctypeID) {
            var ten = GetSvrCtlValue("ID8_33686451_E1A7_4F11_9D8D_A0D697D0D5B6");
            var muctieukiemsoat = $("#ctl00_FormContent_ID8_236D789E_8EE2_4ACC_B960_1C47144ACF90").val();
            var url = "RuiRoKiemSoat_Load.aspx";
            var query = "act=checkvalue";
            query += "&p=33686451-E1A7-4F11-9D8D-A0D697D0D5B6";
            query += "&v=" + ten;
            query += "&muctieukiemsoat=" + muctieukiemsoat;
            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (data) {
                    FinishProcessingForm();
                    if (data == "0")
                        //alert("OK");
                        savedocument(documentID, doctypeID, savedoc_success, savedoc_error);
                    else
                        alert(MSG_DATA_ESXIT);
                }
            });
        }

        function prepareupdatedoc(documentID) {
            var ten = GetSvrCtlValue("ID8_33686451_E1A7_4F11_9D8D_A0D697D0D5B6");
            var muctieukiemsoat = $("#ctl00_FormContent_ID8_236D789E_8EE2_4ACC_B960_1C47144ACF90").val();

            var url = "RuiRoKiemSoat_Load.aspx";
            var query = "act=checkvalueupdate";
            query += "&p=33686451-E1A7-4F11-9D8D-A0D697D0D5B6";
            query += "&v=" + ten;
            query += "&doc=" + documentID;
            query += "&muctieukiemsoat=" + muctieukiemsoat;
            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (data) {
                    FinishProcessingForm();
                    if (data == "0")
                        //alert("OK");
                        updatedocument(documentID, update_success, update_error);
                    else
                        alert(MSG_DATA_ESXIT);
                }
            });
        }

        function LoadDocument(DocumentID) {
            url = "KiemSoat_Load.aspx?act=loaddoc&doc=" + DocumentID;
            window.location.href = url;
        }

        function savedoc_success() {
            alert(MSG_ADD_OK);
            window.location.href = 'RuiRoKiemSoat.aspx';
        }
        function savedoc_error() {
            alert(MSG_ADD_ER);
        }
        function update_success() {
            alert(MSG_EDIT_OK);
            window.location.href = 'RuiRoKiemSoat.aspx';
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
