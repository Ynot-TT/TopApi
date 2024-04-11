using TopStyle.Domain.Entities;

namespace TopStyleApi.Data.Interfaces
{
    public interface IOrderItemRepo
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int productId);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int productId, int userId);
    }
}
