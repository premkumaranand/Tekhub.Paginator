using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekhub.Paginator
{
    public class Paginator
    {
        private const int MidRangeItemsMaxCount = 5;
        private int _midRangeStart;
        private int _midRangeEnd;

        public long TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }

        public int TotalPages
        {
            get
            {
                return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
            }
        }

        public List<PaginatorItem> GetPages()
        {
            var paginatorItems = new List<PaginatorItem>();

            var midRangePages = GetMidRange();

            if (DisplayPreviousLink())
            {
                paginatorItems.Add(new PaginatorItem { PaginatorItemType = PaginatorItemType.Previous, Page = CurrentPage - 1 });
            }

            if (DisplayFirstPageLink())
            {
                paginatorItems.Add(new PaginatorItem { PaginatorItemType = PaginatorItemType.Page, Page = 1 });
                paginatorItems.Add(new PaginatorItem { PaginatorItemType = PaginatorItemType.Separator });
            }

            foreach (var midRangePage in midRangePages)
            {
                paginatorItems.Add(new PaginatorItem
                {
                    PaginatorItemType = PaginatorItemType.Page,
                    Page = midRangePage,
                    IsCurrentPage = (midRangePage == CurrentPage)
                });
            }

            if (DisplayLastPageLink())
            {
                paginatorItems.Add(new PaginatorItem { PaginatorItemType = PaginatorItemType.Separator });
                paginatorItems.Add(new PaginatorItem { PaginatorItemType = PaginatorItemType.Page, Page = TotalPages });
            }

            if (DisplayNextLink())
            {
                paginatorItems.Add(new PaginatorItem { PaginatorItemType = PaginatorItemType.Next, Page = CurrentPage + 1 });
            }

            return paginatorItems;
        }

        private List<int> GetMidRange()
        {
            if (TotalPages > MidRangeItemsMaxCount + 2)
            {
                _midRangeStart = Math.Max(1, CurrentPage - 2);
                _midRangeEnd = Math.Min(_midRangeStart + 4, TotalPages);

                if (_midRangeStart == 2)
                {
                    _midRangeStart = 1;
                }

                if (_midRangeEnd == TotalPages - 1)
                {
                    _midRangeEnd = TotalPages;
                }


                var midRangeLength = _midRangeEnd - _midRangeStart + 1;

                if (_midRangeEnd == TotalPages && midRangeLength > MidRangeItemsMaxCount)
                {
                    _midRangeStart += midRangeLength - MidRangeItemsMaxCount;
                }

                if (_midRangeEnd == TotalPages && midRangeLength < MidRangeItemsMaxCount)
                {
                    _midRangeStart -= (MidRangeItemsMaxCount - midRangeLength);
                }

                if (_midRangeStart == 1 && midRangeLength > MidRangeItemsMaxCount)
                {
                    _midRangeEnd -= midRangeLength - 5;
                }

                _midRangeStart = Math.Max(1, _midRangeStart);
                _midRangeEnd = Math.Min(TotalPages, _midRangeEnd);

                if (_midRangeStart == 2)
                {
                    _midRangeStart = 1;
                }
            }
            else
            {
                _midRangeStart = 1;
                _midRangeEnd = TotalPages;
            }

            var pages = new List<int>();

            for (var pageIter = _midRangeStart; pageIter <= _midRangeEnd; pageIter++)
            {
                pages.Add(pageIter);
            }

            return pages;
        }

        private bool DisplayFirstPageLink()
        {
            return _midRangeStart > 2;
        }

        private bool DisplayLastPageLink()
        {
            return _midRangeEnd < TotalPages - 1;
        }

        private bool DisplayPreviousLink()
        {
            return CurrentPage != 1;
        }

        private bool DisplayNextLink()
        {
            return CurrentPage != TotalPages;
        }
    }
}
