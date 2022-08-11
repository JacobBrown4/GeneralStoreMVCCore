using System.ComponentModel.DataAnnotations;

namespace GeneralStoreMVC.Models.Product
{
    public enum ProductType
    {
        Unset = 0,
        Electronic,
        Grocery,
        Clothing,
        Appliance,
        Furniture,
        Office,
        Home,
        Tool,
        Toy,
        Garden,
        Care,
        Beauty,
        Health,
        Sports,
        Outdoor,
        Automotive,
        Pet,
        Industrial,
        Craft,
        Media,
    }
    public class ProductCreate
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int QuantityInStock { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public ProductType ProductType { get; set; }
    }
}
