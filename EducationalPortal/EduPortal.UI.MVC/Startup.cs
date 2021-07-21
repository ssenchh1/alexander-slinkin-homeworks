using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EduPortal.Application.Interfaces;
using EduPortal.Application.Services;
using EduPortal.Domain.Interfaces;
using EduPortal.Domain.Models;
using EduPortal.Domain.Models.Materials;
using EduPortal.Domain.Models.Users;
using EduPortal.Infrastructure.Context;
using EduPortal.Infrastructure.Repositories;
using EduPortal.UI.MVC.Middleware.Extentions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EduPortal.UI.MVC
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
            services.AddControllersWithViews();

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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseExceptionLogging();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
