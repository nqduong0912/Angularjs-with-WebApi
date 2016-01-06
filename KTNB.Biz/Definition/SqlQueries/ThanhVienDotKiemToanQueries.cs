namespace KTNB.Biz.Definition.SqlQueries
{
    public static partial class Queries
    {
        /// <summary>
        /// Danh Sach Nhan vien
        /// </summary>
        public static string GetDsThanhVienDotKiemToan = @"
select tv.IdDotKiemToan, u.PK_UserID, u.Name, u.Fullname, t3.DESCRIPTION as 'PhongBan', IsThanhVienDotKiemToan = CASE WHEN tv.UserId IS NOT NULL THEN CAST (1 AS BIT) ELSE CAST (0 AS BIT) END from T_USER as u 
LEFT JOIN T_USER_IN_GROUP AS t2 ON u.PK_UserID = t2.FK_USERID
LEFT JOIN T_GROUP AS t3 ON t2.FK_GROUPID = t3.PK_GROUPID
LEFT JOIN [dbo].[EX_QT_ThanhVienDotKiemToan] as tv ON u.PK_UserID = tv.UserId AND tv.IdDotKiemToan = @p0";


        /// <summary>
        /// Them moi Thanh Vien Dot Kiem Toan
        /// </summary>
        public static string InsertThanhVienDotKiemToan = @"
INSERT INTO EX_QT_ThanhVienDotKiemToan (IdDotKiemToan, UserId) VALUES";


        /// <summary>
        /// Xoa Thanh Vien Dot Kiem Toan 
        /// </summary>
        public static string DeleteThanhVienDotKiemToanbyId = @"
DELETE EX_QT_ThanhVienDotKiemToan WHERE IdDotKiemToan = @p0";




        /// <summary>
        /// Them moi hoac Xoa mot file dinh kem
        /// </summary>
        public static string UpdateThanhVienDotKiemToanUploadFile = @"
UPDATE EX_QT_NAM_DTKT SET ThanhVienDotKiemToanUploadFile = @p0
WHERE Id = @p1";

        public static string GetManager = "";

        /// <summary>
        /// Danh Sach Thanh vien dot Kiem Toan
        /// </summary>
        public static string GetDanhSachThanhVienDotKiemToan = @"
SELECT t1.IdDotKiemToan, t1.UserId, t2.Name, t2.Fullname, t4.DESCRIPTION AS 'PhongBan', t2.Email
FROM EX_QT_ThanhVienDotKiemToan AS t1 INNER JOIN T_USER AS t2
ON t1.UserId = t2.PK_UserID INNER JOIN T_USER_IN_GROUP AS t3 ON t2.PK_UserID = t3.FK_USERID
INNER JOIN T_GROUP AS t4 ON t3.FK_GROUPID = t4.PK_GROUPID WHERE t1.IdDotKiemToan = @p0";
    }
}
