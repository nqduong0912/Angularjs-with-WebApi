using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using VPB_KTNB.Helpers;

namespace VPB_PROMOTION.Report.Viewer
{
    public partial class Viewer : PageBase
    {
        #region initiation page variables

        protected string _company = string.Empty;
        protected string _department = string.Empty;
        protected string _title = string.Empty;
        protected string _subtitle = string.Empty;
        protected string _subtitle2 = string.Empty;
        protected string _footer = string.Empty;

        protected string _rpt = string.Empty;
        protected string _rpt_path = string.Empty;
        protected string _rpt_export_type = string.Empty;

        protected ReportHelper _rptHelper;
        protected ReportDocument _rptDoc;

        protected DataTable _datasource = null;

        protected string _viewid = string.Empty;
        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;

        #endregion

        #region page init & load
        /// <summary>
        /// OnInit
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            base.AuthorizeUserCtx();

            #region get data submit
            _rpt = Request["rpt"];
            _rpt_path = Request["rptpath"];
            _rpt_export_type = Request["rptexport"]; if (!string.IsNullOrEmpty(_rpt_export_type)) _rpt_export_type = _rpt_export_type.ToUpper();
            #endregion

            #region set datasource by report
            //if (_rpt == "ThongKe_TinhTrang_SDT.rpt")
            //    _datasource = ThongKe_TinhTrang_SDT();
            #endregion

            #region init form

            #endregion

            #region client control event handler

            #endregion

            DoReport(_rpt_export_type);
        }
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        #endregion

        #region page helper processing
        /// <summary>
        /// 
        /// </summary>
        private void DoReport(string ExportFileFormat)
        {
            string rptfile = _rpt_path + _rpt;

            _rptDoc = new ReportDocument();
            _rptDoc.Load(Server.MapPath(rptfile));

            if (_datasource!=null)
                _rptDoc.SetDataSource(_datasource);

            _rptHelper = new ReportHelper(_rptDoc, rptMainViewer);

            _rptHelper.SetParameter("Company", _company);
            _rptHelper.SetParameter("Department", _department);
            _rptHelper.SetParameter("Title", _title);
            _rptHelper.SetParameter("Subtitle", _subtitle);
            _rptHelper.SetParameter("Footer", _footer);

            _rptHelper.Open();
            if (ExportFileFormat=="RPT")
                _rptHelper.Show();
            else 
            {
                string client_path = "~/Modules/KhuyenMai/reports/client/";
                string url = "";
                
                string filename = "Demo" + "." + ExportFileFormat;
                string export_filepath = Server.MapPath(client_path + filename);

                if (ExportFileFormat == "PDF")
                    SaveRpt(export_filepath, ExportFormatType.PortableDocFormat);
                else if (ExportFileFormat == "XLS")
                    SaveRpt(export_filepath, ExportFormatType.Excel);
                else if (ExportFileFormat == "HTML")
                    SaveRpt(export_filepath, ExportFormatType.HTML40);
                else if (ExportFileFormat == "DOC")
                    SaveRpt(export_filepath, ExportFormatType.WordForWindows);

                client_path = client_path.Replace("~", "../..");
                url = client_path + filename;

                base.FeedBackClient(url);
                //ViewRpt(url);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="format"></param>
        private void SaveRpt(string file, ExportFormatType format)
        {
            ExportOptions exportOpts = new ExportOptions();
            DiskFileDestinationOptions diskOpts = ExportOptions.CreateDiskFileDestinationOptions();

            exportOpts.ExportFormatType = format;
            exportOpts.ExportDestinationType = ExportDestinationType.DiskFile;

            diskOpts.DiskFileName = file;
            exportOpts.ExportDestinationOptions = diskOpts;

            _rptDoc.Export(exportOpts);
        }
        /// <summary>
        /// 
        /// </summary>
        private void ViewRpt(string url)
        {
            //string url = _rptexportpath + _rptfilename;
            Response.Redirect(url);
        }

        #endregion

        #region page button processing

        #endregion
    }
}
