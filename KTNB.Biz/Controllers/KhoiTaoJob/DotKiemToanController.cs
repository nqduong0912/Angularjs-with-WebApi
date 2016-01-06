using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using KTNB.Extended.Commons.Helpers;
using KTNB.Extended.Commons.PagedUtils;
using KTNB.Extended.Entities.KhoiTaoJob;

namespace KTNB.Biz.Controllers.KhoiTaoJob
{
    public class DotKiemToanController : BaseApiController
    {
        // GET api/DotKiemToanController/Get
        [HttpGet]
        [ResponseType(typeof(PagedList<DotKiemToan>))]
        public IHttpActionResult Get(int? page, int? trangThai)
        {
            var result = new
            {
                //DsLoaiDTKT = CoreFactory<DotKiemToan>.EntityManager.GetList(x => x.Status == 4),
                DotKiemToanPaged = GetDsDotKiemToan(page, trangThai)
            };

            return Ok(result);
        }

        // GET api/DotKiemToanController/Filter
        [HttpGet]
        [ResponseType(typeof(PagedList<DotKiemToan>))]
        public IHttpActionResult Filter(int? page, int? trangThai)
        {
            var pagedList = GetDsDotKiemToan(page, trangThai);

            return Ok(pagedList);
        }

        private PagedList<DotKiemToan> GetDsDotKiemToan(int? page, int? trangThai)
        {
            string sqlQuery = @"
                SELECT n.[Id], n.Nam, n.TenDotKT AS [DotKT], n.LDTKT AS [LoaiDTKT], l.Ten AS [LoaiDTKTText], 
                n.[DTKT], n.TenDTKT AS [DTKTText], n.[NextKT] AS [ThangDuKienKT], n.[TrongKeHoach], n.[DonViLienQuan], 
                n.[TrangThai], n.[LapKHStart], n.[LapKHEnd],right(CONVERT(nvarchar,n.[LapKHStart],103),7)+'-'+right(CONVERT(nvarchar,n.[LapKHEnd],103),7) AS [LapKH], 
                n.[ThucDiaStart], n.[ThucDiaEnd], right(CONVERT(nvarchar,n.[ThucDiaStart],103),7)+'-'+right(CONVERT(nvarchar,n.[ThucDiaEnd],103),7) AS [ThucDia],
                n.[BaoCaoStart],n.[BaoCaoEnd],right(CONVERT(nvarchar,n.[BaoCaoStart],103),7)+'-'+right(CONVERT(nvarchar,n.[BaoCaoEnd],103),7) AS [BaoCao], n.[FileThongTinChung], n.[NgayQD]
                FROM [dbo].[EX_QT_NAM_DTKT] AS n
                LEFT JOIN [dbo].[EX_DM_LOAIDTKT] AS l ON n.LDTKT = l.SourceId AND n.Nam = l.Nam";
            if (trangThai.HasValue)
            {
                sqlQuery += " Where n.TrangThai = @p0";
            }

            var pagedList = GetPagedList<DotKiemToan>(sqlQuery, page, trangThai);

            for (int i = 0; i < pagedList.Items.Count; i++)
            {
                DotKiemToan dotKT = pagedList.Items[i];
                
                if (!string.IsNullOrEmpty(dotKT.DonViLienQuan))
                {
                    string dsdonvi = "";
                    List<DonViLienQuan> lst = JsonHelper.ToList<DonViLienQuan>(dotKT.DonViLienQuan);
                    for (int j = 0; j < lst.Count; j++)
			        {
                        dsdonvi += lst[j].TenDTKT;
                        if (j < lst.Count - 1) dsdonvi += ", "; 
			        }
                    dotKT.DonViLienQuanText = dsdonvi;
                }
            }
            return pagedList;
        }
    }
}