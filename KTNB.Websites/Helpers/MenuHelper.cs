using System;
using System.Configuration;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using CORE.CoreObjectContext;
using vpb.app.business.ktnb.CoreBusiness;
using vpb.app.business.ktnb.Definition.DMS;

namespace VPB_KTNB.Helpers
{
    public class MenuHelper
    {
        #region menu box helper

        private StringBuilder _script = new StringBuilder();
        private UserContext _objUserContext = null;
        private Role _role;
        private string _dbname;
        private string _roleID;
        private string _roleName;
        private bus_Application _objApp;
        private DataSet _dsApp;


        /// <summary>
        /// MenuHelper
        /// </summary>
        public MenuHelper()
        {
            _objUserContext = (UserContext)System.Web.HttpContext.Current.Session["objUserContext"];
            _role = (Role)_objUserContext.Roles[0];
            _dbname = System.Configuration.ConfigurationManager.AppSettings["DBName"].ToString();
            _roleID = _role.RoleID;
            _roleName = _role.RoleName;
            _objApp = bus_Application.Instance(_objUserContext);
        }

        /// <summary>
        /// CreateMenuBox
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="Color"></param>
        /// <param name="items"></param>
        /// <param name="urls"></param>
        /// <author>dungnt</author>
        /// <created>20080822</created>
        /// <modifiedby></modifiedby>
        /// <modifieddate></modifieddate>
        private void CreateMenuBox(string Title, string Color, string[] Items, string[] Urls)
        {
            string begin_box = "<div class='collapsed'>";
            string end_box = "</div>";
            string title = "<span><font color='" + Color + "'>" + Title + "</font></span>";
            StringBuilder box = new StringBuilder();

            box.Append(begin_box);
            box.Append(title);

            //loop for adding item
            for (int i = 0; i < Items.Length; i++)
            {
                string item = Items[i];
                string url = Urls[i];
                string il = "<a href='" + url + "' target='fraDetail'>" + item + "</a>";
                box.Append(il);
            }
            box.Append(end_box);
            _script.Append(box.ToString());
        }

        private void CreateMenuBar(string currentApp, string appID, string Title, string Color, string Url)
        {
            string begin_box = "";
            string end_box = "";
            string divID = appID.Replace("-", "_");
            string title;
        
            //title = "<td><div id='" + divID + "' class='Menubar'  onmouseover='Overme(this);' onmouseout='Outme(this);' onclick='" + Url + "'>" + Title + "</div></td>";
            //title = "<td><a><span id='" + divID + "' class='Menubar'  onmouseover='Overme(this);' onmouseout='Outme(this);' onclick='" + Url + "'>" + Title + "</span></a></td>";
            title = "<li class=\"menu-main\">";
            title += "    <a href=\"/Default.aspx?a=" + appID + "\" style='" + (currentApp == appID ? "border-bottom:4px solid #E61C29;padding: 8px 8px 4px 8px !important;" : "border-bottom:0px solid #E61C29;") + "'>";
            title += "        <span class=\"car-name\"></span><span class=\"sp-text\">" + Title + "</span>";
            title += "    </a>";
            title += "</li>";
            StringBuilder box = new StringBuilder();
            box.Append(begin_box);
            box.Append(title);
            box.Append(end_box);
            _script.Append(box.ToString());
        }

        /// <summary>
        /// BuildMenuBox
        /// </summary>
        private string BuildMenuBox()
        {
            string begin = "<div style='float: left' id='my_menu' class='sdmenu'>";
            string end = "</div>";
            _script.Insert(0, begin);
            _script.Append(end);
            return _script.ToString();
        }

        private string BuildMenuBar()
        {
            //string begin = "<div style='float: left' id='my_menu' class='sdmenu'><table cellspacing='0' id='uxp_hdr_tabs' class='uxp_hdr_pointer'><tr>";
            //string end = "</tr></table></div>";
            string begin = "<ul class=\"nav navbar-nav navbar-left menu-top\">";
            string end = "</ul>";
            _script.Insert(0, begin);
            _script.Append(end);
            return _script.ToString();
        }

        /// <summary>
        /// LoadMenuBox
        /// </summary>
        /// <param name="role"></param>
        /// <author>dungnt</author>
        /// <createdate>20080823</createdate>
        /// <modified></modified>
        /// <modifieddate></modifieddate>
        public string LoadMenuBox(string ApplicationID)
        {
            if(string.IsNullOrEmpty(ApplicationID))
                _dsApp = _objApp.getApplicationByRole(_roleID, " AND LEFTRIGHTPOSITION=1");
            else
                _dsApp = _objApp.getApplicationByRole(_roleID, " AND LEFTRIGHTPOSITION=3 AND PK_APPLICATIONID='" + ApplicationID + "'");

            if ((_dsApp == null) || (_dsApp.Tables[0].Rows.Count == 0)) return string.Empty;

            //loop for getting all applications for this user
            foreach (DataRow R in _dsApp.Tables[0].Rows)
            {
                string appID = R["PK_APPLICATIONID"].ToString();
                string appName = R["NAME"].ToString();
                string Title = appName;

                //loop for getting all components related to each application and role
                DataSet dsCom = _objApp.getComponentByApplication(appID, _roleID);
                if ((dsCom == null) || (dsCom.Tables[0].Rows.Count == 0))
                {
                    string[] Items = new string[0];
                    string[] Urls = new string[0];
                    string Color = "";
                    CreateMenuBox(Title, Color, Items, Urls);
                }
                else
                {
                    int comps = dsCom.Tables[0].Rows.Count;
                    string[] Items = new string[comps];
                    string[] Urls = new string[comps];
                    string Color = "";
                    int index = 0;
                    foreach (DataRow Rcomp in dsCom.Tables[0].Rows)
                    {
                        string item = Rcomp["COMPONENTNAME"].ToString();
                        string url = Rcomp["URL"].ToString();
                        Items[index] = item; Urls[index] = url;
                        index++;
                    }
                    CreateMenuBox(Title, Color, Items, Urls);
                }
            }
            return BuildMenuBox();
        }

        /// <summary>
        /// LoadMenBar
        /// </summary>
        /// <returns></returns>
        public String LoadMenuBar(string currentApp)
        {
            _objApp = null;
            _objApp = bus_Application.Instance(_objUserContext);
            _dsApp = null;
        
            string condition = " AND LEFTRIGHTPOSITION=3 AND FK_PARENTAPPLICATIONID IS NULL";
            condition += " AND FK_PROJECTID='" + _objUserContext.ProjectID + "'"; 
            _dsApp = _objApp.getApplicationByRole(_roleID, condition);

            if ((_dsApp == null) || (_dsApp.Tables[0].Rows.Count == 0)) return string.Empty;
        
            foreach (DataRow R in _dsApp.Tables[0].Rows)
            {
                string appID = R["PK_APPLICATIONID"].ToString();
                string appName = R["NAME"].ToString();
                string URL = R["URL"].ToString();
                string Title = appName;
                string Color = "";
                CreateMenuBar(currentApp, appID, Title, Color, URL);    
            }
            return BuildMenuBar();
        }

        #endregion
    }

    public class XMenuHelper
    {
        #region Xmenu box helper
        private StringBuilder _script = new StringBuilder();
        private UserContext _objUserContext = null;
        private Role _role;
        private string _dbname;
        private string _roleID;
        private string _roleName;
        private bus_Application _objApp;
        private DataSet _dsApp;

        public XMenuHelper()
        {
            _objUserContext = (UserContext)System.Web.HttpContext.Current.Session["objUserContext"];
            _role = (Role)_objUserContext.Roles[0];
            _dbname = System.Configuration.ConfigurationManager.AppSettings["DBName"].ToString();
            _roleID = _role.RoleID;
            _roleName = _role.RoleName;
            _objApp = bus_Application.Instance(_objUserContext); //_objApp = new bus_Application(_objUserContext, _dbname);
        }

        /// <summary>
        /// CreateMenuBox
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="Color"></param>
        /// <param name="items"></param>
        /// <param name="urls"></param>
        /// <author>dungnt</author>
        /// <created>20080822</created>
        /// <modifiedby></modifiedby>
        /// <modifieddate></modifieddate>
        private void CreateMenuBox(string AppID, string ComponentID, string Title, string TitleColor, string[] Items, string[] Urls, string[] Components)
        {
            //string begin_box = "<div class='ListNugget' id='" + AppID + "' style='width: 100%' name='" + AppID + "'>";
            //begin_box += "<table class='ListNuggetHeader' id='" + AppID + "Header' cellSpacing='0' cellPadding='0' width='100%' name='" + AppID + "Header'>";
            string begin_box = "<ul class=\"nav nav-stacked nav-pills\">";
            begin_box += "<li>";
            begin_box += "        <ul class=\"nav nav-second-level\">";
            begin_box += "            <li>";
            begin_box += "                <span class=\"icon-menu\"></span><span class=\"left-text\" onclick=\"return PartWrapperToggle('" + AppID + "');\">" + Title + "</span>";
            begin_box += "            </li>";
            string end_box = "</ul>";

            //string title = "<tr>";
            //title += "<td width='10' class='ListNuggetButtonCellWhite' onclick=\"PartWrapperToggle('" + AppID + "');\" >";
            //title += "<div class='ListNuggetButton'>";
            //title += "<img class='ListNuggetUpButton' id='" + AppID + "Up' title='' height='16' alt='' src='Controls/Xmenu/open.gif' width='16' align='right' border='0' name='" + AppID + "Up' />";
            //title += "<img class='ListNuggetDownButton' id='" + AppID + "Down' title='' height='16' alt='' src='Controls/Xmenu/close.gif' width='16' align='right' border='0' name='" + AppID + "Down' />";
            //title += "</div></td>";
            //title += "<td width='507' class='ListNuggetTitleCellWhite' onselectstart='window.event.cancelBubble=true; return false;' onclick=\"PartWrapperToggle('" + AppID + "');\" >";
        
            //title += "<a onmousemove='return overme();' class='ListNuggetTitle' onclick=\"return PartWrapperToggle('" + AppID + "');\"><font color='Yellow'>" + Title + "</font></a>";

            //title += "</td>";
            //title += "</tr>";

            StringBuilder box = new StringBuilder();

            box.Append(begin_box);
            //box.Append(title);

            //string begin_item = "<tr><td width='100%' colspan='2'><div class='ListNuggetBody' id='" + AppID + "Body' name='" + AppID + "Body'><ul class='LinkPartListUL'>";
            //string end_item = "</ul></div></td></tr>";
            string end_item = "</ul></li>";

            //box.Append(begin_item);

            //loop for adding item
            for (int i = 0; i < Items.Length; i++)
            {
                string item = Items[i];
                string _componentId = Components[i];
                if (string.IsNullOrEmpty(item)) continue;
                string url = Urls[i];

                byte onnewwindow = 0;
                if ((url.IndexOf("nw=1") != -1) || (url.IndexOf(".pdf") != -1))
                    onnewwindow = 1;

                string li;
                li = "<li>";
                if (onnewwindow==0)
                    li += "    <a" + (ComponentID == _componentId ? " style=\"color:" + TitleColor + "\"" : "") + " href=\"" + (url.StartsWith("/") ? url : "/" + url) + "\">" + item + "</a>";
                else
                    li += "    <a" + (ComponentID == _componentId ? " style=\"color:" + TitleColor + "\"" : "") + " href=\"" + (url.StartsWith("/") ? url : "/" + url) + "\" target=\"_blank\">" + item + "</a>";
                li += "</li>";
                box.Append(li);
            }
            box.Append(end_item);
            box.Append(end_box);
            _script.Append(box.ToString());
        }

        protected string GetOnNewWindow(string AppID, string url)
        {
            bus_Component objcom = bus_Component.Instance(_objUserContext); //bus_Component objcom = new bus_Component(_objUserContext,_dbname);
            DataSet ds = objcom.getList(" AND FK_ApplicationID='" + AppID + "' AND URL='" + url + "'", "OnNewWindow");
            if (ds == null) return "0";
            if (ds.Tables[0].Rows.Count == 0) return "0";
            return ds.Tables[0].Rows[0]["OnNewWindow"].ToString();
        }

        /// <summary>
        /// LoadMenuBox
        /// </summary>
        /// <param name="role"></param>
        /// <author>dungnt</author>
        /// <createdate>20080823</createdate>
        /// <modified>dungnt</modified>
        /// <modifieddate>20090120 - loading sub application</modifieddate>
        public string LoadMenuBox(string curApplication, string ApplicationID, string ComponentID,  string TitleColor)
        {
            DataSet ds_comprolegroup = GetComponentNGroupMappedWithRole(_roleID);

            string icondefault = "bullet.gif";

            //string bicon = "";
            string eicon = "</img>";

            string _groupid;
            _groupid = ((CORE.CoreObjectContext.Group)_objUserContext.Groups[_objUserContext.GroupDefault]).GroupID;

            if (string.IsNullOrEmpty(curApplication))
                _dsApp = _objApp.getApplicationByRole(_roleID, " AND LEFTRIGHTPOSITION=1"); 
            else
                _dsApp = _objApp.getApplicationByRole(_roleID, " AND (LEFTRIGHTPOSITION=3) AND (PK_APPLICATIONID='" + curApplication + "' OR FK_PARENTAPPLICATIONID='" + curApplication + "')");

            if ((_dsApp == null) || (_dsApp.Tables[0].Rows.Count == 0)) return string.Empty;

            //loop for getting all applications for this user
            foreach (DataRow R in _dsApp.Tables[0].Rows)
            {
                //string appID = R["FK_PARENTAPPLICATIONID"] == null ? R["PK_APPLICATIONID"].ToString() : R["FK_PARENTAPPLICATIONID"].ToString();
                string appID = R["PK_APPLICATIONID"].ToString();
                string appName = R["NAME"].ToString();
                string Title = appName;

                //loop for getting all components related to each application and role
                DataSet dsCom;
                //if (appID.ToUpper() == VPB_CRM.Helper.APPLICATION.SHARED_PERMISSION.ID.ToUpper())
                if(1==2)
                {
                    dsCom = GetComponentDuocUQ();
                }
                else
                    dsCom = _objApp.getComponentByApplication(appID, _roleID);

                if ((dsCom == null) || (dsCom.Tables[0].Rows.Count == 0))
                {
                    continue;
                }
                else
                {
                    int comps = dsCom.Tables[0].Rows.Count;
                    string[] Items = new string[comps];
                    string[] Urls = new string[comps];
                    string[] Components = new string[comps];
                    int index = 0;
                    foreach (DataRow Rcomp in dsCom.Tables[0].Rows)
                    {
                        string group_mapped = getgroupmapped(Rcomp["PK_ComponentID"].ToString(), _roleID, ds_comprolegroup);

                        bool writecomponent = false;

                        if (string.IsNullOrEmpty(group_mapped))
                            writecomponent = true;
                        else
                            if (StringHelper.isSubstring(group_mapped.ToUpper(), _groupid.ToUpper()))
                                writecomponent = false;
                            else
                                writecomponent = true;
                    
                        if (writecomponent)
                        {
                            string icon;
                            if (Rcomp["ICON"]!=DBNull.Value)
                                icon = Rcomp["ICON"].ToString();
                            else
                                icon = icondefault;

                            //bicon = "<img src='Images/AppCom/" + icon + "' border='0'>   ";
                            //string item = "<div id='div_" + appID.ToString() + "_" + index.ToString() + "' style='color:Teal' onmouseout='outme(this);' onmousemove='movedme(this);' onclick='selectingItem(this);'>" + bicon + Rcomp["COMPONENTNAME"].ToString() + eicon + "</div>";
                            string item = Rcomp["COMPONENTNAME"].ToString();
                            string url = Rcomp["URL"].ToString();
                            string componentId = Rcomp["PK_COMPONENTID"].ToString();
                            Regex regex = new Regex(@"a=(\w+)-(\w+)-(\w+)-(\w+)-(\w+)");
                            MatchCollection matchC = regex.Matches(url);
                            int idx = 0;
                            foreach(Match match in matchC)
                            {
                                if (idx == 0)
                                    url = url.Replace(match.Value, match.Value + "&curApp=" + curApplication);
                                else
                                {
                                    url = url.Replace("&" + match.Value, string.Empty);                                
                                }
                                idx++;
                            }
                            //ApplicationID
                            Items[index] = item; 
                            Urls[index] = url;
                            Components[index] = componentId;
                            index++;
                        }
                    }
                    CreateMenuBox(appID, ComponentID, Title, TitleColor, Items, Urls, Components);
                }
            }
            return _script.ToString();
        }

        private DataSet GetComponentDuocUQ()
        {
            string condition = " and pk_componentid in (select fk_appliedonobjectid from t_permission where typeofobject=" + TYPE_OF_OBJECT.COMPONENT + " and fk_appliedonid='" + _objUserContext.UserID + "')";
            string query = "pk_componentid, url+'&uyquyen=1' as url, name as componentname,onnewwindow,icon";
            bus_Component objcomp = bus_Component.Instance(_objUserContext); //bus_Component objcomp = new bus_Component(_objUserContext,_dbname);
            DataSet ds = objcomp.getList(condition, query);
            objcomp = null;
            return ds;
        }

        private DataSet GetComponentNGroupMappedWithRole(string roleID)
        {
            bus_Component objcomp = bus_Component.Instance(_objUserContext); //bus_Component objcomp = new bus_Component(_objUserContext,_dbname);
            DataSet ds = objcomp.getComponentNGroupMappedWithRole(roleID);
            objcomp = null;
            return ds;
        }

        private string getgroupmapped(string componentID, string roleID, DataSet ds_comprolegroup)
        {
            if (ds_comprolegroup == null) return string.Empty;
            if (ds_comprolegroup.Tables[0].Rows.Count == 0) return string.Empty;
            DataTable table = ds_comprolegroup.Tables[0];
            DataRow[] Rs = table.Select("FK_ComponentID='" + componentID + "' AND FK_RoleID='" + roleID + "'");
            if (Rs.Length == 0) return string.Empty;
            string s=string.Empty;
            for (int i = 0; i < Rs.Length; i++)
                s += Rs[i]["FK_GroupID"].ToString() + ",";
            return s;
        }

        #endregion
    }
}