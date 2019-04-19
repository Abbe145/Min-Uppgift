using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication6.ViewModels.Category
{
    
    public class CategoryIndexViewModel
    {

        public CategoryIndexViewModel()
        {
            Categories = new List<CategoryListViewModel>();
        }

        public class CategoryListViewModel
        {
            public int CategoryId { get; set; }
            public string CategoryName { get; set; }
        }

        public List<CategoryListViewModel> Categories { get; set; }
    }
}