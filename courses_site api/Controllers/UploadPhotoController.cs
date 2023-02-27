using courses_site_api.models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace courses_site_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadPhotoController : ControllerBase
    {
        private readonly courses_entitiy _context;
        private readonly IWebHostEnvironment _web;
        private readonly IHttpContextAccessor _IHttpContext;


        public UploadPhotoController(courses_entitiy context, IWebHostEnvironment env, IHttpContextAccessor IhttpcontextAccessor)
        {
            _context = context;
            _web = env;
            this._IHttpContext = IhttpcontextAccessor;

        }

        [HttpPost]
        public IActionResult SavePhoto(List<IFormFile> files)
        {
            if (files.Count == 0)
            {
                return BadRequest();
            }
            string directoryPath = Path.Combine(_web.ContentRootPath, "Photos");

            var BaseUrl = _IHttpContext.HttpContext.Request.Scheme + "://" +
                       _IHttpContext.HttpContext.Request.Host + _IHttpContext.HttpContext.Request.PathBase;
            var FileInfos = new List<fileuplaodinfo>();

            foreach (var file in files)
                {
                    string filepath = Path.Combine(directoryPath, file.FileName);
                    using (var stream = new FileStream(filepath, FileMode.Create))
                    {
                        file.CopyTo(stream);

                    }

                FileInfos.Add(new fileuplaodinfo { name = BaseUrl + "/Photos/" + file.FileName, lenght = file.Length });

            }
            return Ok(FileInfos);
        }
        [HttpGet]
        public IActionResult GetPhoto(string filepath)
        {
            if (filepath is null)
            {
                return BadRequest();
            }
            else
            {
                var Filepath = Path.GetFileName(filepath);
                var path = Path.Combine(_web.ContentRootPath, "Photos");
                FileInfo fi = new FileInfo(Path.Combine(path, Filepath));
                if (fi.Exists)
                {
                    fi.OpenRead();
                    return Ok("file is opening");
                }
                else
                {
                    return NotFound();
                }
            }



        }

        [HttpDelete]
        public IActionResult DeletePhoto(string filepath)
        {
            if (filepath is null)
            {
                return BadRequest();
            }
            else
            {
                var Filepath = Path.GetFileName(filepath);
                var path = Path.Combine(_web.ContentRootPath, "Photos");
                FileInfo fi = new FileInfo(Path.Combine(path, Filepath));
                if (fi.Exists)
                {
                    fi.Delete();
                    return Ok("file deleted");
                }
                else
                {
                    return NotFound();
                }
            }
        }

    }
}

