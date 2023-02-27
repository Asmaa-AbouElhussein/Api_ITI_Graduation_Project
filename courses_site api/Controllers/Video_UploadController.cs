using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using courses_site_api.models;

namespace courses_site_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Video_UploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _IwebHostEnvironment;
        private readonly IHttpContextAccessor _IHttpContext;


        public Video_UploadController(IWebHostEnvironment webHost, IHttpContextAccessor IhttpcontextAccessor)
        {
            this._IwebHostEnvironment = webHost;
            this._IHttpContext = IhttpcontextAccessor;
        }
        [HttpPost]
        public IActionResult Upload(List<IFormFile> Files)
        {
            if (Files.Count == 0)
            {
                return BadRequest();
            }
            else
            {
                var path = Path.Combine(_IwebHostEnvironment.ContentRootPath, "Uploads");

                var BaseUrl = _IHttpContext.HttpContext.Request.Scheme + "://" +
                       _IHttpContext.HttpContext.Request.Host + _IHttpContext.HttpContext.Request.PathBase;

                var FileInfos = new List<FileUploadedInfo>();

                foreach (IFormFile file in Files)
                {
                    string uniquenum = new Random().Next(0000, 9999).ToString();
                    string uniqFileName = uniquenum + file.FileName;
                    string filePath = Path.Combine(path, uniqFileName);
                    using (FileStream fs = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fs);

                    }
                    FileInfos.Add(new FileUploadedInfo { name = BaseUrl + "/Uploads/" + uniqFileName, lenght = file.Length });
                }
                return Ok(FileInfos);




            }

        }
        [HttpDelete]
        
        public IActionResult RemoveImg(string filepath)
        {
            if (filepath is null)
            {
                return BadRequest();
            }
            else
            {
                var Filepath=Path.GetFileName(filepath);
                var path = Path.Combine(_IwebHostEnvironment.ContentRootPath, "Uploads");
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
