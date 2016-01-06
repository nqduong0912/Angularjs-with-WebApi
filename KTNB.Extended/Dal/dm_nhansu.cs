using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KTNB.Extended.Biz;
using ServiceStack.DataAnnotations;

namespace KTNB.Extended.Dal
{
    [Alias("T_USER")]
    public class dm_nhansu
    {
        [Required]
        public System.Guid PK_UserID { get; set; }

        [StringLength(50)]
        public string UserCode { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(128)]
        public string PassWord { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

        public byte IsExpired { get; set; }

        public string ActivationDateTime { get; set; }

        [StringLength(256)]
        public string Email { get; set; }

        [StringLength(20)]
        public string MobilePhone { get; set; }

        [StringLength(50)]
        public string Order_Number { get; set; }

        [StringLength(50)]
        public string Fullname { get; set; }

        [StringLength(256)]
        public string Address { get; set; }

        public int IsAuthenticateSQL { get; set; }

        [StringLength(255)]
        public string AvatarURL { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime JoinDate { get; set; }

        public DateTime BirthDate { get; set; }

        public string EducationLevel { get; set; }
    }
}
