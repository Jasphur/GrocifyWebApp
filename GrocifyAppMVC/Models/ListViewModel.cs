using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyWeb.Models
{
    public class ListViewModel<T>
    {
        public List<T> ListItems { get; set; }

        public bool AllowEdit { get; set; }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int Pagecount { get; set; }

        public ListViewModel()
        {
            PageSize = 2;
            PageNumber = 1;
            ListItems = new List<T>();
        }
    }
}