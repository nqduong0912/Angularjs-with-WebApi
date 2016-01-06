<%@ Page Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" Inherits="Modules_Report_DataReported" Title="Untitled Page" Codebehind="DataReported.aspx.cs" %>
<%@ OutputCache Duration="1" NoStore="true" VaryByParam="None" %>
<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" Runat="Server">
    <table class="Table" style="width:100%">
        <tr>
            <td>
                <asp:ObjectDataSource runat="server" ID="ObjectDataSource1" SelectMethod="RPT_CUS_GEN_TERM_STATE" TypeName="DataSource">
                    <SelectParameters>
                        <asp:Parameter Name="MM" Type="Int16" />
                        <asp:Parameter Name="YYYY" Type="Int16" />
                        <asp:Parameter Name="State" Type="String" />
                        <asp:Parameter Name="Rank" Type="String" />
                        <asp:Parameter Name="Sector" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <C1WebGrid:C1WebGrid runat="server" ID="dataCtrl" Width="100%" 
                  DataSourceID="ObjectDataSource1" 
                  OnItemCreated="dataCtrl_OnItemCreated"
                  OnItemDataBound="dataCtrl_OnItemDataBound">
                        <Columns>
                            <C1WebGrid:C1BoundColumn HeaderText="TT" DataField="STT"></C1WebGrid:C1BoundColumn>
                            <C1WebGrid:C1BoundColumn HeaderText="CN" DataField="CHI NHANH"></C1WebGrid:C1BoundColumn>
                            <C1WebGrid:C1BoundColumn HeaderText="CIF" DataField="MA KHACH HANG"></C1WebGrid:C1BoundColumn>
                            <C1WebGrid:C1BoundColumn HeaderText="Khách hàng" DataField="HO TEN"></C1WebGrid:C1BoundColumn>
                            <C1WebGrid:C1BoundColumn HeaderText="Loại KH" DataField="LOAI KH"></C1WebGrid:C1BoundColumn>
                            <C1WebGrid:C1BoundColumn HeaderText="Ngày sinh" DataField="NGAY SINH"></C1WebGrid:C1BoundColumn>
                            <C1WebGrid:C1BoundColumn HeaderText="ĐT" DataField="DIEN THOAI"></C1WebGrid:C1BoundColumn>
                            <C1WebGrid:C1BoundColumn HeaderText="Địa chỉ" DataField="DIA CHI"></C1WebGrid:C1BoundColumn>
                            
                            <C1WebGrid:C1TemplateColumn HeaderText="SDBQN">
                                <ItemTemplate>
                                    <div align="right"><asp:Label runat="server" ID="SDBQN" BackColor="Yellow" Text='<%# DataBinder.Eval(Container.DataItem,"SDBQN")%>'></asp:Label></div>
                                </ItemTemplate>
                            </C1WebGrid:C1TemplateColumn>
                            
                            <C1WebGrid:C1BoundColumn HeaderText="Hạng" DataField="HANG"></C1WebGrid:C1BoundColumn>
                            <C1WebGrid:C1BoundColumn HeaderText="Ngày hết hiệu lực" DataField="NGAY HET HIEU LUC"></C1WebGrid:C1BoundColumn>
                            <C1WebGrid:C1BoundColumn HeaderText="Trạng thái" DataField="TRANG THAI"></C1WebGrid:C1BoundColumn>
                        </Columns>
                </C1WebGrid:C1WebGrid>            
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        /******************************************************************************************************************************************/
        function GetData2CSV()
        {
            var m="<%=_month %>";
            var y="<%=_year %>";
            var state="<%=_state %>";
            var rank ="<%=_rank %>";
            var sector="<%=_sector %>";
            var action="exportcsv";
            var qry="m=" + m + "&y=" + y + "&s=" + state + "&r=" + rank + "&sec=" + sector + "&a=" + action;
            var url="DataReported.aspx";
            StartProcessingForm("");
            $.ajax({
            type: "POST",
            url: url,
            data: qry,
            success: function(msg){
                FinishProcessingForm();
                if(msg!="-1")
                    window.open("../../Modules/Download/" + msg);
            }
            });
            return false;
        }
        /******************************************************************************************************************************************/
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ButtonExtendBefore" Runat="Server">
    <asp:RadioButton ID="RadioButton1" runat="server" Checked="True" 
                    GroupName="ExportFormat" Text="CSV file" />
                <asp:RadioButton ID="RadioButton2" runat="server" GroupName="ExportFormat" 
                    Text="PDF report file" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ButtonExtend" Runat="Server">
</asp:Content>

