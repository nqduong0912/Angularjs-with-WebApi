using System;
using System.Collections.Generic;
using System.Linq;

namespace KTNB.Extended.Commons.PagedUtils
{
    public class PagedList<TData> : IDisposable
    {
        private int[] _pages;

        [Obsolete("Không sử dụng constructor này.")]
        public PagedList()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection">Danh sách các item trên trang hiện tại.</param>
        /// <param name="totalItems">Số bản ghi.</param>
        /// /// <param name="currentPage">Trang hiện tại.</param>
        /// <param name="pageSize">Số lượng item trên một trang.</param>
        /// <param name="numberOfLinks">Should be an odd number.</param>
        public PagedList(List<TData> collection, int totalItems, int currentPage = 1, int pageSize = 10, int numberOfLinks = 5)
        {
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            if (currentPage < 1)
            {
                currentPage = totalPages > 0 ? 1 : 0;
            }
            else if (currentPage > totalPages)
            {
                // Gán page thành page cuối cùng
                currentPage = totalPages;
            }

            int canLeftRightPages = (int)Math.Floor(numberOfLinks / 2d);
            int firstPageNumber = currentPage - canLeftRightPages, lastPageNumber = currentPage + canLeftRightPages;
            int addLeft = 0, addRight = 0;
            if (firstPageNumber < 1)
            {
                addRight = 1 - firstPageNumber;
            }

            lastPageNumber += addRight;
            if (lastPageNumber > totalPages)
            {
                addLeft = lastPageNumber - totalPages;
                lastPageNumber = totalPages;
            }

            firstPageNumber -= addLeft;
            if (firstPageNumber < 1)
            {
                firstPageNumber = 1;
            }

            var hasPreviousPage = currentPage > 1;
            var hasNextPage = currentPage < totalPages;

            Items = collection;
            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            NumberOfLinks = numberOfLinks;
            HasNextPage = hasNextPage;
            HasPreviousPage = hasPreviousPage;
            FirstPageNumber = firstPageNumber;
            LastPageNumber = lastPageNumber;
        }

        public List<TData> Items { get; set; }

        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public int TotalItems { get; set; }

        public int NumberOfLinks { get; set; }

        public int TotalPages { get; set; }

        public bool HasPreviousPage { get; set; }

        public bool HasNextPage { get; set; }

        public int FirstPageNumber { get; set; }

        public int LastPageNumber { get; set; }

        public int[] Pages
        {
            get
            {
                if (_pages == null)
                {
                    _pages = Enumerable.Range(FirstPageNumber, LastPageNumber - FirstPageNumber + 1).ToArray();
                }

                return _pages;
            }
            set
            {

            }
        }

        public void Dispose()
        {
            if (Items != null)
            {
                Items.Clear();
            }

            if (_pages != null)
            {
                _pages = null;
            }
        }
    }
}
