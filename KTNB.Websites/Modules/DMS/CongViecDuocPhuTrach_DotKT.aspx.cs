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
using VPB_KTNB.Helpers;

namespace VPB_TDHS.Modules.DMS
{
    public partial class CongViecDuocPhuTrach_DotKT : PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;
        protected string _doctypeid = DOCTYPE.DOT_KIEMTOAN;
        protected string _doctypeid_doankiemtoan = DOCTYPE.DOAN_KIEMTOAN;

        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;
        protected string _docspaceid = string.Empty;
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
            base.InitForm("Công việc được phân công theo kế hoạch", string.Empty, _doctypeid, 0);
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
            GetList();
        }
        #endregion

        #region page helper processing
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DocumentTypeID"></param>
        private void GetList()
        {
            if(_cv == "nguoithuchien")
                ObjectDataSource1.SelectParameters["FormatUserAssign"].DefaultValue = "Người thực hiện";
            if (_cv == "nguoiduyet")
                ObjectDataSource1.SelectParameters["FormatUserAssign"].DefaultValue = "Người duyệt";
            //if (_cv == "nguoiduyet1")
            //    ObjectDataSource1.SelectParameters["FormatUserAssign"].DefaultValue = "Người duyệt 1";
            //else if (_cv == "nguoiduyet2")
            //    ObjectDataSource1.SelectParameters["FormatUserAssign"].DefaultValue = "Người duyệt 2";
            //if (_cv == "nguoiduyet1")
            //    ObjectDataSource1.SelectParameters["FormatUserAssign"].DefaultValue = "Người duyệt 1";
            //else if (_cv == "nguoiduyet2")
            //    ObjectDataSource1.SelectParameters["FormatUserAssign"].DefaultValue = "Người duyệt 2";

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
                Label Persons = (Label)e.Item.FindControl("Persons") as Label;
                Label DoanKT = (Label)e.Item.FindControl("DoanKT") as Label;
                Image imgEdit = (Image)e.Item.FindControl("imgEdit") as Image;
                Image imgDoanKT = (Image)e.Item.FindControl("imgDoanKT") as Image;
                Image imgCongViec = (Image)e.Item.FindControl("imgCongViec") as Image;
                //Image imgRRCL = (Image)e.Item.FindControl("imgRRCL") as Image;
                Label lblTrangThai = (Label)e.Item.FindControl("lblTrangThai") as Label;

                if (PK_DocumentID != null && DoanKT != null)
                {
                    if (imgEdit != null)
                        imgEdit.Attributes.Add("onclick", "{LoadDocument('" + PK_DocumentID.Text + "')}");
                    if(imgDoanKT != null)
                        imgDoanKT.Attributes.Add("onclick", "{LapDoanKiemToan('" + PK_DocumentID.Text + "')}");
                    imgCongViec.Attributes.Add("onclick", "{LoadCongViec('" + PK_DocumentID.Text + "','" + DoanKT.Text + "')}");
                    //imgRRCL.Attributes.Add("onclick", "{LoadDanhGiaRRCL('" + PK_DocumentID.Text + "','" + DoanKT.Text + "')}");
                    if (Persons != null)
                        GetCountDoanKiemToan(_doctypeid_doankiemtoan, PK_DocumentID.Text, Persons);
                    if (lblTrangThai != null)
                        CommonFunc.SetTrangThaiDotKT(lblTrangThai, PK_DocumentID.Text);
                }
            }
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
            {
                lbl.Text = ds.Tables[0].Rows.Count.ToString();
            }
        }

        #endregion
               
    }
}