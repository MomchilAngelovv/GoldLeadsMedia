namespace GoldLeadsMedia.Web.Controllers
{
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;

    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.Web.Infrastructure.HttpHelper;

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

        public async Task<IActionResult> SeedData()
        {
            //TODO Make migration with data seed
            if (this.db.Users.Count() != 0)
            {
                return this.Content("Database is not empty!");
            }

            var adminUser = new GoldLeadsMediaUser
            {
                UserName = "Lora",
                Email = "contact@goldleadsmedia.com",
            };

            var testBroker = new Broker
            {
                Name = "Test"
            };

            var verticalNames = new List<string> { "Crypto", "Forex", "Casino" };
            var accessNames = new List<string> { "Regular", "Private", "Vip" };
            var paymentTypeNames = new List<string> { "CPL", "CPA", "CPC" };
            var targetDeviceNames = new List<string> { "Computer", "Mobile", "Computer and mobile" };
            var roleNames = new List<string> { "Affiliate", "Manager", "Administrator" };
            var offerGroupNames = new List<string> { "Best offers", "New offers", "Vip offers" };
            var tierCountryNames = new List<string> { "Tier 1", "Tier 2", "Tier 3" };

            var varticals = new List<Vertical>();
            var accesses = new List<Access>();
            var paymentTypes = new List<PayType>();
            var targetDevices = new List<TargetDevice>();
            var offerGroups = new List<OfferGroup>();
            var tierCountries = new List<CountryTier>();

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

            foreach (var name in tierCountryNames)
            {
                var tierCountry = new CountryTier
                {
                    Name = name,
                };

                tierCountries.Add(tierCountry);
            }

            await this.SeedCountries();
            await this.SeedLanguages();

            await this.userManager.CreateAsync(adminUser, "1234567890aA");

            await this.userManager.AddToRoleAsync(adminUser, "Administrator");
            await this.userManager.AddToRoleAsync(adminUser, "Manager");

            await this.db.Verticals.AddRangeAsync(varticals);
            await this.db.Accesses.AddRangeAsync(accesses);
            await this.db.OfferGroups.AddRangeAsync(offerGroups);
            await this.db.PayTypes.AddRangeAsync(paymentTypes);
            await this.db.TargetDevices.AddRangeAsync(targetDevices);
            await this.db.CountryTiers.AddRangeAsync(tierCountries);
            await this.db.Brokers.AddAsync(testBroker);

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
            stringBuilder.AppendLine($"[Payment types:{db.PayTypes.Count()}]");
            stringBuilder.AppendLine($"[Target devices:{db.TargetDevices.Count()}]");
            stringBuilder.AppendLine($"[Tier countries:{db.CountryTiers.Count()}]");
            stringBuilder.AppendLine($"[Tier countries:{db.Brokers.Count()}]");

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
        private async Task SeedLanguages()
        {
            var languages = new List<Language>();

            AddLanguage("ar", "Arabic", languages);
            AddLanguage("hy", "Armenian", languages);
            AddLanguage("be", "Belarusian", languages);
            AddLanguage("bg", "Bulgarian", languages);
            AddLanguage("zh", "Chinese", languages);
            AddLanguage("hr", "Croatian", languages);
            AddLanguage("cs", "Czech", languages);
            AddLanguage("da", "Danish", languages);
            AddLanguage("nl", "Dutch", languages);
            AddLanguage("en", "English", languages);
            AddLanguage("et", "Estonian", languages);
            AddLanguage("fi", "Finnish", languages);
            AddLanguage("fr", "French", languages);
            AddLanguage("de", "German", languages);
            AddLanguage("el", "Greek", languages);
            AddLanguage("hi", "Hindi", languages);
            AddLanguage("hu", "Hungarian", languages);
            AddLanguage("it", "Italian", languages);
            AddLanguage("ja", "Japanese", languages);
            AddLanguage("ko", "Korean", languages);
            AddLanguage("lb", "Luxembourgish", languages);
            AddLanguage("lt", "Lithuanian", languages);
            AddLanguage("lv", "Latvian", languages);
            AddLanguage("mk", "Macedonian", languages);
            AddLanguage("no", "Norwegian", languages);
            AddLanguage("pl", "Polish", languages);
            AddLanguage("pt", "Portuguese", languages);
            AddLanguage("ro", "Romanian", languages);
            AddLanguage("ru", "Russian", languages);
            AddLanguage("sr", "Serbian", languages);
            AddLanguage("sk", "Slovak", languages);
            AddLanguage("sl", "Slovene", languages);
            AddLanguage("es", "Spanish", languages);
            AddLanguage("sv", "Swedish", languages);
            AddLanguage("tr", "Turkish", languages);
            AddLanguage("uk", "Ukrainian", languages);

            await this.db.Languages.AddRangeAsync(languages);
        }
        private void AddLanguage(string code, string name, List<Language> languages)
        {
            var language = new Language
            {
                Name = name,
                Code = code.ToUpper()
            };

            languages.Add(language);
        }
    }

    public class CountrySeedModel
    {
        public string Name { get; set; }
        public string Alpha2Code { get; set; }
        public List<string> CallingCodes { get; set; }
    }
}
