using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using TopStyle.Data;
using TopStyle.Domain.Entities;
using TopStyleApi.Data.Interfaces;

namespace TopStyleApi.Data.Repos
{
    public class ProductRepo : IProductRepo
    {
        private readonly TopStyleContext _context;

        public ProductRepo(TopStyleContext context)
        {
            _context = context;
        }

        public async Task AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int productId, int userId)
        {
            var productToDelete = await _context.Products.FindAsync(productId);
            if (productToDelete != null) 
            {
                _context.Products.Remove(productToDelete); 
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetOwnProductsAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await _context.Products.FindAsync(productId);
        }

        public async Task<IEnumerable<Product>> GetProductByTitleAsync(string title)
        {
            return await _context.Products
                .Where(r => r.ProductName.Contains(title))
                .ToListAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            var existingProduct = await _context.Products.FindAsync(product.ProductId);

            if (existingProduct != null)
            {
                existingProduct.ProductName = product.ProductName;
                existingProduct.Description = product.Description;
                existingProduct.Price = product.Price;
                existingProduct.CategoryId = product.CategoryId;    
            }
            await _context.SaveChangesAsync();
        }
    }
}
