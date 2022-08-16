using System.ComponentModel.DataAnnotations;

namespace GeneralStoreMVC.Models.Product
{
    
    public class ProductCreate
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Quantity In Stock")]
        public int QuantityInStock { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        [Display(Name = "Product Category")]
        public ProductType ProductCategory { get; set; }
    }
}
