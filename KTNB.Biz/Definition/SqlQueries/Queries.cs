namespace KTNB.Biz.Definition.SqlQueries
{
    public static partial class Queries
    {
        public const string GetPhongBanQuery = "SELECT p.Id, p.SourceId, p.MaDonVi, p.Ten, p.MaTruongPhong, p.NguonLuc, p.TrangThai, p.Nam FROM [dbo].[EX_DM_PHONGBAN] as p";

        public const string GetUsersQuery = @"
SELECT u.PK_UserID, u.UserCode, u.Name, u.Fullname, u.Address, u.MobilePhone, u.Email, u.AvatarURL, u.Description, u.BirthDate, u.JoinDate, u.EducationLevel
FROM [dbo].[T_USER] AS u";

        //actor duongnq
        public const string GetUsersQueryId = @"
SELECT u.PK_UserID, u.Name
FROM [dbo].[T_USER] AS u";

        public const string GetTanSuatQuery = @"
SELECT t.[Id]
      ,t.[SourceId]
      ,t.[Name]
      ,t.[Value]
      ,t.[Nam]
  FROM [dbo].[EX_DM_TANSUAT] AS t
  ORDER BY t.Nam DESC";

        public const string GetTanSuatByNamQuery = @"
SELECT t.[Id]
      ,t.[SourceId]
      ,t.[Name]
      ,t.[Value]
      ,t.[Nam]
  FROM [dbo].[EX_DM_TANSUAT] AS t
  WHERE t.Nam = @p0";

        public const string GetLoaiDTKTQuery = @"
SELECT l.[Id]
      ,l.[SourceId]
      ,l.[Ten]
      ,l.[Diengiai]
      ,l.[Nam]
      ,l.[Phongban]
  FROM [dbo].[EX_DM_LOAIDTKT] AS l
  ORDER BY l.Nam DESC";

        public const string GetLoaiDTKTByNamQuery = @"
SELECT l.[Id]
      ,l.[SourceId]
      ,l.[Ten]
      ,l.[Diengiai]
      ,l.[Nam]
      ,l.[Phongban]
  FROM [dbo].[EX_DM_LOAIDTKT] AS l
  WHERE l.Nam = @p0";

        /// <summary>
        ///  Danh sách đối tượng kiểm toán theo năm
        /// </summary>
        public const string GetDsLapKeHoachNamQuery = @"
SELECT n.[Id], n.Nam, n.LDTKT AS [LoaiDTKT], l.Ten AS [LoaiDTKTText], n.[DTKT], n.TenDTKT AS [DTKTText], n.[Rank], r.MarkValue AS [RankText], n.[GiaTriGoc], n.[DiemQuyDoi], n.[LastKT] AS [ThoiGianKTGanNhat], b.Id AS [BoQuyMoId], b.Ten AS [BoQuyMo], n.[QuyMoId], q.[Ten] as QuyMo, n.[NextKT] AS [ThangDuKienKT], n.[TanSuat], t.Name AS [TanSuatText], n.[NextKT_1] AS [DotKTTiep1], n.[NextKT_2] AS [DotKTTiep2], n.[MucTieu], n.[PhamVi], n.[PhongBan] AS [Phong], p.[Ten] AS [PhongText], n.Leader AS [TruongDoan], u1.FullName AS [TruongDoanText], n.Manager, u2.[FullName] as [ManagerText], IsTrong3Nam
FROM [dbo].[EX_QT_NAM_DTKT] AS n
LEFT JOIN [dbo].[EX_DM_LOAIDTKT] AS l ON n.LDTKT = l.SourceId AND n.Nam = l.Nam
LEFT JOIN [dbo].[T_USER] AS u1 ON n.Leader = u1.PK_UserID
LEFT JOIN [dbo].[T_USER] AS u2 ON n.Manager = u2.PK_UserID
LEFT JOIN [dbo].[EX_DM_PHONGBAN] AS p ON n.PhongBan = p.SourceId
LEFT JOIN [dbo].[EX_DM_TANSUAT] AS t ON n.TanSuat = t.SourceId AND n.Nam = t.Nam
LEFT JOIN [dbo].[EX_DM_RANK] AS r ON n.Rank = r.SourceId AND n.Nam = r.Nam
LEFT JOIN [dbo].[EX_DM_QUYMO] AS q ON n.QuyMoId = q.Id
LEFT JOIN [dbo].[EX_DM_BOQUYMO] AS b ON q.BoQuyMoId = b.Id AND l.SourceId = b.LoaiDTKT AND n.Nam = b.Nam AND b.Nam = l.Nam
WHERE n.Nam = @p0";

        /// <summary>
        /// DANH SÁCH ĐỐI TƯỢNG KIỂM TOÁN ĐƯỢC CHỌN CHO KẾ HOẠCH NĂM
        /// </summary>
        public const string GetDsDoiTuongKiemToanDuocChonChoKeHoachNamQuery = @"
SELECT n.[Id], n.Nam, n.LDTKT AS [LoaiDTKT], l.Ten AS [LoaiDTKTText], n.[DTKT], n.TenDTKT AS [DTKTText], n.[Rank], r.MarkValue AS [RankText], n.[GiaTriGoc], n.[DiemQuyDoi], n.[LastKT] AS [ThoiGianKTGanNhat], b.Id AS [BoQuyMoId], b.Ten AS [BoQuyMo], n.[QuyMoId], q.Ten AS [QuyMo], n.[NextKT] AS [ThangDuKienKT], n.[TanSuat], t.Name AS [TanSuatText], n.[NextKT_1] AS [DotKTTiep1], n.[NextKT_2] AS [DotKTTiep2], n.[MucTieu], n.[PhamVi], n.[PhongBan] AS [Phong], p.[Ten] AS [PhongText], n.Leader AS [TruongDoan], u1.FullName AS [TruongDoanText], n.Manager, u2.[FullName] as [ManagerText], IsTrong3Nam
FROM [dbo].[EX_QT_NAM_DTKT] AS n
LEFT JOIN [dbo].[EX_DM_LOAIDTKT] AS l ON n.LDTKT = l.SourceId AND n.Nam = l.Nam
LEFT JOIN [dbo].[T_USER] AS u1 ON n.Leader = u1.PK_UserID
LEFT JOIN [dbo].[T_USER] AS u2 ON n.Manager = u2.PK_UserID
LEFT JOIN [dbo].[EX_DM_PHONGBAN] AS p ON n.PhongBan = p.SourceId
LEFT JOIN [dbo].[EX_DM_TANSUAT] AS t ON n.TanSuat = t.SourceId AND n.Nam = t.Nam
LEFT JOIN [dbo].[EX_DM_RANK] AS r ON n.Rank = r.SourceId AND n.Nam = r.Nam
LEFT JOIN [dbo].[EX_DM_QUYMO] AS q ON n.QuyMoId = q.Id
LEFT JOIN [dbo].[EX_DM_BOQUYMO] AS b ON q.BoQuyMoId = b.Id AND l.SourceId = b.LoaiDTKT AND n.Nam = b.Nam AND b.Nam = l.Nam
WHERE n.QuyMoId IS NOT NULL AND n.Nam = @p0";

        /// <summary>
        ///  DUYỆT KẾ HOẠCH NĂM
        /// </summary>
        public const string GetDsDuyetKeHoachNamQuery = @"
SELECT n.[Id], n.Nam, n.LDTKT AS [LoaiDTKT], l.Ten AS [LoaiDTKTText], n.[DTKT], n.TenDTKT AS [DTKTText], n.[Rank], r.MarkValue AS [RankText], n.[GiaTriGoc], n.[DiemQuyDoi], n.[LastKT] AS [ThoiGianKTGanNhat], b.Id AS [BoQuyMoId], b.Ten AS [BoQuyMo], n.[QuyMoId], q.[Ten] as QuyMo, n.[NextKT] AS [ThangDuKienKT], n.[TanSuat], t.Name AS [TanSuatText], n.[NextKT_1] AS [DotKTTiep1], n.[NextKT_2] AS [DotKTTiep2], n.[MucTieu], n.[PhamVi], n.[PhongBan] AS [Phong], p.[Ten] AS [PhongText], n.Leader AS [TruongDoan], u1.FullName AS [TruongDoanText], n.Manager, u2.[FullName] as [ManagerText], IsTrong3Nam
FROM [dbo].[EX_QT_NAM_DTKT] AS n
LEFT JOIN [dbo].[EX_DM_LOAIDTKT] AS l ON n.LDTKT = l.SourceId AND n.Nam = l.Nam
LEFT JOIN [dbo].[T_USER] AS u1 ON n.Leader = u1.PK_UserID
LEFT JOIN [dbo].[T_USER] AS u2 ON n.Manager = u2.PK_UserID
LEFT JOIN [dbo].[EX_DM_PHONGBAN] AS p ON n.PhongBan = p.SourceId
LEFT JOIN [dbo].[EX_DM_TANSUAT] AS t ON n.TanSuat = t.SourceId AND n.Nam = t.Nam
LEFT JOIN [dbo].[EX_DM_RANK] AS r ON n.Rank = r.SourceId AND n.Nam = r.Nam
LEFT JOIN [dbo].[EX_DM_QUYMO] AS q ON n.QuyMoId = q.Id
LEFT JOIN [dbo].[EX_DM_BOQUYMO] AS b ON q.BoQuyMoId = b.Id AND l.SourceId = b.LoaiDTKT AND n.Nam = b.Nam AND b.Nam = l.Nam
WHERE n.QuyMoId IS NOT NULL AND n.Nam = @p0";

        public const string GetBoQyMoActiveByNamQuery = @"
SELECT TOP 1 [Id]
      ,[SourceId]
      ,[Ten]
      ,[Nam]
      ,[LoaiDTKT]
      ,[TrangThai]
  FROM [dbo].[EX_DM_BOQUYMO]
  WHERE Nam = @p0 AND TrangThai = 1";

        public const string GetDsQuyMoByNamQuery = @"
SELECT q.[Id]
      ,q.[BoQuyMoId]
      ,q.[Ten]
      ,q.[NguonLuc]
  FROM [dbo].[EX_DM_QUYMO] AS q
  INNER JOIN [dbo].[EX_DM_BOQUYMO] AS b
  ON q.BoQuyMoId = b.Id
  WHERE b.TrangThai = 1 AND b.Nam = @p0";

        public static string GetDanhMucNamByNamQuery = @"
SELECT TOP 1 [Id]
      ,[Nam]
      ,[TrangThaiKeHoachNam]
  FROM [dbo].[EX_DM_NAM]
  WHERE Nam = @p0";

        public const string SuaKeHoachNamQuery = @"
UPDATE [dbo].[EX_QT_NAM_DTKT]
SET DiemQuyDoi = @p1, LastKT = @p2, QuyMoId = @p3, NextKT = @p4, TanSuat = @p5, MucTieu = @p6, PhamVi = @p7, PhongBan = @p8, Leader = @p9, Manager = @p10
WHERE Id = @p0";

        public const string GetDsLoaiDoiTuongKiemToan = @"
SELECT        PK_DocumentID,Status,[Ten],[DienGiai], [SoLuongDTKT]
FROM            T_DOCUMENT DOC OUTER APPLY
					 (SELECT        [Ten] = TEXTVALUE
						 FROM            T_TYPE_DOC_PROPERTY
						 WHERE        FK_DOCUMENTID = DOC.PK_DOCUMENTID AND FK_PROPERTYID = '410D55A8-48D8-4EED-894D-836E24E1E36D') [Ten] OUTER APPLY
					 (SELECT        [DienGiai] = TEXTVALUE
						 FROM            T_TYPE_DOC_PROPERTY
						 WHERE        FK_DOCUMENTID = DOC.PK_DOCUMENTID AND FK_PROPERTYID = '563054B4-4305-4D2D-AE75-8B54F80F56EB') [DienGiai] OUTER APPLY                             
						                           
					 (SELECT        [SoLuongDTKT] = COUNT(1)
						 FROM            T_TYPE_DOC_PROPERTY 
						 WHERE				 TEXTVALUE = Cast([PK_DocumentID] as varchar(36))
						         AND FK_PROPERTYID = '94F402C1-CCC3-4A93-8D9F-2D24BDB8EE2C' ) [SoLuongDTKT]
WHERE        DOC.FK_DOCUMENTTYPEID = '96C6D12F-F4BD-4598-AB18-873AE2362572'
";

        public const string GetNguonLucNamByNamQuery = @"
SELECT SUM (p.NguonLuc) FROM [dbo].[EX_DM_PHONGBAN] AS p
WHERE p.Nam = @p0 AND p.TrangThai = @p1";

        public const string GetNguonLucCanThietByNamQuery = @"
SELECT SUM(q.NguonLuc) FROM [dbo].[EX_DM_QUYMO] AS q
INNER JOIN [dbo].[EX_QT_NAM_DTKT] AS n ON q.Id = n.QuyMoId
WHERE n.Nam = @p0";

        public const string GetDsDoiTuongKiemToan = @"
SELECT        PK_DocumentID, Status, [Ten], [DienGiai], [IDLDTKT], [TenLDTKT]
FROM            T_DOCUMENT DOC OUTER APPLY
					 (SELECT        [Ten] = TEXTVALUE
						 FROM            T_TYPE_DOC_PROPERTY
						 WHERE        FK_DOCUMENTID = DOC.PK_DOCUMENTID AND FK_PROPERTYID = 'F4018BE5-84AD-4FE2-B3AF-5BC3F5981CEA') [Ten] OUTER APPLY
					 (SELECT        [DienGiai] = TEXTVALUE
						 FROM            T_TYPE_DOC_PROPERTY
						 WHERE        FK_DOCUMENTID = DOC.PK_DOCUMENTID AND FK_PROPERTYID = '18BCCAAE-F688-4D50-98C1-0F1EE034AEA0') [DienGiai] OUTER APPLY                             
						(SELECT        [IDLDTKT] = TEXTVALUE
						 FROM            T_TYPE_DOC_PROPERTY
						 WHERE        FK_DOCUMENTID = DOC.PK_DOCUMENTID AND FK_PROPERTYID = '94F402C1-CCC3-4A93-8D9F-2D24BDB8EE2C') [IDLDTKT] OUTER APPLY                             
					 (SELECT        [TenLDTKT] = t2.TEXTVALUE
						 FROM            T_TYPE_DOC_PROPERTY t1 LEFT JOIN
																			 T_TYPE_DOC_PROPERTY t2 ON [IDLDTKT] = Cast(t2.FK_DOCUMENTID as varchar(36))
						 WHERE        t1.FK_DOCUMENTID = DOC.PK_DOCUMENTID AND t1.FK_PROPERTYID = '94F402C1-CCC3-4A93-8D9F-2D24BDB8EE2C' AND t2.FK_PROPERTYID='410D55A8-48D8-4EED-894D-836E24E1E36D') [TenLDTKT]
WHERE        DOC.FK_DOCUMENTTYPEID = '3C976DA6-5E81-41B5-8B6C-D2A593EF6FEC'
";
    }
}