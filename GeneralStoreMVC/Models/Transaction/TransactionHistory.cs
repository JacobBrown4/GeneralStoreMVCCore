namespace GeneralStoreMVC.Models.Transaction
{
    public class TransactionHistory
    {
        public int Quantity { get; set; }
        public DateTime DateOfTransaction { get; set; }
        public double TotalCost { get; set; }
    }
}
