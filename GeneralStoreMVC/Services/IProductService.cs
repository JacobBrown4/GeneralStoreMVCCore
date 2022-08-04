using GeneralStoreMVC.Models.Product;

namespace GeneralStoreMVC.Services
{
    public interface IProductService
    {
        Task<bool> CreateProduct(ProductCreate model);
        Task<IEnumerable<ProductListItem>> GetAllProducts();
        Task<ProductDetail> GetProductById(int productId);
        Task<bool> UpdateProduct(ProductEdit model);
        Task<bool> DeleteProduct(int productId);
    }
}
