using courses_site_api.DTOs;
using courses_site_api.Extensions;
using courses_site_api.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace courses_site_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class registrationController : ControllerBase
    {
        private courses_entitiy _context;

        public registrationController(courses_entitiy context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<registration>>> Getregistration()
        {
            return await _context.registrations.ToListAsync();
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<registration>> Getregistername()
        {
            var data = _context.registrations.Where(d=>d.username!= "Admin22").Select(d => new {d.username,d.avatarpath}).ToList();
            return Ok(data);
        }



        [HttpGet("Auth")]
        [Authorize]
        public  IActionResult checkAuth()
        {
            var identity = User.Identity as ClaimsIdentity;
            List<Claim> li = identity.Claims.ToList();
            List<string> Udata = new List<string> { li[0].Value, li[1].Value, li[2].Value };

            return Ok(Udata);
        }



        [HttpGet("{id}", Name = "GetUsData")]
        public async Task<ActionResult<registration>> Getregistration(int id)
        {
            var registration = await _context.registrations.FindAsync(id);

            if (registration == null)
            {
                return NotFound();
            }
            else
            {

                return Ok(registration);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Postregistration(registration registration)
        {
            if (_context.registrations.Where(e => e.email == registration.email).FirstOrDefault() == null)
            {

                registration.password = registration.password.HashPassword();
                _context.registrations.Add(registration);
                await _context.SaveChangesAsync();

                return Created("GetUsData", registration);
            }
            else
            {
                return BadRequest();
            }


        }
        [HttpDelete()]
        public async Task<IActionResult> Deleteemail(string email)
        {
            var id = await _context.registrations.Where(o => o.email == email).Select(o => o.id).FirstOrDefaultAsync();
            var obj = await _context.registrations.FindAsync(id);
            if (obj == null)
            {
                return NotFound();
            }

            _context.registrations.Remove(obj);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> PutCourses_videos(newpasswordDTO dto)
        {
            var obj = _context.registrations.Where(o => o.email == dto.email).Select(o => new { o.id, o.gender, o.avatarpath, o.username }).FirstOrDefault();
            dto.password = dto.password.HashPassword();
            registration objreg = new registration { email = dto.email, password = dto.password, id = obj.id, username = obj.username, avatarpath = obj.avatarpath, gender = obj.gender };
            _context.Entry(objreg).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                BadRequest();
            }

            return NoContent();
        }



        [HttpPost("UTokin")]
        public IActionResult CreateTokin(UserLogin user)
        {
            var usdata = _context.registrations.Where(r => r.username == user.username
            && r.password == user.password.HashPassword()).FirstOrDefault();
            if (usdata is not null)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.
                GetBytes("Secret_Key_235K2K1a23"));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var data = new List<Claim>();
                data.Add(new Claim("Email", usdata.email));
                data.Add(new Claim("username", usdata.username));
                if (user.username == "Admin22")
                {
                    data.Add(new Claim(ClaimTypes.Role, "Admin"));
                }
                else
                {
                    data.Add(new Claim(ClaimTypes.Role, "defaultUser"));

                }
                var token = new JwtSecurityToken(
                claims: data,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: credentials);

                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            else
            {
                return Unauthorized();
            }
        }


    }
}
