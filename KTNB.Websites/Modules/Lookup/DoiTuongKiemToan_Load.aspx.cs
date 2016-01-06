using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.Definition.DMS;
using vpb.app.business.ktnb.Definition.UMS;
using vpb.app.business.ktnb.CoreBusiness;
using System.Data;
using CORE.CoreObjectContext;
using VPB_KTNB.Helpers;

namespace VPB_TDHS.Modules.Lookup
{
    public partial class DoiTuongKiemToan_Load : PageBase
    {
        protected string _action = string.Empty;
        protected string _doctypeid = DOCTYPE.DOITUONG_KT;
        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;
        protected string _docspaceid = string.Empty;
        protected string _property = string.Empty;
        protected string _propertyvalue = string.Empty;

        /// <summary>
        /// OnInit
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            base.AuthorizeUserCtx();

            _documentid = Request["doc"];
            _action = Request["act"];
            _property = Request["p"];
            _propertyvalue = Request["v"];

            if (string.IsNullOrEmpty(_documentid))
                _viewtype = VIEWTYPE.ADDNEW;
            else
                _viewtype = VIEWTYPE.EDIT;

            if (!string.IsNullOrEmpty(_action))
            {
                if (_action == "checkvalue")
                    FeedBackClient(CommonFunc.CountPropertyValue(_property, _propertyvalue).ToString());
                if (_action == "checkvalueupdate")
                    FeedBackClient(CommonFunc.CountPropertyValue(_property, _propertyvalue, _documentid).ToString());
            }

            string caption = "Thêm mới đối tượng kiểm toán";
            if (!string.IsNullOrEmpty(_documentid))
                caption = "Thông tin đối tượng kiểm toán";
            base.InitForm(caption, string.Empty, _doctypeid, _viewtype);

            _btnSave.Attributes.Add("onclick", "{preparesavedoc('" + System.Guid.NewGuid().ToString() + "','" + _doctypeid + "'); return false;}");
            //_btnEdit.Attributes.Add("onclick", "{updatedocument('" + _documentid + "',update_success,update_error); return false;}");
            _btnEdit.Attributes.Add("onclick", "{prepareupdatedoc('" + _documentid + "'); return false;}");
            _btnDelete.Attributes.Add("onclick", "{deletedocument('" + _documentid + "',delete_success,delete_error); return false;}");
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
                CommonFunc.LoadStatus(this.DOCSTATUS);
                CommonFunc.GetLookUpValue("94F402C1-CCC3-4A93-8D9F-2D24BDB8EE2C", this.ID8_94F402C1_CCC3_4A93_8D9F_2D24BDB8EE2C, 4);
                //CommonFunc.BindUserInRoleToCombo(ID8_A4BC472E_B041_4093_8F53_D46452B1E130, ROLES.CANBO_GSTX);
                //BindDoiTuongQuanLy();
                if (!string.IsNullOrEmpty(_action))
                    if (_action == "loaddoc")
                    {
                        CommonFunc.LoadDocInfo(_documentid, Page.Master);
                        _btnDelete.Visible = false;
                        
                    }
            }
        }
    }
}