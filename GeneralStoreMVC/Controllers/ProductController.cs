using GeneralStoreMVC.Models.Product;
using GeneralStoreMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace GeneralStoreMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _service;
        public ProductController(IProductService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _service.GetAllProducts();
            return View(products);
        }


        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,QuantityInStock,Price")]ProductCreate model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMsg"] = "Model State is Invalid";
                return View(model);
            }

            if (await _service.CreateProduct(model))
                return RedirectToAction(nameof(Index));

            TempData["ErrorMsg"] = "Unable to save to database. Please try again later.";
            return View(model);
        }
        public async Task<IActionResult> Details(int id)
        {
            var product = await _service.GetProductById(id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ProductDetail product = await _service.GetProductById(id);

            if (product == null) return NotFound();

            var productEdit = new ProductEdit
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                QuantityInStock = product.QuantityInStock
            };
            return View(productEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductEdit model)
        {
            if (id != model.Id || !ModelState.IsValid)
                return View(ModelState);

            bool wasUpdated = await _service.UpdateProduct(model);
            if (wasUpdated) return RedirectToAction(nameof(Index));

            ViewData["ErrorMsg"] = "Unable to save to the database. Please try again later.";
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _service.GetProductById(id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ProductDetail model)
        {
            if (await _service.DeleteProduct(model.Id))
                return RedirectToAction(nameof(Index));
            else
                return BadRequest();
        }
    }
}
