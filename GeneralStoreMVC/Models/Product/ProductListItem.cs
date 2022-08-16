using System.ComponentModel.DataAnnotations;

namespace GeneralStoreMVC.Models.Product
{
    public class ProductListItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Display(Name = "Quantity In Stock")]
        public int QuantityInStock { get; set; }
        public double Price { get; set; }
    }
}
