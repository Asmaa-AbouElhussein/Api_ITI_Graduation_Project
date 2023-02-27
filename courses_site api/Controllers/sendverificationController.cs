using courses_site_api.DTOs;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using System.Threading.Tasks;

namespace courses_site_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class sendverificationController : ControllerBase
    {
       
        [HttpPost]
        public  IActionResult sendemail(emaildataDTO dto)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("www.michael.malak.shehata.com@gmail.com"));
                email.To.Add(MailboxAddress.Parse(dto.mailto));
                email.Subject = dto.subject;
                email.Body = new TextPart(TextFormat.Text) { Text = dto.code };
                using var smtp = new SmtpClient();
                smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("www.michael.malak.shehata.com@gmail.com", "gkthwrgppfdibikq");
                smtp.Send(email);
                smtp.Disconnect(true);

            }
            catch
            {
                return BadRequest();
            }


            return Ok();
        }
        
  
    }
}
