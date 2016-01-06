<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" Inherits="VPB_PROMOTION.CFG.Modules_CFG_Parameters" Codebehind="Parameters.aspx.cs" %>
<asp:Content ID="frmContent" ContentPlaceHolderID="FormContent" Runat="Server">
    <table style="width:100%; background-color:Black">
        <tr >
            <td align="left" style="width: 89px; color:White" class="lblNormal">Nhóm tham số</td>
            <td>
                <asp:TextBox ID="txtGroupName" runat="server" SkinID="TextBox" ForeColor="Red" Font-Bold="true"></asp:TextBox>
                <asp:DropDownList runat="server" ID="cboGroupName" SkinID="DropDownList"></asp:DropDownList>
            </td>
        </tr>
        <tr class="lblNormal">
            <td align="left" style="width: 89px; color:White" class="lblNormal">Ứng dụng</td>
            <td>
                <asp:DropDownList ID="cboApplication" runat="server" SkinID="DropDownList">
                </asp:DropDownList>
            </td>
        </tr>
        <tr class="lblNormal">
            <td align="left" style="width: 89px; color:White" class="lblNormal">Kiểu tham số</td>
            <td>
                <asp:DropDownList ID="cboParamType" runat="server" SkinID="DropDownList" Font-Bold="true">
                </asp:DropDownList>
            </td>
        </tr>
        <tr class="lblNormal">
            <td align="left" style="width: 89px; color:White; " class="lblNormal">(<font color='red'>*</font>) Tên tham số</td>
            <td>
                <asp:TextBox ID="txtName" runat="server" SkinID="TextBox" ReadOnly="true" ForeColor="Red" Font-Bold="true"></asp:TextBox>
            </td>
        </tr>
        <tr class="lblNormal">
            <td align="left" style="width: 89px;color:White;" class="lblNormal">(<font color='red'>*</font>) Giá trị</td>
            <td>
                <asp:TextBox ID="txtValue" runat="server" SkinID="TextBox" Width="97%" ForeColor="Blue" Font-Bold="true"></asp:TextBox>
            </td>
        </tr>
        <tr class="lblNormal">
            <td align="left" style="width: 89px;color:White" class="lblNormal">Ghi chú</td>
            <td>
                <asp:TextBox ID="txtFullName" runat="server" SkinID="TextBox" Width="97%"></asp:TextBox>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        function delform() {
            var action = "del";
            var docspace = "<%=_docspace %>";
            var name = $("#ctl00_FormContent_txtName").attr("value");
            if (window.confirm("Bạn thực sự muốn xóa tham số này ?")) {
                $.post("Parameters.aspx"
                    , { a: action,
                        id: name,
                        name: "",
                        fullname: ""
                    }
                    , function(data) {
                    alert(data);
                        var url = "../MainView/FolderViewer.aspx?op=&docspace=" + docspace;
                        window.open(url, "fraToc");
                        window.location = "Parameters.aspx?a=new&docspace=" + docspace;
                    }
              );
            }
        }
        function switchgroup(groupname) {
            $("#ctl00_FormContent_txtGroupName").attr("value", groupname);
            var nameselected = $("#ctl00_FormContent_txtGroupName").attr("value");
            if (nameselected == undefined)
                $("#ctl00_FormContent_txtGroupName").focus();
            else
                $("#ctl00_FormContent_txtName").focus();
        }
        function saveform() {
            var action = "<%=_action %>";
            var docspace = "<%=_docspace %>";
            var name = $("#ctl00_FormContent_txtName").attr("value");
            var value = $("#ctl00_FormContent_txtValue").attr("value");
            var fullname = $("#ctl00_FormContent_txtFullName").attr("value");
            var groupname = $("#ctl00_FormContent_txtGroupName").attr("value");if (groupname == undefined) groupname = "";
            var ungdung = $("#ctl00_FormContent_cboApplication option:selected").val(); if (ungdung == undefined) ungdung = "";
            var kieuthamso = $("#ctl00_FormContent_cboParamType option:selected").val();if (kieuthamso == undefined) kieuthamso = "";
            
            if (action == "new") {
                if (name == undefined) {
                    $("#ctl00_FormContent_txtName").focus();
                    return;
                }
            }
            if (value == undefined) {
                $("#ctl00_FormContent_txtValue").focus();
                return false;
            }

            if (action == "") action = "update";

            $.post("Parameters.aspx"
                    , { a: action,
                        id: name,
                        name: value,
                        groupname: groupname,
                        fullname: fullname,
                        ungdung: ungdung,
                        kieuthamso: kieuthamso
                    }
                    , function(data) {
                        alert(data);
                        if (action == "new") {
                            var url = "../MainView/FolderViewer.aspx?op=&docspace=" + docspace;
                            window.open(url,"fraToc");
                        }
                    }
              );

        }
        function verifyname(obj) {
            var para_name = obj.value;
            var url = "Parameters.aspx";
            var query = "a=verifyname&name=" + para_name;
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function(data) {
                    if (data == "duplicated") {
                        alert('Đặt tên tham số bị trùng lặp. Hãy đặt tên khác.');
                        obj.focus();
                    }
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="frmB1" ContentPlaceHolderID="ButtonExtendBefore" Runat="Server">
</asp:Content>
<asp:Content ID="frmB2" ContentPlaceHolderID="ButtonExtend" Runat="Server">
</asp:Content>

