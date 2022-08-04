using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GeneralStoreMVC.Models.Transaction
{
    public class TransactionCreate
    {

        [Required]
        public int ProductId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int Quantity { get; set; }

        public IEnumerable<SelectListItem> ProductOptions { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> CustomerOptions { get; set; } = new List<SelectListItem>();
    }
}
