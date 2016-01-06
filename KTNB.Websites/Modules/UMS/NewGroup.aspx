<%@ Page Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" Inherits="VPB_PROMOTION.UMS.UMS_NewGroup" CodeBehind="NewGroup.aspx.cs" %>

<asp:Content runat="server" ID="NewUser" ContentPlaceHolderID="FormContent">
    <div class="form-horizontal ">
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label" id="lblMaCN">Mã CN/PGD<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="CompanyCode" runat="server" CssClass="form-control"></asp:TextBox>
                <img src="../../Images/indicator.gif" style="display: none" id="wait" />
                <a style="display: none">
                    <input type="checkbox" id="chkTrungTamHT" onclick='toogleCNQuanLy();'>Trung tâm hỗ trợ
                </a>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label" >Tên gợi nhớ<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="Mnemonic" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label" >Tên đầy đủ<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="CompanyName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Diễn giải</label>
            <div class="col-sm-6">
                <asp:TextBox ID="ID8_4E6F4C58_F130_451B_9779_3434753A53BA" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group" style="display: none">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Trạng thái</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="DOCSTATUS" runat="server" CssClass="form-control" Width="100px">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for="inputDescription" class="col-sm-3 col-sm-offset-1 control-label">Cấp quản lý</label>
            <div class="col-sm-6">
                <asp:DropDownList ID="cboBranchCodes" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group" style="display: none">
            <div class="col-sm-offset-4 col-sm-2">
                <asp:CheckBox runat="server" ID="IsExpired" Checked />
                <label for="ex2-a">Kích hoạt</label>
            </div>
        </div>
    </div>
    <%--<table style="width: 100%">
        <!--thong tin chi tiet-->
        <tr class="GridHeader">
            <td colspan="4">Thông tin chi tiết</td>
        </tr>
        <tr class="lblNormal">
            <td align="left" style="width: 129px" id="lblMaCN">Mã CN/PGD</td>
            <td>
                <asp:TextBox ID="CompanyCode" runat="server" SkinID="TextBoxRequired"></asp:TextBox>
                <img src="../../Images/indicator.gif" style="display: none" id="wait" />
                <a style="display: none">
                    <input type="checkbox" id="chkTrungTamHT" onclick='toogleCNQuanLy();'>Trung tâm hỗ trợ
                </a>
            </td>
            <td></td>
            <td style="width: 118px">
        </tr>
        <tr class="lblNormal">
            <td style="width: 129px" align="left">Tên gợi nhớ</td>
            <td>
                <asp:TextBox ID="Mnemonic" runat="server" SkinID="TextBoxRequired"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
            <td style="width: 118px">&nbsp;</td>
        </tr>

        <tr class="lblNormal">
            <td style="width: 129px" align="left">Tên đầy đủ</td>
            <td>
                <asp:TextBox runat="server" ID="CompanyName" SkinID="TextBoxRequired" Style="width: 94%"></asp:TextBox></td>
            <td></td>
            <td style="width: 118px"></td>
        </tr>

        <!--trang thai-->
        <tr class="lblNormal">
            <td align="left" style="width: 129px">Cấp quản lý</td>
            <td>
                <asp:DropDownList ID="cboBranchCodes" runat="server" SkinID="DropDownList" Width="75%">
                </asp:DropDownList></td>
            <td></td>
            <td style="width: 118px"></td>
        </tr>
        <tr class="lblCaption" style="display: none">
            <td colspan="4">Trạng thái</td>
        </tr>
        <tr class="lblNormal" style="display: none">
            <td style="width: 129px"></td>
            <td>
                <asp:CheckBox runat="server" ID="IsExpired" Checked />Kích hoạt</td>
            <td></td>
            <td style="width: 118px"></td>
        </tr>
    </table>--%>

    <script type="text/javascript">
        $(document).ready(function () {
            var m_roleID = new String("<%=_m_roleID %>");
            if (m_roleID.toUpperCase() == "DC064BD2-35B2-4A39-A3F6-6B45F79A21B9") {
                $("#lblMaCN").html("Mã đơn vị KTNB");

            }
        });

        function createnewgroup() {
            var docspace = "<%=_docspace %>";
            var query = "";
            query = parsingform();
            if (query != undefined) {
                $("#ctl00_btnSAVE").hide();
                $.ajax({
                    type: "POST",
                    url: "NewGroup.aspx",
                    data: query,
                    success: function (data) {
                        alert(data);
                        $("#ctl00_btnSAVE").hide();
                        parent["fraToc"].location.reload();
                        window.location.href = "NewGroup.aspx";
                    }
                });
            }
        }
        function parsingform() {

            var grouptype = 0;
            if ($("#chkTrungTamHT").attr("checked"))
                grouptype = 1;

            var groupname = $("#ctl00_FormContent_CompanyCode").val();
            if (groupname.length == 0) {
                $("#ctl00_FormContent_CompanyCode").focus();
                return;
            }

            var mnemonic = $("#ctl00_FormContent_Mnemonic").val();
            if (mnemonic.length == 0) {
                $("#ctl00_FormContent_Mnemonic").focus();
                return;
            }

            var groupdes = $("#ctl00_FormContent_CompanyName").val();
            if (groupdes.length == 0) {
                $("#ctl00_FormContent_CompanyName").focus();
                return;
            }

            var parentgroup = $("#ctl00_FormContent_cboBranchCodes").val();
            var isexpired = $("#ctl00_FormContent_IsExpired").attr("checked");
            if (isexpired)
                isexpired = 0;
            else
                isexpired = 1;

            var query = "act=new&name=" + groupname + "&mnemonic=" + mnemonic + "&desc=" + groupdes + "&isexpired=" + isexpired + "&parentgrp=" + parentgroup + "&type=" + grouptype;
            return query;
        }
        function verifycocode(obj) {
            if (obj.value == "") return;
            var url = "NewGroup.aspx";
            var action = "CHECKCOCODE";
            var query = "act=" + action + "&name=" + obj.value;
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (data) {
                    if (data == "True") {
                        alert('CN/PGD này đã có. Bạn hãy nhập tên khác.');
                        obj.focus();
                    }
                }
            });
        }
        function verifymnemonic(obj) {
            if (obj.value == "") return;
            var url = "NewGroup.aspx";
            var action = "CHECKMNEMONIC";
            var query = "act=" + action + "&mnemonic=" + obj.value;
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (data) {
                    if (data == "dupplicated") {
                        alert('Tên gợi nhớ này đã có. Bạn hãy nhập tên khác.');
                        obj.focus();
                    }
                }
            });
        }
        function toogleCNQuanLy() {
            if ($("#chkTrungTamHT").attr("checked")) {
                $("#ctl00_FormContent_cboBranchCodes").attr("selectedIndex", 0);
                $("#ctl00_FormContent_cboBranchCodes").attr("disabled", true);
            }
            else {
                $("#ctl00_FormContent_cboBranchCodes").attr("disabled", false);
            }

        }
    </script>
</asp:Content>
