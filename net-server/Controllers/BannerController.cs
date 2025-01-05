using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using net_server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;

namespace net_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]   
    public class BannerController : Controller
    {
        private readonly GegeDbContext _context;

        public BannerController(GegeDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 获取所有 Banner
        /// </summary>
        /// <returns>返回所有 Banner 的列表</returns>
        [HttpGet]
        [SwaggerOperation(Summary = "获取所有 Banner", Description = "返回所有 Banner 的列表")]
        [SwaggerResponse(200, "成功获取 Banner 列表", typeof(IEnumerable<Banner>))]
        public async Task<ActionResult<IEnumerable<Banner>>> GetBanners()
        {
            var banners = await _context.Banners.ToListAsync();
            return Ok(new { data = banners });
        }

        /// <summary>
        /// 上传一个新的 Banner
        /// </summary>
        /// <param name="banner">Banner 对象</param>
        /// <returns>返回创建的 Banner 对象</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "上传一个新的 Banner", Description = "上传一个新的 Banner 对象")]
        [SwaggerResponse(201, "成功创建 Banner", typeof(Banner))]
        [SwaggerResponse(400, "Banner 对象为空")]
        public async Task<ActionResult<Banner>> UploadBanner([FromBody] Banner banner)
        {
            if (banner == null)
            {
                return BadRequest("Banner is null.");
            }

            _context.Banners.Add(banner);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBanners), new { id = banner.Id }, banner);
        }

        /// <summary>
        /// 删除指定的 Banner
        /// </summary>
        /// <param name="id">Banner 的 ID</param>
        /// <returns>返回删除结果</returns>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "删除指定的 Banner", Description = "根据 ID 删除指定的 Banner")]
        [SwaggerResponse(200, "成功删除 Banner")]
        [SwaggerResponse(404, "未找到指定的 Banner")]
        public async Task<IActionResult> DeleteBanner(int id)
        {
            var banner = await _context.Banners.FindAsync(id);
            if (banner == null)
            {
                return NotFound();
            }

            _context.Banners.Remove(banner);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
