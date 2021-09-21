using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IPTreatmentManagement.Models.ApiRepositoryInterface;
using IPTreatmentManagement.Web.ConfigurationModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Refit;

namespace IPTreatmentManagement.Web
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
            //services.AddAutoMapper(typeof(IPTreatmentManagement.Models.MappingPofile));

            services.Configure<JwtCredentialConfiguration>(Configuration.GetSection("JwtCredentials"));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                    options.LoginPath = "/User/Login";
                    options.SlidingExpiration = true;
                });

            var iPTMApiConfigSection = Configuration.GetSection("IPTreatmentManagement.Api");
            var iPTMApiConfig = iPTMApiConfigSection.Get<IPTreatmentManagementApiConfiguration>();
            services.Configure<IPTreatmentManagementApiConfiguration>(iPTMApiConfigSection);

            services.AddRefitClient<IIPTreatmentPackageApiRepository>()
                .ConfigureHttpClient(c => c.BaseAddress = iPTMApiConfig.BaseUrlUri);
            services.AddRefitClient<IUserApiRepository>()
                .ConfigureHttpClient(c => c.BaseAddress = iPTMApiConfig.BaseUrlUri);

            services.AddControllersWithViews();
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
