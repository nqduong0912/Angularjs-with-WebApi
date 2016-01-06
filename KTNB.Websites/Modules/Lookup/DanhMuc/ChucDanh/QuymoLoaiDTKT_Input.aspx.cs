using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.Definition.DMS;
using VPB_CRM.Helper;
using CORE.UMS.CoreBusiness;
using System.Data;
using vpb.app.business.ktnb.Definition.UMS;

namespace VPB_KTNB.Modules.Lookup.DanhMuc.ChucDanh
{
    public partial class QuymoLoaiDTKT_Input : PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;
        protected string _doctypeid = DOCTYPE.QuymoLoaiDTKT;
        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;
        protected string _docspaceid = string.Empty;
        protected string _property = string.Empty;
        protected string _propertyvalue = string.Empty;
        //protected string _group = DOCTYPE.GROUP_OF_KTNB;
        #endregion
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            base.AuthorizeUserCtx();

            #region get data submit
            _documentid = Request["doc"];
            _action = Request["act"];
            _property = Request["p"];
            _propertyvalue = Request["v"];
            #endregion

            if (string.IsNullOrEmpty(_documentid))
                _viewtype = VIEWTYPE.ADDNEW;
            else
                _viewtype = VIEWTYPE.EDIT;

            #region action handler
            if (!string.IsNullOrEmpty(_action))
            {
                if (_action == "checkvalue")
                    FeedBackClient(CommonFunc.CountPropertyValue(_property, _propertyvalue).ToString());
                if (_action == "checkvalueupdate")
                    FeedBackClient(CommonFunc.CountPropertyValue(_property, _propertyvalue, _documentid).ToString());
            }
            #endregion

            #region init form
            string caption = "Thêm mới Quy mô Loại đối tượng kiểm toán";
            if (!string.IsNullOrEmpty(_documentid))
                caption = "Thông tin Quy mô loại đối tượng kiểm toán";
            base.InitForm(caption, string.Empty, _doctypeid, _viewtype);
            #endregion

            #region client control event handler
            _btnSave.Attributes.Add("onclick", "{preparesavedoc('" + System.Guid.NewGuid().ToString() + "','" + _doctypeid + "'); return false;}");
            //_btnEdit.Attributes.Add("onclick", "{updatedocument('" + _documentid + "',update_success,update_error); return false;}");
            _btnEdit.Attributes.Add("onclick", "{prepareupdatedoc('" + _documentid + "'); return false;}");
            _btnDelete.Attributes.Add("onclick", "{deletedocument('" + _documentid + "',delete_success,delete_error); return false;}");
            #endregion
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int year = DateTime.Now.Year;
                for (int i = 0; i < 10; i++)
                {
                    int newyear = year - i;
                    this.ID8_3661C77F_A22A_440D_8D32_FE472A117505.Items.Add(new ListItem(newyear.ToString()));
                }
                CommonFunc.LoadStatus(this.DOCSTATUS);
                CommonFunc.GetLookUpValue("A3AFE5F3-3412-4418-854C-BD17EFAFD6BE", this.ID8_A3AFE5F3_3412_4418_854C_BD17EFAFD6BE, 4);
                CommonFunc.GetLookUpValue("48163DD4-9F4B-4AC4-A1B9-A25BBB1F0EA2", this.ID8_48163DD4_9F4B_4AC4_A1B9_A25BBB1F0EA2, 4);
                if (!string.IsNullOrEmpty(_action))
                {
                    if (_action == "loaddoc")
                    {
                        CommonFunc.LoadDocInfo(_documentid, Page.Master);
                        _btnDelete.Visible = false;
                    }
                }
                    
            }
        }
        //protected void loadBranches()
        //{
        //    string condition = "";
        //    if (_m_roleID == vpb.app.business.ktnb.Definition.UMS.ROLES.TRUONGPHONG_CSCC)
        //        condition += " AND FK_PARENTGROUPID='" + vpb.app.business.ktnb.Definition.UMS.GROUPS.GROUP_OF_KTNB + "'";
        //    condition += " ORDER BY Description";
        //    string query = "Description";
        //    bus_Group objGroup = new bus_Group(_objUserContext, _dbName);
        //    DataSet ds = objGroup.getList(condition, query);
        //    int count = ds.Tables[0].Rows.Count;
        //    this.ID8_558DD2DC_841B_4368_A426_B84F01079086.DataSource = ds.Tables[0];
        //    this.ID8_558DD2DC_841B_4368_A426_B84F01079086.DataTextField = "Description";
        //    this.ID8_558DD2DC_841B_4368_A426_B84F01079086.DataValueField = "Description";
        //    this.ID8_558DD2DC_841B_4368_A426_B84F01079086.DataBind();
        //}
    }
}