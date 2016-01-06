using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using KTNB.Biz.Definition.SqlQueries;
using KTNB.Extended.Biz;
using KTNB.Extended.Commons;
using KTNB.Extended.Commons.PagedUtils;
using KTNB.Extended.Dal;
using KTNB.Extended.Enums;

namespace KTNB.Biz.Controllers.CacDTKT
{
    public class QuyMoDTKTController : BaseApiController
    {
        private const string UpdateStatusBoQuyMoQuery = @"
UPDATE [dbo].[EX_DM_BOQUYMO]
SET [TrangThai] = CASE WHEN Id = @p0 THEN 1 ELSE 0 END
where Nam = @p1 and LoaiDTKT = (select SourceId from EX_DM_LOAIDTKT where Ten = @p2)
";

        // get Year
        [HttpGet]
        public IHttpActionResult DanhSachNam()
        {
            return Ok(MiscUtils.GetAllYears());
        }

        //get Loai doi tuong kiem toan
        [HttpGet]
        public IEnumerable<string> DanhSachLoaiDoiTuongKiemToan()
        {
            List<dm_loaidoituongkiemtoan> lstLoaiDoiTuongKiemToan = ManagerFactory.loaidoituongkiemtoan_manager.GetList();
            List<string> lstTenDoiTuongKiemToan = new List<string>();
            if (lstLoaiDoiTuongKiemToan.Count > 0)
                foreach (dm_loaidoituongkiemtoan item in lstLoaiDoiTuongKiemToan)
                    lstTenDoiTuongKiemToan.Add(item.Ten);
            return lstTenDoiTuongKiemToan;
        }

        //get Danh sach bo quy mo (phan trang)

        [HttpGet]
        [ResponseType(typeof(PagedList<dm_boquymo>))]
        public IHttpActionResult GetDanhSachBoQuyMo(int? page)
        {
            int currentPage = page.GetValueOrDefault(1);

            var pagedList = GetPagedList<dm_boquymo>(Queries.GetBoQuyMo, currentPage);

            return Ok(pagedList);
        }


        //get Danh sach bo quy mo theo nam, loai doi tuong kiem toan

        [HttpGet]
        [ResponseType(typeof(PagedList<dm_boquymo>))]
        public IHttpActionResult DanhSachBoQuyMobyNamLoaiDoiTuongKiemToan(int nam, string tenLoaiDoiTuongKiemToan)
        {
            int currentPage = 1;
            var pagedList = GetPagedList<dm_boquymo>(Queries.GetBoQuyMoByNamLoaiDoiTuongKiemToan, currentPage, nam, tenLoaiDoiTuongKiemToan);
            return Ok(pagedList);
        }

        //Tìm kiếm bộ quy mô theo loai doi tuong kiem toan
        [HttpGet]
        public IEnumerable<dm_boquymo> SearchBoQuyMobyLoaiDoiTuongKiemToan(string loaiDoiTuongKiemToan)
        {
            List<dm_boquymo> lstBoQuyMo;
            if (string.IsNullOrEmpty(loaiDoiTuongKiemToan))
            {
                lstBoQuyMo = Db.Database.SqlQuery<dm_boquymo>(Queries.SearchBoQuyMo).ToList();
            }
            else
            {
                string condition = "AND t2.SourceId = (select SourceId from EX_DM_LOAIDTKT where Ten = '" + loaiDoiTuongKiemToan + "')";
                lstBoQuyMo = Db.Database.SqlQuery<dm_boquymo>(Queries.SearchBoQuyMo + condition).ToList();
            }
            return lstBoQuyMo;
        }


        //Tìm kiếm bộ quy mô theo Nam
        [HttpGet]
        public IEnumerable<dm_boquymo> SearchBoQuyMobyNam(int? nam)
        {
            List<dm_boquymo> lstBoQuyMo;
            if (nam == null || nam < 2006)
            {
                lstBoQuyMo = Db.Database.SqlQuery<dm_boquymo>(Queries.SearchBoQuyMo).ToList();
            }
            else
            {
                string condition = "AND  t1.Nam = '" + nam.ToString() + "' ";
                lstBoQuyMo = Db.Database.SqlQuery<dm_boquymo>(Queries.SearchBoQuyMo + condition).ToList();
            }

            return lstBoQuyMo;
        }

        // POST api/QuyMoDTKT/
        //Them moi bo quy mo
        [HttpPost]
        public IHttpActionResult InsertNewBoQuyMo(dm_boquymo model)
        {
            //ManagerFactory.boquymo_manager.InsertNewwBoQuyMo(model);
            int reVal = ManagerFactory.boquymo_manager.InsertBoQuyMo(model);
            int isSuccess = 0;
            if (reVal > 0)
            {
                if (model.LstQuyMo.Count > 0)
                {
                    foreach (var item in model.LstQuyMo)
                    {
                        isSuccess = ManagerFactory.quymo_manager.Insert(new dm_quymo()
                        {
                            BoQuyMoId = reVal,
                            NguonLuc = item.NguonLuc,
                            Ten = item.Ten
                        });
                        if (isSuccess < 1)
                        {
                            return BadRequest("Thêm mới quy mô cho bộ quy mô không thành công");
                        }
                    }
                }
                if (isSuccess > 0)
                {
                    return Ok("Thêm mới thành công!");
                }

                return BadRequest();
            }

            return BadRequest("Bộ quy mô chưa có quy mô!");
        }


        //thong tin bo quy mo
        [HttpGet]
        public dm_boquymo GetBoQuyMobyID(int id)
        {
            dm_boquymo boQuyMo = ManagerFactory.boquymo_manager.GetBoquymobyId(id);
            boQuyMo.LstQuyMo = Db.Database.SqlQuery<dm_quymo>(Queries.GetQuyMoByBoQuyMoId, id).ToList();
            return boQuyMo;
        }


        //Cap nhat Bo Quy Mo
        [HttpPost]
        public IHttpActionResult UpdateBoQuyMo(dm_boquymo model)
        {
            if (model.Nam < DateTime.Now.Year)
            {
                return BadRequest("Không cho phép cập nhật!");
            }

            if (CurrentDanhMucNam.TrangThaiKeHoachNam == KeHoachNamEnum.ChuaDuyet || CurrentDanhMucNam.TrangThaiKeHoachNam == KeHoachNamEnum.KhoiTao)
            {
                int isSuccess = ManagerFactory.boquymo_manager.UpdateBoQuyMo(model);
                if (isSuccess > 0)
                {
                    return BadRequest("Cập nhật không thành công");
                }

                return Ok("Cập nhật thành công!");
            }

            return BadRequest("Bộ quy mô đang trong quá trình chờ duyệt hoặc kiểm tra, không cho phép cập nhật");
        }

        //update bo quy mo

        [HttpPost]
        public IHttpActionResult UpdateStatusBoQuyMo(dm_boquymo model)
        {
            int changed = Db.Database.ExecuteSqlCommand(UpdateStatusBoQuyMoQuery, model.Id, model.Nam, model.LoaiDTKT);
            if (changed > 0)
            {
                return Ok("Cập nhật thành công.");
            }

            return BadRequest("Không cập nhật được.");
        }

        //Them quy mo cho bo quy mo
        [HttpPost]
        public dm_quymo AddQuyMo(dm_quymo model)
        {
            int isSuccess = ManagerFactory.quymo_manager.Insert(model);
            if (isSuccess > 0)
            {
                return model;
            }

            return null;
        }


        //xoa quy mo
        [HttpPost]
        public IHttpActionResult DeleteQuyMo(dm_quymo model)
        {
            int isSuccess = ManagerFactory.quymo_manager.Delete(model.Id);
            if (isSuccess > 0)
            {
                return Ok("Xóa thành công");
            }

            return BadRequest("Không thể xóa quy mô");
        }
    }
}