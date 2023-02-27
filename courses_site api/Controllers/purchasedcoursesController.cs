using courses_site_api.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace courses_site_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class purchasedcoursesController : ControllerBase
    {
        private readonly courses_entitiy _context;

        public purchasedcoursesController(courses_entitiy context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<purchasedcourses>>> GetPurchasedcourses()
        {
            return await _context.purchasedcourses.ToListAsync();  
            
        }
        [Route("[action]/{id}")]
        [HttpGet]
        public async Task<ActionResult<purchasedcourses>> Getpurchased(string id)
        {
            string email = await _context.registrations.Where(o => o.username == id).Select(o => o.email).FirstOrDefaultAsync();
            List<int> purch = await _context.purchasedcourses.Where(o => o.email == email).Select(o => o.courseid).ToListAsync();
            return Ok(purch);
        }

        [HttpPost]
        public async Task<ActionResult<string>> PostPurchasedcourses(purchasedcourses purchased)
        {
            _context.purchasedcourses.Add(purchased);
            await _context.SaveChangesAsync();

            return Ok("تم تفعيل الكورس بنجاح");
        }
    }
}
