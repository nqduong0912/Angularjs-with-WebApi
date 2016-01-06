using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CORE.CoreObjectContext;
using CORE.UMS.CoreBusiness;
using vpb.app.business.ktnb.Definition.DMS;
using vpb.app.business.ktnb.Definition.UMS;
using VPB_KTNB.Helpers;

namespace VPB_PROMOTION.UMS
{
    public partial class UMS_NewGroup : PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;
        protected string _grpname = string.Empty;
        protected byte _grptype = 0;
        protected string _mnemonic = string.Empty;
        protected string _grpdes = string.Empty;
        protected byte _isexpired = 0;
        protected string _parentgrp = string.Empty;
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
            
            #region get data submit
            _action = "" + Request["act"];
            _grpname = Request["name"];
            if (!string.IsNullOrEmpty(Request["type"]))
                _grptype = Convert.ToByte(Request["type"]);
            _mnemonic = Request["mnemonic"];
            _grpdes = Request["desc"];
            _isexpired = Convert.ToByte(Request["isexpired"]);
            _parentgrp = Request["parentgrp"];
            _docspace = Request["docspace"];
            #endregion

            #region action handler
            if (!string.IsNullOrEmpty(_action))
            {
                if (_action.ToUpper() == "NEW")
                    CreateNewGroup(_grpname, _mnemonic, _grpdes, _isexpired, _parentgrp, _grptype);
                else if (_action.ToUpper()=="CHECKCOCODE")
                    CheckCoCode(_grpname);
                else if (_action.ToUpper()=="CHECKMNEMONIC")
                    CheckMnemonic(_mnemonic);
            }
            #endregion

            string caption = "Thêm mới chi nhánh / phòng giao dịch";
            if (_m_roleID == ROLES.TRUONGPHONG_CSCC)
                caption = "Thêm mới Đơn vị KTNB";
            base.InitForm(caption, "group.gif", string.Empty, VIEWTYPE.ADDNEW);

            #region client control event handler
            _btnSave.Attributes.Add("onclick", "{createnewgroup();return false;}");
            CompanyCode.Attributes.Add("onblur", "{verifycocode(this); return false;}");
            Mnemonic.Attributes.Add("onblur", "{verifymnemonic(this); return false;}");
            #endregion
        }
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            loadBranches();
            this.CompanyCode.Focus();
        }
        /// <summary>
        /// CreateNewGroup
        /// </summary>
        /// <param name="name"></param>
        /// <param name="mnemonic"></param>
        /// <param name="description"></param>
        /// <param name="isexpired"></param>
        /// <param name="parentgroupid"></param>
        protected void CreateNewGroup(string name, string mnemonic, string description, byte isexpired, string parentgroupid, byte grptype)
        {
            byte grouptype = grptype;

            bus_Group objGroup = bus_Group.Instance(_objUserContext);
            DataSet ds = objGroup.getEmpty(string.Empty);

            DataRow R = ds.Tables[0].NewRow();

            R["PK_GROUPID"] = Guid.NewGuid();
            R["NAME"] = name.Replace(" ","");
            R["MNEMONIC"] = mnemonic.Replace(" ","");
            R["DESCRIPTION"] = description;
            R["ISEXPIRED"] = isexpired;

            if (string.IsNullOrEmpty(parentgroupid))
            {
                R["FK_PARENTGROUPID"] = DBNull.Value;
                grouptype = GROUPTYPE.CHI_NHANH;
            }
            else
            {
                R["FK_PARENTGROUPID"] = parentgroupid;
                grouptype = GROUPTYPE.PHONG_GD;
            }

            if (grptype == GROUPTYPE.TT_HOTRO) grouptype = grptype;

            R["ACTIVATIONDATETIME"] = DBNull.Value;
            R["ORDERINDEX"] = DBNull.Value;
            R["TYPE"] = grouptype;

            ds.Tables[0].Rows.Add(R);

            if (objGroup.addnewDataSet(ds) == 0)
            {
                Response.Write("CN/PGD đã được tạo.");
                Response.Flush();
                Response.End();
            }
            else
            {
                Response.Write("Không tạo được CN/PGD.");
                Response.Flush();
                Response.End();
            }
        }
        /// <summary>
        /// CheckCoCode
        /// </summary>
        /// <param name="cocode"></param>
        protected void CheckCoCode(string cocode)
        {
            bus_Group objGroup = new bus_Group(_objUserContext, _dbName);
            DataSet ds = objGroup.getList(" AND Name='" + cocode + "'", "Name");
            objGroup = null;
            if (!base.isValidDataSet(ds))
                Response.Write(false.ToString());
            else
                Response.Write(true.ToString());

            Response.Flush();
            Response.End();
        }
        /// <summary>
        /// CheckMnemonic
        /// </summary>
        /// <param name="mnemonic"></param>
        protected void CheckMnemonic(string mnemonic)
        {
            string feedstring = string.Empty;
            bus_Group objGroup = new bus_Group(_objUserContext, _dbName);
            DataSet ds = objGroup.getList(" AND MNEMONIC='" + mnemonic + "'", "MNEMONIC");
            objGroup = null;
            if (base.isValidDataSet(ds))
                feedstring = "dupplicated";
            Response.Write(feedstring);
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
            //base.SaveForm(sender, e);
            bus_Group objGroup = new bus_Group(_objUserContext, _dbName);
            DataSet ds = objGroup.getEmpty(string.Empty);

            DataRow R = ds.Tables[0].NewRow();

            int iIsExpired = 0;
            if (!this.IsExpired.Checked)
                iIsExpired = 1;

            string companycode = Request.Form["ctl00$FormContent$CompanyCode"].ToString().ToUpper();
            string companyname = Request.Form["ctl00$FormContent$CompanyName"].ToString();
            string branchid = Request.Form["ctl00$FormContent$cboBranchCodes"].ToString();

            R["PK_GROUPID"] = Guid.NewGuid();
            R["NAME"] = companycode;
            R["DESCRIPTION"] = companyname;
            R["ISEXPIRED"] = iIsExpired;

            if (string.IsNullOrEmpty(branchid))
                R["FK_PARENTGROUPID"] = DBNull.Value;
            else
                R["FK_PARENTGROUPID"] = branchid;

            R["ACTIVATIONDATETIME"] = DBNull.Value;
            R["ORDERINDEX"] = DBNull.Value;

            ds.Tables[0].Rows.Add(R);
            if (objGroup.addnewDataSet(ds) == 0)
            {
                Response.Write("<script language='javascript'>");
                Response.Write("window.open('../Admin/UserManager.aspx?docspace=group&toc=new','fraDetail');");
                Response.Write("</script>");
            }
        }
        /// <summary>
        /// loadBranches
        /// </summary>
        protected void loadBranches()
        {
            string condition = " AND Type IN (" + GROUPTYPE.CHI_NHANH + ")";
            if (_m_roleID == vpb.app.business.ktnb.Definition.UMS.ROLES.TRUONGPHONG_CSCC)
                condition += " AND PK_GroupID='" + vpb.app.business.ktnb.Definition.UMS.GROUPS.GROUP_OF_KTNB + "'";
            condition += " ORDER BY Description";
            string query = "PK_GroupID,Name+'.....'+Description As Description";
            
            bus_Group objGroup = new bus_Group(_objUserContext, _dbName);
            DataSet ds = objGroup.getList(condition, query);
            cboBranchCodes.Items.Clear();
            base.BindData2Combo(this.cboBranchCodes, ds, "Description", "PK_GroupID", string.Empty);

            if (_m_roleID != vpb.app.business.ktnb.Definition.UMS.ROLES.TRUONGPHONG_CSCC)
                this.cboBranchCodes.Items.Insert(0, new ListItem(""));

            if (_docspace == "supportgroup")
                this.cboBranchCodes.SelectedValue = GROUPS.HOI_SO.ToLower();
        }

    }
}