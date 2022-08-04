using GeneralStoreMVC.Data;
using GeneralStoreMVC.Models.Customer;
using Microsoft.EntityFrameworkCore;

namespace GeneralStoreMVC.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly GeneralStoreDbContext _context;
        public CustomerService(GeneralStoreDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateCustomer(CustomerCreate model)
        {
            if (model == null) return false;

            _context.Customers.Add(new Customer
            {
                Name = model.Name,
                Email = model.Email
            });

            if (await _context.SaveChangesAsync() == 1)
                return true;
            return false;
        }


        public async Task<CustomerDetail> GetCustomerById(int customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer is null)
            {
                return null;
            }

            return new CustomerDetail
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email
            };
        }

        public async Task<IEnumerable<CustomerListItem>> ListCustomers()
        {
            var customers = await _context.Customers.Select(customer => new CustomerListItem
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email
            }).ToListAsync();
            return customers;
        }

        public async Task<bool> UpdateCustomer(CustomerEdit model)
        {
            var customer = await _context.Customers.FindAsync(model.Id);

            if (customer is null) return false;

            customer.Name = model.Name;
            customer.Email = model.Email;

            if (await _context.SaveChangesAsync() == 1)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteCustomer(int customerId)
        {
            var customer = _context.Customers.Find(customerId);

            if (customer is null) return false;

            _context.Customers.Remove(customer);
            if (await _context.SaveChangesAsync() == 1)
            {
                return true;
            }
            return false;
        }
    }
}
