using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Example.Cloudon.API.Dtos;
using Example.Cloudon.API.Entities;
using Example.Cloudon.API.Helpers;
using Example.Cloudon.API.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MediaTypeHeaderValue = Microsoft.Net.Http.Headers.MediaTypeHeaderValue;

namespace Example.Cloudon.API.Services
{
    public interface IProductService
    {
        Task<Product> AddAsync(Product product);
        Task<Product> DeleteAsync(int productId);
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetProductAsync(int productId);
        Task<bool> SoftoneSyncAsync();
        Task<bool> UpdateAsync(Product product);
    }

    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepo;

        public ProductService(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        public async Task<Product> AddAsync(Product product)
        {
            return await _productRepo.AddAsync(product);
        }

        public async Task<Product> DeleteAsync(int productId)
        {
            return await _productRepo.DeleteAsync(productId);
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _productRepo.GetAllProductsAsync();
        }

        public async Task<Product> GetProductAsync(int productId)
        {
            var productsIds = new List<int>() { productId };
            var products = await GetProductsAsync(productsIds);
            return products.FirstOrDefault();
        }

        private async Task<List<Product>> GetProductsAsync(List<int> productIds)
        {
            return await _productRepo.GetProductsAsync(productIds);
        }
        public async Task<bool> SoftoneSyncAsync()
        {
            // API response had issues, so I hardcode the data as stringify json to proceed 
            // var client = new HttpClient();
            // var request = new HttpRequestMessage(HttpMethod.Get, "https://cloudonapi.oncloud.gr/s1services/JS/updateItems/cloudOnTest");
            // Encoding.RegisterProvider(CodePagesEncodingProvider.Instance); // using this to support charset=windows-1253
            //
            // var response = await client.SendAsync(request);
            //
            // // Unfortunately it returns something like: ¶§*•r\u008azI¥\u009ezθ¬!\u0016±-¥UΘ\t³ΓΞ
            // var obj = await response.Content.ReadAsStringAsync();

            var obj = StringHelper.ProductsContentAsString();

            JObject jObject = JObject.Parse(obj);
            var productsJtoken = jObject["data"];
            var productsDtos = ProductHelper.GetProductsDtos(productsJtoken);

            var source = productsDtos.FirstOrDefault()?.Source;
            var productCodes = productsDtos.Select(x => x.Code).ToList();
            var savedProducts = await _productRepo.GetSoftOneProductsAsync(productCodes, source);
            var productsToSave = new List<Product>();
            var now = DateTime.UtcNow;
            foreach (var dto in productsDtos)
            {
                var p = savedProducts.FirstOrDefault(x => x.Code == dto.Code) ?? new Product()
                {
                    Code = dto.Code,
                    Source = dto.Source,
                    Barcode = dto.Barcode,
                    DateCreated = now,
                };

                p.Name = dto.Name;
                p.Description = dto.Description;
                p.Discount = dto.Discount;
                p.DateUpdated = now;
                p.ExternalId = dto.ExternalId;
                p.RetailPrice = dto.RetailPrice;
                p.WholesalePrice = dto.WholesalePrice;

                productsToSave.Add(p);
            }

            var productsToCreate = productsToSave.Where(x => x.Id == 0).ToList();
            var productsToUpdate = productsToSave.Where(x => x.Id > 0).ToList();

            await _productRepo.UpdateAsync(productsToUpdate);
            await _productRepo.AddAsync(productsToCreate);

            return true;
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            return await _productRepo.UpdateAsync(product);
        }

    }
}