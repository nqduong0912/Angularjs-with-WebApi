using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORE.CoreObjectContext;
using CORE.UMS.CoreBusiness;
using KTNB.Extended.Biz;
using KTNB.Extended.Dal;
using vpb.app.business.ktnb.Definition.DMS;
using vpb.app.business.ktnb.Definition.UMS;
using vpb.app.business.ktnb.Definition.OPERATORS;
using VPB_KTNB.Helpers;

namespace VPB_PROMOTION.UMS
{
    public partial class UMS_NewUser : PageBase
    {
        #region initiation page variables
        protected string _chinhanhid = string.Empty;
        protected string _action = string.Empty;
        protected string _fullname = string.Empty;
        protected string _email = string.Empty;
        protected string _mobile = string.Empty;
        protected string _desc = string.Empty;
        protected string _uname = string.Empty;
        protected string _ucode = string.Empty;
        protected string _pwd = string.Empty;
        protected string _cn = string.Empty;
        protected string _pgd = string.Empty;
        protected string _vaitro = string.Empty;
        protected byte _exp = 0;
        protected string _docspace = string.Empty;
        protected string _caption = string.Empty;
        protected string _edu = string.Empty;
        protected string _joinDate = string.Empty;
        protected string _birthDate = string.Empty;
        protected dm_nhansu userNhansu;
        #endregion

        /// <summary>
        /// OnInit
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.AuthorizeUserCtx();
            base.OnInit(e);

            #region get data submit
            _action = "" + Request["act"];
            _fullname = Request["fn"];
            _email = Request["em"];
            _mobile = Request["mb"];
            _desc = Request["des"];
            _uname = Request["un"];
            _ucode = Request["uc"];
            _pwd = Request["pwd"];
            _cn = Request["cn"];
            _pgd = Request["pgd"];
            _vaitro = Request["vt"];
            _exp = Convert.ToByte(Request["exp"]);
            _docspace = Request["docspace"];
            _chinhanhid = Request["ctl00$FormContent$cboChiNhanh"];
            _edu = Request["edu"];
            _joinDate = Request["joinDate"];
            _birthDate = Request["birthDate"];
            #endregion

            #region action handler
            if (_action.ToUpper().Equals("NEW"))
            {
                CreateNewUser(_fullname, _email, _mobile, _desc, _uname, _ucode, _pwd, _cn, _pgd, _vaitro, _exp, _edu, DateTime.Parse(_joinDate), DateTime.Parse(_birthDate));
            }
            #endregion

            _caption = "Người sử dụng";
            if (_docspace.ToLower() == "sysuser") _caption = "Người quản trị";
            base.InitForm(_caption, "UserInfo.gif", string.Empty, VIEWTYPE.ADDNEW);

            #region client control event handler
            UserName.Attributes.Add("onblur", "checkAccountName(this,this.value)");
            _btnSave.Attributes.Add("onclick", "{createuser(); return false;}");
            #endregion

        }
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadChiNhanh();
            if (_docspace.ToLower() == "appuser")
                LoadPGD(_chinhanhid);
            else
                this.cboPGD.Enabled = false;

            LoadRole();

            if (!string.IsNullOrEmpty(_chinhanhid))
                cboChiNhanh.SelectedValue = _chinhanhid;
            UserName.Focus();
        }

        /// <summary>
        /// LoadChiNhanh
        /// </summary>
        protected void LoadChiNhanh()
        {
            this.cboChiNhanh.Items.Clear();

            string condition = "";

            if (_docspace == "appuser")
            {
                if (_m_roleID == vpb.app.business.ktnb.Definition.UMS.ROLES.TRUONGPHONG_CSCC)
                    condition = " And PK_GroupID='" + vpb.app.business.ktnb.Definition.UMS.GROUPS.GROUP_OF_KTNB + "'";
                else
                    condition = " And Type In (" + GROUPTYPE.CHI_NHANH + "," + GROUPTYPE.TT_HOTRO + ")";
            }
            else if (_docspace == "sysuser")
                condition = " And Type In (" + GROUPTYPE.APP_ADMIN + "," + GROUPTYPE.SYS_ADMIN + ")";

            condition += " ORDER BY Description";

            bus_Group objGroup = bus_Group.Instance(_objUserContext);
            string query = "PK_GROUPID,NAME,DESCRIPTION,NAME+'...'+DESCRIPTION AS FULLNAME";
            DataSet ds = objGroup.getList(condition, query);

            if (!base.isValidDataSet(ds))
                return;

            base.BindData2Combo(cboChiNhanh, ds, "FULLNAME", "PK_GROUPID", _chinhanhid);
            cboChiNhanh.Items.Insert(0, new ListItem("", ""));
        }
        /// <summary>
        /// LoadRole
        /// </summary>
        protected void LoadRole()
        {
            string query = "PK_ROLEID,NAME";
            string condition = " AND ISEXPIRED=0";

            if (_docspace.ToLower() == "appuser")
            {
                if (_m_roleID == vpb.app.business.ktnb.Definition.UMS.ROLES.TRUONGPHONG_CSCC)
                    condition += " AND PK_ROLEID IN ('" + vpb.app.business.ktnb.Definition.UMS.ROLES.CANBO_DUYET + "','" + vpb.app.business.ktnb.Definition.UMS.ROLES.THANHVIEN_KIEMTOAN + "','" + vpb.app.business.ktnb.Definition.UMS.ROLES.TRUONGPHONG_DONVI_KIEMTOAN + "')";
                else
                    condition += " AND PK_ROLEID NOT IN ('" + ROLES.SYS_ADMIN + "','" + ROLES.APP_ADMIN + "')";
            }
            else if (_docspace.ToLower() == "sysuser")
                condition += " AND PK_ROLEID IN ('" + ROLES.SYS_ADMIN + "','" + ROLES.APP_ADMIN + "')";

            condition += " ORDER BY DESCRIPTION";

            bus_Role objRole = new bus_Role(_objUserContext, _dbName);
            DataSet ds = objRole.getList(condition, query);

            if (!base.isValidDataSet(ds))
                return;
            base.BindData2Combo(cboVaiTro, ds, "NAME", "PK_ROLEID", string.Empty);
        }
        /// <summary>
        /// LoadPGD
        /// </summary>
        /// <param name="chinhanhid"></param>
        protected void LoadPGD(string chinhanhid)
        {
            cboPGD.Items.Clear();

            if (string.IsNullOrEmpty(_chinhanhid)) return;

            string condition = " AND ISEXPIRED=0 AND FK_PARENTGROUPID ='" + chinhanhid + "' ORDER BY Description";
            string query = "PK_GROUPID,NAME,DESCRIPTION,NAME+'...'+DESCRIPTION AS FULLNAME";

            bus_Group objGroup = bus_Group.Instance(_objUserContext);
            DataSet ds = objGroup.getList(condition, query);
            if (!base.isValidDataSet(ds))
                return;
            else
            {
                base.BindData2Combo(this.cboPGD, ds, "FULLNAME", "PK_GROUPID", string.Empty);

                if (_m_roleID != vpb.app.business.ktnb.Definition.UMS.ROLES.TRUONGPHONG_CSCC)
                    cboPGD.Items.Insert(0, new ListItem("ALL", "ALL"));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullname"></param>
        /// <param name="email"></param>
        /// <param name="mobile"></param>
        /// <param name="desc"></param>
        /// <param name="uname"></param>
        /// <param name="ucode"></param>
        /// <param name="pwd"></param>
        /// <param name="cn"></param>
        /// <param name="pgd"></param>
        /// <param name="vaitro"></param>
        /// <param name="exp"></param>
        protected void CreateNewUser(string fullname, string email, string mobile, string desc, string uname, string ucode, string pwd, string cn, string pgd, string vaitro, byte exp, string edu, DateTime joinDate,
            DateTime birthDate)
        {
            if ((string.IsNullOrEmpty(pgd)) || (pgd.Equals("ALL")))
                pgd = cn;
            string mess = "";
            userNhansu = new dm_nhansu();
            userNhansu.PK_UserID = System.Guid.NewGuid();
            userNhansu.UserCode = ucode;
            userNhansu.Name = uname;
            userNhansu.PassWord = pwd;
            userNhansu.Description = desc;
            userNhansu.IsExpired = exp;
            userNhansu.Email = email;
            userNhansu.MobilePhone = mobile;
            userNhansu.Order_Number = "0";
            userNhansu.Fullname= fullname.Replace(",", "");
            userNhansu.Address = "";
            userNhansu.IsAuthenticateSQL = 0;
            userNhansu.AvatarURL = "";
            userNhansu.JoinDate = joinDate;
            userNhansu.BirthDate= birthDate;
            userNhansu.EducationLevel= edu;
            int errorNumber = ManagerFactory.user_manager.SetNhansu(userNhansu, pgd, vaitro);
            //int errorNumber = objUser.addnewDataSet(dsUser);
            if (errorNumber == 0)
            {
                CommonFunc.AddAuditLog(0, AUDITLOG.CREATE, "Thêm mới", "Thêm mới người dùng " + uname, _objUserContext);
                mess = "Tạo người sử dụng mới thành công.";
                _objUserContext.setPwd(uname, "123456");
            }
            else
            {
                if (errorNumber == 100)
                    mess = "Trùng lặp mã DAO của user khác, kiểm tra và nhập lại.";
                else
                    mess = "Lỗi, không tạo mới được người sử dụng.";
            }

            Response.Write(mess);
            Response.Flush();
            Response.End();
        }
        /// <summary>
        /// SaveForm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void SaveForm(object sender, EventArgs e)
        {
            //base.AddnewForm(sender, e);
            return;
        }
    }
}