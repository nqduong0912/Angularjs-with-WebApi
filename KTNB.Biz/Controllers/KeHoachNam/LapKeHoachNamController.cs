using System.Linq;
using System.Web.Http;
using KTNB.Biz.Definition.SqlQueries;
using KTNB.Extended.Commons;
using KTNB.Extended.Commons.PagedUtils;
using KTNB.Extended.Entities.KeHoachNam;
using KTNB.Extended.Enums;
using KTNB.Extended.Extensions;

namespace KTNB.Biz.Controllers.KeHoachNam
{
    public class LapKeHoachNamController : BaseApiController
    {
        // GET: api/LapKeHoachNam/Get
        [HttpGet]
        public IHttpActionResult Get(int? year, int? page1, int? page2)
        {
            var results = new
            {
                DsNam = MiscUtils.GetAllYears(),
                Rooms = GetDanhSachPhongBan().ToList(),
                DsLoaiDTKT = GetDanhSachLoaiDTKTByNam(year),
                Users = GetDanhSachUsers().ToList(),
                DsTanSuat = GetDanhSachTanSuatByNam(year),
                BoQuyMo = GetBoQyMoActiveByNam(year),
                DsQuyMo = GetDsQuyMoActiveByNam(year),
                NguonLucNam = GetNguonLucNamByNam(year),
                NguonLucCanThiet = GetNguonCanThietByNam(year),
                KeHoachNam1Paged = GetDsKeHoachNam1(page1, year, null, null),
                KeHoachNam2Paged = GetDsKeHoachNam2(page2, year, null, null),
                TrangThaiNam = GetDanhMucNam(year)
            };

            return Ok(results);
        }

        // GET: api/LapKeHoachNam/Filter1
        [HttpGet]
        public IHttpActionResult Filter1(int? year, int? page, int? loaiDTKT, string tenDTKT)
        {
            var results = new
            {
                KeHoachNam1Paged = GetDsKeHoachNam1(page, year, loaiDTKT, tenDTKT)
            };

            return Ok(results);
        }

        // GET: api/LapKeHoachNam/Filter2
        [HttpGet]
        public IHttpActionResult Filter2(int? year, int? page, int? loaiDTKT, string tenDTKT)
        {
            var results = new
            {
                KeHoachNam2Paged = GetDsKeHoachNam2(page, year, loaiDTKT, tenDTKT)
            };

            return Ok(results);
        }

        // POST api/LapKeHoachNam/Edit
        [HttpPost]
        public IHttpActionResult Edit(DuyetKeHoachNam model)
        {
            if (ModelState.IsValid)
            {
                string editQuery = @"
IF EXISTS(SELECT TOP 1 TrangThaiKeHoachNam FROM [dbo].[EX_DM_NAM] WHERE Nam = @p11)
	UPDATE [dbo].[EX_DM_NAM] SET TrangThaiKeHoachNam = 0
--
UPDATE [dbo].[EX_QT_NAM_DTKT]
SET DiemQuyDoi = @p1, LastKT = @p2, QuyMoId = @p3, NextKT = @p4, TanSuat = @p5, MucTieu = @p6, PhamVi = @p7, PhongBan = @p8, Leader = @p9, Manager = @p10
WHERE Id = @p0";
                int change = Db.Database.ExecuteSqlCommand(editQuery, model.Id, model.DiemQuyDoi, model.ThoiGianKTGanNhat, model.QuyMoId, model.ThangDuKienKT, model.TanSuat, model.MucTieu, model.PhamVi, model.Phong, model.TruongDoan, model.Manager, model.Nam);
                if (change > 0)
                {
                    var results = new
                    {
                        NguonLucCanThiet = GetNguonCanThietByNam(model.Nam),
                        TrangThaiNam = GetDanhMucNam(model.Nam)

                    };
                    return Ok(results);
                }

                return Ok("Không có sự thay đổi dữ liệu.");
            }

            return BadRequest();
        }

        // POST api/LapKeHoachNam/XinDuyet
        [HttpPost]
        public IHttpActionResult XinDuyet(int id)
        {
            const string query = @"
UPDATE [dbo].[EX_DM_NAM]
SET [TrangThaiKeHoachNam] = @p1
WHERE Nam = @p0";
            int year = id;
            int changed = Db.Database.ExecuteSqlCommand(query, year, KeHoachNamEnum.ChuaDuyet);
            if (changed > 0)
            {
                return Ok(new
                {
                    TrangThaiNam = GetDanhMucNam(year)
                });
            }

            return BadRequest();
        }

        // POST api/LapKeHoachNam/XuatExcel
        [HttpPost]
        public IHttpActionResult XuatExcel()
        {
            if (ModelState.IsValid)
            {
                return Ok();
            }

            return BadRequest();
        }

        private PagedList<DoiTuongKiemToan> GetDsKeHoachNam1(int? page, int? year, int? loaiDTKT, string tenDTKT)
        {
            int currentYear = year.GetYearOrDefault();
            string sqlQuery = Queries.GetDsLapKeHoachNamQuery + " AND [IsTrong3Nam] = 0";
            if (loaiDTKT.HasValue)
            {
                sqlQuery += " AND l.Id = @p1";
            }

            if (!string.IsNullOrEmpty(tenDTKT))
            {
                sqlQuery += " AND l.Ten LIKE @p2";
            }

            var pagedList = GetPagedList<DoiTuongKiemToan>(sqlQuery, page, currentYear, loaiDTKT, string.Format("%{0}%", tenDTKT));

            return pagedList;
        }

        private PagedList<DoiTuongKiemToan> GetDsKeHoachNam2(int? page, int? year, int? loaiDTKT, string tenDTKT)
        {
            int currentYear = year.GetYearOrDefault();
            string sqlQuery = Queries.GetDsLapKeHoachNamQuery + " AND [IsTrong3Nam] = 1"; ;
            if (loaiDTKT.HasValue)
            {
                sqlQuery += " AND l.Id = @p1";
            }

            if (!string.IsNullOrEmpty(tenDTKT))
            {
                sqlQuery += " AND l.Ten LIKE @p2";
            }

            var pagedList = GetPagedList<DoiTuongKiemToan>(sqlQuery, page, currentYear, loaiDTKT, string.Format("%{0}%", tenDTKT));

            return pagedList;
        }
    }
}
