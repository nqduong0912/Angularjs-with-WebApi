using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.Definition.DMS;
using vpb.app.business.ktnb.CoreBusiness;
using System.Data;
using C1.Web.C1Input;
using VPB_KTNB.Helpers;

namespace VPB_TDHS.Modules.Lookup
{
    public partial class MucDoRuiRo_Load : PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;
        protected string _doctypeid = DOCTYPE.MUCDO_RUIRO;
        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;
        protected string _docspaceid = string.Empty;
        protected string _property = string.Empty;
        protected string _propertyvalue = string.Empty;
        protected string _diemchantren = string.Empty;
        protected string _diemchanduoi = string.Empty;
        protected string _tenruiro = string.Empty;

        #endregion

        #region page init & load
        /// <summary>
        /// OnInit
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            base.AuthorizeUserCtx();

            #region get data submit
            _documentid = Request["doc"];
            _action = Request["act"];
            _property = Request["p"];
            _diemchantren = Request["diemchantren"];
            _diemchanduoi = Request["diemchanduoi"];
            _tenruiro = Request["tenruiro"];

            #endregion

            if (string.IsNullOrEmpty(_documentid))
                _viewtype = VIEWTYPE.ADDNEW;
            else
                _viewtype = VIEWTYPE.EDIT;

            #region action handler
            if (!string.IsNullOrEmpty(_action))
            {
                if (_action == "checkvalue")
                    FeedBackClient(CommonFunc.CountPropertyValue(_property, _propertyvalue).ToString());
                if (_action == "checkvalueupdate")
                    FeedBackClient(CommonFunc.CountPropertyValue(_property, _propertyvalue,_documentid).ToString());
                if (_action == "adddoc")
                    FeedBackClient(AddDoc(_diemchantren, _diemchanduoi, _tenruiro));
                if (_action == "updatedoc")
                    FeedBackClient(UpdateDoc(_diemchantren, _diemchanduoi, _tenruiro,_documentid));
            }
            #endregion

            #region init form
            string caption = "Thêm mới mức độ rủi ro";
            if (!string.IsNullOrEmpty(_documentid))
                caption = "Thông tin mức độ rủi ro";
            base.InitForm(caption, string.Empty, _doctypeid, _viewtype);
            #endregion

            #region client control event handler
            //_btnSave.Attributes.Add("onclick", "{preparesavedoc('" + System.Guid.NewGuid().ToString() + "','" + _doctypeid + "'); return false;}");
           // _btnEdit.Attributes.Add("onclick", "{updatedocument('" + _documentid + "',update_success,update_error); return false;}");
           // _btnEdit.Attributes.Add("onclick", "{prepareupdatedoc('" + _documentid + "'); return false;}");

            _btnSave.Attributes.Add("onclick", "{adddoc('" + System.Guid.NewGuid().ToString() + "','" + _doctypeid + "'); return false;}");
            _btnEdit.Attributes.Add("onclick", "{updatedoc('" + _documentid + "'); return false;}");
            _btnDelete.Attributes.Add("onclick", "{deletedocument('" + _documentid + "',delete_success,delete_error); return false;}");
            #endregion
        }
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CommonFunc.LoadStatus(this.DOCSTATUS);
                if (!string.IsNullOrEmpty(_action))
                    if (_action == "loaddoc")
                    {
                        CommonFunc.LoadDocInfo(_documentid, Page.Master);
                        _btnDelete.Visible = false;
                    }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="documentid"></param>
        public void LoadDocInfo(string documentid, MasterPage Master)
        {
            bus_Document obj = bus_Document.Instance(_objUserContext);
            DataSet ds = obj.loadDocumentInfo(documentid);
            obj = null;
            Control frmWsp = Master.FindControl("frmWsp");
            Control FormContent = frmWsp.FindControl("FormContent");
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
                #endregion

                #region Properties
                if (PropertyID.Length == 40)
                {
                    string stype = StringHelper.Left(PropertyID, 3);
                    PropertyID = StringHelper.Right(PropertyID, 36).Replace("_", "-");
                    DataRow[] R = ds.Tables[0].Select("FK_PropertyID='" + PropertyID + "'");

                    if (R.Length == 0) continue;

                    if (stype == "ID8")
                    {
                        if (ctl.GetType() == typeof(TextBox))
                            ((TextBox)ctl).Text = R[0]["TextValue"].ToString();
                        else if (ctl.GetType() == typeof(DropDownList))
                            ((DropDownList)ctl).SelectedValue = R[0]["TextValue"].ToString();
                        else if (ctl.GetType() == typeof(C1WebNumericEdit))
                            ((C1WebNumericEdit)ctl).Text = R[0]["TextValue"].ToString();
                    }
                    else if (stype == "ID6")
                    {
                        if (ctl.GetType() == typeof(TextBox))
                            ((TextBox)ctl).Text = R[0]["NumericValue"].ToString();
                        else if (ctl.GetType() == typeof(C1WebNumericEdit))
                            ((C1WebNumericEdit)ctl).Text = Math.Round(Convert.ToDecimal(R[0]["NumericValue"]), 0).ToString();
                    }
                    else if (stype == "ID3")
                    {
                        if (ctl.GetType() == typeof(TextBox))
                            ((TextBox)ctl).Text = String.Format("{0:dd/MM/yyyy}", R[0]["DateTimeValue"]);
                    }
                }
                #endregion
            }
        }
        #endregion

        #region page helper processing

        #endregion

        #region page button processing

        #endregion

        #region checkchantren chanduoi
        public string AddDoc(string diemchantren,string diemchanduoi,string tenruiro)
        {
            string data = "1";
            if (isExistTenRuiRo_Add(tenruiro))
            {
                data = "3";
                return data;
            }
            if (isDiemChanTrenChanDuoi_Add(diemchantren, diemchanduoi))
            {
                data = "0";
                return data;
            }
            return data;
        }

        public string UpdateDoc(string diemchantren, string diemchanduoi, string tenruiro,string docID)
        {
            string data = "1";
            if (isExistTenRuiRo_Update(tenruiro,docID))
            {
                data = "3";
                return data;
            }
            if (isDiemChanTrenChanDuoi_Update(diemchantren, diemchanduoi,docID))
            {
                data = "0";
                return data;
            }
            return data;
        }

        bool isDiemChanTrenChanDuoi_Update(string diemchantren,string diemchanduoi,string docID)
        {
            int iDiemTren = Int32.Parse(diemchantren);
            int iDiemDuoi = Int32.Parse(diemchanduoi);
            string DocFields = "PK_DocumentID,Status,[Tên Mức độ rủi ro],[Điểm rủi ro (Chặn trên)],[Điểm rủi ro (Chặn dưới)],[Tần suất kiểm toán]";
            string PropertyFields = "Tên Mức độ rủi ro,Điểm rủi ro (Chặn trên),Điểm rủi ro (Chặn dưới),Tần suất kiểm toán";
            string Condition = " and PK_DocumentID != '" + docID + "' And Status = 4";
            bus_Document doc = new bus_Document(_objUserContext);
            DataSet ds = doc.getDocumentList(DOCTYPE.MUCDO_RUIRO, DocFields, PropertyFields, Condition);
            if (isValidDataSet(ds))
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    int rowDiemTren = GetDiem(row, "Điểm rủi ro (Chặn trên)");
                    int rowDiemDuoi = GetDiem(row, "Điểm rủi ro (Chặn dưới)");
                    if ((iDiemDuoi >= rowDiemDuoi && iDiemDuoi <= rowDiemTren) || (iDiemTren >= rowDiemDuoi && iDiemTren <= rowDiemTren))
                        return true;
                }
            }
            return false;
        }

        int GetDiem(DataRow row,string columnName)
        {
            string strDiem = row[columnName] == null ? String.Empty : row[columnName].ToString();
            strDiem = strDiem.IndexOf(".") > 0 ? strDiem.Substring(0, strDiem.IndexOf(".")) : strDiem;
            //them vao 4/5 dungnq
            strDiem = strDiem.IndexOf(",") > 0 ? strDiem.Substring(0, strDiem.IndexOf(",")) : strDiem;
            
            int rowDiem = String.IsNullOrEmpty(strDiem) ? 0 : Int32.Parse(strDiem);
            return rowDiem;
        }

        bool isDiemChanTrenChanDuoi_Add(string diemchantren, string diemchanduoi)
        {
            int iDiemTren = Int32.Parse(diemchantren);
            int iDiemDuoi = Int32.Parse(diemchanduoi);
            string DocFields = "PK_DocumentID,Status,[Tên Mức độ rủi ro],[Điểm rủi ro (Chặn trên)],[Điểm rủi ro (Chặn dưới)],[Tần suất kiểm toán]";
            string PropertyFields = "Tên Mức độ rủi ro,Điểm rủi ro (Chặn trên),Điểm rủi ro (Chặn dưới),Tần suất kiểm toán";
            string Condition = " and Status = 4";
            bus_Document doc = new bus_Document(_objUserContext);
            DataSet ds = doc.getDocumentList(DOCTYPE.MUCDO_RUIRO, DocFields, PropertyFields, Condition);
            if (isValidDataSet(ds))
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    int rowDiemTren = GetDiem(row, "Điểm rủi ro (Chặn trên)");
                    int rowDiemDuoi = GetDiem(row, "Điểm rủi ro (Chặn dưới)");
                    if ((iDiemDuoi >= rowDiemDuoi && iDiemDuoi <= rowDiemTren) || (iDiemTren >= rowDiemDuoi && iDiemTren <= rowDiemTren))
                        return true;
                }
            }
            return false;
        }

        bool isExistTenRuiRo_Add(string tenruiro)
        {
            string DocFields = "PK_DocumentID,Status,[Tên Mức độ rủi ro]";
            string PropertyFields = "Tên Mức độ rủi ro";
            string Condition = " and [Tên Mức độ rủi ro]=N'"+tenruiro+"'";
            bus_Document doc = new bus_Document(_objUserContext);
            DataSet ds = doc.getDocumentList(DOCTYPE.MUCDO_RUIRO, DocFields, PropertyFields, Condition);
            if (isValidDataSet(ds))
            {
                return true;
            }
            return false;
        }

        bool isExistTenRuiRo_Update(string tenruiro,string docID)
        {
            string DocFields = "PK_DocumentID,Status,[Tên Mức độ rủi ro]";
            string PropertyFields = "Tên Mức độ rủi ro";
            string Condition = " and PK_DocumentID !='" + docID + "'";
            bus_Document doc = new bus_Document(_objUserContext);
            DataSet ds = doc.getDocumentList(DOCTYPE.MUCDO_RUIRO, DocFields, PropertyFields, Condition);
            if (isValidDataSet(ds))
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if (row["Tên Mức độ rủi ro"].ToString() == tenruiro)
                        return true;
                }
            }
            return false;
        }

        #endregion
    }
}