namespace GoldLeadsMedia.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using GoldLeadsMedia.Database;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.Web.Infrastructure.HttpHelper;
    using GoldLeadsMedia.Web.Models.Seeder;
    using Microsoft.AspNetCore.Authorization;

    public class SeederController : Controller
    {
        private readonly GoldLeadsMediaDbContext db;
        private readonly UserManager<GoldLeadsMediaUser> userManager;
        private readonly RoleManager<GoldLeadsMediaRole> roleManager;
        private readonly IAsyncHttpClient httpClient;

        public SeederController(
            GoldLeadsMediaDbContext db,
            UserManager<GoldLeadsMediaUser> userManager,
            RoleManager<GoldLeadsMediaRole> roleManager,
            IAsyncHttpClient httpClient)
        {
            this.db = db;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.httpClient = httpClient;
        }

        [Authorize]
        public IActionResult CheckLoginUrl()
        {
            return this.Ok("Hello!");
        }

        public async Task<IActionResult> SeedData()
        {
            if (this.db.Users.Count() != 0)
            {
                return this.Content("Database is not empty!");
            }

            var user1 = new GoldLeadsMediaUser
            {
                UserName = "Monkata",
                Email = "Monkata@abv.bg",
                IsVip = true
            };

            var user2 = new GoldLeadsMediaUser
            {
                UserName = "Pesho",
                Email = "Pesho@abv.bg",
                ManagerId = user1.Id
            };

            var user3 = new GoldLeadsMediaUser
            {
                UserName = "Gosho",
                Email = "Gosho@abv.bg",
            };

            var language = new Language
            {
                Name = "English",
            };

            var verticalNames = new List<string> { "Crypto", "Forex", "Casino" };
            var accessNames = new List<string> { "Regular", "Private", "Vip" };
            var paymentTypeNames = new List<string> { "CPL", "CPA", "CPC" };
            var targetDeviceNames = new List<string> { "Computer", "Mobile", "Computer and mobile" };
            var roleNames = new List<string> { "Affiliate", "Manager", "Administrator" };
            var offerGroupNames = new List<string> { "Best offers", "New offers", "Vip offers" };

            var varticals = new List<Vertical>();
            var accesses = new List<Access>();
            var paymentTypes = new List<PayType>();
            var targetDevices = new List<TargetDevice>();
            var offerGroups = new List<OfferGroup>();

            foreach (var name in verticalNames)
            {
                var vertical = new Vertical
                {
                    Name = name
                };
                varticals.Add(vertical);
            }

            foreach (var name in accessNames)
            {
                var access = new Access
                {
                    Name = name
                };
                accesses.Add(access);
            }

            foreach (var name in paymentTypeNames)
            {
                var paymentType = new PayType
                {
                    Name = name
                };
                paymentTypes.Add(paymentType);
            }

            foreach (var name in targetDeviceNames)
            {
                var targetDevice = new TargetDevice
                {
                    Name = name
                };
                targetDevices.Add(targetDevice);
            }

            var cryptoNewsCounter = 1;

            foreach (var name in roleNames)
            {
                var role = new GoldLeadsMediaRole
                {
                    Name = name,
                };

                await this.roleManager.CreateAsync(role);
            }

            foreach (var name in offerGroupNames)
            {
                var offerGroup = new OfferGroup
                {
                    Name = name,
                };

                offerGroups.Add(offerGroup);
            }

            await this.SeedCountries();

            await this.userManager.CreateAsync(user1, "123456");
            await this.userManager.CreateAsync(user2, "123456");
            await this.userManager.CreateAsync(user3, "123456");

            await this.userManager.AddToRoleAsync(user1, "Administrator");
            await this.userManager.AddToRoleAsync(user1, "Manager");
            await this.userManager.AddToRoleAsync(user1, "Affiliate");

            await this.userManager.AddToRoleAsync(user2, "Affiliate");
            await this.userManager.AddToRoleAsync(user3, "Affiliate");


            await this.db.Verticals.AddRangeAsync(varticals);
            await this.db.Accesses.AddRangeAsync(accesses);
            await this.db.Languages.AddAsync(language);
            await this.db.OfferGroups.AddRangeAsync(offerGroups);
            await this.db.PaymentTypes.AddRangeAsync(paymentTypes);
            await this.db.TargetDevices.AddRangeAsync(targetDevices);

            await this.db.SaveChangesAsync();

            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Database populated with:");
            stringBuilder.AppendLine($"[Roles:{db.Roles.Count()}]");
            stringBuilder.AppendLine($"[Users:{db.Users.Count()}]");
            stringBuilder.AppendLine($"[Verticals:{db.Verticals.Count()}]");
            stringBuilder.AppendLine($"[Accesses:{db.Accesses.Count()}]");
            stringBuilder.AppendLine($"[Languages:{db.Languages.Count()}]");
            stringBuilder.AppendLine($"[Countries:{db.Countries.Count()}]");
            stringBuilder.AppendLine($"[Offer groups:{db.OfferGroups.Count()}]");
            stringBuilder.AppendLine($"[Payment types:{db.PaymentTypes.Count()}]");
            stringBuilder.AppendLine($"[Target devices:{db.TargetDevices.Count()}]");

            return this.Content(stringBuilder.ToString());
        }

        private async Task SeedCountries()
        {
            var countrySeedModels = await this.httpClient.GetAsync<List<CountrySeedModel>>("https://restcountries.eu/rest/v2/all");

            var countries = new List<Country>();

            foreach (var contrySeedModel in countrySeedModels)
            {
                var country = new Country
                {
                    Name = contrySeedModel.Name,
                    IsoCode = contrySeedModel.Alpha2Code,
                    PhonePrefix = $"+{contrySeedModel.CallingCodes[0] ??= null}"
                };

                if (country.PhonePrefix == "+")
                {
                    country.PhonePrefix = null;
                }

                countries.Add(country);
            }

            await this.db.Countries.AddRangeAsync(countries);
            await this.db.SaveChangesAsync();
        }
    }
}
