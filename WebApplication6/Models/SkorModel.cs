using System.ComponentModel.DataAnnotations;

namespace WebApplication6.Models
{
    public class SkorModel
    {
        [Key]
        [Required]
        public int SkorId { get; set; }
        public string Name { get;  set; }
        public string Color { get; set; }
        public string Model { get; set; }
        public int Size { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public int CategoryId { get; set; }
        public CategoryModel Category { get; set; }
    }
}