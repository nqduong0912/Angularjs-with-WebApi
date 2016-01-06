using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.Definition.DMS;
using C1.Web.C1WebGrid;
using System.Data;
using KTNB.Extended.Biz;
using KTNB.Extended.Dal;
using KTNB.Extended.Dal.Lib;
using VPB_KTNB.Helpers;

namespace VPB_KTNB.Modules.Lookup.DanhMuc.TieuChiNam
{
    public partial class TieuChiThanhPhan : PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;
        protected string _doctypeid = DOCTYPE.TIEUCHI_THANHPHAN;
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
            base.InitForm("Danh mục tiêu chí thành phần", string.Empty, _doctypeid, 0);
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
            //string DocFields = "PK_DocumentID,Status,[Tên],[Diễn giải],[Tỷ trọng],[Tên tiêu chí chính],[Loại tiêu chí]";
            //string PropertyFields = "Tên,Diễn giải,Tỷ trọng,Tên tiêu chí chính,Loại tiêu chí";
            //string Condition = string.Empty;
            //rptDanhmuc.DataSource = CoreFactory<dm_li_kehoachnam_tieuchitp>.EntityManager.GetDMFromLibrary("EF498D3B-C2F7-47DE-9BF0-793F6D23D43D", DocFields, PropertyFields, Condition);
            rptDanhmuc.DataSource = CoreFactory<dm_li_kehoachnam_tieuchitp>.EntityManager.GetList();
            rptDanhmuc.DataBind();
        }
        #endregion
       
        protected void rptDanhmuc_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.AlternatingItem) || (e.Item.ItemType == ListItemType.Item))
            {
                dm_li_kehoachnam_tieuchitp item = (dm_li_kehoachnam_tieuchitp)e.Item.DataItem;
                Literal Status = (Literal)e.Item.FindControl("Status");
                Literal Loaitieuchi = (Literal)e.Item.FindControl("Loaitieuchi");

                Status.Text = item.Status == 4 ? "Active" : "Inactive";
                Loaitieuchi.Text = item.Loaitieuchi == 0 ? "Định tính" : "Định lượng";
            }
        }
    }
}