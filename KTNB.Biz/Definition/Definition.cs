namespace vpb.app.business
{
    namespace ktnb.Definition
    {
        namespace UMS
        {
            public static class USERS
            {
                #region TYPE OF SPECIAL USER

                public const string SYSTEM = "00000000-0000-0000-0000-000000000001";
                public const string ADMIN = "00000000-0000-0000-0000-000000000002";
                public const string EVERYONE = "00000000-0000-0000-0000-000000000003";

                #endregion TYPE OF SPECIAL USER
            }

            public static class ROLES
            {
                #region TYPE OF ROLE

                public const string SYS_ADMIN = "89C2233B-1337-4A4B-B610-313061533820";
                public const string APP_ADMIN = "D7FA5773-8EE4-48F2-BBE7-C3FFE61244F7";

                public const string BANLANHDAO = "298202FD-0888-451C-85A6-C52BB04A8096";
                public const string CANBO_GSTX = "43A7177A-E5F5-46B7-A6AC-8F218DFEEC8C";
                public const string THANHVIEN_KIEMTOAN = "4C5C7F82-4460-40C5-984A-ADE81D47C792";

                // public const string TRUONGDOAN_KIEMTOAN = "9C3B8CC9-12D0-48C6-B568-A05611F6E2AB";
                // public const string TRUONGNHOM_KIEMTOAN = "3F76CE2B-0B70-4983-AD42-1D826781907E";
                public const string TRUONGPHONG_CSCC = "DC064BD2-35B2-4A39-A3F6-6B45F79A21B9";

                public const string TRUONGPHONG_DONVI_KIEMTOAN = "99A121FA-55C1-48BE-B31B-C09C4102ABA6";
                public const string CANBO_DUYET = "81022EFB-B01E-410C-A21A-90D13460AB80";

                #endregion TYPE OF ROLE
            }

            public static class GROUPS
            {
                #region TYPE OF SPECIAL GROUP

                public const string SYS_ADMIN = "60EB2AB8-88E0-4C07-9FD9-33C539E45AF3";
                public const string APP_ADMIN = "8EB1BB6A-B8B9-490F-A29F-A8C317A29E52";
                public const string HOI_SO = "A50BF9F7-8C06-4965-9585-F2B832D58A0A";
                public const string TOANHANG = "2F7709FA-DDFD-44EF-974B-F1151662F84A";
                public const string GROUP_OF_KTNB = "2366996F-97B6-42BA-82F7-F69F957484D4";

                /// <summary>
                /// Ban giám đốc khối
                /// </summary>
                public const string KTNB1 = "20AC286F-FE83-46AD-BD23-776D06CC0CB4";

                /// <summary>
                /// Ban kiểm soát
                /// </summary>
                public const string BKS = "BA0735E6-09AE-4691-8010-F58FE09FB53F";

                #endregion TYPE OF SPECIAL GROUP
            }

            public static class GROUPTYPE
            {
                public const byte TT_HOTRO = 1;
                public const byte CHI_NHANH = 3;
                public const byte PHONG_GD = 5;
                public const byte DONVI_TAI_CHI_NHANH = 35;
                public const byte SYS_ADMIN = 7;
                public const byte APP_ADMIN = 9;
                public const byte TOAN_HANG = 11;
                public const byte PHONGBAN_TAI_HOISO = 13;
                public const byte DONVI = 15;
            }

            public static class PRIVACY
            {
                #region TYPE OF PASSWORD

                public const string PASSWORD_DEFAULT = "123456";

                #endregion TYPE OF PASSWORD
            }

            public static class TYPE_OF_APPLICANT
            {
                #region TYPE OF APPLICANT

                public const byte USER = 1;
                public const byte ROLE = 2;
                public const byte GROUP = 4;

                #endregion TYPE OF APPLICANT
            }
        }

        namespace DMS
        {
            public static class DOCTYPE
            {
                #region DOC TYPE

                /*THƯ MỤC*/
                public const string THU_MUC = "FA95089B-72FF-4B67-B013-6DDBD4B07F59";

                /*CÁC DANH MỤC*/
                public const string RUIRO_CHINH = "9379440E-85BA-4A53-BB98-2A1CC10C9C82";
                public const string RUIRO_PHU = "B1AAB866-6B48-440D-A5B8-3D76836588BD";
                public const string LOAI_DOITUONG_KT = "96C6D12F-F4BD-4598-AB18-873AE2362572";
                public const string DOITUONG_KT = "3C976DA6-5E81-41B5-8B6C-D2A593EF6FEC";
                public const string QUYMO_KT = "86C9EA7F-DF46-4D40-8714-17FBBA84A773";
                public const string MANG_NGHIEPVU = "34187F34-392F-492A-A8F6-6954DDF6DEA9";
                public const string TIEUCHI_DANHGIA_XACSUAT_ANHHUONG = "89A589E7-9990-4CCE-B5BE-EE1C06F5A703";
                public const string RUIRO_KIEMSOAT = "F3F02F88-BEC8-469D-BEC0-80F9DD3ACF22";
                public const string KIEMSOAT = "25A386F6-613E-4A71-9C19-C83D33E81D94";
                public const string TIEUCHI_DANHGIA_CHATLUONG_KTV = "AA429BE7-3EE3-4A83-BB84-02A650D83339";
                public const string LOAI_TIEUCHI_DANHGIA_CHATLUONG_KTV = "EAB2AE8F-9C02-4E0D-8CED-AE155A93E18F";
                public const string MUCTIEU_KIEMSOAT = "F39BF431-3EA0-49FE-AC44-023A65A99171";
                public const string CONGVIEC = "87836B2A-D04C-43EF-8F1A-28DA575FE329";
                public const string THUTUC_KIEMTOAN = "45BF910B-9A1E-4AF8-83BD-46651D010E59";
                public const string PHANLOAI_BOTIEUCHI_DANHGIA_CLKTV = "FA63D094-A264-406C-992F-C7B1FF364BDC";
                public const string BOTIEUCHI_DANHGIA_CLKTV = "D3970051-FC77-47AD-A493-E8B6AA446E04";
                public const string MUCDO_RUIRO = "5CB81D04-29BF-4045-B610-5AEA9BE54615";

                //danh muc them 22/4
                public const string PHANLOAI_BOTIEUCHI_NAM = "82DA7639-CE94-4642-8933-6EB2BF7C931C";

                public const string CACBOTIEUCHI_NAM = "E06A30D9-06A5-4026-9D14-5BCCC0224370";
                public const string TIEUCHI_CHINH = "53F3869E-8A2E-4FCC-AD90-510E7A688970";
                public const string TIEUCHI_THANHPHAN = "EF498D3B-C2F7-47DE-9BF0-793F6D23D43D";
                public const string TIEUCHI_DINHTINH = "BEAC084C-0EF9-42D2-8305-3BC4F7489B54";

                //Nghiệp vụ vi phạm, KhanhNP added 6/5/15
                public const string NGHIEPVU_VIPHAM = "C69A95B6-A55D-4865-846C-57D93AC53EBF";

                public const string NHOMHANHVI_VIPHAM = "51CB76AF-4E64-4E91-AED9-7F9E1B7AADC1";
                public const string HANHVI_VIPHAM = "6B5B27B2-3947-4859-96FD-A041224F52E8";

                //Hình thức xử lý, KhanhNP added 7/5/15
                public const string HINHTHUCXULY_LUATLAODONG = "3328E604-069F-419F-94D3-43EF16F6FFD9";

                public const string HINHTHUCXULY_QUYDINHNOIBO = "1F5DC46B-24F9-4416-A256-56CDB3091AA6";
                public const string QUANLY_PHANLOAI_BOTIEUCHI = "D5FE6A69-0562-4EA6-AFFD-F3934353D700";
                public const string QUANLY_BOTIEUCHI_KEHOACHNAM = "89351BEC-D43C-4B7F-B2E5-9B72504DD95B";
                public const string BAN_DANH_GIA = "D4FDE1CF-D336-43ED-BF3E-23276A49A0CB";
                public const string TAN_SUAT = "1BD885A8-1B6B-4875-B010-1516399A5FF2";
                public const string RANK_LDTKT = "F16E2A5A-5CD4-4267-9F9A-B3FC810D9D6C";

                /*ĐỢT KIỂM TOÁN, ĐOÀN KIỂM TOÁN, NHÓM KIỂM TOÁN*/
                public const string KEHOACH_KIEMTOAN_NAM = "8FB6FBE2-82DB-4B87-9BF7-8E2942DD8B4F";
                public const string DOT_KIEMTOAN = "7AEC7D63-31E1-41AF-B960-42A11117BF1B";
                public const string DOAN_KIEMTOAN = "91845A1B-2BFA-4153-A661-2B5D43963FA4";
                public const string NHOM_KIEMTOAN = "62B9BEBA-57FD-4F19-9FB3-01ACD60AEAB5";

                /*LẬP KẾ HOẠCH*/
                public const string HOSO_PHANTICH_SOBO = "E4AFC4D0-3972-4E7A-9E03-126E69086431";
                public const string CHITIET_HOSO_PHANTICH_SOBO = "E0827C65-4C96-4B78-B778-CC49EDECA112";
                public const string CHITIET_HOSO_RUIRO = "63090F39-2203-499E-A780-AF2EA6025BC3";
                public const string CONGVIEC_TRONG_DOTKIEMTOAN = "C0EB39DA-11B7-4507-B401-0CD9E0AE557B";

                public const string PHATHIEN_HETHONG = "DB4C7D3D-130D-4FFE-8E84-5F1344CE9D6E";
                public const string PHATHIEN_VIPHAM = "AF682797-B702-49C4-961F-B503CA2FB403";
                public const string THONGTIN_PHANHOI = "0E5DC17C-F4A2-479C-896C-584C46A62B93";

                /*Chức Danh*/
                public const string CHUCDANH = "802AD7EE-F0F9-4109-82DC-E4C49C6E258B";
                public const string PHONGBAN = "26A861DF-47AF-43E9-93E5-E35820A29250";
                public const string QLNHANSU_KTNB = "83373FF4-F888-4CD1-BDE5-3B0A2691EC14";
                public const string QuymoLoaiDTKT = "99B33D25-3E20-455B-8676-E56B28E4DBCE";

                /*Quy trình activity sản phẩm*/
                public const string QLQuytrinhActivitySanpham = "80EA9145-A055-4079-9C71-20C3E500A99A";

                /*Quy mô đối tượng kiểm toán*/
                public const string QuyMoDTKT = "B821DF51-818B-4BB4-8D71-CAE979C6F2F9";

                #endregion DOC TYPE
            }

            public static class DOCSPACE
            {
                #region DOCSPACE

                //do not change these docspaces !
                public const string CAC_DANH_MUC = "D970B521-D3FB-42B2-B36D-89F95D1356FF";

                public const string NONE = "00000000-0000-0000-0000-000000000000";

                #endregion DOCSPACE
            }

            public static class VIEWTYPE
            {
                public const byte ADDNEW = 1;
                public const byte SHOW = 3;
                public const byte EDIT = 5;
                public const byte REPORT = 7;
                public const byte ADDNEW_ON_PROCESS = 2;
                public const byte SHOW_ON_PROCESS = 4;
                public const byte EDIT_ON_PROCESS = 6;
            }

            public static class TYPE_OF_OBJECT
            {
                #region TYPE OF OBJECT

                public const byte DOCUMENT = 11;
                public const byte FOLDER = 12;
                public const byte DOCSPACE = 13;
                public const byte APPLICATION = 14;
                public const byte COMPONENT = 15;
                public const byte USER = 16;

                #endregion TYPE OF OBJECT
            }

            public static class TYPE_OF_LINK
            {
                #region TYPE OF LINK

                public const byte DOCUMENT = 11;
                public const byte USER = 13;
                public const byte ROLE = 15;
                public const byte GROUP = 17;
                public const byte FOLDER = 19;

                #endregion TYPE OF LINK
            }
        }

        namespace WFS
        {
            public static class WORKFLOW_DEFINITION
            {
            }

            public static class ACTIVITY_DEFINITION
            {
            }

            public static class ACTIVITY_PROPERTY
            {
            }
        }

        namespace OPERATORS
        {
            public static class TYPE_OF_PERMISSION
            {
                #region TYPE OF PERMISSION

                public const byte READ = 1;
                public const byte EDIT = 2;
                public const byte DELETE = 4;
                public const byte CREATE = 8;
                public const byte FULL = READ + EDIT + DELETE + CREATE;

                #endregion TYPE OF PERMISSION
            }

            public static class BACKEND_ACTION
            {
                public const int CREATE_DOCUMENT = 1;
                public const int UPDATE_DOCUMENT = 2;
                public const int UPDATE_PROPERTYVALUE_ON_DOCUMENT = 3;
                public const int CREATE_DOCUMENT_WITH_DOCLINK = 4;
                public const int LOAD_DOCUMENT = 5;
                public const int SEARCH_DOCUMENT = 6;
                public const int DELETE_DOCUMENT = 8;
                public const int REMOVE_DOCUMENTLINK = 16;

                public const int CREATE_PROCESS = 32;
                public const int DELETE_ACTIVITY = 64;
                public const int DELETE_PROCESS = 128;
                public const int DELETE_TRANSITION = 256;
            }

            public static class AUDITLOG
            {
                public const int LOGIN = 1;
                public const int LOGOUT = 3;
                public const int CREATE = 5;
                public const int EDIT = 7;
                public const int DELETE = 9;
                public const int READ = 11;
                public const int APPEND = 2;
                public const int DISTRIBUTION = 4;
                public const int SET_PERMISSION = 6;
                public const int SHARE_PERMISSION = 8;
                public const int RECALL = 10;
                public const int MONITORING = 12;
            }

            public static class AMNDSTATE
            {
                public const char BLOCK = 'B';

                public const char INACTIVE = 'I';

                public const char ACTIVE = 'A';

                public const char DELETE = 'D';
            }

            public static class STATUS
            {
                public const byte PREPARE = 1;

                /// <summary>
                /// Chưa xem
                /// </summary>
                public const byte INACTIVE = 2;

                /// <summary>
                /// Đang xử lý
                /// </summary>
                public const byte ACTIVE = 4;

                /// <summary>
                /// Hủy giữa chừng
                /// </summary>
                public const byte ABORTED = 8;

                /// <summary>
                /// Đã hoàn tất - xử lý xong
                /// </summary>
                public const byte FINISHED = 16;

                /// <summary>
                /// Quá hạn
                /// </summary>
                public const byte OVERDUE = 32;
            }
        }
    }
}