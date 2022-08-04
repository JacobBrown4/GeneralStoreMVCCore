using GeneralStoreMVC.Models.Transaction;
using GeneralStoreMVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GeneralStoreMVC.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionService _service;
        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;
        public TransactionController(ITransactionService service, IProductService product, ICustomerService customer)
        {
            _service = service;
            _productService = product;
            _customerService = customer;
        }
        public async Task<IActionResult> Index()
        {
            var transactions = await _service.GetAllTransactions();
            return View(transactions);
        }


        public async Task<IActionResult> Create()
        {
            var products = await _productService.GetAllProducts();
            var customers = await _customerService.GetAllCustomers();
            IEnumerable<SelectListItem> productSelect = products.Select(p => new SelectListItem()
            {
                Text = p.Name,
                Value = p.Id.ToString()
            });
            IEnumerable<SelectListItem> customerSelect = customers.Select(p => new SelectListItem()
            {
                Text = p.Name,
                Value = p.Id.ToString()
            });

            TransactionCreate model = new TransactionCreate();

            model.ProductOptions = productSelect;
            model.CustomerOptions = customerSelect;

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TransactionCreate model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMsg"] = "Model State is Invalid";
                return View(model);
            }

            if (await _service.CreateTransaction(model))
                return RedirectToAction(nameof(Index));

            TempData["ErrorMsg"] = "Unable to save to database. Please try again later.";
            return View(model);
        }
        public async Task<IActionResult> Details(int id)
        {
            var transaction = await _service.GetTransactionById(id);
            if (transaction == null)
                return NotFound();

            return View(transaction);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            TransactionDetail transaction = await _service.GetTransactionById(id);

            if (transaction == null) return NotFound();

            var transactionEdit = new TransactionEdit
            {
                Id = transaction.Id,
                ProductId = transaction.ProductId,
                CustomerId = transaction.CustomerId,
                Quantity = transaction.Quantity,
                DateOfTransaction = transaction.DateOfTransaction
            };
            return View(transactionEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, TransactionEdit model)
        {
            if (id != model.Id || !ModelState.IsValid)
                return View(ModelState);

            bool wasUpdated = await _service.UpdateTransaction(model);
            if (wasUpdated) return RedirectToAction(nameof(Index));

            ViewData["ErrorMsg"] = "Unable to save to the database. Please try again later.";
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var transaction = await _service.GetTransactionById(id);
            if (transaction == null) return NotFound();
            return View(transaction);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(TransactionDetail model)
        {
            if (await _service.DeleteTransaction(model.Id))
                return RedirectToAction(nameof(Index));
            else
                return BadRequest();
        }
    }
}
