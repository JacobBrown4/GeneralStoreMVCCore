using System.ComponentModel.DataAnnotations;

namespace GeneralStoreMVC.Models.Product
{
    public class ProductEdit
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int QuantityInStock { get; set; }
        [Required]
        public double Price { get; set; }

    }
}
