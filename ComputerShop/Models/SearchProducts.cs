using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace ComputerShop.Models
{
    public class SearchProducts
    {
        public PagedList.IPagedList<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
    }
}