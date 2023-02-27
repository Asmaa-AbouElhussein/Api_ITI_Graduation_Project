using courses_site_api.models;
using courses_site_api.SignalR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courses_site_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
                 services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                    
                        ValidateLifetime = true,
                        ValidateIssuer=false,
                        ValidateAudience=false,
                        ValidateIssuerSigningKey = true,
                        ClockSkew=TimeSpan.Zero,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
                    (Configuration["secrestkey"]))
                    };
                    
                });

            services.AddControllers().AddNewtonsoftJson(opt => opt.SerializerSettings.
            ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            
            services.AddDbContext<courses_entitiy>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("ItIcrs"));
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApplication1", Version = "v1" });
            });
            services.AddCors(corsoption =>
            {
                corsoption.AddPolicy("myplicy", corsPolicyBuilder => {
                    corsPolicyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });
            services.Configure<FormOptions>(options =>
            {
                options.ValueLengthLimit=int.MaxValue;
                options.MultipartBodyLengthLimit=int.MaxValue;

            });
            services.AddSignalR(e => e.EnableDetailedErrors = true);
            services.AddControllers();
            //.AddNewtonsoftJson(e=>
            //e.SerializerSettings.ReferenceLoopHandling=Newtonsoft.Json.ReferenceLoopHandling.);
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "courses_site_api", Version = "v1" });
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "courses_site_api v1"));
            }
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider
                 (Path.Combine(Directory.GetCurrentDirectory(), "Photos", 
                 Directory.GetCurrentDirectory(), "Photos")), RequestPath = "/Photos",

            });
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider
                 (Path.Combine(Directory.GetCurrentDirectory(), "Uploads",
                 Directory.GetCurrentDirectory(), "Uploads")),
                RequestPath = "/Uploads",

            });

            app.UseRouting();
            app.UseCors("myplicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/toastr");
            });
        }
    }
}
