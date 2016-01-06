using KTNB.Extended.Commons.PagedUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KTNB.Extended.Extensions
{
    public static class PagedExtensions
    {
        public static PagedWeb<TData> ConvertToPagedWeb<TSource, TData>(this PagedList<TData> pagedList)
        {
            return new PagedWeb<TData>(pagedList);
        }
    }
}