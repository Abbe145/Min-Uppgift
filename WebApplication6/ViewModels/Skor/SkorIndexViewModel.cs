using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication6.Models;

namespace WebApplication6.ViewModels.Skor
{
    public class SkorIndexViewModel
    {

        public SkorIndexViewModel()
        {
            SkorList = new List<SkorListViewModel>();
        }

        public class SkorListViewModel
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Model { get; set; }
            public int Size { get; set; }
            public string Color { get; set; }
            public decimal Price { get; set; }
            public string Description { get; set; }
            public int CategoryId { get; set; }
        }

        public string Search { get; set; }
        public string SortOrder { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public List<SkorListViewModel> SkorList { get; set; }
    }
}