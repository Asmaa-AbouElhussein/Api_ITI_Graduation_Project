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
    public class Course_detailesController : ControllerBase
    {
        private readonly courses_entitiy _context;

        public Course_detailesController(courses_entitiy context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course_detailes>>> Getcourse_Detailes()
        {
            return await _context.course_Detailes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Course_detailes>> GetCourse_detailes(int id)
        {
            var course_detailes = await _context.course_Detailes.FindAsync(id);

            if (course_detailes == null)
            {
                return NotFound();
            }

            return course_detailes;
        }

        [HttpGet("Getids")]
        public IActionResult Getcourses_ids()
        {
            List<Course_detailes> emps = _context.course_Detailes.ToList();
            List<int> ids = emps.Select(e => e.id).ToList();
            return Ok(ids);
        }
       
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse_detailes(int id, Course_detailes course_detailes)
        {
            if (id != course_detailes.id)
            {
                return BadRequest();
            }

            _context.Entry(course_detailes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Course_detailesExists(id))
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

    
        [HttpPost]
        public async Task<ActionResult<Course_detailes>> PostCourse_detailes(Course_detailes course_detailes)
        {
            _context.course_Detailes.Add(course_detailes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourse_detailes", new { id = course_detailes.id }, course_detailes);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse_detailes(int id)
        {
            var course_detailes = await _context.course_Detailes.FindAsync(id);
            if (course_detailes == null)
            {
                return NotFound();
            }

            _context.course_Detailes.Remove(course_detailes);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Course_detailesExists(int id)
        {
            return _context.course_Detailes.Any(e => e.id == id);
        }
    }
}
