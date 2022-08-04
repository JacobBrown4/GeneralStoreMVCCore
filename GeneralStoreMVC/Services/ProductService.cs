using GeneralStoreMVC.Data;
using GeneralStoreMVC.Models.Product;
using Microsoft.EntityFrameworkCore;

namespace GeneralStoreMVC.Services
{
    public class ProductService : IProductService
    {
        private readonly GeneralStoreDbContext _context;
        public ProductService(GeneralStoreDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateProduct(ProductCreate model)
        {
            if (model == null) return false;

            _context.Products.Add(new Product
            {
                Name = model.Name,
                Price = model.Price,
                QuantityInStock = model.QuantityInStock
            });

            if (await _context.SaveChangesAsync() == 1)
                return true;
            return false;
        }


        public async Task<ProductDetail> GetProductById(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product is null)
            {
                return null;
            }

            return new ProductDetail
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                QuantityInStock = product.QuantityInStock
            };
        }

        public async Task<IEnumerable<ProductListItem>> GetAllProducts()
        {
            var products = await _context.Products.Select(product => new ProductListItem
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                QuantityInStock = product.QuantityInStock
            }).ToListAsync();
            return products;
        }

        public async Task<bool> UpdateProduct(ProductEdit model)
        {
            var product = await _context.Products.FindAsync(model.Id);

            if (product is null) return false;

            product.Name = model.Name;
            product.QuantityInStock = model.QuantityInStock;
            product.Price = model.Price;

            if (await _context.SaveChangesAsync() == 1)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteProduct(int productId)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product is null) return false;

            _context.Products.Remove(product);
            if (await _context.SaveChangesAsync() == 1)
            {
                return true;
            }
            return false;
        }
    }
}
