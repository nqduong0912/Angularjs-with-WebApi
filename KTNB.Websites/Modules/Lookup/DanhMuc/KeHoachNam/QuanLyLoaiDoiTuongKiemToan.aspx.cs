using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.Definition.DMS;
using C1.Web.C1WebGrid;
using System.Text;
using KTNB.Extended.Dal;
using KTNB.Extended.Biz;
using KTNB.Extended.Core;
using System.Web.Services;
using vpb.app.business.ktnb.Definition.UMS;
using CORE.CoreObjectContext;
using CORE.UMS.CoreBusiness;
using System.Data;
using KTNB.Extended.Commons;
using VPB_KTNB.Helpers;

namespace VPB_KTNB.Modules.Lookup.DanhMuc.KeHoachNam
{
    public partial class QuanLyLoaiDoiTuongKiemToan : PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;
        protected string _year = string.Empty;
        protected string _donvi = string.Empty;
        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;
        protected string _docspaceid = string.Empty;
        protected int countActive = 0;
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
            if (!string.IsNullOrEmpty(Request["doc"]))
                _documentid = Request["doc"];
            if (!string.IsNullOrEmpty(Request["y"]))
                _year = Request["y"];
            if (!string.IsNullOrEmpty(Request["dv"]))
                _donvi = Request["dv"];
            if (string.IsNullOrEmpty(_documentid))
                _viewtype = VIEWTYPE.ADDNEW;
            else
                _viewtype = VIEWTYPE.EDIT;

            #region get data submit
            #endregion

            #region action handler

            #endregion

            #region init form
            base.InitForm("Quản lý Loại đối tượng kiểm toán", string.Empty, string.Empty, 0);
            _btnAddNew.Visible = true;
            _btnAddNew.Attributes.Add("onclick", "{newform(); return false;}");
            loadYears();
            loadBranches();
            
            #endregion

            #region client control event handler

            #endregion
        }
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(_year) && !String.IsNullOrEmpty(_donvi))
                {
                    GetList(_year, _donvi);
                    drpYears.SelectedValue = _year;
                    drpDonVi.SelectedValue = _donvi;
                }
                else
                    GetList(drpYears.SelectedValue, drpDonVi.SelectedValue);
            }
            else
                GetList(drpYears.SelectedValue, drpDonVi.SelectedValue);
        }
        #endregion

        #region page helper processing
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DocumentTypeID"></param>
        private void GetList(string year, string donvi)
        {
            List<dm_loaidoituongkiemtoan> lst = ManagerFactory.loaidoituongkiemtoan_manager.GetList(x => x.Nam == int.Parse(year) && x.Phongban == donvi);

            dataCtrl.DataSource = lst;
            dataCtrl.DataBind();
        }
        #endregion

        #region page button processing
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void dataCtrl_OnItemDataBound(object sender, C1ItemEventArgs e)
        {
            C1ListItemType elemType = e.Item.ItemType;
            if ((elemType == C1ListItemType.AlternatingItem) || (elemType == C1ListItemType.Item))
            {
                Label Id = (Label)e.Item.FindControl("Id") as Label;
                Image imgDelete = (Image)e.Item.FindControl("imgDelete") as Image;

                if (Id != null)
                    imgDelete.Attributes.Add("onclick", "{DeleteDocument('" + Id.Text + "')}");

                
            }
        }
        [WebMethod]
        public static void DeleteEntity(int dtktId)
        {
            ManagerFactory.loaidoituongkiemtoan_manager.Delete(dtktId);
        }
        protected void drpYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            _year = drpYears.SelectedValue;
            _donvi = drpDonVi.SelectedValue;
            GetList(_year, _donvi);
        }
        #endregion

        protected void loadBranches()
        {
            string condition = " AND Type IN (" + GROUPTYPE.PHONG_GD + ")";
            condition += " AND FK_PARENTGROUPID='" + vpb.app.business.ktnb.Definition.UMS.GROUPS.GROUP_OF_KTNB + "'";
            condition += " ORDER BY Description";
            string query = "Name,Name+'.....'+Description As Description";

            bus_Group objGroup = new bus_Group(_objUserContext, _dbName);
            DataSet ds = objGroup.getList(condition, query);
            this.drpDonVi.DataSource = ds;
            this.drpDonVi.DataValueField = "Name";
            this.drpDonVi.DataTextField = "Description";
            this.drpDonVi.DataBind();
            drpDonVi.Items.Insert(0, new ListItem("", ""));
            drpDonVi.Items.FindByValue("").Attributes.Add("Disabled", "Disabled");
            drpDonVi.SelectedIndex = 1;
        }
        private void loadYears()
        {
            List<String> listYears = MiscUtils.GetAllYear();
            drpYears.DataSource = listYears;
            drpYears.DataBind();
            drpYears.Items.Insert(0, new ListItem("", ""));
            drpYears.Items.FindByValue("").Attributes.Add("Disabled", "Disabled");
            drpYears.SelectedIndex = 1;
        }

    }
}
