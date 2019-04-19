using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication6.ViewModels.Skor
{
    public class SkoViewModel
    {
        public int SkorId { get; set; }
        [Required(ErrorMessage = "The Name field is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The Color field is required.")]
        public string Color { get; set; }
        [Required(ErrorMessage = "The Model field is required.")]
        public string Model { get; set; }
        [Required(ErrorMessage = "The Size field is required.")]
        public int Size { get; set; }
        [Required(ErrorMessage = "The Price field is required.")]
        [DataType(DataType.Currency, ErrorMessage = "You have to enter a number.")]
        [Range(1, int.MaxValue, ErrorMessage = "The Price field must be between 1 and 2147483647.")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "The Description field is required.")]
        public string Description { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<SelectListItem> CategoryDropDownList { get; set; }
    }
}