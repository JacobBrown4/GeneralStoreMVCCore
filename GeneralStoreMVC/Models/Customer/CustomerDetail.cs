using GeneralStoreMVC.Models.Product;

namespace GeneralStoreMVC.Models.Customer
{
    public class CustomerDetail
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<ProductPurchase> Purchases { get; set; } = new List<ProductPurchase>();
    }
}
