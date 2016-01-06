using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using C1.Web.C1Input;
using CORE.CoreObjectContext;
using CORE.UMS.CoreBusiness;
using CORE.WFS.CoreBusiness;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;
using KTNB.Extended.Biz;
using KTNB.Extended.Commons;
using KTNB.Extended.Dal;
using vpb.app.business.ktnb.CoreBusiness;
using vpb.app.business.ktnb.Definition.DMS;
using vpb.app.business.ktnb.Definition.OPERATORS;

namespace VPB_KTNB.Helpers
{
    /// <summary>
    /// Summary description for Common
    /// </summary>
    public static class CommonFunc
    {
        #region for user & role & group

        /// <summary>
        /// getUserInfo
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="objUserContext"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        /// <author>dungnt</author>
        /// <createddate>2008</createddate>
        public static DataSet getUserInfo(string UserID, UserContext objUserContext, string dbName)
        {
            bus_User objUser = bus_User.Instance(objUserContext);
            return objUser.getByID(UserID, string.Empty);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="Query"></param>
        /// <returns></returns>
        public static DataSet getUserInfo(string Condition, string Query)
        {
            bus_User objUser = bus_User.Instance((UserContext)HttpContext.Current.Session["objUserContext"]);
            return objUser.getListOnView(Condition, Query);
        }

        /// <summary>
        /// getRoleInfo
        /// </summary>
        /// <param name="RoleID"></param>
        /// <param name="objUserContext"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        /// <author>dungnt</author>
        /// <createddate>2008</createddate>
        public static DataSet getRoleInfo(string RoleID, UserContext objUserContext, string dbName)
        {
            bus_Role objRole = bus_Role.Instance(objUserContext);
            return objRole.getByID(RoleID, string.Empty);
        }

        /// <summary>
        /// getMNEMONIC
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="objUserContext"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public static string getMNEMONIC(string Name, CORE.CoreObjectContext.UserContext objUserContext, string dbName)
        {
            string condition = " AND Name='" + Name + "'";
            string query = "MNEMONIC";
            CORE.UMS.CoreBusiness.bus_Group objgrp = bus_Group.Instance(objUserContext); //CORE.UMS.CoreBusiness.bus_Group objgrp = new bus_Group(objUserContext,dbName);
            DataSet ds = objgrp.getList(condition, query);
            objgrp = null;
            if (ds == null) return string.Empty;
            if (ds.Tables[0].Rows.Count == 0) return string.Empty;
            return ds.Tables[0].Rows[0]["MNEMONIC"].ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="objUserContext"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public static string getGroupDescription(string Name, CORE.CoreObjectContext.UserContext objUserContext, string dbName)
        {
            string condition = " AND Name='" + Name + "'";
            string query = "DESCRIPTION";
            CORE.UMS.CoreBusiness.bus_Group objgrp = bus_Group.Instance(objUserContext);
            DataSet ds = objgrp.getList(condition, query);
            objgrp = null;
            if (ds == null) return string.Empty;
            if (ds.Tables[0].Rows.Count == 0) return string.Empty;
            return ds.Tables[0].Rows[0]["DESCRIPTION"].ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="groupID"></param>
        /// <param name="objUserContext"></param>
        /// <returns></returns>
        public static int getGroupType(string groupID, CORE.CoreObjectContext.UserContext objUserContext)
        {
            CORE.UMS.CoreBusiness.bus_Group objgrp = bus_Group.Instance(objUserContext); //CORE.UMS.CoreBusiness.bus_Group objgrp = new bus_Group(objUserContext);
            DataSet ds = objgrp.getByID(groupID, "Type");
            objgrp = null;
            if ((ds == null) || (ds.Tables[0].Rows.Count == 0)) return -1;
            return Convert.ToInt32(ds.Tables[0].Rows[0]["Type"]);
        }

        /// <summary>
        /// GetParentGroupID
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public static string getParentGroupID(string groupID, CORE.CoreObjectContext.UserContext objUserContext, string dbName)
        {
            bus_Group obj = bus_Group.Instance(objUserContext);
            DataSet ds = obj.getByID(groupID, "FK_ParentGroupID");
            obj = null;
            if (ds == null) return string.Empty;
            if (ds.Tables[0].Rows.Count == 0) return string.Empty;
            return ds.Tables[0].Rows[0]["FK_ParentGroupID"].ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public static string getParentGroupID(string groupID)
        {
            bus_Group obj = bus_Group.Instance((UserContext)HttpContext.Current.Session["objUserContext"]);
            DataSet ds = obj.getByID(groupID, "FK_ParentGroupID");
            obj = null;
            if (ds == null) return string.Empty;
            if (ds.Tables[0].Rows.Count == 0) return string.Empty;
            return ds.Tables[0].Rows[0]["FK_ParentGroupID"].ToString();
        }

        /// <summary>
        /// getCompanyInfo
        /// </summary>
        /// <param name="groupid"></param>
        /// <param name="objUserContext"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public static string getCompanyInfo(string groupid, CORE.CoreObjectContext.UserContext objUserContext, string dbName)
        {
            bus_Group obj = bus_Group.Instance(objUserContext); //bus_Group obj = new bus_Group(objUserContext, dbName);
            DataSet ds = obj.getByID(groupid, "Name, Description");
            obj = null;
            if (ds == null) return string.Empty;
            if (ds.Tables[0].Rows.Count == 0) return string.Empty;
            return ds.Tables[0].Rows[0]["Name"].ToString() + "....." + ds.Tables[0].Rows[0]["Description"].ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="groupid"></param>
        /// <returns></returns>
        public static string getCompanyInfo(string groupid)
        {
            bus_Group obj = bus_Group.Instance((UserContext)HttpContext.Current.Session["objUserContext"]);
            DataSet ds = obj.getByID(groupid, "Name, Description");
            obj = null;
            if (ds == null) return string.Empty;
            if (ds.Tables[0].Rows.Count == 0) return string.Empty;
            return ds.Tables[0].Rows[0]["Name"].ToString() + "....." + ds.Tables[0].Rows[0]["Description"].ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="Company"></param>
        /// <param name="Description"></param>
        /// <returns></returns>
        public static void getCompanyInfo(string Company, out string Description)
        {
            bus_Group obj = bus_Group.Instance((UserContext)HttpContext.Current.Session["objUserContext"]);
            DataRow R = obj.getList(" AND Name='" + Company + "'", "Description").Tables[0].Rows[0];
            Description = R["Description"].ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="Company">VN0010101</param>
        /// <returns></returns>
        public static byte getCompanyType(string Company)
        {
            bus_Group objgr = bus_Group.Instance((UserContext)HttpContext.Current.Session["objUserContext"]);
            string Query = "Type";
            string Condition = " AND Name='" + Company + "'";
            DataSet ds = objgr.getList(Condition, Query);
            return Convert.ToByte(ds.Tables[0].Rows[0]["Type"]);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cbo"></param>
        /// <param name="RoleID"></param>
        public static void BindUserInRoleToCombo(DropDownList cbo, string RoleID)
        {
            bus_User obj = bus_User.Instance((UserContext)HttpContext.Current.Session["objUserContext"]);
            DataSet ds = obj.GetUsersInRole(RoleID, "Name,FullName=FullName + ' (' + Name + ')'");
            if (isValidDataSet(ds))
            {
                cbo.DataSource = ds.Tables[0];
                cbo.DataTextField = "FullName";
                cbo.DataValueField = "Name";
                cbo.DataBind();
                cbo.Items.Insert(0, new ListItem(string.Empty, string.Empty));
            }
            obj = null;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cbo"></param>
        public static void BindRoleToDoanKiemToan(DropDownList cbo)
        {
            cbo.Items.Add(new ListItem("Trưởng đoàn", "Trưởng đoàn"));
            cbo.Items.Add(new ListItem("Thành viên", "Thành viên"));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cbo"></param>
        public static void BindRoleToNhomKiemToan(DropDownList cbo)
        {
            cbo.Items.Add(new ListItem("Trưởng nhóm", "Trưởng nhóm"));
            cbo.Items.Add(new ListItem("Thành viên", "Thành viên"));
        }

        #endregion for user & role & group

        #region for document

        /// <summary>
        ///
        /// </summary>
        /// <param name="DocumentID"></param>
        /// <returns></returns>
        /// <author>thangma</author>
        public static int GetDocStatus(string DocumentID)
        {
            int iResult = 0;
            bus_Document obj = bus_Document.Instance((UserContext)HttpContext.Current.Session["objUserContext"]);
            DataSet ds = obj.getByID(DocumentID, "");
            if (isValidDataSet(ds))
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    iResult = Convert.ToInt32(ds.Tables[0].Rows[0]["STATUS"].ToString());
                }
            }
            return iResult;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="DocumentID"></param>
        /// <returns></returns>
        /// <author>thangma</author>
        public static string GetDocType(string DocumentID)
        {
            bus_Document obj = bus_Document.Instance((UserContext)HttpContext.Current.Session["objUserContext"]);
            DataSet ds = obj.getByID(DocumentID, "FK_DOCUMENTTYPEID");
            string sDocTypeID = string.Empty;
            if (isValidDataSet(ds))
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    sDocTypeID = ds.Tables[0].Rows[0]["FK_DOCUMENTTYPEID"].ToString();
                }
            }
            return sDocTypeID;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="DocumentID"></param>
        /// <returns></returns>
        public static bool IsSubmitted(string DocumentID)
        {
            bool bResult = false;
            bus_Document obj = bus_Document.Instance((UserContext)HttpContext.Current.Session["objUserContext"]);
            DataSet ds = obj.getByID(DocumentID, "STATUS");
            if (isValidDataSet(ds))
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    int iResult = Convert.ToInt32(ds.Tables[0].Rows[0]["STATUS"].ToString());
                    if (iResult == 4)
                        bResult = true;
                    else
                        bResult = false;
                }
            }
            return bResult;
        }

        /// <summary>
        /// check xem da submitted
        /// </summary>
        /// <param name="DocumentID"></param>
        /// <returns></returns>
        /// <author>quangna</author>
        public static bool IsSubmitted_CV(string DocumentID)
        {
            bool bResult = false;
            bus_Document obj = bus_Document.Instance((UserContext)HttpContext.Current.Session["objUserContext"]);
            DataSet ds = obj.getByID(DocumentID, "");
            if (isValidDataSet(ds))
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    int iResult = Convert.ToInt32(ds.Tables[0].Rows[0]["STATUS"].ToString());
                    if (iResult >= 2)
                        bResult = true;
                    else if (iResult == 0)
                        bResult = false;
                }
            }
            return bResult;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="PropertyID"></param>
        /// <returns></returns>
        /// <author>thangma</author>
        public static string GetDataType(string PropertyID)
        {
            bus_Property p = bus_Property.Instance((UserContext)HttpContext.Current.Session["objUserContext"]);
            DataSet ds = p.getByID(PropertyID, "Type");
            p = null;
            if (isValidDataSet(ds))
                return ds.Tables[0].Rows[0][0].ToString();
            return "0";
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="DocumentID"></param>
        /// <param name="PropertyID"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static string UpdateDocPropertyValue(string DocumentID, string PropertyID, string Value)
        {
            bus_Type_Doc_Property objTDP = bus_Type_Doc_Property.Instance((UserContext)HttpContext.Current.Session["objUserContext"]);
            string ErrorMessage = objTDP.updatePropertyValue(DocumentID, PropertyID, Value);
            objTDP = null;
            return ErrorMessage;
        }

        /// <summary>
        /// Chuyển trạng thái đợt kiểm toán
        /// </summary>
        /// <param name="dotkt"></param>
        /// <param name="trangthai"></param>
        /// <returns></returns>
        /// <author>thangma</author>
        public static string ChuyenTrangThaiDotKT(string dotkt, int trangthai)
        {
            //thangma: update Trang thai Dot kiem toan
            int _status_dotkt = CommonFunc.GetDocStatus(dotkt);
            if (_status_dotkt < trangthai)
            {
                bool IsTrangThaiDacBiet = Enum.IsDefined(typeof(TrangThaiDacBiet), _status_dotkt);
                if (!IsTrangThaiDacBiet)
                {
                    CommonFunc.UpdateDocStatus(dotkt, trangthai);
                    return "1";
                }
                else
                    return "0";
            }
            return "0";
            //end
        }

        private static readonly string[,] MaTranRuiRoConLai = new string[5, 5] {
                {"C", "B", "B", "A", "A"},
                {"C", "C", "B", "B", "A"},
                {"C", "C", "C" ,"B", "B"},
                {"D", "C", "C" ,"C", "B"},
                {"D", "D", "C" ,"C", "C"},
            };

        /// <summary>
        /// lay diem rui ro con lai
        /// </summary>
        /// <param name="RRCH"></param>
        /// <param name="DiemKiemSoat"></param>
        /// <returns></returns>
        /// thangma
        public static string GetRuiRoConLai(int RRCH, int DiemKiemSoat)
        {
            int range = CalculateRRCoHuuRange(RRCH);
            return MaTranRuiRoConLai[range, DiemKiemSoat - 1];
        }

        /// <summary>
        /// Tính khoảng điểm rủi ro còn lại
        /// </summary>
        /// <param name="iRRCoHuu"></param>
        /// <returns></returns>
        /// <author>thangma</author>
        public static int CalculateRRCoHuuRange(int iRRCoHuu)
        {
            if ((iRRCoHuu >= 21) && (iRRCoHuu <= 25))
                return 0;
            else if ((iRRCoHuu >= 16) && (iRRCoHuu <= 20))
                return 1;
            else if ((iRRCoHuu >= 11) && (iRRCoHuu <= 15))
                return 2;
            else if ((iRRCoHuu >= 6) && (iRRCoHuu <= 10))
                return 3;
            else if ((iRRCoHuu >= 1) && (iRRCoHuu <= 5))
                return 4;
            return 0;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="DocumentID"></param>
        /// <param name="Status"></param>
        public static void UpdateDocStatus(string DocumentID, int Status)
        {
            bus_Document obj = bus_Document.Instance((UserContext)HttpContext.Current.Session["objUserContext"]);
            obj.updateDocumentState(DocumentID, Status);
            obj = null;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="DocumentID"></param>
        /// <param name="Status"></param>
        public static void UpdateDocNameDescription(string DocumentID, string name, string description)
        {
            bus_Document obj = bus_Document.Instance((UserContext)HttpContext.Current.Session["objUserContext"]);
            obj.updateNameDescription(DocumentID, name, description);
            obj = null;
        }

        /// <summary>
        /// hasBody
        /// </summary>
        /// <param name="DocumentID"></param>
        /// <returns></returns>
        /// <author>dungnt</author>
        /// <createddate>2008</createddate>
        public static bool hasBody(string DocumentID, UserContext objUserContext, string dbName)
        {
            bus_Doc_Version_Body objBody = bus_Doc_Version_Body.Instance(objUserContext); //bus_Doc_Version_Body objBody = new bus_Doc_Version_Body(objUserContext, dbName);
            DataSet dsBody = objBody.getList(" AND FK_DocumentID='" + DocumentID + "'", "FK_DocumentID");
            //System.Web.HttpContext.Current.Response.Write();
            if ((dsBody != null) && (dsBody.Tables[0].Rows.Count > 0))
                return true;
            else
                return false;
        }

        /// <summary>
        /// OpenAttachment
        /// </summary>
        /// <param name="DocumentID"></param>
        /// <param name="objUserContext"></param>
        /// <param name="dbName"></param>
        /// <author>dungnt</author>
        /// <createddate>2008</createddate>
        public static void OpenAttachment(string DocumentID, UserContext objUserContext, string dbName)
        {
            string VersionID = getLastestVersion(DocumentID, objUserContext, dbName);
            if (string.IsNullOrEmpty(VersionID)) return;

            bus_Doc_Version_Body objBody = bus_Doc_Version_Body.Instance(objUserContext);  //bus_Doc_Version_Body objBody = new bus_Doc_Version_Body(objUserContext, dbName);
            DataSet dsBody = objBody.getList(" AND FK_DocVersionID='" + VersionID + "' AND FK_DocumentID='" + DocumentID + "'", "FileName,FilePath,DisplayFileName");

            if ((dsBody == null) || (dsBody.Tables[0].Rows.Count == 0)) return;

            DataRow R = dsBody.Tables[0].Rows[0];
            string filename = R["FilePath"].ToString() + R["FileName"].ToString();

            string url = "http://localhost/BODY_TIMESHEET/f14ef21e-690d-4ab3-8894-b4dab0248819/10be0a3b-0a00-4608-a28b-296f4bd722ef/adb23696-f1d9-4a33-b980-44802ff6d6c4/4347e9b5-619a-4da5-b766-797eebf5d5d1_01_01.JPG";
            openUrl(url, "_blank");
        }

        /// <summary>
        /// getFileAttachment
        /// </summary>
        /// <param name="DocumentID"></param>
        /// <param name="objUserContext"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        /// <author>dungnt</author>
        /// <createddate>2008</createddate>
        public static string getFileAttachment(string DocumentID, UserContext objUserContext, string dbName)
        {
            string VersionID = getLastestVersion(DocumentID, objUserContext, dbName);
            if (string.IsNullOrEmpty(VersionID)) return string.Empty;

            bus_Doc_Version_Body objBody = bus_Doc_Version_Body.Instance(objUserContext);  //bus_Doc_Version_Body objBody = new bus_Doc_Version_Body(objUserContext, dbName);
            DataSet dsBody = objBody.getList(" AND FK_DocVersionID='" + VersionID + "' AND FK_DocumentID='" + DocumentID + "'", "FileName,FilePath,DisplayFileName");

            if ((dsBody == null) || (dsBody.Tables[0].Rows.Count == 0)) return string.Empty;

            DataRow R = dsBody.Tables[0].Rows[0];
            string filename = R["FilePath"].ToString() + R["FileName"].ToString();

            bus_Project_Parameter objParam = bus_Project_Parameter.Instance(objUserContext);  //bus_Project_Parameter objParam = new bus_Project_Parameter(objUserContext, dbName);
            DataSet ds = objParam.getList(" AND Name='BODY_DIRECTORY'", "Value");
            string body_directory = ds.Tables[0].Rows[0]["Value"].ToString();
            filename = filename.Replace(body_directory, "");
            if (filename.StartsWith("\\"))
                filename = filename.Substring(1, filename.Length - 1);
            filename = filename.Replace("\\", "/");

            return filename;
        }

        /// <summary>
        /// getPropertyValueOnDocument
        /// </summary>
        /// <param name="documentID"></param>
        /// <param name="propertyID"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <author>dungnt</author>
        /// <createddate>2008</createddate>
        public static string getPropertyValueOnDocument(string documentID, string propertyID)
        {
            bus_Type_Doc_Property objTDP = bus_Type_Doc_Property.Instance((UserContext)HttpContext.Current.Session["objUserContext"]);
            string svalue = objTDP.GetPropertyValueOnDocument(documentID, propertyID);
            objTDP = null;
            return svalue;
        }

        /// <summary>
        /// getDocIDByValue
        /// </summary>
        /// <param name="documentID"></param>
        /// <param name="propertyID"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <author>thangma</author>
        /// <createddate>2014</createddate>
        //public static string getDocIDByValue( string propertyID, string value)
        //{
        //    bus_Type_Doc_Property objTDP = bus_Type_Doc_Property.Instance((UserContext)HttpContext.Current.Session["objUserContext"]);
        //    DataSet ds = objTDP.getList(" TEXTVALUE = N'" + value + "'", "FK_DOCUMENTID");
        //    if (isValidDataSet(ds))
        //    {
        //        return ds.Tables[0].Rows[0]["FK_DOCUMENTID"].ToString();
        //    }
        //    objTDP = null;
        //    return "";
        //}
        /// <summary>
        ///
        /// </summary>
        /// <param name="PropertyID"></param>
        /// <returns></returns>
        public static void GetLookUpValue(string PropertyID, DropDownList combo)
        {
            bus_Property obj = bus_Property.Instance((UserContext)HttpContext.Current.Session["objUserContext"]);
            DataSet ds = obj.GetLookUpValue(PropertyID);
            obj = null;
            combo.DataSource = ds;
            combo.DataValueField = "Value";
            combo.DataTextField = "Value";
            combo.DataBind();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="PropertyID"></param>
        /// <param name="combo"></param>
        public static void GetLookUpValue(string PropertyID, DropDownList combo, int DocStatus)
        {
            bus_Property obj = bus_Property.Instance((UserContext)HttpContext.Current.Session["objUserContext"]);
            DataSet ds = obj.GetLookUpValue(PropertyID, DocStatus);
            obj = null;
            combo.DataSource = ds;
            combo.DataValueField = "ID";
            combo.DataTextField = "Value";
            combo.DataBind();
        }

        //Lay du lieu tat ca loai doi tuong
        /// <summary>
        ///
        /// </summary>
        /// <param name="PropertyID"></param>
        /// <param name="DocField"></param>
        /// <returns></returns>

        //lay tat ca du lieu nam trong 1 bang
        public static void GetT_Type_Doc_Property(string PropertyID, string DocField, DropDownList drpl)
        {
            string strConn = ConfigurationManager.AppSettings["ConnectionString"].ToString();
            SqlConnection conn = new SqlConnection(strConn);
            string sql = @"select TEXTVALUE from T_TYPE_DOC_PROPERTY as a left join T_DOCUMENT as b on a.FK_DOCUMENTID = b.PK_DOCUMENTID
                           where a.FK_PROPERTYID = (SELECT PK_PROPERTYID FROM T_PROPERTY WHERE PK_PROPERTYID ='" + PropertyID + @"')
                           and b.STATUS = 4";
            SqlCommand sqlCommand = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataReader dr = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
            DataTable dt = new DataTable();
            dt.Load(dr);
            drpl.DataSource = dt;
            drpl.DataTextField = "TEXTVALUE";
            drpl.DataValueField = "TEXTVALUE";
            drpl.DataBind();
        }

        //get year for Dropdownlist
        public static void GetYear2Dropdownlist(DropDownList drpl, string defaultValue = "")
        {
            List<string> lstYear = MiscUtils.GetAllYear();
            drpl.DataSource = lstYear;
            drpl.DataBind();
            drpl.SelectedValue = string.IsNullOrEmpty(defaultValue) ? MiscUtils.CurrentYear : defaultValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="DocumentID"></param>
        /// <param name="Owner"></param>
        /// <returns></returns>
        public static bool isDocumentOwner(string DocumentID, string UserName)
        {
            bus_Document objDoc = bus_Document.Instance((UserContext)HttpContext.Current.Session["objUserContext"]);
            string createdby = objDoc.getByID(DocumentID, "CreatedBy").Tables[0].Rows[0]["CreatedBy"].ToString();
            if (createdby.ToLower().Trim().Equals(UserName.ToLower().Trim()))
                return true;
            else
                return false;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="DocumentTypeID"></param>
        /// <returns></returns>
        public static string getDoctypeName(string DocumentTypeID)
        {
            bus_Document_Type obj = bus_Document_Type.Instance((UserContext)HttpContext.Current.Session["objUserContext"]);
            DataSet ds = obj.getByID(DocumentTypeID, "Name");
            obj = null;
            return ds.Tables[0].Rows[0]["Name"].ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="DocumentID"></param>
        /// <returns></returns>
        public static string deleteDocument(string DocumentID)
        {
            bus_Document obj = bus_Document.Instance((UserContext)HttpContext.Current.Session["objUserContext"]);
            return obj.deleteDocument(DocumentID);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="DocumentID"></param>
        /// <param name="DisplayFileName"></param>
        /// <returns></returns>
        public static string deleteBody(string DocumentID, string DisplayFileName)
        {
            bus_Doc_Version_Body obj = bus_Doc_Version_Body.Instance((UserContext)HttpContext.Current.Session["objUserContext"]);
            string Condition = " And ";
            Condition += " And DisplayFileName=N'" + DisplayFileName + "'";
            int n = obj.delete(Condition);
            obj = null;
            return n == 0 ? "" : "Lỗi, không xóa được file attachment.";
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="DocVersionBodyID"></param>
        /// <returns></returns>
        public static string deleteBody(string DocVersionBodyID)
        {
            bus_Doc_Version_Body obj = bus_Doc_Version_Body.Instance((UserContext)HttpContext.Current.Session["objUserContext"]);
            int n = obj.deleteByID(DocVersionBodyID);
            obj = null;
            return n == 0 ? "" : "Lỗi, không xóa được file attachment.";
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="documentid"></param>
        public static void LoadDocInfo(string documentid, MasterPage Master)
        {
            bus_Document obj = bus_Document.Instance((UserContext)HttpContext.Current.Session["objUserContext"]);
            DataSet ds = obj.loadDocumentInfo(documentid);
            obj = null;
            ContentPlaceHolder ph = (ContentPlaceHolder)Master.Master.FindControl("ContentPlaceHolder1");
            Control FormContent = ph.FindControl("FormContent");
            foreach (Control ctl in FormContent.Controls)
            {
                string PropertyID = ctl.ID;
                if (string.IsNullOrEmpty(PropertyID))
                    continue;

                #region docname & desc & docstatus

                if ("DOCNAME".LastIndexOf(PropertyID.ToUpper()) != -1)
                    ((TextBox)ctl).Text = ds.Tables[0].Rows[0]["DocName"].ToString();
                else if ("DOCDESCRIPTION".LastIndexOf(PropertyID.ToUpper()) != -1)
                    ((TextBox)ctl).Text = ds.Tables[0].Rows[0]["DocDescription"].ToString();
                else if ("DOCSTATUS".LastIndexOf(PropertyID.ToUpper()) != -1)
                    ((DropDownList)ctl).SelectedValue = ds.Tables[0].Rows[0]["DocStatus"].ToString();

                #endregion docname & desc & docstatus

                #region Properties

                if (PropertyID.Length == 40)
                {
                    string stype = StringHelper.Left(PropertyID, 3);
                    PropertyID = StringHelper.Right(PropertyID, 36).Replace("_", "-");
                    DataRow[] R = ds.Tables[0].Select("FK_PropertyID='" + PropertyID + "'");

                    if (R.Length == 0) continue;
                    if (stype == "ID9")
                    {
                        if (ctl.GetType() == typeof(TextBox))
                            ((TextBox)ctl).Text = R[0]["TextValue"].ToString();
                        if (ctl.GetType() == typeof(HiddenField))
                            ((HiddenField)ctl).Value = R[0]["TextValue"].ToString();
                        else if (ctl.GetType() == typeof(DropDownList))
                        {
                            DropDownList cbo = ((DropDownList)ctl);
                            string s = R[0]["TextValue"].ToString();

                            bool s_isExist = false;
                            foreach (ListItem item in cbo.Items)
                                if (item.Value.ToUpper() == s.ToUpper())
                                {
                                    s_isExist = true;
                                    break;
                                }

                            if (!s_isExist)
                                cbo.Items.Insert(0, new ListItem(s, s));

                            cbo.SelectedValue = s;
                        }
                        else if (ctl.GetType() == typeof(C1WebNumericEdit))
                            ((C1WebNumericEdit)ctl).Text = R[0]["TextValue"].ToString();
                    }
                    else if (stype == "ID8")
                    {
                        if (ctl.GetType() == typeof(TextBox))
                            ((TextBox)ctl).Text = R[0]["TextValue"].ToString();
                        if (ctl.GetType() == typeof(HiddenField))
                            ((HiddenField)ctl).Value = R[0]["TextValue"].ToString();
                        else if (ctl.GetType() == typeof(DropDownList))
                        {
                            DropDownList cbo = ((DropDownList)ctl);
                            string s = R[0]["TextValue"].ToString();

                            bool s_isExist = false;
                            foreach (ListItem item in cbo.Items)
                                if (item.Value.ToUpper() == s.ToUpper())
                                {
                                    s_isExist = true;
                                    break;
                                }

                            if (!s_isExist)
                                cbo.Items.Insert(0, new ListItem(s, s));

                            cbo.SelectedValue = s;
                        }
                        else if (ctl.GetType() == typeof(C1WebNumericEdit))
                            ((C1WebNumericEdit)ctl).Text = R[0]["TextValue"].ToString();
                    }
                    else if (stype == "ID6")
                    {
                        if (ctl.GetType() == typeof(TextBox))
                            ((TextBox)ctl).Text = R[0]["NumericValue"].ToString();
                        else if (ctl.GetType() == typeof(C1WebNumericEdit))
                            //((C1WebNumericEdit)ctl).Text = R[0]["NumericValue"].ToString();
                            ((C1WebNumericEdit)ctl).Text = Math.Round(Convert.ToDecimal(R[0]["NumericValue"]), 0).ToString();
                    }
                    else if (stype == "ID3")
                    {
                        if (ctl.GetType() == typeof(TextBox))
                            ((TextBox)ctl).Text = String.Format("{0:dd/MM/yyyy}", R[0]["DateTimeValue"]);
                    }
                }

                #endregion Properties
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="PropertyID"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int CountPropertyValue(string PropertyID, string value)
        {
            bus_Type_Doc_Property obj = bus_Type_Doc_Property.Instance((UserContext)HttpContext.Current.Session["objUserContext"]);
            int n = obj.CountPropertyValue(PropertyID, value.Trim());
            obj = null;
            return n;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="PropertyID"></param>
        /// <param name="value"></param>
        /// <param name="ExclusiveDocumentID"></param>
        /// <returns></returns>
        public static int CountPropertyValue(string PropertyID, string value, string ExclusiveDocumentID)
        {
            bus_Type_Doc_Property obj = bus_Type_Doc_Property.Instance((UserContext)HttpContext.Current.Session["objUserContext"]);
            int n = obj.CountPropertyValue(PropertyID, value, ExclusiveDocumentID);
            obj = null;
            return n;
        }

        #endregion for document

        #region for doclink

        /// <summary>
        ///
        /// </summary>
        /// <param name="fk_documentid"></param>
        /// <param name="fk_doclinkid"></param>
        /// <param name="objctx"></param>
        /// <returns></returns>
        public static int AddDocLink(string fk_documentid, string fk_doclinkid, byte linktype, UserContext objctx)
        {
            bus_Document objdoc = bus_Document.Instance(objctx);
            return objdoc.addLink(fk_documentid, fk_doclinkid, linktype);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="fk_documentid"></param>
        /// <param name="fk_doclinkid"></param>
        /// <param name="linktype"></param>
        /// <param name="objctx"></param>
        /// <param name="AdditionalData1"></param>
        /// <param name="AdditionalData2"></param>
        /// <returns></returns>
        public static int AddDocLink(string fk_documentid, string fk_doclinkid, byte linktype, UserContext objctx, string AdditionalData1, string AdditionalData2)
        {
            bus_Document objdoc = bus_Document.Instance(objctx);
            return objdoc.addLink(fk_documentid, fk_doclinkid, linktype, AdditionalData1, AdditionalData2);
        }

        /// <summary>
        /// removeDocumentLink
        /// </summary>
        /// <param name="FK_DocLinkID"></param>
        /// <param name="FK_DocumentID"></param>
        /// <param name="objUserContext"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public static int RemoveDocLink(string fk_documentid, string fk_doclinkid, UserContext objctx)
        {
            bus_Document objDoc = bus_Document.Instance(objctx);  //bus_Document objDoc = new bus_Document(objctx);
            return objDoc.removeLink(fk_doclinkid, fk_documentid);
        }

        /// <summary>
        /// removeDocumentLink
        /// </summary>
        /// <param name="FK_DocLinkID"></param>
        /// <param name="FK_DocumentID"></param>
        /// <param name="objUserContext"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public static string RemoveDocLink(string linkID, UserContext objctx)
        {
            bus_Document objDoc = bus_Document.Instance(objctx);  //bus_Document objDoc = new bus_Document(objctx);
            return objDoc.removeLink(linkID);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="Condition"></param>
        /// <returns></returns>
        public static int getCountOnDocumentLink(string Condition)
        {
            int n = 0;
            bus_Document obj = bus_Document.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            n = obj.getCountOnDocumentLink(Condition);
            obj = null;
            return n;
        }

        #endregion for doclink

        #region for docspace

        /// <summary>
        ///
        /// </summary>
        /// <param name="DocSpaceID"></param>
        /// <param name="objctx"></param>
        /// <returns></returns>
        public static string getDocspaceName(string DocSpaceID, UserContext objctx)
        {
            string DocspaceName = string.Empty;
            bus_DocSpace objdocspace = bus_DocSpace.Instance(objctx);
            DataSet ds = objdocspace.getByID(DocSpaceID, "Name");
            DocspaceName = ds.Tables[0].Rows[0]["Name"].ToString();
            ds = null;
            objdocspace = null;
            return DocspaceName;
        }

        #endregion for docspace

        #region for Activity and Process instance

        /// <summary>
        ///
        /// </summary>
        /// <param name="ProcessStatus"></param>
        /// <param name="ActivityStatus"></param>
        /// <param name="Performer"></param>
        /// <returns></returns>
        public static int CountByActivityStatusOnProcess(int ProcessStatus, int ActivityStatus, string Performer)
        {
            bus_Activity_Instance obj = bus_Activity_Instance.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            return obj.CountByActivityStatusOnProcess(ProcessStatus, ActivityStatus, Performer);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ActivityStatus"></param>
        /// <param name="Performer"></param>
        /// <returns></returns>
        public static int CountActivityOverDue(string Performer)
        {
            string today = String.Format("{0:yyyyMMdd}", DateTime.Now);
            string query = "N=count(distinct FK_WF_InstanceID)";
            string condition = " and fk_performer='" + Performer + "'";
            condition += " and convert(char(8),Deadline,112)<" + today;
            bus_Activity_Instance obj = bus_Activity_Instance.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            DataSet ds = obj.getList(condition, query);
            obj = null;
            return int.Parse(ds.Tables[0].Rows[0]["N"].ToString());
        }

        /// <summary>
        /// finishActivityInstance
        /// </summary>
        /// <param name="ActivityInstanceID"></param>
        /// <author>dungnt</author>
        /// <createddate>2008</createddate>
        public static void finishActivityInstance(string ActivityInstanceID, UserContext _objUserContext, string _dbName)
        {
            bus_Activity_Instance objAct = new bus_Activity_Instance(_objUserContext, _dbName);
            int err_sys = objAct.UpdateActivityInstance(" Status=" + CORE.WFS.Const.ACTIVITY_INSTANCE_STATUS_FINISHED, " AND PK_ActivityInstanceID='" + ActivityInstanceID + "'");
        }

        /// <summary>
        /// finishActivityInstanceByPerformer
        /// </summary>
        /// <param name="ProcessInstanceID"></param>
        /// <param name="PerformerID"></param>
        /// <param name="_objUserContext"></param>
        /// <param name="_dbName"></param>
        /// <author>dungnt</author>
        /// <createddate>2008</createddate>
        public static void finishActivityInstanceByPerformer(string ProcessInstanceID, string PerformerID, UserContext _objUserContext, string _dbName)
        {
            bus_Activity_Instance objAct = new bus_Activity_Instance(_objUserContext, _dbName);
            int err_sys = objAct.UpdateActivityInstance(" Status=" + CORE.WFS.Const.ACTIVITY_INSTANCE_STATUS_FINISHED, " AND FK_WF_InstanceID='" + ProcessInstanceID + "' AND FK_PerformerID='" + PerformerID + "'");
        }

        /// <summary>
        /// activeAllActivityInstance
        /// </summary>
        /// <param name="ProcessInstanceID"></param>
        /// <param name="ActivityDefinition"></param>
        /// <author>dungnt</author>
        /// <createddate>2008</createddate>
        public static void activeAllActivityInstance(string ProcessInstanceID, string ActivityDefinition, UserContext _objUserContext, string _dbName)
        {
            bus_Activity_Instance objACT = new bus_Activity_Instance(_objUserContext, _dbName);
            objACT.UpdateActivityInstance(" Status=" + CORE.WFS.Const.ACTIVITY_INSTANCE_STATUS_ACTIVE, " AND FK_WF_InstanceID='" + ProcessInstanceID + "' AND FK_ActivityDefinitionID='" + ActivityDefinition + "'");
        }

        /// <summary>
        /// inactiveAllActivityInstance
        /// </summary>
        /// <param name="ProcessInstanceID"></param>
        /// <param name="ActivityDefinition"></param>
        /// <param name="_objUserContext"></param>
        /// <param name="_dbName"></param>
        /// <author>dungnt</author>
        /// <createddate>2008</createddate>
        public static void inactiveAllActivityInstance(string ProcessInstanceID, string ActivityDefinition, UserContext _objUserContext, string _dbName)
        {
            bus_Activity_Instance objACT = new bus_Activity_Instance(_objUserContext, _dbName);
            objACT.UpdateActivityInstance(" Status=" + CORE.WFS.Const.ACTIVITY_INSTANCE_STATUS_INACTIVE, " AND FK_WF_InstanceID='" + ProcessInstanceID + "' AND FK_ActivityDefinitionID='" + ActivityDefinition + "'");
        }

        /// <summary>
        /// activeActivityInstance
        /// </summary>
        /// <param name="ActivityInstanceID"></param>
        /// <param name="_objUserContext"></param>
        /// <param name="_dbName"></param>
        /// <author>dungnt</author>
        /// <createddate>2008</createddate>
        public static int activeActivityInstance(string ActivityInstanceID)
        {
            string curDateTime = String.Format("{0:dd/MM/yyyy HH:mm:ss}", DateTime.Now);
            bus_Activity_Instance objACT = bus_Activity_Instance.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            return objACT.UpdateActivityInstance(" Status=" + STATUS.ACTIVE + ",StartedDate=getdate()", " AND PK_ActivityInstanceID='" + ActivityInstanceID + "'");
        }

        /// <summary>
        /// activeActivityInstanceByPerformer
        /// </summary>
        /// <param name="ProcessInstanceID"></param>
        /// <param name="PerformerID"></param>
        /// <param name="_objUserContext"></param>
        /// <param name="_dbName"></param>
        /// <author>dungnt</author>
        /// <createddate>2008</createddate>
        public static void activeActivityInstanceByPerformer(string ProcessInstanceID, string PerformerID, UserContext _objUserContext, string _dbName)
        {
            bus_Activity_Instance objACT = new bus_Activity_Instance(_objUserContext, _dbName);
            objACT.UpdateActivityInstance(" Status=" + CORE.WFS.Const.ACTIVITY_INSTANCE_STATUS_ACTIVE, " AND FK_WF_InstanceID='" + ProcessInstanceID + "' AND FK_PerformerID='" + PerformerID + "'");
        }

        /// <summary>
        /// inactiveActivityInstance
        /// </summary>
        /// <param name="ActivityInstanceID"></param>
        /// <param name="_objUserContext"></param>
        /// <param name="_dbName"></param>
        /// <author>dungnt</author>
        /// <createddate>2008</createddate>
        public static void inactiveActivityInstance(string ActivityInstanceID, UserContext _objUserContext, string _dbName)
        {
            bus_Activity_Instance objACT = new bus_Activity_Instance(_objUserContext, _dbName);
            objACT.UpdateActivityInstance(" Status=" + CORE.WFS.Const.ACTIVITY_INSTANCE_STATUS_INACTIVE, " AND FK_ActivityInstanceID='" + ActivityInstanceID + "'");
        }

        /// <summary>
        /// inactiveActivityInstanceByPerformer
        /// </summary>
        /// <param name="ProcessInstanceID"></param>
        /// <param name="PerformerID"></param>
        /// <param name="_objUserContext"></param>
        /// <param name="_dbName"></param>
        /// <author>dungnt</author>
        /// <createddate>2008</createddate>
        public static void inactiveActivityInstanceByPerformer(string ProcessInstanceID, string PerformerID, UserContext _objUserContext, string _dbName)
        {
            bus_Activity_Instance objACT = new bus_Activity_Instance(_objUserContext, _dbName);
            objACT.UpdateActivityInstance(" Status=" + CORE.WFS.Const.ACTIVITY_INSTANCE_STATUS_INACTIVE, " AND FK_WF_InstanceID='" + ProcessInstanceID + "' AND FK_PerformerID='" + PerformerID + "'");
        }

        /// <summary>
        /// FinishingAllActivityInstance
        /// </summary>
        /// <param name="ProcessInstanceID"></param>
        /// <author>dungnt</author>
        /// <createddate>2008</createddate>
        public static void finishAllActivityInstance(string ProcessInstanceID, UserContext _objUserContext, string _dbName)
        {
            bus_Activity_Instance objACT = new bus_Activity_Instance(_objUserContext, _dbName);
            objACT.UpdateActivityInstance(" Status=" + CORE.WFS.Const.ACTIVITY_INSTANCE_STATUS_FINISHED, " AND FK_WF_InstanceID='" + ProcessInstanceID + "'");
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="DocumentID"></param>
        /// <param name="ActivityDefinitionID"></param>
        /// <param name="objUserContext"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public static bool isDocumentAssigned(string DocumentID, string ActivityDefinitionID, UserContext objUserContext, string dbName)
        {
            bus_WF_Instance objPI = new bus_WF_Instance(objUserContext, dbName);
            DataSet dsPI = objPI.getList(" AND FK_DocumentID='" + DocumentID + "'", "PK_WF_InstanceID");

            if ((dsPI == null) || (dsPI.Tables[0].Rows.Count == 0))
                return false;

            string ProcessInstanceID = dsPI.Tables[0].Rows[0]["PK_WF_InstanceID"].ToString();

            bus_Activity_Instance objACT = new bus_Activity_Instance(objUserContext, dbName);
            string Query = "FK_PerformerID";
            string Condition = " AND FK_WF_InstanceID='" + ProcessInstanceID + "' AND FK_ActivityDefinitionID='" + ActivityDefinitionID + "'";

            DataSet dsACT = objACT.getList(Condition, Query);

            if ((dsACT == null) || (dsACT.Tables[0].Rows.Count == 0))
                return false;

            return true;
        }

        /// <summary>
        /// Kiem tra mot document da ton tai hay chua
        /// </summary>
        /// <param name="DocumentTypeID">doctype of document</param>
        /// <param name="Condition">dieu kien kiem tra su ton tai cua document</param>
        /// <returns>true: document da ton tai; false: document chua ton tai</returns>
        public static bool isDocumentExists(string DocumentTypeID, string Condition)
        {
            //bus_Document objdoc = bus_Document.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            //DataSet ds = objdoc.getDocumentList(DocumentTypeID, "PK_DocumentID", Condition);
            //if (ds == null)
            //    return false;
            //else
            return true;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public static string getToActivityDefinitionID(string FromActivityDefinitionID)
        {
            string FK_ToActivityID = string.Empty;
            bus_Transition objTrans = bus_Transition.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            string Query = "FK_ToActivityID";
            string Condition = " AND FK_FromActivityID='" + FromActivityDefinitionID + "'";
            DataSet ds = objTrans.getList(Condition, Query);
            if (ds.Tables[0].Rows.Count > 0)
                FK_ToActivityID = ds.Tables[0].Rows[0]["FK_ToActivityID"].ToString();
            ds = null;
            objTrans = null;
            return FK_ToActivityID;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ActivityInstanceID"></param>
        /// <returns></returns>
        public static int getActivityInstanceStatus(string ActivityInstanceID)
        {
            bus_Activity_Instance objAct = bus_Activity_Instance.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            DataSet ds = objAct.getByID(ActivityInstanceID, "Status");
            objAct = null;
            return Convert.ToInt16(ds.Tables[0].Rows[0]["Status"]);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ProcessInstanceID"></param>
        /// <returns></returns>
        public static int getProcessInstanceStatus(string ProcessInstanceID)
        {
            bus_WF_Instance obj = bus_WF_Instance.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            DataSet ds = obj.getByID(ProcessInstanceID, "Status");
            obj = null;
            return Convert.ToInt16(ds.Tables[0].Rows[0]["Status"]);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ActivityInstanceID"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        public static DataRow getActivityInfo(string ActivityInstanceID, string Query)
        {
            bus_Activity_Instance objAct = bus_Activity_Instance.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            return objAct.getByID(ActivityInstanceID, Query).Tables[0].Rows[0];
        }

        public static bool IsOwnerActivityInstance(string ActivityInstanceID, string Performer)
        {
            DataRow R = getActivityInfo(ActivityInstanceID, "FK_Performer");
            if (R["FK_Performer"].ToString().ToUpper() == Performer.ToUpper())
                return true;
            return false;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ActivityInstanceID"></param>
        /// <returns></returns>
        public static string getActivityDefinition(string ActivityInstanceID)
        {
            bus_Activity_Instance objAct = bus_Activity_Instance.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            DataSet ds = objAct.getByID(ActivityInstanceID, "FK_ActivityDefinitionID");
            return ds.Tables[0].Rows[0]["FK_ActivityDefinitionID"].ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ProcessDefinitionID"></param>
        /// <returns></returns>
        public static string getFirstActivityDefinition(string ProcessDefinitionID)
        {
            string ActivityDefinition = string.Empty;
            bus_Activity_Definition obj = bus_Activity_Definition.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            DataSet ds = obj.getList(" And FK_WF_DefinitionID='" + ProcessDefinitionID + "' And Step=1", "Top 1 PK_ActivityID");
            obj = null;
            ActivityDefinition = ds.Tables[0].Rows[0]["PK_ActivityID"].ToString();
            return ActivityDefinition;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ProcessInstanceID"></param>
        /// <returns></returns>
        public static string getLastestActivity(string ProcessInstanceID)
        {
            bus_Activity_Instance obj = bus_Activity_Instance.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            string Query = "Top 1 FK_ACTIVITYDEFINITIONID";
            string Condition = " And FK_WF_INSTANCEID='" + ProcessInstanceID + "'";
            Condition += " Order By CREATEDDATE Desc";
            DataSet ds = obj.getList(Condition, Query);
            obj = null;
            return ds.Tables[0].Rows[0]["FK_ACTIVITYDEFINITIONID"].ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ProcessInstanceID"></param>
        /// <param name="PerformerID"></param>
        /// <returns></returns>
        public static string getLastestActivityInstanceByPerformer(string ProcessInstanceID, string Performer)
        {
            string ActivityInstanceID = string.Empty;
            bus_Activity_Instance objAct = bus_Activity_Instance.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            string Query = "TOP 1 PK_ActivityInstanceID";
            string Condition = " AND FK_WF_InstanceID='" + ProcessInstanceID + "' AND FK_Performer='" + Performer + "'";
            Condition += " ORDER BY CREATEDDATE DESC";
            DataSet ds = objAct.getList(Condition, Query);
            if (ds.Tables[0].Rows.Count > 0)
                ActivityInstanceID = ds.Tables[0].Rows[0]["PK_ActivityInstanceID"].ToString();
            ds = null;
            objAct = null;
            return ActivityInstanceID;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ProcessInstanceID"></param>
        /// <returns></returns>
        public static string getLastestActivityInstance(string ProcessInstanceID, string groupid)
        {
            string ActivityInstanceID = string.Empty;
            bus_Activity_Instance objAct = bus_Activity_Instance.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            string Query = "TOP 1 PK_ActivityInstanceID";
            string Condition = " AND FK_WF_InstanceID='" + ProcessInstanceID + "'";
            Condition += " AND FK_GROUPID='" + groupid + "'";
            Condition += " ORDER BY CREATEDDATE DESC";
            DataSet ds = objAct.getList(Condition, Query);
            if (ds.Tables[0].Rows.Count > 0)
                ActivityInstanceID = ds.Tables[0].Rows[0]["PK_ActivityInstanceID"].ToString();
            ds = null;
            objAct = null;
            return ActivityInstanceID;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ProcessInstanceID"></param>
        /// <returns></returns>
        public static string getLastestActivityInstance(string ProcessInstanceID)
        {
            string ActivityInstanceID = string.Empty;
            bus_Activity_Instance objAct = bus_Activity_Instance.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            string Query = "TOP 1 PK_ActivityInstanceID";
            string Condition = " AND FK_WF_InstanceID='" + ProcessInstanceID + "'";
            Condition += " ORDER BY CREATEDDATE DESC";
            DataSet ds = objAct.getList(Condition, Query);
            if (ds.Tables[0].Rows.Count > 0)
                ActivityInstanceID = ds.Tables[0].Rows[0]["PK_ActivityInstanceID"].ToString();
            ds = null;
            objAct = null;
            return ActivityInstanceID;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="WF_Definition"></param>
        public static void buildActivityNavigation(string ProcessDefinitionID, string CurrentActivityDefinitionID, Literal Navigation, int ProcessStatus)
        {
            bus_Activity_Definition obj = bus_Activity_Definition.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            string Query = "PK_ActivityID,Step,Name,Description";
            string Condition = " And FK_WF_DefinitionID='" + ProcessDefinitionID + "'";
            Condition += " Order By Step";
            DataSet ds = obj.getList(Condition, Query);

            System.Text.StringBuilder navigation = new System.Text.StringBuilder();

            foreach (DataRow R in ds.Tables[0].Rows)
            {
                string PK_ActivityID = R["PK_ActivityID"].ToString();
                if (PK_ActivityID.ToUpper() != CurrentActivityDefinitionID.ToUpper())
                {
                    navigation.Append("<font color='Gray'>" + R["Step"].ToString() + ". " + R["Name"].ToString() + "</font> >>> ");
                }
                else
                {
                    if (ProcessStatus != STATUS.FINISHED)
                        navigation.Append("<font color='Navy'><b>" + R["Step"].ToString() + ". " + R["Name"].ToString() + "</b></font> >>> ");
                    else
                        navigation.Append("<font color='Gray'>" + R["Step"].ToString() + ". " + R["Name"].ToString() + "</font> >>> ");
                }
            }

            if (ProcessStatus != STATUS.FINISHED)
                navigation.Append("<font color='Gray'>Hoàn tất xử lý</font>");
            else
                navigation.Append("<font color='Navy'><b>Hoàn tất xử lý</b></font>");
            Navigation.Text = navigation.ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ProcessInstance"></param>
        /// <param name="ProcessStatus"></param>
        /// <param name="Navigation"></param>
        public static void buildActivityNavigation(string ProcessDefinitionID, string ProcessInstance, Literal Navigation)
        {
            int ProcessStatus = 0;
            string ActivityDefinitionID = "";

            if (string.IsNullOrEmpty(ProcessInstance))
            {
                ActivityDefinitionID = getFirstActivityDefinition(ProcessDefinitionID);
            }
            else
            {
                ActivityDefinitionID = getActivityDefinition(getLastestActivityInstance(ProcessInstance));
                ProcessStatus = getProcessInstanceStatus(ProcessInstance);
            }

            bus_Activity_Definition obj = bus_Activity_Definition.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            string Query = "PK_ActivityID,Step,Name,Description";
            string Condition = " And FK_WF_DefinitionID='" + ProcessDefinitionID + "'";
            Condition += " Order By Step";
            DataSet ds = obj.getList(Condition, Query);

            System.Text.StringBuilder navigation = new System.Text.StringBuilder();

            foreach (DataRow R in ds.Tables[0].Rows)
            {
                string PK_ActivityID = R["PK_ActivityID"].ToString();
                if (PK_ActivityID.ToUpper() != ActivityDefinitionID.ToUpper())
                {
                    navigation.Append("<font color='Gray'>" + R["Step"].ToString() + ". " + R["Name"].ToString() + "</font> >>> ");
                }
                else
                {
                    if (ProcessStatus != STATUS.FINISHED)
                        navigation.Append("<font color='Navy'><b>" + R["Step"].ToString() + ". " + R["Name"].ToString() + "</b></font> >>> ");
                    else
                        navigation.Append("<font color='Gray'>" + R["Step"].ToString() + ". " + R["Name"].ToString() + "</font> >>> ");
                }
            }

            if (ProcessStatus != STATUS.FINISHED)
                navigation.Append("<font color='Gray'>Hoàn tất xử lý</font>");
            else
                navigation.Append("<font color='Navy'><b>Hoàn tất xử lý</b></font>");
            Navigation.Text = navigation.ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ActivityID"></param>
        /// <returns></returns>
        public static string getRoleNameOnActivity(string ActivityID)
        {
            string RoleName = string.Empty;
            bus_WF_Definition_Role obj = bus_WF_Definition_Role.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            string Query = "Name";
            string Condition = " And PK_WF_DefinitionRoleID In (Select FK_RoleID From T_ACTIVITY_DEFINITION Where PK_ActivityID='" + ActivityID + "')";
            DataSet ds = obj.getList(Condition, Query);
            obj = null;
            return ds.Tables[0].Rows[0]["Name"].ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="processInstanceID"></param>
        /// <param name="toactivityID"></param>
        public static void updateActiviyDeadline(string processInstanceID, string toactivityID, byte days)
        {
            string Query = "Deadline=DateAdd(D," + days.ToString() + ",StartedDate)";
            string Condition = " And FK_WF_InstanceID='" + processInstanceID + "' And FK_ActivityDefinitionID='" + toactivityID + "'";
            bus_Activity_Instance obj = bus_Activity_Instance.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            obj.UpdateActivityInstance(Query, Condition);
            obj = null;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="processInstanceID"></param>
        /// <param name="days"></param>
        public static void updateProcessInstanceDeadline(string processInstanceID, byte days)
        {
            string Query = "Deadline=DateAdd(D," + days.ToString() + ",StartedDate)";
            string Condition = " And PK_WF_InstanceID='" + processInstanceID + "'";
            bus_WF_Instance obj = bus_WF_Instance.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            obj.UpdateProcessInstance(Query, Condition);
            obj = null;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ActivityInstanceID"></param>
        /// <param name="ProcessInstanceID"></param>
        public static void updateActivityInstanceDeadlineByProcessDeadline(string ActivityInstanceID, string ProcessInstanceID)
        {
            bus_Activity_Instance obj = bus_Activity_Instance.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            obj.updateActivityInstanceDeadlineByProcessDeadline(ActivityInstanceID, ProcessInstanceID);
            obj = null;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="processInstanceID"></param>
        /// <returns></returns>
        public static string getProcessInstanceDeadline(string processInstanceID)
        {
            string Query = "Deadline=IsNull(Convert(VarChar(10),Deadline,103),'')";
            bus_WF_Instance obj = bus_WF_Instance.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            DataSet ds = obj.getByID(processInstanceID, Query);
            obj = null;
            return ds.Tables[0].Rows[0]["Deadline"].ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ActivityInstanceID"></param>
        /// <param name="PropertyID"></param>
        /// <returns></returns>
        public static DataTable getPropertyValuesOnActivity(string ActivityInstanceID, string PropertyID)
        {
            bus_Activity_Instance_Property_Values obj = bus_Activity_Instance_Property_Values.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            DataSet ds = obj.getList(" and FK_ActivityInstanceID='" + ActivityInstanceID + "'", "TextValue");
            obj = null;
            return ds.Tables[0];
        }

        public static string GetGroupXly(string processInstanceID)
        {
            string groupid = getProcessInfo(processInstanceID, "FK_GroupID")["FK_GroupID"].ToString();
            UserContext userContext = (UserContext)System.Web.HttpContext.Current.Session["objUserContext"];
            bus_Permission obj = bus_Permission.Instance(userContext);
            DataSet ds = obj.getList(" AND FK_APPLIEDONOBJECTID='" + groupid + "'", " FK_APPLIEDONID ");
            if (!isValidDataSet(ds))
            {
                return string.Empty;
            }
            else
            {
                //bus_Group objGroup = bus_Group.Instance(userContext);
                //return objGroup.getList(" AND PK_GROUPID='" + ds.Tables[0].Rows[0]["FK_APPLIEDONID"].ToString() + "'", " FK_PARENTGROUPID ").Tables[0].Rows[0]["FK_PARENTGROUPID"].ToString();
                return ds.Tables[0].Rows[0]["FK_APPLIEDONID"].ToString();
            }
        }

        #endregion for Activity and Process instance

        #region for Process instance

        /// <summary>
        /// activeProcessInstance
        /// </summary>
        /// <param name="ProcessInstanceID"></param>
        /// <author>dungnt</author>
        /// <createddate>2008</createddate>
        public static void activeProcessInstance(string ProcessInstanceID, UserContext _objUserContext, string _dbName)
        {
            bus_WF_Instance objPI = new bus_WF_Instance(_objUserContext, _dbName);
            DataSet dsPI = objPI.getByID(ProcessInstanceID, string.Empty);
            DataRow rPI = dsPI.Tables[0].Rows[0];
            rPI["Status"] = CORE.WFS.Const.PROCESS_STATUS_ACTIVE;
            objPI.saveDataSet(dsPI);
            objPI = null;
        }

        /// <summary>
        /// finishProcessInstance
        /// </summary>
        /// <param name="ProcessInstanceID"></param>
        /// <author>dungnt</author>
        /// <createddate>2008</createddate>
        public static string finishProcessInstance(string ProcessInstanceID)
        {
            bus_WF_Instance objPI = bus_WF_Instance.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            return objPI.finishProcessInstance(ProcessInstanceID);
        }

        /// <summary>
        /// isUserAsignedOnProcess
        /// </summary>
        /// <param name="ProcessInstanceID"></param>
        /// <param name="PerformerID"></param>
        /// <returns></returns>
        /// <author>dungnt</author>
        /// <createddate>20081206</createddate>
        public static bool isUserAsignedOnProcess(string ProcessInstanceID, string PerformerID, UserContext objUserContext, string dbName)
        {
            bus_Activity_Instance objACT = new bus_Activity_Instance(objUserContext, dbName);
            string Query = "FK_PerformerID";
            string Condition = " AND FK_WF_InstanceID='" + ProcessInstanceID + "' AND FK_PerformerID='" + PerformerID + "'";
            DataSet dsACT = objACT.getList(Condition, Query);
            if ((dsACT == null) || (dsACT.Tables[0].Rows.Count == 0))
                return false;

            return true;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="WF_InstanceID"></param>
        /// <param name="Query"></param>
        /// <returns></returns>
        public static DataRow getProcessInfo(string ProcessInstanceID, string Query)
        {
            bus_WF_Instance objWfi = bus_WF_Instance.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            return objWfi.getByID(ProcessInstanceID, Query).Tables[0].Rows[0];
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ProcessInstanceID"></param>
        /// <returns></returns>
        public static string getProcessInfo(string ProcessInstanceID)
        {
            string ProcessInfo = string.Empty;
            bus_WF_Instance objWfi = bus_WF_Instance.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            ProcessInfo = objWfi.getProcessInfo_Footer(ProcessInstanceID);
            objWfi = null;
            return ProcessInfo;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ProcessInstanceID"></param>
        /// <param name="objUserContext"></param>
        /// <returns></returns>
        public static string getDocumentIDAttachedOnProcess(string ProcessInstanceID, UserContext objUserContext)
        {
            string DocumentID = string.Empty;
            bus_WF_Instance objwfi = bus_WF_Instance.Instance(objUserContext);
            string Query = "FK_DocumentID";
            DataSet ds = objwfi.getByID(ProcessInstanceID, Query);
            if (ds.Tables[0].Rows.Count > 0)
                DocumentID = ds.Tables[0].Rows[0]["FK_DocumentID"].ToString();
            ds = null;
            objwfi = null;
            return DocumentID;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ProcessInstanceID"></param>
        /// <param name="litProcessInfo"></param>
        public static void BuildProcessInfoFooter(string ProcessInstanceID, Literal litProcessInfo)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ProcessInstanceID"></param>
        /// <returns></returns>
        public static string loadAllDiscusion(string ProcessInstanceID)
        {
            return string.Empty;
            //StringBuilder table_Data = new StringBuilder();
            //bus_Activity_Instance_Property_Values obj = bus_Activity_Instance_Property_Values.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            //DataSet ds = obj.getListTextValue(ACTIVITY_PROPERTY.THONGTIN_TRAODOI, ProcessInstanceID);
            //obj = null;
            //if (isValidDataSet(ds))
            //{
            //    foreach (DataRow R in ds.Tables[0].Rows)
            //    {
            //        table_Data.Append("<pre><font color='Gray'>" + R["CreatedDateTime"].ToString() + " [" + R["CreatedBy"].ToString() + "]:   </font>");
            //        table_Data.Append("<i>" + R["TEXTVALUE"].ToString() + "</i></pre>");
            //    }
            //}
            //return table_Data.ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ActivityInstanceID"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        public static string addnewDiscusion(string ActivityInstanceID, string Message)
        {
            return string.Empty;
            //bus_Activity_Instance_Property_Values obj = bus_Activity_Instance_Property_Values.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            //string ErrorMessage = obj.saveTextValue(ActivityInstanceID, ACTIVITY_PROPERTY.THONGTIN_TRAODOI, Message, ((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]).UserName);
            //obj = null;
            //return ErrorMessage;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ProcessInstanceID"></param>
        /// <returns></returns>
        public static string getCompany(string ProcessInstanceID)
        {
            string Company = string.Empty;
            bus_WF_Instance obj = bus_WF_Instance.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            DataSet ds = obj.getByID(ProcessInstanceID, "Company");
            obj = null;
            Company = ds.Tables[0].Rows[0]["Company"].ToString();
            if (!string.IsNullOrEmpty(Company))
            {
                bus_Group group = bus_Group.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
                ds = group.getList("and Name='" + Company + "'", "Top 1 Description");
                group = null;
                Company += " - " + ds.Tables[0].Rows[0]["Description"].ToString();
            }
            return Company;
        }

        #endregion for Process instance

        #region for Transition

        /// <summary>
        ///
        /// </summary>
        /// <param name="FromActivityID"></param>
        /// <param name="objctx"></param>
        /// <returns></returns>
        public static DataSet getNextTransition(string FromActivityID, UserContext objctx)
        {
            bus_Transition objTransition = bus_Transition.Instance(objctx);
            string Query = "[NAME],[FK_TOACTIVITYID]";
            string Condition = " AND FK_FROMACTIVITYID='" + FromActivityID + "'";
            //Condition += " AND [FK_TOACTIVITYID] IS NOT NULL";
            DataSet ds = objTransition.getList(Condition, Query);
            objTransition = null;
            return ds;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="FromActivityID"></param>
        /// <param name="ToActivityID"></param>
        /// <returns></returns>
        public static bool IsTransitionRollback(string FromActivityID, string ToActivityID)
        {
            bool IsRollback = false;
            bus_Transition obj = bus_Transition.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            DataSet ds = obj.getList(" And FK_FromActivityID='" + FromActivityID + "' And FK_ToActivityID='" + ToActivityID + "'", "IsRollback=IsNull(IsRollback,0)");
            obj = null;
            if (isValidDataSet(ds))
            {
                IsRollback = int.Parse(ds.Tables[0].Rows[0]["IsRollback"].ToString()) == 1 ? true : false;
            }
            return IsRollback;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="FK_RoleID"></param>
        /// <param name="FK_WF_DefinitionID"></param>
        /// <returns></returns>
        public static DataSet getTransition(string FK_RoleID, string FK_WF_DefinitionID)
        {
            bus_Transition obj = bus_Transition.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            DataSet ds = obj.getTransition(FK_RoleID, FK_WF_DefinitionID);
            obj = null;
            return ds;
        }

        #endregion for Transition

        #region for window operation

        public static void CloseMe()
        {
            System.Text.StringBuilder script = new System.Text.StringBuilder();
            script.Append("<script type=\"text/javascript\">");
            script.Append("window.opener=null;");
            script.Append("window.close(true);");
            script.Append("</script>");
            System.Web.HttpContext.Current.Response.Write(script.ToString());
        }

        #endregion for window operation

        #region for permission

        /// <summary>
        /// getPermissionOnObject
        /// </summary>
        /// <param name="objectid"></param>
        /// <param name="appliedid"></param>
        /// <returns></returns>
        public static int getPermissionOnObject(string objectid, string appliedid, UserContext objusercontext, string dbName)
        {
            bus_Permission objper = bus_Permission.Instance(objusercontext);  //bus_Permission objper = new bus_Permission(objusercontext, dbName);
            return objper.getPermissionOnObject(objectid, appliedid);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="applicantid"></param>
        /// <param name="typeofapplicant"></param>
        /// <param name="objectid"></param>
        /// <param name="typeofobject"></param>
        /// <param name="grandtedright"></param>
        /// <param name="objctx"></param>
        /// <param name="dbname"></param>
        /// <returns></returns>
        public static int SetPermissionOnObject(string applicantid, byte typeofapplicant, string objectid, byte typeofobject, byte grandtedright, string FK_ParentFolderIDOnPermission, string CreatedBy, string FinishedDate, byte Status, UserContext objctx, string dbname)
        {
            bus_Permission objpermission = bus_Permission.Instance(objctx);  //bus_Permission objpermission = new bus_Permission(objctx, dbname);
            return objpermission.updatePermissionOnObject(applicantid, typeofapplicant, objectid, typeofobject, grandtedright, FK_ParentFolderIDOnPermission, CreatedBy, FinishedDate, Status);
        }

        /// <summary>
        /// changePermissionOnObject
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="UserType"></param>
        /// <param name="ObjectID"></param>
        /// <param name="TypeOfObject"></param>
        /// <param name="GrantedRight"></param>
        /// <param name="objctx"></param>
        /// <param name="dbname"></param>
        /// <returns></returns>
        public static int changePermissionOnObject(string UserID, byte UserType, string ObjectID, byte TypeOfObject, byte GrantedRight, UserContext objctx, string dbname)
        {
            bus_Permission objpermission = bus_Permission.Instance(objctx);  //bus_Permission objpermission = new bus_Permission(objctx, dbname);
            return objpermission.changePermissionOnObject(UserID, UserType, ObjectID, TypeOfObject, GrantedRight);
        }

        /// <summary>
        /// GetFinishedDateOnObject
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="UserType"></param>
        /// <param name="ObjectID"></param>
        /// <param name="TypeOfObject"></param>
        /// <param name="objctx"></param>
        /// <param name="dbname"></param>
        /// <returns></returns>
        public static string GetFinishedDateOnObject(string UserID, byte UserType, string ObjectID, byte TypeOfObject, UserContext objctx, string dbname)
        {
            string condition = " AND FK_AppliedOnID='" + UserID + "'";
            condition += " AND TypeOfApplicant=" + UserType;
            condition += " AND FK_AppliedOnObjectID='" + ObjectID + "'";
            condition += " AND TypeOfObject=" + TypeOfObject;
            string query = "CONVERT(CHAR(8),FinishedDate,112) As FinishedDate";
            bus_Permission objpermission = bus_Permission.Instance(objctx);  //bus_Permission objpermission = new bus_Permission(objctx, dbname);
            DataSet ds = objpermission.getList(condition, query);
            objpermission = null;
            if (ds == null) return string.Empty;
            if (ds.Tables[0].Rows.Count == 0) return string.Empty;
            string finisheddate = ds.Tables[0].Rows[0]["FinishedDate"].ToString();
            if (finisheddate == "30000101") finisheddate = string.Empty;
            return finisheddate;
        }

        #endregion for permission

        #region for audit log

        /// <summary>
        ///
        /// </summary>
        /// <param name="ObjectType"></param>
        /// <param name="OperationType"></param>
        /// <param name="AdditionalData1"></param>
        /// <param name="AdditionalData2"></param>
        /// <param name="_objusercontext"></param>
        public static void AddAuditLog(byte ObjectType, byte OperationType, string AdditionalData1, string AdditionalData2, UserContext _objusercontext)
        {
            //UserContext _objusercontext = (UserContext)System.Web.HttpContext.Current.Session["objUserContext"];

            bus_Audit objaudit = bus_Audit.Instance((UserContext)HttpContext.Current.Session["objUserContext"]);
            DataSet ds = objaudit.getEmpty(string.Empty);
            DataRow R = ds.Tables[0].NewRow();

            Guid PK_AuditID = System.Guid.NewGuid();
            R["PK_AuditID"] = PK_AuditID;
            R["FK_UserID"] = _objusercontext.UserID;
            R["FK_ObjectID"] = DBNull.Value;
            R["ObjectType"] = ObjectType;
            R["OperationType"] = OperationType;
            R["AdditionalData1"] = AdditionalData1;
            R["AdditionalData2"] = AdditionalData2;
            R["ProjectCode"] = _objusercontext.ProjectID;
            R["UserName"] = _objusercontext.UserName;
            R["UserIP"] = _objusercontext.UserIP;

            ds.Tables[0].Rows.Add(R);

            int err = objaudit.addnewDataSet(ds);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ProcessInstanceID"></param>
        /// <param name="ActivityInstanceID"></param>
        /// <param name="FK_ObjectID"></param>
        /// <param name="ObjectType"></param>
        /// <param name="OperationType"></param>
        /// <param name="AdditionalData1"></param>
        /// <param name="AdditionalData2"></param>
        /// <param name="rolename"></param>
        /// <param name="groupname"></param>
        /// <returns></returns>
        public static string AddAuditLog(string ProcessInstanceID, string ActivityInstanceID, string FK_ObjectID, byte ObjectType, byte OperationType, string AdditionalData1, string AdditionalData2, string rolename, string groupname)
        {
            UserContext objUserContext = (UserContext)System.Web.HttpContext.Current.Session["objUserContext"];
            bus_Audit objLog = bus_Audit.Instance(objUserContext);
            return objLog.AddAuditLog(ProcessInstanceID, ActivityInstanceID, FK_ObjectID, ObjectType, OperationType, AdditionalData1, AdditionalData2, objUserContext.UserID, objUserContext.UserIP, objUserContext.UserName, rolename, groupname);
        }

        #endregion for audit log

        #region for application and component

        /// <summary>
        /// GetComponentName
        /// </summary>
        /// <param name="componentid"></param>
        /// <param name="objUserContext"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public static string GetComponentName(string componentid, UserContext objUserContext, string dbName)
        {
            bus_Component obj = bus_Component.Instance(objUserContext); //bus_Component obj = new bus_Component(objUserContext, dbName);
            DataSet ds = obj.getByID(componentid, "Name");
            obj = null;
            if (ds == null) return string.Empty;
            if (ds.Tables[0].Rows.Count == 0) return string.Empty;
            return ds.Tables[0].Rows[0]["Name"].ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="componentid"></param>
        /// <param name="objUserContext"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public static string GetApplicationID(string componentid, UserContext objUserContext, string dbName)
        {
            bus_Component obj = bus_Component.Instance(objUserContext); //bus_Component obj = new bus_Component(objUserContext, dbName);
            DataSet ds = obj.getByID(componentid, "FK_ApplicationID");
            obj = null;
            if (ds == null) return string.Empty;
            if (ds.Tables[0].Rows.Count == 0) return string.Empty;
            return ds.Tables[0].Rows[0]["FK_ApplicationID"].ToString();
        }

        /// <summary>
        /// IsRoleAssignedOnComponent
        /// </summary>
        /// <param name="componentid"></param>
        /// <param name="roleid"></param>
        /// <param name="objUserContext"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public static bool IsRoleAssignedOnComponent(string componentid, string roleid, UserContext objUserContext, string dbName)
        {
            bus_Component objcomp = bus_Component.Instance(objUserContext); //bus_Component objcomp = new bus_Component(objUserContext,dbName);
            return objcomp.isRoleAssignedOnComponent(componentid, roleid);
        }

        /// <summary>
        /// IsRoleAssignedOnApplication
        /// </summary>
        /// <param name="applicationid"></param>
        /// <param name="roleid"></param>
        /// <param name="objUserContext"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public static bool IsRoleAssignedOnApplication(string applicationid, string roleid, UserContext objUserContext, string dbName)
        {
            return true;
        }

        /// <summary>
        /// RemoveRoleFromComponent
        /// </summary>
        /// <param name="componentid"></param>
        /// <param name="roleid"></param>
        /// <param name="objUserContext"></param>
        /// <param name="dbName"></param>
        public static int deleteRoleFromComponent(string componentid, string roleid, UserContext objUserContext, string dbName)
        {
            bus_Component objcomp = bus_Component.Instance(objUserContext); //bus_Component objcomp = new bus_Component(objUserContext,dbName);
            return objcomp.deleteRoleFromComponent(componentid, roleid);
        }

        /// <summary>
        /// AddRoleToComponent
        /// </summary>
        /// <param name="componentid"></param>
        /// <param name="roleid"></param>
        /// <param name="objUserContext"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public static int AddRoleToComponent(string componentid, string roleid, UserContext objUserContext, string dbName)
        {
            bus_Component objcomp = bus_Component.Instance(objUserContext); //bus_Component objcomp = new bus_Component(objUserContext, dbName);
            return objcomp.addRoleToComponent(componentid, roleid);
        }

        #endregion for application and component

        #region for message notifycation

        /// <summary>
        /// DelMessNotify
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="componentid"></param>
        /// <param name="objUserContext"></param>
        /// <param name="dbName"></param>
        public static void DelMessNotify(string userid, string componentid, UserContext objUserContext, string dbName)
        {
            bus_Message obj = new bus_Message(objUserContext, dbName);
            obj.delete(" And FK_ToUserID='" + userid + "' And MessageType='" + componentid + "'");
            obj = null;
        }

        /// <summary>
        /// AddMessNotify
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="message"></param>
        public static void AddMessNotify(string userid, string message, string MessageType, UserContext objUserContext, string dbName)
        {
            bus_Message obj = new bus_Message(objUserContext, dbName);
            DataSet ds_newmessage = obj.getEmpty(string.Empty);
            DataRow r = ds_newmessage.Tables[0].NewRow();
            r["PK_MessageID"] = Guid.NewGuid();
            r["FK_FromUserID"] = new Guid(objUserContext.UserID);
            r["FK_ToUserID"] = new Guid(userid);
            r["Content"] = message;
            r["MessageType"] = MessageType;
            ds_newmessage.Tables[0].Rows.Add(r);
            obj.addnewDataSet(ds_newmessage);
            obj = null;
        }

        #endregion for message notifycation

        #region for invoke sp

        public static object InvokeSP(string sp, object[] par)
        {
            return null;
        }

        #endregion for invoke sp

        #region for reprot helper

        /// <summary>
        ///
        /// </summary>
        /// <param name="rptfile"></param>
        /// <param name="rptExportPath"></param>
        /// <param name="datasource"></param>
        /// <param name="param"></param>
        /// <param name="exportFileType"></param>
        /// <param name="exportFileName"></param>
        /// <param name="objUserContext"></param>
        /// <returns></returns>
        public static string do_report(string rptfile, string rptExportPath, DataSet datasource, SortedList<string, object> param, string exportFileType, string exportFileName, UserContext objUserContext)
        {
            ExportFormatType formattype = ExportFormatType.NoFormat;
            if (exportFileType.ToUpper() == "PDF")
                formattype = ExportFormatType.PortableDocFormat;
            else if (exportFileType.ToUpper() == "XLS")
                formattype = ExportFormatType.Excel;
            else if (exportFileType.ToUpper() == "DOC")
                formattype = ExportFormatType.WordForWindows;

            CrystalReportViewer _rptViewer = new CrystalReportViewer();

            ReportDocument _rptDoc = new ReportDocument();

            try
            {
                _rptDoc.Load(System.Web.HttpContext.Current.Server.MapPath(rptfile));
            }
            catch (Exception ex)
            {
                return "Error:\n" + ex.Message + "\n" + System.Web.HttpContext.Current.Server.MapPath(rptfile);
            }

            if (datasource != null)
                _rptDoc.SetDataSource(datasource);

            ReportHelper _rptHelper = new ReportHelper(_rptDoc, _rptViewer);

            if (param.Count > 0)
                for (int i = 0; i < param.Count; i++)
                {
                    string name = param.Keys[i];
                    string value = param[name].ToString();
                    _rptHelper.SetParameter(name, value);
                }

            _rptHelper.Open();

            string client_path = rptExportPath;

            string filename = exportFileName + "." + exportFileType;
            string export_filepath = System.Web.HttpContext.Current.Server.MapPath(client_path + filename);

            _rptHelper.SaveRpt(export_filepath, formattype, _rptDoc);
            string url = client_path + filename;

            _rptDoc.Close();
            _rptDoc.Dispose();
            _rptHelper.Close();
            _rptHelper.Dispose();

            return url;
        }

        #endregion for reprot helper

        #region feedback client

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        public static void FeedBackClient(string message)
        {
            HttpContext.Current.Response.Write(message);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        #endregion feedback client



        #region for export excel

        /// <summary>
        ///
        /// </summary>
        /// <param name="db"></param>
        /// <param name="filename"></param>
        public static void exportTbl2Excel(DataTable db, string filename)
        {
            HttpContext.Current.Response.Clear();

            HttpContext.Current.Response.Charset = "UTF-8";
            HttpContext.Current.Response.HeaderEncoding = System.Text.Encoding.UTF8;
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Unicode;
            HttpContext.Current.Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());

            HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", filename));
            HttpContext.Current.Response.ContentType = "Session/ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    GridView gv = new GridView();
                    gv.DataSource = db;
                    gv.DataBind();
                    gv.RenderControl(htw);
                    HttpContext.Current.Response.Write(sw.ToString());
                    HttpContext.Current.Response.End();
                }
            }
        }

        #endregion for export excel

        #region for convert date vn - t24

        /// <summary>
        ///
        /// </summary>
        /// <param name="vnDate"></param>
        /// <returns></returns>
        public static string conertDateVN2T24(string vnDate)
        {
            return String.Format("{0:yyyyMMdd}", DateTime.Parse(vnDate, new CultureInfo("vi-VN", false)));
        }

        #endregion for convert date vn - t24

        #region for File Process

        /// <summary>
        /// copy file to server
        /// </summary>
        /// <param name="fUpl"></param>
        /// <param name="COMPANY"></param>
        /// <param name="MAHOSO1"></param>
        /// <param name="MaTaiSan"></param>
        /// <param name="objUserContext"></param>
        /// <param name="MaLoaiGiayTo"></param>
        /// <returns></returns>
        public static string UploadFile(FileUpload fUpl, string Company, string DocumentID, CORE.CoreObjectContext.UserContext objUserContext)
        {
            string Err = string.Empty;

            try
            {
                string strDateFolder = string.Empty;

                // Specify the path to save the uploaded file to.
                string savePath = WebConfigurationManager.AppSettings["RootFile"];
                if (!System.IO.Directory.Exists(savePath))
                {
                    System.IO.Directory.CreateDirectory(savePath);
                }
                savePath += "\\" + Company;
                if (!System.IO.Directory.Exists(savePath))
                {
                    System.IO.Directory.CreateDirectory(savePath);
                }

                //upload
                string fn = fUpl.FileName;
                fn = fn.Replace("+", "_");
                fn = fn.Replace(" ", "");
                fn = fn.Replace("'", "");

                string folder = savePath;
                string FileName = DateTime.Now.ToString("ddMMyyyyhhmmss") + "_" + fn;
                savePath += "\\" + FileName;
                fUpl.SaveAs(savePath);

                //cap nhat vao database
                bus_Doc_Version_Body obj = bus_Doc_Version_Body.Instance(objUserContext);
                DataSet ds = obj.getEmpty("");
                DataRow dr = ds.Tables[0].NewRow();

                dr["PK_DOCVERSIONBODYID"] = System.Guid.NewGuid();
                dr["FK_DOCUMENTID"] = DocumentID;
                dr["FILENAME"] = FileName;
                dr["FILEPATH"] = folder;
                dr["CREATEDDATE"] = DateTime.Now.ToString("yyyy/MM/dd");
                dr["DISPLAYFILENAME"] = fUpl.FileName;
                dr["Description"] = "";
                ds.Tables[0].Rows.Add(dr);
                //kiem tra xem co file chua neu co roi thi thoi khong insert nua
                DataSet ds_Check = obj.getList(" and FILENAME = '" + FileName + "' and FILEPATH = '" + folder + "'", "");
                if (ds_Check.Tables[0].Rows.Count > 0)
                {
                    Err = "Đã tồn tại file có tên trên gắn cho đối tượng đang sử dụng.";
                }
                else
                {
                    int result = obj.addnewDataSet(ds);
                    Err = "OK";
                }
            }
            catch (Exception ex)
            {
                Err = ex.ToString();
            }
            return Err;
        }

        /// <summary>
        /// copy file to server
        /// </summary>
        /// <param name="fUpl"></param>
        /// <param name="COMPANY"></param>
        /// <param name="MAHOSO1"></param>
        /// <param name="MaTaiSan"></param>
        /// <param name="objUserContext"></param>
        /// <param name="MaLoaiGiayTo"></param>
        /// <returns></returns>
        /// <author>quangna</author>
        public static string UploadFile(FileUpload fUpl, string Company, string DocumentID, string description, CORE.CoreObjectContext.UserContext objUserContext)
        {
            string Err = string.Empty;

            try
            {
                string strDateFolder = string.Empty;

                // Specify the path to save the uploaded file to.
                string savePath = WebConfigurationManager.AppSettings["RootFile"];
                if (!System.IO.Directory.Exists(savePath))
                {
                    System.IO.Directory.CreateDirectory(savePath);
                }
                savePath += "\\" + Company;
                if (!System.IO.Directory.Exists(savePath))
                {
                    System.IO.Directory.CreateDirectory(savePath);
                }

                //upload
                string fn = fUpl.FileName;
                fn = fn.Replace("+", "_");
                fn = fn.Replace(" ", "");
                fn = fn.Replace("'", "");

                string folder = savePath;
                string FileName = DateTime.Now.ToString("ddMMyyyyhhmmss") + "_" + fn;
                savePath += "\\" + FileName;
                fUpl.SaveAs(savePath);

                //cap nhat vao database
                bus_Doc_Version_Body obj = bus_Doc_Version_Body.Instance(objUserContext);
                DataSet ds = obj.getEmpty("");
                DataRow dr = ds.Tables[0].NewRow();

                dr["PK_DOCVERSIONBODYID"] = System.Guid.NewGuid();
                dr["FK_DOCUMENTID"] = DocumentID;
                dr["FILENAME"] = FileName;
                dr["FILEPATH"] = folder;
                dr["CREATEDDATE"] = DateTime.Now.ToString("yyyy/MM/dd");
                dr["DISPLAYFILENAME"] = fUpl.FileName;
                dr["Description"] = description;
                ds.Tables[0].Rows.Add(dr);
                //kiem tra xem co file chua neu co roi thi thoi khong insert nua
                DataSet ds_Check = obj.getList(" and FILENAME = '" + FileName + "' and FILEPATH = '" + folder + "'", "");
                if (ds_Check.Tables[0].Rows.Count > 0)
                {
                    Err = "Đã tồn tại file có tên trên gắn cho đối tượng đang sử dụng.";
                }
                else
                {
                    int result = obj.addnewDataSet(ds);
                    Err = "OK";
                }
            }
            catch (Exception ex)
            {
                Err = ex.ToString();
            }
            return Err;
        }

        public static string CopyFile(string DocIDTaiSan, string MaLoaiGiayTo, string COMPANY, string DocIDHoSo, string FilePath, string FileName, CORE.CoreObjectContext.UserContext objUserContext)
        {
            string Err = string.Empty;
            string MAHOSO = DocIDHoSo.Replace("/", "_");

            //save file theo cau truc COMPANY/MAHOSO
            try
            {
                string strDateFolder = string.Empty;

                // Specify the path to save the uploaded file to.
                string savePath = WebConfigurationManager.AppSettings["RootFile"];
                if (!System.IO.Directory.Exists(savePath))
                {
                    System.IO.Directory.CreateDirectory(savePath);
                }
                savePath += "\\" + COMPANY;
                if (!System.IO.Directory.Exists(savePath))
                {
                    System.IO.Directory.CreateDirectory(savePath);
                }
                savePath += "\\" + MAHOSO;
                if (!System.IO.Directory.Exists(savePath))
                {
                    System.IO.Directory.CreateDirectory(savePath);
                }
                //Copy file.

                File.Copy(FilePath + "\\" + FileName, savePath + "\\" + FileName, true);
                //cap nhat tren DB
                bus_Doc_Version_Body obj = bus_Doc_Version_Body.Instance(objUserContext);
                DataSet ds = obj.getList(" and FILENAME = '" + FileName + "' and FILEPATH = '" + FilePath.Replace("/", "\\") + "'", "");
                DataRow dr = ds.Tables[0].NewRow();

                dr["PK_DOCVERSIONBODYID"] = System.Guid.NewGuid();
                dr["FK_DOCUMENTID"] = DocIDTaiSan;
                dr["FILENAME"] = FileName;
                dr["FILEPATH"] = savePath;
                dr["CREATEDDATE"] = DateTime.Now.ToString("yyyy/MM/dd");
                dr["DISPLAYFILENAME"] = ds.Tables[0].Rows[0]["DISPLAYFILENAME"].ToString(); ;
                dr["COMMENT"] = ds.Tables[0].Rows[0]["COMMENT"].ToString();
                ds.Tables[0].Rows.Add(dr);

                //kiem tra xem co file chua neu co roi thi thoi khong insert nua
                DataSet ds_Check = obj.getList(" and FILENAME = '" + FileName + "' and FILEPATH = '" + savePath + "'", "");
                if (ds_Check.Tables[0].Rows.Count > 0)
                {
                    Err = "Đã tồn tại file có tên trên gắn cho tài sản đang sử dụng";
                }
                else
                {
                    obj.addnewDataSet(ds);
                    Err = "OK, đã copy file thành công";
                }
            }
            catch (Exception ex)
            {
                Err = ex.ToString();
            }
            return Err;
        }

        #endregion for File Process

        #region for KTNB

        /// <summary>
        /// check whether string is number
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        /// <author>quangna</author>
        public static bool IsNumber(string text)
        {
            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            return regex.IsMatch(text);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="satus"></param>
        public static void LoadStatus(DropDownList obj, string defaultValue = "2")
        {
            obj.Items.Clear();
            obj.Items.Add(new ListItem("Active", "4"));
            obj.Items.Add(new ListItem("Inactive", "2"));
            obj.SelectedValue = defaultValue == "2" ? defaultValue : "4";
        }

        /// <summary>
        /// Load items for dropdownlist by flag
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="flag">variable flag</param>
        /// <author>quangna</author>
        public static void LoadDropDownList(DropDownList obj, int flag)
        {
            if (flag == 1)
            {
                obj.Items.Add(new ListItem("Xác suất", "Xác suất"));
                obj.Items.Add(new ListItem("Ảnh hưởng", "Ảnh hưởng"));
                obj.Items.Add(new ListItem("Kiểm soát", "Kiểm soát"));
            }
            if (flag == 2)
            {
                string currentYear = DateTime.Now.Year.ToString();
                for (int i = 1; i <= 12; i++)
                {
                    string text = String.Format("Tháng {0}", i);
                    obj.Items.Add(new ListItem(text));
                }
            }
            if (flag == 3)
            {
                int currentYear = DateTime.Now.Year;
                for (int i = currentYear; i >= currentYear - 10; i--)
                {
                    obj.Items.Add(new ListItem(i.ToString()));
                }
            }
            if (flag == 4)//dropdownlist congviec
            {
                obj.Items.Add(new ListItem("Draft", "0"));
                obj.Items.Add(new ListItem("Not started", "2"));
                obj.Items.Add(new ListItem("In progress", "4"));
                obj.Items.Add(new ListItem("Rejected", "8"));
                obj.Items.Add(new ListItem("Done", "16"));
                obj.Items.Add(new ListItem("Approved", "32"));
                obj.Items.Add(new ListItem("Approved2", "64"));
                //obj.Items.Add(new ListItem("Rejected", "128"));
            }
            if (flag == 5)
            {
                //for dropdownlist muc do phat hien
                var list = MucDoPhatHienValue();
                foreach (var element in list)
                {
                    obj.Items.Add(new ListItem(element.Value, element.Key));
                }
            }
            if (flag == 6)//dropdownlist phathien + phanhoi
            {
                obj.Items.Add(new ListItem("Draft", "0"));
                //obj.Items.Add(new ListItem("Rejected", "1"));
                obj.Items.Add(new ListItem("Done", "2"));
                obj.Items.Add(new ListItem("Approved", "4"));
                obj.Items.Add(new ListItem("Approved2", "16"));
                obj.Items.Add(new ListItem("Rejected", "32"));
            }
            if (flag == 7)
            {
                obj.Items.Add(new ListItem("---Tất cả---", "-1"));
                obj.Items.Add(new ListItem("Lập kế hoạch", "1"));
                obj.Items.Add(new ListItem("Thực hiện", "2"));
                obj.Items.Add(new ListItem("Lập báo cáo", "3"));
                obj.Items.Add(new ListItem("Đánh giá", "4"));
                obj.Items.Add(new ListItem("Giám sát", "5"));
            }
        }

        public static List<KeyValuePair<string, string>> MucDoPhatHienValue()
        {
            var list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("Thấp", "Thấp"));
            list.Add(new KeyValuePair<string, string>("Trung bình", "Trung bình"));
            list.Add(new KeyValuePair<string, string>("Cao", "Cao"));
            return list;
        }

        /// <summary>
        /// check whether user is TruongDoan
        /// </summary>
        /// <param name="dotkt"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        /// author:quangna
        public static bool IsTruongDoan(string dotkt, string userName)
        {
            string _doctypeid_doankiemtoan = DOCTYPE.DOAN_KIEMTOAN;
            string DocFields = "PK_DocumentID,[Name]";
            string PropertyFields = "Name";
            string Condition = " and PK_DocumentID in (Select FK_documentID From T_DocLink Where FK_DocLinkID='" + dotkt + "')";
            bus_Document obj = bus_Document.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            DataSet ds = obj.getDocumentList(_doctypeid_doankiemtoan, DocFields, PropertyFields, Condition);
            if (ds != null)
                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                        return ds.Tables[0].Select("[Name]='" + userName + "'").Length > 0 ? true : false;
            return false;
        }

        /// <summary>
        /// Set status of all Doc to Inactive, except input ID
        /// </summary>
        /// <param name="DocumentID"></param>
        /// <returns></returns>
        /// author:KhanhNP
        public static int setInactiveLoaiTC(string documentID, string year)
        {
            string _doctypeid = DOCTYPE.QUANLY_PHANLOAI_BOTIEUCHI;
            string DocFields = "PK_DocumentID,Status,[Năm],[Loại bộ tiêu chí]";
            string PropertyFields = "Năm,Loại bộ tiêu chí";
            string Condition = string.Format(" and [Năm] = '" + year + "' and PK_DocumentID !='" + documentID + "' Order By [Năm],[Loại bộ tiêu chí]");
            bus_Document obj = bus_Document.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            DataSet ds = obj.getDocumentList(_doctypeid, DocFields, PropertyFields, Condition);
            DataRow[] dr_allDiff = null;
            dr_allDiff = ds.Tables[0].Select("", "PK_DocumentID");
            string fk_parentgroupid = "";
            foreach (DataRow r_parent_group in dr_allDiff)
            {
                fk_parentgroupid = r_parent_group["PK_DocumentID"].ToString();
                UpdateDocStatus(fk_parentgroupid, 2);
            }
            return 0;
        }

        public static int countLoaiTC(string name, string year)
        {
            string _doctypeid = DOCTYPE.QUANLY_PHANLOAI_BOTIEUCHI;
            string DocFields = "PK_DocumentID,Status,[Năm],[Loại bộ tiêu chí]";
            string PropertyFields = "Năm,Loại bộ tiêu chí";
            string Condition = string.Format(" and [Năm] = '" + year + "' and [Loại bộ tiêu chí] = '" + name + "' Order By [Năm],[Loại bộ tiêu chí]");
            bus_Document obj = bus_Document.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            DataSet ds = obj.getDocumentList(_doctypeid, DocFields, PropertyFields, Condition);

            return ds.Tables[0].Rows.Count;
        }

        public static int countRankLoaiDTKT(string name, string rank)
        {
            string _doctypeid = DOCTYPE.RANK_LDTKT;
            string DocFields = "PK_DocumentID,Status,[Rank],[Loại đối tượng kiểm toán]";
            string PropertyFields = "Rank,Loại đối tượng kiểm toán";
            string Condition = string.Format(" and [Rank] = " + rank + " and [Loại đối tượng kiểm toán] = '" + name + "'");
            bus_Document obj = bus_Document.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            DataSet ds = obj.getDocumentList(_doctypeid, DocFields, PropertyFields, Condition);

            return ds.Tables[0].Rows.Count;
        }

        /// <summary>
        /// Add New Bo quy mo
        /// </summary>
        /// <param name="T_Document"></param>
        /// /// <param name="List<dm_quymo>"></param>
        /// <returns></returns>
        /// author:DuongNQ
        public static void AddNewQuyMo(T_Document documnetInfo, string lstquymo)
        {
            string _doctypeid = DOCTYPE.QuyMoDTKT;
            bus_Document_Type objType = bus_Document_Type.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            ManagerFactory.t_document_manager.Insert(documnetInfo, new Guid(_doctypeid));
            DataSet ds = objType.GetProperyList(_doctypeid);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                T_TypeDocProperty tmpTypeDocProperty;
                if (dr["NAME"].ToString() == "Tên quy mô")
                {
                    tmpTypeDocProperty = new T_TypeDocProperty()
                    {
                        fk_documentid = documnetInfo.Pk_documentid,
                        fk_propertyid = new Guid(dr["PK_PROPERTYID"].ToString()),
                        type = int.Parse(dr["TYPE"].ToString()),
                        textvalue = System.Guid.NewGuid().ToString()
                    };
                    ManagerFactory.t_type_doc_property.AddNewDocProperty(tmpTypeDocProperty);
                }
                else if (dr["NAME"].ToString() == "Giá trị")
                {
                    tmpTypeDocProperty = new T_TypeDocProperty()
                    {
                        fk_documentid = documnetInfo.Pk_documentid,
                        fk_propertyid = new Guid(dr["PK_PROPERTYID"].ToString()),
                        type = int.Parse(dr["TYPE"].ToString()),
                        textvalue = lstquymo
                    };
                    ManagerFactory.t_type_doc_property.AddNewDocProperty(tmpTypeDocProperty);
                }
                else
                {
                    tmpTypeDocProperty = new T_TypeDocProperty()
                    {
                        fk_documentid = documnetInfo.Pk_documentid,
                        fk_propertyid = new Guid(dr["PK_PROPERTYID"].ToString()),
                        type = int.Parse(dr["TYPE"].ToString()),
                        textvalue = string.Empty
                    };
                    ManagerFactory.t_type_doc_property.AddNewDocProperty(tmpTypeDocProperty);
                }
            }
        }

        public static int countqlBTCKHN(string year, string loaiDTKT, string boTCN)
        {
            string _doctypeid = DOCTYPE.QUANLY_BOTIEUCHI_KEHOACHNAM;
            string DocFields = "PK_DocumentID,[Năm],[Loại đối tượng kiểm toán],[Bộ tiêu chí năm]";
            string PropertyFields = "Năm,Loại đối tượng kiểm toán,Bộ tiêu chí năm";
            string Condition = string.Format(" and [Năm] = '" + year + "' and [Loại đối tượng kiểm toán] = '" + loaiDTKT + "' and [Bộ tiêu chí năm] = '" + boTCN + "' Order By [Năm]");
            bus_Document obj = bus_Document.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            DataSet ds = obj.getDocumentList(_doctypeid, DocFields, PropertyFields, Condition);

            return ds.Tables[0].Rows.Count;
        }

        /// <summary>
        /// back previous page by url
        /// </summary>
        /// <param name="btn"></param>
        /// <param name="url"></param>
        /// author: quangna
        public static void SetButtonBack(Button btn, string url)
        {
            btn.Visible = true;
            btn.Text = "Quay lại";
            btn.Attributes.Add("onclick", "{window.location.href='" + url + "'; return false;}");
        }

        /// <summary>
        /// back previous page
        /// </summary>
        /// <param name="btn"></param>
        /// author:quangna
        public static void SetButtonBack(Button btn)
        {
            btn.Visible = true;
            btn.Text = "Huỷ";
            btn.Attributes.Add("onclick", "{window.history.go(-1); return false;}");
        }

        /// <summary>
        /// format theo ngay/thang/nam
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        /// <author>quangna</author>
        public static string formatDateTimeDDMMYYY(string datetime)
        {
            if (String.IsNullOrEmpty(datetime))
                return String.Empty;
            string result = datetime.Contains("1900") ? String.Empty : (datetime.IndexOf(" ") <= 0 ? datetime : datetime.Substring(0, datetime.IndexOf(" ")));
            return result;
        }

        //public static bool IsTruongNhom(string doankt, string userID)
        //{
        //    string _doctypeid_nhomkiemtoan = DOCTYPE.NHOM_KIEMTOAN;
        //    string DocFields = "PK_DocumentID,[Name]";
        //    string PropertyFields = "Name";
        //    string Condition = " and PK_DocumentID in (Select FK_documentID From T_DocLink Where FK_DocLinkID='" + doankt + "' and FK_documentID='"+userID+"')";
        //    bus_Document obj = bus_Document.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
        //    DataSet ds = obj.getDocumentList(_doctypeid_nhomkiemtoan, DocFields, PropertyFields, Condition);
        //    if (ds != null)
        //        if (ds.Tables.Count > 0)
        //            return ds.Tables[0].Rows.Count > 0 ? true : false;
        //    return false;
        //}

        /// <summary>
        /// check la nguoiduyet1 hay nguoiduyet2
        /// </summary>
        /// <param name="docID"></param>
        /// <returns></returns>
        /// <author>quangna</author>quangna>
        public static string CheckNguoiDuyet(string docID)
        {
            string DocFields = "PK_DocumentID,[Tên công việc],[Người thực hiện],[Người duyệt 1],[Người duyệt 2],[Ngày bắt đầu],[Ngày kết thúc],Status";
            string PropertyFields = "Tên công việc,Người thực hiện,Người duyệt 1,Người duyệt 2,Ngày bắt đầu,Ngày kết thúc";
            string Condition = " and PK_DocumentID ='" + docID + "'";
            bus_Document doc = new bus_Document((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            DataSet dsCV = doc.getDocumentList(DOCTYPE.CONGVIEC_TRONG_DOTKIEMTOAN, DocFields, PropertyFields, Condition);
            if (isValidDataSet(dsCV) == false)
                return String.Empty;
            string nguoiduyet1 = dsCV.Tables[0].Rows[0]["Người duyệt 1"].ToString();
            if (((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]).UserName == nguoiduyet1)
                return "nguoiduyet1";
            string nguoiduyet2 = dsCV.Tables[0].Rows[0]["Người duyệt 2"] == null ?
               String.Empty : dsCV.Tables[0].Rows[0]["Người duyệt 2"].ToString();
            if (((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]).UserName == nguoiduyet2)
                return "nguoiduyet2";
            return String.Empty;
        }

        /// <summary>
        /// check la nguoiduyet1 hay nguoiduyet2
        /// </summary>
        /// <param name="docID"></param>
        /// <returns></returns>
        /// <author>thangma</author>quangna>
        public static string CheckNguoiDuyetOfCongViec(string docID, string UserName)
        {
            string DocFields = "PK_DocumentID,[Tên công việc],[Người thực hiện],[Người duyệt 1],[Người duyệt 2],[Ngày bắt đầu],[Ngày kết thúc],Status";
            string PropertyFields = "Tên công việc,Người thực hiện,Người duyệt 1,Người duyệt 2,Ngày bắt đầu,Ngày kết thúc";
            string Condition = " and PK_DocumentID ='" + docID + "'";
            bus_Document doc = new bus_Document((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            DataSet dsCV = doc.getDocumentList(DOCTYPE.CONGVIEC_TRONG_DOTKIEMTOAN, DocFields, PropertyFields, Condition);
            if (isValidDataSet(dsCV) == false)
                return String.Empty;
            string nguoiduyet1 = dsCV.Tables[0].Rows[0]["Người duyệt 1"].ToString();
            string nguoiduyet2 = dsCV.Tables[0].Rows[0]["Người duyệt 2"].ToString();
            if (UserName == nguoiduyet1)
                return "nguoiduyet1";
            if (UserName == nguoiduyet2)
                return "nguoiduyet2";
            return String.Empty;
        }

        /// <summary>
        /// Kiểm tra xem đợt kiểm toán đã phân tích hết các hồ sơ rủi ro chưa
        /// </summary>
        /// <param name="DotKiemToanID"></param>
        /// <returns></returns>
        /// <author>thangma</author>
        public static bool KiemTraDotDaPhanTichHetPTRR(string DotKiemToanID)
        {
            //bool DaPhanTichHetPTRR = false;
            bus_LapKeHoach lapKeHoach = bus_LapKeHoach.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            bus_Doankiemtoan doankiemtoan = bus_Doankiemtoan.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            DataSet dsDanhSachDoan = doankiemtoan.DanhSachDoanKTByDotKT(DotKiemToanID);
            if (isValidDataSet(dsDanhSachDoan))
            {
                //lấy đoàn của đợt
                string DoanKiemToanID = dsDanhSachDoan.Tables[0].Rows[0]["DoanKiemToanID"].ToString();
                DataSet dsDanhSachNhom = doankiemtoan.DanhSachNhomByDoanKT(DoanKiemToanID);
                if (isValidDataSet(dsDanhSachNhom))
                {
                    foreach (DataRow rowNhom in dsDanhSachNhom.Tables[0].Rows)
                    {
                        string NhomKiemToanID = rowNhom["NhomID"].ToString();
                        //lấy các hồ sơ rủi ro
                        DataSet dsHoSoRuiRo = lapKeHoach.ChiTietHoSoPhanTichRuiRo(NhomKiemToanID);
                        if (isValidDataSet(dsHoSoRuiRo))
                        {
                            //nếu chưa có hồ sơ rủi ro này thì đồng nghĩa là chưa phân tích
                            if (dsHoSoRuiRo.Tables[0].Rows.Count == 0)
                                return false;
                            DataRow[] foundRows;
                            string expression = "xac_suat > 0 and anh_huong > 0 and status = 4";
                            foundRows = dsHoSoRuiRo.Tables[0].Select(expression);
                            if (foundRows.Length != dsHoSoRuiRo.Tables[0].Rows.Count)
                            {
                                return false;
                            }
                        }
                        else
                            return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// check la nguoiduyet1 hay nguoiduyet2
        /// </summary>
        /// <param name="docID"></param>
        /// <returns></returns>
        /// <author>quangna</author>quangna>
        public static string CheckNguoiDuyet(string docID, string userNameLogin)
        {
            string DocFields = "PK_DocumentID,[Tên công việc],[Người thực hiện],[Người duyệt 1],[Người duyệt 2],[Ngày bắt đầu],[Ngày kết thúc],Status";
            string PropertyFields = "Tên công việc,Người thực hiện,Người duyệt 1,Người duyệt 2,Ngày bắt đầu,Ngày kết thúc";
            string Condition = " and PK_DocumentID ='" + docID + "'";
            bus_Document doc = new bus_Document((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            DataSet dsCV = doc.getDocumentList(DOCTYPE.CONGVIEC_TRONG_DOTKIEMTOAN, DocFields, PropertyFields, Condition);
            if (isValidDataSet(dsCV) == false)
                return String.Empty;
            string nguoiduyet1 = dsCV.Tables[0].Rows[0]["Người duyệt 1"].ToString();
            string nguoiduyet2 = dsCV.Tables[0].Rows[0]["Người duyệt 2"].ToString();
            if (userNameLogin == nguoiduyet1)
                return "nguoiduyet1";
            if (userNameLogin == nguoiduyet2)
                return "nguoiduyet2";
            return String.Empty;
        }

        /// <summary>
        /// set enabled control
        /// </summary>
        /// <param name="isEnabled"></param>
        /// <param name="Master">page Master</param>
        /// <author>quangna</author>
        public static void SetEnableControl(bool isEnabled, MasterPage Master)
        {
            Control frmWsp = Master.FindControl("frmWsp");
            Control FormContent = frmWsp.FindControl("FormContent");
            foreach (Control c in FormContent.Controls)
            {
                if (c is TextBox)
                {
                    TextBox txt = c as TextBox;
                    txt.Enabled = isEnabled;
                }
                if (c is DropDownList)
                {
                    DropDownList ddl = c as DropDownList;
                    ddl.Enabled = isEnabled;
                }
            }
        }

        /// <summary>
        /// set visiable control
        /// </summary>
        /// <param name="isEnabled"></param>
        /// <param name="Master">page Master</param>
        /// <author>quangna</author>
        public static void SetVisiableControl(bool isEnabled, MasterPage Master, Page page)
        {
            Control frmWsp = Master.FindControl("frmWsp");
            Control FormContent = frmWsp.FindControl("FormContent");
            foreach (Control c in FormContent.Controls)
            {
                if (c is TextBox)
                {
                    TextBox txt = c as TextBox;
                    txt.Enabled = isEnabled;
                }
                if (c is DropDownList)
                {
                    DropDownList ddl = c as DropDownList;
                    ddl.Enabled = isEnabled;
                }
            }

            foreach (Control c in page.Controls)
            {
                if (c is Button)
                {
                    Button btn = c as Button;
                    if (btn.Visible)
                        btn.Visible = false;
                }
            }
        }

        /// <summary>
        /// get trang thai dot kiem toan
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        /// <author>quangna</author>
        public static string GetTrangThaiDotKT(int status)
        {
            string re = String.Empty;
            if (status == 11)
                re = "Lập kế hoạch - Lập đoàn KT";
            if (status == 12)
                re = "Lập kế hoạch - Nhập thông tin đợt KT";
            if (status == 13)
                re = "Lập kế hoạch - Phân tích sơ bộ";
            if (status == 14)
                re = "Lập kế hoạch - Phân tích chi tiết";
            if (status == 141)
                re = "Lập kế hoạch - Xin BLĐ duyệt lập chương trình KT";

            if (status == 15)
                re = "Lập kế hoạch - Lập chương trình KT";

            if (status == 21)
                re = "Thực hiện - Cập nhật phát hiện";
            if (status == 22)
                re = "Thực hiện - Cập nhật công việc (chuyển duyệt)";
            if (status == 23)
                re = "Thực hiện - Đánh giá rr còn lại";
            if (status == 231)
                re = "Thực hiện - Xin BLĐ duyệt xuất BBKT";
            if (status == 24)
                re = "Thực hiện - Xuất biên bản kết thúc KT";
            if (status == 25)
                re = "Thực hiện - Cập nhật phản hồi";
            if (status == 26)
                re = "Thực hiện - Xin phê duyệt đóng đợt KT";

            if (status == 31)
                re = "Lập BC (hoàn thành) - Đóng đợt KT";

            if (status == 41)
                re = "Đánh giá - Tự đánh giá";
            if (status == 42)
                re = "Đánh giá - Đánh giá chéo";
            if (status == 43)
                re = "Đánh giá - Đánh giá của lãnh đạo";
            if (status == 44)
                re = "Đánh giá - Đánh giá HSRR";

            if (status == 51)
                re = "Giám sát khắc phục - Cập nhật thông tin khắc phục";
            if (status == 52)
                re = String.Empty;
            if (status == 53)
                re = "Giám sát khắc phục - Lập BC khắc phục";
            return re;
        }

        /// <summary>
        /// lay trang thai dot kiem toan
        /// </summary>
        /// <param name="lbl"></param>
        /// <param name="docID"></param>
        /// <author>quangna</author>
        public static void SetTrangThaiDotKT(Label lbl, string docID_dotkt)
        {
            if (string.IsNullOrEmpty(docID_dotkt))
                return;
            string re = string.Empty;
            int status = CommonFunc.GetDocStatus(docID_dotkt);
            if (status == 11)
                re = "Lập kế hoạch - Lập đoàn KT";
            if (status == 12)
                re = "Lập kế hoạch - Nhập thông tin đợt KT";
            if (status == 13)
                re = "Lập kế hoạch - Phân tích sơ bộ";
            if (status == 14)
                re = "Lập kế hoạch - Phân tích chi tiết";
            if (status == 141)
                re = "Lập kế hoạch - Xin BLĐ duyệt lập chương trình KT";

            if (status == 15)
                re = "Lập kế hoạch - Lập chương trình KT";

            if (status == 21)
                re = "Thực hiện - Cập nhật phát hiện";
            if (status == 22)
                re = "Thực hiện - Cập nhật công việc (chuyển duyệt)";
            if (status == 23)
                re = "Thực hiện - Đánh giá rr còn lại";
            if (status == 231)
                re = "Thực hiện - Xin BLĐ duyệt xuất BBKT";

            if (status == 24)
                re = "Thực hiện - Xuất biên bản kết thúc KT";
            if (status == 25)
                re = "Thực hiện - Cập nhật phản hồi";
            if (status == 26)
                re = "Thực hiện - Xin phê duyệt đóng đợt KT";

            if (status == 31)
                re = "Lập BC (hoàn thành) - Đóng đợt KT";

            if (status == 41)
                re = "Đánh giá - Tự đánh giá";
            if (status == 42)
                re = "Đánh giá - Đánh giá chéo";
            if (status == 43)
                re = "Đánh giá - Đánh giá của lãnh đạo";
            if (status == 44)
                re = "Đánh giá - Đánh giá HSRR";

            if (status == 51)
                re = "Giám sát khắc phục - Cập nhật thông tin khắc phục";
            if (status == 52)
                re = String.Empty;
            if (status == 53)
                re = "Giám sát khắc phục - Lập BC khắc phục";
            lbl.Text = re;
        }

        /// <summary>
        /// lay trang thai cong viec
        /// </summary>
        /// <param name="lbl"></param>
        /// <param name="docID"></param>
        /// <author>quangna</author>
        public static void SetTrangThaiCongViec(Label lbl, string status)
        {
            string trangthai = String.Empty;
            //int status = CommonFunc.GetDocStatus(docID);
            if (status == "0") trangthai = "Draft";
            if (status == "2") trangthai = "Not started";
            if (status == "4") trangthai = "Inprogress";
            if (status == "8") trangthai = "Rejected";
            if (status == "16") trangthai = "Done";
            if (status == "32") trangthai = "Approved";
            if (status == "64") trangthai = "Approved2";
            //if (status == "128") trangthai = "Rejected";
            lbl.Text = trangthai;
        }

        /// <summary>
        /// lay trang thai cong viec
        /// </summary>
        /// <param name="lbl"></param>
        /// <param name="docID"></param>
        /// <author>quangna</author>
        public static string GetTrangThaiCongViec(string docID)
        {
            string trangthai = string.Empty;
            int status = CommonFunc.GetDocStatus(docID);
            if (status == 0) trangthai = "Draft";
            if (status == 2) trangthai = "Not started";
            if (status == 4) trangthai = "Inprogress";
            if (status == 8) trangthai = "Rejected";
            if (status == 16) trangthai = "Done";
            if (status == 32) trangthai = "Approved";
            if (status == 64) trangthai = "Approved2";
            //if (status == "128") trangthai = "Rejected";
            return trangthai;
        }

        /// <summary>
        /// lay trang thai phat hien
        /// </summary>
        /// <param name="lbl"></param>
        /// <param name="docID"></param>
        /// <author>quangna</author>
        public static void SetTrangThaiPhatHien(Label lbl, string status)
        {
            string trangthai = String.Empty;
            //int status = CommonFunc.GetDocStatus(docID);
            if (status == "0") trangthai = "Draft";
            //if (status == "1") trangthai = "Rejected";
            if (status == "2") trangthai = "Done";
            if (status == "4") trangthai = "Approved";
            if (status == "16") trangthai = "Approved2";
            if (status == "32") trangthai = "Rejected";
            lbl.Text = trangthai;
        }

        /// <summary>
        /// lay trang thai phat hien
        /// </summary>
        /// <param name="lbl"></param>
        /// <param name="docID"></param>
        /// <author>quangna</author>
        public static string GetTrangThaiPhatHien(string docID)
        {
            string trangthai = String.Empty;
            int status = CommonFunc.GetDocStatus(docID);
            if (status == 0) trangthai = "Draft";
            //if (status == "1") trangthai = "Rejected";
            if (status == 2) trangthai = "Done";
            if (status == 4) trangthai = "Approved";
            if (status == 16) trangthai = "Approved2";
            if (status == 32) trangthai = "Rejected";
            return trangthai;
        }

        /// <summary>
        /// lay trang thai phan hoi
        /// </summary>
        /// <param name="lbl"></param>
        /// <param name="docID"></param>
        /// <author>quangna</author>
        public static void SetTrangThaiPhanHoi(Label lbl, string status)
        {
            string trangthai = String.Empty;
            //int status = CommonFunc.GetDocStatus(docID);
            if (status == "0") trangthai = "Draft";
            //if (status == "1") trangthai = "Rejected";
            if (status == "2") trangthai = "Done";
            if (status == "4") trangthai = "Approved";
            if (status == "16") trangthai = "Approved2";
            if (status == "32") trangthai = "Rejected";
            lbl.Text = trangthai;
        }

        public static bool CheckAllCongViecApprovedByDoanKT(string doankt)
        {
            //get nhomkt by doankt
            if (String.IsNullOrEmpty(doankt))
                return false;
            string DocFields = "PK_DocumentID,Status";
            string PropertyFields = "";
            string Condition = " and PK_DocumentID in (Select FK_documentID From T_DocLink Where FK_DocLinkID='" + doankt + "')";
            bus_Document bus = new bus_Document((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            DataSet dsCV = bus.getDocumentList(DOCTYPE.CONGVIEC_TRONG_DOTKIEMTOAN, DocFields, PropertyFields, Condition);
            if (isValidDataSet(dsCV) == false)
                return true;
            int sumCV = dsCV.Tables[0].Rows.Count;
            int sumCV_Approved = dsCV.Tables[0].Select("Status >= 32").Length;
            if (sumCV == sumCV_Approved)
                return true;
            return false;
        }

        #endregion for KTNB

        /// <summary>
        /// Tao moi mot User
        /// </summary>
        /// <param></param>
        /// <returns>ReturnValue</returns>
        public static int CreateNewUser(string fullname, string email, string mobile, string desc, string uname,
            string ucode, string pwd, string cn, string pgd, string vaitro, byte exp, string edu, DateTime joinDate,
            DateTime birthDate)
        {
            int returnvalue = 0;
            string strConn = ConfigurationManager.AppSettings["ConnectionString"].ToString();
            SqlConnection conn = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand("Users_AddNewUser_dev", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("PK_UserID", SqlDbType.UniqueIdentifier).Value = System.Guid.NewGuid();
            cmd.Parameters.Add("UserCode", SqlDbType.NVarChar).Value = ucode;
            cmd.Parameters.Add("Name", SqlDbType.NVarChar).Value = uname;
            cmd.Parameters.Add("PassWord", SqlDbType.NVarChar).Value = pwd;
            cmd.Parameters.Add("Description", SqlDbType.NVarChar).Value = desc;
            cmd.Parameters.Add("IsExpired", SqlDbType.TinyInt).Value = exp;
            cmd.Parameters.Add("Email", SqlDbType.NVarChar).Value = email;
            cmd.Parameters.Add("MobilePhone", SqlDbType.NVarChar).Value = mobile;
            cmd.Parameters.Add("Order_Number", SqlDbType.NVarChar).Value = "0";
            cmd.Parameters.Add("Fullname", SqlDbType.NVarChar).Value = fullname.Replace(",", "");
            cmd.Parameters.Add("Address", SqlDbType.NVarChar).Value = "";
            cmd.Parameters.Add("IsAuthenticateSQL", SqlDbType.TinyInt).Value = 0;
            cmd.Parameters.Add("AvatarURL", SqlDbType.NVarChar).Value = "";
            cmd.Parameters.Add("JoinDate", SqlDbType.NVarChar).Value = joinDate;
            cmd.Parameters.Add("BirthDate", SqlDbType.NVarChar).Value = birthDate;
            cmd.Parameters.Add("EducationLevel", SqlDbType.NVarChar).Value = edu;
            cmd.Parameters.Add("@GroupID", SqlDbType.Char).Value = pgd;
            cmd.Parameters.Add("@RoleID", SqlDbType.NVarChar).Value = vaitro;
            cmd.Parameters.Add("@ReturnValue", SqlDbType.Int);
            cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;
            conn.Open();
            cmd.ExecuteNonQuery();
            returnvalue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
            conn.Close();
            return returnvalue;
        }

        #region Helper methods

        public static void ShowMessage(string messagebox)
        {
            StringBuilder stringbuilder = new StringBuilder();
            stringbuilder.Append("<script language='javascript'>");
            stringbuilder.Append("alert(\"" + messagebox + "\");");
            stringbuilder.Append("<" + "/script>");
            System.Web.HttpContext.Current.Response.Write(stringbuilder.ToString());
        }

        /// <summary>
        /// openUrl
        /// </summary>
        /// <param name="url"></param>
        private static void openUrl(string url, string target)
        {
            System.Text.StringBuilder script = new System.Text.StringBuilder();
            script.Append("<script type=\"text/javascript\">");
            script.Append("window.open('" + url + "','" + target + "')");
            script.Append("</script>");
            System.Web.HttpContext.Current.Response.Write(script.ToString());
        }

        /// <summary>
        /// getLastestVersion
        /// </summary>
        /// <param name="DocumentID"></param>
        /// <param name="objUserContext"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        private static string getLastestVersion(string DocumentID, UserContext objUserContext, string dbName)
        {
            bus_Doc_Version objVersion = bus_Doc_Version.Instance(objUserContext);  //bus_Doc_Version objVersion = new bus_Doc_Version(objUserContext, dbName);
            DataSet dsVersion = objVersion.getList(" AND FK_DocumentID='" + DocumentID + "' ORDER BY VersionNumber DESC", " TOP 1 PK_VersionID");

            if ((dsVersion == null) || (dsVersion.Tables[0].Rows.Count == 0))
                return string.Empty;

            return dsVersion.Tables[0].Rows[0]["PK_VersionID"].ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        private static bool isValidDataSet(DataSet ds)
        {
            bool valid = false;
            if (ds != null)
                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                        valid = true;
            return valid;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="T24_DateFrom"></param>
        /// <param name="T24_DateTo"></param>
        /// <returns></returns>
        public static int dateDiffDay(string T24_DateFrom, string T24_DateTo)
        {
            DateTime dateFrom, dateTo;
            TimeSpan span;
            try
            {
                dateFrom = DateTime.ParseExact(T24_DateFrom, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None);
                dateTo = DateTime.ParseExact(T24_DateTo, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None);
                span = dateTo.Subtract(dateFrom);
            }
            catch (Exception ex)
            { return 0; }

            return span.Days;
        }

        /// <summary>
        /// convert Datatable to JSON
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        /// <author>thangma</author>
        public static string GetJson(DataTable dt)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new

            System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows =
              new List<Dictionary<string, object>>();
            Dictionary<string, object> row = null;

            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName.Trim(), dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
        }

        /// <summary>
        /// Danh sách trạng thái cho Công việc, phát hiện, phản hồi
        /// </summary>
        /// <returns></returns>
        /// <author>thangma</author>
        public static List<string> GetDanhSachTrangThai()
        {
            List<string> TrangThaiList = new List<string>();
            TrangThaiList.Add("Chưa bắt đầu");
            TrangThaiList.Add("Đang thực hiện");
            TrangThaiList.Add("Hoàn thành, chờ duyệt");
            TrangThaiList.Add("Đã phê duyệt (bởi người duyệt 1)");
            TrangThaiList.Add("Đã phê duyệt (bởi người duyệt 2)");
            TrangThaiList.Add("Bị từ chối");

            return TrangThaiList;
        }

        #endregion Helper methods
    }

    public enum TrangThaiDacBiet
    {
        ChoDuyetHSRR = 141,
        ChoDuyetXuatBB = 231,
        ChoDuyetReview = 26
    }

    /// <summary>
    /// Class xu ly JSON
    /// </summary>
    public class JSONHelper
    {
        /// <summary>
        /// xu ly tu object sang string co dinh dang JSON
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serialize<T>(T obj)
        {
            System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(obj.GetType());
            MemoryStream ms = new MemoryStream();
            serializer.WriteObject(ms, obj);
            string retVal = Encoding.UTF8.GetString(ms.ToArray());
            ms.Dispose();
            return retVal;
        }

        /// <summary>
        /// xu ly tu string co dinh dang JSON sang object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string json)
        {
            T obj = Activator.CreateInstance<T>();
            MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
            System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(obj.GetType());
            obj = (T)serializer.ReadObject(ms);
            ms.Close();
            ms.Dispose();
            return obj;
        }
    }

    /// <summary>
    /// Class kiểm tra trạng thái đợt kiểm toán
    /// </summary>
    /// <author>thangma</author>
    public class TrangThaiDotKiemToan
    {
        //tất cả các trạng thái
        private static readonly int[] _TatCa = { 11, 12, 13, 14, 141, 15, 21, 22, 23, 231, 24, 25, 26, 31, 41, 42, 43, 44, 51, 52, 53 };

        // thêm/bỏ mảng nghiệp vụ
        private static readonly int[] _KhongThemMangNghiepVu = { 141, 231, 26, 31, 41, 42, 43, 44, 51, 52, 53 };

        private static readonly int[] _KhongXoaMangNghiepVu = { 12, 13, 14, 141, 15, 21, 22, 23, 231, 24, 25, 26, 31, 41, 42, 43, 44, 51, 52, 53 };

        // thêm/xóa phát hiện
        private static readonly int[] _KhongThemPhatHien = { 11, 12, 13, 14, 141, 15, 231, 24, 25, 26, 41, 42, 43, 44, 51, 52, 53 };

        private static readonly int[] _KhongXoaPhatHien = { 141, 22, 23, 24, 25, 26, 31, 41, 42, 43, 44, 51, 52, 53 };

        // đánh giá rủi ro còn lại
        private static readonly int[] _KhongDanhGiaRRCL = { 11, 12, 13, 14, 141, 15, 231, 26, 31, 41, 42, 43, 44, 51, 52, 53 };

        // phản hồi đơn vị
        private static readonly int[] _CoThemPhanHoiDonVi = { 21, 22, 23, 24, 25 };

        private static readonly int[] _CoTheXoaPhanHoiDonVi = { 21, 22, 23, 24, 25 };

        // phản hồi biên bản(role trưởng đoàn)
        private static readonly int[] _CoThemPhanHoiBienBan = { 24, 25 };

        private static readonly int[] _CoTheXoaPhanHoiBienBan = { 24, 25 };

        //Check Services
        public static bool IsCotheThemMangNghiepVu(int StatusDotKiemToan)
        {
            bool KhongTheThemMNV = Array.IndexOf(_KhongThemMangNghiepVu, StatusDotKiemToan) >= 0;
            return !KhongTheThemMNV;
        }

        public static bool IsCoTheXoaMangNghiepVu(int StatusDotKiemToan)
        {
            bool KhongTheXoaMNV = Array.IndexOf(_KhongXoaMangNghiepVu, StatusDotKiemToan) >= 0;
            return !KhongTheXoaMNV;
        }

        public static bool IsCoTheThemPhatHien(int StatusDotKiemToan)
        {
            bool IsKhongThe = Array.IndexOf(_KhongThemPhatHien, StatusDotKiemToan) >= 0;
            return !IsKhongThe;
        }

        public static bool IsCoTheXoaPhatHien(int StatusDotKiemToan)
        {
            bool isKhongTheXoa = Array.IndexOf(TrangThaiDotKiemToan._KhongXoaPhatHien, StatusDotKiemToan) >= 0;
            return !isKhongTheXoa;
        }

        public static int[] KhongDanhGiaRRCL()
        {
            return _KhongDanhGiaRRCL;
        }

        public static bool IsCoTheThemPhanHoiDonVi(int StatusDotKiemToan)
        {
            bool isCoTheThem = Array.IndexOf(TrangThaiDotKiemToan._CoThemPhanHoiDonVi, StatusDotKiemToan) >= 0;
            return isCoTheThem;
        }

        public static bool IsCoTheXoaPhanHoiDonVi(int StatusDotKiemToan)
        {
            bool isCoTheXoa = Array.IndexOf(TrangThaiDotKiemToan._CoThemPhanHoiDonVi, StatusDotKiemToan) >= 0;
            return isCoTheXoa;
        }

        public static bool IsCoTheThemPhanHoiBienBan(int StatusDotKiemToan)
        {
            bool isCoTheThem = Array.IndexOf(TrangThaiDotKiemToan._CoThemPhanHoiBienBan, StatusDotKiemToan) >= 0;
            return isCoTheThem;
        }

        public static bool IsCoTheXoaPhanHoiBienBan(int StatusDotKiemToan)
        {
            bool isCoTheXoa = Array.IndexOf(TrangThaiDotKiemToan._CoTheXoaPhanHoiBienBan, StatusDotKiemToan) >= 0;
            return isCoTheXoa;
        }

        /// <summary>
        /// Set Trạng thái của đợt kiểm toán kèm theo kiểm tra xem có đủ điều kiện để set không
        /// </summary>
        /// <param name="dotkt"></param>
        /// <param name="trangthai"></param>
        /// <returns></returns>
        /// <author>thangma</author>
        public static string SetStatus(string dotkt, int trangthai)
        {
            //thangma: update Trang thai Dot kiem toan
            int currentStatus = CommonFunc.GetDocStatus(dotkt);
            int indexOfCurrentStatus = Array.IndexOf(_TatCa, currentStatus);

            int validStatusToSet = _TatCa[indexOfCurrentStatus + 1];
            if (trangthai == validStatusToSet)
            {
                CommonFunc.UpdateDocStatus(dotkt, trangthai);
                return "1";
            }
            return "0";
            //end
        }
    }

    /// <summary>
    /// Class dac ta Phat hien trong bao cao bien ban ket thuc kiem toan
    /// </summary>
    [DataContract]
    public class PhatHienBaoCaoBienBan
    {
        public PhatHienBaoCaoBienBan()
        {
        }

        public PhatHienBaoCaoBienBan(string phatHienID, string tenPhatHien, string mucDo, string danChieu, string phanHoiDonVi, string trangthai)
        {
            this.PhatHienID = phatHienID;
            this.TenPhatHien = tenPhatHien;
            this.DanChieu = danChieu;
            this.MucDo = mucDo;
            this.PhanHoiDonVi = phanHoiDonVi;
            this.TrangThai = trangthai;
        }

        public PhatHienBaoCaoBienBan(string phatHienID, string tenPhatHien, string mucDo, string danChieu)
        {
            this.PhatHienID = phatHienID;
            this.TenPhatHien = tenPhatHien;
            this.DanChieu = danChieu;
            this.MucDo = mucDo;
        }

        [DataMember]
        public string PhatHienID { get; set; }

        [DataMember]
        public string TenPhatHien { get; set; }

        [DataMember]
        public string DanChieu { get; set; }

        [DataMember]
        public string MucDo { get; set; }

        [DataMember]
        public string PhanHoiDonVi { get; set; }

        [DataMember]
        public string TrangThai { get; set; }
    }

    [DataContract]
    public class MangNghiepVuInBienBan
    {
        public MangNghiepVuInBienBan()
        {
        }

        [DataMember]
        public string MangNghiepVuID { get; set; }

        [DataMember]
        public string TenMangNghiepVu { get; set; }

        [DataMember]
        public List<PhatHienBaoCaoBienBan> PhatHienBienBanList { get; set; }
    }

    /// <summary>
    /// Class đặc tả Nhận xét
    /// </summary>
    [DataContract]
    public class NhanXet
    {
        public NhanXet()
        {
        }

        public NhanXet(string lyDo, string nguoiNhanXet, string ngayNhanXet, string hanhDong)
        {
            this.LyDo = lyDo;
            this.NguoiNhanXet = nguoiNhanXet;
            this.NgayNhanXet = ngayNhanXet;
            this.HanhDong = hanhDong;
        }

        [DataMember]
        public string LyDo { get; set; }

        [DataMember]
        public string NguoiNhanXet { get; set; }

        [DataMember]
        public string NgayNhanXet { get; set; }

        //Value: "PheDuyet" or "TuChoi"
        [DataMember]
        public string HanhDong { get; set; }
    }

    /// <summary>
    /// Class dac ta Phat hien cho bao cao chi tiet-sobo
    /// </summary>
    /// <author>quangna</author>
    [DataContract]
    public class PhatHien_BaoCao_ChiTiet_SoBo
    {
        public PhatHien_BaoCao_ChiTiet_SoBo()
        {
        }

        public PhatHien_BaoCao_ChiTiet_SoBo(string phatHienID, string tenPhatHien, string mucDo, string danChieu, string phanHoiDonVi,
            string nguyennhan, string anhhuong, string khuyennghi, string trangthai)
        {
            this.PhatHienID = phatHienID;
            this.TenPhatHien = tenPhatHien;
            this.DanChieu = danChieu;
            this.MucDo = mucDo;
            this.NguyenNhan = nguyennhan;
            this.AnhHuong = anhhuong;
            this.KhuyenNghi = khuyennghi;
            this.TrangThai = trangthai;
        }

        [DataMember]
        public string PhatHienID { get; set; }

        [DataMember]
        public string TenPhatHien { get; set; }

        [DataMember]
        public string DanChieu { get; set; }

        [DataMember]
        public string MucDo { get; set; }

        [DataMember]
        public string NguyenNhan { get; set; }

        [DataMember]
        public string AnhHuong { get; set; }

        [DataMember]
        public string KhuyenNghi { get; set; }

        [DataMember]
        public string TrangThai { get; set; }
    }

    /// <summary>
    /// class phuc vu bao cao chi tiet-sobo
    /// </summary>
    /// <author>quangna</author>
    [DataContract]
    public class MangNghiepVuInBaoCao_ChiTiet_SoBo
    {
        public MangNghiepVuInBaoCao_ChiTiet_SoBo()
        {
        }

        [DataMember]
        public string MangNghiepVuID { get; set; }

        [DataMember]
        public string TenMangNghiepVu { get; set; }

        [DataMember]
        public List<PhatHien_BaoCao_ChiTiet_SoBo> PhatHienBienBanList { get; set; }
    }

    [DataContract]
    public class DiemDanhGiaXacSuatAnhHuong
    {
        public DiemDanhGiaXacSuatAnhHuong()
        {
        }

        public DiemDanhGiaXacSuatAnhHuong(string diem, string ten)
        {
            this.Diem = diem;
            this.Ten = ten;
        }

        [DataMember]
        public string Diem { get; set; }

        [DataMember]
        public string Ten { get; set; }
    }
}