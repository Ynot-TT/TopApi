using TopStyleApi.Domain.DTO;

namespace TopStyleApi.Core.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
        Task<ProductDTO> GetProductByIdAsync(int productId);
        Task AddProductAsync(ProductDTO product);
        Task UpdateProductAsync(ProductDTO product);
        Task DeleteProductAsync(int productId, int userId);
        Task<IEnumerable<ProductDTO>> GetProductByTitleAsync(string title);
    }
}
