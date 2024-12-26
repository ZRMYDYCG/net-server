using Microsoft.AspNetCore.Mvc;
using net_server.Models;
using System.Collections.Generic;
using System.Linq;

namespace net_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private static List<ProductsCategory> categories = new List<ProductsCategory>();
        private static List<Product> products = new List<Product>();

        [HttpGet("categories")]
        public ActionResult GetCategories([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var totalItems = categories.Count;
            var pagedCategories = categories.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var result = new
            {
                TotalItems = totalItems,
                Page = page,
                PageSize = pageSize,
                Items = pagedCategories
            };
            return Ok(result);
        }

        [HttpGet("category/{categoryId}/products")]
        public ActionResult GetProductsByCategory(int categoryId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var productsByCategory = products.Where(p => p.CategoryId == categoryId);
            var totalItems = productsByCategory.Count();
            var pagedProducts = productsByCategory.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var result = new
            {
                TotalItems = totalItems,
                Page = page,
                PageSize = pageSize,
                Items = pagedProducts
            };
            return Ok(result);
        }

        [HttpGet("search")]
        public ActionResult SearchProducts([FromQuery] string? name, [FromQuery] string? subtitle, [FromQuery] string? description, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var query = products.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.ProductName.Contains(name));
            }

            if (!string.IsNullOrEmpty(subtitle))
            {
                query = query.Where(p => p.Subtitle != null && p.Subtitle.Contains(subtitle));
            }

            if (!string.IsNullOrEmpty(description))
            {
                query = query.Where(p => p.Description != null && p.Description.Contains(description));
            }

            var totalItems = query.Count();
            var pagedProducts = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var result = new
            {
                TotalItems = totalItems,
                Page = page,
                PageSize = pageSize,
                Items = pagedProducts
            };
            return Ok(result);
        }

        [HttpPost]
        public ActionResult<Product> UploadProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Product is null.");
            }

            products.Add(product);
            return CreatedAtAction(nameof(GetProductById), new { id = product.ProductId }, product);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var product = products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            products.Remove(product);
            return NoContent();
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProductById(int id)
        {
            var product = products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
    }
}
