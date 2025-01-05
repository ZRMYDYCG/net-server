using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using net_server.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace net_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobController : ControllerBase
    {
        private readonly GegeDbContext _context;

        public JobController(GegeDbContext context)
        {
            _context = context;
        }

        [HttpGet("listings")]
        public async Task<ActionResult<object>> GetJobListings(int pageNumber = 1, int pageSize = 10)
        {
            var jobListings = await _context.Jobs
                .OrderBy(j => j.JobId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(j => new { j.JobTitle, j.JobId })
                .ToListAsync();

            var response = new
            {
                data = jobListings
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Job>> GetJobById(int id)
        {
            var job = await _context.Jobs.FindAsync(id);

            if (job == null)
            {
                return NotFound();
            }

            return Ok(job);
        }
    }
}
