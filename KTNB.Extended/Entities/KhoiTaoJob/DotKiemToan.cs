using System;
using ServiceStack.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KTNB.Extended.Entities.KhoiTaoJob
{
    /// <summary>
    /// EX_QT_NAM_DTKT
    /// </summary>
    public class DotKiemToan
    {
        [Key]
        public int Id { get; set; }

        public int Nam { get; set; }

        public string DotKT { get; set; }

        public Guid? LoaiDTKT { get; set; }

        public string LoaiDTKTText { get; set; }

        public Guid? DTKT { get; set; }

        public string DTKTText { get; set; }

        public byte? ThangDuKienKT { get; set; }

        public bool? TrongKeHoach { get; set; }

        public string DonViLienQuan { get; set; }

        public string DonViLienQuanText { get; set; }

        public int TrangThai { get; set; }

        public string TrangThaiText { get; set; }

        public string LapKH { get; set; }

        public DateTime? LapKHStart { get; set; }

        public string LapKHStartText { get; set; }

        public DateTime? LapKHEnd { get; set; }

        public string LapKHEndText { get; set; }

        public string ThucDia { get; set; }

        public DateTime? ThucDiaStart { get; set; }

        public string ThucDiaStartText { get; set; }

        public DateTime? ThucDiaEnd { get; set; }

        public string ThucDiaEndText { get; set; }

        public string BaoCao { get; set; }

        public DateTime? BaoCaoStart { get; set; }

        public string BaoCaoStartText { get; set; }

        public DateTime? BaoCaoEnd { get; set; }

        public string BaoCaoEndText { get; set; }

        public string FileThongTinChung { get; set; }

        public DateTime? NgayQD { get; set; }

        public string NgayQDText { get; set; }

        public string MucTieu { get; set; }

        public string PhamVi { get; set; }

        public Guid? Manager { get; set; }

        public string ManagerText { get; set; }

        public string ThanhVienDotKiemToanUploadFile { get; set; }
    }

    public class DonViLienQuan
    {
        public string IDDTKT { set; get; }
        public string TenDTKT { set; get; }
        public string IDLoaiDTKT { set; get; }
        public string TenLoaiDTKT { set; get; }
    }
}
