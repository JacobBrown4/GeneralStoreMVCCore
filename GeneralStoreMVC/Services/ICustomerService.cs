using GeneralStoreMVC.Models.Customer;

namespace GeneralStoreMVC.Services
{
    public interface ICustomerService
    {
        Task<bool> CreateCustomer(CustomerCreate model);
        Task<IEnumerable<CustomerListItem>> GetAllCustomers();
        Task<CustomerDetail> GetCustomerById(int customerId);
        Task<bool> UpdateCustomer(CustomerEdit model);
        Task<bool> DeleteCustomer(int customerId);
    }
}
