using System;
using vpb.app.business.ktnb.Definition.UMS;
using vpb.app.business.ktnb.Definition.DMS;
using VPB_KTNB.Helpers;

namespace VPB_PROMOTION.MainviewerDVTC
{
    public partial class MainviewerDVTC_Mainviewer : PageBase
    {
        #region initiation page variables
        string _op = string.Empty;
        string _docspace = string.Empty;
        string _toc = string.Empty;
        string _applicationid = string.Empty;
        string _applicationname = string.Empty;
        string _componentid = string.Empty;
        string _componentname = string.Empty;
        string _finisheddate = string.Empty;
        #endregion

        #region page init & load
        /// <summary>
        /// OnInit
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.AuthorizeUserCtx();
            base.OnInit(e);

            #region get data submit
            _op = Request.QueryString["op"];
            _docspace = Request.QueryString["docspace"];
            _toc = Request.QueryString["toc"];
            _applicationid = Request["a"];
            _applicationname = Request["an"];
            _componentid = Request["c"];
            _componentname = Request["cn"];
            #endregion
        }
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["boxuyquyen"] = null;
            if (!string.IsNullOrEmpty(Request["uyquyen"]))
            {
                Session["boxuyquyen"] = Request["uyquyen"];
                _finisheddate = CommonFunc.GetFinishedDateOnObject(_objUserContext.UserID, TYPE_OF_APPLICANT.USER, _componentid, TYPE_OF_OBJECT.COMPONENT, _objUserContext, _dbName);
                

                CommonFunc.DelMessNotify(_objUserContext.UserID, _componentid, _objUserContext, _dbName);

                if (!string.IsNullOrEmpty(_finisheddate))
                {
                    string today = String.Format("{0:yyyyMMdd}", DateTime.Now);
                    if (string.Compare(today, _finisheddate) == 1)
                        FormHelper.FormWarning("Công việc quá hạn", "Công việc này của bạn đã quá hạn thực hiện.", "black");
                }
            }
            if (!string.IsNullOrEmpty(_applicationid)) Session["application"] = _applicationid;
            if (!string.IsNullOrEmpty(_componentid)) Session["component"] = _componentid;

            string url;
            this.fraToc.Attributes["src"] = "DonViToChuc.aspx?op=" + _op + "&docspace=" + _docspace + "&a=" + _applicationid + "&c=" + _componentid + "&an=" + _applicationname + "&cn=" + _componentname + "&u=" + _objUserContext.UserName;

            if (!string.IsNullOrEmpty(_docspace))
            {
                if ((_docspace.ToLower() == "appuser") || (_docspace.ToLower() == "sysuser"))
                    if (_toc == "new")
                    {
                        url = UrlHelper.GetSiteUrl() + "/Modules/UMS/NewUser.aspx";
                        this.fraTopic.Attributes["src"] = url;
                    }
                    else
                        this.fraTopic.Attributes["src"] = "Toolbar.aspx?docspace=" + _docspace + "&a=" + _applicationid + "&c=" + _componentid + "&an=" + _applicationname + "&cn=" + _componentname;
                else if ((_docspace.ToLower() == "donvi") || (_docspace.ToLower() == "supportgroup"))
                    if (_toc == "new")
                    {
                        url = "../../Modules/UMS/DonVi_Input.aspx";
                        this.fraTopic.Attributes["src"] = url;
                    }
                    else
                        this.fraTopic.Attributes["src"] = "Toolbar.aspx?docspace=" + _docspace + "&a=" + _applicationid + "&c=" + _componentid + "&an=" + _applicationname + "&cn=" + _componentname;
                else if ((_docspace.ToLower() == "appcfg") || (_docspace.ToLower() == "syscfg"))
                    this.fraTopic.Attributes["src"] = "Toolbar.aspx?docspace=" + _docspace + "&a=" + _applicationid + "&c=" + _componentid + "&an=" + _applicationname + "&cn=" + _componentname;
            }
        }
        #endregion
    }
}