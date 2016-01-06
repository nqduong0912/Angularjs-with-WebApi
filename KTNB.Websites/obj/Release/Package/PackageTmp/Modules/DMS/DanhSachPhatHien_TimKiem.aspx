<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="DanhSachPhatHien_TimKiem.aspx.cs" Inherits="VPB_TDHS.Modules.DMS.DanhSachPhatHien_TimKiem" %>
<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    
    <table width="100%">
        <tr>
            <td style="width: 222px">Từ</td>
            <td>
                <asp:DropDownList  ID="ddlTuNgayThang" runat="server" 
                    SkinID="DropDownList"></asp:DropDownList>
                <asp:DropDownList  ID="ddlTuNgayNam" runat="server" 
                    SkinID="DropDownList"></asp:DropDownList>
            </td>
        </tr>
         <tr>
            <td style="width: 222px">Đến</td>
            <td>
                <asp:DropDownList  ID="ddlDenNgayThang" runat="server" 
                    SkinID="DropDownList"></asp:DropDownList>
                <asp:DropDownList  ID="ddlDenNgayNam" runat="server" 
                    SkinID="DropDownList"></asp:DropDownList>
            </td>
        </tr>

         <tr>
            <td style="width: 222px">Đối tượng kiểm toán</td>
            <td>
                <asp:DropDownList  ID="ddlLoaiDoiTuong" runat="server" 
                    SkinID="DropDownListRequired"></asp:DropDownList>
            </td>
        </tr>

        
         <tr>
            <td style="width: 222px">Trạng thái</td>
            <td>
                <asp:DropDownList  ID="ddlTrangThai" runat="server" 
                    SkinID="DropDownListRequired"></asp:DropDownList>
            </td>
        </tr>

         <tr>
            <td style="width: 222px"></td>
            <td>
<input class="SearchButton" type="button" onclick="javascript:TimKiemDotKiemToan();" value="Tìm kiếm" name="ctl00$FormContent$ctl00">
            </td>
        </tr>
    </table>
   <asp:ScriptManager runat="server" ID="ScriptManager1"></asp:ScriptManager>

<asp:ObjectDataSource runat="server" ID="ObjectDataSource1" SelectMethod="getTimKiemPhatHien"
    TypeName="DataSource">
    <SelectParameters>
        <asp:Parameter Name="TuNgay" Type="String" />
        <asp:Parameter Name="DenNgay" Type="String" />
        <asp:Parameter Name="DoiTuongKiemToan" Type="String" />
         <asp:Parameter Name="TrangThai" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>
    <table width="100%">
    <tr>
            <td>
             <asp:UpdatePanel runat="server" ID="updatepanel1" UpdateMode="Conditional" OnLoad="updatepanel1_OnLoad">
            <ContentTemplate>
                <c1webgrid:c1webgrid runat="server" ID="dataCtrl" Width="100%" 
                    OnItemCreated="dataCtrl_OnItemCreated" OnItemDataBound="dataCtrl_OnItemDataBound"
                    GroupIndent="20" ItemStyle-Height="30px" DataSourceID="ObjectDataSource1">
                    <Columns>
                  
                      
                        <C1WebGrid:C1TemplateColumn>
                            <ItemTemplate>
                              <asp:Image runat="server" ID="imgDotKT" ImageUrl="~/Images/viewdetail.gif" ToolTip="Chi tiết đợt kiểm toán"
                                    Style="cursor: pointer" />

                                <asp:Image runat="server" ID="imgEdit" ImageUrl="~/Images/contentsview.gif" ToolTip="Chi tiết phát hiện"
                                    Style="cursor: pointer" />
                            </ItemTemplate>
                            <ItemStyle Width="8%" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </C1WebGrid:C1TemplateColumn>

                        <C1WebGrid:C1TemplateColumn Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="PK_DocumentID" Text='<%# DataBinder.Eval(Container.DataItem,"PK_DocumentID")%>'></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>

                          <C1WebGrid:C1TemplateColumn Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="DotKiemToanID" Text='<%# DataBinder.Eval(Container.DataItem,"DotKiemToanID")%>'></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>

                         <C1WebGrid:C1TemplateColumn Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="Kieu" Text='<%# DataBinder.Eval(Container.DataItem,"Kieu")%>'></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>

                        <C1WebGrid:C1BoundColumn HeaderText="Năm" DataField="Nam" RowMerge="Restricted" Visible="false">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <GroupInfo HeaderText="Kế hoạch năm {0}" Position="Header" OutlineMode="StartCollapsed"></GroupInfo>
                        </C1WebGrid:C1BoundColumn>

                          <C1WebGrid:C1BoundColumn HeaderText="Loại đối tượng kiểm toán" DataField="LoaiDoiTuongKiemToan" RowMerge="Restricted" Visible="false">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <GroupInfo HeaderText="{0}" Position="Header" OutlineMode="StartCollapsed"></GroupInfo>
                        </C1WebGrid:C1BoundColumn>

                        <C1WebGrid:C1BoundColumn HeaderText="Tên đợt kiểm toán" DataField="DotKiemToan">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>

                         <C1WebGrid:C1BoundColumn HeaderText="Đối tượng kiểm toán" DataField="DoiTuongKiemToan">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>

                         <C1WebGrid:C1BoundColumn HeaderText="Thời gian dự kiến" DataField="ThoiGianDuKien">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>

                        <C1WebGrid:C1TemplateColumn HeaderText="Trạng thái">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="Status" Text='<%# DataBinder.Eval(Container.DataItem,"Status")%>'></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>


                         <C1WebGrid:C1BoundColumn HeaderText="Phát hiện" DataField="PhatHien">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>

                           <C1WebGrid:C1TemplateColumn Visible="true" HeaderText="Loại">
                           <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblLoai" Text='<%# DataBinder.Eval(Container.DataItem,"Kieu")%>'></asp:Label>
                            </ItemTemplate>
                        </C1WebGrid:C1TemplateColumn>

                         <C1WebGrid:C1BoundColumn HeaderText="Mức độ" DataField="MucDo">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>

                         <C1WebGrid:C1BoundColumn HeaderText="Chi tiết" DataField="ChiTiet">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>

                         <C1WebGrid:C1BoundColumn HeaderText="Ảnh hưởng" DataField="AnhHuong">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>

                           <C1WebGrid:C1BoundColumn HeaderText="Ghi chú" DataField="GhiChu">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>

                           <C1WebGrid:C1BoundColumn HeaderText="Khuyến nghị" DataField="KhuyenNghi">
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </C1WebGrid:C1BoundColumn>

                    </Columns>
                </c1webgrid:c1webgrid>  
                </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
       

    </table>
    <asp:HiddenField ID="hdLoaiDoiTuong" runat="server" />
    <asp:HiddenField ID="hdTuNgay" runat="server" />
    <asp:HiddenField ID="hdDenNgay" runat="server" />
    <asp:HiddenField ID="hdTrangThai" runat="server" />
<script type="text/javascript">
    /*********************************************************/
    $(document).ready(function () {
        //do smt here
    });
    /*********************************************************/
    function newform() {
        window.location.href = "DotKiemToanNam_Load.aspx";
    }
    function LoadDocumentHT(DocumentID) {
        url = "PhatHienHeThong_Load.aspx?act=loaddoc&doc=" + DocumentID+"&timkiem=tk";
        window.location.href = url;
    }

    function LoadDocumentVP(DocumentID) {
        url = "PhatHienViPham_Load.aspx?act=loaddoc&doc=" + DocumentID + "&timkiem=tk";
        window.location.href = url;
    }

    function LoadDocumentDotKT(DocumentID) {
        url = "DotKiemToanNam_Load.aspx?act=loaddoc&doc=" + DocumentID + "&timkiem=tk";
        window.location.href = url;
    }

    function LapDoanKiemToan(DocumentID) {
        //var url = "DoanKiemToan_Load.aspx?dotkt=" + DocumentID;
        var url = "../../Controls/Tab/ThanhLapDoanKT_Load.aspx?doc=" + DocumentID + "&act=dotkt"+"&timkiem=tk";
        window.location.href = url;
        //opendetail(url, "LapDoanKT");
    }

    function TimKiemDotKiemToan() {
        //var tungay = $("#ctl00_FormContent_txtTuNgay").val();
        //var denngay = $("#ctl00_FormContent_txtDenNgay").val();
        //format dd/MM/yyyy
        var tungay = '01/' + $("#ctl00_FormContent_ddlTuNgayThang").val().replace("Tháng ", "").trim() + '/' + $("#ctl00_FormContent_ddlTuNgayNam").val();
        var denngay = '01/' + $("#ctl00_FormContent_ddlDenNgayThang").val().replace("Tháng ", "").trim() + '/' + $("#ctl00_FormContent_ddlDenNgayNam").val();
        var loaidoituong = $("#ctl00_FormContent_ddlLoaiDoiTuong").val()
        var trangthai = $("#ctl00_FormContent_ddlTrangThai").val()
        if ((loaidoituong == '') || (loaidoituong == null) || (loaidoituong == 'undefined')) {
            alert("BẠN VUI LÒNG NHẬP ĐẦY ĐỦ THÔNG TIN VÀO CÁC TRƯỜNG ĐƯỢC YÊU CẦU.");
            return false;
        }


        $('#<%=hdTuNgay.ClientID %>').val(tungay);
        $('#<%=hdDenNgay.ClientID %>').val(denngay);
        $('#<%=hdLoaiDoiTuong.ClientID %>').val(loaidoituong);
        $('#<%=hdTrangThai.ClientID %>').val(trangthai);
        __doPostBack('<%=updatepanel1.ClientID %>', '');
    }

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ButtonExtendBefore" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ButtonExtend" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FormFooter" runat="server">
</asp:Content>