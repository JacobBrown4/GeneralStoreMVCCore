using System.ComponentModel.DataAnnotations;

namespace GeneralStoreMVC.Models.Transaction
{
    public class TransactionListItem
    {
        public int Id { get; set; }
        public string Product { get; set; }
        public string Customer { get; set; }
        public int Quantity { get; set; }
        [DisplayFormat(DataFormatString="{0:d}")]
        public DateTime DateOfTransaction { get; set; }
    }
}
