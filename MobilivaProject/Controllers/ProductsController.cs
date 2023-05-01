using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using MobilivaProject.Data;
using MobilivaProject.DTOs;
using MobilivaProject.Entities;
using MobilivaProject.Models;


namespace MobilivaProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<ProductsController> _logger;

        const string catCacheKey = "catProductKey";
        const string cacheKey = "productKey";

        public ProductsController(DataContext context, IMapper mapper, IMemoryCache memoryCache, ILogger<ProductsController> logger)
        {
            _context = context;
            _mapper = mapper;
            _memoryCache = memoryCache;
            _logger = logger;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ApiResponse<List<ProductDTO>>> GetProducts()
        {
            ApiResponse<List<ProductDTO>> apiResponse = new ApiResponse<List<ProductDTO>>();

            if (_context.Products == null)
            {
                return apiResponse.Failed("Data not found", HttpStatusCode.NotFound);
            }

            if (!_memoryCache.TryGetValue(cacheKey, out List<ProductDTO> productList))
            {
                _logger.Log(LogLevel.Information, "Product list not found in cache. Fetching from database.");

                var products = await _context.Products.ToListAsync();


                if (!products.Any())
                {
                    return apiResponse.Failed("Data not found", HttpStatusCode.NotFound);
                }
                var productsToReturn = _mapper.Map<List<ProductDTO>>(products);

                var cacheExpOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                    Priority = CacheItemPriority.Normal
                };

                _memoryCache.Set(cacheKey, productsToReturn, cacheExpOptions);

                return apiResponse.Success(productsToReturn);

            }
            else
            {
                _logger.Log(LogLevel.Information, "Employee list found in cache.");
                return apiResponse.Success(productList);
            }
        }

        [HttpGet("{Category}")]
        public async Task<ApiResponse<List<ProductDTO>>> GetProducts(string Category)
        {
            ApiResponse<List<ProductDTO>> apiResponse = new ApiResponse<List<ProductDTO>>();


            if (!_memoryCache.TryGetValue(catCacheKey, out List<ProductDTO> productList))
            {
                _logger.Log(LogLevel.Information, "Product list not found in cache. Fetching from database.");

                var products = await _context.Products.Where(a => a.Category == Category).ToListAsync();


                if (!products.Any())
                {
                    return apiResponse.Failed("Data not found", HttpStatusCode.NotFound);
                }
                var productsToReturn = _mapper.Map<List<ProductDTO>>(products);

                var cacheExpOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                    Priority = CacheItemPriority.Normal
                };

                _memoryCache.Set(catCacheKey, productsToReturn, cacheExpOptions);

                return apiResponse.Success(productsToReturn);

            }
            else
            {
                _logger.Log(LogLevel.Information, "Product list found in cache.");
                return apiResponse.Success(productList);
            }

        }


        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'DataContext.Products'  is null.");
            }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
