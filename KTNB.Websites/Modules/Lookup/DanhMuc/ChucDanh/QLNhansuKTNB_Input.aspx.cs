using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.Definition.DMS;
using CORE.UMS.CoreBusiness;
using System.Data;
using vpb.app.business.ktnb.Definition.UMS;
using VPB_KTNB.Helpers;

namespace VPB_KTNB.Modules.Lookup.DanhMuc.ChucDanh
{
    public partial class QLNhansuKTNB_Input : PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;
        protected string _doctypeid = DOCTYPE.QLNHANSU_KTNB;
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
            string caption = "Thêm mới Nhân viên";
            if (!string.IsNullOrEmpty(_documentid))
                caption = "Thông tin nhân viên";
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
                CommonFunc.LoadStatus(this.DOCSTATUS);
                loadBranches();
                CommonFunc.GetLookUpValue("936D771B-D880-4A1C-8D6E-42C571C2853C", this.ID8_936D771B_D880_4A1C_8D6E_42C571C2853C, 4);
                if (!string.IsNullOrEmpty(_action))
                    if (_action == "loaddoc")
                    {
                        CommonFunc.LoadDocInfo(_documentid, Page.Master);
                        _btnDelete.Visible = true;
                    }
            }
        }

        /// <summary>
        /// LoadGroup, load tat ca phong ban
        /// </summary>
        /// <param name="PropertyID"></param>
        /// /// <param name="combo"></param>
        //void LoadGroup(string PropertyID, DropDownList combo)
        //{
        //    string rootText = "Các đơn vị kiểm toán";
        //    string condition = "";
        //    if (_m_roleID == vpb.app.business.ktnb.Definition.UMS.ROLES.TRUONGPHONG_CSCC)
        //        condition += " AND FK_PARENTGROUPID='" + vpb.app.business.ktnb.Definition.UMS.GROUPS.GROUP_OF_KTNB + "'";
        //    condition += "Order By Name";
        //    VPB_CRM.Helper.AppCached.group_list = "ds_allgroup";

        //    string query = "PK_GroupID,Name,Mnemonic,Description,FK_ParentGroupID,Type";

        //    bus_Group objgrp = bus_Group.Instance(_objUserContext);
        //    DataSet ds_allgroup;
        //    ds_allgroup = objgrp.getList(condition, query);
        //    base.BindData2Combo(combo, ds_allgroup, "Description", "PK_GroupID", string.Empty);
        //    objgrp = null;
        //    combo.Items.Insert(0, new ListItem(""));
        //}
        protected void loadBranches()
        {
            string condition = "";
            if (_m_roleID == vpb.app.business.ktnb.Definition.UMS.ROLES.TRUONGPHONG_CSCC)
                condition += " AND FK_PARENTGROUPID='" + vpb.app.business.ktnb.Definition.UMS.GROUPS.GROUP_OF_KTNB + "'";
            condition += " ORDER BY Description";
            string query = "Description";
            bus_Group objGroup = new bus_Group(_objUserContext, _dbName);
            DataSet ds = objGroup.getList(condition, query);
            this.ID8_558DD2DC_841B_4368_A426_B84F01079086.DataSource = ds.Tables[0];
            this.ID8_558DD2DC_841B_4368_A426_B84F01079086.DataTextField = "Description";
            this.ID8_558DD2DC_841B_4368_A426_B84F01079086.DataValueField = "Description";
            this.ID8_558DD2DC_841B_4368_A426_B84F01079086.DataBind();
        }
        #region page helper processing

        #endregion

        #region page button processing

        #endregion
    }
}