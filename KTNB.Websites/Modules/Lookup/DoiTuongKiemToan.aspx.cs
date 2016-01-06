using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.Definition.DMS;
using C1.Web.C1WebGrid;
using KTNB.Extended.Dal.Lib;
using KTNB.Extended.Biz;
using VPB_KTNB.Helpers;

namespace VPB_TDHS.Modules.Lookup
{
    public partial class DoiTuongKiemToan : PageBase
    {
        protected string _action = string.Empty;
        protected string _doctypeid = DOCTYPE.DOITUONG_KT;
        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;
        protected string _docspaceid = string.Empty;
        private string _namedtkt = string.Empty;

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
            _namedtkt = Request["loaidtkt"];
            if (string.IsNullOrEmpty(_documentid))
                _viewtype = VIEWTYPE.ADDNEW;
            else
                _viewtype = VIEWTYPE.EDIT;
            
            base.InitForm("Đối tượng kiểm toán", string.Empty, _doctypeid, 0);
            //_btnAddNew.Visible = true;
            //_btnAddNew.Attributes.Add("onclick", "{newform(); return false;}");
        }
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //GetList(_doctypeid);
        }

        //private void GetList(string DocumentTypeID)
        //{
        //    List<dm_li_doituongkiemtoan> lst = CoreFactory<dm_li_doituongkiemtoan>.EntityManager.GetList();
        //    dataCtrl.DataSource = lst;
        //    dataCtrl.DataBind();
        //}

        protected void btnSearch_Click(object sender, EventArgs e)
        {
        //    string searchName = tbTenDTKT.Text.ToUpper();
        //    string searchType = drpLoaiDTKT.SelectedItem.Value;
        //    List<dm_li_doituongkiemtoan> lst = new List<dm_li_doituongkiemtoan>();
        //    if (!string.IsNullOrEmpty(searchName) && !string.IsNullOrEmpty(searchType))
        //        lst = CoreFactory<dm_li_doituongkiemtoan>.EntityManager.GetList(x => x.Ten.Contains(searchName) && x.IDLDTKT == searchType);
        //    else if (!string.IsNullOrEmpty(searchName))
        //        lst = CoreFactory<dm_li_doituongkiemtoan>.EntityManager.GetList(x => x.Ten.Contains(searchName));
        //    else if (!string.IsNullOrEmpty(searchType))
        //        lst = CoreFactory<dm_li_doituongkiemtoan>.EntityManager.GetList(x => x.IDLDTKT == searchType);
        //    else lst = CoreFactory<dm_li_doituongkiemtoan>.EntityManager.GetList();
        //    dataCtrl.DataSource = lst;
        //    dataCtrl.DataBind();
        }

        protected void dataCtrl_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.AlternatingItem) || (e.Item.ItemType == ListItemType.Item))
            {
                dm_li_doituongkiemtoan item = (dm_li_doituongkiemtoan)e.Item.DataItem;
                Literal Status = (Literal)e.Item.FindControl("Status");
                Status.Text = item.Status == 4 ? "Active" : "Inactive";
            }
        }
    }
}