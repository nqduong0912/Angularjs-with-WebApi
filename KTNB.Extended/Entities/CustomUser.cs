using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTNB.Extended.Entities
{
    public class CustomUser
    {
        [Key]
        public Guid PK_UserID { get; set; }

        [StringLength(50)]
        public string UserCode { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string FullName { get; set; }

        //public DateTime? CreatedDate { get; set; }

        //public byte? IsExpired { get; set; }

        //public byte? IsAuthenticateSQL { get; set; }

        //public DateTime? ActivationDateTime { get; set; }

        //public short? Order_Number { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        [StringLength(20)]
        public string MobilePhone { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(255)]
        public string AvatarURL { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        //[StringLength(128)]
        //public string PassWord { get; set; }

        //[StringLength(255)]
        //public string SessionID { get; set; }

        //public DateTime? SessionCreatedDateTime { get; set; }

        public DateTime? BirthDate { get; set; }

        public DateTime? JoinDate { get; set; }

        [StringLength(50)]
        public string EducationLevel { get; set; }

        public string PhongBan { get; set; }
    }
}
