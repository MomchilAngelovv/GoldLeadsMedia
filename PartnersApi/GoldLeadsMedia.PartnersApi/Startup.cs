namespace GoldLeadsMedia.PartnersApi
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Hosting;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.PartnersApi.Services.Application.Common;
    using GoldLeadsMedia.PartnersApi.Services.Application;
    using GoldLeadsMedia.Database.Models;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(
            IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCors();

            services.AddDbContext<GoldLeadsMediaDbContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(this.configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentity<GoldLeadsMediaUser, GoldLeadsMediaRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;

                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            })
           .AddEntityFrameworkStores<GoldLeadsMediaDbContext>()
           .AddDefaultTokenProviders();

            services.AddHttpClient();

            services.AddTransient<ILeadsService, LeadsService>();
            services.AddTransient<ICountriesService, CountriesService>();
            services.AddTransient<IOffersService, OffersService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseCors(options =>
            {
                options.AllowAnyOrigin();
                options.AllowAnyHeader();
                options.AllowAnyMethod();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
