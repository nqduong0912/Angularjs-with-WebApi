using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using KTNB.Biz.Definition.SqlQueries;
using KTNB.Extended.Biz;
using KTNB.Extended.Commons.Helpers;
using KTNB.Extended.Commons.PagedUtils;
using KTNB.Extended.Dal.Lib;
using KTNB.Extended.Entities.KhoiTaoJob;
using KTNB.Extended.Enums;
using Newtonsoft.Json;

namespace KTNB.Biz.Controllers.KhoiTaoJob
{
    public class ThongTinDotKiemToanController : BaseApiController
    {
        private List<ThanhVienDotKT> lstThanhVienTrongDotKT;
        // GET api/DotKiemToanController/Get
        [HttpGet]
        [ResponseType(typeof(PagedList<DotKiemToan>))]
        public IHttpActionResult Get(int id, Guid loaiDoiTuongKiemToanId)
        {
            lstThanhVienTrongDotKT = GetDsThanhVienTrongDotKT(id);
            var dotKiemToan = GetDotKiemToan(id);
            string strUploadFile = dotKiemToan.ThanhVienDotKiemToanUploadFile;
            var uploadFilebyThanhVien = string.IsNullOrEmpty(strUploadFile) ? null : JsonConvert.DeserializeObject<List<UpLoadFileDotKiemToan>>(strUploadFile);
            var result = new
            {
                DsLoaiDTKT = CoreFactory<dm_li_loaidoituongkiemtoan>.EntityManager.GetList(x => x.Status == 4),
                DotKiemToan = dotKiemToan,
                DsDonViLienQuan = JsonHelper.ToList<DonViLienQuan>(dotKiemToan.DonViLienQuan),
                DsFileThongTinChung = JsonHelper.ToList<UpLoadFileDotKiemToan>(dotKiemToan.FileThongTinChung),
                DsUsers = Db.Database.SqlQuery<ThanhVienDotKiemToan>(Queries.GetDsThanhVienDotKiemToan, id).ToList(),
                DsUploadFileByThanhVien = uploadFilebyThanhVien,
                DsMangNghiepVu = Db.Database.SqlQuery<MangNghiepVu>(Queries.GetMangNghiepVu, loaiDoiTuongKiemToanId.ToString()).ToList(),
                DsThanhVienTrongDotKT = lstThanhVienTrongDotKT
            };

            return Ok(result);
        }

        //// GET api/DotKiemToanController/Filter
        //[HttpGet]
        //[ResponseType(typeof(PagedList<DotKiemToan>))]
        //public IHttpActionResult Filter(int? page, int? trangThai)
        //{
        //    var pagedList = GetDsDotKiemToan(page, trangThai);

        //    return Ok(pagedList);
        //}

        private DotKiemToan GetDotKiemToan(int id)
        {
            string sqlQuery = @"
                SELECT TOP 1 n.[Id], n.Nam, n.TenDotKT AS [DotKT], n.LDTKT AS [LoaiDTKT], l.Ten AS [LoaiDTKTText], 
                n.[DTKT], n.TenDTKT AS [DTKTText], n.[NextKT] AS [ThangDuKienKT], n.[TrongKeHoach], n.[DonViLienQuan], 
                n.[TrangThai], n.[LapKHStart], CONVERT(nvarchar,n.LapKHStart,103) AS LapKHStartText, n.[LapKHEnd], CONVERT(nvarchar,n.LapKHEnd,103) AS LapKHEndText,right(CONVERT(nvarchar,n.[LapKHStart],103),7)+'-'+right(CONVERT(nvarchar,n.[LapKHEnd],103),7) AS [LapKH], 
                n.[ThucDiaStart], CONVERT(nvarchar,n.ThucDiaStart,103) AS ThucDiaStartText, n.[ThucDiaEnd], CONVERT(nvarchar,n.ThucDiaEnd,103) AS ThucDiaEndText, right(CONVERT(nvarchar,n.[ThucDiaStart],103),7)+'-'+right(CONVERT(nvarchar,n.[ThucDiaEnd],103),7) AS [ThucDia],
                n.[BaoCaoStart], CONVERT(nvarchar,n.BaoCaoStart,103) AS BaoCaoStartText,n.[BaoCaoEnd], CONVERT(nvarchar,n.BaoCaoEnd,103) AS BaoCaoEndText,right(CONVERT(nvarchar,n.[BaoCaoStart],103),7)+'-'+right(CONVERT(nvarchar,n.[BaoCaoEnd],103),7) AS [BaoCao], n.[FileThongTinChung],n.[MucTieu], n.[PhamVi], n.[NgayQD], CONVERT(nvarchar,n.NgayQD,103) AS NgayQDText,
				n.Manager, u.[FullName] as [ManagerText], n.ThanhVienDotKiemToanUploadFile
                FROM [dbo].[EX_QT_NAM_DTKT] AS n
                LEFT JOIN [dbo].[EX_DM_LOAIDTKT] AS l ON n.LDTKT = l.SourceId AND n.Nam = l.Nam
				LEFT JOIN [dbo].[T_USER] AS u ON n.Manager = u.PK_UserID
                Where n.Id = @p0";

            var result = Db.Database.SqlQuery<DotKiemToan>(sqlQuery, id).FirstOrDefault();
            result.TrangThaiText = result.TrangThai == 0 ? "Chưa thực hiện" : "Đã duyệt";
            return result;
        }

        // GET api/ThongTinDotKiemToan/Filter
        [HttpGet]
        [ResponseType(typeof(PagedList<DotKiemToan>))]
        public IHttpActionResult Filter(string loaiDTKT)
        {
            var result = new
            {
                DsDTKT = CoreFactory<dm_li_doituongkiemtoan>.EntityManager.GetList(x => x.IDLDTKT == loaiDTKT)
            };

            return Ok(result);
        }

        // GET api/ThongTinDotKiemToan/PushDV
        [HttpGet]
        [ResponseType(typeof(PagedList<DotKiemToan>))]
        public IHttpActionResult PushDV(string DTKT, string loaiDTKT)
        {
            var result = new
            {
                DsDTKT = CoreFactory<dm_li_doituongkiemtoan>.EntityManager.GetList(x => x.IDLDTKT == loaiDTKT)
            };

            return Ok(result);
        }

        // POST api/ThongTinDotKiemToan/Edit
        [HttpPost]
        public IHttpActionResult Edit(DotKiemToan dotKiemToan)
        {
            dotKiemToan.NgayQD = DateTime.ParseExact(dotKiemToan.NgayQDText, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            dotKiemToan.LapKHStart = DateTime.ParseExact(dotKiemToan.LapKHStartText, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            dotKiemToan.LapKHEnd = DateTime.ParseExact(dotKiemToan.LapKHEndText, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            dotKiemToan.ThucDiaStart = DateTime.ParseExact(dotKiemToan.ThucDiaStartText, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            dotKiemToan.ThucDiaEnd = DateTime.ParseExact(dotKiemToan.ThucDiaEndText, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            dotKiemToan.BaoCaoStart = DateTime.ParseExact(dotKiemToan.BaoCaoStartText, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            dotKiemToan.BaoCaoEnd = DateTime.ParseExact(dotKiemToan.BaoCaoEndText, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            if (dotKiemToan.LapKHStart > dotKiemToan.LapKHEnd)
            {
                ModelState.AddModelError("Lập kế hoạch", "Ngày lập KH bắt đầu phải trước ngày kết thúc.");
            }
            if (dotKiemToan.ThucDiaStart > dotKiemToan.ThucDiaEnd)
            {
                ModelState.AddModelError("Thực địa", "Ngày thực địa bắt đầu phải trước ngày kết thúc.");
            }
            if (dotKiemToan.BaoCaoStart > dotKiemToan.BaoCaoEnd)
            {
                ModelState.AddModelError("Báo cáo", "Ngày báo cáo bắt đầu phải trước ngày kết thúc.");
            }
            if (ModelState.IsValid)
            {
                string SuaDotKiemToan = @"
                    UPDATE [dbo].[EX_QT_NAM_DTKT]
                    SET NextKT = @p1, NgayQD = @p2, TenDotKT = @p3, MucTieu = @p4, PhamVi = @p5, LapKHStart = @p6, LapKHEnd = @p7, ThucDiaStart = @p8, ThucDiaEnd = @p9, BaoCaoStart = @p10, BaoCaoEnd = @p11, DonViLienQuan = @p12, FileThongTinChung = @p13
                    WHERE Id = @p0";

                int change = Db.Database.ExecuteSqlCommand(SuaDotKiemToan, dotKiemToan.Id, dotKiemToan.ThangDuKienKT, dotKiemToan.NgayQD, dotKiemToan.DotKT, dotKiemToan.MucTieu, dotKiemToan.PhamVi, dotKiemToan.LapKHStart, dotKiemToan.LapKHEnd, dotKiemToan.ThucDiaStart, dotKiemToan.ThucDiaEnd, dotKiemToan.BaoCaoStart, dotKiemToan.BaoCaoEnd, dotKiemToan.DonViLienQuan, dotKiemToan.FileThongTinChung);
                if (change > 0)
                {
                    var results = new
                    {
                    };
                    return Ok(results);
                }

                return Ok("Không có sự thay đổi dữ liệu.");
            }

            return BadRequest(ModelState);
        }

        // POST api/ThongTinDotKiemToan/EditListUser
        [HttpPost]
        public IHttpActionResult EditListUser(int id, List<ThanhVienDotKT> lst)
        {
            if (ModelState.IsValid)
            {
                string SuaDotKiemToan = @"
                            UPDATE [dbo].[EX_QT_ThanhVienDotKiemToan]
                            SET QuyTrinh = null
                            WHERE IdDotKiemToan = @p0";
                int change = Db.Database.ExecuteSqlCommand(SuaDotKiemToan, id);
                if (change >0 )
                    for (int i = 0; i < lst.Count; i++)
                    {
                        ThanhVienDotKT thanhvien = lst[i];
                        SuaDotKiemToan = @"
                        UPDATE [dbo].[EX_QT_ThanhVienDotKiemToan]
                        SET QuyTrinh = @p1
                        WHERE UserId = @p0";
                        change = Db.Database.ExecuteSqlCommand(SuaDotKiemToan, thanhvien.PK_UserID, thanhvien.QuyTrinh);
                    }
                var results = new
                {
                };
                return Ok(results);
            }

            return BadRequest(ModelState);
        }

        // POST api/ThongTinDotKiemToan/SubmitJob
        [HttpPost]
        public IHttpActionResult SubmitJob(int id)
        {
            const string submitJobQuery = @"
UPDATE [dbo].[EX_DM_NAM] SET [TrangThaiDotKiemToan] = @p1
WHERE [TrangThaiDotKiemToan] IS NULL
	AND [Nam] = (SELECT TOP 1 Nam WHERE Id = @p0)";
            int changed = Db.Database.ExecuteSqlCommand(submitJobQuery, id, DotKiemToanEnum.KhoiTao);
            if (changed > 0)
            {
                return Ok();
            }

            return BadRequest();
        }

        // GET api/ThongTinDotKiemToan/GetDsQuyTrinh
        [HttpGet]
        [ResponseType(typeof(List<quytrinh>))]
        public IHttpActionResult GetDsQuyTrinh(string idMangNv)
        {
            var result = new
            {
                DsQuyTrinh = CoreFactory<quytrinh>.EntityManager.GetList(x => x.Status == 4 && x.IDMangNV == idMangNv)
            };

            return Ok(result);
        }

        private List<ThanhVienDotKT> GetDsThanhVienTrongDotKT(int id)
        {
            string sqlQuery = @"
                SELECT t1.UserId as PK_UserID, t2.Fullname as UserName, t1.QuyTrinh, t3.Ten as QuyTrinhText
                FROM [dbo].[EX_QT_ThanhVienDotKiemToan] AS t1
                LEFT JOIN [dbo].[T_USER] AS t2 ON t1.UserId = t2.PK_UserID
                LEFT JOIN [dbo].[V_LIB_DM_QUYTRINH] AS t3 ON t1.QuyTrinh = t3.PK_DocumentID
                Where t1.IdDotKiemToan = @p0";
            var result = Db.Database.SqlQuery<ThanhVienDotKT>(sqlQuery, id).ToList();
            return result;
        }

        // GET api/ThongTinDotKiemToan/GetRiskProfileByMangNghiepVu

        
        private PagedList<RiskProfile> GetRiskProfile(Guid documentID, int? page)
        {
            var pagedList = GetPagedList<RiskProfile>(Queries.GetRiskProfile, page, documentID);
            return pagedList;
        }

        [HttpGet]
        [ResponseType(typeof(PagedList<RiskProfile>))]
        public IHttpActionResult GetRiskProfileByMangNghiepVu(Guid documentID, int? page)
        {
            var results = new
            {
                DsRiskProfile = GetRiskProfile(documentID, page)
            };
            return Ok(results);
        }

        // POST api/ThongTinDotKiemToan/UpdateTenMangNghiepVu
        [HttpPost]
        public IHttpActionResult UpdateTenMangNghiepVu(MangNghiepVu modelMangNghiepVu)
        {
            int isUpdated = Db.Database.ExecuteSqlCommand(Queries.UpdateMangNghiepVu, modelMangNghiepVu.Ten, modelMangNghiepVu.PK_DocumentID);
            if (isUpdated > 0)
            {
                return Ok("Thay đổi thành công!");
            }
            return BadRequest("Thay đổi không thành công. Vui lòng nhập lại mảng nghiệp vụ!");
        }
    }
}