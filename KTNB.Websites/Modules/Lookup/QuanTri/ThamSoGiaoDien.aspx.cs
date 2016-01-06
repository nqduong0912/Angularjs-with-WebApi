using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.Definition.DMS;
using KTNB.Extended.Dal;
using KTNB.Extended.Biz;
using KTNB.Extended.Core;
using System.Data;
using vpb.app.business.ktnb.CoreBusiness;
using CORE.CoreObjectContext;
using VPB_KTNB.Helpers;

namespace VPB_KTNB.Modules.Lookup.QuanTri
{
    public partial class ThamSoGiaoDien : PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;
        protected string _high = string.Empty;
        protected string _medium = string.Empty;
        protected string _low = string.Empty;
        protected byte _viewtype = 0;
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
            _action = Request["act"];
            _high = Request["highColor"];
            _medium = Request["mediumColor"];
            _low = Request["lowColor"];

            #endregion

            _viewtype = VIEWTYPE.EDIT;

            #region action handler
            if (!string.IsNullOrEmpty(_action))
            {

                if (_action == "checkvalueupdate")
                    FeedBackClient(updateConfig(_high, _medium, _low));
            }
            #endregion

            #region init form
            string caption = "Thiết lập tham số giao diện";
            base.InitForm(caption, string.Empty, String.Empty, _viewtype);
            #endregion

            #region client control event handler
            //_btnEdit.Attributes.Add("onclick", "{updatedocument('" + _documentid + "',update_success,update_error); return false;}");
            _btnEdit.Attributes.Add("onclick", "{prepareupdatedoc(); return false;}");
            //_btnDelete.Attributes.Add("onclick", "{deletedocument('" + _documentid + "',delete_success,delete_error); return false;}");
            #endregion
        }
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                _btnDelete.Visible = false;
                List<qt_serverconf> lst = ManagerFactory.serverconf_manager.GetList(x => x.CType == (int)ConfigType.Color);
                for (int i = 0; i < lst.Count; i++)
                {
                    qt_serverconf child = lst[i];
                    switch (child.Ten)
                    {
                        case "HighColor":
                            this.clHigh.Text = child.Value;
                            break;
                        case "MediumColor":
                            this.clMedium.Text = child.Value;
                            break;
                        case "LowColor":
                            this.clLow.Text = child.Value;
                            break;
                    }
                }
            }
        }

        protected void updateSC(String name, String value)
        {
            List<qt_serverconf> lst = ManagerFactory.serverconf_manager.GetList(x => x.Ten == name);
            bus_Document doc = bus_Document.Instance((UserContext)HttpContext.Current.Session["objUserContext"]);
            if (lst.Count == 0)
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt.Columns.Add("PROPERTYTYPE");
                dt.Columns.Add("FK_PROPERTYID");
                dt.Columns.Add("VALUE");
                DataRow dr;
                dr = dt.NewRow();
                dr["PROPERTYTYPE"] = "6";
                dr["FK_PROPERTYID"] = "5EF3B3AA-B8AF-4699-A8CC-ACD66794BE0C";
                dr["VALUE"] = (int)ConfigType.Color;
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr["PROPERTYTYPE"] = "8";
                dr["FK_PROPERTYID"] = "F1CABABA-EF6B-4148-A423-05A3E62C44DF";
                dr["VALUE"] = name;
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr["PROPERTYTYPE"] = "8";
                dr["FK_PROPERTYID"] = "69DC1C1C-0A2E-43A2-AD3E-DB2249D2E808";
                dr["VALUE"] = value;
                dt.Rows.Add(dr);

                ds.Tables.Add(dt);
                string docid = Guid.NewGuid().ToString();
                string mess = doc.createDocument(docid, "", "", "A45510EB-C7D0-4839-B41A-AE9C9B1DBAEA", "00000000-0000-0000-0000-000000000000", "",
                _objUserContext.UserName, _m_groupname, DateTime.Now.Year, DateTime.Now.Month);
                if (string.IsNullOrEmpty(mess))
                {
                    mess = doc.createDocumentProperties(docid, ds);
                }

            }
            else
            {
                string documentID = lst[0].PK_DocumentID.ToString();
                bus_Type_Doc_Property objTDP = bus_Type_Doc_Property.Instance((UserContext)HttpContext.Current.Session["objUserContext"]);
                objTDP.updatePropertyValue(documentID, "69DC1C1C-0A2E-43A2-AD3E-DB2249D2E808", value);
            }
        }
        protected string updateConfig(String high, String medium, String low)
        {
            updateSC("HighColor", high);
            updateSC("MediumColor", medium);
            updateSC("LowColor", low);
            return "0";
        }
        #endregion
    }
}