using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using KTNB.Biz.Definition.SqlQueries;
using KTNB.Extended.Commons.PagedUtils;
using KTNB.Extended.Entities;

namespace KTNB.Biz.Controllers.DonViKiemToanNoiBo
{
    public class ThongTinDonViKiemToanNoiBoController : BaseApiController
    {

        //Get Danh Sach Phong Ban
        // GET api/ThongTinDonViKiemToanNoiBo/GetDanhSachPhongBans
        [HttpGet]
        [ResponseType(typeof(PagedList<PhongBan>))]
        public IHttpActionResult GetDanhSachPhongBans(int? page)
        {
            int currentPage = page.GetValueOrDefault(1);
            var pagedList = GetPagedList<PhongBan>(Queries.GetDanhSachPhongBans, currentPage);
            return Ok(pagedList);
        }


        //Tao moi mot Phong Ban
        // POST api/ThongTinDonViKiemToanNoiBo/InsertPhongBan
        [HttpPost]
        public IHttpActionResult InsertPhongBan(PhongBan model)
        {
            Guid sourceId = Guid.NewGuid();
            model.Nam = CurrentYear;
            int isSuccess = Db.Database.ExecuteSqlCommand(Queries.InsertPhongBan, model.MaDonVi, sourceId, model.Ten, model.MaTruongPhong, model.NguonLuc, model.TrangThai, model.Nam);
            if (isSuccess > 0)
            {
                return Ok("Cập nhật thành công!");
            }
            else
            {
                return BadRequest("Cập nhật không thành công!");
            }
        }



        //Get Chi tiet Phong Ban
        // GET api/ThongTinDonViKiemToanNoiBo/GetPhongBan
        [HttpGet]
        public IHttpActionResult GetPhongBan(int id)
        {
            PhongBan phongBan = Db.Database.SqlQuery<PhongBan>(Queries.GetPhongBanDetail, id).FirstOrDefault();

            return Ok(phongBan);
        }

        //Cap nhat mot Phong Ban
        //POST api/ThongTinDonViKiemToanNoiBo/UpdatePhongBan
        [HttpPost]
        public IHttpActionResult UpdatePhongBan(PhongBan model)
        {
            int isSuccess = Db.Database.ExecuteSqlCommand(Queries.UpdatePhongBan, model.MaDonVi, model.Ten, model.MaTruongPhong, model.NguonLuc, model.TrangThai, model.Id);
            if (isSuccess > 0)
            {
                return Ok("Cập nhật thành công!");
            }
            else
            {
                return BadRequest("Cập nhật không thành công");
            }
        }

        //Get Danh Sach User
        //GET api/ThongTinDonViKiemToanNoiBo/LoadCustomUser
        [HttpGet]
        public List<CustomUser> LoadCustomUser()
        {
            var lstCustomUser = Db.Database.SqlQuery<CustomUser>(Queries.GetUsersQueryId).ToList();

            return lstCustomUser;
        }
    }
}