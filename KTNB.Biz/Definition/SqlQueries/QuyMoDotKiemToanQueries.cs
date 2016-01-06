namespace KTNB.Biz.Definition.SqlQueries
{
    public static partial class Queries
    {
        //------------ Thao tac voi trang Quy Mo Doi Tuong Kiem Toan ----------------//
        /// <summary>
        ///  Danh sach bo quy mo
        /// </summary>
        public static string GetBoQuyMo = @"
select t1.Nam, t2.Ten as 'LoaiDTKT', t1.Ten, t1.TrangThai, t1.ID, t1.SourceId 
from EX_DM_BOQUYMO as t1 inner join EX_DM_LOAIDTKT t2 on t1.LoaiDTKT = t2.SourceId";
        /// <summary>
        ///  Quy mô trong 1 bộ quy mo
        /// </summary>
        public static string GetQuyMoByBoQuyMoId = @"
SELECT Id, SourceId, Ten, Nam, LoaiDTKT
FROM VPB_KTNB_DEV.dbo.EX_DM_QUYMO WHERE BoQuyMoId = @p0";

        /// <summary>
        ///  Tìm kiếm bộ quy mô theo năm và loại đối tượng kiểm toán
        /// </summary>
        public static string GetBoQuyMoByNamLoaiDoiTuongKiemToan = @"
select t1.Nam, t2.Ten as 'LoaiDTKT', t1.Ten, t1.TrangThai, t1.ID, t1.SourceId 
from EX_DM_BOQUYMO as t1 inner join EX_DM_LOAIDTKT t2 on t1.LoaiDTKT = t2.SourceId
where 1=1 and
t1.Nam = @p0 and t2.SourceId = (select SourceId from EX_DM_LOAIDTKT where Ten = @p1)";

        public static string SearchBoQuyMo = @"
select t1.Nam, t2.Ten as 'LoaiDTKT', t1.Ten, t1.TrangThai, t1.ID, t1.SourceId 
from EX_DM_BOQUYMO as t1 inner join EX_DM_LOAIDTKT t2 on t1.LoaiDTKT = t2.SourceId
where 1=1 ";

        public static string CopyQuyMo = @"
insert into EX_DM_QUYMO (BoQuyMoId, Ten, NguonLuc)
select @p1, Ten, NguonLuc from EX_DM_QUYMO where BoQuyMoId = @p0";
    }
}
