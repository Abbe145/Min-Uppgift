using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication6.ViewModels.Category
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "The Name field is required.")]
        public string CategoryName { get; set; }
    }
}