using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTNB.Extended.Core
{
    public class AppConfig
    {
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["VPB_KTNB_DEV"].ConnectionString ?? "Data Source=10.37.0.114;Initial Catalog=VPB_KTNB_DEV;Persist Security Info=True;User ID=ktnb_dev; Password=vsi2015; Max Pool Size=300;Min Pool Size=10;Pooling=true;MultipleActiveResultSets=false;";
            }
        }
    }
}
