using TopStyle.Domain.Entities;

namespace TopStyleApi.Data.Interfaces
{
    public interface IProductRepo
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int productId);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int productId, int userId);
        Task<IEnumerable<Product>> GetProductByTitleAsync(string title);
        Task<int?> GetProductPrice(int productId);
    }
}
