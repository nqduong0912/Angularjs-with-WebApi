using System.Linq;
using System.Web.Http;
using KTNB.Biz.Definition.SqlQueries;
using KTNB.Extended.Commons;
using KTNB.Extended.Commons.PagedUtils;
using KTNB.Extended.Entities.KeHoachNam;
using KTNB.Extended.Enums;
using KTNB.Extended.Extensions;
using vpb.app.business.ktnb.Definition.UMS;

namespace KTNB.Biz.Controllers.KeHoachNam
{
    public class DuyetKeHoachNamController : BaseApiController
    {
        // GET api/DuyetKeHoachNam/Get
        [HttpGet]
        public IHttpActionResult Get(int? page, int? year, int? loaiDTKT)
        {
            var currentDanhMucNam = GetDanhMucNam(year);
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
                TrangThaiNam = GetDanhMucNam(year),
                // TrangThai = currentDanhMucNam.TrangThaiKeHoachNam,
                // TrangThaiText = currentDanhMucNam.TrangThaiKeHoachNam.GetText(),
                KeHoachNamPaged = GetDsKeHoachNam(page, year, loaiDTKT)
            };

            return Ok(results);
        }

        // GET api/DuyetKeHoachNam/Filter
        [HttpGet]
        public IHttpActionResult Filter(int? page, int? year, int? loaiDTKT)
        {
            var results = new
            {
                KeHoachNamPaged = GetDsKeHoachNam(page, year, loaiDTKT),
                NguonLucNam = GetNguonLucNamByNam(year),
                NguonLucCanThiet = GetNguonCanThietByNam(year)
            };

            return Ok(results);
        }

        // POST api/DuyetKeHoachNam/Edit
        [HttpPost]
        public IHttpActionResult Edit(DuyetKeHoachNam model)
        {
            if (ModelState.IsValid)
            {
                int change = Db.Database.ExecuteSqlCommand(Queries.SuaKeHoachNamQuery, model.Id, model.DiemQuyDoi, model.ThoiGianKTGanNhat, model.QuyMoId, model.ThangDuKienKT, model.TanSuat, model.MucTieu, model.PhamVi, model.Phong, model.TruongDoan, model.Manager);
                if (change > 0)
                {
                    var results = new
                    {
                        NguonLucCanThiet = GetNguonCanThietByNam(model.Nam)
                    };
                    return Ok(results);
                }

                return Ok("Không có sự thay đổi dữ liệu.");
            }

            return BadRequest();
        }

        // POST api/DuyetKeHoachNam/Duyet
        [HttpPost]
        public IHttpActionResult Duyet(int id)
        {
            if (ModelState.IsValid)
            {
                int year = id;
                KeHoachNamEnum trangThai;
                string query;
                if (UserContext.IsInGroup(GROUPS.KTNB1))
                {
                    trangThai = KeHoachNamEnum.DaKiemTra;
                    query = @"
UPDATE [dbo].[EX_DM_NAM] SET [TrangThaiKeHoachNam] = @p0
WHERE [TrangThaiKeHoachNam] = 1 AND Nam = @p1";
                }
                else if (UserContext.IsInGroup(GROUPS.BKS))
                {
                    trangThai = KeHoachNamEnum.BanKiemSoatDuyet;
                    query = @"
UPDATE [dbo].[EX_DM_NAM] SET [TrangThaiKeHoachNam] = @p0
WHERE [TrangThaiKeHoachNam] = 2 AND Nam = @p1";
                }
                else
                {
                    return BadRequest("Bạn không có quyền thực hiện hành động: Duyệt.");
                }

                int changed = Db.Database.ExecuteSqlCommand(query, trangThai, year);
                var results = new
                {
                    TrangThaiNam = GetDanhMucNam(year)
                };

                return Ok(results);
            }

            return BadRequest();
        }

        // POST api/DuyetKeHoachNam/TuChoi
        [HttpPost]
        public IHttpActionResult TuChoi(int id)
        {
            if (ModelState.IsValid)
            {
                int year = id;
                KeHoachNamEnum trangThai;
                string query;
                if (UserContext.IsInGroup(GROUPS.KTNB1))
                {
                    trangThai = KeHoachNamEnum.DieuChinh;
                    query = @"
UPDATE [dbo].[EX_DM_NAM] SET [TrangThaiKeHoachNam] = @p0
WHERE [TrangThaiKeHoachNam] = 1 AND Nam = @p1";
                }
                else if (UserContext.IsInGroup(GROUPS.BKS))
                {
                    trangThai = KeHoachNamEnum.KhoiTao;
                    query = @"
UPDATE [dbo].[EX_DM_NAM] SET [TrangThaiKeHoachNam] = @p0
WHERE [TrangThaiKeHoachNam] = 2 AND Nam = @p1";
                }
                else
                {
                    return BadRequest("Bạn không có quyền thực hiện hành động: Từ Chối.");
                }

                int changed = Db.Database.ExecuteSqlCommand(query, trangThai, year);
                var results = new
                {
                    TrangThaiNam = GetDanhMucNam(year)
                };

                return Ok(results);
            }

            return BadRequest();
        }

        // POST api/DuyetKeHoachNam/XuatExcel
        [HttpPost]
        public IHttpActionResult XuatExcel()
        {
            if (ModelState.IsValid)
            {
                return Ok();
            }

            return BadRequest();
        }

        private PagedList<DoiTuongKiemToan> GetDsKeHoachNam(int? page, int? year, int? loaiDTKT)
        {
            int currentYear = year.GetYearOrDefault();
            string sqlQuery = Queries.GetDsDuyetKeHoachNamQuery;
            if (loaiDTKT.HasValue)
            {
                sqlQuery += " AND l.Id = @p1";
            }

            var pagedList = GetPagedList<DoiTuongKiemToan>(sqlQuery, page, currentYear, loaiDTKT);

            return pagedList;
        }
    }
}