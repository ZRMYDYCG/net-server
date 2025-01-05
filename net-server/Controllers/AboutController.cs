using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using net_server.Models;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;

namespace net_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AboutController : Controller
    {
        private readonly GegeDbContext _context;

        public AboutController(GegeDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 获取公司信息
        /// </summary>
        /// <returns>返回公司信息</returns>
        [HttpGet("company-info")]
        [SwaggerOperation(Summary = "获取公司信息", Description = "返回公司信息")]
        [SwaggerResponse(200, "成功获取公司信息", typeof(About))]
        public async Task<ActionResult<About>> GetCompanyInfo()
        {
            var companyInfo = await _context.Abouts.FirstOrDefaultAsync();
            if (companyInfo == null)
            {
                return NotFound("Company information not found.");
            }
            return Ok(new { data = companyInfo });
        }

        /// <summary>
        /// 更新公司信息
        /// </summary>
        /// <param name="updatedInfo">更新后的公司信息</param>
        /// <returns>返回更新结果</returns>
        [HttpPut("company-info")]
        [SwaggerOperation(Summary = "更新公司信息", Description = "更新公司信息")]
        [SwaggerResponse(204, "成功更新公司信息")]
        [SwaggerResponse(400, "更新信息为空")]
        public async Task<ActionResult> UpdateCompanyInfo([FromBody] About updatedInfo)
        {
            if (updatedInfo == null)
            {
                return BadRequest("Updated information is null.");
            }

            var companyInfo = await _context.Abouts.FirstOrDefaultAsync();
            if (companyInfo == null)
            {
                return NotFound("Company information not found.");
            }

            companyInfo.CompanyIntroduction = updatedInfo.CompanyIntroduction;
            companyInfo.ProductCount = updatedInfo.ProductCount;
            companyInfo.EmployeeCount = updatedInfo.EmployeeCount;
            companyInfo.WorkshopCount = updatedInfo.WorkshopCount;

            _context.Abouts.Update(companyInfo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}


