﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Example.Cloudon.API.Entities;
using Example.Cloudon.API.Services;

namespace Example.Cloudon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Get individual product
        /// </summary>
        /// <param name="id"> The id of the product </param>
        /// <returns></returns>
        // GET: api/Products/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productService.GetProductAsync(id);
            if (product == null) return NotFound();
            return product;
        }

        /// <summary>
        /// Update individual product
        /// </summary>
        /// <param name="id"> The id of the product </param>
        /// <param name="product"></param>
        /// <returns></returns>
        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id) return BadRequest();
            await _productService.UpdateAsync(product);
            return NoContent();
        }

        /// <summary>
        /// Create Individual product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            return await _productService.AddAsync(product);
        }

        /// <summary>
        /// Delete individual product
        /// </summary>
        /// <param name="id"> The id of the product </param>
        /// <returns></returns>
        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var deletedProduct = await _productService.DeleteAsync(id);
            if (deletedProduct == null) return NotFound();
            return NoContent();
        }
    }
}