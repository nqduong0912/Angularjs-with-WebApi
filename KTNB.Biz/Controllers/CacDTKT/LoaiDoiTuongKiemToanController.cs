using System.Web.Http;
using System.Web.Http.Description;
using KTNB.Biz.Definition.SqlQueries;
using KTNB.Extended.Commons.PagedUtils;
using KTNB.Extended.Dal.Lib;

namespace KTNB.Biz.Controllers.CacDTKT
{
    public class LoaiDoiTuongKiemToanController : BaseApiController
    {
        // GET api/LoaiDoiTuongKiemToan/Get
        [HttpGet]
        [ResponseType(typeof(PagedList<dm_li_loaidoituongkiemtoan>))]
        public IHttpActionResult Get(int? page)
        {
            int currentPage = page.GetValueOrDefault(1);

            var pagedList = GetPagedList<dm_li_loaidoituongkiemtoan>(Queries.GetDsLoaiDoiTuongKiemToan, currentPage);

            return Ok(pagedList);
        }
    }
}