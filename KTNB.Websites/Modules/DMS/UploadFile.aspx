<%@ Page Title="" Language="C#" MasterPageFile="~/Share/Wsp.master" AutoEventWireup="true" CodeBehind="UploadFile.aspx.cs" Inherits="VPB_TDHS.Modules.DMS.UploadFile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContent" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server" />
    <table>
        <tr>
            <td style="width: 79px">
                File
            </td>
            <td style="width: 350px">
                <asp:FileUpload ID="FileUploadDoc" runat="server"  />
                
            </td>
          
        </tr>
         
          <tr>
            <td style="width: 79px">
                Diễn giải
            </td>
            <td style="width: 350px">
                  <asp:TextBox ID="txtDescription" runat="server" 
                SkinID="" Rows="2" TextMode="MultiLine" Width="350px" ></asp:TextBox>
            </td>
          
        </tr>
         <tr>
           <td style="width: 79px">
             
            </td>
          <td >
               <asp:Button ID="btnUpload" runat="server" Text="Tải lên" OnClick="btnUpload_Click" SkinID="AttachFileButton" /><br />
          </td>
        </tr>
        <tr>
          <td colspan="2">
                <asp:Label ID="lblError" runat="server" />
          </td>
        </tr>
    </table>
        <script type="text/javascript">
            /*********************************************************/
            $(document).ready(function () {
                //do smt here
                $("#ctl00_FormContent_btnUpload").val("Tải lên");
            });
            /*********************************************************/
            function UpLoadFileDoc() {
                var doc = Qry["doc"]; //getParameterByName("doankt");
                if (doc == null || doc == '' || doc == 'undefined') {
                    alert("Chưa có DocumentID");
                    return false;
                }
                var url = "UploadFile.aspx";
                var query = "act=uploadfiledoc&doc=" + doc;
                $.ajax({
                    type: "POST",
                    url: url,
                    data: query,
                    success: function (fullname) {
                        if (fullname == "2") {
                            alert("sucess");
                        }
                        else {
                            alert("error");
                        }
                    }
                });

            }
            function LoadDocument(DocumentID) {
                var doankt = Qry["doankt"]; //getParameterByName("doankt");
                var dotkt = Qry["dotkt"];
                url = "CongViecDuocPhuTrach_Load.aspx?act=loaddoc&doc=" + DocumentID + "&dotkt=" + dotkt + "&doankt=" + doankt;
                window.location.href = url;
            }
            function LoadPageDanhSachPhatHien(DocumentID) {
                //            var doankt = Qry["doankt"]; //getParameterByName("doankt");
                //            var dotkt = Qry["dotkt"];
                url = "DanhSachPhatHien.aspx?act=loaddoc&doc=" + DocumentID;
                window.location.href = url;
            }
            function LapChuongTrinhKiemToan(DocumentID) {
                var url = "../../Controls/Tab/ThanhLapDoanKT_Load.aspx?doc=" + DocumentID + "&act=dotkt";
                window.location.href = url;
            }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ButtonExtendBefore" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ButtonExtend" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FormFooter" runat="server">
</asp:Content>
