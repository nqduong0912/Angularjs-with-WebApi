using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using CORE.CoreObjectContext;
using EnterpriseDT.Net;
using ldapif;
using vpb.app.business.ktnb.Definition.OPERATORS;
using VPB_KTNB.Helpers;

namespace VPB_PROMOTION.BackEnd
{
    public partial class Modules_BackEnd_BackEndProcess : PageBase
    {
        #region initiation page variables

        protected const byte SEARCH_MODE_FULL = 0;
        protected const byte SEARCH_MODE_ABS = 1;
        protected const byte SEARCH_MODE_RIGHT = 2;
        protected const byte SEARCH_MODE_LEFT = 3;
        protected const byte SEARCH_MODE_MID = 4;

        protected string _action;
        protected int _formaction = 0;
        protected string _documenttypeid = string.Empty;
        protected string _documentid = string.Empty;
        protected string _documentlinkid = string.Empty;

        protected string _app;
        protected string _appdownloadfolder = string.Empty;
        protected string _pattern = string.Empty;
        protected string _company;
        protected string _valuedate;
        protected string _searchword;
        protected byte _searchmode;

        protected string _ftproot = string.Empty;
        protected string _prefix = string.Empty;
        protected string _datefolder = string.Empty;
        protected string _companyfolder = string.Empty;
        protected string _searchpath = string.Empty;
        protected string _componentname = string.Empty;

        protected EnterpriseDT.Net.Ftp.FTPConnection _objFTP = new EnterpriseDT.Net.Ftp.FTPConnection();

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

            #region get data submitted
            _action = Request["action"];
            _documenttypeid = Request["documenttypeid"];
            _documentid = Request["documentid"];
            _documentlinkid = Request["doclink"];

            if (!string.IsNullOrEmpty(_action))
            {
                _action = _action.ToUpper();
                try
                {
                    _formaction = int.Parse(_action);
                }
                catch (Exception ex)
                {
                    _formaction = 0;
                }
            }
            #endregion

            #region action handler
            if (_formaction == BACKEND_ACTION.CREATE_DOCUMENT) 
            {
                base.FeedBackClient(CreateNewDocument().ToString());
            }
            else if (_formaction == BACKEND_ACTION.UPDATE_DOCUMENT) 
            {
                base.FeedBackClient(UpdateDocument(_documentid).ToString());
            }
            else if (_formaction == BACKEND_ACTION.DELETE_DOCUMENT)
            {
                base.FeedBackClient(DeleteDocument(_documentid).ToString());
            }
            else if (_formaction == BACKEND_ACTION.CREATE_DOCUMENT_WITH_DOCLINK)
            {
                if (!string.IsNullOrEmpty(_documentlinkid))
                {
                    base.FeedBackClient(CreateDocumentWithDocLink(_documentlinkid).ToString());
                }

            }
            #endregion
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
        #endregion

        #region page helper processing (document)
        /// <summary>
        /// CreateNewDocument
        /// </summary>
        /// <returns></returns>
        private int CreateNewDocument()
        {
            int err_sys = FormHelper.SaveForm(_objUserContext, Request.Form, _documenttypeid);
            return err_sys;
        }
        /// <summary>
        /// UpdateDocument
        /// </summary>
        /// <param name="documentid"></param>
        /// <returns></returns>
        private int UpdateDocument(string documentid)
        {
            return FormHelper.EditForm(_objUserContext, documentid, _documenttypeid, Request.Form);
        }
        /// <summary>
        /// DeleteDocument
        /// </summary>
        /// <param name="documentid"></param>
        /// <returns></returns>
        private int DeleteDocument(string documentid)
        {
            return FormHelper.DeleteForm(_objUserContext, documentid);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fk_doclinkid"></param>
        /// <returns></returns>
        protected int CreateDocumentWithDocLink(string fk_doclinkid)
        {
            // in this case, create document [thong tin trao doi] and fk_doclinkid was an issue
            if (CreateNewDocument() == 0)
            {
                string fk_documentid = Session["DocumentID"].ToString();
                if (CommonFunc.AddDocLink(fk_documentid, fk_doclinkid, 0, _objUserContext) == 0)
                    return 0;
            }
            return -1;
        }
        #endregion

        #region page button processing

        #endregion
    }
}