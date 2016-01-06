using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using KTNB.Biz.Definition.SqlQueries;
using KTNB.Extended.Biz;
using KTNB.Extended.Dal;

namespace KTNB.Biz.Controllers.CacDTKT
{
    public class QuyMoController : BaseApiController
    {
        [HttpPost]
        public IHttpActionResult InsertQuyMo(dm_quymo model)
        {
            int isSuccess = ManagerFactory.quymo_manager.Insert(model);
            if (isSuccess > 0)
            {
                return Ok("Thêm mới thành công");
            }
            else
            {
                return BadRequest("Thêm mới không thành công");
            }
        }

        [HttpPost]
        public IHttpActionResult InsertListQuyMo(List<dm_quymo> lstModel)
        {
            if (lstModel.Count > 0)
            {
                foreach (var item in lstModel)
                {
                    int isSuccess = ManagerFactory.quymo_manager.Insert(item);
                    if (isSuccess < 1)
                    {
                        return BadRequest("Thêm mới quy mô không thành công");
                        break;
                    }
                }
                return Ok("Thêm mới thành công");
            }
            else
            {
                return BadRequest("Chưa có quy mô cho bộ quy mô");
            }
        }

        [HttpPost]
        public IHttpActionResult DeletetQuyMo(dm_quymo model)
        {
            int isSuccess = ManagerFactory.quymo_manager.Delete(model.Id);
            if (isSuccess > 0)
            {
                return Ok("Xóa thành công");
            }
            else
            {
                return BadRequest("Không thể xóa quy mô");
            }
        }

        [HttpGet]
        public IEnumerable<dm_quymo> GetQuyMobyBoQuyMoId(int boQuyMoId)
        {
            List<dm_quymo> lstQuyMo = Db.Database.SqlQuery<dm_quymo>(Queries.GetQuyMoByBoQuyMoId, boQuyMoId).ToList();
            return lstQuyMo;
        }
    }
}