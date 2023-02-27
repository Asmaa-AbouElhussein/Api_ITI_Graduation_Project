using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using courses_site_api.DTOs;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace courses_site_api.Extensions
{
    public static class Extension
    {
        public static string HashPassword(this string str) 
        {
            //HASHING
            byte[] salt = BitConverter.GetBytes(128 / 8);
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: str!,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));

            return hashed;
        }


        public static List<Usercomment> Ucomment<T>(this List<T> li)
        {
            List<Usercomment> liUser = new List<Usercomment>();
            foreach (dynamic item in li)
            {
                liUser.Add(new Usercomment() { comment=item.comment.comment,avatarpath= item.avatarpath, 
                    userName=item.userName,date=item.comment.date });
            }

            return liUser;
        }

        public static string sendEmail(this emaildataDTO dto)
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


            return "تم التسجيل بنجاح";
        }
    }
}
