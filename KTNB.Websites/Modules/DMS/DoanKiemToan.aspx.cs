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
using vpb.app.business.ktnb.Definition.UMS;
using VPB_KTNB.Helpers;

namespace VPB_TDHS.Modules.DMS
{
    public partial class DoanKiemToan : PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;
        protected string _doctypeid = DOCTYPE.DOAN_KIEMTOAN;
        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;
        protected string _docspaceid = string.Empty;


        protected string _dotkt = string.Empty;
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
            if (string.IsNullOrEmpty(_documentid))
                _viewtype = VIEWTYPE.ADDNEW;
            else
                _viewtype = VIEWTYPE.EDIT;

            #region get data submit
            #endregion

            #region action handler

            #endregion

            #region init form
            base.InitForm("Thông tin đoàn kiểm toán", string.Empty, _doctypeid, 0);
            //truongphong -> tao,sua doan kiem toan
            if(_m_roleID == ROLES.TRUONGPHONG_DONVI_KIEMTOAN)
                _btnAddNew.Visible = true;
            else
                _btnAddNew.Visible = false;
            _btnAddNew.Attributes.Add("onclick", "{newform(); return false;}");
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
            GetList(_doctypeid,_dotkt);

        }
        #endregion

        #region page helper processing
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DocumentTypeID"></param>
        private void GetList(string DocumentTypeID,string dotkt)
        {
            if (String.IsNullOrEmpty(dotkt))
                return;
            string DocFields = "PK_DocumentID,[Name]";
            string PropertyFields = "Name";
            string Condition = " and PK_DocumentID in (Select FK_documentID From T_DocLink Where FK_DocLinkID='" + dotkt + "')";

            ObjectDataSource1.SelectParameters["DocumentTypeID"].DefaultValue = DocumentTypeID;
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
                Label DotKiemToan = (Label)e.Item.FindControl("PK_DocumentID") as Label;
                Label SLThanhVien = (Label)e.Item.FindControl("SLThanhVien") as Label;
                Image imgEdit = (Image)e.Item.FindControl("imgEdit") as Image;
                Image imgMappingMangNV = (Image)e.Item.FindControl("imgMappingMangNV") as Image;
                string truongdoan = String.IsNullOrEmpty(e.Item.Cells[3].Text) ? String.Empty : e.Item.Cells[3].Text;
                if (DotKiemToan != null)
                {
                    imgEdit.Attributes.Add("onclick", "{LoadDocument('" + DotKiemToan.Text + "','"+truongdoan+"')}");
                    imgMappingMangNV.Attributes.Add("onclick", "{LoadMappingPage('" + DotKiemToan.Text + "','" + truongdoan + "')}");
                    if (SLThanhVien != null)
                        GetSoLuongThanhVien(DotKiemToan.Text, SLThanhVien);
                }
                //truongphong moi duoc hien thi
                //if (imgEdit != null && imgMappingMangNV != null)
                //    if (_m_roleID != ROLES.TRUONGPHONG_DONVI_KIEMTOAN)
                //        imgMappingMangNV.Visible = false;
               
            }
        }

        private void GetSoLuongThanhVien(string truongdoan,Label lbl)
        {
            if (String.IsNullOrEmpty(truongdoan))
                return;
            bus_Doankiemtoan doankiemtoan = bus_Doankiemtoan.Instance(_objUserContext);
            DataSet ds = doankiemtoan.DanhSachThanhVienDoanKT(truongdoan);
            if (ds != null)
                if (ds.Tables.Count > 0)
                    lbl.Text = ds.Tables[0].Rows.Count.ToString();
        }

        #endregion
    }
}