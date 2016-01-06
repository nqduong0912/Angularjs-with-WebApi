using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORE.UMS.CoreBusiness;
using vpb.app.business.ktnb.Definition.UMS;
using VPB_KTNB.Helpers;

namespace VPB_KTNB.Modules.Admin
{
    public partial class NhanSu_Manager : PageBase
    {
        protected override void OnInit(EventArgs e)
        {

            base.OnInit(e);
            base.AuthorizeUserCtx();
            base.InitForm("Chi nhánh / Phòng GD", string.Empty, String.Empty, 0);
            if (_m_roleID == vpb.app.business.ktnb.Definition.UMS.ROLES.TRUONGPHONG_CSCC)
                LoadGroup("Các đơn vị kiểm toán");
            else
                LoadGroup("Chi nhánh/Phòng GD/Phòng ban");
            this.btnAddnew.Click += new EventHandler(AddNew);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }
        protected void LoadGroup(string rootText)
        {
            string condition = "";
            if (_m_roleID == vpb.app.business.ktnb.Definition.UMS.ROLES.TRUONGPHONG_CSCC)
                condition += " AND FK_PARENTGROUPID='" + vpb.app.business.ktnb.Definition.UMS.GROUPS.GROUP_OF_KTNB + "'";

            condition += "Order By Name";

            AppCached.group_list = "ds_allgroup";

            string query = "PK_GroupID,Name,Mnemonic,Description,FK_ParentGroupID,Type";

            bus_Group objgrp = bus_Group.Instance(_objUserContext);
            DataSet ds_allgroup;
            ds_allgroup = objgrp.getList(condition, query);
            foreach (DataRow dr in ds_allgroup.Tables[0].Rows)
            {
                string text = dr["Description"].ToString();
                string value = "../UMS/UserInGroup.aspx?id=" + dr["PK_GroupID"].ToString() + "&name=" + dr["Description"].ToString() + "&docspace=appuser";
                cboChiNhanh.Items.Add(new ListItem(text, value));
            }
        }
        protected void AddNew(object sender, EventArgs e)
        {
            Response.Redirect("~/Modules/UMS/NewUser.aspx");
        }

        protected void cboChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(cboChiNhanh.SelectedValue);
        }
    }
}