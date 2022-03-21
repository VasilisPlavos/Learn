using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Example.Cloudon.API.Databases;
using Example.Cloudon.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Example.Cloudon.API.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProductsAsync(List<int> productIds);
        Task<Product> GetProductAsync(int productId);
        Task<bool> UpdateAsync(Product product);
        Task<Product> AddAsync(Product product);
        Task<Product> DeleteAsync(int productId);
    }

    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<List<Product>> GetProductsAsync(List<int> productIds)
        {
            return await _db.Products.Where(x => productIds.Contains(x.Id)).ToListAsync();
        }

        public async Task<Product> GetProductAsync(int productId)
        {
            var productIds = new List<int>() { productId };
            var products = await GetProductsAsync(productIds);
            return products.FirstOrDefault();
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            _db.Products.Update(product);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<Product> AddAsync(Product product)
        {
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task<Product> DeleteAsync(int productId)
        {
            var product = await GetProductAsync(productId);
            if (product == null) return null;

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            return product;
        }
    }
}