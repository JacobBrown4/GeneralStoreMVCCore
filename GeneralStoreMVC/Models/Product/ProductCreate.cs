using System.ComponentModel.DataAnnotations;

namespace GeneralStoreMVC.Models.Product
{
    public class ProductCreate
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int QuantityInStock { get; set; }
        [Required]
        public double Price { get; set; }
    }
}
