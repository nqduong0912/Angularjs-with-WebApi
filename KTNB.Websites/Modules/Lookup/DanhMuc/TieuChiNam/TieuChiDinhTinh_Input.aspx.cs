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
using System.Drawing;
using KTNB.Extended.Commons.Helpers;
using VPB_KTNB.Helpers;

namespace VPB_KTNB.Modules.Lookup.DanhMuc.TieuChiNam
{
    public partial class TieuChiDinhTinh_Input : PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;
        protected string _doctypeid = DOCTYPE.TIEUCHI_DINHTINH;
        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;
        protected string _docspaceid = string.Empty;
        protected string _property = string.Empty;
        protected string _propertyvalue = string.Empty;
        protected string _diem = string.Empty;
        protected string _diengiai = string.Empty;
        protected string _valueactive = string.Empty;
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
            _propertyvalue = Request["v"];
            _diengiai = Request["diengiai"];
            _diem = Request["diem"];
            _valueactive = Request["valueactive"];
            #endregion

            if (string.IsNullOrEmpty(_documentid))
                _viewtype = VIEWTYPE.ADDNEW;
            else
            {
                _viewtype = VIEWTYPE.EDIT;
            }
            #region action handler
            if (!string.IsNullOrEmpty(_action))
            {
                if (_action == "checkvalue")
                    FeedBackClient(CommonFunc.CountPropertyValue(_property, _propertyvalue).ToString());
                if (_action == "checkvalueupdate")
                    FeedBackClient(CommonFunc.CountPropertyValue(_property, _propertyvalue, _documentid).ToString());
            }
            #endregion

            #region init form
            string caption = "Thêm mới tiêu chí định tính";
            if (!string.IsNullOrEmpty(_documentid))
                caption = "Thông tin tiêu chí định tính";
            base.InitForm(caption, string.Empty, _doctypeid, _viewtype);
            ID8_1EB1E332_0620_4AD1_A87B_D672E7D04BC3.Style.Add("display", "none");
            #endregion

            #region client control event handler
            _btnSave.Attributes.Add("onclick", "{preparesavedoc('" + System.Guid.NewGuid().ToString() + "','" + _doctypeid + "'); return false;}");
            //_btnEdit.Attributes.Add("onclick", "{updatedocument('" + _documentid + "',update_success,update_error); return false;}");
            _btnEdit.Attributes.Add("onclick", "{prepareupdatedoc('" + _documentid + "'); return false;}");
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
                //CommonFunc.GetLookUpValue("3952C713-E304-443D-9CC7-C55D51408A5F", this.ID8_3952C713_E304_443D_9CC7_C55D51408A5F, 4);
                if (!string.IsNullOrEmpty(_action))
                    if (_action == "loaddoc")
                    {
                        CommonFunc.LoadDocInfo(_documentid, Page.Master);
                    }


                    if (!string.IsNullOrEmpty(ID8_1EB1E332_0620_4AD1_A87B_D672E7D04BC3.Text))
                    {
                        List<ThanhPhanDinhTinh> lst = new List<ThanhPhanDinhTinh>();
                        lst = JsonHelper.ToList<ThanhPhanDinhTinh>(ID8_1EB1E332_0620_4AD1_A87B_D672E7D04BC3.Text);
                        dsTPDinhTinh.DataSource = lst;
                        dsTPDinhTinh.DataBind();
                    }

            }
        }
        #endregion

        protected void btnCapNhatDinhTinh_Click(object sender, EventArgs e)
        {
            
            List<ThanhPhanDinhTinh> lst =  new List<ThanhPhanDinhTinh>();
            if (!string.IsNullOrEmpty(ID8_1EB1E332_0620_4AD1_A87B_D672E7D04BC3.Text))
            {
                lst = JsonHelper.ToList<ThanhPhanDinhTinh>(ID8_1EB1E332_0620_4AD1_A87B_D672E7D04BC3.Text);
            }
            int index = lst.FindIndex(x => x.Ten == tbGTDinhTinh.Text);
            if (index >=0)
            {
                setText(tbDinhTinh, "Giá trị này đã tồn tại");
                return;
            }
            if (String.IsNullOrEmpty(tbGTDinhTinh.Text))
            {
                setText(tbDinhTinh, "Nhập tên giá trị định tính");
                return;
            }
            tbDinhTinh.Text = "";
            ThanhPhanDinhTinh tp = new ThanhPhanDinhTinh();
            tp.Ten = this.tbGTDinhTinh.Text;
            tp.Diem = int.Parse(tbDiemDinhTinh.Text);
            lst.Add(tp);
            ID8_1EB1E332_0620_4AD1_A87B_D672E7D04BC3.Text = JsonHelper.ToJSON(lst);
            dsTPDinhTinh.DataSource = lst;
            dsTPDinhTinh.DataBind();
        }
        protected void setText(Label tb, String text)
        {
            tb.Text = text;
            tb.ForeColor = Color.Red;
            tb.Font.Bold = true;
        }
        protected void dsTPDinhTinh_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "delete":
                    string jsonDinhTinh = ID8_1EB1E332_0620_4AD1_A87B_D672E7D04BC3.Text;
                    if (!string.IsNullOrEmpty(jsonDinhTinh))
                    {
                        List<ThanhPhanDinhTinh> lst = JsonHelper.ToList<ThanhPhanDinhTinh>(jsonDinhTinh);
                        int index = lst.FindIndex(x => x.Ten == e.CommandArgument.ToString());
                        lst.RemoveAt(index);
                        ID8_1EB1E332_0620_4AD1_A87B_D672E7D04BC3.Text = JsonHelper.ToJSON(lst);
                        dsTPDinhTinh.DataSource = lst;
                        dsTPDinhTinh.DataBind();
                    }
                    break;
                default:
                    break;
            }
        }

    }
    public class ThanhPhanDinhTinh
    {
        public string Ten { set; get; }
        public int Diem { set; get; }
    }

    
}