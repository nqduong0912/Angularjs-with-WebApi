<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="RankLoaiDTKT.aspx.cs" Inherits="VPB_KTNB.Modules.Lookup.CacDTKT.RankLoaiDTKT" %>
<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<%@ Register Assembly="C1.Web.C1Input.2" Namespace="C1.Web.C1Input" TagPrefix="cc1" %>
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
            <label class="col-sm-1 col-sm-offset-1 control-label">Loại ĐTKT<span class="star-red">*</span></label>
            <div class="col-sm-2">
                 <asp:DropDownList ID="ID8_D156BDD2_ACCB_4985_B95A_1429F44D65B6" runat="server" Height="30px"></asp:DropDownList>
            </div>
            <label for="inputDescription" class="col-sm-1 col-sm-offset-1 control-label">Rank<span class="star-red">*</span></label>
            <div class="col-sm-2">
                <cc1:C1WebNumericEdit ID="ID6_4A6A8527_1C1C_4526_AE82_5C1EE7FE3B05" runat="server"
                    SkinID="C1WebNumeric" Width="50%" Font-Size="Small" DecimalPlaces="0" Culture="en-US"
                    ThousandsSeparator="false"  Value="1" SmartInputMode="false"  MinValue="0"
                    UpDownAlign="None" Height="30px" CssClass="form-control">
                </cc1:C1WebNumericEdit>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-1 col-sm-offset-1 control-label">Từ điểm<span class="star-red">*</span></label>
            <div class="col-sm-2">
                <cc1:C1WebNumericEdit ID="ID6_DF099D26_D6F3_4965_B854_8CC2FE8A37E2" runat="server"
                    SkinID="C1WebNumeric" Width="50%" Font-Size="Small" DecimalPlaces="0" Culture="en-US"
                    ThousandsSeparator="false" Value="1" SmartInputMode="false"  MinValue="0" MaxValue="100"
                    UpDownAlign="None" Height="30px" CssClass="form-control">
                </cc1:C1WebNumericEdit>
            </div>
            <label for="inputDescription" class="col-sm-1 col-sm-offset-1 control-label">Đến điểm<span class="star-red">*</span></label>
            <div class="col-sm-2">
                <cc1:C1WebNumericEdit ID="ID6_F9A6DBEA_DFD3_4425_A628_C15E18DA997F" runat="server"
                    SkinID="C1WebNumeric" Width="50%" Font-Size="Small" DecimalPlaces="0" Culture="en-US"
                    ThousandsSeparator="false" Value="1" SmartInputMode="false"  MinValue="0" MaxValue="100"
                    UpDownAlign="None" Height="30px" CssClass="form-control">
                </cc1:C1WebNumericEdit>
            </div>
            <div class="col-sm-2">
                <asp:Button ID="btnThem" runat="server" Text="Thêm" Width="70px" Height="30px" CssClass="form-control"/>
            </div>
        </div>
        <div class="form-group" style="width:80%">
            <asp:Repeater ID="dataCtrl" runat="server">
            <HeaderTemplate>
                <div class="table-responsive">
                    <table class="vpb--table table table-striped table-bordered table-hover">
                        <thead>
                            <tr>
                                <th>Rank</th>
                                <th>Loại đối tượng kiểm toán</th>
                                <th>Từ điểm</th>
                                <th>Đến điểm</th>
                                <th>Thao tác</th>
                            </tr>
                        </thead>
                        <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Eval("Rank") %></td>
                    <td><%# Eval("TenLDTKT") %></td>
                    <td><%# Eval("MarkFrom") %></td>
                    <td><%# Eval("MarkTo") %></td>
                    <td class="text-center">
                        <a class="click-icon" href="javascript:;" title="Xóa" onclick="DeleteDocument('<%# Eval("PK_DocumentID") %>')">
                            <asp:Image runat="server" ImageUrl="~/Images/delete.gif" />
                        </a>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody>
                    </table>
                </div>
                <!-- /.table-responsive -->
            </FooterTemplate>
        </asp:Repeater>
            
        </div>
    </div>
<script type="text/javascript">
    /*********************************************************/
    $(document).ready(function () {
        //do smt here
    });
    /*********************************************************/
    function DeleteDocument(documentid)
    {
        deletedocument(documentid, delete_success, delete_error, MSG_DEL_QUESTION);
    }
    function preparesavedoc(documentID, doctypeID) {
        var loaiDTKT = GetSvrCtlValue("ID8_D156BDD2_ACCB_4985_B95A_1429F44D65B6");
        var rank = GetSvrCtlValue("ID6_4A6A8527_1C1C_4526_AE82_5C1EE7FE3B05");
        var from = GetSvrCtlValue("ID6_DF099D26_D6F3_4965_B854_8CC2FE8A37E2");
        var to = GetSvrCtlValue("ID6_F9A6DBEA_DFD3_4425_A628_C15E18DA997F");
        if (parseInt(from) > parseInt(to))
        {
            alert("Giá trị Từ điểm không được lớn hoản giá trị Đến điểm.");
            return;
        }
        var url = "RankLoaiDTKT.aspx";
        var query = "act=checkvalue";
        query += "&ldtkt=" + loaiDTKT;
        query += "&rank=" + rank;
        query += "&from=" + from;
        query += "&to=" + to;
        StartProcessingForm("");
        $.ajax({
            type: "POST",
            url: url,
            data: query,
            success: function (data) {
                FinishProcessingForm();
                if (data == "0") {
                    savedocument(documentID, doctypeID, savedoc_success, savedoc_error);
                }
                else if (data == "1") {
                    alert("Rank ứng với loại đối tượng kiểm toán đã tồn tại. Kiểm tra lại.");
                }
                else if (data == "2") {
                    alert("Khoảng dữ liệu bị trùng lặp. Kiểm tra lại.");
                }
            }
        });
    }
    function savedoc_success() {
        alert(MSG_ADD_OK);
        location.reload();
    }
    function savedoc_error() {
        alert(MSG_ADD_ER);
    }
    function delete_success() {
        alert(MSG_DEL_OK);
        location.reload();
    }
    function delete_error() {
        alert(MSG_DEL_ER);
    }
</script>
</asp:Content>