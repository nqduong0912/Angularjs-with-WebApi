using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.Definition.DMS;
using C1.Web.C1WebGrid;
using System.Data;
using vpb.app.business.ktnb.CoreBusiness;
using vpb.app.business.ktnb.Definition.UMS;
using vpb.app.business.ktnb.CoreBusiness;
using VPB_KTNB.Helpers;

namespace VPB_TDHS.Modules.DMS
{
    public partial class DanhGiaXacSuat : PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;
        protected string _type = string.Empty;
        protected string _doctypeid = DOCTYPE.TIEUCHI_DANHGIA_XACSUAT_ANHHUONG;
        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;
        protected string _docspaceid = string.Empty;


        protected string _dotkt = string.Empty;
        protected string _nhomkt = string.Empty;
        protected string _value = string.Empty;
        protected string _value_xs = string.Empty;
        protected string _value_ah = string.Empty;
        protected string _json_diem_xs = string.Empty;
        protected string _json_diem_ah = string.Empty;



        //thangma
        protected string xacXuatPropertyID = "26322953-20BB-4D2E-A651-289960B78202";
        protected string anhHuongPropertyID = "72812D71-8A97-4BCA-B495-E0664DC1B6CF";

        protected string diem_xacXuatPropertyID = "B45EC8CF-CA21-4FD7-9820-1C4A715E1831";
        protected string diem_anhHuongPropertyID = "DACA775F-FA70-40B0-97B5-2ED385A56474";

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
            _dotkt = Request["dotkt"];
            _nhomkt = Request["nhomkt"];
            _type = Request["type"];
            _value = Request["value"];
            _value_xs = Request["value_xs"];
            _value_ah = Request["value_ah"];

            _json_diem_xs = Request["json_diem_xs"];
            _json_diem_ah = Request["json_diem_ah"];

            _action = Request["act"];
            if (string.IsNullOrEmpty(_documentid))
                _viewtype = VIEWTYPE.ADDNEW;
            else
                _viewtype = VIEWTYPE.EDIT;

            #region get data submit
            #endregion

            #region action handler
            if (!string.IsNullOrEmpty(_action))
            {
                if (_action == "danhgiaanhhuong")
                {
                    string value = _value;
                    FeedBackClient(CommonFunc.UpdateDocPropertyValue(_documentid, anhHuongPropertyID, value));
                }
                else if (_action == "danhgiaxacsuat")
                {
                    string value = _value;
                    FeedBackClient(CommonFunc.UpdateDocPropertyValue(_documentid, xacXuatPropertyID, value));
                }
                else if (_action == "danhgiaxs_ah")
                {
                    string value_xs = _value_xs;
                    string value_ah = _value_ah;
                    FeedBackClient(UpdateXSAHValue(_documentid, _value_xs, _value_ah,_json_diem_xs,_json_diem_ah));
                }
            }

            #endregion

            #region init form
            string initTitle = "Thông tin đánh giá xác suất/ảnh hưởng";
            //if(_type == "ah")
            //    initTitle = "Thông tin đánh giá ảnh hưởng";
            base.InitForm(initTitle, string.Empty, _doctypeid, 0);
            _btnEdit.Visible = true;
            //_btnEdit.Attributes.Add("onclick", "{updatedocument('"+_type+"'); return false;}");
            _btnEdit.Attributes.Add("onclick", "{updatedocumentxs_ah(); return false;}");
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
            if (!IsPostBack)
            {
                //if (_type == "xs")
                //    lblTieuChi.Text = "Tiêu chí đánh giá xác suất";
                //if (_type == "ah")
                //    lblTieuChi.Text = "Tiêu chí đánh giá ảnh hưởng";
                GetList(_doctypeid, "xs");
                GetList(_doctypeid, "ah");
            }
        }
        #endregion

        #region page helper processing
        //thangma
        private string UpdateXSAHValue(string hosoID, string XSValue, string AHValue)
        {
            CommonFunc.UpdateDocPropertyValue(hosoID, xacXuatPropertyID, XSValue);
            CommonFunc.UpdateDocPropertyValue(hosoID, anhHuongPropertyID, AHValue);
            return string.Empty;
        }


        //thangma
        private string UpdateXSAHValue(string hosoID, string XSValue, string AHValue,string json_diem_xs,string json_diem_ah)
        {
            
            
            CommonFunc.UpdateDocPropertyValue(hosoID, xacXuatPropertyID, XSValue);
            CommonFunc.UpdateDocPropertyValue(hosoID, anhHuongPropertyID, AHValue);

            CommonFunc.UpdateDocPropertyValue(hosoID, diem_xacXuatPropertyID, json_diem_xs);
            CommonFunc.UpdateDocPropertyValue(hosoID, diem_anhHuongPropertyID, json_diem_ah);
            return string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DocumentTypeID"></param>
        private void GetList(string DocumentTypeID, string type)
        {
            string DocFields = "PK_DocumentID,[Tên tiêu chí],[Nhóm tiêu chí đánh giá],[Diễn giải],[Điểm 1],[Điểm 2],[Điểm 3],[Điểm 4],[Điểm 5]";
            // string DocFields = "PK_DocumentID,[Tên tiêu chí],[Diễn giải],[Điểm 1],[Điểm 2],[Điểm 3],[Điểm 4],[Điểm 5]";
            string PropertyFields = "Tên tiêu chí,Nhóm tiêu chí đánh giá,Diễn giải,Điểm 1,Điểm 2,Điểm 3,Điểm 4,Điểm 5";
            string Condition = String.Empty;
            if (type == "xs")
                Condition = " And [Nhóm tiêu chí đánh giá]=N'Xác suất'";
            else if (type == "ah")
                Condition = " And [Nhóm tiêu chí đánh giá]=N'Ảnh hưởng'";
            bus_Document obj = bus_Document.Instance(_objUserContext);
            DataSet ds = obj.getDocumentList(DocumentTypeID, DocFields, PropertyFields, Condition);
            if (isValidDataSet(ds))
            {
                if (type == "xs")
                {
                    dataCtrl.DataSource = ds;
                    dataCtrl.DataBind();
                }
                else
                {
                    dataCtrl_ah.DataSource = ds;
                    dataCtrl_ah.DataBind();
                }
            }

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
                string ten = e.Item.Cells[1].Text;
                DropDownList ddlTieuChi_xs = (DropDownList)e.Item.FindControl("ddlTieuChi_xs") as DropDownList;
                if (PK_DocumentID != null && ddlTieuChi_xs != null)
                    BuildDDLTieuChi_xs(ddlTieuChi_xs, PK_DocumentID.Text, _type,ten);
            }
        }

        void BuildDDLTieuChi_xs(DropDownList ddl, string PK_DocumentID, string type,string ten)
        {
            string DocFields = "PK_DocumentID,[Tên tiêu chí],[Nhóm tiêu chí đánh giá],[Diễn giải],[Điểm 1],[Điểm 2],[Điểm 3],[Điểm 4],[Điểm 5]";
            string PropertyFields = "Tên tiêu chí,Nhóm tiêu chí đánh giá,Diễn giải,Điểm 1,Điểm 2,Điểm 3,Điểm 4,Điểm 5";
            string Condition = String.Empty;
            //if (_type == "xs")
            Condition = " And [Nhóm tiêu chí đánh giá]=N'Xác suất' And PK_DocumentID='" + PK_DocumentID + "'";
            //else if (_type == "ah")
            //    Condition = " And [Nhóm tiêu chí đánh giá]=N'Ảnh hưởng' And PK_DocumentID='" + PK_DocumentID + "'";

            bus_Document obj = bus_Document.Instance(_objUserContext);
            DataSet ds = obj.getDocumentList(_doctypeid, DocFields, PropertyFields, Condition);
            ddl.Items.Add(new ListItem("----None----", "0"));
            if (isValidDataSet(ds))
            {
                string json_diem_xs = CommonFunc.getPropertyValueOnDocument(_documentid, diem_xacXuatPropertyID);
                List<DiemDanhGiaXacSuatAnhHuong> lst_xs = new List<DiemDanhGiaXacSuatAnhHuong>();
                if (!string.IsNullOrEmpty(json_diem_xs))
                    lst_xs = JSONHelper.Deserialize<List<DiemDanhGiaXacSuatAnhHuong>>(json_diem_xs);

                DataRow row = ds.Tables[0].Rows[0];
                SetDiemDanhGia(ddl, row, "Điểm 1", "1");
                SetDiemDanhGia(ddl, row, "Điểm 2", "2");
                SetDiemDanhGia(ddl, row, "Điểm 3", "3");
                SetDiemDanhGia(ddl, row, "Điểm 4", "4");
                SetDiemDanhGia(ddl, row, "Điểm 5", "5");

                if (lst_xs.Count > 0)
                    foreach (DiemDanhGiaXacSuatAnhHuong obj_xs in lst_xs)
                        if (obj_xs.Ten.Trim() == ten.Trim())
                            if (ddl.Items.FindByValue(obj_xs.Diem) != null)
                            {
                                ddl.Items.FindByValue(obj_xs.Diem).Selected = true;
                                break;
                            }
                            
            }
        }

        void SetDiemDanhGia(DropDownList ddl, DataRow rowDiem, string diem,string valueDiem)
        {
            if (rowDiem[diem] != null)
                 ddl.Items.Add(new ListItem(diem + " - " + rowDiem[diem].ToString(), valueDiem));
            else
                ddl.Items.Add(new ListItem(diem + " - ", valueDiem));
        }

        void BuildDDLTieuChi_ah(DropDownList ddl, string PK_DocumentID, string type,string ten)
        {
            string DocFields = "PK_DocumentID,[Tên tiêu chí],[Nhóm tiêu chí đánh giá],[Diễn giải],[Điểm 1],[Điểm 2],[Điểm 3],[Điểm 4],[Điểm 5]";
            string PropertyFields = "Tên tiêu chí,Nhóm tiêu chí đánh giá,Diễn giải,Điểm 1,Điểm 2,Điểm 3,Điểm 4,Điểm 5";
            string Condition = String.Empty;
            Condition = " And [Nhóm tiêu chí đánh giá]=N'Ảnh hưởng' And PK_DocumentID='" + PK_DocumentID + "'";

            bus_Document obj = bus_Document.Instance(_objUserContext);
            DataSet ds = obj.getDocumentList(_doctypeid, DocFields, PropertyFields, Condition);
            ddl.Items.Add(new ListItem("----None----", "0"));
            if (isValidDataSet(ds))
            {
                string json_diem_ah = CommonFunc.getPropertyValueOnDocument(_documentid, diem_anhHuongPropertyID);
                List<DiemDanhGiaXacSuatAnhHuong> lst_ah = new List<DiemDanhGiaXacSuatAnhHuong>();
                if (!string.IsNullOrEmpty(json_diem_ah))
                    lst_ah = JSONHelper.Deserialize<List<DiemDanhGiaXacSuatAnhHuong>>(json_diem_ah);


                DataRow row = ds.Tables[0].Rows[0];
                SetDiemDanhGia(ddl, row, "Điểm 1", "1");
                SetDiemDanhGia(ddl, row, "Điểm 2", "2");
                SetDiemDanhGia(ddl, row, "Điểm 3", "3");
                SetDiemDanhGia(ddl, row, "Điểm 4", "4");
                SetDiemDanhGia(ddl, row, "Điểm 5", "5");
                if (lst_ah.Count > 0)
                    foreach (DiemDanhGiaXacSuatAnhHuong obj_ah in lst_ah)
                        if (obj_ah.Ten.Trim() == ten.Trim())
                            if (ddl.Items.FindByValue(obj_ah.Diem) != null)
                            {
                                ddl.Items.FindByValue(obj_ah.Diem).Selected = true;
                                break;
                            }
                            
            }
        }
        #endregion

        protected void dataCtrl_ah_ItemDataBound(object sender, C1ItemEventArgs e)
        {
            C1ListItemType elemType = e.Item.ItemType;
            if ((elemType == C1ListItemType.AlternatingItem) || (elemType == C1ListItemType.Item))
            {
                Label PK_DocumentID = (Label)e.Item.FindControl("PK_DocumentID") as Label;
                string ten = e.Item.Cells[1].Text;
                DropDownList ddlTieuChi_ah = (DropDownList)e.Item.FindControl("ddlTieuChi_ah") as DropDownList;
                if (PK_DocumentID != null && ddlTieuChi_ah != null)
                    BuildDDLTieuChi_ah(ddlTieuChi_ah, PK_DocumentID.Text, _type,ten);
            }
        }
    }
}