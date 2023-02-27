using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using courses_site_api.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace courses_site_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {

        private readonly courses_entitiy _context;

        public ChatController(courses_entitiy context)
        {
            _context = context;
        }
        
        [HttpGet]
        public IActionResult Getmessages(string name)
        {
              var data= _context.chats.Where(r => (r.Sender == "Admin22" && r.Receiver==name )  || r.Sender==name).Select(d=>new {message= d.message,date=d.date,sender=d.Sender }).ToList();
            return Ok(data);
        }
    }
}
