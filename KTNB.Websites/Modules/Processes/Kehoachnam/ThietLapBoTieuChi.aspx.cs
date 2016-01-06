using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.Definition.DMS;
using C1.Web.C1WebGrid;
using System.Text;
using System.Data;
using KTNB.Extended.Dal;
using KTNB.Extended.Biz;
using KTNB.Extended.Core;
using KTNB.Extended.Dal.Lib;
using KTNB.Extended.Enums;
using VPB_KTNB.Helpers;

namespace VPB_KTNB.Modules.Processes.KeHoachNam
{
    public partial class ThietLapBoTieuChi : PageBase
    {
        protected string _action = string.Empty;
        protected string _doctypeid = DOCTYPE.QUANLY_BOTIEUCHI_KEHOACHNAM;
        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;
        protected string _docspaceid = string.Empty;
        protected int countActive = 0;

        #region page init & load
        /// <summary>
        /// OnInit
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            base.AuthorizeUserCtx();
            if (!string.IsNullOrEmpty(Request["doc"]))
                _documentid = Request["doc"];
            if (string.IsNullOrEmpty(_documentid))
                _viewtype = VIEWTYPE.ADDNEW;
            else
                _viewtype = VIEWTYPE.EDIT;

            #region get data submit
            #endregion

            #region action handler

            #endregion

            base.InitForm("Quản lý bộ tiêu chí kế hoạch năm");
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
                CommonFunc.GetYear2Dropdownlist(drpYears);
                GetLDTKT();
                GetList(drpYears.SelectedValue, this.drpLoaiDTKT.SelectedValue);
            }
        }
        #endregion
        private void GetLDTKT()
        {
            int currentYear = Int32.Parse(drpYears.SelectedValue);
            List<dm_loaidoituongkiemtoan> lst = CoreFactory<dm_loaidoituongkiemtoan>.EntityManager.GetList(x => x.Nam == currentYear);
            if(lst.Count == 0)
            {
                //Add tất cả loại đối tượng kiểm toán đang active vào extend
                List<dm_li_loaidoituongkiemtoan> lstLib = CoreFactory<dm_li_loaidoituongkiemtoan>.EntityManager.GetList(x => x.Status == 4);
                if(lstLib.Count > 0)
                {
                    foreach(var info in lstLib)
                    {
                        lst.Add(new dm_loaidoituongkiemtoan() { 
                            SourceId = info.PK_DocumentID,
                            Ten = info.Ten,
                            Diengiai = info.Diengiai,
                            Nam = currentYear,
                            Phongban = string.Empty
                        });
                    }
                    CoreFactory<dm_loaidoituongkiemtoan>.EntityManager.InsertOrUpdateAll(lst);                    
                }
            }
            foreach (var info in lst)
            {
                drpLoaiDTKT.Items.Add(new ListItem(info.Ten, info.SourceId.ToString()));
            }
        }
        #region page helper processing
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DocumentTypeID"></param>
        private void GetList(string nam, string loaiDTKT)
        {            
            long total;
            List<qt_dm_kehoachnam_botieuchi> lst = CoreFactory<qt_dm_kehoachnam_botieuchi>.EntityManager.GetList(20, out total, x => x.LDTKT == Guid.Parse(loaiDTKT) && x.Nam == Int32.Parse(nam), 0);
            if(lst.Count > 0)
            {
                rptBoTieuChi.DataSource = lst;
                rptBoTieuChi.DataBind();
                rptBoTieuChi.Visible = true;
            }
            else
            {                
                rptBoTieuChi.DataSource = lst;
                rptBoTieuChi.DataBind();
                rptBoTieuChi.Visible = false;
            }
        }
        #endregion    
        
        protected void drpYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetList(drpYears.SelectedValue, this.drpLoaiDTKT.SelectedValue);
        }      
      
        protected void rptBoTieuChi_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.AlternatingItem) || (e.Item.ItemType == ListItemType.Item))
            {
                qt_dm_kehoachnam_botieuchi info = (qt_dm_kehoachnam_botieuchi)e.Item.DataItem;
                Literal lblStatus = (Literal)e.Item.FindControl("lblStatus");
                Literal lblUpdatedate = (Literal)e.Item.FindControl("lblUpdatedate");
                Label lblIsActive = (Label)e.Item.FindControl("lblIsActive");
                LinkButton btnActiveBTC = (LinkButton)e.Item.FindControl("btnActiveBTC");

                LinkButton btnMark = (LinkButton)e.Item.FindControl("btnMark");
                LinkButton btnCopy = (LinkButton)e.Item.FindControl("btnCopy");
                LinkButton btnSend = (LinkButton)e.Item.FindControl("btnSend");
                
                if (info.IsOn == 1)
                {
                    btnActiveBTC.Visible = false;
                    lblIsActive.Visible = true;
                }
                else
                {
                    if (info.IsActive == 2)
                        btnActiveBTC.Visible = false;
                    else
                        btnActiveBTC.Visible = true;
                    lblIsActive.Visible = false;
                }
                switch(info.Status)
                {
                    case (int)BoTieuChiKeHoachNamEnum.KhoiTao:
                        lblStatus.Text = "Khởi tạo";
                        break;
                    case (int)BoTieuChiKeHoachNamEnum.ChuaRaSoat:
                        lblStatus.Text = "Chưa rà soát";
                        break;
                    case (int)BoTieuChiKeHoachNamEnum.DaRaSoat:
                        lblStatus.Text = "Đã rà soát";
                        break;
                    case (int)BoTieuChiKeHoachNamEnum.DaDuyet:
                        lblStatus.Text = "Đã duyệt";
                        btnActiveBTC.Visible = true;
                        break;
                    default:
                        lblStatus.Text = "Khởi tạo";
                        break;
                }
                if (info.IsActive == 2)
                    lblStatus.Text += " / " + "Inactive";
                else
                {
                    lblStatus.Text += " / " + "Active";
                    btnMark.Visible = true;
                    btnCopy.Visible = true;
                    btnSend.Visible = true;
                }
                lblUpdatedate.Text = info.CreateDate.ToString("dd/MM/yyyy");
            }
        }

        protected void rptBoTieuChi_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string documentId = (string)e.CommandArgument;
            switch (e.CommandName)
            {
                case "onbtc":
                    break;
                default:
                    break;
            }
        }

    }
}
