using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Example.Cloudon.API.Databases;
using Example.Cloudon.API.Dtos;
using Example.Cloudon.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Example.Cloudon.API.Repository
{
    public interface IProductRepository
    {
        Task<Product> AddAsync(Product product);
        Task<List<Product>> AddAsync(List<Product> products);
        Task<Product> DeleteAsync(int productId);
        Task<List<Product>> GetAllProductsAsync();
        Task<List<Product>> GetProductsAsync(List<int> productIds);
        Task<List<Product>> GetSoftOneProductsAsync(List<string> productCodes, string source);
        Task<bool> UpdateAsync(Product product);
        Task<bool> UpdateAsync(List<Product> products);

        Task<Product> SaveOrUpdateAsync(Product product);
    }

    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Product> AddAsync(Product product)
        {
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task<List<Product>> AddAsync(List<Product> products)
        {
            _db.Products.AddRange(products);
            await _db.SaveChangesAsync();
            return products;
        }

        public async Task<Product> DeleteAsync(int productId)
        {
            var product = await GetProductAsync(productId);
            if (product == null) return null;

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _db.Products.ToListAsync();
        }
        private async Task<Product> GetProductAsync(int productId)
        {
            var productIds = new List<int>() { productId };
            var products = await GetProductsAsync(productIds);
            return products.FirstOrDefault();
        }
        public async Task<List<Product>> GetProductsAsync(List<int> productIds)
        {
            return await _db.Products.Where(x => productIds.Contains(x.Id)).ToListAsync();
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            _db.Products.Update(product);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(List<Product> products)
        {
            _db.Products.UpdateRange(products);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<Product> SaveOrUpdateAsync(Product product)
        {
            if (_db.Products.Any(x => x.Id == product.Id))
            {
                await UpdateAsync(product);
                return product;
            }
            else
            {
                return await AddAsync(product);
            }
        }

        public async Task<List<Product>> GetSoftOneProductsAsync(List<string> productCodes, string source)
        {
            var products = await _db.Products.Where(x => productCodes.Contains(x.Code) || x.Source == source).ToListAsync();
            return products;
        }
    }
}