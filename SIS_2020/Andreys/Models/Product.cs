namespace Andreys.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MinLength(4), MaxLength(10)]
        public string Name { get; set; }

        [MaxLength(10)]
        public string Description { get; set; }

        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public ProductCategory Category { get; set; }
        public Gender Gender { get; set; }

    }
}