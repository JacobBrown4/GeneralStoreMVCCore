using System.ComponentModel.DataAnnotations;

namespace GeneralStoreMVC.Models.Customer
{
    public class CustomerCreate
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
