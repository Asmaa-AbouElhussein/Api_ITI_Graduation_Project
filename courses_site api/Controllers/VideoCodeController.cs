using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using courses_site_api.DTOs;
using courses_site_api.Extensions;
using courses_site_api.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace courses_site_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoCodeController : ControllerBase
    {
        private readonly courses_entitiy _context;
        

        public VideoCodeController(courses_entitiy context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult SendSavedEmails()
        {
            var emails =  _context.purchasedcourses.Select(e => e.email).Distinct().ToList();
            if (emails is not null)
            {
                return Ok(emails);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpDelete]
        public ActionResult Deletecode( DeletepurchasedcodeDTO crs)
        {
            var crsObj = _context.purchasedcourses.Where(e => e.email == crs.email
             && e.courseid == crs.crsid).FirstOrDefault();

            if (crsObj is not null)
            {
                _context.purchasedcourses.Remove(crsObj);
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }



        }








        [HttpPost]
        public async Task<ActionResult> Sendcode(purchasedcourses Pcrs)
        {
            if (Pcrs is not null)
            {
                if (_context.purchasedcourses.FirstOrDefault(e => e.email == Pcrs.email && e.courseid == Pcrs.courseid) == null)
                {
                    await _context.purchasedcourses.AddAsync(Pcrs);
                    await _context.SaveChangesAsync();

                    emaildataDTO emailTosend = new emaildataDTO()
                    {
                        mailto = Pcrs.email,
                        subject = "كود التحقق",
                        code = Pcrs.code
                    };
                    string res = emailTosend.sendEmail();
                    return Ok(res);
                }
                else
                {
                    return BadRequest(new { error = "تم التسجيل بالفعل" });
                }
            }
            else
            {
                return BadRequest();
            }
        }



    }
}
