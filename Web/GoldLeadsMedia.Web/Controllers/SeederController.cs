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
    using GoldLeadsMedia.Web.Models.Seeder;
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

            var user1 = new GoldLeadsMediaUser
            {
                UserName = "Lora",
                Email = "contact@goldleadsmedia.com",
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
            var tierCountries = new List<TierCountry>();

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
                var tierCountry = new TierCountry
                {
                    Name = name,
                };

                tierCountries.Add(tierCountry);
            }

            await this.SeedCountries();
            await this.SeedLanguages();

            await this.userManager.CreateAsync(user1, "1234567890aA");

            await this.userManager.AddToRoleAsync(user1, "Administrator");
            await this.userManager.AddToRoleAsync(user1, "Manager");

            await this.db.Verticals.AddRangeAsync(varticals);
            await this.db.Accesses.AddRangeAsync(accesses);
            await this.db.OfferGroups.AddRangeAsync(offerGroups);
            await this.db.PaymentTypes.AddRangeAsync(paymentTypes);
            await this.db.TargetDevices.AddRangeAsync(targetDevices);
            await this.db.TierCountries.AddRangeAsync(tierCountries);

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
            stringBuilder.AppendLine($"[Tier countries:{db.TierCountries.Count()}]");

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

            AddLanguage("aa", "Afar", languages);
            AddLanguage("af", "Afrikaans", languages);
            AddLanguage("ak", "Akan", languages);
            AddLanguage("sq", "Albanian", languages);
            AddLanguage("am", "Amharic", languages);
            AddLanguage("ar", "Arabic", languages);
            AddLanguage("an", "Aragonese", languages);
            AddLanguage("hy", "Armenian", languages);
            AddLanguage("as", "Assamese", languages);
            AddLanguage("av", "Avaric", languages);
            AddLanguage("ae", "Avestan", languages);
            AddLanguage("ay", "Aymara", languages);
            AddLanguage("az", "Azerbaijani", languages);
            AddLanguage("bm", "Bambara", languages);
            AddLanguage("ba", "Bashkir", languages);
            AddLanguage("eu", "Basque", languages);
            AddLanguage("be", "Belarusian", languages);
            AddLanguage("bn", "Bengali", languages);
            AddLanguage("bh", "Bihari", languages);
            AddLanguage("bi", "Bislama", languages);
            AddLanguage("bs", "Bosnian", languages);
            AddLanguage("bg", "Bulgarian", languages);
            AddLanguage("zh", "Chinese", languages);
            AddLanguage("cv", "Chuvash", languages);
            AddLanguage("kw", "Cornish", languages);
            AddLanguage("co", "Corsican", languages);
            AddLanguage("cr", "Cree", languages);
            AddLanguage("hr", "Croatian", languages);
            AddLanguage("cs", "Czech", languages);
            AddLanguage("da", "Danish", languages);
            AddLanguage("nl", "Dutch", languages);
            AddLanguage("en", "English", languages);
            AddLanguage("et", "Estonian", languages);
            AddLanguage("ee", "Ewe", languages);
            AddLanguage("fo", "Faroese", languages);
            AddLanguage("fj", "Fijian", languages);
            AddLanguage("fi", "Finnish", languages);
            AddLanguage("fr", "French", languages);
            AddLanguage("gl", "Galician", languages);
            AddLanguage("ka", "Georgian", languages);
            AddLanguage("de", "German", languages);
            AddLanguage("el", "Greek", languages);
            AddLanguage("gn", "Guaraní", languages);
            AddLanguage("gu", "Gujarati", languages);
            AddLanguage("ht", "Haitian", languages);
            AddLanguage("ha", "Hausa", languages);
            AddLanguage("he", "Hebrew", languages);
            AddLanguage("hz", "Herero", languages);
            AddLanguage("hi", "Hindi", languages);
            AddLanguage("hu", "Hungarian", languages);
            AddLanguage("id", "Indonesian", languages);
            AddLanguage("ga", "Irish", languages);
            AddLanguage("is", "Icelandic", languages);
            AddLanguage("it", "Italian", languages);
            AddLanguage("ja", "Japanese", languages);
            AddLanguage("kr", "Kanuri", languages);
            AddLanguage("kk", "Kazakh", languages);
            AddLanguage("km", "Khmer", languages);
            AddLanguage("ki", "Kikuyu", languages);
            AddLanguage("rw", "Kinyarwanda", languages);
            AddLanguage("ky", "Kirghiz", languages);
            AddLanguage("kg", "Kongo", languages);
            AddLanguage("ko", "Korean", languages);
            AddLanguage("lb", "Luxembourgish", languages);
            AddLanguage("lg", "Luganda", languages);
            AddLanguage("li", "Limburgish", languages);
            AddLanguage("ln", "Lingala", languages);
            AddLanguage("lo", "Lao", languages);
            AddLanguage("lt", "Lithuanian", languages);
            AddLanguage("lu", "Luba-Katanga", languages);
            AddLanguage("lv", "Latvian", languages);
            AddLanguage("gv", "Manx", languages);
            AddLanguage("mk", "Macedonian", languages);
            AddLanguage("mg", "Malagasy", languages);
            AddLanguage("ms", "Malay", languages);
            AddLanguage("ml", "Malayalam", languages);
            AddLanguage("mt", "Maltese", languages);
            AddLanguage("mi", "Māori", languages);
            AddLanguage("mr", "Marathi", languages);
            AddLanguage("mh", "Marshallese", languages);
            AddLanguage("mn", "Mongolian", languages);
            AddLanguage("na", "Nauru", languages);
            AddLanguage("ng", "Ndonga", languages);
            AddLanguage("no", "Norwegian", languages);
            AddLanguage("ii", "Nuosu", languages);
            AddLanguage("oc", "Occitan", languages);
            AddLanguage("os", "Ossetian, Ossetic", languages);
            AddLanguage("pa", "Panjabi, Punjabi", languages);
            AddLanguage("pl", "Polish", languages);
            AddLanguage("pt", "Portuguese", languages);
            AddLanguage("qu", "Quechua", languages);
            AddLanguage("rm", "Romansh", languages);
            AddLanguage("rn", "Kirundi", languages);
            AddLanguage("ro", "Romanian", languages);
            AddLanguage("ru", "Russian", languages);
            AddLanguage("sa", "Sanskrit", languages);
            AddLanguage("sc", "Sardinian", languages);
            AddLanguage("sd", "Sindhi", languages);
            AddLanguage("sm", "Samoan", languages);
            AddLanguage("sg", "Sango", languages);
            AddLanguage("sr", "Serbian", languages);
            AddLanguage("sk", "Slovak", languages);
            AddLanguage("sl", "Slovene", languages);
            AddLanguage("so", "Somali", languages);
            AddLanguage("es", "Spanish", languages);
            AddLanguage("su", "Sundanese", languages);
            AddLanguage("sw", "Swahili", languages);
            AddLanguage("ss", "Swati", languages);
            AddLanguage("sv", "Swedish", languages);
            AddLanguage("th", "Thai", languages);
            AddLanguage("ti", "Tigrinya", languages);
            AddLanguage("tl", "Tagalog", languages);
            AddLanguage("tn", "Tswana", languages);
            AddLanguage("to", "Tonga", languages);
            AddLanguage("tr", "Turkish", languages);
            AddLanguage("uk", "Ukrainian", languages);
            AddLanguage("ve", "Venda", languages);
            AddLanguage("vi", "Vietnamese", languages);
            AddLanguage("cy", "Welsh", languages);

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
}
