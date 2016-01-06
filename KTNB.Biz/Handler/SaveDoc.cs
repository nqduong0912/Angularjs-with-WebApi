using CORE.CoreObjectContext;
using vpb.app.business.ktnb.CoreBusiness;
using CORE.HELPERS;
using CORE.WFS.CoreBusiness;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;
using System.Collections.Generic;
using System.Globalization;
using KTNB.Extended.Biz;

namespace KTNB.Handler
{
    public class SaveDoc : IHttpHandler, IRequiresSessionState
    {
        private int _action;
        private string _activitydefinition = string.Empty;
        private string _activityinstanceid = string.Empty;
        private string _archivedpath = string.Empty;
        private string _doclinkid = string.Empty;
        private string _docspaceid = string.Empty;
        private string _documentdescription = string.Empty;
        private string _documentid = string.Empty;
        private string _documentname = string.Empty;
        private string _documentstatus = string.Empty;
        private string _documenttypeid = string.Empty;
        private string _folderid = string.Empty;
        private UserContext _objUserContext;
        private string _processdefinition = string.Empty;
        private string _processinstanceid = string.Empty;

        private string CreateBody(HttpContext context)
        {
            string str = string.Empty;
            string str2 = string.Empty;
            string str3 = string.Empty;
            string str4 = string.Empty;
            string str5 = string.Empty;
            StringBuilder builder = new StringBuilder();
            builder.Append("<properties>");
            NameValueCollection form = context.Request.Form;
            for (int i = 0; i < form.Count; i++)
            {
                str = "";
                str2 = "";
                str3 = "";
                str4 = "";
                str5 = "";
                str = form.Keys[i];
                if (!string.IsNullOrEmpty(str))
                {
                    if (str.ToUpper() == "DOCNAME")
                    {
                        this._documentname = form[str].ToString();
                    }
                    else if (str.ToUpper() == "DOCDESCRIPTION")
                    {
                        this._documentdescription = form[str].ToString();
                    }
                    else if ((str.Length >= 40) && str.Contains("ID"))
                    {
                        str4 = "";
                        str2 = form[str].ToString();
                        str5 = this.Mid(str, 4, 0x24).Replace("_", "-");
                        str3 = this.Mid(str, 2, 1);
                        str4 = ("<id>" + str5 + "</id>") + "<type>" + str3 + "</type>";
                        if (str3 == "6")
                        {
                            str2 = str2.Replace(",", "");
                        }
                        str4 = str4 + "<value>" + str2 + "</value>";
                        str4 = "<property>" + str4 + "</property>";
                        builder.Append(str4);
                    }
                }
            }
            builder.Append("</properties>");
            return builder.ToString();
        }

        private string createDocumentProperties(string documentid, HttpContext context)
        {
            string str = string.Empty;
            string str2 = string.Empty;
            string str3 = string.Empty;
            string str4 = string.Empty;
            string str5 = string.Empty;
            DataSet ds = bus_Type_Doc_Property.Instance(this._objUserContext).getEmpty("FK_PROPERTYID,PROPERTYTYPE,VALUE=''");
            NameValueCollection form = context.Request.Form;
            for (int i = 0; i < form.Count; i++)
            {
                str2 = "";
                str3 = "";
                str4 = "";
                str5 = "";
                str2 = form.Keys[i];
                if (((!string.IsNullOrEmpty(str2) && (str2.ToUpper() != "DOCNAME")) && ((str2.ToUpper() != "DOCDESCRIPTION") && (str2.Length >= 40))) && str2.Contains("ID"))
                {
                    str3 = form[str2].ToString();
                    str5 = this.Mid(str2, 4, 0x24).Replace("_", "-");
                    str4 = this.Mid(str2, 2, 1);
                    if (str4 == "6")
                    {
                        str3 = str3.Replace(",", "");
                    }
                    DataRow row = ds.Tables[0].NewRow();
                    row["FK_PROPERTYID"] = str5;
                    row["PROPERTYTYPE"] = str4;
                    row["VALUE"] = str3;
                    ds.Tables[0].Rows.Add(row);
                }
            }
            if (CommonFunc.isValidDataSet(ds))
            {
                str = bus_Document.Instance(this._objUserContext).createDocumentProperties(documentid, ds);
            }
            return str;
        }

        private string createDocumentProperties(string documentid, string documenttypeid, HttpContext context)
        {
            string str = string.Empty;
            string str2 = string.Empty;
            string str3 = string.Empty;
            string str4 = string.Empty;
            string str5 = string.Empty;
            string str6 = this.getPropertiesOfDoctype(documenttypeid);
            DataSet ds = bus_Type_Doc_Property.Instance(this._objUserContext).getEmpty("FK_PROPERTYID,PROPERTYTYPE,VALUE=''");
            NameValueCollection form = context.Request.Form;
            for (int i = 0; i < form.Count; i++)
            {
                str2 = "";
                str3 = "";
                str4 = "";
                str5 = "";
                str2 = form.Keys[i];
                if (((!string.IsNullOrEmpty(str2) && (str2.ToUpper() != "DOCNAME")) && ((str2.ToUpper() != "DOCDESCRIPTION") && (str2.Length >= 40))) && str2.Contains("ID"))
                {
                    str3 = form[str2].ToString();
                    str5 = this.Mid(str2, 4, 0x24).Replace("_", "-");
                    if (str6.ToUpper().LastIndexOf(str5.ToUpper()) != -1)
                    {
                        str4 = this.Mid(str2, 2, 1);
                        if (str4 == "6")
                        {
                            str3 = str3.Replace(",", "");
                        }
                        DataRow row = ds.Tables[0].NewRow();
                        row["FK_PROPERTYID"] = str5;
                        row["PROPERTYTYPE"] = str4;
                        row["VALUE"] = str3;
                        ds.Tables[0].Rows.Add(row);
                    }
                }
            }
            if (CommonFunc.isValidDataSet(ds))
            {
                str = bus_Document.Instance(this._objUserContext).createDocumentProperties(documentid, ds);
            }
            return str;
        }

        private string createProcess(string ProcessDefinition, string ProcessInstanceID, string DocumentID)
        {
            string str = string.Empty;
            bus_WF_Instance instance = bus_WF_Instance.Instance(this._objUserContext);
            Guid groupID = new Guid(((CORE.CoreObjectContext.Group)this._objUserContext.Groups[0]).GroupID);
            string groupName = ((CORE.CoreObjectContext.Group)this._objUserContext.Groups[0]).GroupName;
            str = instance.CreateNew_WFInstance(new Guid(ProcessInstanceID), this._documentname, this._documentdescription, new Guid(ProcessDefinition), this._objUserContext.UserName, this._objUserContext.UserName, 4, new Guid(DocumentID), groupID, groupName);
            instance = null;
            return str;
        }

        private void deleteDocument(string documentid)
        {
            bus_Document.Instance(this._objUserContext).deleteDocument(documentid);
        }

        private string getPropertiesOfDoctype(string DocumentTypeId)
        {
            string str = string.Empty;
            DataTable table = bus_Property.Instance(this._objUserContext).getList(" AND FK_DOCTYPEID='" + DocumentTypeId + "'", "PK_PROPERTYID").Tables[0];
            foreach (DataRow row in table.Rows)
            {
                str = str + row["PK_PROPERTYID"].ToString() + ";";
            }
            return str;
        }

        private bool isSubstring(string main, string substr)
        {
            return Regex.Match(main, substr).Success;
        }

        private string Left(string param, int length)
        {
            return param.Substring(0, length);
        }

        private string Mid(string param, int startIndex)
        {
            return param.Substring(startIndex);
        }

        private string Mid(string param, int startIndex, int length)
        {
            return param.Substring(startIndex, length);
        }

        public void ProcessRequest(HttpContext context)
        {
            string str = string.Empty;
            if (!context.Response.IsClientConnected)
            {
                str = "Your connection was disconnected. Please try again.";
            }
            else
            {
                if (!string.IsNullOrEmpty(context.Request.Form["act"]))
                {
                    this._action = int.Parse(context.Request.Form["act"]);
                }
                this._documenttypeid = context.Request.Form["doctype"];
                this._documentid = context.Request.Form["doc"];
                this._doclinkid = context.Request.Form["doclink"];
                this._documentname = context.Request.Form["docname"];
                this._documentdescription = context.Request.Form["docdescription"];
                this._documentstatus = context.Request.Form["docstatus"];
                this._docspaceid = context.Request.Form["docspace"];
                this._folderid = context.Request.Form["folder"];
                this._processdefinition = context.Request.Form["wf_def_id"];
                this._activitydefinition = context.Request.Form["act_def_id"];
                this._processinstanceid = context.Request.Form["wf_id"];
                this._activityinstanceid = context.Request.Form["act_id"];
                if (string.IsNullOrEmpty(this._docspaceid))
                {
                    this._docspaceid = "00000000-0000-0000-0000-000000000000";
                }
                if (string.IsNullOrEmpty(this._folderid))
                {
                    this._folderid = this._docspaceid;
                }
                if (this._action == 0)
                {
                    str = "Server unknow your action. Please check your action.";
                }
                else
                {
                    this._objUserContext = (UserContext)context.Session["objUserContext"];
                    if (this._action == 1)
                    {
                        bus_Document document = bus_Document.Instance(this._objUserContext);
                        if (string.IsNullOrEmpty(this._documentstatus))
                        {
                            str = document.createDocument(this._documentid, this._documentname, this._documentdescription, this._documenttypeid, this._folderid, this._archivedpath, this._objUserContext.UserName, ((CORE.CoreObjectContext.Group)this._objUserContext.Groups[0]).GroupName, DateTime.Now.Year, DateTime.Now.Month);
                        }
                        else
                        {
                            str = document.createDocument(this._documentid, this._documentname, this._documentdescription, this._documenttypeid, this._folderid, this._archivedpath, this._objUserContext.UserName, ((CORE.CoreObjectContext.Group)this._objUserContext.Groups[0]).GroupName, DateTime.Now.Year, DateTime.Now.Month, int.Parse(this._documentstatus));
                        }
                        if (string.IsNullOrEmpty(str))
                        {
                            str = this.createDocumentProperties(this._documentid, this._documenttypeid, context);
                            if (!string.IsNullOrEmpty(str))
                            {
                                this.deleteDocument(this._documentid);
                            }
                        }
                    }
                    else if (this._action == 4)
                    {
                        bus_Document document2 = bus_Document.Instance(this._objUserContext);
                        if (string.IsNullOrEmpty(this._documentstatus))
                        {
                            str = document2.createDocument(this._documentid, this._documentname, this._documentdescription, this._documenttypeid, this._folderid, this._archivedpath, this._objUserContext.UserName, ((CORE.CoreObjectContext.Group)this._objUserContext.Groups[0]).GroupName, DateTime.Now.Year, DateTime.Now.Month);
                        }
                        else
                        {
                            str = document2.createDocument(this._documentid, this._documentname, this._documentdescription, this._documenttypeid, this._folderid, this._archivedpath, this._objUserContext.UserName, ((CORE.CoreObjectContext.Group)this._objUserContext.Groups[0]).GroupName, DateTime.Now.Year, DateTime.Now.Month, int.Parse(this._documentstatus));
                        }
                        if (string.IsNullOrEmpty(str))
                        {
                            if (!string.IsNullOrEmpty(this._doclinkid))
                            {
                                document2.addLink(this._documentid, this._doclinkid);
                            }
                            str = this.createDocumentProperties(this._documentid, this._documenttypeid, context);
                            if (!string.IsNullOrEmpty(str))
                            {
                                this.deleteDocument(this._documentid);
                            }
                        }
                    }
                    else if (this._action == 2)
                    {
                        str = this.updateDocument(this._documentid, context);
                    }
                    else if (this._action == 8)
                    {
                        str = bus_Document.Instance(this._objUserContext).deleteDocument(this._documentid);
                    }
                    else if (this._action == 0x80)
                    {
                        str = bus_WF_Instance.Instance(this._objUserContext).deleteProcessInstance(this._processinstanceid, this._objUserContext.UserName, string.Empty, this._objUserContext.UserID, this._objUserContext.UserIP);
                    }
                    else if (this._action == 0x20)
                    {
                        bus_Document document4 = bus_Document.Instance(this._objUserContext);
                        if (string.IsNullOrEmpty(this._documentstatus))
                        {
                            str = document4.createDocument(this._documentid, this._documentname, this._documentdescription, this._documenttypeid, this._folderid, this._archivedpath, this._objUserContext.UserName, ((CORE.CoreObjectContext.Group)this._objUserContext.Groups[0]).GroupName, DateTime.Now.Year, DateTime.Now.Month);
                        }
                        else
                        {
                            str = document4.createDocument(this._documentid, this._documentname, this._documentdescription, this._documenttypeid, this._folderid, this._archivedpath, this._objUserContext.UserName, ((CORE.CoreObjectContext.Group)this._objUserContext.Groups[0]).GroupName, DateTime.Now.Year, DateTime.Now.Month, int.Parse(this._documentstatus));
                        }
                        if (string.IsNullOrEmpty(str))
                        {
                            str = this.createDocumentProperties(this._documentid, context);
                        }
                        if (string.IsNullOrEmpty(str))
                        {
                            str = this.createProcess(this._processdefinition, this._processinstanceid, this._documentid);
                        }
                        if (!string.IsNullOrEmpty(str))
                        {
                            this.deleteDocument(this._documentid);
                        }
                    }
                }
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(str);
            context.Response.Flush();
            context.Response.End();
        }

        private string Right(string param, int length)
        {
            try
            {
                return param.Substring(param.Length - length, length);
            }
            catch (Exception)
            {
                return param;
            }
        }

        private string updateDocument(string documentid, HttpContext context)
        {
            string str = string.Empty;
            string str2 = string.Empty;
            string str3 = string.Empty;
            string str4 = string.Empty;
            string str5 = string.Empty;
            string propertyValue = string.Empty;
            string str7 = string.Empty;
            string propertyID = string.Empty;
            NameValueCollection form = context.Request.Form;
            bus_Type_Doc_Property property = bus_Type_Doc_Property.Instance(this._objUserContext);
            //Update theo từng vòng for
            for (int i = 0; i < form.Count; i++)
            {
                str5 = "";
                propertyValue = "";
                str7 = "";
                propertyID = "";
                str5 = form.Keys[i];
                if (!string.IsNullOrEmpty(str5))
                {
                    if (str5.ToUpper() == "DOCNAME")
                    {
                        str2 = form[str5].ToString();
                    }
                    else if (str5.ToUpper() == "DOCDESCRIPTION")
                    {
                        str3 = form[str5].ToString();
                    }
                    else if (str5.ToUpper() == "DOCSTATUS")
                    {
                        str4 = form[str5].ToString();
                    }
                    else if ((str5.Length >= 40) && str5.Contains("ID"))
                    {
                        propertyValue = form[str5].ToString();
                        propertyID = this.Mid(str5, 4, 0x24).Replace("_", "-");
                        int propertyType;
                        int.TryParse(this.Mid(str5, 2, 1), out propertyType);
                        if (propertyType == 6)
                        {
                            propertyValue = propertyValue.Replace(",", "");
                        }
                        str = property.updatePropertyValue(documentid, propertyID, propertyValue, propertyType);
                        if (!string.IsNullOrEmpty(str))
                        {
                            property = null;
                            return str;
                        }
                    }
                }
            }
            bus_Document document = bus_Document.Instance(this._objUserContext);
            if (!string.IsNullOrEmpty(str2) && !string.IsNullOrEmpty(str3))
            {
                document.updateNameDescription(documentid, str2, str3);
            }
            if (!string.IsNullOrEmpty(str4))
            {
                document.updateDocumentState(documentid, int.Parse(str4));
            }
            document = null;
            return str;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
