<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="QuanLyLoaiDoiTuongKiemToan_Input.aspx.cs" Inherits="VPB_KTNB.Modules.Lookup.DanhMuc.KeHoachNam.QuanLyLoaiDoiTuongKiemToan_Input" %>
<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <asp:ObjectDataSource runat="server" ID="ObjectDataSource1" SelectMethod="getDocumentList"
        TypeName="DataSource">
        <SelectParameters>
            <asp:Parameter Name="DocumentTypeID" Type="String" />
            <asp:Parameter Name="DocFields" Type="String" />
            <asp:Parameter Name="PropertyFields" Type="String" />
            <asp:Parameter Name="Condition" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <div class="form-horizontal">
        <div class="form-group">
            <label class="col-sm-3 col-sm-offset-1 control-label">Năm<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:DropDownList ID="drpYears" runat="server" AutoPostBack="true" Height="30px">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-3 col-sm-offset-1 control-label">Đơn vị<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:DropDownList ID="drpDonVi" runat="server" AutoPostBack="true" Height="30px"> 
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-3 col-sm-offset-1 control-label">Chọn loại ĐTKT</label>
            <div class="col-sm-6">
                <C1WebGrid:C1WebGrid runat="server" ID="dataCtrl" Width="100%" DataSourceID="ObjectDataSource1"
                    OnItemCreated="dataCtrl_OnItemCreated" OnItemDataBound="dataCtrl_OnItemDataBound" AllowPaging="true" PageSize="10">
                        <Columns>
                            <C1WebGrid:C1TemplateColumn>
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkItem"></asp:CheckBox>
                                </ItemTemplate>
                            </C1WebGrid:C1TemplateColumn>
                            <C1WebGrid:C1TemplateColumn Visible="false">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="PK_DocumentID" Text='<%# DataBinder.Eval(Container.DataItem,"PK_DocumentID")%>'></asp:Label>
                                </ItemTemplate>
                            </C1WebGrid:C1TemplateColumn>
                            <C1WebGrid:C1BoundColumn HeaderText="Tên" DataField="Tên loại đối tượng kiểm toán">
                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            </C1WebGrid:C1BoundColumn>
                            <C1WebGrid:C1BoundColumn HeaderText="Diễn giải" DataField="Diễn Giải">
                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            </C1WebGrid:C1BoundColumn>
                        </Columns>
                </C1WebGrid:C1WebGrid>
            </div>
        </div>
    </div>
    <asp:ScriptManager ID='ScriptManager1' runat='server' EnablePageMethods='true' />
    <script type="text/javascript">
        var _documentid;
        var oldList = [];
        var listDocument = [];
        /*********************************************************/
        $(document).ready(function () {
            //$('[id*="chkItem"]').each(function () {
            //    if (this.checked) {
            //        oldList.push($(this).parent().attr('documentID'));
            //    }
            //})
        });
        /*********************************************************/
        function ThemLoaiDTKT(id, ten, diengiai, donvi, nam) {
            PageMethods.themLoaiDTKT(id, ten, diengiai, donvi, nam, Success, Failure);
        }
        function Success(result) {
            alert(result);
        }
        function Failure(error) {
            
        }
        function preparesavedoc(documentID, doctypeID) {
            var count = 0;
            $('[id*="chkItem"]').each(function () {
                if (this.checked) {
                    count++;
                }
            })
            if (count == 0)
            {
                alert("Chọn ít nhất 1 loại ĐTKT.");
                return;
            }
            var r = confirm(MSG_ADD_QUESTION);
            if (!r) return false;
            $('[id*="chkItem"]').each(function () {
                if (this.checked) {
                    //listDocument.push($(this).parent().attr('documentID'));
                    var id = $(this).parent().attr('documentID');
                    var ten = $(this).parent().attr('tenLDTKT');
                    var diengiai = $(this).parent().attr('diengiai');
                    var donvi = GetSvrCtlValue("drpDonVi");
                    var nam = GetSvrCtlValue("drpYears");
                    ThemLoaiDTKT(id, ten, diengiai, donvi, nam);
                }
            })
            savedoc_success();
        }
        
        function savedoc_success() {
            //alert(MSG_ADD_OK);
            window.location.href = 'QuanLyLoaiDoiTuongKiemToan.aspx?y=' + GetSvrCtlValue("drpYears") + "&dv=" + GetSvrCtlValue("drpDonVi");
        }
        function savedoc_error() {
            alert(MSG_ADD_ER);
        }
    </script>
</asp:Content>
