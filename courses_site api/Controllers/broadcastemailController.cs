using courses_site_api.DTOs;
using courses_site_api.models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using System.Linq;

namespace courses_site_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class broadcastemailController : ControllerBase
    {
        private courses_entitiy _context;

        public broadcastemailController(courses_entitiy context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult sendemail(broadcastDTO dto)
        {
            var lisemail = _context.registrations.Select(d => d.email).ToList();
            try
            {
                foreach (var l in lisemail)
                {
                    var email = new MimeMessage();
                    email.From.Add(MailboxAddress.Parse("aya483263@gmail.com"));
                    email.To.Add(MailboxAddress.Parse(l));
                    email.Subject = dto.subject;
                    email.Body = new TextPart(TextFormat.Text) { Text = dto.body };
                    using var smtp = new SmtpClient();
                    smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    smtp.Authenticate("aya483263@gmail.com", "rdytcgubsaiwwulg");
                    smtp.Send(email);
                    smtp.Disconnect(true);
                }
            }
            catch
            {
                return BadRequest();
            }
            
            return Ok();
        }
    }
}
