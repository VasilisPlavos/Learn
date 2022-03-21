using System.Collections.Generic;
using System.Threading.Tasks;
using Example.Cloudon.API.Databases;
using Example.Cloudon.API.Entities;
using Example.Cloudon.API.Repository;

namespace Example.Cloudon.API.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsAsync(List<int> productIds);
        Task<Product> GetProductAsync(int productId);
        Task<bool> UpdateAsync(Product product);
        Task<Product> AddAsync(Product product);
        Task<Product> DeleteAsync(int productId);
    }

    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepo;

        public ProductService(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        public async Task<List<Product>> GetProductsAsync(List<int> productIds)
        {
            return await _productRepo.GetProductsAsync(productIds);
        }

        public async Task<Product> GetProductAsync(int productId)
        {
            return await _productRepo.GetProductAsync(productId);
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            return await _productRepo.UpdateAsync(product);
        }

        public async Task<Product> AddAsync(Product product)
        {
            return await _productRepo.AddAsync(product);
        }

        public async Task<Product> DeleteAsync(int productId)
        {
            return await _productRepo.DeleteAsync(productId);
        }
    }
}