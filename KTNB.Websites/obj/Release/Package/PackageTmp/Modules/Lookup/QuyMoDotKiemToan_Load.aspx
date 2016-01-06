<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="QuyMoDotKiemToan_Load.aspx.cs" Inherits="VPB_TDHS.Modules.Lookup.QuyMoDotKiemToan_Load" %>
<%@ Register Assembly="C1.Web.C1Input.2" Namespace="C1.Web.C1Input" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <div class="form-horizontal">
       <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Quy mô<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_B041CD82_06B3_46F6_ABC1_897E5FBBF110" runat="server" CssClass="form-control" ></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Diễn giải</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_61E320B2_1AE9_4ADE_8E09_431CB5F5F697" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Số người<span class="star-red">*</span></label>
            <div class="col-sm-6">
               <cc1:C1WebNumericEdit ID="ID6_DE82AAE4_AE92_4435_B609_6673AA45C62F" runat="server"
                    Width="9%" Font-Size="Small" DecimalPlaces="0" Culture="en-US"
                    ThousandsSeparator="false" Value="1" SmartInputMode="false" MaxValue="1000" MinValue="0"
                    UpDownAlign="None" CssClass="form-control" Height="35px" BorderColor="#48B061">
                </cc1:C1WebNumericEdit>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Số ngày công<span class="star-red">*</span></label>
            <div class="col-sm-6">
               <cc1:C1WebNumericEdit ID="ID6_479E464B_0FB8_46D6_BF48_CF7F2DE518AE" 
            runat="server" Width="9%"  
            Font-Size="Small" DecimalPlaces="0" Culture="en-US"  ThousandsSeparator="false" 
            Value="1" SmartInputMode="false" MaxValue="10000" MinValue="0" UpDownAlign="None" CssClass="form-control" BorderColor="#48B061" Height="35px"></cc1:C1WebNumericEdit>
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
        <td style="width: 222px">Quy mô</td>
        <td>
            <asp:TextBox ID="ID8_B041CD82_06B3_46F6_ABC1_897E5FBBF110" runat="server" 
                SkinID="TextBoxRequired"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="width: 222px">Diễn giải</td>
        <td>
            <asp:TextBox ID="ID8_61E320B2_1AE9_4ADE_8E09_431CB5F5F697" runat="server" 
                SkinID="TextBox"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="width: 222px">Số người</td>
      <%--  <td>
            <asp:TextBox ID="ID6_DE82AAE4_AE92_4435_B609_6673AA45C62F" runat="server" 
                SkinID="TextBoxRequired"></asp:TextBox>
        </td>--%>
        <%--<td>
        <cc1:C1WebNumericEdit ID="ID6_DE82AAE4_AE92_4435_B609_6673AA45C62F" 
            runat="server" SkinID="C1WebNumeric" Width="6%"  
            Font-Size="Small" DecimalPlaces="0" Culture="en-US"  ThousandsSeparator="false" 
            Value="1" SmartInputMode="false" MaxValue="10000" MinValue="0" UpDownAlign="None"></cc1:C1WebNumericEdit>
        </td>
    </tr>
    <tr>
        <td style="width: 222px">Số ngày công</td>--%>
        <%--<td>
            <asp:TextBox ID="ID6_479E464B_0FB8_46D6_BF48_CF7F2DE518AE" runat="server" 
                SkinID="TextBoxRequired"></asp:TextBox>
        </td>--%>
         <%--<td>
         <cc1:C1WebNumericEdit ID="ID6_479E464B_0FB8_46D6_BF48_CF7F2DE518AE" 
            runat="server" SkinID="C1WebNumeric" Width="6%"  
            Font-Size="Small" DecimalPlaces="0" Culture="en-US"  ThousandsSeparator="false" 
            Value="1" SmartInputMode="false" MaxValue="10000" MinValue="0" UpDownAlign="None"></cc1:C1WebNumericEdit>
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
<script type="text/javascript">
    var _documentid;
    /*********************************************************/
    $(document).ready(function () {
        //do smt here
    });
    /*********************************************************/
    function preparesavedoc(documentID, doctypeID) {
        var ten = GetSvrCtlValue("ID8_B041CD82_06B3_46F6_ABC1_897E5FBBF110");
        var url = "QuyMoDotKiemToan_Load.aspx";
        var query = "act=checkvalue";
        query += "&p=B041CD82-06B3-46F6-ABC1-897E5FBBF110";
        query += "&v=" + ten;
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
                    alert("Dữ liệu nhập bị trùng lặp. Kiểm tra lại.");
            }
        });
    }
    function prepareupdatedoc(documentID) {
        var ten = GetSvrCtlValue("ID8_B041CD82_06B3_46F6_ABC1_897E5FBBF110");
        var url = "QuyMoDotKiemToan_Load.aspx";
        var query = "act=checkvalueupdate";
        query += "&p=B041CD82-06B3-46F6-ABC1-897E5FBBF110";
        query += "&v=" + ten;
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
    function savedoc_success() {
        alert(MSG_ADD_OK);
        window.location.href = "QuyMoDotKiemToan.aspx";
    }
    function savedoc_error() {
        alert(MSG_ADD_ER);
    }
    function update_success() {
        alert(MSG_EDIT_OK);
        window.location.href = "QuyMoDotKiemToan.aspx";
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
