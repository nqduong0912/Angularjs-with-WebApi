using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORE.CoreObjectContext;
using CORE.WFS.CoreBusiness;
using vpb.app.business.ktnb.CoreBusiness;
using vpb.app.business.ktnb.Definition.DMS;
using vpb.app.business.ktnb.Definition.OPERATORS;
using vpb.app.business.ktnb.Definition.UMS;

namespace VPB_KTNB.Helpers
{
    /// <summary>
    /// FormHelper
    /// </summary>
    /// <author>dungnt</author>
    /// <createddate>2008</createddate>
    /// <modifiedby></modifiedby>
    /// <modifieddate></modifieddate>
    public static class FormHelper
    {

        #region Fields
        public static Hashtable CT = new Hashtable();
        public static Page page;
        public static Control Form;
        public static string ViewID;
        public static byte ViewType;            //1: addnew; 0: See
        public static string DocSpaceID;
        public static string FolderID;
        public static string DocumentID;
        public static string DocumentName;
        public static string ProcessDefinitionID;
        public static string ProcessInstanceID;
        public static string ProcessDescription;
        public static string ActivityDefinitionID;
        public static string ActivityInstanceID;
        #endregion

        #region SaveForm
        /// <summary>
        /// SaveForm
        /// </summary>
        /// <param name="objUserContext"></param>
        /// <param name="dbName"></param>
        /// <param name="ProcessDefinitionID"></param>
        /// <param name="ActivityDefinitionID"></param>
        /// <param name="ProcessName"></param>
        /// <param name="ProcessDescription"></param>
        /// <param name="ProcessStartDate"></param>
        /// <param name="ProcessEndDate"></param>
        /// <param name="Form"></param>
        /// <returns></returns>
        public static int SaveForm(UserContext objUserContext, string dbName, string ProcessDefinitionID, string ActivityDefinitionID, string ProcessName, string ProcessDescription, string ProcessStartDate, string ProcessEndDate, NameValueCollection Form)
        {
            int err_sys = 0;
            string key = string.Empty;
            string property = string.Empty;
            string type = string.Empty;
            string value = string.Empty;

            #region Create new document

            #region Tao folder luu tru document

            string ForderID = newcheckFolder(DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), DocSpaceID, FolderID, objUserContext, dbName);
            string ArchivedPath = GetArchivedPath(ForderID, objUserContext);

            #endregion

            #region Create document

            Guid DocumentID = System.Guid.NewGuid();
            string PropertyID = string.Empty;
            string Value = string.Empty;
            string EValue = string.Empty;
            string Type = string.Empty;
            //8:text    3:datetime   6:numeric

            bus_Document objDocument = new bus_Document(objUserContext, dbName);
            DataSet ds_Document = objDocument.getEmpty(null);
            DataRow R_Document = ds_Document.Tables[0].NewRow();

            R_Document["PK_DOCUMENTID"] = DocumentID;
            R_Document["FK_DOCSPACEID"] = new Guid(DocSpaceID);
            R_Document["FK_PARENTFOLDERID"] = new Guid(ForderID);
            R_Document["FK_DOCUMENTTYPEID"] = new Guid(ViewID);
            R_Document["ARCHIVEDPATH"] = ArchivedPath;

            if (string.IsNullOrEmpty(DocumentName))
            {
                R_Document["NAME"] = "";
            }
            else
            {
                R_Document["NAME"] = string.Empty;
            }
            R_Document["DESCRIPTION"] = DBNull.Value;
            R_Document["CREATEDBY"] = objUserContext.UserName;
            R_Document["YEAR"] = DateTime.Now.Year.ToString();
            string mvarMonth = DateTime.Now.Month.ToString();
            if (mvarMonth.Length == 1) mvarMonth = "0" + mvarMonth;
            R_Document["MONTH"] = mvarMonth;

            ds_Document.Tables[0].Rows.Add(R_Document);
            err_sys = objDocument.addnewDataSet(ds_Document);
            if (err_sys != 0) return err_sys;

            #endregion

            #region Add document properties

            bus_Type_Doc_Property objDocProperty = new bus_Type_Doc_Property(objUserContext, dbName);
            DataSet ds_DocProperty = objDocProperty.getEmpty(null);

            for (int i = 0; i < Form.Count; i++)
            {
                #region loop form for saving each property

                key = Form.Keys[i];
                if ((key.Length >= 40) && (key.Contains("ID")))
                {
                    value = Form[key];
                    key = Right(key, 40);
                    property = Right(key, 36); property = property.Replace("_", "-");
                    type = Mid(key, 2, 1);

                    DataRow R = ds_DocProperty.Tables[0].NewRow();

                    R["FK_DOCUMENTID"] = DocumentID;
                    R["FK_PROPERTYID"] = new Guid(property);
                    R["PROPERTYTYPE"] = Convert.ToByte(type);

                    if (type.Equals("8"))
                    {
                        R["TEXTVALUE"] = value;
                        R["ETEXTVALUE"] = DBNull.Value;
                    }
                    else if (type.Equals("6"))
                    {
                        R["NUMERICVALUE"] = Convert.ToDouble(value);
                    }
                    else if (type.Equals("3"))
                    {
                        if (!string.IsNullOrEmpty(value))
                        {
                            string[] a = value.Split(new char[1] { '/' });
                            string d = a[0]; if (d.Length == 1) d = "0" + d;
                            string m = a[1]; if (m.Length == 1) m = "0" + m;
                            string y = a[2];
                            R["DATETIMEVALUE"] = y + "-" + m + "-" + d;
                        }
                        else
                        {
                            R["DATETIMEVALUE"] = DBNull.Value;
                        }
                    }
                    else if (type.Equals("1"))
                    {
                        if (value == "on")
                        {
                            value = "1";
                        }
                        else
                        {
                            value = "0";
                        }
                        R["NUMERICVALUE"] = Convert.ToDouble(value);
                    }
                    ds_DocProperty.Tables[0].Rows.Add(R);
                }
                #endregion
            }
            err_sys = objDocProperty.addnewDataSet(ds_DocProperty);
            if (err_sys != 0) return err_sys;

            #endregion

            #region Update permission on object
            objDocument.updatePermissionOnObject(objUserContext.UserID, TYPE_OF_APPLICANT.USER, DocumentID.ToString(), TYPE_OF_OBJECT.DOCUMENT, TYPE_OF_PERMISSION.FULL);
            
            #endregion

            #endregion

            #region Create new Process Instance
            bus_WF_Instance objProcessInstance = new bus_WF_Instance(objUserContext, dbName);
            Guid ProcessInstanceID = System.Guid.NewGuid();
            DataSet dsPI = objProcessInstance.getEmpty(string.Empty);
            DataRow rPI = dsPI.Tables[0].NewRow();

            rPI["PK_WF_InstanceID"] = ProcessInstanceID;
            rPI["Name"] = ProcessName;
            rPI["Description"] = ProcessDescription;
            rPI["FK_WF_DefinitionID"] = new Guid(ProcessDefinitionID);
            rPI["FK_CreatedByUserID"] = new Guid(objUserContext.UserID);
            rPI["FK_StartedByUserID"] = new Guid(objUserContext.UserID);
            rPI["StartedDate"] = ProcessStartDate;
            rPI["Deadline"] = ProcessEndDate;
            rPI["Status"] = 2;
            rPI["FK_ParentWFInstanceID"] = DBNull.Value;
            rPI["FK_DocumentID"] = DocumentID;
            rPI["FK_GroupID"] = DBNull.Value;
            rPI["IsRequired"] = 1;

            dsPI.Tables[0].Rows.Add(rPI);

            if (objProcessInstance.addnewDataSet(dsPI) != 0)
                return -1;

            #endregion

            #region Create pseudo instance

            bus_Pseudo_Activity_Instance objPseudoInstance = new bus_Pseudo_Activity_Instance(objUserContext, dbName);
            DataSet dsPseudoInstance = objPseudoInstance.getEmpty(string.Empty);
            DataRow rPseudoInstance = dsPseudoInstance.Tables[0].NewRow();

            Guid PseudoInstanceID = System.Guid.NewGuid();

            rPseudoInstance["PK_PseudoInstanceID"] = PseudoInstanceID;
            rPseudoInstance["FK_ActivityDefinitionID"] = new Guid(ActivityDefinitionID);
            rPseudoInstance["FK_WF_InstanceID"] = ProcessInstanceID;
            rPseudoInstance["FK_CreatedByID"] = new Guid(USERS.SYSTEM);
            
            dsPseudoInstance.Tables[0].Rows.Add(rPseudoInstance);

            if (objPseudoInstance.addnewDataSet(dsPseudoInstance) != 0)
                return -1;

            #endregion

            #region Create activity

            bus_Activity_Instance objActivityInstance = new bus_Activity_Instance(objUserContext, dbName);
            DataSet dsActivity = objActivityInstance.getEmpty(string.Empty);
            DataRow rActivity = dsActivity.Tables[0].NewRow();

            Guid PK_ActivityInstance = System.Guid.NewGuid();
            rActivity["PK_ActivityInstanceID"] = PK_ActivityInstance;
            rActivity["FK_ActivityDefinitionID"] = new Guid(ActivityDefinitionID);
            rActivity["FK_WF_InstanceID"] = ProcessInstanceID;
            rActivity["FK_PseudoActivityInstanceID"] = PseudoInstanceID;
            rActivity["FK_PerformerID"] = new Guid(objUserContext.UserID);
            rActivity["StartedDate"] = ProcessStartDate;
            rActivity["Deadline"] = ProcessEndDate;
            rActivity["Status"] = 2;
            rActivity["CreatedByUser"] = new Guid(objUserContext.UserID);

            dsActivity.Tables[0].Rows.Add(rActivity);

            err_sys = objActivityInstance.addnewDataSet(dsActivity);

            #endregion

            System.Web.HttpContext.Current.Session["DocumentID"] = DocumentID;
            System.Web.HttpContext.Current.Session["ProcessInstanceID"] = ProcessInstanceID;
            System.Web.HttpContext.Current.Session["ActivityInstanceID"] = PK_ActivityInstance;
            System.Web.HttpContext.Current.Session["ActivityInstanceStatus"] = CORE.WFS.Const.ACTIVITY_INSTANCE_STATUS_ACTIVE;
            System.Web.HttpContext.Current.Session["ViewType"] = VIEWTYPE.EDIT_ON_PROCESS;

            return err_sys;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objUserContext"></param>
        /// <param name="dbName"></param>
        /// <param name="Form"></param>
        /// <param name="ViewID"></param>
        /// <returns></returns>
        public static int SaveForm(UserContext objUserContext, NameValueCollection Form, string ViewID)
        {
            string dbName = objUserContext.DBName;
            string key = string.Empty;
            string property = string.Empty;
            string type = string.Empty;
            string value = string.Empty;

            //string vFolderID = newcheckFolder(DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), DocSpaceID, FolderID, objUserContext, dbName);
            //string ArchivedPath = GetArchivedPath(vFolderID, objUserContext);

            string ArchivedPath = "";
            string DocSpaceID = DOCSPACE.NONE;
            string vFolderID = DOCSPACE.NONE;
            
            string DocumentID = CreateDocument(objUserContext, ArchivedPath, DocSpaceID, vFolderID, ViewID, Form);

            if (string.IsNullOrEmpty(DocumentID)) return -1;

            System.Web.HttpContext.Current.Session["DocumentID"] = DocumentID;

            //if (!CreateDocumentProperties(objUserContext, DocumentID, Form)) return -1;

            return 0;
        }
        /// <summary>
        /// SaveFormAndProcess
        /// </summary>
        /// <param name="objUserContext"></param>
        /// <param name="dbName"></param>
        /// <param name="ProcessDefinitionID"></param>
        /// <param name="ActivityDefinitionID"></param>
        /// <author>dungnt</author>
        /// <createddate>20080929</createddate>
        /// <returns></returns>
        /// <modfied>
        /// 20081003 by dungnt: append ProcessName, ProcessDescription
        /// </modfied>
        public static int SaveForm(UserContext objUserContext, string dbName, string ProcessDefinitionID, string ActivityDefinitionID, string ProcessName, string ProcessDescription, string ProcessStartDate, string ProcessEndDate)
        {
            int err_sys = 0;
            string startdate = ProcessStartDate;
            string enddate = ProcessEndDate;
            string[] arr = startdate.Split(new char[1] { '/' });
            string dd = arr[0]; if (dd.Length == 1) dd = "0" + dd;
            string mm = arr[1]; if (mm.Length == 1) mm = "0" + mm;
            string yy = arr[2];
            startdate = yy + "-" + mm + "-" + dd;

            arr = enddate.Split(new char[1] { '/' });
            dd = arr[0]; if (dd.Length == 1) dd = "0" + dd;
            mm = arr[1]; if (mm.Length == 1) mm = "0" + mm;
            yy = arr[2];
            enddate = yy + "-" + mm + "-" + dd;

            #region Create new document

            int FormElementSubmit = System.Web.HttpContext.Current.Request.Form.AllKeys.Length;
            string FormElemenName;
            string FormElemenValue;

            //Tao folder luu tru document
            string ForderID = newcheckFolder(DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), DocSpaceID, FolderID, objUserContext, dbName);
            string ArchivedPath = GetArchivedPath(ForderID, objUserContext);

            //
            Guid DocumentID = System.Guid.NewGuid();
            string PropertyID = string.Empty;
            string Value = string.Empty;
            string EValue = string.Empty;
            string Type = string.Empty;
            //8:text    3:datetime   6:numeric

            //CREATE NEW DOCUMENT
            bus_Document objDocument = new bus_Document(objUserContext, dbName);
            DataSet ds_Document = objDocument.getEmpty(null);
            DataRow R_Document = ds_Document.Tables[0].NewRow();

            //DOCUMENT INFO
            R_Document["PK_DOCUMENTID"] = DocumentID;
            R_Document["FK_DOCSPACEID"] = new Guid(DocSpaceID);
            R_Document["FK_PARENTFOLDERID"] = new Guid(ForderID);
            R_Document["FK_DOCUMENTTYPEID"] = new Guid(ViewID);
            R_Document["ARCHIVEDPATH"] = ArchivedPath;

            if (string.IsNullOrEmpty(DocumentName))
            {
                R_Document["NAME"] = "";
            }
            else
            {
                TextBox ctl = (TextBox)Form.FindControl(DocumentName);
                R_Document["NAME"] = ctl.Text;
            }
            R_Document["DESCRIPTION"] = DBNull.Value;
            R_Document["CREATEDBY"] = objUserContext.UserName;
            R_Document["YEAR"] = DateTime.Now.Year.ToString();
            string mvarMonth = DateTime.Now.Month.ToString();
            if (mvarMonth.Length == 1) mvarMonth = "0" + mvarMonth;
            R_Document["MONTH"] = mvarMonth;

            ds_Document.Tables[0].Rows.Add(R_Document);
            err_sys = objDocument.addnewDataSet(ds_Document);
            if (err_sys != 0) return err_sys;

            //Add Document Property value
            //bus_Type_Doc_Property objDocProperty = new bus_Type_Doc_Property(objUserContext);
            bus_Type_Doc_Property objDocProperty = new bus_Type_Doc_Property(objUserContext, dbName);
            DataSet ds_DocProperty = objDocProperty.getEmpty(null);

            foreach (Control ctl in Form.Controls)
            {
                if (ctl.GetType() == typeof(FileUpload))
                {
                    FileUpload ctlUpload = (FileUpload)ctl;
                    if (!string.IsNullOrEmpty(ctlUpload.FileName))
                    {
                        string name = ctlUpload.FileName;
                        string displayname = name;
                        string ext = name.Substring(name.LastIndexOf('.'), 4);
                        string filename = ArchivedPath + "\\" + DocumentID.ToString() + "_01_01" + ext;
                        UploadAttachment(filename, ctlUpload);
                        CreateVersion(DocumentID, filename, displayname, objUserContext, dbName);
                    }
                }

                PropertyID = "";
                Type = "";
                Value = "";

                if ((!string.IsNullOrEmpty(ctl.ID)) && (ctl.ID.Substring(0, 2) == "ID"))
                {
                    DataRow R = ds_DocProperty.Tables[0].NewRow();
                    PropertyID = ctl.ID;
                    Type = PropertyID.Substring(2, 1);
                    PropertyID = PropertyID.Substring(4, PropertyID.Length - 4);
                    PropertyID = PropertyID.Replace("_", "-");

                    //for (int i = 0; i < FormElementSubmit; i++)
                    if (1 == 1)
                    {
                        //FormElemenName = System.Web.HttpContext.Current.Request.Form.AllKeys[i].ToString().ToUpper();
                        FormElemenName = "CTL00$FORMCONTENT$ID" + Type.ToString() + "_" + PropertyID.Replace("-", "_");
                        //if (FormElemenName.Contains(ctl.ID.ToUpper()))
                        if (1 == 1)
                        {
                            FormElemenValue = System.Web.HttpContext.Current.Request.Form[FormElemenName];
                            R["FK_DOCUMENTID"] = DocumentID;
                            R["FK_PROPERTYID"] = new Guid(PropertyID);
                            R["PROPERTYTYPE"] = Convert.ToByte(Type);
                            switch (Type)
                            {
                                case "8":
                                    R["TEXTVALUE"] = FormElemenValue;
                                    R["ETEXTVALUE"] = DBNull.Value;
                                    break;
                                case "6":
                                    R["NUMERICVALUE"] = Convert.ToDouble(FormElemenValue);
                                    break;
                                case "3":
                                    if (!string.IsNullOrEmpty(FormElemenValue))
                                    {
                                        string[] a = FormElemenValue.Split(new char[1] { '/' });
                                        string d = a[0]; if (d.Length == 1) d = "0" + d;
                                        string m = a[1]; if (m.Length == 1) m = "0" + m;
                                        string y = a[2];
                                        R["DATETIMEVALUE"] = y + "-" + m + "-" + d;
                                    }
                                    else
                                    {
                                        R["DATETIMEVALUE"] = DBNull.Value;
                                    }
                                    break;
                                case "1":
                                    if (FormElemenValue == "on")
                                    {
                                        FormElemenValue = "1";
                                    }
                                    else
                                    {
                                        FormElemenValue = "0";
                                    }
                                    R["NUMERICVALUE"] = Convert.ToDouble(FormElemenValue);
                                    break;
                            }
                            ds_DocProperty.Tables[0].Rows.Add(R);
                        }
                    }
                }
            }

            //Save doc_type_property
            err_sys = objDocProperty.addnewDataSet(ds_DocProperty);

            #endregion

            #region Update permission on object
            objDocument.updatePermissionOnObject(objUserContext.UserID, TYPE_OF_APPLICANT.USER, DocumentID.ToString(), TYPE_OF_OBJECT.DOCUMENT, TYPE_OF_PERMISSION.FULL);
            #endregion

            #region Create new Process Instance
            bus_WF_Instance objProcessInstance = new bus_WF_Instance(objUserContext, dbName);
            Guid ProcessInstanceID = System.Guid.NewGuid();
            DataSet dsPI = objProcessInstance.getEmpty(string.Empty);
            DataRow rPI = dsPI.Tables[0].NewRow();

            rPI["PK_WF_InstanceID"] = ProcessInstanceID;
            rPI["Name"] = ProcessName;
            rPI["Description"] = ProcessDescription;
            rPI["FK_WF_DefinitionID"] = new Guid(ProcessDefinitionID);
            rPI["FK_CreatedByUserID"] = new Guid(objUserContext.UserID);
            rPI["FK_StartedByUserID"] = new Guid(objUserContext.UserID);
            rPI["StartedDate"] = startdate;
            rPI["Deadline"] = enddate;
            rPI["Status"] = 2;
            rPI["FK_ParentWFInstanceID"] = DBNull.Value;
            rPI["FK_DocumentID"] = DocumentID;
            rPI["FK_GroupID"] = DBNull.Value;
            rPI["IsRequired"] = 1;

            dsPI.Tables[0].Rows.Add(rPI);

            err_sys = objProcessInstance.addnewDataSet(dsPI);

            #endregion

            #region Create pseudo instance

            bus_Pseudo_Activity_Instance objPseudoInstance = new bus_Pseudo_Activity_Instance(objUserContext, dbName);
            DataSet dsPseudoInstance = objPseudoInstance.getEmpty(string.Empty);
            DataRow rPseudoInstance = dsPseudoInstance.Tables[0].NewRow();

            Guid PseudoInstanceID = System.Guid.NewGuid();

            rPseudoInstance["PK_PseudoInstanceID"] = PseudoInstanceID;
            rPseudoInstance["FK_ActivityDefinitionID"] = new Guid(ActivityDefinitionID);
            rPseudoInstance["FK_WF_InstanceID"] = ProcessInstanceID;
            rPseudoInstance["FK_CreatedByID"] = new Guid(USERS.SYSTEM);

            dsPseudoInstance.Tables[0].Rows.Add(rPseudoInstance);

            err_sys = objPseudoInstance.addnewDataSet(dsPseudoInstance);

            #endregion

            #region Create activity

            bus_Activity_Instance objActivityInstance = new bus_Activity_Instance(objUserContext, dbName);
            DataSet dsActivity = objActivityInstance.getEmpty(string.Empty);
            DataRow rActivity = dsActivity.Tables[0].NewRow();

            Guid PK_ActivityInstance = System.Guid.NewGuid();
            rActivity["PK_ActivityInstanceID"] = PK_ActivityInstance;
            rActivity["FK_ActivityDefinitionID"] = new Guid(ActivityDefinitionID);
            rActivity["FK_WF_InstanceID"] = ProcessInstanceID;
            rActivity["FK_PseudoActivityInstanceID"] = PseudoInstanceID;
            rActivity["FK_PerformerID"] = new Guid(objUserContext.UserID);
            rActivity["StartedDate"] = startdate;
            rActivity["Deadline"] = enddate;
            rActivity["Status"] = 2;
            rActivity["CreatedByUser"] = new Guid(objUserContext.UserID);

            dsActivity.Tables[0].Rows.Add(rActivity);

            err_sys = objActivityInstance.addnewDataSet(dsActivity);

            #endregion

            System.Web.HttpContext.Current.Session["DocumentID"] = DocumentID;
            System.Web.HttpContext.Current.Session["ProcessInstanceID"] = ProcessInstanceID;
            System.Web.HttpContext.Current.Session["ActivityInstanceID"] = PK_ActivityInstance;
            System.Web.HttpContext.Current.Session["ActivityInstanceStatus"] = CORE.WFS.Const.ACTIVITY_INSTANCE_STATUS_ACTIVE;
            System.Web.HttpContext.Current.Session["ViewType"] = VIEWTYPE.EDIT_ON_PROCESS;

            return err_sys;
        }
        #endregion

        #region EditForm
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objUserContext"></param>
        /// <param name="DocumentID"></param>
        /// <param name="DocumentTypeID"></param>
        /// <param name="Form"></param>
        /// <returns></returns>
        public static int EditForm(UserContext objUserContext, string DocumentID, string DocumentTypeID, NameValueCollection Form)
        {
            //bus_Document objdoc = bus_Document.Instance(objUserContext);
            //DataSet ds = objdoc.getEmpty("PK_DOCUMENTID,BODY");
            //DataRow row = ds.Tables[0].NewRow();
            //row["PK_DOCUMENTID"] = new System.Guid(DocumentID);
            //row["BODY"] = CreateBody(Form);
            //ds.Tables[0].Rows.Add(row);
            //return objdoc.UpdateDocumentBody(ds);
            return -1;
        }
        /// <summary>
        /// EditForm
        /// </summary>
        /// <param name="objUserContext"></param>
        /// <param name="dbName"></param>
        /// <param name="DocumentID"></param>
        /// <param name="DocumentTypeID"></param>
        /// <param name="Form"></param>
        /// <returns></returns>
        public static int EditForm_Old(UserContext objUserContext, string DocumentID, string DocumentTypeID, NameValueCollection Form)
        {
            int err_sys = 0;
            string key = string.Empty;
            string property = string.Empty;
            string type = string.Empty;
            string value = string.Empty;

            string fk_propertyid;
            string fk_property_type;

            #region update all property with new value

            bus_Type_Doc_Property objTDP = bus_Type_Doc_Property.Instance(objUserContext);
            DataSet ds;
            ds = objTDP.getList(" AND FK_DOCUMENTID='" + DocumentID + "'", string.Empty);
            if (!isValidDataSet(ds)) return -1;

            foreach (DataRow R in ds.Tables[0].Rows)
            {
                fk_propertyid = R["FK_PROPERTYID"].ToString();
                fk_propertyid = fk_propertyid.Replace("-", "_").ToUpper();
                fk_property_type = R["PROPERTYTYPE"].ToString();

                #region loop all Form content for updating property value

                for (int i = 0; i < Form.Count; i++)
                {
                    key = Form.Keys[i];
                    if (key.Contains(fk_propertyid))
                    {
                        value = Form[key];
                        if (fk_property_type.Equals("8"))
                        {
                            R["TEXTVALUE"] = value;
                            break;
                        }
                        else if (fk_property_type.Equals("6"))
                        {
                            if (string.IsNullOrEmpty(value))
                                R["NUMERICVALUE"] = 0;
                            else
                                R["NUMERICVALUE"] = Convert.ToDouble(value);
                            break;
                        }
                        else if (fk_property_type.Equals("3"))
                        {
                            if (!string.IsNullOrEmpty(value))
                            {
                                string[] a = value.Split(new char[1] { '/' });
                                string d = a[0]; if (d.Length == 1) d = "0" + d;
                                string m = a[1]; if (m.Length == 1) m = "0" + m;
                                string y = a[2];
                                R["DATETIMEVALUE"] = y + "-" + m + "-" + d;
                            }
                            else
                            {
                                R["DATETIMEVALUE"] = DBNull.Value;
                            }
                            break;
                        }
                        else if (fk_property_type.Equals("1"))
                        {
                            if (string.IsNullOrEmpty(value))
                                R["NUMERICVALUE"] = 0;
                            else
                                R["NUMERICVALUE"] = 1;
                            break;
                        }
                    }
                }
                #endregion
            }


            err_sys = objTDP.saveDataSet(ds);
            return err_sys;

            #endregion
        }
        /// <summary>
        /// EditFormAndProcess
        /// </summary>
        /// <param name="objUserContext"></param>
        /// <param name="dbName"></param>
        /// <param name="DocumentID"></param>
        /// <param name="DocumentTypeID"></param>
        /// <param name="ProcessInstanceID"></param>
        /// <param name="ActivityInstanceID"></param>
        /// <author>dungnt</author>
        /// <createddate>20081003</createddate>
        /// <returns></returns>
        /// 
        public static int EditFormAndProcess(UserContext objUserContext, string DocumentID, string DocumentTypeID, string ProcessInstanceID, string ActivityInstanceID, string ProcessName, string ProcessDescription, string ProcessStartDate, string ProcessEndDate)
        {
            return -1;    
            //string dbName = objUserContext.DBName;

            //#region update document

            //int err_sys = EditForm(objUserContext, DocumentID, DocumentTypeID);
            //if (err_sys != 0) return err_sys;

            //#endregion

            //#region update process info

            //bus_WF_Instance objPI = new bus_WF_Instance(objUserContext, dbName);
            //DataSet dsPI = objPI.getByID(ProcessInstanceID, string.Empty);
            //DataRow R = dsPI.Tables[0].Rows[0];

            //string startdate = ProcessStartDate;
            //string enddate = ProcessEndDate;
            //string[] arr = startdate.Split(new char[1] { '/' });
            //string dd = arr[0]; if (dd.Length == 1) dd = "0" + dd;
            //string mm = arr[1]; if (mm.Length == 1) mm = "0" + mm;
            //string yy = arr[2];
            //startdate = yy + "-" + mm + "-" + dd;

            //arr = enddate.Split(new char[1] { '/' });
            //dd = arr[0]; if (dd.Length == 1) dd = "0" + dd;
            //mm = arr[1]; if (mm.Length == 1) mm = "0" + mm;
            //yy = arr[2];
            //enddate = yy + "-" + mm + "-" + dd;

            //R["Name"] = ProcessName;
            //R["Description"] = ProcessDescription;
            //R["StartedDate"] = startdate;
            //R["Deadline"] = enddate;

            //err_sys = objPI.saveDataSet(dsPI);

            //#endregion

            //return err_sys;
        }
        #endregion

        #region LoadForm
        /// <summary>
        /// LoadForm
        /// </summary>
        /// <param name="objUserContext"></param>
        /// <param name="dbName"></param>
        /// <param name="DocumentID"></param>
        /// <param name="DocumentTypeID"></param>
        /// <param name="Query"></param>
        /// <author>dungnt</author>
        /// <createddate>20081003</createddate>
        public static void LoadForm(UserContext objUserContext, string DocumentID, string DocumentTypeID, string Query)
        {
            //DataSet ds;
            //bus_Document objDocument = bus_Document.Instance(objUserContext);
            //if (string.IsNullOrEmpty(Query))
            //    ds = objDocument.getDocumentList(DocumentTypeID, "", " AND PK_DOCUMENTID='" + DocumentID + "'");
            //else
            //    ds = objDocument.getDocumentList(DocumentTypeID, Query, " AND PK_DOCUMENTID='" + DocumentID + "'");

            //if (!isValidDataSet(ds)) return;

            //DataRow R = ds.Tables[0].Rows[0];
            //string PropertyID = "";
            //string FormControlID;
            //string sValue;

            //foreach (Control ctl in VPB_CRM.Helper.FormHelper.Form.Controls)
            //{
            //    if ((!string.IsNullOrEmpty(ctl.ID)) && (ctl.ID.Substring(0, 2) == "ID"))
            //    {
            //        FormControlID = ctl.ID;
            //        PropertyID = FormControlID;
            //        PropertyID = PropertyID = "COL" + PropertyID.Substring(3, PropertyID.Length - 3);
            //        PropertyID = PropertyID.Replace("-", "_");

            //        try
            //        {
            //            sValue = R[PropertyID].ToString();
            //        }
            //        catch (Exception e)
            //        {
            //            sValue = string.Empty;
            //        }

            //        Control c = (Control)Form.FindControl(FormControlID);
            //        if (c != null)
            //        {
            //            if (c.GetType() == typeof(TextBox))
            //            {
            //                ((TextBox)c).Text = sValue;
            //            }
            //            else if (c.GetType() == typeof(HiddenField))
            //            {
            //                ((HiddenField)c).Value = sValue;
            //            }
            //            else if (c.GetType() == typeof(Label))
            //            {
            //                ((Label)c).Text = sValue;
            //            }
            //            else if (c.GetType() == typeof(CheckBox))
            //            {
            //                try
            //                {
            //                    if (Convert.ToByte(sValue) == 1)
            //                        ((CheckBox)c).Checked = true;
            //                }
            //                catch (Exception ex)
            //                {
            //                }
            //            }
            //            else if (c.GetType() == typeof(RadioButton))
            //            {
            //                ((RadioButton)c).Checked = true;
            //            }
            //            else if (c.GetType() == typeof(DropDownList))
            //            {
            //                if (!string.IsNullOrEmpty(sValue))
            //                    ((DropDownList)c).SelectedValue = sValue;
            //            }
            //            else if (c.GetType() == typeof(ListBox))
            //            {
            //                ((ListBox)c).SelectedIndex = 0;
            //            }
            //        }
            //    }
            //}

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objUserContext"></param>
        /// <param name="DocumentID"></param>
        /// <param name="DocumentTypeID"></param>
        /// <param name="Query"></param>
        public static void LoadDocument(string DocumentID, string DocumentTypeID, string Query,UserContext objUserContext)
        {
            //DataSet ds;
            //bus_Document objDocument = bus_Document.Instance(objUserContext);
            //if (string.IsNullOrEmpty(Query))
            //    ds = objDocument.getDocumentList(DocumentTypeID, "", " AND PK_DOCUMENTID='" + DocumentID + "'");
            //else
            //    ds = objDocument.getDocumentList(DocumentTypeID, Query, " AND PK_DOCUMENTID='" + DocumentID + "'");

            //if (!isValidDataSet(ds)) return;

            //DataRow R = ds.Tables[0].Rows[0];
            //string PropertyID = "";
            //string FormControlID;
            //string sValue;

            //foreach (Control ctl in VPB_CRM.Helper.FormHelper.Form.Controls)
            //{
            //    if ((!string.IsNullOrEmpty(ctl.ID)) && (ctl.ID.Substring(0, 2) == "ID"))
            //    {
            //        FormControlID = ctl.ID;
            //        PropertyID = FormControlID;
            //        PropertyID = PropertyID = "[" + PropertyID.Substring(3, PropertyID.Length - 3) + "]";
            //        PropertyID = PropertyID.Replace("-", "_");

            //        try
            //        {
            //            sValue = R[PropertyID].ToString();
            //        }
            //        catch (Exception e)
            //        {
            //            sValue = string.Empty;
            //        }

            //        Control c = (Control)Form.FindControl(FormControlID);
            //        if (c != null)
            //        {
            //            if (c.GetType() == typeof(TextBox))
            //            {
            //                ((TextBox)c).Text = sValue;
            //            }
            //            else if (c.GetType() == typeof(HiddenField))
            //            {
            //                ((HiddenField)c).Value = sValue;
            //            }
            //            else if (c.GetType() == typeof(Label))
            //            {
            //                ((Label)c).Text = sValue;
            //            }
            //            else if (c.GetType() == typeof(CheckBox))
            //            {
            //                try
            //                {
            //                    if (Convert.ToByte(sValue) == 1)
            //                        ((CheckBox)c).Checked = true;
            //                }
            //                catch (Exception ex)
            //                {
            //                }
            //            }
            //            else if (c.GetType() == typeof(RadioButton))
            //            {
            //                ((RadioButton)c).Checked = true;
            //            }
            //            else if (c.GetType() == typeof(DropDownList))
            //            {
            //                if (!string.IsNullOrEmpty(sValue))
            //                    ((DropDownList)c).SelectedValue = sValue;
            //            }
            //            else if (c.GetType() == typeof(ListBox))
            //            {
            //                ((ListBox)c).SelectedIndex = 0;
            //            }
            //        }
            //    }
            //}

        }
        #endregion

        #region DeleteForm
        /// <summary>
        /// DeleteForm
        /// </summary>
        /// <param name="objUserContext"></param>
        /// <param name="dbName"></param>
        /// <param name="DocumentID"></param>
        /// <returns></returns>
        public static int DeleteForm(UserContext objUserContext, string DocumentID)
        {
            bus_Document objDocunent = bus_Document.Instance(objUserContext);
            return objDocunent.deleteByID(DocumentID);
        }
        /// <summary>
        /// RemoveForm
        /// remove a document link
        /// </summary>
        /// <param name="objUserContext"></param>
        /// <param name="dbName"></param>
        /// <param name="DocumentID"></param>
        /// <param name="DocLinkID"></param>
        /// <returns></returns>
        public static int RemoveForm(UserContext objUserContext, string dbName, string DocumentID, string DocLinkID)
        {
            return -1;
        }
        #endregion

        #region Form Warning
        /// <summary>
        /// FormWarning
        /// </summary>
        /// <param name="Message"></param>
        public static void FormWarning(string Message)
        {
            StringBuilder script = new StringBuilder();
            script.Append("<table align='center' style='width:70%;' cellspacing='0' cellpading='0'>");
            script.Append("<tr id='title' style='background-color:Blue;'>");
            script.Append("<td align='left' width='95%'><font color='White' face='tahoma' size=2><b>Warning</b></font></td>");
            script.Append("<td id='close' width='5%' align='right' valign='top'></td>");
            script.Append("<tr id='bodymessage' style='background-color:Silver;'>");
            script.Append("<td align='left' colspan='2' valign-'top'>");

            script.Append("<table align='left' style='width:100%;' border='0'>");

            script.Append("<tr>");
            script.Append("<td colspan='2' style='width:100%; height:50px' id='icon' width='100%' align='left' valign='bottom'></td>");
            script.Append("</tr>");

            script.Append("<tr style='width:100%; height:70px'>");
            script.Append("<td id='icon' width='10%'></td>");
            script.Append("<td id='mess' width='90%' align='left' valign='top'>");
            script.Append("<font face='Tahoma' size='2'>" + Message + "</font>");
            script.Append("</td>");
            script.Append("</tr>");
            script.Append("</table>");

            script.Append("</td>");
            script.Append("<tr>");
            script.Append("</table>");
            System.Web.HttpContext.Current.Response.Write(script.ToString());
            System.Web.HttpContext.Current.Response.Flush();
            System.Web.HttpContext.Current.Response.End();
        }
        /// <summary>
        /// FormWarning
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="Message"></param>
        /// <param name="MessageColor"></param>
        public static void FormWarning(string Title, string Message, string MessColor)
        {
            string url = "~/modules/dialog/message.aspx?";
            url += "t=" + Title;
            url += "&m=" + Message;
            System.Web.HttpContext.Current.Server.Transfer(url, true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="Message"></param>
        public static void FormPopupWarning(string Title, string Message)
        {
            string url = "~/Modules/Dialog/PopUp.aspx?tite=" + Title + "&message=" + Message;
            PopUp(url);
        }
        public static void Exports(C1.Web.C1WebGrid.C1WebGrid myDataGrid, string filename)
        {
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + filename);
            System.Web.HttpContext.Current.Response.Charset = "";
            System.Web.HttpContext.Current.Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            System.Web.HttpContext.Current.Response.ContentType = "application/vnd.xls";
            StringWriter stringWrite = new System.IO.StringWriter();
            HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            myDataGrid.RenderControl(htmlWrite);
            System.Web.HttpContext.Current.Response.Write(stringWrite.ToString());
            System.Web.HttpContext.Current.Response.Flush();
            System.Web.HttpContext.Current.Response.End();
        }
        #endregion

        #region Helper methods

        /// <summary>
        /// UploadAttachment
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="ctlUpload"></param>
        public static void UploadAttachment(string filename, FileUpload ctlUpload)
        {
            if (ctlUpload.PostedFile != null)
                ctlUpload.PostedFile.SaveAs(filename);
        }
        /// <summary>
        /// CreateVersion
        /// </summary>
        /// <param name="DocumentID"></param>
        private static void CreateVersion(Guid DocumentID, string filename, string displayname, UserContext objUserContext, string dbName)
        {
            #region add doc version

            bus_Doc_Version objVer = new bus_Doc_Version(objUserContext, dbName);
            DataSet ds = objVer.getEmpty(string.Empty);
            DataRow R = ds.Tables[0].NewRow();
            Guid DocversionID = System.Guid.NewGuid();

            R["PK_VersionID"] = DocversionID;
            R["FK_DocumentID"] = DocumentID;
            R["Comment"] = DBNull.Value;
            R["VersionNumber"] = getNextVersion(DocumentID, objUserContext, dbName);
            R["CreatedBy"] = new Guid(objUserContext.UserID);
            R["FileName"] = DBNull.Value;

            ds.Tables[0].Rows.Add(R);

            int err_sys = objVer.addnewDataSet(ds);

            if (err_sys != 0) return;

            #endregion

            #region add version body

            string fname;
            string fpath;
            string[] a = filename.Split(new char[1] { '\\' });
            fname = a[a.Length - 1].ToString();
            fpath = filename.Replace(fname, "");

            bus_Doc_Version_Body objBody = new bus_Doc_Version_Body(objUserContext, dbName);
            DataSet dsBody = objBody.getEmpty(string.Empty);
            DataRow rBody = dsBody.Tables[0].NewRow();

            Guid BodyID = Guid.NewGuid();

            rBody["PK_DocversionbodyID"] = BodyID;
            rBody["FK_DocversionID"] = DocversionID;
            rBody["FK_DocumentID"] = DocumentID;
            rBody["NumberBody"] = 1;
            rBody["FileName"] = fname;
            rBody["FilePath"] = fpath;
            rBody["Comment"] = DBNull.Value;
            rBody["isCheckOut"] = 0;
            rBody["DisplayFileName"] = displayname;
            rBody["ISLOCK"] = DBNull.Value;
            rBody["CheckingOutByUser"] = DBNull.Value;

            dsBody.Tables[0].Rows.Add(rBody);

            err_sys = objBody.addnewDataSet(dsBody);

            #endregion
        }
        /// <summary>
        /// getNextVersion
        /// </summary>
        /// <param name="DocumentID"></param>
        /// <returns></returns>
        private static int getNextVersion(Guid DocumentID, UserContext objUserContext, string dbName)
        {
            bus_Doc_Version objVer = new bus_Doc_Version(objUserContext, dbName);
            DataSet ds = objVer.getList(" AND FK_DocumentID='" + DocumentID.ToString() + "' ORDER BY VersionNumber DESC", " TOP 1 VersionNumber");
            if (!isValidDataSet(ds))
                return 1;
            else
                return Convert.ToInt32(ds.Tables[0].Rows[0]["VersionNumber"]);
        }
        /// <summary>
        /// newcheckFolder
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <param name="DocspaceID"></param>
        /// <param name="DoctypeID"></param>
        /// <param name="objUserContext"></param>
        /// <param name="DataBaseName"></param>
        /// <returns></returns>
        private static string newcheckFolder(string Year, string Month, string DocspaceID, string DoctypeID, UserContext objUserContext, string DataBaseName)
        {
            bus_Document objFolder = new bus_Document(objUserContext, DataBaseName);
            string sFolder = objFolder.newcheckFolder(Year, Month, DocspaceID, DoctypeID);
            return sFolder;
        }
        /// <summary>
        /// GetArchivedPath
        /// </summary>
        /// <param name="DocumentID"></param>
        /// <param name="objUserContext"></param>
        /// <returns></returns>
        private static string GetArchivedPath(string DocumentID, UserContext objUserContext)
        {
            try
            {
                bus_Document objDocument = new bus_Document(objUserContext);
                return objDocument.getByID(DocumentID, "ArchivedPath").Tables[0].Rows[0]["ArchivedPath"].ToString() + "\\" + DocumentID;
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// ControlCanbeEdit
        /// </summary>
        /// <param name="PropertyID"></param>
        /// <param name="PropertyType"></param>
        /// <returns></returns>
        /// <author>dungnt</author>
        /// <createddate>20090104</createddate>
        private static bool ControlCanbeEdit(string PropertyID, string PropertyType)
        {
            string ctl_id = PropertyID.Replace("-", "_");
            ctl_id = "ID" + PropertyType + "_" + ctl_id;

            Control ctl = Form.FindControl(ctl_id) as Control;

            if (ctl.GetType() == typeof(TextBox))
                if (!((TextBox)ctl).Enabled) return false;

            if (ctl.GetType() == typeof(DropDownList))
                if (!((DropDownList)ctl).Enabled) return false;

            if (ctl.GetType() == typeof(ListBox))
                if (!((ListBox)ctl).Enabled) return false;

            if (ctl.GetType() == typeof(CheckBox))
                if (!((CheckBox)ctl).Enabled) return false;

            return true;
        }
        /// <summary>
        /// GetArchivedPathOfDocument
        /// </summary>
        /// <param name="DocumentID"></param>
        /// <param name="objUserContext"></param>
        /// <returns></returns>
        private static string GetArchivedPathOfDocument(string DocumentID, UserContext objUserContext)
        {
            try
            {
                bus_Document objDocument = new bus_Document(objUserContext);
                return objDocument.getByID(DocumentID, "ArchivedPath").Tables[0].Rows[0]["ArchivedPath"].ToString();
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// Traceln
        /// </summary>
        /// <param name="Message"></param>
        private static void Traceln(string Message)
        {
            System.Web.HttpContext.Current.Response.Write("<br/>" + Message);
        }
        /// <summary>
        /// Pause
        /// </summary>
        private static void Pause()
        {
            System.Web.HttpContext.Current.Response.End();
        }
        /// <summary>
        /// isValidDataSet
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        private static bool isValidDataSet(DataSet ds)
        {
            if ((ds == null) || (ds.Tables[0].Rows.Count == 0))
                return false;
            else
                return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private static string Left(string param, int length)
        {
            //we start at 0 since we want to get the characters starting from the
            //left and with the specified lenght and assign it to a variable
            string result = param.Substring(0, length);
            //return the result of the operation
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private static string Right(string param, int length)
        {
            //start at the index based on the lenght of the sting minus
            //the specified lenght and assign it a variable
            string result = param.Substring(param.Length - length, length);
            //return the result of the operation
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private static string Mid(string param, int startIndex, int length)
        {
            //start at the specified index in the string ang get N number of
            //characters depending on the lenght and assign it to a variable
            string result = param.Substring(startIndex, length);
            //return the result of the operation
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        private static string Mid(string param, int startIndex)
        {
            //start at the specified index and return all characters after it
            //and assign it to a variable
            string result = param.Substring(startIndex);
            //return the result of the operation
            return result;
        }
        /// <summary>
        /// GetPropertyValue_ByDocument
        /// </summary>
        /// <param name="DocumentID"></param>
        /// <param name="PropertyID"></param>
        /// <param name="objUserContext"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public static string GetPropertyValue_ByDocument(string DocumentID, string PropertyID, UserContext objUserContext, string dbName)
        {
            return string.Empty;
            //try
            //{                
            //    string COL_Property = "COL_"+ PropertyID.Replace("-","_");
            //    string Condition = " WHERE PK_DocumentID ='" + DocumentID + "'";
            //    bus_Document objDoc = new bus_Document(objUserContext, dbName);
            //    List<Para> IntParameter = new List<Para>();
            //    IntParameter.Add(new Para("@PK_DocumentID", DbType.String, DocumentID));
            //    IntParameter.Add(new Para("@PK_PropertyID", DbType.String, PropertyID));
            //    IntParameter.Add(new Para("@Condition", DbType.String, Condition));
            //    DataSet dsPropertyValue = objDoc.getList(IntParameter, "DMS_GetPropertyValue_ByDocumentID");
            //    return dsPropertyValue.Tables[0].Rows[0][COL_Property].ToString(); 
            //}
            //catch
            //{
            //    return "";
            //}
        }
        /// <summary>
        /// Create a new document
        /// </summary>
        /// <param name="objUserContext"></param>
        /// <param name="ArchivedPath"></param>
        /// <param name="DocSpaceID"></param>
        /// <param name="ForderID"></param>
        /// <param name="ViewID"></param>
        /// <param name="Form"></param>
        /// <returns></returns>
        private static string CreateDocument(UserContext objUserContext, string ArchivedPath, string DocSpaceID, string ForderID, string ViewID, NameValueCollection Form)
        {
            Guid DocumentID = System.Guid.NewGuid();
            bus_Document objDocument = bus_Document.Instance(objUserContext);
            DataSet ds_Document = objDocument.getEmpty("");
            DataRow R_Document = ds_Document.Tables[0].NewRow();

            R_Document["PK_DOCUMENTID"] = DocumentID;
            R_Document["FK_DOCSPACEID"] = new Guid(DocSpaceID);
            R_Document["FK_PARENTFOLDERID"] = new Guid(DocSpaceID);
            R_Document["FK_DOCUMENTTYPEID"] = new Guid(ViewID);
            R_Document["ARCHIVEDPATH"] = ArchivedPath;
            R_Document["NAME"] = DBNull.Value;
            R_Document["DESCRIPTION"] = DBNull.Value;
            R_Document["Body"] = CreateBody(Form);
            R_Document["CREATEDBY"] = objUserContext.UserName;
            R_Document["YEAR"] = DateTime.Now.Year;
            R_Document["MONTH"] = DateTime.Now.Month;
            R_Document["FK_GroupID"] = new Guid(((Group)objUserContext.Groups[0]).GroupID);

            ds_Document.Tables[0].Rows.Add(R_Document);

            //if (objDocument.addnewDataSet(ds_Document) == 0)
            //if (objDocument.CreateDocument(ds_Document) == 0)
            //{
            //    //objDocument.updatePermissionOnObject(objUserContext.UserID, TYPE_OF_APPLICANT.USER, DocumentID.ToString(), TYPE_OF_OBJECT.DOCUMENT, TYPE_OF_PERMISSION.FULL);
            //    return DocumentID.ToString();
            //}
            return string.Empty;
        }
        /// <summary>
        /// CreateDocumentProperties
        /// </summary>
        /// <param name="objUserContext"></param>
        /// <param name="dbName"></param>
        /// <param name="DocumentID"></param>
        /// <param name="Form"></param>
        /// <returns></returns>
        private static bool CreateDocumentProperties(UserContext objUserContext, string DocumentID, NameValueCollection Form)
        {
            string dbName = objUserContext.DBName;

            bus_Type_Doc_Property objDocProperty = new bus_Type_Doc_Property(objUserContext, dbName);
            //bus_Type_Doc_Property objDocProperty = bus_Type_Doc_Property.Instance(objUserContext);
            DataSet ds_DocProperty = objDocProperty.getEmpty(string.Empty);
            string key = string.Empty;
            string property = string.Empty;
            string type = string.Empty;
            string value = string.Empty;

            for (int i = 0; i < Form.Count; i++)
            {
                key = Form.Keys[i];
                if (string.IsNullOrEmpty(key)) continue;

                // for file attachment as upload
                if (key.Equals("FileAttachment"))
                    if (!string.IsNullOrEmpty(Form[key])) value = Form[key].ToString();

                if ((key.Length >= 40) && (key.Contains("ID")))
                {
                    value = Form[key].ToString();
                    property = Right(key, 36); property = property.Replace("_", "-");
                    type = Mid(key, 2, 1);

                    DataRow R = ds_DocProperty.Tables[0].NewRow();

                    R["FK_DOCUMENTID"] = DocumentID;
                    R["FK_PROPERTYID"] = new Guid(property);
                    R["PROPERTYTYPE"] = Convert.ToByte(type);

                    if (type.Equals("8"))
                    {
                        R["TEXTVALUE"] = value;
                        R["ETEXTVALUE"] = DBNull.Value;
                    }
                    else if (type.Equals("6"))
                    {
                        //string decimalsymbol = System.Configuration.ConfigurationManager.AppSettings["DecimalSymbol"].ToString();
                        if (!string.IsNullOrEmpty(value))
                        {
                            //if (value.IndexOf('.') != -1)
                            //    if (!string.IsNullOrEmpty(decimalsymbol))
                            //        value = value.Replace(".", decimalsymbol);
                            R["NUMERICVALUE"] = Convert.ToDouble(value);
                        }
                        else
                            R["NUMERICVALUE"] = DBNull.Value;
                    }
                    else if (type.Equals("3"))
                    {
                        if (!string.IsNullOrEmpty(value))
                        {
                            string[] a = value.Split(new char[1] { '/' });
                            string d = a[0]; if (d.Length == 1) d = "0" + d;
                            string m = a[1]; if (m.Length == 1) m = "0" + m;
                            string y = a[2];
                            R["DATETIMEVALUE"] = y + "-" + m + "-" + d;
                        }
                        else
                        {
                            R["DATETIMEVALUE"] = DBNull.Value;
                        }
                    }
                    else if (type.Equals("1"))
                    {
                        if (value == "on")
                        {
                            value = "1";
                        }
                        else
                        {
                            value = "0";
                        }
                        R["NUMERICVALUE"] = Convert.ToDouble(value);
                    }
                    ds_DocProperty.Tables[0].Rows.Add(R);
                }
            }
            return (objDocProperty.addnewDataSet(ds_DocProperty) == 0 ? true : false);
        }
        /// <summary>
        /// CreateBody
        /// </summary>
        /// <param name="Form"></param>
        /// <returns></returns>
        private static string CreateBody(NameValueCollection Form)
        {
            string key = string.Empty;
            string property = string.Empty;
            string type = string.Empty;
            string value = string.Empty;

            StringBuilder Properties = new StringBuilder();
            Properties.Append("<properties>");
            string aProperty;

            for (int i = 0; i < Form.Count; i++)
            {
                key = Form.Keys[i];
                if (string.IsNullOrEmpty(key)) continue;

                //// for file attachment as upload
                //if (key.Equals("FileAttachment"))
                //    if (!string.IsNullOrEmpty(Form[key])) value = Form[key].ToString();

                if ((key.Length >= 40) && (key.Contains("ID")))
                {
                    aProperty = "";

                    value = Form[key].ToString();
                    property = Right(key, 36); property = property.Replace("_", "-");
                    type = Mid(key, 2, 1);

                    aProperty = "<id>" + property + "</id>";
                    aProperty += "<type>" + type + "</type>";
                    aProperty += "<value>" + value + "</value>";

                    aProperty = "<property>" + aProperty + "</property>";

                    Properties.Append(aProperty);
                }
            }
            Properties.Append("</properties>");
            return Properties.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        private static void PopUp(string url)
        {
            string features = "dialogWidth:300px; dialogHeight:201px; center:yes";
            StringBuilder script = new StringBuilder();
            script.Append("<script type=\"text/javascript\">");
            script.Append("window.showModalDialog('" + url + "','popupmessdialog','"+ features + "');");
            script.Append("</script>");
            System.Web.HttpContext.Current.Response.Write(script.ToString());
        }
        #endregion

    }
}