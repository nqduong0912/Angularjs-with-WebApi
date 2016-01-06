using CORE.CoreObjectContext;
using vpb.app.business.ktnb.CoreBusiness;
using CORE.HELPERS;
using CORE.WFS.CoreBusiness;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;
using System.Collections.Generic;
using System.Globalization;
using KTNB.Extended.Biz;
using KTNB.Extended.Core;
using KTNB.Extended.Commons;
using KTNB.Extended.Dal;
using KTNB.Extended.Enums;

namespace KTNB.Handler
{
    public class SaveDocWorkspace : IHttpHandler, IRequiresSessionState
    {
        private string _action;
        private UserContext _objUserContext;

        public void ProcessRequest(HttpContext context)
        {
            string str = string.Empty;
            if (!context.Response.IsClientConnected)
            {
                str = "Your connection was disconnected. Please try again.";
            }
            else
            {
                if (!string.IsNullOrEmpty(context.Request.Form["act"]))
                    this._action = context.Request.Form["act"];
                this._objUserContext = (UserContext)context.Session["objUserContext"];
                switch (_action)
                {
                    case "I_Botieuchi":
                    case "U_Botieuchi":
                    case "IOU_Botieuchi":
                        str = InsertOrUpdateBoTieuChi(context);
                        break;
                    case "R_Botieuchi":
                        str = RemoveBoTieuChi(context);
                        break;
                    default:
                        str = "Server unknow your action. Please check your action.";
                        break;
                }
                context.Response.ContentType = "text/plain";
                context.Response.Write(str);
                context.Response.Flush();
                context.Response.End();
            }
        }
        private string InsertOrUpdateBoTieuChi(HttpContext context)
        {
            //Lấy dữ liệu để thêm vào bảng EX_DM_BOTIEUCHI;
            //qt_dm_kehoachnam_botieuchi
            //Tên: ID8_078EF917_FC13_41CC_88CB_EFC5B8C47773=daild
            //Diễn giải: &ID8_82190182_4CF8_4A2C_A080_E5E942254A6C=daild
            //&DOCSTATUS=4
            //Công thức: &ID8_DC218153_9C9E_4907_B291_D8540F9DD909= 20%{TC1} %2B 80%{TC2} 
            //&&act=IOU_Botieuchi
            //&year=2015&ldtkt=4a998db1-8376-42a4-971a-541215eb171a&btc=7d58eeb3-e0aa-426f-bf90-496c242b6ba6
            if (string.IsNullOrEmpty(context.Request.Form["ldtkt"]))
                return "Thiếu thông tin loại đối tượng kiểm toán!";
            if (string.IsNullOrEmpty(context.Request.Form["btc"]))
                return "Thiếu thông tin bộ tiêu chí!";
            if (string.IsNullOrEmpty(context.Request.Form["ID8_078EF917_FC13_41CC_88CB_EFC5B8C47773"]))
                return "Thiếu thông tin bộ tiêu chí!";

            int nam;
            int.TryParse(string.IsNullOrEmpty(context.Request.Form["year"]) ? MiscUtils.CurrentYear : context.Request.Form["year"], out nam);
            Guid ldtkt;
            Guid.TryParse(context.Request.Form["ldtkt"], out ldtkt);
            Guid btc;
            Guid.TryParse(context.Request.Form["btc"], out btc);
            string botieuchi = context.Request.Form["ID8_078EF917_FC13_41CC_88CB_EFC5B8C47773"];
            string botieuchi_congthuc = context.Request.Form["ID8_DC218153_9C9E_4907_B291_D8540F9DD909"];
            int status;
            int.TryParse(context.Request.Form["DOCSTATUS"], out status);

            var info = CoreFactory<qt_dm_kehoachnam_botieuchi>.EntityManager.GetInfo(x => x.Nam == nam && x.LDTKT == ldtkt && x.BTC == btc);
            if (info == null)
            {
                return CoreFactory<qt_dm_kehoachnam_botieuchi>.EntityManager.Insert(new qt_dm_kehoachnam_botieuchi()
                {
                    Nam = nam,
                    LDTKT = ldtkt,
                    BTC = btc,
                    TenBTC = botieuchi,
                    Congthuc = botieuchi_congthuc,
                    CreateDate = DateTime.Now,
                    Status = (int)BoTieuChiKeHoachNamEnum.KhoiTao,
                    IsActive = status,
                    IsOn = 0
                }) > 0 ? string.Empty : "Có lỗi xảy ra!";
            }
            else
            {
                info.Nam = nam;
                info.LDTKT = ldtkt;
                info.BTC = btc;
                info.TenBTC = botieuchi;
                info.Congthuc = botieuchi_congthuc;
                info.CreateDate = DateTime.Now;
                info.Status = (int)BoTieuChiKeHoachNamEnum.KhoiTao;
                return CoreFactory<qt_dm_kehoachnam_botieuchi>.EntityManager.Update(info) > 0 ? string.Empty : "Có lỗi xảy ra!";
            }
        }
        private string RemoveBoTieuChi(HttpContext context)
        {
            //Lấy dữ liệu để thêm vào bảng EX_DM_BOTIEUCHI;
            //qt_dm_kehoachnam_botieuchi
            //Tên: ID8_078EF917_FC13_41CC_88CB_EFC5B8C47773=daild
            //Diễn giải: &ID8_82190182_4CF8_4A2C_A080_E5E942254A6C=daild
            //&DOCSTATUS=4
            //Công thức: &ID8_DC218153_9C9E_4907_B291_D8540F9DD909= 20%{TC1} %2B 80%{TC2} 
            //&&act=IOU_Botieuchi
            //&year=2015&ldtkt=4a998db1-8376-42a4-971a-541215eb171a&btc=7d58eeb3-e0aa-426f-bf90-496c242b6ba6
            if (string.IsNullOrEmpty(context.Request.Form["ldtkt"]))
                return "Thiếu thông tin loại đối tượng kiểm toán!";
            if (string.IsNullOrEmpty(context.Request.Form["btc"]))
                return "Thiếu thông tin bộ tiêu chí!";

            int nam;
            int.TryParse(string.IsNullOrEmpty(context.Request.Form["year"]) ? MiscUtils.CurrentYear : context.Request.Form["year"], out nam);
            Guid ldtkt;
            Guid.TryParse(context.Request.Form["ldtkt"], out ldtkt);
            Guid btc;
            Guid.TryParse(context.Request.Form["btc"], out btc);

            var info = CoreFactory<qt_dm_kehoachnam_botieuchi>.EntityManager.GetInfo(x => x.Nam == nam && x.LDTKT == ldtkt && x.BTC == btc);
            if (info != null)
                return CoreFactory<qt_dm_kehoachnam_botieuchi>.EntityManager.Delete(info.Id) > 0 ? string.Empty : "Có lỗi xảy ra!";
            else
                return "Có lỗi xảy ra!";
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
