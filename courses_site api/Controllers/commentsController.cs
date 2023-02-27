using courses_site_api.DTOs;
using courses_site_api.Extensions;
using courses_site_api.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace courses_site_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class commentsController : ControllerBase
    {
        private courses_entitiy _context;

        public commentsController(courses_entitiy context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Getcomments()
        {
            var Anoncomment = await _context.comments.Select(data => new {comment=data,
                 userName= data.registration.username,
                avatarpath=data.registration.avatarpath}).ToListAsync();
            List < Usercomment> liUsercomments= Anoncomment.Ucomment();
            return Ok(liUsercomments);
        }

        [HttpGet("{id}")]
        public  async Task<ActionResult<comments>> Getcomments(int id)
        {
            var comments =await _context.comments.FindAsync(id);

            if (comments == null)
            {
                return NotFound();
            }

            return comments;
        }
        [HttpPost]
        public async Task<ActionResult<string>> Postcomments(CommentData ComD)
        {
            if (ComD is not null)
            {
                int UserDataId = _context.registrations.FirstOrDefault(e => e.email == ComD.Email).id;
                if (UserDataId !=0)
                {
                    comments cts = new comments() {comment=ComD.comment,
                    date=DateTime.Now.ToShortDateString(),registrationid=UserDataId  };
                _context.comments.Add(cts);
           await _context.SaveChangesAsync();
                return Ok("تم اضافه التعليق");
                }
                else
                {
                    return NotFound();
                }
                
            }
            else
            {
                return BadRequest();
            }
            
        }


        [HttpDelete()]
        public async Task<IActionResult> Deletecomments(string uname)
        {
            var id =  _context.registrations.FirstOrDefault(s=>s.username==uname)?.id;
            var obj=await _context.comments.FirstOrDefaultAsync(r => r.registrationid == id);
            if (obj is not null)
            {
                var Anoncomment = _context.comments.Remove(obj);
                await _context.SaveChangesAsync();
                return Ok("تم المسح بنجاح");
            }
            else
            {
                return BadRequest();
            }
        }


    }
}
