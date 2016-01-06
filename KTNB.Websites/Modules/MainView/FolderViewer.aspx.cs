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
    public partial class MainView_FolderViewer : PageBase
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
            _op = Request["op"];
            _docspace = Request["docspace"];
            _applicationid = Request["a"];
            _applicationname = Request["an"];
            _componentid = Request["c"];
            _componentname = Request["cn"];
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
            if (_docspace.ToUpper() == DOCSPACE.CAC_DANH_MUC)
                LoadTreeByDocSpace("Các danh mục", _docspace);
            else if (_docspace == "appuser")
            {
                if (_m_roleID == vpb.app.business.ktnb.Definition.UMS.ROLES.TRUONGPHONG_CSCC)
                    LoadtreeUser("Nhân sự đơn vị kiểm toán");
                else
                    LoadtreeUser("Người sử dụng");
            }
            else if (_docspace == "sysuser")
                LoadtreeUser("Nhóm quản trị");
            else if (_docspace == "group")
            {
                if (_m_roleID == vpb.app.business.ktnb.Definition.UMS.ROLES.TRUONGPHONG_CSCC)
                    LoadTreeGroup_Fast("Các đơn vị kiểm toán");
                else
                    LoadTreeGroup_Fast("Chi nhánh/Phòng GD/Phòng ban");
            }
            else if (_docspace == "supportgroup")
                LoadTTHoTro("Các trung tâm hỗ trợ");
            else if (_docspace == "role")
                LoadTreeRole(" Roles");
            else if (_docspace == "syscfg")
                LoadSysParameterNGroup("Tham số hệ thống");
            else if (_docspace == "appcfg")
                LoadAppParameterNGroup("Tham số ứng dụng");
            else if (_docspace == "fm")
                LoadBranchesByComponent(_componentname);
            else if (_docspace == "uyquyen")
                LoadDanhSachUserDaUQ("Danh sách ủy quyền");
            else if (_docspace == "audit")
                LoadTreeAudit("Nhật ký hệ thống");
            //else if ("myactivity42.myactivity16.myactivity1616".LastIndexOf(_docspace) != -1)
            //    Load_HoSoCuaToi(_docspace, DOCTYPE.HOSO_THAMDINH, WORKFLOW_DEFINITION.LUANCHUYEN_HOSO_THAMDINH);
            //else if ("ouractivity42.ouractivity1616".LastIndexOf(_docspace) != -1)
            //    Load_HoSoCuaDonVi(_docspace, DOCTYPE.HOSO_THAMDINH, WORKFLOW_DEFINITION.LUANCHUYEN_HOSO_THAMDINH);
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
        /// 
        /// </summary>
        /// <param name="rootText"></param>
        protected void Load_HoSoCuaToi(string docspace, string doctype, string ProcessDefinition)
        {
            byte wf_status=4;
            string rootText = string.Empty;
            if (docspace == "myactivity42")
            {
                rootText = "Hồ sơ tôi cần xử lý";
            }
            else if (docspace == "myactivity16")
            {
                rootText = "Hồ sơ tôi đã chuyển xử lý";
            }
            else if (docspace == "myactivity1616")
            {
                rootText = "Hồ sơ đã hoàn tất xử lý";
                wf_status = 16;
            }
            #region Root
            TreeNode rootNode = new TreeNode();
            rootNode.Text = rootText;
            treeFolder.Nodes.Add(rootNode);
            #endregion

            #region year/month
            bus_Document obj = bus_Document.Instance(_objUserContext);
            DataSet ds_year = obj.getList(" and FK_DocumentTypeID='" + doctype + "' Order By [Year] Desc", "distinct [Year]");
            if (isValidDataSet(ds_year))
            {
                foreach (DataRow R_Y in ds_year.Tables[0].Rows)
                {
                    TreeNode yearNode = new TreeNode();
                    string scolor = _leafColor;
                    string NodeText = "Năm " + R_Y["Year"].ToString();
                    yearNode.Text = "<img src='../../images/thumuc.gif' border='0' width='16px' hieght='16px'></img><font color='" + scolor + "'>" + NodeText + "</font>";
                    yearNode.ToolTip = NodeText;
                    rootNode.ChildNodes.Add(yearNode);

                    #region add months
                    DataSet ds_month = obj.getList(" and FK_DocumentTypeID='" + doctype + "' And [Year]='" + R_Y["Year"].ToString() + "' Order By [Month] Desc", "distinct [Month]");
                    if (isValidDataSet(ds_month))
                    {
                        foreach (DataRow R_M in ds_month.Tables[0].Rows)
                        {
                            TreeNode monthNode = new TreeNode();
                            NodeText = "Tháng " + R_M["Month"].ToString();
                            monthNode.Text = "<img src='../../images/AppCom/tai_lieu.png' border='0' width='16px' hieght='16px'></img><font color='" + scolor + "'>" + NodeText + "</font>";
                            monthNode.ToolTip = NodeText;
                            monthNode.NavigateUrl = "~/modules/wfs/myactivityinstance.aspx?docspace=" + docspace + "&wf_def_id=" + ProcessDefinition + "&wf_status=" + wf_status + "&y=" + R_Y["Year"].ToString() + "&m=" + R_M["Month"].ToString();
                            monthNode.Target = "fraTopic";
                            yearNode.ChildNodes.Add(monthNode);
                        }
                    }
                    #endregion
                }
            }
            obj = null;
            #endregion

            treeFolder.CollapseAll();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="docspace"></param>
        /// <param name="doctype"></param>
        /// <param name="ProcessDefinition"></param>
        protected void Load_HoSoCuaDonVi(string docspace, string doctype, string ProcessDefinition)
        {
            byte wf_status = 4;
            string rootText = string.Empty;
            if (docspace == "ouractivity42")
            {
                rootText = "Hồ sơ đơn vị cần xử lý";
            }
            else
            {
                rootText = "Hồ sơ đơn vị đã hoàn tất xử lý";
                wf_status = 16;
            }
            #region Root
            TreeNode rootNode = new TreeNode();
            rootNode.Text = rootText;
            treeFolder.Nodes.Add(rootNode);
            #endregion

            #region year/month
            bus_Document obj = bus_Document.Instance(_objUserContext);
            DataSet ds_year = obj.getList(" and FK_DocumentTypeID='" + doctype + "' Order By [Year] Desc", "distinct [Year]");
            if (isValidDataSet(ds_year))
            {
                foreach (DataRow R_Y in ds_year.Tables[0].Rows)
                {
                    TreeNode yearNode = new TreeNode();
                    string scolor = _leafColor;
                    string NodeText = "Năm " + R_Y["Year"].ToString();
                    yearNode.Text = "<img src='../../images/thumuc.gif' border='0' width='16px' hieght='16px'></img><font color='" + scolor + "'>" + NodeText + "</font>";
                    yearNode.ToolTip = NodeText;
                    rootNode.ChildNodes.Add(yearNode);

                    #region add months
                    DataSet ds_month = obj.getList(" and FK_DocumentTypeID='" + doctype + "' And [Year]='" + R_Y["Year"].ToString() + "' Order By [Month] Desc", "distinct [Month]");
                    if (isValidDataSet(ds_month))
                    {
                        foreach (DataRow R_M in ds_month.Tables[0].Rows)
                        {
                            TreeNode monthNode = new TreeNode();
                            NodeText = "Tháng " + R_M["Month"].ToString();
                            monthNode.Text = "<img src='../../images/AppCom/tai_lieu.png' border='0' width='16px' hieght='16px'></img><font color='" + scolor + "'>" + NodeText + "</font>";
                            monthNode.ToolTip = NodeText;
                            monthNode.NavigateUrl = "~/modules/wfs/myactivityinstance.aspx?docspace=" + docspace + "&wf_def_id=" + ProcessDefinition + "&wf_status=" + wf_status + "&y=" + R_Y["Year"].ToString() + "&m=" + R_M["Month"].ToString();
                            monthNode.Target = "fraTopic";
                            yearNode.ChildNodes.Add(monthNode);
                        }
                    }
                    #endregion
                }
            }
            obj = null;
            #endregion

            treeFolder.CollapseAll();
        }
        /// <summary>
        /// LoadDanhSachUserDaUQ
        /// </summary>
        /// <param name="rootText"></param>
        protected void LoadDanhSachUserDaUQ(string rootText)
        {
            //make root node with text
            TreeNode rootNode = new TreeNode();
            rootNode.Text = rootText;
            treeFolder.Nodes.Add(rootNode);

            string condition = " and pk_userid in";
            condition += " (select fk_appliedonid from t_permission";
            condition += " where typeofapplicant=" + TYPE_OF_APPLICANT.USER; 
            condition += " and typeofobject=" + TYPE_OF_OBJECT.COMPONENT;
            condition += ")";

            string query = "[fk_userid],[fk_groupid],[Name1]+'.....'+[Description1] as [companyname], [name]+'.....'+[fullname] as [username]";
            bus_User_In_Group obj = bus_User_In_Group.Instance(_objUserContext); //bus_User_In_Group obj = new bus_User_In_Group(_objUserContext, _dbName);
            DataSet ds_user_group_permited = obj.getListT(condition, query);
            obj = null;
            if (!base.isValidDataSet(ds_user_group_permited)) return;

            //add group nodes
            string group_added = string.Empty;
            foreach (DataRow R in ds_user_group_permited.Tables[0].Rows)
            {
                TreeNode groupNode = new TreeNode();
                string NodeID = R["fk_groupid"].ToString();

                if (StringHelper.isSubstring(group_added, NodeID)) continue;

                string scolor = _leafColor;

                string NodeText = R["companyname"].ToString();
                groupNode.Text = "<img src='../../images/001_20.gif' border='0' width='16px' hieght='16px'></img><font color='" + scolor + "'>" + NodeText + "</font>";
                groupNode.ToolTip = NodeText;
                rootNode.ChildNodes.Add(groupNode);
                AddUserPermiter(groupNode, NodeID, ds_user_group_permited);
                group_added += NodeID + ",";
            }
            treeFolder.CollapseAll();
        }
        /// <summary>
        /// AddUserPermiter
        /// </summary>
        /// <param name="Node"></param>
        /// <param name="ds_user_group_permited"></param>
        protected void AddUserPermiter(TreeNode Node, string GroupID, DataSet ds)
        {
            DataRow[] R = ds.Tables[0].Select("fk_groupid='" + GroupID + "'", "username");
            if (R.Length == 0) return;
            foreach (DataRow r in R)
            {
                TreeNode userNode = new TreeNode();
                string userID = r["fk_userid"].ToString();
                string userName = r["username"].ToString();
                string scolor = _leafColor;
                userNode.Text = "<img src='../../images/UserInfo.gif' border='0' width='16px' hieght='16px'></img>" + " <font color='" + scolor + "'>" + userName + "</font>";
                userNode.ToolTip = userName;
                userNode.NavigateUrl = "~/Modules/cfg/danhsachuyquyen.aspx?userid=" + userID + "&username=" + userName;
                userNode.Target = "fraTopic";
                Node.ChildNodes.Add(userNode);
            }
        }
        /// <summary>
        /// LoadtreeUser
        /// </summary>
        /// <param name="rootText"></param>
        /// <author>dungnt</author>
        /// <createddate>2008</createddate>
        protected void LoadtreeUser(string rootText)
        {
            //make root node with text
            TreeNode rootNode = new TreeNode();
            rootNode.Text = rootText;
            treeFolder.Nodes.Add(rootNode);

            //add group nodes
            string condition = "";
            if (_docspace == "appuser")
            {
                if (_m_roleID == vpb.app.business.ktnb.Definition.UMS.ROLES.TRUONGPHONG_CSCC)
                    condition += " and fk_parentgroupid='" + vpb.app.business.ktnb.Definition.UMS.GROUPS.GROUP_OF_KTNB + "'";
                else
                {
                    condition += " and pk_groupid <> '" + GROUPS.SYS_ADMIN + "'";
                    condition += " and pk_groupid <> '" + GROUPS.APP_ADMIN + "'";
                }
            }
            else
            {
                
                    condition += " and pk_groupid IN ('" + GROUPS.SYS_ADMIN + "','" + GROUPS.APP_ADMIN + "')";
            }
            condition += " Order by Name";
            string query = "PK_GroupID";
            query += ",Name";
            query += ",Description";
            bus_Group objGrp = bus_Group.Instance(_objUserContext);
            DataSet ds = objGrp.getList(condition, query);
            if (!isValidDataSet(ds)) return;

            //string groupsadded = "";
            foreach (DataRow R in ds.Tables[0].Rows)
            {
                TreeNode groupNode = new TreeNode();
                string NodeID = R["PK_GroupID"].ToString();
                string scolor = _leafColor;
                if (NodeID.ToUpper().Equals(GROUPS.APP_ADMIN))
                    scolor = _leafColor;
                else if (NodeID.ToUpper().Equals(GROUPS.SYS_ADMIN))
                {
                    scolor = _leafColor;
                    if (!_m_roleID.ToUpper().Equals(ROLES.SYS_ADMIN)) continue;
                }
                string NodeText = R["Name"].ToString() + "....." + R["Description"].ToString();
                groupNode.Text = "<img src='../../images/001_20.gif' border='0' width='16px' hieght='16px'></img><font color='" + scolor + "'>" + NodeText + "</font>";
                groupNode.ToolTip = R["Name"].ToString();
                groupNode.NavigateUrl = "~/Modules/UMS/UserInGroup.aspx?groupid=" + NodeID + "&name=" + NodeText + "&docspace=" + _docspace;
                groupNode.Target = "fraTopic";
                rootNode.ChildNodes.Add(groupNode);
            }
            treeFolder.CollapseAll();
        }
        /// <summary>
        /// AddUserInGroup
        /// </summary>
        /// <param name="groupNode"></param>
        /// <param name="GroupID"></param>
        /// <param name="ds"></param>
        protected void AddUserInGroup(TreeNode groupNode, string GroupID, DataSet ds)
        {
            DataRow[] R = ds.Tables[0].Select("PK_GroupID='" + GroupID + "'", "UserName");
            if (R.Length == 0) return;
            foreach (DataRow r in R)
            {
                TreeNode userNode = new TreeNode();
                string userID = r["PK_UserID"].ToString();
                string userName = r["UserName"].ToString();
                byte exp = 0;
                if (r["IsExpired"] != DBNull.Value)
                    exp = Convert.ToByte(r["IsExpired"]);
                string scolor = _leafColor;
                if (exp == 1) scolor = "silver";
                userNode.Text = "<img src='../../images/profile_small.gif' border='0'></img>" + " <font color='" + scolor + "'>" + userName + "</font>";
                userNode.ToolTip = userName;

                string url = "~/Modules/UMS/userinfo.aspx?id=" + userID + "&name=" + userName + "&docspace=" + _docspace;
                userNode.NavigateUrl = url;

                userNode.Target = "fraTopic";
                groupNode.ChildNodes.Add(userNode);
            }
        }
        /// <summary>
        /// load branches by user
        /// </summary>
        /// <param name="rootText"></param>
        protected void LoadBranchesByComponent(string rootText)
        {
            string cacheid = _m_roleID + "_" + _m_groupid + "_" + _applicationid + "_" + _componentname;
            string condition = " AND Type In (" + GROUPTYPE.CHI_NHANH + "," + GROUPTYPE.TOAN_HANG + "," + GROUPTYPE.TT_HOTRO + ")";
            

            string query = string.Empty;
            string groupid = string.Empty;
            string parentgroupid = string.Empty;

            groupid = ((Group)_objUserContext.Groups[0]).GroupID;

            string role_special = ROLES.SYS_ADMIN;
            role_special += "," + ROLES.APP_ADMIN;

            string group_special = GROUPS.HOI_SO;

            if ((!StringHelper.isSubstring(role_special, _m_roleID.ToUpper())) && (!StringHelper.isSubstring(group_special, groupid.ToUpper())))
            {
                int grouptype = CommonFunc.getGroupType(groupid, _objUserContext);
                if (grouptype != GROUPTYPE.TT_HOTRO)
                {
                    condition = " AND PK_GroupID='" + groupid + "'";
                    cacheid += "_01";
                }
                else
                {
                    condition = " AND (PK_GroupID='" + groupid + "' OR PK_GroupID In (Select FK_DocLinkID From T_DocLink Where FK_DocumentID='" + groupid + "'))";
                    cacheid += "_02";
                }
            }
            condition += " Order By Name";

            bus_Group objGroup = bus_Group.Instance(_objUserContext);  //bus_Group objGroup = new bus_Group(_objUserContext, _dbName);
            DataSet ds;
            ds = objGroup.getList(condition, query);

            //if (_objUserContext.getObjectCached(cacheid) == null)
            //{
            //    ds = objGroup.getList(condition, query);
            //    _objUserContext.addDataSet2Cache(cacheid, ds, VPB_CRM.Helper.AppCached.CACHED_TIME_OUT);
            //}
            //else
            //{
            //    ds = _objUserContext.getDataSetCached(cacheid);
            //}
            BuilCompanyByComponent(treeFolder, rootText, ds, "PK_GroupID", "Name", "Name", "../../Controls/Tab/tab.aspx?componentname=" + rootText + "&module=FileManager&", "fraTopic", "001_20.gif");
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

            if (_m_roleID == vpb.app.business.ktnb.Definition.UMS.ROLES.TRUONGPHONG_CSCC)
                condition += " AND FK_PARENTGROUPID='" + vpb.app.business.ktnb.Definition.UMS.GROUPS.GROUP_OF_KTNB + "'";

            condition += "Order By Name";

            AppCached.group_list = "ds_allgroup";

            string query = "PK_GroupID,Name,Mnemonic,Description,FK_ParentGroupID,Type";

            bus_Group objgrp = bus_Group.Instance(_objUserContext);
            DataSet ds_allgroup;
            ds_allgroup = objgrp.getList(condition, query);
            DataRow[] dr_parent_group = null;

            if (_m_roleID == vpb.app.business.ktnb.Definition.UMS.ROLES.TRUONGPHONG_CSCC)
                dr_parent_group = ds_allgroup.Tables[0].Select("Type=" + GROUPTYPE.PHONG_GD, "Name"); 
            else
                dr_parent_group = ds_allgroup.Tables[0].Select("Type=" + GROUPTYPE.CHI_NHANH + " Or Type=" + GROUPTYPE.TOAN_HANG + " Or Type=" + GROUPTYPE.TT_HOTRO, "Name"); 

            DataRow[] dr_child_group;

            string fk_parentgroupid = "";
            string pk_groupid = "";
            string nodeText = "";
            foreach (DataRow r_parent_group in dr_parent_group)
            {
                fk_parentgroupid = r_parent_group["pk_groupid"].ToString();
                TreeNode node_parentgroup = new TreeNode();
                nodeText = r_parent_group["Name"].ToString() + "....." + r_parent_group["Description"].ToString();
                node_parentgroup.Text = "<img src='../../Images/001_20.gif' border='0' width='16px' hieght='16px'></img><font color='" + _leafColor + "'> " + nodeText + "</font>";
                node_parentgroup.NavigateUrl = "../UMS/groupinfo.aspx?id=" + fk_parentgroupid + "&name=" + nodeText + "&docspace=" + _docspace;
                node_parentgroup.Target = "fraTopic";
                rootNode.ChildNodes.Add(node_parentgroup);

                dr_child_group = ds_allgroup.Tables[0].Select("FK_ParentGroupID='" + fk_parentgroupid + "'", "Name");
                if (dr_child_group.Length == 0) continue;
                foreach (DataRow r_child_group in dr_child_group)
                {
                    pk_groupid = r_child_group["pk_groupid"].ToString();
                    TreeNode node_childgroup = new TreeNode();
                    nodeText = r_child_group["Name"].ToString() + "....." + r_child_group["Description"].ToString();
                    node_childgroup.Text = "<img src='../../Images/001_20.gif' border='0' width='16px' hieght='16px'></img><font color='" + _leafColor + "'> " + nodeText + "</font>";
                    node_childgroup.NavigateUrl = "../UMS/groupinfo.aspx?id=" + pk_groupid + "&name=" + nodeText + "&docspace=" + _docspace;
                    node_childgroup.Target = "fraTopic";
                    node_parentgroup.ChildNodes.Add(node_childgroup);
                }
            }
            treeFolder.CollapseAll();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rootText"></param>
        protected void Danh_Sach_KH_TrungGiai(string rootText)
        {
            TreeNode rootNode = new TreeNode();
            rootNode.Text = rootText;
            treeFolder.Nodes.Add(rootNode);

            string condition = "AND NAME NOT IN('VN0010000','VN0010001','VN0010002','VN0010003','VN0010004','VN0010005','VN0010242','App Admin','Sys Admin')";

            if (_m_grouptype == GROUPTYPE.CHI_NHANH)
            {
                condition += " AND FK_ParentGroupID='" + _m_groupid + "'"; 
            }
            condition += "Order By Name";

            AppCached.group_list = "ds_allgroup";

            string query = "PK_GroupID,Name,Mnemonic,Description,FK_ParentGroupID,Type";

            bus_Group objgrp = bus_Group.Instance(_objUserContext);
            DataSet ds_allgroup;
            ds_allgroup = objgrp.getList(condition, query);

            DataRow[] dr_parent_group = ds_allgroup.Tables[0].Select("Type=" + GROUPTYPE.CHI_NHANH + " Or Type=" + GROUPTYPE.TOAN_HANG, "Name");
            DataRow[] dr_child_group;

            string fk_parentgroupid = "";
            string pk_groupid = "";
            string nodeText = "";
            foreach (DataRow r_parent_group in dr_parent_group)
            {
                fk_parentgroupid = r_parent_group["pk_groupid"].ToString();
                TreeNode node_parentgroup = new TreeNode();
                nodeText = r_parent_group["Name"].ToString() + "....." + r_parent_group["Description"].ToString();
                node_parentgroup.Text = "<img src='../../Images/001_20.gif' border='0' width='16px' hieght='16px'></img><font color='" + _leafColor + "'> " + nodeText + "</font>";

                node_parentgroup.NavigateUrl = "";

                node_parentgroup.Target = "fraTopic";
                rootNode.ChildNodes.Add(node_parentgroup);

                dr_child_group = ds_allgroup.Tables[0].Select("FK_ParentGroupID='" + fk_parentgroupid + "'", "Name");
                if (dr_child_group.Length == 0) continue;
                foreach (DataRow r_child_group in dr_child_group)
                {
                    pk_groupid = r_parent_group["pk_groupid"].ToString();
                    TreeNode node_childgroup = new TreeNode();
                    nodeText = r_child_group["Name"].ToString() + "....." + r_child_group["Description"].ToString();
                    node_childgroup.Text = "<img src='../../Images/001_20.gif' border='0' width='16px' hieght='16px'></img><font color='" + _leafColor + "'> " + nodeText + "</font>";

                    node_childgroup.NavigateUrl = "";

                    node_childgroup.Target = "fraTopic";
                    node_parentgroup.ChildNodes.Add(node_childgroup);
                }
            }
            treeFolder.CollapseAll();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rootText"></param>
        protected void BoSung_SDT_ChiNhanh(string rootText, string url1, string url2)
        {
            TreeNode rootNode = new TreeNode();
            rootNode.Text = rootText;
            treeFolder.Nodes.Add(rootNode);

            string condition = "AND NAME NOT IN('VN0010000','VN0010001','VN0010002','VN0010003','VN0010004','VN0010005','VN0010242','App Admin','Sys Admin')";
            condition += "Order By Name";

            AppCached.group_list = "ds_allgroup";

            string query = "PK_GroupID,Name,Mnemonic,Description,FK_ParentGroupID,Type";

            bus_Group objgrp = bus_Group.Instance(_objUserContext);
            DataSet ds_allgroup;
            ds_allgroup = objgrp.getList(condition, query);

            DataRow[] dr_parent_group = ds_allgroup.Tables[0].Select("Type=" + GROUPTYPE.CHI_NHANH + " Or Type=" + GROUPTYPE.TOAN_HANG + " Or Type=" + GROUPTYPE.TT_HOTRO, "Name");
            DataRow[] dr_child_group;

            string fk_parentgroupid = "";
            string pk_groupid = "";
            string nodeText = "";
            foreach (DataRow r_parent_group in dr_parent_group)
            {
                fk_parentgroupid = r_parent_group["pk_groupid"].ToString();
                TreeNode node_parentgroup = new TreeNode();
                nodeText = r_parent_group["Name"].ToString() + "....." + r_parent_group["Description"].ToString();
                node_parentgroup.Text = "<img src='../../Images/001_20.gif' border='0' width='16px' hieght='16px'></img><font color='" + _leafColor + "'> " + nodeText + "</font>";

                node_parentgroup.NavigateUrl = url1 + "?cocode=" + r_parent_group["Name"].ToString() + "&name=" + r_parent_group["Description"].ToString() + "&docspace=" + _docspace;

                node_parentgroup.Target = "fraTopic";
                rootNode.ChildNodes.Add(node_parentgroup);

                dr_child_group = ds_allgroup.Tables[0].Select("FK_ParentGroupID='" + fk_parentgroupid + "'", "Name");
                if (dr_child_group.Length == 0) continue;
                foreach (DataRow r_child_group in dr_child_group)
                {
                    pk_groupid = r_parent_group["pk_groupid"].ToString();
                    TreeNode node_childgroup = new TreeNode();
                    nodeText = r_child_group["Name"].ToString() + "....." + r_child_group["Description"].ToString();
                    node_childgroup.Text = "<img src='../../Images/001_20.gif' border='0' width='16px' hieght='16px'></img><font color='" + _leafColor + "'> " + nodeText + "</font>";

                    node_childgroup.NavigateUrl = url2 + "?cocode=" + r_child_group["Name"].ToString() + "&name=" + r_child_group["Description"].ToString() + "&docspace=" + _docspace;

                    node_childgroup.Target = "fraTopic";
                    node_parentgroup.ChildNodes.Add(node_childgroup);
                }
            }
            treeFolder.CollapseAll();
        }
        /// <summary>
        /// LoadTTHoTro
        /// </summary>
        /// <param name="rootText"></param>
        protected void LoadTTHoTro(string rootText)
        {
            //make root node with text
            TreeNode rootNode = new TreeNode();
            rootNode.Text = rootText;
            treeFolder.Nodes.Add(rootNode);

            #region add trung tam ho tro & cac trung tam truc thuoc
            string condition_ttht = " AND ([TYPE]=1 OR FK_ParentGroupID IN (SELECT PK_GroupID FROM T_Group WHERE [TYPE]=1)) ";
            condition_ttht += " Order by Name";
            string query_ttht = "PK_GroupID,Name,Description,Mnemonic,FK_ParentGroupID,Type";
            bus_Group objgrp = bus_Group.Instance(_objUserContext);  //bus_Group objgrp = new bus_Group(_objUserContext, _dbName);
            DataSet ds = objgrp.getList(condition_ttht, query_ttht);
            if (!base.isValidDataSet(ds)) return;

            DataRow[] rows_ttht = ds.Tables[0].Select("Type=1");
            if (rows_ttht.Length == 0) return;
            string url = "../UMS/groupinfo.aspx?";
            foreach (DataRow row_ttht in rows_ttht)
            {
                TreeNode Node_TTHT = new TreeNode();
                string NodeID_TTHT = row_ttht["PK_GroupID"].ToString();
                Node_TTHT.Text = "<img src='../../Images/001_20.gif' border='0' width='16px' hieght='16px'><font color='" + _leafColor + "'>" + row_ttht["Name"].ToString() + "....." + row_ttht["Description"].ToString() + "</font>";
                Node_TTHT.NavigateUrl = url + "id=" + NodeID_TTHT + "&name=" + row_ttht["Name"].ToString() + "....." + row_ttht["Description"].ToString() + "&docspace=" + _docspace;
                Node_TTHT.Target = "fraTopic";

                //DataRow[] rows_company = ds.Tables[0].Select("FK_ParentGroupID='" + NodeID_TTHT + "'");
                //foreach (DataRow row_company in rows_company)
                //{
                //    TreeNode Node_Compnay = new TreeNode();
                //    string NodeID_Compnay = row_company["PK_GroupID"].ToString();
                //    Node_Compnay.Text = "<font color='" + _leafColor + "'>" + row_company["Name"].ToString() + "....." + row_company["Description"].ToString() + "</font>";
                //    Node_TTHT.ChildNodes.Add(Node_Compnay);
                //}

                treeFolder.Nodes[0].ChildNodes.Add(Node_TTHT);
            }
            treeFolder.CollapseAll();
            #endregion
        }
        /// <summary>
        /// BuilCompanyByComponent
        /// </summary>
        /// <param name="tvw"></param>
        /// <param name="rootText"></param>
        /// <param name="ds"></param>
        /// <param name="NodeID_FieldName"></param>
        /// <param name="NodeText_FieldName"></param>
        /// <param name="NodeTooltip_FieldName"></param>
        /// <param name="Url"></param>
        /// <param name="Target"></param>
        /// <param name="icon"></param>
        protected void BuilCompanyByComponent(TreeView tvw, string rootText, DataSet ds, string NodeID_FieldName, string NodeText_FieldName, string NodeTooltip_FieldName, string Url, string Target, string icon)
        {
            //make root node with text
            TreeNode rootNode = new TreeNode();
            rootNode.Text = rootText;
            tvw.Nodes.Add(rootNode);

            if (!isValidDataSet(ds)) return;

            //add branch nodes
            foreach (DataRow R in ds.Tables[0].Rows)
            {
                string scolor = _leafColor;
                TreeNode childNode = new TreeNode();
                string NodeID = R[NodeID_FieldName].ToString();

                byte grouptype = Convert.ToByte(R["Type"]);

                string NodeText = R[NodeText_FieldName].ToString() + "....." + R["Description"].ToString();
                childNode.Text = "<img src='../../Images/" + icon + "' border='0' width='16px' hieght='16px'></img><font color='" + scolor + "'> " + NodeText + "</font>";
                childNode.ToolTip = R[NodeTooltip_FieldName].ToString();

                if (!string.IsNullOrEmpty(Url))
                    childNode.NavigateUrl = Url + "id=" + NodeID + "&name=" + NodeText;
                if (!string.IsNullOrEmpty(Target))
                    childNode.Target = Target;

                rootNode.ChildNodes.Add(childNode);

                if (grouptype == GROUPTYPE.TT_HOTRO)
                    addCompaniesWasSupported(childNode, NodeID, Url, Target);               //add all CN under TTHTRO

            }
            tvw.CollapseAll();
        }
        /// <summary>
        /// addSubChildNode
        /// </summary>
        /// <param name="Node"></param>
        /// <param name="NodeID"></param>
        /// <param name="Url"></param>
        /// <param name="Target"></param>
        protected void addSubChildNode(TreeNode Node, string NodeID, string Url, string Target)
        {
            string condition = " AND FK_ParentGroupID='" + NodeID + "' ORDER BY Name";
            string cacheid = _m_roleID + "_" + _m_groupid + "_" + _applicationid + "_" + NodeID + _componentname;
            string query = string.Empty;

            bus_Group objGroup = bus_Group.Instance(_objUserContext);  //bus_Group objGroup = new bus_Group(_objUserContext, _dbName);
            DataSet ds;
            ds = objGroup.getList(condition, query);

            //if (_objUserContext.getObjectCached(cacheid) == null)
            //{
            //    ds = objGroup.getList(condition, query);
            //    _objUserContext.addDataSet2Cache(cacheid, ds, VPB_CRM.Helper.AppCached.CACHED_TIME_OUT);
            //}
            //else
            //{
            //    ds = _objUserContext.getDataSetCached(cacheid);
            //}

            if (!isValidDataSet(ds)) return;

            foreach (DataRow R in ds.Tables[0].Rows)
            {
                TreeNode childNode = new TreeNode();
                string sNodeID = R["PK_GroupID"].ToString();
                string sNodeText = R["Name"].ToString() + "....." + R["Description"].ToString();
                childNode.Text = "<font color='green'>" + sNodeText + "</font>";
                childNode.ToolTip = R["Description"].ToString();
                if (!string.IsNullOrEmpty(Url))
                    childNode.NavigateUrl = Url + "id=" + sNodeID + "&name=" + sNodeText + "&parentnodeid=" + NodeID;
                if (!string.IsNullOrEmpty(Target))
                    childNode.Target = Target;
                Node.ChildNodes.Add(childNode);
            }
        }
        /// <summary>
        /// addCompaniesWasSupported
        /// load danh sach cac chi nhanh duoc ho tro boi Trung tam ho tro (load ca PGD truc thuoc CN)
        /// </summary>
        /// <param name="Node"></param>
        /// <param name="NodeID"></param>
        /// <param name="Url"></param>
        /// <param name="Target"></param>
        protected void addCompaniesWasSupported(TreeNode Node, string NodeID, string Url, string Target)
        {
            string cacheid = _m_roleID + "_" + _m_groupid + "_" + _applicationid + "_" + _componentname;
            string query = string.Empty;
            string condition = " AND PK_GroupID In (Select FK_DocLinkID From T_DocLink Where FK_DocumentID='" + NodeID + "')";
            condition += " Order By Name";
            bus_Group objGroup = bus_Group.Instance(_objUserContext);  //bus_Group objGroup = new bus_Group(_objUserContext, _dbName);
            DataSet ds;
            ds = objGroup.getList(condition, query);
            //if (_objUserContext.getObjectCached(cacheid) == null)
            //{
            //    ds = objGroup.getList(condition, query);
            //    _objUserContext.addDataSet2Cache(cacheid, ds, VPB_CRM.Helper.AppCached.CACHED_TIME_OUT);
            //}
            //else
            //{
            //    ds = _objUserContext.getDataSetCached(cacheid);
            //}
            if (!isValidDataSet(ds)) return;
            foreach (DataRow R in ds.Tables[0].Rows)
            {
                TreeNode childNode = new TreeNode();
                string sNodeID = R["PK_GroupID"].ToString();
                string sNodeText = R["Name"].ToString() + "....." + R["Description"].ToString();
                childNode.Text = "<font color='green'>" + sNodeText + "</font>";
                childNode.ToolTip = R["Description"].ToString();
                if (!string.IsNullOrEmpty(Url))
                    childNode.NavigateUrl = Url + "id=" + sNodeID + "&name=" + sNodeText + "&parentnodeid=" + NodeID;
                if (!string.IsNullOrEmpty(Target))
                    childNode.Target = Target;
                Node.ChildNodes.Add(childNode);
            }
        }
        /// <summary>
        /// LoadTreeRole
        /// </summary>
        /// <param name="rootText"></param>
        /// <author>dungnt</author>
        /// <createddate>2008</createddate>
        protected void LoadTreeRole(string rootText)
        {
            bus_Role objRole = bus_Role.Instance(_objUserContext);  //bus_Role objRole = new bus_Role(_objUserContext, _dbName);
            DataSet ds = objRole.getList(" ORDER BY NAME", "PK_ROLEID,NAME");
            base.BindData2Tree(treeFolder, rootText, ds, "PK_ROLEID", "NAME", "NAME", "../UMS/roleinfo.aspx", "fraTopic");
        }
        /// <summary>
        /// LoadParameter
        /// </summary>
        /// <param name="rootText"></param>
        protected void LoadParameter(string rootText)
        {
            bus_Project_Parameter objParam = bus_Project_Parameter.Instance(_objUserContext);  //bus_Project_Parameter objParam = new bus_Project_Parameter(_objUserContext, _dbName);
            DataSet ds = objParam.getList(string.Empty, string.Empty);
            base.BindData2Tree(treeFolder, rootText, ds, "NAME", "NAME", "FULLNAME", "../CFG/Parameters.aspx", "fraTopic", "check.gif");
        }
        /// <summary>
        /// LoadParameterNGroup
        /// </summary>
        /// <param name="rootText"></param>
        protected void LoadSysParameterNGroup(string rootText)
        {
            bus_Project_Parameter objParam = bus_Project_Parameter.Instance(_objUserContext);
            DataSet dsgroup = objParam.getList(" And Parameter_Type='SYSTEM' And Len(GroupName)>0 order by GroupName", "distinct GroupName");

            //make root node with text
            TreeNode rootNode = new TreeNode();
            rootNode.Text = rootText;
            treeFolder.Nodes.Add(rootNode);

            //group name
            foreach (DataRow R in dsgroup.Tables[0].Rows)
            {
                TreeNode groupNode = new TreeNode();
                string NodeID = R["GroupName"].ToString();
                string NodeText = R["GroupName"].ToString();
                groupNode.Text = "<img src='../../Images/monitor.gif' border='0'></img><font color='" + _leafColor + "'> " + NodeText + "</font>";
                rootNode.ChildNodes.Add(groupNode);
                if (NodeID == "FILE SERVER")
                {
                    //add file servers param
                    string query = " Name,Replace(name,'_HOST','') As Server";
                    string condition = " And GroupName='FILE SERVER' And Name like '%_HOST'";
                    condition += " Order By Server";
                    DataSet ds_svr = objParam.getList(condition, query);
                    if (base.isValidDataSet(ds_svr))
                    {
                        foreach (DataRow svr in ds_svr.Tables[0].Rows)
                        {
                            TreeNode svrNode = new TreeNode();
                            string svrNodeID = svr["Name"].ToString();
                            string svrNodeText = svr["Server"].ToString();
                            svrNode.Text = "<img src='../../Images/monitor.gif' border='0'></img><font color='" + _leafColor + "'> " + svrNodeText + "</font>";
                            groupNode.ChildNodes.Add(svrNode);

                            query = " Name,Value,FullName";
                            condition = " And GroupName='FILE SERVER'";
                            condition += " And Name Like '" + svrNodeText + "%'";
                            condition += " Order By Name";
                            DataSet ds_svr_info = objParam.getList(condition, query);

                            if (base.isValidDataSet(ds_svr_info))
                            {
                                foreach (DataRow svr_info in ds_svr_info.Tables[0].Rows)
                                {
                                    TreeNode paramNode = new TreeNode();
                                    string paramid = svr_info["Name"].ToString();

                                    string paramtext = svr_info["Name"].ToString();
                                    if ((!paramtext.Contains("_PASS")) && (!paramtext.Contains("_USER")))
                                        paramtext = paramtext + "(<font color='blue'>" + svr_info["Value"].ToString() + "</font>)";

                                    paramNode.Text = "<img src='../../Images/check.gif' border='0'></img><font color='" + _leafColor + "'> " + paramtext + "</font>";
                                    paramNode.Target = "fraTopic";
                                    string url = "../../Modules/cfg/Parameters.aspx?id=" + paramid + "&name=" + paramid + "&groupname=" + NodeID;
                                    url += "&docspace=" + _docspace;
                                    paramNode.NavigateUrl = url;
                                    svrNode.ChildNodes.Add(paramNode);
                                }
                            }
                        }
                    }
                }
                else
                    AddParameter(groupNode, NodeID);
            }

            treeFolder.CollapseAll();
        }
        /// <summary>
        /// LoadAppParameterNGroup
        /// </summary>
        /// <param name="rootText"></param>
        protected void LoadAppParameterNGroup(string rootText)
        {
            bus_Project_Parameter objParam = bus_Project_Parameter.Instance(_objUserContext);  //bus_Project_Parameter objParam = new bus_Project_Parameter(_objUserContext, _dbName);
            DataSet dsgroup = objParam.getList(" And Parameter_Type<>'SYSTEM' And Len(GroupName)>0 order by GroupName", "distinct GroupName");

            //make root node with text
            TreeNode rootNode = new TreeNode();
            rootNode.Text = rootText;
            treeFolder.Nodes.Add(rootNode);

            //group name
            foreach (DataRow R in dsgroup.Tables[0].Rows)
            {
                TreeNode groupNode = new TreeNode();
                string NodeID = R["GroupName"].ToString();
                string NodeText = R["GroupName"].ToString();
                groupNode.Text = "<img src='../../Images/monitor.gif' border='0'></img><font color='" + _leafColor + "'> " + NodeText + "</font>";
                rootNode.ChildNodes.Add(groupNode);
                AddParameter(groupNode, NodeID);
            }

            treeFolder.CollapseAll();
        }
        /// <summary>
        /// AddParameter
        /// </summary>
        /// <param name="groupNode"></param>
        /// <param name="groupName"></param>
        protected void AddParameter(TreeNode groupNode, string groupName)
        {
            bus_Project_Parameter objParam = bus_Project_Parameter.Instance(_objUserContext);  //bus_Project_Parameter objParam = new bus_Project_Parameter(_objUserContext, _dbName);
            DataSet dsparam = objParam.getList("And GroupName='" + groupName + "' order by Name", "Name,Value,FullName");

            //parameter name
            foreach (DataRow R in dsparam.Tables[0].Rows)
            {
                TreeNode paramNode = new TreeNode();
                string NodeID = R["Name"].ToString();
                string NodeText = R["Name"].ToString();
                paramNode.Text = "<img src='../../Images/check.gif' border='0'></img><font color='" + _leafColor + "'> " + NodeText + "</font>";
                paramNode.Target = "fraTopic";
                string url = "../../Modules/cfg/Parameters.aspx?id=" + NodeID + "&name=" + NodeID + "&groupname=" + groupName;
                url += "&docspace=" + _docspace;
                paramNode.NavigateUrl = url;

                groupNode.ChildNodes.Add(paramNode);
            }
        }
        /// <summary>
        /// GetComponentName
        /// </summary>
        /// <param name="componentid"></param>
        /// <returns></returns>
        protected string GetComponentName(string componentid)
        {
            bus_Component objcomp = bus_Component.Instance(_objUserContext);  //bus_Component objcomp = new bus_Component(_objUserContext, _dbName);
            DataSet ds = objcomp.getByID(componentid, "name");
            objcomp = null;
            if (!base.isValidDataSet(ds)) return componentid;
            return ds.Tables[0].Rows[0]["name"].ToString();
        }
        /// <summary>
        /// GetParentGroupID
        /// </summary>
        /// <param name="groupid"></param>
        /// <returns></returns>
        protected string GetParentGroupID(string groupid)
        {
            bus_Group objgrp = bus_Group.Instance(_objUserContext);  //bus_Group objgrp = new bus_Group(_objUserContext, _dbName);
            DataSet ds = objgrp.getByID(groupid, "FK_ParentGroupID");
            objgrp = null;
            if (!base.isValidDataSet(ds)) return string.Empty;
            return ds.Tables[0].Rows[0]["FK_ParentGroupID"].ToString();
        }
        /// <summary>
        /// IsParentGroup
        /// </summary>
        /// <param name="groupid"></param>
        /// <returns></returns>
        protected bool IsParentGroup(string groupid)
        {
            bus_Group objgrp = bus_Group.Instance(_objUserContext);  //bus_Group objgrp = new bus_Group(_objUserContext, _dbName);
            DataSet ds = objgrp.getList(" AND FK_ParentGroupID='" + groupid + "'", "FK_ParentGroupID");
            objgrp = null;
            if (!base.isValidDataSet(ds)) return false;
            return true;
        }
        /// <summary>
        /// AddComponent
        /// </summary>
        /// <param name="appNode"></param>
        /// <param name="NodeID"></param>
        /// <param name="ds"></param>
        protected void AddComponent(TreeNode appNode, string NodeID)
        {
            string condition = " AND FK_ApplicationID='" + NodeID + "'";
            condition += " Order By Name";
            bus_Component objcomp = bus_Component.Instance(_objUserContext);  //bus_Component objcomp = new bus_Component(_objUserContext, _dbName);
            DataSet ds = objcomp.getList(condition, "PK_ComponentID,Name");
            if (!isValidDataSet(ds)) return;
            foreach (DataRow R in ds.Tables[0].Rows)
            {
                TreeNode childNode = new TreeNode();
                string sNodeID = R["PK_ComponentID"].ToString();
                string sNodeText = R["Name"].ToString();
                childNode.Text = "<img src='../../images/minifolder.gif' border='0' width='16px' hieght='16px'></img><font color='green'>" + sNodeText + "</font>";
                childNode.ToolTip = sNodeText;
                if (_docspace.Equals("appcustom"))
                    childNode.NavigateUrl = "../cfg/appcustom.aspx?id=" + sNodeID + "&name=" + sNodeText + "&app=" + NodeID;
                else if (_docspace.Equals("sharepermission"))
                    childNode.NavigateUrl = "../cfg/sharepermission.aspx?id=" + sNodeID + "&name=" + sNodeText + "&app=" + NodeID;
                else
                    childNode.NavigateUrl = "../cfg/apppermission.aspx?id=" + sNodeID + "&name=" + sNodeText + "&app=" + NodeID;
                childNode.Target = "fraTopic";
                appNode.ChildNodes.Add(childNode);
            }
        }
        /// <summary>
        /// LoadTreeByDocSpace
        /// </summary>
        /// <param name="rootText"></param>
        /// <param name="DocspaceID"></param>
        protected void LoadTreeByDocSpace(string rootText, string DocspaceID)
        {
            bus_DocSpace objDocSpace = bus_DocSpace.Instance(_objUserContext);
            DataSet ds = objDocSpace.getDocTypeList(DocspaceID);
            base.BindData2Tree(treeFolder, rootText, ds, "FK_DOCTYPEID", "DOCTYPENAME", "DOCTYPENAME", "~/Modules/Lookup/LookupList.aspx", "fraTopic");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rootText"></param>
        protected void LoadDanhBaKhachHang(string rootText)
        {
            //make root node with text
            TreeNode rootNode = new TreeNode();
            rootNode.Text = rootText;
            treeFolder.Nodes.Add(rootNode);

            //add group nodes
            string condition = "";
            condition += " and pk_groupid <> '" + GROUPS.SYS_ADMIN + "'";
            condition += " and pk_groupid <> '" + GROUPS.APP_ADMIN + "'";
            if (_m_roleID != ROLES.SYS_ADMIN)
            {
                condition += " AND PK_GROUPID='" + _m_groupid + "' OR FK_PARENTGROUPID='" + _m_groupid + "'";
            }
            condition += " Order by Name";
            string query = "PK_GroupID";
            query += ",Name";
            query += ",Description";
            bus_Group objGrp = bus_Group.Instance(_objUserContext);
            DataSet ds = objGrp.getList(condition, query);
            if (!isValidDataSet(ds)) return;

            //string groupsadded = "";
            foreach (DataRow R in ds.Tables[0].Rows)
            {
                TreeNode groupNode = new TreeNode();
                string NodeID = R["PK_GroupID"].ToString();
                string scolor = _leafColor;
                if (NodeID.ToUpper().Equals(GROUPS.APP_ADMIN))
                    scolor = _leafColor;
                else if (NodeID.ToUpper().Equals(GROUPS.SYS_ADMIN))
                {
                    scolor = _leafColor;
                    if (!_m_roleID.ToUpper().Equals(ROLES.SYS_ADMIN)) continue;
                }
                string NodeText = R["Name"].ToString() + "....." + R["Description"].ToString();
                groupNode.Text = "<img src='../../images/001_20.gif' border='0' width='16px' hieght='16px'></img><font color='" + scolor + "'>" + NodeText + "</font>";
                groupNode.ToolTip = R["Name"].ToString();
                groupNode.NavigateUrl = "~/Modules/VPB_PROMOTION/CustomerList.aspx?c=" + R["Name"].ToString() + "&d=" + R["Description"].ToString();
                groupNode.Target = "fraTopic";
                rootNode.ChildNodes.Add(groupNode);
            }
            treeFolder.CollapseAll();
        }
        #endregion
    }
}