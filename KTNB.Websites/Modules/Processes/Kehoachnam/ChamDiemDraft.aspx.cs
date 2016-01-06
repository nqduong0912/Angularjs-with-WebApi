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
using KTNB.Extended.Commons;
using VPB_KTNB.Helpers;

namespace VPB_KTNB.Modules.Processes.KeHoachNam
{
    public partial class ChamDiemDraft : PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;
        protected string _year = string.Empty;
        protected string _doctypeid = DOCTYPE.QUANLY_BOTIEUCHI_KEHOACHNAM;
        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;
        protected string _docspaceid = string.Empty;
        protected string _type = string.Empty;
        protected int countActive = 0;
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
            loadYears();
            if (!string.IsNullOrEmpty(Request["doc"]))
                _documentid = Request["doc"];
            if (!string.IsNullOrEmpty(Request["y"]))
                _year = Request["y"];
            if (!string.IsNullOrEmpty(Request["type"]))
                _type = Request["type"];
            else _type = drpLoaiDTKT.SelectedValue;
            if (string.IsNullOrEmpty(_documentid))
                _viewtype = VIEWTYPE.ADDNEW;
            else
                _viewtype = VIEWTYPE.EDIT;

            #region get data submit
            #endregion

            #region action handler

            #endregion

            #region init form
            base.InitForm("Cập nhật điểm sửa đổi", string.Empty, _doctypeid, 0);
            

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
                if (!String.IsNullOrEmpty(_year) && !String.IsNullOrEmpty(_type))
                {
                    GetList(_doctypeid, _year,_type);
                    drpYears.SelectedValue = _year;
                }
                else
                    GetList(_doctypeid, MiscUtils.CurrentYear, this.drpLoaiDTKT.SelectedValue);
            }
            
        }
        #endregion

        #region page helper processing
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DocumentTypeID"></param>
        private void GetList(string DocumentTypeID, string nam, string loaiDTKT)
        {
            //string DocFields = "PK_DocumentID,Status,[Năm],[Loại đối tượng kiểm toán],[Bộ tiêu chí năm],[Trạng thái],[Ngày cập nhật]";
            //string PropertyFields = "Năm,Loại đối tượng kiểm toán,Bộ tiêu chí năm,Trạng thái,Ngày cập nhật";
            //string Condition = " and [Loại đối tượng kiểm toán] ='" + loaiDTKT+"'";
            //if (!nam.Equals("Tất cả")) Condition+= " and [Năm] = '" + nam + "'";
            //ObjectDataSource1.SelectParameters["DocumentTypeID"].DefaultValue = DocumentTypeID;
            //ObjectDataSource1.SelectParameters["DocFields"].DefaultValue = DocFields;
            //ObjectDataSource1.SelectParameters["PropertyFields"].DefaultValue = PropertyFields;
            //ObjectDataSource1.SelectParameters["Condition"].DefaultValue = Condition;
            //dataCtrl.DataBind();
            List<qt_qlybotieuchi> lst = new List<qt_qlybotieuchi>();
            if (nam.Equals("Tất cả"))
            {
                lst = ManagerFactory.qlybotieuchi_manager.GetList(x => x.LoaiDTKT == loaiDTKT);
            }
            else
            {
                lst = ManagerFactory.qlybotieuchi_manager.GetList(x => x.Nam == int.Parse(nam) && x.LoaiDTKT == loaiDTKT);
            }
            dataCtrl.DataSource = lst;
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
                Label PK_ID = (Label)e.Item.FindControl("PK_ID") as Label;
                Label Status = (Label)e.Item.FindControl("lbStatus") as Label;
                LinkButton Trinhduyet = (LinkButton)e.Item.FindControl("lbTrinhduyet") as LinkButton;
                RadioButton active = (RadioButton)e.Item.FindControl("RowSelector") as RadioButton;
                
                if(active!=null)
                {
                    active.Attributes.Add("ID", PK_ID.Text);
                    active.Attributes.Add("onclick", "{Active('" + PK_ID.Text + "')}");
                }
                ////Image imgEdit = (Image)e.Item.FindControl("imgEdit") as Image;
                ////if (PK_DocumentID != null)
                ////    imgEdit.Attributes.Add("onclick", "{LoadDocument('" + PK_DocumentID.Text + "')}");
                //DataRowView drv = e.Item.DataItem as DataRowView;
                //if (!drv.Row["TrangThai"].ToString().Equals("1"))
                switch (Status.Text)
                {
                    case "1":
                        Status.Text = "Khởi tạo";
                        Trinhduyet.Visible = true;
                        break;
                    case "2":
                        Status.Text = "Chưa rà soát";
                        break;
                    case "3":
                        Status.Text = "Đã rà soát";
                        break;
                    case "4":
                        Status.Text = "Đã duyệt";
                        break;
                }
            }
        }
        protected void drpYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            _year = drpYears.SelectedValue;
            _type = drpLoaiDTKT.SelectedValue;
            GetList(_doctypeid, _year,_type);
        }
        #endregion
        private void loadYears()
        {
            List<String> listYears = MiscUtils.GetAllYear();
            drpYears.DataSource = listYears;
            drpYears.DataBind();
            CommonFunc.GetLookUpValue("BA3A0E4C-33BC-475B-B547-4580CD602D68", this.drpLoaiDTKT, 4);
        }

        protected void dataCtrl_ItemCommand(object sender, C1CommandEventArgs e)
        {
            Label PK_DocumentID = (Label)e.Item.FindControl("PK_DocumentID") as Label;
            string id = PK_DocumentID.Text;
            switch(e.CommandName)
            {
                case "changeStatus":
                    /// Run server
                    CommonFunc.UpdateDocStatus(id, 4);
                    GetList(_doctypeid, _year, _type);
                    break;
            }
        }
             
        protected void lbTrinhduyet_Click(object sender, EventArgs e)
        {
        
        }

    }
}
