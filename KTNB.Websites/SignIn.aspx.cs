using System;
using CORE.CoreObjectContext;
using ldapif;
using System.Data;
using System.Text;
using System.Diagnostics;
using System.Web;
using System.Configuration;
using CORE.UMS.CoreBusiness;
using VPB_KTNB.Helpers;
using VPB_KTNB.ldap;

public partial class SignIn : PageBase
{
    #region initiation page variables

    private UserContext cmdUserContext = null;
    private string _userName = string.Empty;
    private string _password = string.Empty;
    private string _token = string.Empty;
    private string _action = string.Empty;
    private string _errorMessage = string.Empty;

    #endregion initiation page variables

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        #region Token

        //if (bool.Parse(System.Configuration.ConfigurationManager.AppSettings["Token"]))
        //{
        //    string token = Request["t"];
        //    if (string.IsNullOrEmpty(token))
        //    {
        //        showMessageError("Token không xác định");
        //    }
        //    else
        //    {
        //        processToken(token);
        //    }
        //}

        #endregion Token

        #region get data submit

        _action = Request["act"];
        _userName = Request.Form["UserName"];
        _password = Request.Form["Password"];

        #endregion get data submit

        #region action

        if (!string.IsNullOrEmpty(_action))
        {
            if (_action == "out")
            {
                signOut();
            }
            else if (_action == "login")
            {
                if (!string.IsNullOrEmpty(_password))
                {
                    if (!string.IsNullOrEmpty(_userName))
                    {
                        if (System.Configuration.ConfigurationManager.AppSettings["LDAP"] == "1")
                        {
                            if (LDAPloginWs(_userName, _password))
                                Response.Write(AuthorizeUser(_userName));
                            else
                                Response.Write("Bạn kiểm tra lại tên và mật khẩu.\nMessageError:" + _errorMessage);
                        }
                        else
                        {
                            Response.Write(AuthorizeUser(_userName));
                        }
                        Response.Flush();
                        Response.End();
                    }
                }
            }
            else if (_action == "loadgroup")
            {
                Response.Write(loadGroup());
                Response.Flush();
                Response.End();
            }
            else if (_action == "acceptgroup")
            {
                byte g = byte.Parse(Request["group"].ToString());
                Response.Write(acceptGroup(g));
                Response.Flush();
                Response.End();
            }
        }

        #endregion action
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = System.Configuration.ConfigurationManager.AppSettings["ProjectName"];
        this.LoginButton.Attributes.Add("onclick", "{login(); return false;}");
    }

    #region page helper processing

    protected string AuthorizeUser(string username)
    {
        cmdUserContext = new UserContext();
        int groups = 0;
        if (cmdUserContext.Authenticate(username, "", Session.SessionID))
        {
            groups = cmdUserContext.Groups.Count;
            System.Web.HttpContext.Current.Session["objUserContext"] = cmdUserContext;
            string rolename = ((Role)cmdUserContext.Roles[0]).RoleName;
            string groupname = ((Group)cmdUserContext.Groups[0]).GroupName;
            CommonFunc.AddAuditLog(string.Empty, string.Empty, string.Empty, 0, 0, "Login", string.Empty, rolename, groupname);
        }
        StringBuilder data = new StringBuilder();
        data.Append("ci={");
        data.Append("'gs'");
        data.Append(":'" + groups.ToString() + "'");
        data.Append("}");
        return data.ToString();
    }

    private string loadGroup()
    {
        UserContext objUserContext = (UserContext)System.Web.HttpContext.Current.Session["objUserContext"];
        StringBuilder options = new StringBuilder();
        options.Append("<option value='' selected='true'></option>");
        for (int i = 0; i < objUserContext.Groups.Count; i++)
        {
            string option = "@FM<option value='" + i.ToString() + "'>" + ((Group)objUserContext.Groups[i]).GroupDescription + "</option>";
            options.Append(option);
        }
        return options.ToString();
    }

    private string acceptGroup(byte g)
    {
        UserContext cmdUserContext = (UserContext)System.Web.HttpContext.Current.Session["objUserContext"];
        cmdUserContext.GroupDefault = g;
        System.Web.HttpContext.Current.Session["objUserContext"] = cmdUserContext;
        return string.Empty;
    }

    private string AuthenticationToken(string token)
    {
        string username = string.Empty;
        //if (!string.IsNullOrEmpty(token))
        //{
        //    string CAS_Site = System.Configuration.ConfigurationManager.AppSettings["VPB_CAS_SITE"].ToString();
        //    AuthenticationService objCAS = new AuthenticationService();
        //    username = objCAS.ValidateToken(token);
        //    objCAS = null;
        //}
        return username;
    }

    private void SSO_SignIn()
    {
        Session.Abandon();
        string SSO_Site = System.Configuration.ConfigurationManager.AppSettings["VPB_SSO_SITE"].ToString();
        Response.Redirect(SSO_Site);
    }

    private void CloseSite()
    {
        Session.Abandon();
        Response.Write("<script type=\"text/javascript\">");
        Response.Write("window.opener=null;");
        Response.Write("window.close(true);");
        Response.Write("</script>");
        Response.Flush();
        Response.End();
    }

    private void signOut()
    {
        Session.Abandon();
        Response.Redirect("SignIn.aspx");
    }

    private void showMessageError(string ErrorMessage)
    {
        Response.Write("<center><strong><font color='red'>" + ErrorMessage + "</font></strong></center>");
        Response.Flush();
        Response.End();
    }

    private void processValidToken(string username)
    {
        string data = AuthorizeUser(username);
        string[] d = data.Split(':');
        int countgroup = int.Parse(d[1].Substring(1, 1));
        if (countgroup >= 1)
        {
            data = "window.location.href = 'Default.aspx'";
        }
        else
        {
            data = "alert('Kiểm tra lại tên đăng nhập hoặc mật khẩu.');";
        }

        Response.Write("<script type=\"text/javascript\">");
        Response.Write(data);
        Response.Write("</script>");
        Response.Flush();
        Response.End();
    }

    private void processToken(string Token)
    {
        bus_Token vpbToken = bus_Token.Instance();
        double ConsumedTimeOnToken = vpbToken.getConsumedTimeOnToken(Token);
        if (ConsumedTimeOnToken >= 15)
        {
            showMessageError("Token đã hết hạn sử dụng");
        }
        else
        {
            processValidToken(vpbToken.getUser(Token));
        }
    }

    #endregion page helper processing

    #region ldap

    /// <summary>
    /// LDAPlogin
    /// </summary>
    /// <param name="user"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    private bool LDAPlogin(string user, string password)
    {
        string lConnection;
        string lServerName;
        int lport = 389;

        #region get LDAP info config

        string sql = "select name,value from t_project_parameter where name in ('ldap_server','ldap_conn','ldap_port')";
        System.Data.SqlClient.SqlConnection sqlCon = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
        sqlCon.Open();
        System.Data.SqlClient.SqlCommand sqlCmd = new System.Data.SqlClient.SqlCommand(sql, sqlCon);
        System.Data.SqlClient.SqlDataAdapter sqlAdap = new System.Data.SqlClient.SqlDataAdapter(sqlCmd);
        DataSet ds = new DataSet();
        sqlAdap.Fill(ds, "Params");
        if (ds == null)
        {
            sqlCon.Close();
            return false;
        }

        DataRow[] Rs;
        Rs = ds.Tables[0].Select("name='ldap_server'");
        lServerName = Rs[0]["value"].ToString();

        Rs = null;
        Rs = ds.Tables[0].Select("name='ldap_port'");
        lport = Convert.ToInt16(Rs[0]["value"].ToString());

        Rs = null;
        Rs = ds.Tables[0].Select("name='ldap_conn'");
        lConnection = Rs[0]["value"].ToString();

        sqlCon.Close();

        #endregion get LDAP info config

        LdapError error = new LdapError();
        string name = "";

        name = "uid=" + user + lConnection;

        LDAPUSER._ldapServerName = lServerName;
        LDAPUSER._port = lport;
        string isAuthen;

        try
        {
            isAuthen = LDAPUSER.IsAuthenticate(name, password);
        }
        catch (Exception ex)
        {
            _errorMessage = ex.Message;
            return false;
        }

        if (isAuthen.Equals(error.openWith["0"].ToString()))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool LDAPloginWs(string user, string password)
    {
        bool vlogin = false;
        IldapClient client = new IldapClient();
        string err = client.ldapLogin(user, password);
        if (string.IsNullOrEmpty(err))
        {
            vlogin = true;
        }

        client.Close();
        client = null;

        return vlogin;
    }

    #endregion ldap
}