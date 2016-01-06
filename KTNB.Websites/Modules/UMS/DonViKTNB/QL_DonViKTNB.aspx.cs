using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KTNB.Extended.Biz;
using KTNB.Extended.Dal;
using VPB_KTNB.Helpers;

namespace VPB_KTNB.Modules.UMS.DonViKTNB
{
    public partial class QL_DonViKTNB : PageBase
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            base.AuthorizeUserCtx();
            base.SetFormCaption("Danh mục đơn vị KTNB");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
              
            }
        }

        //void LoadDonVi()
        //{
        //    List<group_ktnb> lstGroup = ManagerFactory.group_manager.GetList_Donvi();
        //    rptGroup.DataSource = lstGroup;
        //    rptGroup.DataBind();
        //}

        //protected void rptGroup_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //    {

        //        Label pk_groupid = (Label)e.Item.FindControl("PK_GROUPID");
        //        Label fk_Roleid = (Label)e.Item.FindControl("PK_ROLEID");
        //        Label nameuser = (Label)e.Item.FindControl("lblUser");
        //        Image imgEdit = (Image)e.Item.FindControl("imgEdit") as Image;
        //        Label trangthai = (Label)e.Item.FindControl("TrangThai");
        //        Label lblTrangThai = (Label)e.Item.FindControl("lblStatus");
        //        if (pk_groupid != null && fk_Roleid != null)
        //        {
        //            imgEdit.Attributes.Add("onclick", "editItem('" + pk_groupid.Text + "','" + fk_Roleid.Text + "'" + nameuser.Text + "')");
        //            if (trangthai.Text.ToUpper() == "TRUE")
        //                lblTrangThai.Text = "Active";
        //            else
        //                lblTrangThai.Text = "Inactive";
        //        }
        //    }
        //}
    }
}