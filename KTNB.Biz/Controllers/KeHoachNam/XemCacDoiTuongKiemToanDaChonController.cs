using System.Web.Http;
using System.Web.Http.Description;
using KTNB.Biz.Definition.SqlQueries;
using KTNB.Extended.Commons;
using KTNB.Extended.Commons.PagedUtils;
using KTNB.Extended.Entities.KeHoachNam;

namespace KTNB.Biz.Controllers.KeHoachNam
{
    public class XemCacDoiTuongKiemToanDaChonController : BaseApiController
    {
        // GET api/XemCacDoiTuongKiemToanDaChon/Get
        [HttpGet]
        [ResponseType(typeof(PagedList<DoiTuongKiemToan>))]
        public IHttpActionResult Get(int? page)
        {
            int currentPage = page.GetValueOrDefault(1);
            int currentYear = MiscUtils.GetCurrentYear();

            var pagedList = GetPagedList<DoiTuongKiemToan>(Queries.GetDsDoiTuongKiemToanDuocChonChoKeHoachNamQuery, currentPage, currentYear);

            return Ok(pagedList);
        }
    }
}