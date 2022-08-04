using GeneralStoreMVC.Models.Customer;
using GeneralStoreMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace GeneralStoreMVC.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _service;
        public CustomerController(ICustomerService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var customers = await _service.ListCustomers();
            return View(customers);
        }

        public async Task<IActionResult> Details(int id)
        {
            var customer = await _service.GetCustomerById(id);
            if (customer == null)
                return NotFound();

            return View(customer);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerCreate model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMsg"] = "Model State is Invalid";
            }

            bool wasCreated = await _service.CreateCustomer(model);

            if (!wasCreated)
                return RedirectToAction(nameof(Index));

            TempData["ErrorMsg"] = "Unable to save to database. Please try again later.";
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditAsync(int id)
        {
            CustomerDetail customer = await _service.GetCustomerById(id);

            if (customer == null) return NotFound();

            var customerEdit = new CustomerEdit
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email
            };
            return View(customerEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CustomerEdit model)
        {
            if (id != model.Id || !ModelState.IsValid)
                return View(ModelState);

            bool wasUpdated = await _service.UpdateCustomer(model);
            if (wasUpdated) return RedirectToAction(nameof(Index));

            ViewData["ErrorMsg"] = "Unable to save to the database. Please try again later.";
            return View(model);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var customer = _service.GetCustomerById(id);
            if (customer == null) return NotFound();
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(CustomerDetail model)
        {
            if (await _service.DeleteCustomer(model.Id))
                return RedirectToAction(nameof(Index));
            else
                return BadRequest();
        }
    }
}
