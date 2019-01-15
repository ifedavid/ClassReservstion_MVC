using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LearningMVC_API.Data;
using LearningMVC_API.Models;
using LearningMVC_API.Services;

namespace LearningMVC_API
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();


            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
               
                options.User.RequireUniqueEmail = true;

            });
            services.ConfigureApplicationCookie(option =>
            {
                option.Cookie.Expiration = TimeSpan.FromMinutes(20);
            });
          
            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider service)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });


            CreateUserRoles(service).Wait();
        }


        private async Task CreateUserRoles(IServiceProvider service)
        {
            var RoleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = service.GetRequiredService<UserManager<ApplicationUser>>();
            IdentityResult roleResult;
            string[] roleNames = { "User", "Admin" };
            foreach (var Rolename in roleNames)
            {
                bool roleCheck = await RoleManager.RoleExistsAsync(Rolename);
                if (!roleCheck)
                {


                    roleResult = await RoleManager.CreateAsync(new IdentityRole(Rolename));
                }
            }
            ApplicationUser user1 = await UserManager.FindByEmailAsync("ifeoluwa@gmail.com");
           
            await UserManager.AddToRoleAsync(user1, "Admin");



        }
    }
}



