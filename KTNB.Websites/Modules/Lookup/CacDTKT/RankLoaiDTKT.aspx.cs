using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.Definition.DMS;
using System.Data;
using KTNB.Extended.Biz;
using KTNB.Extended.Entities.CacDoiTuongKiemToan;
using VPB_KTNB.Helpers;

namespace VPB_KTNB.Modules.Lookup.CacDTKT
{
    public partial class RankLoaiDTKT : PageBase
    {
        protected string _action = string.Empty;
        protected string _doctypeid = DOCTYPE.RANK_LDTKT;
        protected string _loaiDTKT = string.Empty;
        protected string _rank = string.Empty;
        protected string _from = string.Empty;
        protected string _to = string.Empty;
        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;
        protected string _docspaceid = string.Empty;

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
            _loaiDTKT = Request["ldtkt"];
            _rank = Request["rank"];
            _from = Request["from"];
            _to = Request["to"];
            if (string.IsNullOrEmpty(_documentid))
                _viewtype = VIEWTYPE.ADDNEW;
            else
                _viewtype = VIEWTYPE.EDIT;

            if (!string.IsNullOrEmpty(_action))
            {
                if (_action == "checkvalue")
                    FeedBackClient(countRank(_loaiDTKT, int.Parse(_rank), int.Parse(_from), int.Parse(_to)).ToString());
            }

            base.InitForm("Quản lý Rank cho loại đối tượng kiểm toán", string.Empty, _doctypeid, 0);
            _btnAddNew.Visible = false;
            btnThem.Attributes.Add("onclick", "{preparesavedoc('" + System.Guid.NewGuid().ToString() + "','" + _doctypeid + "'); return false;}");
            CommonFunc.GetLookUpValue("D156BDD2-ACCB-4985-B95A-1429F44D65B6", this.ID8_D156BDD2_ACCB_4985_B95A_1429F44D65B6, 4);
        }
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            GetList(_doctypeid);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DocumentTypeID"></param>
        private void GetList(string DocumentTypeID)
        {
            List<RankLoaiDoiTuongKiemToan> lst = CoreFactory<RankLoaiDoiTuongKiemToan>.EntityManager.GetList();
            lst = lst.OrderBy(x => x.TenLDTKT).ThenBy(x => x.Rank).ToList();
            dataCtrl.DataSource = lst;
            dataCtrl.DataBind();
        }

        protected int countRank(string loaiDTKT, int rank, int from, int to)
        {
            List<RankLoaiDoiTuongKiemToan> lst = new List<RankLoaiDoiTuongKiemToan>();
            lst = CoreFactory<RankLoaiDoiTuongKiemToan>.EntityManager.GetList(x => x.IDLDTKT == loaiDTKT && x.Rank == rank);
            if (lst.Count > 0) return 1;
            lst = CoreFactory<RankLoaiDoiTuongKiemToan>.EntityManager.GetList();
            int index = lst.FindIndex(x => ((x.MarkFrom <= from && x.MarkTo >= from) || (x.MarkFrom <= to && x.MarkTo >= to) || (x.MarkFrom >= from && x.MarkTo <= to)) && (x.IDLDTKT == loaiDTKT));
            if (index >= 0) return 2;
            return 0;
        }
    }
}
