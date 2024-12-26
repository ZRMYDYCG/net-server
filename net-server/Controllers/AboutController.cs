using Microsoft.AspNetCore.Mvc;
using net_server.Models;

namespace net_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AboutController : Controller
    {
        private static About companyInfo = new About
        {
            AboutId = 1,
            CompanyIntroduction = "Default Introduction",
            ProductCount = 0,
            EmployeeCount = 0,
            WorkshopCount = 0
        };

        [HttpGet("company-info")]
        public ActionResult<About> GetCompanyInfo()
        {
            return Ok(companyInfo);
        }

        [HttpPut("company-info")]
        public ActionResult UpdateCompanyInfo([FromBody] About updatedInfo)
        {
            if (updatedInfo == null)
            {
                return BadRequest("Updated information is null.");
            }

            companyInfo.CompanyIntroduction = updatedInfo.CompanyIntroduction;
            companyInfo.ProductCount = updatedInfo.ProductCount;
            companyInfo.EmployeeCount = updatedInfo.EmployeeCount;
            companyInfo.WorkshopCount = updatedInfo.WorkshopCount;

            return NoContent();
        }
    }
}
