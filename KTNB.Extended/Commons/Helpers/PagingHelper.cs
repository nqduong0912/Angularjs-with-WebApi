using System.Collections.Generic;
using System.Text;

namespace KTNB.Extended.Commons.Helpers
{
    public class PagingHelper
    {
        //int curPage = 0;
        //if (Request.QueryString["page"] != null)
        //    int.TryParse(Request.QueryString["page"].ToString(), out curPage);
        //curPage--;
        //if (curPage < 0)
        //    curPage = 0;
        //Lấy dữ liệu với curpage gốc, bắt đầu từ 0
        //curPage++;
        //Generate paging với curpage bắt đầu từ 1
        //lblPaging.Text = PagingHelper.GetPaging(Request.RawUrl, 5, "page", curPage, (int)Math.Ceiling((double)totalRecord / Constants.PAGESIZE));
        public static string GetPaging(string currentUrl, int pagesToOutput, string key, int currentPage, int pageCount)
        {
            return GetPaging(currentUrl, pagesToOutput, key, currentPage, pageCount, string.Empty);
        }

        public static string GetPaging(string currentUrl, int pagesToOutput, string key, int currentPage, int pageCount, string anchorName)
        {
            // << < 1...5,6,7,8,9..19 > >>
            //pagesToOutput : Số page sẽ hiển thị
            //key : tên tham số eg. page=1, key=3
            //currentPage : Be care ^^, currentPage bắt đầu = 1
            //pageCount : Tổng số page
            if (pagesToOutput % 2 != 0)
                pagesToOutput++;

            int pagesToOutputHalfed = pagesToOutput / 2;

            //trả về string format
            var urlParser = new UrlParser(currentUrl);

            //Tính số lượng page sẽ hiển thị
            int startPageNumbersFrom = currentPage - pagesToOutputHalfed;
            int stopPageNumbersAt = currentPage + pagesToOutputHalfed;

            StringBuilder output = new StringBuilder();
            output.Append("<ul class=\"page\" style=\"margin: 0px;\">");

            if (currentPage > 1)
            {
                var lstUrl = new Dictionary<string, string>();
                lstUrl.Add(key, currentPage.ToString());

                output.Append("<li class=\"next_back\"><a href=\"" + urlParser.CreateQueryString(new Dictionary<string, string>() { { key, "1" } }, anchorName) +
                              "\">Trang đầu</a></li>");
                output.Append("<li class=\"next_back\"><a href=\"" + urlParser.CreateQueryString(new Dictionary<string, string>() { { key, (currentPage - 1).ToString() } }, anchorName) +
                              "\">Trước</a></li>");
            }

            if (startPageNumbersFrom < 1)
            {
                startPageNumbersFrom = 1;
                stopPageNumbersAt = pagesToOutput;
            }

            if (stopPageNumbersAt > pageCount)
            {
                stopPageNumbersAt = pageCount;
            }

            if ((stopPageNumbersAt - startPageNumbersFrom) < pagesToOutput)
            {
                startPageNumbersFrom = stopPageNumbersAt - pagesToOutput;
                if (startPageNumbersFrom < 1)
                {
                    startPageNumbersFrom = 1;
                }
            }

            if (startPageNumbersFrom > 1)
                output.Append("<li><a href=\"" +
                                urlParser.CreateQueryString(new Dictionary<string, string>() { { key, (startPageNumbersFrom - 1).ToString() } }, anchorName)
                               + "\">&hellip;</a></li>");

            for (int i = startPageNumbersFrom; i <= stopPageNumbersAt; i++)
            {
                if (currentPage == i)
                    output.Append("<li><a class=\"next_back\" href=\"" + urlParser.CreateQueryString(new Dictionary<string, string>() { { key, i.ToString() } }, anchorName) + "\">" + i.ToString() + "</a></li>");
                else
                    output.Append("<li><a href=\"" + urlParser.CreateQueryString(new Dictionary<string, string>() { { key, i.ToString() } }, anchorName) + "\">" + i.ToString() + "</a></li>");
            }

            if (stopPageNumbersAt < pageCount)
                output.Append("<li><a href=\"" + urlParser.CreateQueryString(new Dictionary<string, string>() { { key, (stopPageNumbersAt + 1).ToString() } }, anchorName) +
                              "\">&hellip;</a></li>");

            if (currentPage != pageCount)
            {
                output.Append("<li class=\"next_back\"><a href=\"" + urlParser.CreateQueryString(new Dictionary<string, string>() { { key, (currentPage + 1).ToString() } }, anchorName) +
                              "\">Sau</a></li>");
                output.Append("<li class=\"next_back\"><a href=\"" + urlParser.CreateQueryString(new Dictionary<string, string>() { { key, pageCount.ToString() } }, anchorName) +
                              "\">Trang cuối</a></li>");
            }

            output.Append("</ul>");

            return output.ToString();
        }
    }
}