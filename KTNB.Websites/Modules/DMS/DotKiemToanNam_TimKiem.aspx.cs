using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.Definition.DMS;
using C1.Web.C1WebGrid;
using vpb.app.business.ktnb.CoreBusiness;
using System.Data;
using CORE.CoreObjectContext;
using vpb.app.business.ktnb.Definition.UMS;
using vpb.app.business.ktnb.Definition.OPERATORS;
using System.Globalization;
using VPB_KTNB.Helpers;

namespace VPB_TDHS.Modules.DMS
{
    public partial class DotKiemToanNam_TimKiem : PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;
        protected string _doctypeid = DOCTYPE.DOT_KIEMTOAN;
        protected string _doctypeid_doankiemtoan = DOCTYPE.DOAN_KIEMTOAN;

        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;
        protected string _docspaceid = string.Empty;

        protected string _tungay = string.Empty;
        protected string _denngay = string.Empty;
        protected string _loaidoituong = string.Empty;

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
            _documentid = Request["doc"];
            _action = Request["act"];
            _tungay = Request["tungay"];
            _denngay = Request["denngay"];
            _loaidoituong = Request["loaidoituong"];

            if (string.IsNullOrEmpty(_documentid))
                _viewtype = VIEWTYPE.ADDNEW;
            else
                _viewtype = VIEWTYPE.EDIT;

            #region get data submit
            #endregion

            #region action handler
            if (!string.IsNullOrEmpty(_action))
            {
                if (_action == "timkiemdotkt")
                {
                    //GetList();
                    //updatepanel1_OnLoad(null, null);
                }
            }
            #endregion

            #region init form
            base.InitForm("Tìm kiếm đợt kiểm toán", string.Empty, _doctypeid, 0);
            //if (_m_roleID == ROLES.TRUONGPHONG_CSCC)
            //    _btnAddNew.Visible = true;
            //else
            //    _btnAddNew.Visible = false;
            //_btnAddNew.Attributes.Add("onclick", "{newform(); return false;}");
            #endregion

            #region client control event handler

            #endregion
        }
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //GetList(_doctypeid);
           
            if(!IsPostBack)
            {
                LoadDoiTuongKiemToan();
                BuildThangNam();
                BuildTrangThai();
            }
        }
        #endregion

        #region page helper processing
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DocumentTypeID"></param>
        private void GetList()
        {
            string tungay = hdTuNgay.Value;
            string denngay = hdDenNgay.Value;
            string loaidoituong = hdLoaiDoiTuong.Value;
            string trangthai = hdTrangThai.Value;

            if (String.IsNullOrEmpty(tungay) || String.IsNullOrEmpty(denngay) || String.IsNullOrEmpty(loaidoituong))
                return;

            string DocFields = "PK_DocumentID,[Tên đợt kiểm toán],[Đối tượng kiểm toán],[Quy mô đợt kiểm toán],[Đơn vị thực hiện],[Năm],[Thời gian dự kiến kiểm toán],[Đơn vị thực hiện],[Loại đối tượng kiểm toán],Status";
            string PropertyFields = "Tên đợt kiểm toán,Đối tượng kiểm toán,Quy mô đợt kiểm toán,Đơn vị thực hiện,Năm,Thời gian dự kiến kiểm toán,Đơn vị thực hiện,Loại đối tượng kiểm toán";
            //string Condition = loaidoituong == "-1" ? " Order By [Năm],[Loại đối tượng kiểm toán]" :
            //                    " And [Đối tượng kiểm toán]=N'" + loaidoituong + "'";
            string Condition = loaidoituong == "-1" ? String.Empty :
                                " And [Đối tượng kiểm toán]=N'" + loaidoituong + "'";
            string status = String.Empty;
            if (trangthai == "-1")
            { 
                status = " Order By [Năm],[Loại đối tượng kiểm toán]";
                Condition = Condition + status;
            }
            if (trangthai == "1")
            {
                status = " And (Status = 11 Or Status = 12 Or Status = 13 Or Status = 14 Or Status = 15)  Order By [Năm],[Loại đối tượng kiểm toán]";
                Condition = Condition + status;
            }
            if (trangthai == "2")
            {
                status = " And (Status = 21 Or Status = 22 Or Status = 23 Or Status =24 Or Status =25 Or Status = 26)  Order By [Năm],[Loại đối tượng kiểm toán]";
                Condition = Condition + status;
            }
            if (trangthai == "3")
            {
                status = " And (Status = 31)  Order By [Năm],[Loại đối tượng kiểm toán]";
                Condition = Condition + status;
            }
            if (trangthai == "4")
            {
                status = " And (Status = 41 Or Status = 42  Or Status = 43 Or Status =44)  Order By [Năm],[Loại đối tượng kiểm toán]";
                Condition = Condition + status;
            }
            if (trangthai == "5")
            {
                status = " And (Status = 51 Or Status = 52  Or Status = 53)  Order By [Năm],[Loại đối tượng kiểm toán]";
                Condition = Condition + status;
            }

            //bus_Document doc = new bus_Document(_objUserContext);
            //DataSet ds = doc.getDocumentList(DOCTYPE.DOT_KIEMTOAN, DocFields, PropertyFields, Condition);
            //if(isValidDataSet(ds))
            //{
            //    //format dd/MM/yyyy
            //    DateTime dtTuNgay = Convert.ToDateTime(tungay);
            //    DateTime dtDenNgay = Convert.ToDateTime(denngay);

            //    DataTable dt = ds.Tables[0];
            //    dt.Columns.Add("ThoiGian", typeof(DateTime));
            //    foreach (DataRow row in dt.Rows)
            //    { 
            //        string thoigian = String.Format("1/{0}/{1}",row["Thời gian dự kiến kiểm toán"].ToString().Replace("Tháng","").Trim(),row["Năm"]);
            //        row["ThoiGian"] = String.Format("{0:M/d/yyyy}",Convert.ToDateTime(thoigian));
            //    }

            //    string filter = "ThoiGian >= '" + String.Format("{0:M/d/yyyy}", dtTuNgay) + "' AND ThoiGian <= '" + String.Format("{0:M/d/yyyy}", dtDenNgay) + "'";
            //    DataRow[] drs = dt.Select(filter);
            //    //make a new "results" datatable via clone to keep structure
            //    DataTable dt2 = dt.Clone();
            //    //Import the Rows
            //    foreach (DataRow d in drs)
            //        dt2.ImportRow(d);



                ObjectDataSource1.SelectParameters["DocumentTypeID"].DefaultValue = DOCTYPE.DOT_KIEMTOAN;
                ObjectDataSource1.SelectParameters["DocFields"].DefaultValue = DocFields;
                ObjectDataSource1.SelectParameters["PropertyFields"].DefaultValue = PropertyFields;
                ObjectDataSource1.SelectParameters["Condition"].DefaultValue = Condition;
                ObjectDataSource1.SelectParameters["TuNgay"].DefaultValue = tungay;
                ObjectDataSource1.SelectParameters["DenNgay"].DefaultValue = denngay;
                ObjectDataSource1.SelectParameters["TrangThai"].DefaultValue = trangthai;
                dataCtrl.DataBind();
                //dataCtrl.DataSource = dt2;
                //dataCtrl.DataBind();
            //}
            
        }

        void LoadDoiTuongKiemToan()
        {
            string DocFields = "PK_DocumentID,Status,[Tên đối tượng kiểm toán],[Tên hiển thị],[Loại đối tượng kiểm toán],[Email],[Cán bộ GSTX]";
            string PropertyFields = "Tên đối tượng kiểm toán,Tên hiển thị,Loại đối tượng kiểm toán,Email,Cán bộ GSTX";
            string Condition = " Order By [Loại đối tượng kiểm toán]";
            bus_Document doc = new bus_Document(_objUserContext);
            DataSet ds= doc.getDocumentList(DOCTYPE.DOITUONG_KT, DocFields, PropertyFields, Condition);
            ddlLoaiDoiTuong.Items.Add(new ListItem("--- Tất cả ---", "-1"));
            if(isValidDataSet(ds))
                foreach (DataRow row in ds.Tables[0].Rows)
                    ddlLoaiDoiTuong.Items.Add(new ListItem(row["Tên đối tượng kiểm toán"].ToString(), row["Tên đối tượng kiểm toán"].ToString()));
        }

        #endregion

        #region page button processing
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void dataCtrl_OnItemDataBound(object sender, C1ItemEventArgs e)
        {
            C1ListItemType elemType = e.Item.ItemType;
            if ((elemType == C1ListItemType.AlternatingItem) || (elemType == C1ListItemType.Item))
            {
                Label PK_DocumentID = (Label)e.Item.FindControl("PK_DocumentID") as Label;
                Label Status = (Label)e.Item.FindControl("Status") as Label;
                //Label Persons = (Label)e.Item.FindControl("Persons") as Label;
                Image imgEdit = (Image)e.Item.FindControl("imgEdit") as Image;
                Image imgDoanKT = (Image)e.Item.FindControl("imgDoanKT") as Image;
                if (PK_DocumentID != null)
                {
                    imgEdit.Attributes.Add("onclick", "{LoadDocument('" + PK_DocumentID.Text + "')}");
                    imgDoanKT.Attributes.Add("onclick", "{LapDoanKiemToan('" + PK_DocumentID.Text + "')}");
                    //if (Persons != null)
                    //    GetCountDoanKiemToan(_doctypeid_doankiemtoan, PK_DocumentID.Text, Persons);
                    //CommonFunc.getCountOnDocumentLink(" And FK_DocLinkID='" + PK_DocumentID.Text + "'").ToString(); 
                }
                if (Status != null)
                    SetStatus(Status, Status.Text);
            }
        }

        void SetStatus(Label lblStatus,string text)
        {
            string result = String.Empty;
            if(string.IsNullOrEmpty(text))
                result = String.Empty;
            result = CommonFunc.GetTrangThaiDotKT(Int32.Parse(text));
            //if (text == "11" || text == "12" || text == "13" || text == "14" || text == "15")
            //{
            //    result = "Lập kế hoạch";
            //}
            //if (text == "21" || text == "22" || text == "23" || text == "24" || text == "25" || text == "26")
            //{
            //    result = "Thực hiện";
            //}
            //if (text == "31")
            //{
            //    result = "Lập báo cáo";
            //}
            //if (text == "41" || text == "42" || text == "43" || text == "44")
            //{
            //    result = "Đánh giá";
            //}
            //if (text == "51" || text == "52" || text == "53")
            //{
            //    result = "Giám sát khắc phục";
            //}
            lblStatus.Text = result;

        }

        private void GetCountDoanKiemToan(string DocumentTypeID, string dotkt, Label lbl)
        {
            string DocFields = "PK_DocumentID,[Name]";
            string PropertyFields = "Name";
            string Condition = " and PK_DocumentID in (Select FK_documentID From T_DocLink Where FK_DocLinkID='" + dotkt + "')";
            bus_Document obj = bus_Document.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            DataSet ds = obj.getDocumentList(DocumentTypeID, DocFields, PropertyFields, Condition);
            //int i = obj.getCountOnDocumentLink(Condition);
            int i = 0;
            if (ds != null && ds.Tables.Count > 0)
                lbl.Text = ds.Tables[0].Rows.Count.ToString();
        }

        #endregion

        #region xuly grid
        protected void updatepanel1_OnLoad(object sender, EventArgs e)
        {
            GetList();
        }

        void BuildThangNam()
        {
            for (int i = 1; i <= 12; i++)
            { 
                ddlTuNgayThang.Items.Add(new ListItem("Tháng " +i.ToString(), i.ToString()));
                ddlDenNgayThang.Items.Add(new ListItem("Tháng " +i.ToString(), i.ToString()));
            }
            CommonFunc.LoadDropDownList(ddlTuNgayNam, 3);
            CommonFunc.LoadDropDownList(ddlDenNgayNam, 3);
        }

        void BuildTrangThai()
        {
            CommonFunc.LoadDropDownList(this.ddlTrangThai, 7);
        }
        #endregion end xuly grid

      
    }
}