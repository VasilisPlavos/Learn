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
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<Product>> GetProductsAsync(List<int> productIds)
        {
            return await _productRepository.GetProductsAsync(productIds);
        }

        public async Task<Product> GetProductAsync(int productId)
        {
            return await _productRepository.GetProductAsync(productId);
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            return await _productRepository.UpdateAsync(product);
        }

        public async Task<Product> AddAsync(Product product)
        {
            return await _productRepository.AddAsync(product);
        }

        public async Task<Product> DeleteAsync(int productId)
        {
            return await _productRepository.DeleteAsync(productId);
        }
    }
}