using System.ComponentModel.DataAnnotations;

namespace TalabatAPIs.DTOs
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        [Range(0.1 , double.MaxValue , ErrorMessage ="Price can NOT be Zero")]
        public decimal Price { get; set; }
        [Required]
        [Range(1,int.MaxValue , ErrorMessage = "Must Be At Least One item ")]
        public int Quantity { get; set; }
    }
}