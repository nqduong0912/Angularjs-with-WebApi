<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Viewer.aspx.cs" Inherits="VPB_PROMOTION.Report.Viewer.Viewer" %>
<%@ Register TagPrefix="cr" Namespace="CrystalDecisions.Web" Assembly="CrystalDecisions.Web, Version=12.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" %>
<!DOCTYPE html>

<html lang="vi">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
    <title>VPB_CRM ReportViewer...</title>
</head>
<body>
    <form id="form1" runat="server">
    <center>
            <table style="width:100%">
                <tr>
                    <td style="width:100%">
                        <cr:CrystalReportViewer runat="server" ID="rptMainViewer" AutoDataBind="true" Width="100%" />
                    </td>
                </tr>
            </table>
        </center>
    </form>
</body>
</html>
