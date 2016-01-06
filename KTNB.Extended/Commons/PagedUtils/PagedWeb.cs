using System;
using System.Text;

namespace KTNB.Extended.Commons.PagedUtils
{
    public class PagedWeb<TData> : IDisposable
    {
        private readonly PagedList<TData> _pagedUtils;

        public PagedWeb(PagedList<TData> pagedUtils)
        {
            _pagedUtils = pagedUtils;
        }

        public string GenerateHtmlPaged(Func<int, string> calcPageUrl)
        {
            StringBuilder builder = new StringBuilder("<ul class=\"pagination\">");
            if (_pagedUtils.HasPreviousPage)
            {
                builder.AppendLine("<li>");
            }
            else
            {
                builder.AppendLine("<li class=\"disabled\">");
            }

            builder.AppendFormat("<a href=\"{0}\"><span aria-hidden=\"true\">Previous</span><span class=\"sr-only\">Previous</span></a>", calcPageUrl(1));
            builder.Append("</li>");

            for (int i = _pagedUtils.FirstPageNumber; i <= _pagedUtils.LastPageNumber; i++)
            {
                if (i == _pagedUtils.CurrentPage)
                {
                    builder.AppendLine("<li class=\"active\">");
                }
                else
                {
                    builder.AppendLine("<li>");
                }

                builder.AppendFormat("<a href=\"{0}\">{1} <span class=\"sr-only\">(current)</span></a>", calcPageUrl(i), i);
                builder.Append("</li>");
            }

            if (_pagedUtils.HasNextPage)
            {
                builder.AppendLine("<li>");
            }
            else
            {
                builder.AppendLine("<li class=\"disabled\">");
            }

            builder.AppendFormat("<a href=\"{0}\"><span aria-hidden=\"true\">Next</span><span class=\"sr-only\">Next</span></a>", calcPageUrl(_pagedUtils.TotalPages));
            builder.Append("</li>");

            builder.AppendLine("</ul>");

            return builder.ToString();
        }

        public void Dispose()
        {
            _pagedUtils.Dispose();
        }
    }
}
