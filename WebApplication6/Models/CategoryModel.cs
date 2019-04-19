using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication6.Models
{
    public class CategoryModel
    {
        [Key]
        [Required]
        public int CategoryId { get; set; }
        [StringLength(100)]
        public string CategoryName { get; set; }

        public ICollection<SkorModel> Skor { get; set; }

    }
}