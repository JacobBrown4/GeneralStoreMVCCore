using System.ComponentModel.DataAnnotations;

namespace GeneralStoreMVC.Models.Product
{
    public class ProductEdit
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name ="Quantity In Stock")]
        public int QuantityInStock { get; set; }
        [Required]
        public double Price { get; set; }
        [Display(Name ="Product Category")]
        public ProductType ProductCategory { get; set; }

    }
}
