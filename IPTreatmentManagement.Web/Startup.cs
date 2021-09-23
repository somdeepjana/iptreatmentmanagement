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
using IPTreatmentManagement.Web.Handlers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
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

            #region Authentication Configuration
            services.Configure<JwtCredentialConfiguration>(Configuration.GetSection("JwtCredentials"));

            services.AddHttpContextAccessor();
            //services.AddSession(options =>
            //{
            //    options.IdleTimeout = TimeSpan.FromMinutes(30);
            //    options.Cookie.IsEssential = true;
            //});
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                    options.LoginPath = "/User/Login";
                    options.SlidingExpiration = true;
                });
            #endregion

            services.AddTransient<AuthorizationMessageHandler>();

            var iPTMApiConfigSection = Configuration.GetSection("IPTreatmentManagement:Api");
            var iPTMApiConfig = iPTMApiConfigSection.Get<IPTreatmentManagementApiConfiguration>();
            services.Configure<IPTreatmentManagementApiConfiguration>(iPTMApiConfigSection);

            #region Api Repositories
            services.AddRefitClient<IIPTreatmentPackageApiRepository>()
                .ConfigureHttpClient(c => c.BaseAddress = iPTMApiConfig.BaseUrlUri)
                .AddHttpMessageHandler<AuthorizationMessageHandler>();
            services.AddRefitClient<ISpecialistApiRepository>()
                .ConfigureHttpClient(c => c.BaseAddress = iPTMApiConfig.BaseUrlUri)
                .AddHttpMessageHandler<AuthorizationMessageHandler>();
            services.AddRefitClient<IInsurerApiRepository>()
                .ConfigureHttpClient(c => c.BaseAddress = iPTMApiConfig.BaseUrlUri)
                .AddHttpMessageHandler<AuthorizationMessageHandler>();
            services.AddRefitClient<ITreatmentPlanApiRepository>()
                .ConfigureHttpClient(c => c.BaseAddress = iPTMApiConfig.BaseUrlUri)
                .AddHttpMessageHandler<AuthorizationMessageHandler>();
            services.AddRefitClient<IClaimsApiRepository>()
                .ConfigureHttpClient(c => c.BaseAddress = iPTMApiConfig.BaseUrlUri)
                .AddHttpMessageHandler<AuthorizationMessageHandler>();

            services.AddRefitClient<IUserApiRepository>()
                .ConfigureHttpClient(c => c.BaseAddress = iPTMApiConfig.BaseUrlUri);
            #endregion

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
            //app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Launch}/{id?}")
                .RequireAuthorization();
            });
        }
    }
}
