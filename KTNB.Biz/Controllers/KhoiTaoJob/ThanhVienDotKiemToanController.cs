using System.Web.Http;
using KTNB.Biz.Definition.SqlQueries;
using KTNB.Extended.Entities.KhoiTaoJob;
using Newtonsoft.Json;

namespace KTNB.Biz.Controllers.KhoiTaoJob
{
    public class ThanhVienDotKiemToanController : BaseApiController
    {
        // Cap nhat thanh vien doan kiem toan
        // POST: api/ThanhVienDotKiemToan/InsertDanhSachDoanKiemToan
        [HttpPost]
        public IHttpActionResult InsertDanhSachDoanKiemToan(ChonThanhVienDotKiemToanRequest chonThanhVienDotKiemToanRequest)
        {
            string condition = "";
            int? dotKiemToanId = chonThanhVienDotKiemToanRequest.DsThanhVienDotKiemToan[0].IdDotKiemToan;
            Db.Database.ExecuteSqlCommand(Queries.DeleteThanhVienDotKiemToanbyId, dotKiemToanId);
            foreach (var item in chonThanhVienDotKiemToanRequest.DsThanhVienDotKiemToan)
            {
                condition += " (" + item.IdDotKiemToan + ", '" + item.PK_UserID + "'),";
            }

            condition = condition.Substring(0, condition.Length - 1);
            int isInsert = Db.Database.ExecuteSqlCommand(Queries.InsertThanhVienDotKiemToan + condition);

            string uploadFilebyThanhVien = JsonConvert.SerializeObject(chonThanhVienDotKiemToanRequest.DsUpLoadFileDotKiemToan);

            isInsert = Db.Database.ExecuteSqlCommand("UPDATE EX_QT_NAM_DTKT SET ThanhVienDotKiemToanUploadFile = @p0 WHERE Id = @p1", uploadFilebyThanhVien, dotKiemToanId);
            if (isInsert > 0)
            {
                return Ok("Thêm mới thành viên thành công!");
            }

            return BadRequest("Thêm mới thành viên không thành công!");
        }
    }
}