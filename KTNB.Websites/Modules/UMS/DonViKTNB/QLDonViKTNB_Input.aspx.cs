using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KTNB.Extended.biz;
using KTNB.Extended.dal;
using VPB_CRM.Helper;

namespace VPB_KTNB.Modules.UMS.DonViKTNB
{
    public partial class QLDonViKTNB_Input : PageBase
    {
        #region initiation page variables

        private string _action = string.Empty;
        private string _groupid = string.Empty;
        private string _roleid = string.Empty;
        private string _user = string.Empty;
        private group_ktnb infoGroup;
        #endregion

        #region page init & load

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            base.AuthorizeUserCtx();
            _action = Request["act"];
            _groupid = Request["groupid"];
            _roleid = Request["roleid"];
            _user = Request["user"];
            if(!string.IsNullOrEmpty(_action))
                base.SetFormCaption("Thông tin đơn vị kiểm toán nội bộ");
            else
                base.SetFormCaption("Thêm mới đơn vị kiểm toán nội bộ");

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUser();
                CommonFunc.LoadStatus(drpTT);
            }
        }
        #endregion

        #region page helper processing

        void LoadUser()
        {
            List<dm_nhansu> lstNhansu = ManagerFactory.user_manager.GetList();
            if(lstNhansu.Count > 0)
                foreach (dm_nhansu itemUser in lstNhansu)
                    drpTP.Items.Add(new ListItem(itemUser.Name, itemUser.PK_UserID.ToString()));
        }
        #endregion

        #region button processing

        #endregion

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            
            if (!string.IsNullOrEmpty(_action))
            {
                
            }

            else
            {
                Guid _pkGroupid = Guid.NewGuid();
                Guid _fkRoleid = Guid.NewGuid();
                infoGroup = new group_ktnb()
                {
                    Pk_groupid = _pkGroupid,
                    Fk_Roleid = _fkRoleid,
                    Id_group = txtMaDV.Text,
                    LeaderName = drpTP.SelectedItem.Value,
                    IsActive = bool.Parse(drpTT.SelectedItem.Value),
                    Name = txtTenDV.Text,
                    Resource = int.Parse(txtNL.Text)
                };
                ManagerFactory.group_manager.AddNewGroup(infoGroup);
            }
        }
    }
}