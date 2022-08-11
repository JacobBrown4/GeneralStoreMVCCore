using GeneralStoreMVC.Models.Transaction;
using System.ComponentModel.DataAnnotations;

namespace GeneralStoreMVC.Models.Product
{
    public class ProductDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Display(Name = "Quantity In Stock")]
        public int QuantityInStock { get; set; }
        public double Price { get; set; }
        [Display(Name = "Product Type")]
        public ProductType ProductType {get; set;}
        public List<TransactionHistory> TransactionHistories = new List<TransactionHistory>();
    }
}
