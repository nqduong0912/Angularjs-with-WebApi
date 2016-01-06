<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="MailServer.aspx.cs" Inherits="VPB_KTNB.Modules.Lookup.QuanTri.MailServer" %>
<%@ Register Assembly="C1.Web.C1Input.2" Namespace="C1.Web.C1Input" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
    <div class="form-horizontal">
        <div class="form-group">
            <label class="col-sm-3 col-sm-offset-1 control-label">Username<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="tbUserName" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-3 col-sm-offset-1 control-label">Password<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="tbPassword" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-3 col-sm-offset-1 control-label">Server<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="tbServerAdd" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
            </div>
        </div>
         <div class="form-group">
            <label class="col-sm-3 col-sm-offset-1 control-label">Port<span class="star-red">*</span></label>
            <div class="col-sm-6">
                <asp:TextBox ID="tbPort" runat="server" CssClass="form-control" Width="100px"></asp:TextBox>
            </div>
        </div>
    </div>
    
    <script type="text/javascript">
        var _documentid;
        /*********************************************************/
        $(document).ready(function () {
            //do smt here
        });
        /*********************************************************/        

        function prepareupdatedoc(documentID) {
            var username = GetSvrCtlValue("tbUserName");
            var password = GetSvrCtlValue("tbPassword");
            var serveraddress = GetSvrCtlValue("tbServerAdd");
            var port = GetSvrCtlValue("tbPort");
            if (username.length == 0) {
                alert("Nhập Username");
                return false;
            }
            if (password.length == 0) {
                alert("Nhập Password.");
                return false;
            }
            if (serveraddress.length == 0) {
                alert("Nhập Server address.");
                return false;
            }
            if (port.length == 0) {
                alert("Nhập port.");
                return false;
            }
            var r = confirm(MSG_EIDT_QUESTION);
            if (!r) return false;
            var url = "MailServer.aspx";
            var query = "act=checkvalueupdate";
            query += "&username=" + username;
            query += "&password=" + password;
            query += "&serveraddress=" + serveraddress;
            query += "&port=" + port;
            StartProcessingForm("");

            $.ajax({
                type: "POST",
                url: url,
                data: query,
                success: function (data) {
                    FinishProcessingForm();
                    if (data == "0")
                    {
                        alert(MSG_EDIT_OK);
                    }
                    else
                        alert("Dữ liệu nhập bị trùng lặp. Kiểm tra lại.");
                }
            });
        }

        function savedocument(document, doctype, callbackfunction_onsuccess, callbackfunction_onerror, confirmsg) {
            if (!FormValidated())
                return false;
            if ((confirmsg == '') || (confirmsg == null) || (confirmsg == 'undefined'))
                confirmsg = MSG_ADD_QUESTION;
            post_url = "savedoc.do";
            post_data = ParsingForm();

            if (!window.confirm(confirmsg))
                return false;

            post_data += "&act=" + CREATE_DOCUMENT;
            post_data += "&doctype=" + doctype;
            post_data += "&doc=" + document;

            post_data = post_data.replace("undefined", "");
            while (post_data.lastIndexOf(pre_id_formcontrol) != -1)
                post_data = post_data.replace(pre_id_formcontrol, "");

            /*prepare file attachment for uploading*/
            var fileattach = "";
            if (post_data.lastIndexOf("FileAttachment") != -1)
                fileattach = getfileattacht(post_data);

            StartProcessingForm("");
            $.ajax({
                type: "POST",
                url: post_url,
                data: post_data,
                success: function (msg) {

                    FinishProcessingForm();
                    var ErrorMessage = new String(msg);
                    if (ErrorMessage == "") {
                        if (callbackfunction_onsuccess != "") {

                            var f = callbackfunction_onsuccess;
                            f();
                        }
                    }
                    else {
                        alert(ErrorMessage);
                        if (callbackfunction_onerror != "") {
                            var f = callbackfunction_onerror;
                            f();
                        }
                        else {
                            alert(ErrorMessage);
                        }
                    }
                }
            });
        }

    </script>
</asp:Content>