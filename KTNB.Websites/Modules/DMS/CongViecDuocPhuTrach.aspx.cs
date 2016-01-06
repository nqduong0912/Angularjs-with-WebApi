using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.Definition.DMS;
using vpb.app.business.ktnb.Definition.UMS;
using C1.Web.C1WebGrid;
using vpb.app.business.ktnb.CoreBusiness;
using System.Data;
using vpb.app.business.ktnb.CoreBusiness;
using VPB_KTNB.Helpers;

namespace VPB_TDHS.Modules.DMS
{
    public partial class CongViecDuocPhuTrach : PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;
        protected string _doctypeid = DOCTYPE.CONGVIEC_TRONG_DOTKIEMTOAN;
        protected string _doctypeid_thutuckiemtoan = DOCTYPE.THUTUC_KIEMTOAN;

        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;
        protected string _docspaceid = string.Empty;


        protected string _dotkt = string.Empty;
        protected string _doankt = string.Empty;
        protected bool _isTruongDoan = false;
        protected string _cv = string.Empty;
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
            _doankt = Request["doankt"];
            _cv = Request["cv"];

            if (string.IsNullOrEmpty(_documentid))
                _viewtype = VIEWTYPE.ADDNEW;
            else
                _viewtype = VIEWTYPE.EDIT;

            #region get data submit
            #endregion

            #region action handler

            #endregion

            #region init form
            base.InitForm("Công việc được phụ trách", string.Empty, _doctypeid, 0);
            //_isTruongDoan = CommonFunc.IsTruongDoan(_dotkt, _objUserContext.UserName);
            //if (_isTruongDoan)
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
            if (!IsPostBack)
            {
                GetList();
                if (!String.IsNullOrEmpty(_dotkt))
                {
                    CommonFunc.LoadDocInfo(_dotkt, Page.Master);
                }
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
            if (String.IsNullOrEmpty(_objUserContext.UserName))
                return;
            string DocFields = "PK_DocumentID,[Tên công việc],[Người thực hiện],[Người duyệt 1],[Người duyệt 2],[Ngày bắt đầu],[Ngày kết thúc],[Nhận xét],Status";
            string PropertyFields = "Tên công việc,Người thực hiện,Người duyệt 1,Người duyệt 2,Ngày bắt đầu,Ngày kết thúc,Nhận xét";
            string Condition = String.Empty;
            if (_cv == "nguoithuchien")
            {
                Condition = " and [Người thực hiện]=N'" + _objUserContext.UserName + "'";
                Condition += " and PK_DocumentID IN (select FK_DOCUMENTID from T_DOCLINK where FK_DOCLINKID = '" + _doankt + "'  ) and (Status >= 2 and Status <=64)";
            }
            if (_cv == "nguoiduyet")
            {
                Condition = " and ([Người duyệt 1]=N'" + _objUserContext.UserName + "' or [Người duyệt 2]=N'" + _objUserContext.UserName + "')";
                Condition += " and (PK_DocumentID IN (select FK_DOCUMENTID from T_DOCLINK where FK_DOCLINKID = '" + _doankt + "'  )) and (Status > 8)";//status >4

            }

            ObjectDataSource1.SelectParameters["DocumentTypeID"].DefaultValue = _doctypeid;
            ObjectDataSource1.SelectParameters["DocFields"].DefaultValue = DocFields;
            ObjectDataSource1.SelectParameters["PropertyFields"].DefaultValue = PropertyFields;
            ObjectDataSource1.SelectParameters["Condition"].DefaultValue = Condition;
            dataCtrl.DataBind();
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
                Label lblTongSoPhatHien = (Label)e.Item.FindControl("lblTongSoPhatHien") as Label;
                Label lblTrangThai = (Label)e.Item.FindControl("lblTrangThai") as Label;
                //get tong so phat hien
                bus_Doankiemtoan doankiemtoan = bus_Doankiemtoan.Instance(_objUserContext);
                DataSet dsPhatHien = new DataSet();
                if(_cv == "nguoithuchien")
                    dsPhatHien = doankiemtoan.GetTatCaDanhSachPhatHienByUserType(PK_DocumentID.Text,_cv);
                 if(_cv == "nguoiduyet")
                    dsPhatHien = doankiemtoan.GetTatCaDanhSachPhatHienByUserType(PK_DocumentID.Text,CommonFunc.CheckNguoiDuyet(PK_DocumentID.Text));
                if (isValidDataSet(dsPhatHien))
                    lblTongSoPhatHien.Text = dsPhatHien.Tables[0].Rows.Count.ToString();
                else
                    lblTongSoPhatHien.Text = "0";
                TreeView treeViewThuTuc = (TreeView)e.Item.FindControl("treeViewThuTuc") as TreeView;
                Image imgEdit = (Image)e.Item.FindControl("imgEdit") as Image;
                if (PK_DocumentID != null)
                {
                    imgEdit.Attributes.Add("onclick", "{LoadDocument('" + PK_DocumentID.Text + "')}");
                    //ImgPhatHien.Attributes.Add("onclick", "{LoadPageDanhSachPhatHien('" + PK_DocumentID.Text + "')}");
                    lblTongSoPhatHien.Attributes.Add("onclick", "{LoadPageDanhSachPhatHien('" + PK_DocumentID.Text + "')}");
                    if (treeViewThuTuc != null)
                        BuildTreeView(PK_DocumentID.Text, treeViewThuTuc);
                    if (lblTrangThai != null)
                        CommonFunc.SetTrangThaiCongViec(lblTrangThai, lblTrangThai.Text);
                }
                Label NgayBatDau = (Label)e.Item.FindControl("NgayBatDau") as Label;
                Label NgayKetThuc = (Label)e.Item.FindControl("NgayKetThuc") as Label;
                if (NgayBatDau != null && NgayKetThuc != null)
                {
                    NgayBatDau.Text = CommonFunc.formatDateTimeDDMMYYY(NgayBatDau.Text);
                    NgayKetThuc.Text = CommonFunc.formatDateTimeDDMMYYY(NgayKetThuc.Text);
                }
                Label lblSoLanTuChoi = (Label)e.Item.FindControl("lblSoLanTuChoi");
                if (lblSoLanTuChoi != null)
                    SumRejected(lblSoLanTuChoi, lblSoLanTuChoi.Text);
            }
        }

        void BuildTreeView(string documentID, TreeView treeView)
        {
            string DocFields = "PK_DocumentID,[Tên thủ tục kiểm toán]";
            string PropertyFields = "Tên thủ tục kiểm toán";
            string Condition = " and PK_DocumentID in (Select FK_documentID From T_DocLink Where FK_DocLinkID='" + documentID + "')";
            bus_Document obj = new bus_Document(_objUserContext);
            DataSet ds = obj.getDocumentList(_doctypeid_thutuckiemtoan, DocFields, PropertyFields, Condition);
            if (isValidDataSet(ds))
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    TreeNode node = new TreeNode("- " + row["Tên thủ tục kiểm toán"].ToString());
                    treeView.Nodes.Add(node);
                }
            }
        }

        #endregion
        
        #region tong so lan tuchoi
        void SumRejected(Label lbl, string value,char format)
        {
            int sumRejected = 0;
            if (string.IsNullOrEmpty(value))
            {
                lbl.Text = "0";
                return;
            }
            if (value.IndexOf(format) > 0)
            {
                lbl.Text = value.Split(format).Count().ToString();
                return;
            }
            lbl.Text = "0";
        }

        void SumRejected(Label lbl, string json)
        {
            int sumRejected = 0;
            if (string.IsNullOrEmpty(json))
            {
                lbl.Text = "0";
                return;
            }
            List<NhanXet> objs = JSONHelper.Deserialize<List<NhanXet>>(json);
            
            if (objs != null) 
            {
                List<NhanXet> objs_new =  objs.Where(x => x.HanhDong == "TuChoi").ToList();
                if (objs_new.Count > 0)
                {
                    lbl.Text = objs_new.Count.ToString();
                    return;
                }
            }
            
            lbl.Text = "0";
        }

        #endregion
    }
}