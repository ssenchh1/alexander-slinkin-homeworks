using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduPortal.Application.Interfaces;
using EduPortal.Application.Services;
using EduPortal.Domain.Interfaces;
using EduPortal.Domain.Models;
using EduPortal.Domain.Models.Materials;
using EduPortal.Domain.Models.Users;
using EduPortal.Infrastructure.Context;
using EduPortal.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EduPortalWebApi
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EduPortalWebApi", Version = "v1" });
            });

            var a = Configuration.GetConnectionString("Default");

            services.AddDbContext<EducationalPortalContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<EducationalPortalContext>();

            services.AddTransient<IGenericUserRepository<User>, UserRepository>();
            services.AddScoped<IRepository<Skill>, Repository<Skill>>();
            services.AddTransient<IUserRepository<Mentor>, MentorRepository>();
            services.AddScoped<IUserRepository<Student>, StudentRepository>();
            services.AddTransient<IRepository<Material>, Repository<Material>>();
            services.AddTransient<IMentorService, MentorService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IRepository<Course>, Repository<Course>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EduPortalWebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
