using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using courses_site_api.models;

namespace courses_site_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Courses_videosController : ControllerBase
    {
        private readonly courses_entitiy _context;

        public Courses_videosController(courses_entitiy context)
        {
            _context = context;
        }

        // GET: api/Courses_videos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Courses_videos>>> Getcourses_Videos()
        {
            return await _context.courses_Videos.ToListAsync();
        }

        // GET: api/Courses_videos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Courses_videos>> GetCourses_videos(int id)
        {
            var courses_videos = await _context.courses_Videos.FindAsync(id);

            if (courses_videos == null)
            {
                return NotFound();
            }
            else
            {

                return Ok(courses_videos);
            }
        }
        [Route("[action]/{id}")]
        [HttpGet()]
        public async Task<ActionResult<Courses_videos>> Get_videos(int id)
        {
            ////var courses_videos = await _context.courses_Videos.FindAsync(id);
            var courses_videos = await _context.courses_Videos.Where(e => e.Courses_Categoryid == id).ToListAsync();

            if (courses_videos == null)
            {
                return NotFound();
            }
            else
            {

                return Ok(courses_videos);
            }
        }
        // PUT: api/Courses_videos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourses_videos(int id, Courses_videos courses_videos)
        {
            if (id != courses_videos.id)
            {
                return BadRequest();
            }

            _context.Entry(courses_videos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Courses_videosExists(id))
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

        // POST: api/Courses_videos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Courses_videos>> PostCourses_videos(Courses_videos courses_videos)
        {
            _context.courses_Videos.Add(courses_videos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourses_videos", new { id = courses_videos.id }, courses_videos);
        }

        // DELETE: api/Courses_videos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourses_videos(int id)
        {
            var courses_videos = await _context.courses_Videos.FindAsync(id);
            if (courses_videos == null)
            {
                return NotFound();
            }

            _context.courses_Videos.Remove(courses_videos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Courses_videosExists(int id)
        {
            return _context.courses_Videos.Any(e => e.id == id);
        }
    }
}
