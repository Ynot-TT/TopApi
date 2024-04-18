using AutoMapper;
using TopStyle.Domain.Entities;
using TopStyleApi.Core.Interfaces;
using TopStyleApi.Data.Interfaces;
using TopStyleApi.Data.Repos;
using TopStyleApi.Domain.DTO;

namespace TopStyleApi.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _repo;
        private readonly IMapper _mapper;

        public ProductService(IProductRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task AddProductAsync(ProductDTO productDTO)
        {
            var product = _mapper.Map<Product>(productDTO);
            await _repo.AddProductAsync(product);
        }
      
        public async Task<IEnumerable<ProductDTO>> GetProductByTitleAsync(string title)
        {
            var products = await _repo.GetProductByTitleAsync(title);

            var productDTOs = new List<ProductDTO>();

            foreach (var product in products)
            {
                var productDTO = new ProductDTO
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Description = product.Description,
                    Price = product.Price + " kr",
                    CategoryId = product.CategoryId,
                };
                productDTOs.Add(productDTO);
            }
            return productDTOs;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            var products = await _repo.GetAllProductsAsync();
            var productDTOs = new List<ProductDTO>();

            foreach (var product in products)
            {
                var productDTO = new ProductDTO
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Description = product.Description,
                    Price = product.Price + " kr",
                    CategoryId = product.CategoryId,
                };
                productDTOs.Add(productDTO);
            }
            return productDTOs; 
        }

        public async Task DeleteProductAsync(int productId, int userId)
        {
            await _repo.DeleteProductAsync(productId, userId);
        }

        public async Task<ProductDTO> GetProductByIdAsync(int productId)
        {
            var product = await _repo.GetProductByIdAsync(productId);
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<int?> GetProductPrice(int productId)
        {
            return await _repo.GetProductPrice(productId);
        }

        public async Task UpdateProductAsync(ProductDTO product)
        {
            var updateProduct = _mapper.Map<Product>(product);
            await _repo.UpdateProductAsync(updateProduct);
        }
    }
}
