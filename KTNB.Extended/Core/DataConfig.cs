using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTNB.Extended.Core
{
    public class DataConfig
    {
        public static IDbConnection GetConnection()
        {
            OrmLiteConfig.DialectProvider = SqlServerDialect.Provider;
            OrmLiteConfig.DialectProvider.UseUnicode = true;

            return AppConfig.ConnectionString.OpenDbConnection();
        }
    }
}
