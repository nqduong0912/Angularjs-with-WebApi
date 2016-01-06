using KTNB.Extended.Biz.Dao;
using KTNB.Extended.Biz.IDao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTNB.Extended.Biz
{
    public class CoreFactory<T>
    {
        public static IEntityManager<T> EntityManager
        {
            get
            {
                return new EntityManager<T>();
            }
        }
    }

    public class ManagerFactory
    {
        public static Iloaidoituongkiemtoan loaidoituongkiemtoan_manager
        {
            get
            {
                return new loaidoituongkiemtoan();
            }
        }

        public static Iserverconf serverconf_manager
        {
            get
            {
                return new serverconf();
            }
        }

        public static Iapplicationrole applicationrole_manager
        {
            get
            {
                return new bus_app_role();
            }
        }

        public static IUserManager user_manager
        {
            get
            {
                return  new usermanager	();
            }
        }

        public static IBoQuymoManager boquymo_manager
        {
            get
            {
                return new BoQuyMoManager();
            }
        }

        public static IQuymoManager quymo_manager
        {
            get
            {
                return  new QuyMoManager();
            }
        }

        public static IT_DoucumentManager t_document_manager
        {
            get
            {
                return new T_DocumentManager();
            }
        }

        public static Iqlybotieuchi qlybotieuchi_manager
        {
            get
            {
                return new qlybotieuchi();
            }
        }

        public static IT_TypeDocProperty t_type_doc_property
        {
            get
            {
                return  new T_TypeDocPropertyManager();
            }
        }
        public static IGroup_Manager group_manager
        {
            get
            {
                return new Group_Manager();
            }
        }

        public static Idotkiemtoan dotkiemtoan_manager
        {
            get
            {
                return new dotkiemtoan();
            }
        }
    }
}
