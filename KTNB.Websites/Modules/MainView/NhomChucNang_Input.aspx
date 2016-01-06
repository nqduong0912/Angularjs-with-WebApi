<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="NhomChucNang_Input.aspx.cs" Inherits="VPB_KTNB.Modules.MainView.NhomChucNang_Input" %>
<%@ Register Assembly="C1.Web.C1Input.2" Namespace="C1.Web.C1Input" TagPrefix="cc1" %>
<%@ Register TagPrefix="C1WebGrid" Namespace="C1.Web.C1WebGrid" Assembly="C1.Web.C1WebGrid.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <div class="form-horizontal">
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Tên nhóm<span class="star-red">(*)</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="txtTenNhom" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>
       
        <div class="form-group">
            <label for="inputName" class="col-sm-3 col-sm-offset-1 control-label">Diễn giải</label>
            <div class="col-sm-6">
                <asp:TextBox ID="txtDienGiai" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-3 col-sm-offset-1 control-label">Chọn chức năng</label>
            <div class="col-sm-6">
                <C1WebGrid:C1WebGrid runat="server" ID="dataCtrl" Width="100%" 
                        OnItemCreated="dataCtrl_OnItemCreated" OnItemDataBound="dataCtrl_OnItemDataBound"
                        GroupIndent="20" ItemStyle-Height="30px">
                        <Columns>
                            <C1WebGrid:C1TemplateColumn>
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkItem"></asp:CheckBox>
                                </ItemTemplate>
                            </C1WebGrid:C1TemplateColumn>
                            <C1WebGrid:C1TemplateColumn Visible="false">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="PK_COMPONENTID" Text='<%# DataBinder.Eval(Container.DataItem,"PK_COMPONENTID")%>'></asp:Label>
                                </ItemTemplate>
                            </C1WebGrid:C1TemplateColumn>
                            <C1WebGrid:C1BoundColumn HeaderText="Tên chức năng" DataField="NAME">
                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            </C1WebGrid:C1BoundColumn>
                        </Columns>
                </C1WebGrid:C1WebGrid>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        var _documentid;
        var oldList = [];
        var listComponent = [];
        /*********************************************************/
        $(document).ready(function () {
            $('[id*="chkItem"]').each(function () {
                if (this.checked) {
                    oldList.push($(this).parent().attr('componentID'));
                }
            })
        });
        /*********************************************************/
        function AddNewRole(ten, diengiai) {
            PageMethods.createRole(ten, diengiai, Success, Failure);
        }
        function Success(result) {
        }
        function Failure(error) {
        }
        function preparesavedoc(documentID, doctypeID) {
            var ten = GetSvrCtlValue("txtTenNhom");
            var diengiai = GetSvrCtlValue("txtDienGiai");
            if (ten.length == 0) {
                alert("Nhập tên nhóm.");
                return false;
            }
            var r = confirm(MSG_ADD_QUESTION);
            if (!r) return false;
            $('[id*="chkItem"]').each(function () {
                if (this.checked) {
                    listComponent.push($(this).parent().attr('componentID'));
                }
            })
            var url = "NhomChucNang_Input.aspx";
            var query = "act=checkvalue";
            query += "&name="+ ten;
            query += "&des=" + diengiai;

            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (data) {
                    FinishProcessingForm();
                    if (data == "0") {
                        
                        $.ajax({
                            type: "POST",
                            url: "NhomChucNang_Input.aspx/SetRoleForComponent",
                            data: JSON.stringify({ componentIDs: listComponent }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function () {
                                savedoc_success();
                            }
                        });
                    }
                    else
                        alert("Dữ liệu nhập bị trùng lặp. Kiểm tra lại.");
                }
            });
        }

        function prepareupdatedoc(documentID) {
            var ten = GetSvrCtlValue("txtTenNhom");
            var diengiai = GetSvrCtlValue("txtDienGiai");
            if (ten.length == 0) {
                alert("Nhập tên nhóm.");
                return false;
            }
            var r = confirm(MSG_EIDT_QUESTION);
            if (!r) return false;
            $('[id*="chkItem"]').each(function () {
                if (this.checked) {
                    listComponent.push($(this).parent().attr('componentID'));
                }
            })
            var url = "NhomChucNang_Input.aspx";
            var query = "act=checkvalueupdate";
            query += "&doc=" + documentID;
            query += "&name=" + ten;
            query += "&des=" + diengiai;
            StartProcessingForm("");

            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (data) {
                    FinishProcessingForm();
                    if (data == "0") {
                        $.ajax({
                            type: "POST",
                            url: "NhomChucNang_Input.aspx/ClearRoleForComponent",
                            data: JSON.stringify({ componentIDs: oldList }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function () {
                                $.ajax({
                                    type: "POST",
                                    url: "NhomChucNang_Input.aspx/SetRoleForComponent",
                                    data: JSON.stringify({ componentIDs: listComponent }),
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function () {
                                        update_success();
                                    }
                                });
                            }
                        });
                    }
                    else
                        alert("Dữ liệu nhập bị trùng lặp. Kiểm tra lại.");
                }
            });
        }

        function preparedeletedoc(documentID)
        {
            var r = confirm(MSG_DEL_QUESTION);
            if (!r) return false;
            var url = "NhomChucNang_Input.aspx";
            var query = "act=deleteRole";
            query += "&doc=" + documentID;
            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (data) {
                    FinishProcessingForm();
                    if (data == "0") {
                        $.ajax({
                            type: "POST",
                            url: "NhomChucNang_Input.aspx/ClearRoleForComponent",
                            data: JSON.stringify({ componentIDs: oldList }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function () {
                                delete_success();
                            }
                        });
                    }
                }
            });
        }
        function savedoc_success() {
            alert(MSG_ADD_OK);
            window.location.href = 'NhomChucNang.aspx';
        }
        function savedoc_error() {
            alert(MSG_ADD_ER);
        }
        function update_success() {
            alert(MSG_EDIT_OK);
            window.location.href = 'NhomChucNang.aspx';
        }
        function update_error() {
            alert(MSG_EDIT_ER);
        }
        function delete_success() {
            alert(MSG_DEL_OK);
            window.location.href = 'NhomChucNang.aspx';
        }
        function delete_error() {
            alert(MSG_DEL_ER);
        }
    </script>
</asp:Content>
