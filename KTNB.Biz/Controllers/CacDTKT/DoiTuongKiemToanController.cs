using System;
using System.Web.Http;
using System.Web.Http.Description;
using KTNB.Biz.Definition.SqlQueries;
using KTNB.Extended.Biz;
using KTNB.Extended.Commons.PagedUtils;
using KTNB.Extended.Dal.Lib;

namespace KTNB.Biz.Controllers.CacDTKT
{
    public class DoiTuongKiemToanController : BaseApiController
    {
        // GET api/DoiTuongKiemToanController/Get
        [HttpGet]
        [ResponseType(typeof(PagedList<dm_li_doituongkiemtoan>))]
        public IHttpActionResult Get(int? page, Guid? loaiDTKT, string tenDTKT)
        {
            var result = new
            {
                DsLoaiDTKT = CoreFactory<dm_li_loaidoituongkiemtoan>.EntityManager.GetList(x => x.Status == 4),
                DoiTuongKiemToanPaged = GetDsDoiTuongKiemToan(page, loaiDTKT, tenDTKT)
            };

            return Ok(result);
        }

        // GET api/DoiTuongKiemToanController/Filter
        [HttpGet]
        [ResponseType(typeof(PagedList<dm_li_doituongkiemtoan>))]
        public IHttpActionResult Filter(int? page, Guid? loaiDTKT, string tenDTKT)
        {
            var pagedList = GetDsDoiTuongKiemToan(page, loaiDTKT, tenDTKT);

            return Ok(pagedList);
        }

        private PagedList<dm_li_doituongkiemtoan> GetDsDoiTuongKiemToan(int? page, Guid? loaiDTKT, string tenDTKT)
        {
            string sqlQuery = Queries.GetDsDoiTuongKiemToan;
            if (loaiDTKT.HasValue)
            {
                sqlQuery += " AND [IDLDTKT] = @p0";
            }

            if (!string.IsNullOrEmpty(tenDTKT))
            {
                sqlQuery += " AND [Ten] LIKE @p1";
            }

            var pagedList = GetPagedList<dm_li_doituongkiemtoan>(sqlQuery, page, loaiDTKT.ToString(), string.Format("%{0}%", tenDTKT));

            return pagedList;
        }


    }
}