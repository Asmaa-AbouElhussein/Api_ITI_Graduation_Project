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
    public class Courses_categoryController : ControllerBase
    {
        private courses_entitiy _context;

        public Courses_categoryController(courses_entitiy context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Courses_category>>>  Getcourses_Categories()
        {
            List<Courses_category> emps = await _context.courses_Categories.ToListAsync();
            return Ok(emps);
        }


        [HttpGet("Getids")]
        public async Task<ActionResult<Courses_category>> Getcourses_ids()
        {
            List<Courses_category> emps =await _context.courses_Categories.ToListAsync();
            List<int> ids = emps.Select(e => e.id).ToList();
            return Ok(ids);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Courses_category>>  GetCourses_category(int id)
        {
            var courses_category = await _context.courses_Categories.FindAsync(id);

            if (courses_category == null)
            {
                return NotFound();
            }

            return courses_category;
        }

       
        [HttpPut("{id}")]
        public async Task<ActionResult<string>> PutCourses_category(int id, Courses_category courses_category)
        {

            if (id != courses_category.id)
            {
                return Ok("العنصر غي موجود");
            }
            _context.Entry(courses_category).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Courses_categoryExists(id))
                {
                    return Ok("العنصر غي موجود");
                }
                else
                {
                    throw;
                }
            }

            return Ok("تم التعديل");
        }

        [HttpPost]
        public async Task<ActionResult<string>> PostCourses_category(Courses_category courses_category)
        {
            _context.courses_Categories.Add(courses_category);
            await _context.SaveChangesAsync();

            return Ok("تم الاضافه بنجاح");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteCourses_category(int id)
        {
            var item = _context.courses_Categories.FirstOrDefault(i => i.id == id);
            if (item == null)
            {
                return "العنصر غير موجود";
            }
            else
            {
                _context.courses_Categories.Remove(item);
                await _context.SaveChangesAsync();
                return Ok("تم المسح بنجاح");
            }
        }
        [Route("[action]/{id}")]
        [HttpGet()]
        public async Task<ActionResult<Courses_category>> Get_category(int id)
        {
            var courses_cats = await _context.courses_Categories.Where(e => e.Course_Detailesid == id).ToListAsync();

            if (courses_cats == null)
            {
                return NotFound();
            }
            else
            {

                return Ok(courses_cats);
            }
        }
        private bool Courses_categoryExists(int id)
        {
            return _context.courses_Categories.Any(e => e.id == id);
        }
    }
}
