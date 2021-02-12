using Common;
using DataAccess;
using DataAccess.Contracts;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.Services;
using System;
using WebFramework.Configuration;
using WebFramework.Middlewares;

namespace PosManagementAPI
{
    public class Startup
    {
        private SiteSettings _siteSetting;
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _siteSetting = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.AllowAnyHeader();
                        builder.AllowAnyOrigin();
                        builder.AllowAnyMethod();
                    });
            });
            services.Configure<SiteSettings>(Configuration.GetSection(nameof(SiteSettings)));
            services.AddControllers();
            services.AddDbContext<TmsManagementContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SqlServer"));
            });
            services.AddCustomIdentity();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddjwtAuthentication(_siteSetting.JwtSettings);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCustomExceptionHandler();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            try
            {
                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                    .CreateScope())
                {
                    serviceScope.ServiceProvider.GetService<TmsManagementContext>().Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
