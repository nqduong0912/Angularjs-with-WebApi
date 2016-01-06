using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.Definition.DMS;
using C1.Web.C1WebGrid;
using KTNB.Extended.Dal;
using KTNB.Extended.Biz;
using VPB_KTNB.Helpers;

namespace VPB_KTNB.Modules.Lookup.DanhMuc.TieuChiNam
{
    public partial class PhanLoaiBoTieuChiNam : PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;
        protected string _doctypeid = DOCTYPE.PHANLOAI_BOTIEUCHI_NAM;
        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;
        protected string _docspaceid = string.Empty;
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
            _documentid = Request["doc"];
            if (string.IsNullOrEmpty(_documentid))
                _viewtype = VIEWTYPE.ADDNEW;
            else
                _viewtype = VIEWTYPE.EDIT;

            #region get data submit
            #endregion

            #region action handler

            #endregion

            #region init form
            base.InitForm("Phân loại bộ tiêu chí năm", string.Empty, _doctypeid, 0);
            _btnAddNew.Visible = true;
            _btnAddNew.Attributes.Add("onclick", "{newform(); return false;}");
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
            GetList(_doctypeid);
            //Lấy danh sách đối tượng kiểm toán
            List<dm_loaidoituongkiemtoan> lstLoaiDTKT = ManagerFactory.loaidoituongkiemtoan_manager.GetList();
            //Nếu có thì load vào dropdownlist
            if(lstLoaiDTKT.Count > 0)
            {
                foreach(dm_loaidoituongkiemtoan info in lstLoaiDTKT)
                {
                    drpLoaiDTKT.Items.Add(new ListItem(info.Ten, info.Id.ToString()));
                }
            }
            //Nếu không có thì lấy toàn bộ danh sách hiện tại cho vào Khu extend
            else
            {
                //List<dm_loaidoituongkiemtoan> lstLoaiDTKTFromLibrary = ManagerFactory.loaidoituongkiemtoan_manager.GetLoaiDTKTFromLibrary(DOCTYPE.LOAI_DOITUONG_KT);
                //if (lstLoaiDTKTFromLibrary.Count > 0)
                //{
                //    ManagerFactory.loaidoituongkiemtoan_manager.InsertOrUpdateAll(lstLoaiDTKTFromLibrary);
                //    //dm_loaidoituongkiemtoan info = new dm_loaidoituongkiemtoan();
                //    //ManagerFactory.loaidoituongkiemtoan_manager.Insert(info);
                //    //ManagerFactory.loaidoituongkiemtoan_manager.GetInfo(9);
                //    //ManagerFactory.loaidoituongkiemtoan_manager.GetList();
                //}
            }
        }
        #endregion

        #region page helper processing
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DocumentTypeID"></param>
        private void GetList(string DocumentTypeID)
        {
            string DocFields = "PK_DocumentID,Status,[Tên phân loại bộ tiêu chí năm],[Diễn giải]";
            string PropertyFields = "Tên phân loại bộ tiêu chí năm,Diễn giải";
            string Condition = string.Empty;
       
            ObjectDataSource1.SelectParameters["DocumentTypeID"].DefaultValue = DocumentTypeID;
            ObjectDataSource1.SelectParameters["DocFields"].DefaultValue = DocFields;
            ObjectDataSource1.SelectParameters["PropertyFields"].DefaultValue = PropertyFields;
            ObjectDataSource1.SelectParameters["Condition"].DefaultValue = Condition;
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
                Label PK_DocumentID = (Label)e.Item.FindControl("PK_DocumentID") as Label;
                Label Status = (Label)e.Item.FindControl("Status") as Label;
                Image imgEdit = (Image)e.Item.FindControl("imgEdit") as Image;

                if (PK_DocumentID != null)
                    imgEdit.Attributes.Add("onclick", "{LoadDocument('" + PK_DocumentID.Text + "')}");

                if (Status != null)
                {
                    if (Status.Text == "2")
                        Status.Text = "Inactive";
                    else if (Status.Text == "4")
                        Status.Text = "Active";
                }
            }
        }
        #endregion

        protected void drpYear_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void drpLoaiDTKT_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}