using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo.Core.Utilities
{
    public class PagerHelper
    {
        public int RecordCount { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int PageCount
        {
            get
            {
                return (RecordCount - 1) / PageSize + 1;
            }
        }

        const int SHOW_COUNT = 6; //This constant represents the number of page links in the middle when there are ellipses on both sides, such as 1..4 5 6 7 8 9..100
        private List<int> CalcPages()
        {
            List<int> pages = new List<int>();

            int start = (PageIndex - 1) / SHOW_COUNT * SHOW_COUNT + 1;
            int end = Math.Min(PageCount, start + SHOW_COUNT - 1);

            if (start == 1)
            {
                end = Math.Min(PageCount, end + 1);
            }
            else if (end == PageCount)
            {
                start = Math.Max(1, end - SHOW_COUNT);
            }
            pages.AddRange(Enumerable.Range(start, end - start + 1));

            if (start == PageIndex && start > 2)
            {
                pages.Insert(0, start - 1);
                pages.RemoveAt(pages.Count - 1);//Keep the number of pages displayed uniform
            }
            if (end == PageIndex && end + 1 < PageCount)
            {
                pages.Add(end + 1);
                pages.RemoveAt(0);  //Keep the number of pages displayed uniform
            }
            if (PageCount > 1)
            {
                if (pages[0] > 1)
                {
                    pages.Insert(0, 1); //If the first item in the page list is not the first page, add the first page to the team header
                }
                if (pages[1] == 3)
                {
                    pages.Insert(1, 2); //If is 1.. 3 in this case, the.. With 2
                }
                else if (pages[1] > 3)
                {
                    pages.Insert(1, -1); //Insert the left ellipsis
                }

                if (pages.Last() == PageCount - 2)
                {
                    pages.Add(PageCount - 1); //If it is 98.. 100 in this case,... In 99
                }
                else if (pages.Last() < PageCount - 2)
                {
                    pages.Add(-1); //Insert the right ellipsis
                }

                if (pages.Last() < PageCount)
                {
                    pages.Add(PageCount); //Last page
                }
            }

            return pages;
        }

        /// <summary>
        /// An array of page Numbers used for display that is an ellipsis if there is a page number less than 0 in the middle
        /// </summary>
        public List<int> Pages
        {
            get
            {
                return CalcPages();
            }
        }
    }
}