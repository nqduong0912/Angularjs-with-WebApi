using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.SessionState;
using CORE.CoreObjectContext;
using KTNB.Biz.Definition.SqlQueries;
using KTNB.Extended.Commons;
using KTNB.Extended.Commons.PagedUtils;
using KTNB.Extended.Core;
using KTNB.Extended.Entities;
using KTNB.Extended.Extensions;

namespace KTNB.Biz.Controllers
{
    public class BaseApiController : ApiController
    {
        private const int PageSize = 10;

        private DbContext _db;
        private UserContext _userContext;
        private DanhMucNam _currentDanhMucNam;

        public BaseApiController()
        {

        }

        protected HttpSessionState Session
        {
            get
            {
                return HttpContext.Current.Session;
            }
        }

        protected UserContext UserContext
        {
            get
            {
                if (_userContext == null)
                {
                    _userContext = (UserContext)Session["objUserContext"];
                }

                return _userContext;
            }
        }

        protected DbContext Db
        {
            get
            {
                if (_db == null)
                {
                    string connectionString = AppConfig.ConnectionString;

                    _db = new DbContext(connectionString);
                }

                return _db;
            }
        }

        protected UserContext CustomUser
        {
            get
            {
                return (Session["objUserContext"] as UserContext);
            }
        }

        protected int CurrentYear
        {
            get
            {
                return MiscUtils.GetCurrentYear();
            }
        }

        protected DanhMucNam CurrentDanhMucNam
        {
            get
            {
                if (_currentDanhMucNam == null)
                {
                    _currentDanhMucNam = GetDanhMucNam(CurrentYear);
                }

                return _currentDanhMucNam;
            }
        }

        public DbRawSqlQuery<PhongBan> GetDanhSachPhongBan()
        {
            return Db.Database.SqlQuery<PhongBan>(Queries.GetPhongBanQuery);
        }

        public DbRawSqlQuery<CustomUser> GetDanhSachUsers()
        {
            return Db.Database.SqlQuery<CustomUser>(Queries.GetUsersQuery);
        }

        public DbRawSqlQuery<TanSuat> GetDanhSachTanSuat()
        {
            return Db.Database.SqlQuery<TanSuat>(Queries.GetTanSuatQuery);
        }

        public DanhMucNam GetDanhMucNam(int? nam)
        {
            int currentYear = nam.GetYearOrDefault();

            return Db.Database.SqlQuery<DanhMucNam>(Queries.GetDanhMucNamByNamQuery, currentYear).FirstOrDefault();
        }

        public DbRawSqlQuery<TanSuat> GetDanhSachTanSuatByNam(int? nam)
        {
            int currentYear = nam.GetYearOrDefault();

            return Db.Database.SqlQuery<TanSuat>(Queries.GetTanSuatByNamQuery, currentYear);
        }

        public DbRawSqlQuery<LoaiDoiTuongKiemToan> GetDanhSachLoaiDTKT()
        {
            return Db.Database.SqlQuery<LoaiDoiTuongKiemToan>(Queries.GetLoaiDTKTQuery);
        }

        public DbRawSqlQuery<LoaiDoiTuongKiemToan> GetDanhSachLoaiDTKTByNam(int? nam)
        {
            int currentYear = nam.GetYearOrDefault();

            return Db.Database.SqlQuery<LoaiDoiTuongKiemToan>(Queries.GetLoaiDTKTByNamQuery, currentYear);
        }

        /// <summary>
        /// Lấy ra bộ quy mô đang active theo năm.
        /// </summary>
        /// <param name="nam">Năm</param>
        public BoQuyMo GetBoQyMoActiveByNam(int? nam)
        {
            int currentYear = nam.GetYearOrDefault();
            BoQuyMo result = Db.Database.SqlQuery<BoQuyMo>(Queries.GetBoQyMoActiveByNamQuery, currentYear).FirstOrDefault();

            return result;
        }

        /// <summary>
        /// Lấy ra danh sách quy mô theo năm và bộ quy mô đang active.
        /// </summary>
        /// <param name="nam">Năm</param>
        public List<QuyMo> GetDsQuyMoActiveByNam(int? nam)
        {
            int currentYear = nam.GetYearOrDefault();
            var result = Db.Database.SqlQuery<QuyMo>(Queries.GetDsQuyMoByNamQuery, currentYear).ToList();

            return result;
        }

        /// <summary>
        /// Lấy ra nguồn lực năm
        /// </summary>
        /// <param name="nam">Năm</param>
        /// <param name="status">false: Inactive, true: active (default)</param>
        /// <returns>Nguồn lực của một năm</returns>
        public int? GetNguonLucNamByNam(int? nam, bool status = true)
        {
            int currentYear = nam.GetYearOrDefault();
            int? result = Db.Database.SqlQuery<int?>(Queries.GetNguonLucNamByNamQuery, currentYear, status).First();

            return result;
        }

        /// <summary>
        /// Lấy ra nguồn lực cần thiết theo năm
        /// </summary>
        /// <param name="nam">Năm</param>
        /// <returns>Nguồn lực cần thiết của một năm</returns>
        public int? GetNguonCanThietByNam(int? nam)
        {
            int currentYear = nam.GetYearOrDefault();
            int? result = Db.Database.SqlQuery<int?>(Queries.GetNguonLucCanThietByNamQuery, currentYear).First();

            return result;
        }

        protected PagedList<T> GetPagedList<T>(string query, int? page, params object[] parameters)
        {
            int currentPage = page.GetValueOrDefault(1);
            DbRawSqlQuery<T> sqlQuery = Db.Database.SqlQuery<T>(query, parameters);
            int totalItems = sqlQuery.Count();
            int startIndex = (currentPage - 1) * PageSize;
            List<T> collection = sqlQuery.Skip(startIndex).Take(PageSize).ToList();
            PagedList<T> pagedList = new PagedList<T>(collection, totalItems, currentPage, PageSize);

            return pagedList;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_db != null)
                {
                    _db.Dispose();
                }
            }

            base.Dispose(disposing);
        }
    }
}