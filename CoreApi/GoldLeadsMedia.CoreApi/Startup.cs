namespace GoldLeadsMedia.CoreApi
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.CoreApi.Services.Accesses;
    using GoldLeadsMedia.CoreApi.Services.Countries;
    using GoldLeadsMedia.CoreApi.Services.Languages;
    using GoldLeadsMedia.CoreApi.Services.Leads;
    using GoldLeadsMedia.CoreApi.Services.OfferClicks;
    using GoldLeadsMedia.CoreApi.Services.OfferGroups;
    using GoldLeadsMedia.CoreApi.Services.Offers;
    using GoldLeadsMedia.CoreApi.Services.PaymentTypes;
    using GoldLeadsMedia.CoreApi.Services.TargetDevices;
    using GoldLeadsMedia.CoreApi.Services.Verticals;
    using GoldLeadsMedia.CoreApi.Services.LandingPages;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(
            IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<GoldLeadsMediaDbContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddCors();

            services.AddTransient<IOfferGroupsService, OfferGroupsService>();
            services.AddTransient<ILanguagesService, LanguagesService>();
            services.AddTransient<ITargetDevicesService, TargetDevicesService>();
            services.AddTransient<IAccessesService, AccessesService>();
            services.AddTransient<IPaymentTypesService, PaymentTypesService>();
            services.AddTransient<IVerticalsService, VerticalsService>();
            services.AddTransient<ICountriesService, CountriesService>();
            services.AddTransient<IOffersService, OffersService>();
            services.AddTransient<ILeadsService, LeadsService>();
            services.AddTransient<IOfferClicksService, OfferClicksService>();
            services.AddTransient<ILandingPagesService, LandingPagesService>();
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
