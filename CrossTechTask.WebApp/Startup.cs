using CrossTech.ClientApi.Service;
using CrossTech.ClientApi.Service.Implementation;
using CrossTech.Core.Providers;
using CrossTech.Core.Providers.Implementation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace CrossTechTask
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
            services.AddMvc(options =>
                options.EnableEndpointRouting = false
                )
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Authorization/Index");
                    options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Authorization/AccessDenied");
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", x => { x.RequireClaim("Admin"); });
                options.AddPolicy("User", x => { x.RequireClaim("User"); });
            });

            services.AddSingleton(Configuration);

            services.AddControllersWithViews();

            services.AddScoped<IWebRequestProvider, WebRequestProvider>();
            services.AddScoped<IEmployeeRemoteCallService, EmployeeRemoteCallService>();
            services.AddScoped<IAuthorizationRemoteCallService, AuthorizationRemoteCallService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
            app.UseCookiePolicy();

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
