using System;
using System.Collections.Generic;
using System.Linq;


using System.Threading.Tasks;
using courses_site_api.DTOs;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace courses_site_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        [HttpPost]
        public IActionResult sendEmailmessage([FromBody]EmailSubject EMsubject )
        {
            string pass = "rdytcgubsaiwwulg";
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(EMsubject.name,EMsubject.Email));
            email.To.Add(MailboxAddress.Parse("aya483263@gmail.com"));
            email.Subject = "Message from course Site";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text=EMsubject.body};

            using var SMTP = new SmtpClient();
            
            try
            {
                SMTP.Connect("smtp.gmail.com", 465, true);
                SMTP.Authenticate("aya483263@gmail.com", pass);
                SMTP.Send(email);
                return Ok(" تم الارسال");
            }
            catch (Exception ex)
            {

                return StatusCode(500) ;
            }
            finally
            {
                SMTP.Disconnect(true);
                SMTP.Dispose();
            }
            
        }

    }
}
