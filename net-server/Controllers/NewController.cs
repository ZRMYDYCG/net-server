using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using net_server.Models;
using System.Threading.Tasks;

namespace net_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewController : Controller
    {
        private readonly GegeDbContext _context;

        public NewController(GegeDbContext context)
        {
            _context = context;
        }

        // GET: api/New?pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetNews(int pageNumber = 1, int pageSize = 10)
        {
            var news = await _context.News
                .OrderBy(n => n.Date)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(news);
        }

        // GET: api/New/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNew(int id)
        {
            var newsItem = await _context.News.FindAsync(id);

            if (newsItem == null)
            {
                return NotFound();
            }

            return Ok(newsItem);
        }
    }
}
