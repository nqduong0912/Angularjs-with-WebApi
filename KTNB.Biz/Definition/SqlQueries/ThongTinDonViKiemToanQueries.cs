namespace KTNB.Biz.Definition.SqlQueries
{
    public static partial class Queries
    {
        /// <summary>
        /// Danh sach Phong ban 
        /// </summary>
        public static string GetDanhSachPhongBans = @"
SELECT t1.Id, t1.SourceId, t1.MaDonVi, t1.Ten, t1.MaTruongPhong, t1.NguonLuc, t1.TrangThai, t1.Nam , t2.Name as 'TruongPhong'
FROM EX_DM_PHONGBAN AS t1 INNER JOIN T_USER AS t2 on t1.MaTruongPhong = t2.PK_UserID ";

        /// <summary>
        /// Chi tiet Phong Ban
        /// </summary>
        public static string GetPhongBanDetail = @"
SELECT TOP 1 t1.Id, t1.SourceId, t1.MaDonVi, t1.Ten, t1.MaTruongPhong, t1.NguonLuc, t1.TrangThai, t1.Nam , t2.Name as 'TruongPhong'
FROM EX_DM_PHONGBAN AS t1 INNER JOIN T_USER AS t2 on t1.MaTruongPhong = t2.PK_UserID 
WHERE Id = @p0";

        /// <summary>
        /// Them moi Phong Ban
        /// </summary>
        public static string InsertPhongBan = @"
INSERT INTO EX_DM_PHONGBAN(MaDonVi, SourceId, Ten, MaTruongPhong, NguonLuc, TrangThai, Nam)
VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6)";

        /// <summary>
        /// Cap nhat Phong Ban
        /// </summary>
        public static string UpdatePhongBan = @"
UPDATE EX_DM_PHONGBAN SET MaDonVi = @p0, Ten = @p1, MaTruongPhong = @p2, NguonLuc = @p3, TrangThai = @p4
WHERE Id = @p5";


        /// <summary>
        /// Chi tiet mang nghiep vu theo loai doi tuong kiem toan
        /// </summary>
        public static string GetMangNghiepVu = @"
SELECT        PK_DocumentID, Status, [Ten], [DienGiai]
FROM            T_DOCUMENT DOC OUTER APPLY
                    (SELECT        [Ten] = TEXTVALUE
                      FROM            T_TYPE_DOC_PROPERTY
                      WHERE        FK_DOCUMENTID = DOC.PK_DOCUMENTID AND FK_PROPERTYID = '144DFC1C-5D45-4FE7-8772-CD573CEFD04F') [Ten] OUTER APPLY
                    (SELECT        [DienGiai] = TEXTVALUE
                      FROM            T_TYPE_DOC_PROPERTY
                      WHERE        FK_DOCUMENTID = DOC.PK_DOCUMENTID AND FK_PROPERTYID = 'E577E063-C972-4D5D-8B55-38B8737C7D03') [DienGiai] OUTER APPLY
                    (SELECT        LoaiDoiTuongKiemToanId = TEXTVALUE
                      FROM            T_TYPE_DOC_PROPERTY
                      WHERE        FK_DOCUMENTID = DOC.PK_DOCUMENTID AND FK_PROPERTYID = 'B65E8D96-F253-40AB-A645-D210E09DA504') LoaiDoiTuongKiemToanId
WHERE        DOC.FK_DOCUMENTTYPEID = '34187F34-392F-492A-A8F6-6954DDF6DEA9'  and LoaiDoiTuongKiemToanId = @p0";


        /// <summary>
        /// Chi tiet RiskProfile
        /// </summary>
        public static string GetRiskProfile = @"
select TEXTVALUE as MangNghiepVu, QuyTrinh, BuocHoatDong, MucTieu, RuiRo, KiemSoat, ThuTuc           
from T_TYPE_DOC_PROPERTY _TYPE 																		
OUTER APPLY																							
(SELECT        QuyTrinh = t2.TEXTVALUE, [_QT] = t2.FK_DOCUMENTID									
    FROM            T_TYPE_DOC_PROPERTY t1															
 INNER JOIN T_TYPE_DOC_PROPERTY t2																	
 ON t1.DOCUMENT_LOOKUP_VALUE = _TYPE.FK_DOCUMENTID AND t1.FK_DOCUMENTID = t2.FK_DOCUMENTID
    WHERE t2.FK_PROPERTYID = 'DA131AD9-234F-43CE-BBED-ED408543A23E')QuyTrinh
OUTER APPLY
(SELECT        BuocHoatDong = t2.TEXTVALUE, [_BHD] = t2.FK_DOCUMENTID
    FROM            T_TYPE_DOC_PROPERTY t1
 INNER JOIN T_TYPE_DOC_PROPERTY t2
 ON t1.DOCUMENT_LOOKUP_VALUE = [_QT] AND t1.FK_DOCUMENTID = t2.FK_DOCUMENTID
    WHERE t2.FK_PROPERTYID = '6C3F724B-896F-4656-91C8-781F5B5C13F1')BuocHoatDong
OUTER APPLY
(SELECT        MucTieu = t2.TEXTVALUE, [_MTKS] = t2.FK_DOCUMENTID
    FROM            T_TYPE_DOC_PROPERTY t1
 INNER JOIN T_TYPE_DOC_PROPERTY t2
 ON t1.DOCUMENT_LOOKUP_VALUE = [_BHD] AND t1.FK_DOCUMENTID = t2.FK_DOCUMENTID
    WHERE t2.FK_PROPERTYID = '9D641C2E-99D2-4D3F-9034-116EFAF2C3BC')MucTieu

OUTER APPLY
  (SELECT        RuiRo = t2.TEXTVALUE, [_RR] = t2.FK_DOCUMENTID
    FROM            T_TYPE_DOC_PROPERTY t1
 INNER JOIN T_TYPE_DOC_PROPERTY t2
 ON t1.DOCUMENT_LOOKUP_VALUE = [_MTKS] AND t1.FK_DOCUMENTID = t2.FK_DOCUMENTID
    WHERE t2.FK_PROPERTYID = '33686451-E1A7-4F11-9D8D-A0D697D0D5B6') RuiRo 


OUTER APPLY
  (SELECT        KiemSoat = t2.TEXTVALUE, [_KS] = t2.FK_DOCUMENTID
    FROM            T_TYPE_DOC_PROPERTY t1
 INNER JOIN T_TYPE_DOC_PROPERTY t2
 ON t1.DOCUMENT_LOOKUP_VALUE = [_RR] AND t1.FK_DOCUMENTID = t2.FK_DOCUMENTID
    WHERE t2.FK_PROPERTYID = '4138B099-7CBD-443E-A12B-9A1FF5D1E08F') KiemSoat 

	OUTER APPLY
  (SELECT        ThuTuc = t2.TEXTVALUE
    FROM            T_TYPE_DOC_PROPERTY t1
 INNER JOIN T_TYPE_DOC_PROPERTY t2
 ON t1.DOCUMENT_LOOKUP_VALUE = [_KS] AND t1.FK_DOCUMENTID = t2.FK_DOCUMENTID
    WHERE t2.FK_PROPERTYID = '98F450F3-824E-4BBD-9F9D-6C9845FD8186') ThuTuc 
where _TYPE.FK_DOCUMENTID = @p0 
     and _TYPE.FK_PROPERTYID='144DFC1C-5D45-4FE7-8772-CD573CEFD04F'";

        /// <summary>
        /// Sua ten mang nghiep vu
        /// </summary>
        public static string UpdateMangNghiepVu = @"
UPDATE T_TYPE_DOC_PROPERTY SET TEXTVALUE = @p0 
WHERE FK_PROPERTYID = '144DFC1C-5D45-4FE7-8772-CD573CEFD04F' AND FK_DOCUMENTID = @p1";
    }
}
