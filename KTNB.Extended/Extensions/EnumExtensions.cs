using KTNB.Extended.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KTNB.Extended.Extensions
{
    public static class EnumExtensions
    {
        public static string GetText(this KeHoachNamEnum status)
        {
            string text;
            switch (status)
            {
                case KeHoachNamEnum.KhoiTao:
                    text = "Khởi tạo";
                    break;
                case KeHoachNamEnum.ChuaDuyet:
                    text = "Chưa duyệt";
                    break;
                case KeHoachNamEnum.DaKiemTra:
                    text = "BGĐ đã kiểm tra";
                    break;
                case KeHoachNamEnum.BanKiemSoatDuyet:
                    text = "BKS duyệt";
                    break;
                case KeHoachNamEnum.DieuChinh:
                    text = "Điều chỉnh";
                    break;
                default:
                    text = "N/A";
                    break;
            }

            return text;
        }

        public static string GetTrangThai(this bool trangThai)
        {
            return trangThai ? "Active" : "Inactive";
        }
    }
}