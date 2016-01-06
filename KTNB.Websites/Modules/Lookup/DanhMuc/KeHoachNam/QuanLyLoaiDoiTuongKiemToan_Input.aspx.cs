using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web;
using System.Web.UI;
using System.Web.Services;
using CORE.CoreObjectContext;
using CORE.UMS.CoreBusiness;
using vpb.app.business.ktnb.CoreBusiness;
using C1.Web.C1WebGrid;
using vpb.app.business.ktnb.Definition.DMS;
using vpb.app.business.ktnb.Definition.WFS;
using vpb.app.business.ktnb.Definition.UMS;
using System.Collections.Generic;
using KTNB.Extended.Dal;
using KTNB.Extended.Biz;
using KTNB.Extended.Core;
using KTNB.Extended.Commons;
using VPB_KTNB.Helpers;

namespace VPB_KTNB.Modules.Lookup.DanhMuc.KeHoachNam
{
    public partial class QuanLyLoaiDoiTuongKiemToan_Input : PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;
        protected string _doctypeid = DOCTYPE.LOAI_DOITUONG_KT;
        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;
        protected static string _year = string.Empty;
        protected static string _donvi = string.Empty;
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
            _documentid = Request["doc"];
            loadBranches();
            loadYears();
            if (!string.IsNullOrEmpty(Request["y"]))
            {
                _year = Request["y"];
                drpYears.SelectedValue = _year;
            }
            if (!string.IsNullOrEmpty(Request["dv"]))
            {
                _donvi = Request["dv"];
                drpDonVi.SelectedValue = _donvi;
            }
            #endregion
            if (string.IsNullOrEmpty(_documentid))
                _viewtype = VIEWTYPE.ADDNEW;
            else
                _viewtype = VIEWTYPE.EDIT;

            #region action handler
            if (!string.IsNullOrEmpty(_action))
            {
                //if (_action == "checkvalue")
                //    FeedBackClient(createRole(_name, _des).ToString());
                //if (_action == "checkvalueupdate")
                //    FeedBackClient(editRole(_name, _des).ToString());
                //if (_action == "deleteRole")
                //    FeedBackClient(deleteRole(_roleID).ToString());
            }
            #endregion

            #region init form
            string caption = "Thêm mới loại đối tượng kiểm toán";
            base.InitForm(caption, string.Empty, string.Empty, _viewtype);
            #endregion

            #region client control event handler
            _btnSave.Attributes.Add("onclick", "{preparesavedoc('" + System.Guid.NewGuid().ToString() + "',''); return false;}");
            #endregion
        }
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            GetList(_doctypeid);

        }
        #endregion

        #region page helper processing
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DocumentTypeID"></param>
        private void GetList(string DocumentTypeID)
        {
            string DocFields = "PK_DocumentID,Status,[Tên loại đối tượng kiểm toán],[Diễn giải]";
            string PropertyFields = "Tên loại đối tượng kiểm toán,Diễn giải";
            string Condition = " and Status = 4";

            ObjectDataSource1.SelectParameters["DocumentTypeID"].DefaultValue = DocumentTypeID;
            ObjectDataSource1.SelectParameters["DocFields"].DefaultValue = DocFields;
            ObjectDataSource1.SelectParameters["PropertyFields"].DefaultValue = PropertyFields;
            ObjectDataSource1.SelectParameters["Condition"].DefaultValue = Condition;
            dataCtrl.DataBind();
            dataCtrl.PageSize = 10;
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
                Label PK_DocumentID = (Label)e.Item.FindControl("PK_DocumentID") as Label;
                CheckBox chk = (CheckBox)e.Item.FindControl("chkItem") as CheckBox;
                DataRowView drv = e.Item.DataItem as DataRowView;
                //if (!string.IsNullOrEmpty(_roleID))
                //{
                //    if (CommonFunc.IsRoleAssignedOnComponent(PK_COMPONENTID.Text, _roleID, objContext, dbName)) chk.Checked = true;
                //}
                if (chk != null)
                {
                    chk.Attributes.Add("documentID", PK_DocumentID.Text);
                    chk.Attributes.Add("tenLDTKT", drv["Tên loại đối tượng kiểm toán"].ToString());
                    chk.Attributes.Add("diengiai", drv["Diễn giải"].ToString());
                }
            }
        }

        [WebMethod]
        public static string themLoaiDTKT(Guid id, string ten, string diengiai, string donvi, int nam)
        {
            List<dm_loaidoituongkiemtoan> lst = ManagerFactory.loaidoituongkiemtoan_manager.GetList(x => x.Nam == int.Parse(_year) && x.SourceId == id && x.Phongban == donvi);
            if (lst.Count == 0)
            {
                dm_loaidoituongkiemtoan loaiDTKT = new dm_loaidoituongkiemtoan();
                loaiDTKT.SourceId = id;
                loaiDTKT.Ten = ten;
                loaiDTKT.Diengiai = diengiai;
                loaiDTKT.Phongban = _donvi;
                loaiDTKT.Nam = nam;
                ManagerFactory.loaidoituongkiemtoan_manager.Insert(loaiDTKT);
                return "Thêm mới loại ĐTKT "+ten+" thành công.";
            }
            return "Loại ĐTKT "+ten+" đã tồn tại.";
        }

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
        #endregion
    }
}