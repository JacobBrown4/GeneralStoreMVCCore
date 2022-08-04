using System.ComponentModel.DataAnnotations;

namespace GeneralStoreMVC.Models.Transaction
{
    public class TransactionEdit
    {
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public DateTime DateOfTransaction { get; set; }

    }
}
