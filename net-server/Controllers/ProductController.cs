using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using net_server.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace net_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly GegeDbContext _context;

        public ProductController(GegeDbContext context)
        {
            _context = context;
        }

        // 查询所有分类以及该分下的产品
        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<ProductsCategory>>> GetCategories()
        {
            var categories = await _context.ProductsCategories
                .Include(c => c.Products)
                .Select(c => new
                {
                    c.CategoryId,
                    c.CategoryName,
                    Products = c.Products.Select(p => new
                    {
                        p.ProductId,
                        p.ProductName,
                        p.Subtitle,
                        p.Description,
                        p.ProductImage
                    }).ToList()
                })
                .ToListAsync();

            return Ok(new { data = categories });
        }

        // 新建分类
        [HttpPost("categories")]
        public async Task<ActionResult<ProductsCategory>> CreateCategory(ProductsCategory category)
        {
            _context.ProductsCategories.Add(category);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCategories), new { id = category.CategoryId }, new { data = category });
        }

        // 删除分类
        [HttpDelete("categories/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.ProductsCategories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.ProductsCategories.Remove(category);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // 搜索分类
        [HttpGet("categories/search")]
        public async Task<ActionResult<IEnumerable<ProductsCategory>>> SearchCategories(string name)
        {
            var categories = await _context.ProductsCategories
                .Where(c => c.CategoryName.Contains(name))
                .Select(c => new ProductsCategory
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName
                })
                .ToListAsync();

            return Ok(new { data = categories });
        }

        // 分页查询产品列表
        [HttpGet("products")]
        public async Task<ActionResult> GetProducts(int pageNumber = 1, int pageSize = 10)
        {
            var totalRecords = await _context.Products.CountAsync();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            var products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Productattributes)
                .Include(p => p.Productimages)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var response = new
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalRecords = totalRecords,
                Data = products
            };

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            return new JsonResult(new { data = response }, options);
        }

        // 查询某个产品详情
        [HttpGet("products/{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Productattributes)
                .Include(p => p.Productimages)
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            return new JsonResult(new { data = product }, options);
        }
    }
}

