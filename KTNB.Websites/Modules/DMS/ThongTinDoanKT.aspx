<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="ThongTinDoanKT.aspx.cs" Inherits="VPB_TDHS.Modules.DMS.ThongTinDoanKT" %>
<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <table width="100%">
   
</table>

<asp:ObjectDataSource runat="server" ID="ObjectDataSource1" SelectMethod="getListThanhVienDoanKiemToan"
    TypeName="VPB_KTNB.Helpers.DataSource">
    <SelectParameters>
        <asp:Parameter Name="DoanKT" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>

  <table width="70%">

    <tr class="GridHeader">
        <td colspan="2">thông tin đoàn kiểm toán</td>
    </tr>

    <tr>
        <td style="width: 222px">Tên trưởng đoàn</td>
        <td>
            <asp:TextBox  Enabled="false" ID="DOCNAME" runat="server" 
                SkinID="TextBoxReadOnly"></asp:TextBox>
        </td>
    </tr>

     <tr class="GridHeader">
            <td colspan="2">Danh sách thành viên đoàn kiểm toán</td>
    </tr>

    <tr>
            <td colspan="2">
                <C1WebGrid:C1WebGrid runat="server" ID="dataCtrl" Width="75%"
                    OnItemCreated="dataCtrl_OnItemCreated" OnItemDataBound="dataCtrl_OnItemDataBound"
                    GroupIndent="20" ItemStyle-Height="30px" DataSourceID="ObjectDataSource1">
                    <Columns>
                        <C1WebGrid:C1TemplateColumn Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="PK_UserID" Text='<%# DataBinder.Eval(Container.DataItem,"PK_UserID")%>'></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>
                        
                         <C1WebGrid:C1TemplateColumn Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="FK_DOCLINKID" Text='<%# DataBinder.Eval(Container.DataItem,"FK_DOCLINKID")%>'></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>

                          <C1WebGrid:C1BoundColumn HeaderText="Tên thành viên" DataField="UserName">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>

                        
                          <C1WebGrid:C1BoundColumn HeaderText="Tên đầy đủ" DataField="FullName">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>

                        <C1WebGrid:C1BoundColumn HeaderText="Phòng ban" DataField="GroupName">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="30%" />
                        </C1WebGrid:C1BoundColumn>
                        
                    </Columns>
                </C1WebGrid:C1WebGrid>
            </td>
        </tr>

</table>



<%--<table>
    <tr class="GridHeader">
        <td colspan="2">thông tin đợt kiểm toán</td>
    </tr>
    <tr>
        <td style="width: 222px">Năm</td>
        <td>
            <asp:TextBox  Enabled="false" ID="ID8_23FB0D9B_6A02_44B0_A1AA_CEFA895CD9C1" runat="server" 
                SkinID="TextBox"></asp:TextBox>
        </td>
    </tr>
   
      <tr>
        <td style="width: 222px">Tên đợt kiểm toán</td>
        <td>
        <asp:TextBox Enabled="false" ID="ID8_63A0C4B1_2088_4994_B891_2FF65EB20265" runat="server" 
                SkinID="TextBox"></asp:TextBox>
        </td>
    </tr>

     <tr>
        <td style="width: 222px">Loại đối tượng kiểm toán</td>
        <td>
           <asp:TextBox Enabled="false" ID="ID8_737694DF_DE17_4FF9_AE15_70E197C83593" runat="server" 
                SkinID="TextBox">
            </asp:TextBox>
        </td>
    </tr>

    
     <tr>
        <td style="width: 222px">Đối tượng kiểm toán</td>
        <td>
        <asp:TextBox Enabled="false" ID="ID8_2A4CA2AD_0282_4D57_86AC_D973D281EF54" runat="server" 
                SkinID="TextBox"></asp:TextBox>
        </td>
    </tr>

    <tr>
        <td style="width: 222px">Đơn vị thực hiện</td>
        <td>
        <asp:TextBox Enabled="false" ID="ID8_A8A3EDBA_F569_4C06_8A57_045FBFED55FD" runat="server" 
                SkinID="TextBox"></asp:TextBox>
        </td>
    </tr>

     <tr>
        <td style="width: 222px">Quy mô đợt kiểm toán</td>
        <td>
            <asp:TextBox Enabled="false" ID="ID8_06991CFA_158B_4460_8B7E_EC010C14DFE4" runat="server" 
                SkinID="TextBox">
            </asp:TextBox>
        </td>
    </tr>

     <tr>
        <td style="width: 222px">Thời gian thực hiện</td>
        <td>
        <asp:TextBox Enabled="false" ID="ID8_B23A439D_95D3_466E_8AD1_CDDAC260CB27" runat="server" 
                SkinID="TextBox" ></asp:TextBox>
        </td>
    </tr>
</table>--%>


<script type="text/javascript">
    var _documentid;
    /*********************************************************/
    $(document).ready(function () {
    });
    /*********************************************************/
    /*Insert 2 table
    + T_TYPE_DOC_PROPERTY
    + T_DOCLINK
    */
    function preparesavedoc(documentID, doctypeID) {
        var ten = GetSvrCtlValue("ID8_63A0C4B1_2088_4994_B891_2FF65EB20265");
        var url = "DotKiemToanNam_Load.aspx";
        var query = "act=checkvalue";
        query += "&p=63A0C4B1-2088-4994-B891-2FF65EB20265";
        query += "&v=" + ten;
        StartProcessingForm("");
        $.ajax({
            type: "POST",
            url: url,
            data: query,
            success: function (data) {
                FinishProcessingForm();
                if (data == "0") {
                    //alert("documentID" + documentID);
                    savedocument(documentID, doctypeID, savedoc_success, savedoc_error, null);
                    //savedocument(documentID, doctypeID, savedoc_success, savedoc_error);
                    //window.location.reload(true);
                }
                else {
                    alert(MSG_DATA_ESXIT);
                }
            }
        });
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