using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using KTNB.Extended.Biz;
using KTNB.Extended.Dal;
using vpb.app.business.ktnb.Definition.DMS;
using VPB_KTNB.Helpers;

namespace VPB_KTNB.Modules.Lookup.CacDTKT
{
    public partial class QuyMoDTKT_Input : PageBase
    {
        //public string _documentid = string.Empty;
        //public string _doctype = DOCTYPE.QuyMoDTKT;
        //private string _idboquymo = string.Empty;
        //private byte _viewtype = 0;
        //public string _action = string.Empty;
        //private dm_boquymo dmBoquymo;
        //public List<dm_quymo> lstQuymo
        //{
        //    get
        //    {
        //        if(string.IsNullOrEmpty(hddQuyMo.Value))
        //            return new List<dm_quymo>();
        //        return JsonHelper.ToList<dm_quymo>(hddQuyMo.Value);
        //    }
        //}
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            base.AuthorizeUserCtx();
            base.SetFormCaption("Thêm mới bộ quy mô");
            //_documentid = Request["doc"];
            //_idboquymo = Request["idboqm"];
            //_action = Request["act"];
            //if (string.IsNullOrEmpty(_documentid))
            //{
            //    _viewtype = VIEWTYPE.ADDNEW;
            //    base.SetFormCaption("Thêm mới bộ quy mô");

            //}

            //else
            //{
            //    _viewtype = VIEWTYPE.EDIT;
            //    base.SetFormCaption("Thông tin bộ quy mô");
            //}
            //btnBack.Attributes.Add("onclick", "{backMainPage(); return false;}");
            //btnSave.Attributes.Add("onclick", "saveValue(); return false;");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //CommonFunc.GetT_Type_Doc_Property("410D55A8-48D8-4EED-894D-836E24E1E36D", "TEXTVALUE", drpLoaiDTKT);
                //CommonFunc.GetYear2Dropdownlist(drpNam);
                //if (!string.IsNullOrEmpty(_action))
                //{
                //    dmBoquymo = new dm_boquymo();
                //    CommonFunc.LoadDocInfo(_documentid, Page.Master);
                //    dmBoquymo = ManagerFactory.boquymo_manager.GetInfo(int.Parse(_idboquymo));
                //    drpLoaiDTKT.SelectedItem.Value = dmBoquymo.LoaiDTKT.ToString();
                //    drpNam.SelectedItem.Value = dmBoquymo.Nam.ToString();
                //    txtTenBoQM.Text = dmBoquymo.Ten;
                //    hddQuyMo.Value = ManagerFactory.t_type_doc_property.GetTextValue(_documentid);
                //    rptQuyMo.DataSource = lstQuymo;
                //    rptQuyMo.DataBind();
                //}
            }

        }
 
        //protected void btnNewQM_OnClick(object sender, EventArgs e)
        //{
        //    int nguonluc;
        //    int.TryParse(txtNguonLuc.Text, out nguonluc);
        //    List<dm_quymo> _lstQuymo = new List<dm_quymo>();
        //    if (!string.IsNullOrEmpty(hddQuyMo.Value))
        //    {
        //        _lstQuymo = JsonHelper.ToList<dm_quymo>(hddQuyMo.Value);
        //    }
        //    _lstQuymo.Add(new dm_quymo() { Ten = txtQM.Text, NguonLuc = nguonluc });
        //    hddQuyMo.Value = JsonHelper.ToJSON(_lstQuymo);
        //    //Clear data
        //    txtQM.Text = string.Empty;
        //    txtNguonLuc.Text = string.Empty;
        //    //bind data to repeater
        //    rptQuyMo.DataSource = _lstQuymo;
        //    rptQuyMo.DataBind();
        //}

        //protected void rptQuyMo_OnItemCommand(object source, RepeaterCommandEventArgs e)
        //{
        //    switch (e.CommandName)
        //    {
        //        case "delete":
        //            if (!string.IsNullOrEmpty(hddQuyMo.Value))
        //            {
        //                List<dm_quymo> lst = JsonHelper.ToList<dm_quymo>(hddQuyMo.Value);
        //                int index = lst.FindIndex(x => x.Ten == e.CommandArgument.ToString());
        //                lst.RemoveAt(index);
        //                hddQuyMo.Value = JsonHelper.ToJSON(lst);
        //                rptQuyMo.DataSource = lst;
        //                rptQuyMo.DataBind();
        //            }
        //            break;
        //        default:
        //            break;
        //    }
        //}
    }
}