using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using CORE.CoreObjectContext;
using vpb.app.business.ktnb.CoreBusiness;
using CORE.UMS.CoreBusiness;
using vpb.app.business.ktnb.Definition.UMS;
using vpb.app.business.ktnb.Definition.OPERATORS;
using VPB_KTNB.Helpers;

namespace VPB_PROMOTION.UMS
{
    public partial class UMS_DonVi : PageBase
    {
        #region declare variables here
        string _groupID = string.Empty;
        DataSet dsDonVi = null;
        bus_Group objGroup = null;
        string _parentnodeid = string.Empty;
        protected string _action = string.Empty;
        protected string _companycode = string.Empty;
        protected string _mnemonic = string.Empty;
        protected string _parentgroupid = string.Empty;
        protected string _companyname = string.Empty;
        protected byte _isexpired = 0;
        protected string _docspace;
        #endregion

        /// <summary>
        /// OnInit
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            base.AuthorizeUserCtx();

            objGroup = new bus_Group(_objUserContext, _dbName);

            #region get data submit
            _docspace = Request["docspace"];
            _groupID = "" + Request["id"];
            _parentnodeid = "" + Request["parentnodeid"];
            _action = "" + Request["act"];
            _companycode = Request["cocode"];
            _mnemonic = Request["mnemonic"];
            _parentgroupid = Request["parentgroup"];
            if (string.IsNullOrEmpty(_parentnodeid)) _parentnodeid = CommonFunc.getParentGroupID(_groupID, _objUserContext, _dbName);
            _companyname = Request["des"];
            _isexpired = Convert.ToByte(Request["exp"]);
            _m_grouptype = GROUPTYPE.DONVI;
            #endregion

            #region action handler
            if (_action.ToUpper().Equals("DEL"))
                Delete(_groupID);
            else if (_action.ToUpper().Equals("EDIT"))
                UpdateGroup(_groupID, _companycode, _mnemonic, _companyname, _isexpired, _parentgroupid, _m_grouptype);
            #endregion

            //string caption = "Thông tin chi nhánh/phòng giao dịch";
            //if (_docspace == "supportgroup")
            //{
            //    caption = "Thông tin trung tâm hỗ trợ";
            //}
            string caption = "Thông tin chi nhánh/phòng giao dịch";
            if (_m_roleID == ROLES.TRUONGPHONG_CSCC)
                caption = "Thông tin Đơn vị KTNB";
            base.InitForm(caption, "group.gif", string.Empty, 0);

            #region client control event handler
            _btnDelete.Attributes.Add("onclick", "{deletegroup(this);return false;}");
            _btnEdit.Attributes.Add("onclick", "{updategroup();return false;}");
            if ((_groupID.ToUpper().Equals(GROUPS.SYS_ADMIN)) || (_groupID.ToUpper().Equals(GROUPS.APP_ADMIN)))
            {
                _btnDelete.Enabled = false;
            }
            _btnEdit.Visible = true;
            _btnDelete.Visible = true;
            if (_docspace == "supportgroup")
            {
                _btnMonitoring.Visible = true;
                _btnMonitoring.Text = "Danh sách hỗ trợ";
                _btnMonitoring.Width = Unit.Pixel(130);
                string url = "viewgroupbytype.aspx?type=" + GROUPTYPE.TT_HOTRO + "&groupid=" + _groupID;
                _btnMonitoring.Attributes.Add("onclick", "{openurl('" + url + "');return false;}");
            }
            this.chkTrungTamHT.Attributes.Add("onclick", "{toogleCNQuanLy();}");
            #endregion
        }
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            _btnAddNew.Visible = true;
            _btnAddNew.Click += new EventHandler(AddNew);
            LoadGroupDetail(_groupID);
            loadBranches();
            string Query = string.Empty;
            if (!string.IsNullOrEmpty(_parentnodeid))
            {
                this.chkPGD.Checked = true;
                this.cboDonVi.SelectedValue = _parentnodeid;
                this.chkTrungTamHT.Enabled = false;
            }
        }
        /// <summary>
        /// AddNew
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AddNew(object sender, EventArgs e)
        {
            Response.Redirect("DonVi_Input.aspx?docspace=" + _docspace);
        }
        /// <summary>
        /// LoadGroupDetail
        /// </summary>
        /// <param name="groupID"></param>
        protected void LoadGroupDetail(string groupID)
        {
            dsDonVi = objGroup.getByID(groupID, string.Empty);

            if (!base.isValidDataSet(dsDonVi))
                return;

            DataRow R = dsDonVi.Tables[0].Rows[0];

            this.FULLNAME.Text = R["NAME"].ToString();
            this.Mnemonic.Text = R["MNEMONIC"].ToString();
            this.DESCRIPTION.Text = R["DESCRIPTION"].ToString();

            if ((Convert.ToInt32(R["ISEXPIRED"])) == 1)
                this.IsExpired.Checked = false;
            else
                this.IsExpired.Checked = true;

            if ((Convert.ToInt32(R["TYPE"])) == 1)
                this.chkTrungTamHT.Checked = true;
            else
                this.chkTrungTamHT.Checked = false;

        }
        /// <summary>
        /// UpdateGroup
        /// </summary>
        /// <param name="groupID"></param>
        /// <param name="companycode"></param>
        /// <param name="mnemonic"></param>
        /// <param name="companyname"></param>
        /// <param name="isExpired"></param>
        /// <param name="parentgroupid"></param>
        protected void UpdateGroup(string groupID, string companycode, string mnemonic, string companyname, byte isExpired, string parentgroupid, byte grouptype)
        {
            dsDonVi = objGroup.getByID(groupID, string.Empty);

            dsDonVi.Tables[0].Rows[0]["NAME"] = companycode;
            dsDonVi.Tables[0].Rows[0]["MNEMONIC"] = mnemonic;
            dsDonVi.Tables[0].Rows[0]["DESCRIPTION"] = companyname;
            dsDonVi.Tables[0].Rows[0]["ISEXPIRED"] = isExpired;
            dsDonVi.Tables[0].Rows[0]["TYPE"] = grouptype;

            if (string.IsNullOrEmpty(parentgroupid))
                dsDonVi.Tables[0].Rows[0]["FK_ParentGroupID"] = DBNull.Value;
            else
                dsDonVi.Tables[0].Rows[0]["FK_ParentGroupID"] = parentgroupid;
            dsDonVi.Tables[0].Rows[0]["ActivationDateTime"] = DBNull.Value;
            dsDonVi.Tables[0].Rows[0]["OrderIndex"] = DBNull.Value;

            if (objGroup.saveDataSet(dsDonVi) == 0)
            {
                CommonFunc.AddAuditLog(0, AUDITLOG.EDIT, "Cập nhật", "Cập nhật thông tin CN/PGD " + companycode, _objUserContext);
                Response.Write("CN/PGD đã được cập nhật.");
                Response.Flush();
                Response.End();
            }
            else
            {
                Response.Write("Không cập nhật được CN/PGD.");
                Response.Flush();
                Response.End();
            }
        }
        /// <summary>
        /// EditForm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void EditForm(object sender, EventArgs e)
        {
            string companycode = Request.Form["ctl00$FormContent$FULLNAME"];
            string companyname = Request.Form["ctl00$FormContent$DESCRIPTION"];

            dsDonVi.Tables[0].Rows[0]["NAME"] = companycode;
            dsDonVi.Tables[0].Rows[0]["DESCRIPTION"] = companyname;
            byte isExpired = 1;
            if (!string.IsNullOrEmpty(Request.Form["ctl00$FormContent$IsExpired"]))
                isExpired = 0;
            dsDonVi.Tables[0].Rows[0]["ISEXPIRED"] = isExpired;
            dsDonVi.Tables[0].Rows[0]["FK_ParentGroupID"] = DBNull.Value;
            dsDonVi.Tables[0].Rows[0]["ActivationDateTime"] = DBNull.Value;
            dsDonVi.Tables[0].Rows[0]["OrderIndex"] = DBNull.Value;

            if (objGroup.saveDataSet(dsDonVi) == 0)
            {
                Response.Write("<script language='javascript'>");
                Response.Write("window.open('../MainView/MainViewerDVTC.aspx?docspace=DonVi&toc=new','fraDetail');");
                Response.Write("</script>");
            }
        }
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="groupID"></param>
        protected void Delete(string groupID)
        {
            bus_Group objGroup = new bus_Group(_objUserContext, _dbName);
            if (objGroup.deleteByID(groupID) == 0)
            {
                Response.Write("Đã xóa CN/PGD.");
                Response.End();
            }
            else
            {
                Response.Write("Không xóa được CN/PGD.");
                Response.Flush();
                Response.End();
            }

        }
        /// <summary>
        /// loadBranches
        /// </summary>
        protected void loadBranches()
        {
            string condition = " AND Type = " + GROUPTYPE.DONVI;
            
            condition += " ORDER BY Description";
            string query = "PK_GroupID,Name+'.....'+Description as Description";
            bus_Group objGroup = new bus_Group(_objUserContext, _dbName);
            DataSet ds = objGroup.getList(condition, query);
            base.BindData2Combo(this.cboDonVi, ds, "Description", "PK_GroupID", string.Empty);
            this.cboDonVi.Items.Insert(0, new ListItem(""));
        }
    }
}