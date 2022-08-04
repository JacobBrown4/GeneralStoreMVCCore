using System.ComponentModel.DataAnnotations;

namespace GeneralStoreMVC.Models.Customer
{
    public class CustomerEdit
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
