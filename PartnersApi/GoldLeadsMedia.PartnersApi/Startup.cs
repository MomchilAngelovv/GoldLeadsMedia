namespace GoldLeadsMedia.PartnersApi
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Hosting;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.PartnersApi.Services;
    using GoldLeadsMedia.PartnersApi.Services.Application.Common;
    using GoldLeadsMedia.PartnersApi.Services.Application;

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

            services.AddHttpClient();

            services.AddTransient<ILeadsService, LeadsService>();
            services.AddTransient<ICountriesService, CountriesService>();
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
