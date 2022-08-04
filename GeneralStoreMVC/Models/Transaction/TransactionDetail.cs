namespace GeneralStoreMVC.Models.Transaction
{
    public class TransactionDetail
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Product { get; set; }
        public int CustomerId { get; set; }
        public string Customer { get; set; }
        public int Quantity { get; set; }
        public DateTime DateOfTransaction { get; set; }
    }
}
