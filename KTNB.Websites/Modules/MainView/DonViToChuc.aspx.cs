using System;
using System.Data;
using System.Web.UI.WebControls;
using CORE.CoreObjectContext;
using CORE.UMS.CoreBusiness;
using vpb.app.business.ktnb.CoreBusiness;
using vpb.app.business.ktnb.Definition.DMS;
using vpb.app.business.ktnb.Definition.WFS;
using vpb.app.business.ktnb.Definition.UMS;
using VPB_KTNB.Helpers;

namespace VPB_PROMOTION.MainView
{
    public partial class MainView_DonViToChuc : PageBase
    {
        #region initiation page variables
        string _op = string.Empty;
        string _docspace = string.Empty;
        string _applicationid = string.Empty;
        string _applicationname = string.Empty;
        string _componentid = string.Empty;
        string _componentname = string.Empty;
        string _image = string.Empty;
        string _leafColor = "#0E602F";
        #endregion

        #region page init & load
        /// <summary>
        /// OnInit
        /// </summary>
        /// <param name="e"></param>
        /// <author>dungnt</author>
        /// <createddate>2008</createddate>
        protected override void OnInit(EventArgs e)
        {
            base.AuthorizeUserCtx();
            base.OnInit(e);

            #region get data submit
            //_op = Request["op"];
            //_docspace = Request["docspace"];
            //_applicationid = Request["a"];
            //_applicationname = Request["an"];
            //_componentid = Request["c"];
            //_componentname = Request["cn"];
            _image = "treeroot.gif";
            #endregion

            #region init tree
            treeFolder.Attributes.Add("onmousemove", "{window.status='';}");
            treeFolder.RootNodeStyle.ImageUrl = "~/Images/AppCom/" + _image;
            treeFolder.RootNodeStyle.HorizontalPadding = Unit.Pixel(2);
            #endregion
        }
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>dungnt</author>
        /// <createddate>2008</createddate>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //if (_m_roleID == vpb.app.business.ktnb.Definition.UMS.ROLES.TRUONGPHONG_CSCC)
                    LoadTreeGroup_Fast("Cơ cấu tổ chức");
                //else
                    //LoadTreeGroup_Fast("Chi nhánh/Phòng GD/Phòng ban");
            }
        }
        #endregion

        #region page helper processing
        /// <summary>
        /// LoadTreeAudit
        /// </summary>
        /// <param name="rootText"></param>
        protected void LoadTreeAudit(string rootText)
        {
            //make root node with text
            TreeNode rootNode = new TreeNode();
            rootNode.Text = rootText;
            treeFolder.Nodes.Add(rootNode);
        }
        
        /// <summary>
        /// LoadTreeGroup_Fast
        /// </summary>
        /// <param name="rootText"></param>
        protected void LoadTreeGroup_Fast(string rootText)
        {
            string condition = "";
            TreeNode rootNode = new TreeNode();
            rootNode.Text = rootText;
            treeFolder.Nodes.Add(rootNode);

            condition += " AND FK_PARENTGROUPID is null";

            condition += " AND Type=" + GROUPTYPE.DONVI + " Order By Name";

            AppCached.group_list = "ds_allgroup";

            string query = "PK_GroupID,Name,Mnemonic,Description,FK_ParentGroupID,Type";

            bus_Group objgrp = bus_Group.Instance(_objUserContext);
            
            DataSet ds_allgroup = objgrp.getList(condition, query);
            DataSet listAllGroup = objgrp.getList("AND Type=" + GROUPTYPE.DONVI+" Order By Name", query);
            DataRow[] dr_parent_group = null;

            dr_parent_group = ds_allgroup.Tables[0].Select("", "Name"); 

            addChildNote(listAllGroup, dr_parent_group, rootNode);
            treeFolder.ExpandAll();
        }
        private void addChildNote(DataSet allGroup, DataRow[] parentGroup, TreeNode parentNode)
        {
            DataRow[] dr_child_group;
            string fk_parentgroupid = "";
            string nodeText = "";
            foreach (DataRow r_parent_group in parentGroup)
            {
                fk_parentgroupid = r_parent_group["pk_groupid"].ToString();
                TreeNode node_parentgroup = new TreeNode();
                nodeText = r_parent_group["Name"].ToString() + "....." + r_parent_group["Description"].ToString();
                node_parentgroup.Text = "<img src='../../Images/001_20.gif' border='0' width='16px' hieght='16px'></img><font color='" + _leafColor + "'> " + nodeText + "</font>";
                node_parentgroup.NavigateUrl = "../UMS/DonVi.aspx?id=" + fk_parentgroupid + "&name=" + nodeText + "&docspace=" + _docspace;
                node_parentgroup.Target = "fraTopic";
                parentNode.ChildNodes.Add(node_parentgroup);

                dr_child_group = allGroup.Tables[0].Select("FK_ParentGroupID='" + fk_parentgroupid + "'", "Name");
                if (dr_child_group.Length == 0) continue;
                foreach (DataRow r_child_group in dr_child_group)
                {
                    addChildNote(allGroup, dr_child_group, node_parentgroup);
                }
            }
        }
       
        #endregion
    }
}