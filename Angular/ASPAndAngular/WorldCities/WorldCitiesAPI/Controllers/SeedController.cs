using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorldCitiesAPI.Data;

namespace WorldCitiesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SeedController(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
