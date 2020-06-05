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

            AddLang("aa", "Afar", languages);
            AddLang("af", "Afrikaans", languages);
            AddLang("ak", "Akan", languages);
            AddLang("sq", "Albanian", languages);
            AddLang("am", "Amharic", languages);
            AddLang("ar", "Arabic", languages);
            AddLang("an", "Aragonese", languages);
            AddLang("hy", "Armenian", languages);
            AddLang("as", "Assamese", languages);
            AddLang("av", "Avaric", languages);
            AddLang("ae", "Avestan", languages);
            AddLang("ay", "Aymara", languages);
            AddLang("az", "Azerbaijani", languages);
            AddLang("bm", "Bambara", languages);
            AddLang("ba", "Bashkir", languages);
            AddLang("eu", "Basque", languages);
            AddLang("be", "Belarusian", languages);
            AddLang("bn", "Bengali", languages);
            AddLang("bh", "Bihari", languages);
            AddLang("bi", "Bislama", languages);
            AddLang("bs", "Bosnian", languages);
            AddLang("bg", "Bulgarian", languages);
            AddLang("zh", "Chinese", languages);
            AddLang("cv", "Chuvash", languages);
            AddLang("kw", "Cornish", languages);
            AddLang("co", "Corsican", languages);
            AddLang("cr", "Cree", languages);
            AddLang("hr", "Croatian", languages);
            AddLang("cs", "Czech", languages);
            AddLang("da", "Danish", languages);
            AddLang("nl", "Dutch", languages);
            AddLang("en", "English", languages);
            AddLang("et", "Estonian", languages);
            AddLang("ee", "Ewe", languages);
            AddLang("fo", "Faroese", languages);
            AddLang("fj", "Fijian", languages);
            AddLang("fi", "Finnish", languages);
            AddLang("fr", "French", languages);
            AddLang("gl", "Galician", languages);
            AddLang("ka", "Georgian", languages);
            AddLang("de", "German", languages);
            AddLang("el", "Greek", languages);
            AddLang("gn", "Guaraní", languages);
            AddLang("gu", "Gujarati", languages);
            AddLang("ht", "Haitian", languages);
            AddLang("ha", "Hausa", languages);
            AddLang("he", "Hebrew", languages);
            AddLang("hz", "Herero", languages);
            AddLang("hi", "Hindi", languages);
            AddLang("hu", "Hungarian", languages);
            AddLang("id", "Indonesian", languages);
            AddLang("ga", "Irish", languages);
            AddLang("is", "Icelandic", languages);
            AddLang("it", "Italian", languages);
            AddLang("ja", "Japanese", languages);
            AddLang("kr", "Kanuri", languages);
            AddLang("kk", "Kazakh", languages);
            AddLang("km", "Khmer", languages);
            AddLang("ki", "Kikuyu", languages);
            AddLang("rw", "Kinyarwanda", languages);
            AddLang("ky", "Kirghiz", languages);
            AddLang("kg", "Kongo", languages);
            AddLang("ko", "Korean", languages);
            AddLang("lb", "Luxembourgish", languages);
            AddLang("lg", "Luganda", languages);
            AddLang("li", "Limburgish", languages);
            AddLang("ln", "Lingala", languages);
            AddLang("lo", "Lao", languages);
            AddLang("lt", "Lithuanian", languages);
            AddLang("lu", "Luba-Katanga", languages);
            AddLang("lv", "Latvian", languages);
            AddLang("gv", "Manx", languages);
            AddLang("mk", "Macedonian", languages);
            AddLang("mg", "Malagasy", languages);
            AddLang("ms", "Malay", languages);
            AddLang("ml", "Malayalam", languages);
            AddLang("mt", "Maltese", languages);
            AddLang("mi", "Māori", languages);
            AddLang("mr", "Marathi", languages);
            AddLang("mh", "Marshallese", languages);
            AddLang("mn", "Mongolian", languages);
            AddLang("na", "Nauru", languages);
            AddLang("ng", "Ndonga", languages);
            AddLang("no", "Norwegian", languages);
            AddLang("ii", "Nuosu", languages);
            AddLang("oc", "Occitan", languages);
            AddLang("os", "Ossetian, Ossetic", languages);
            AddLang("pa", "Panjabi, Punjabi", languages);
            AddLang("pl", "Polish", languages);
            AddLang("pt", "Portuguese", languages);
            AddLang("qu", "Quechua", languages);
            AddLang("rm", "Romansh", languages);
            AddLang("rn", "Kirundi", languages);
            AddLang("ro", "Romanian", languages);
            AddLang("ru", "Russian", languages);
            AddLang("sa", "Sanskrit", languages);
            AddLang("sc", "Sardinian", languages);
            AddLang("sd", "Sindhi", languages);
            AddLang("sm", "Samoan", languages);
            AddLang("sg", "Sango", languages);
            AddLang("sr", "Serbian", languages);
            AddLang("sk", "Slovak", languages);
            AddLang("sl", "Slovene", languages);
            AddLang("so", "Somali", languages);
            AddLang("es", "Spanish", languages);
            AddLang("su", "Sundanese", languages);
            AddLang("sw", "Swahili", languages);
            AddLang("ss", "Swati", languages);
            AddLang("sv", "Swedish", languages);
            AddLang("th", "Thai", languages);
            AddLang("ti", "Tigrinya", languages);
            AddLang("tl", "Tagalog", languages);
            AddLang("tn", "Tswana", languages);
            AddLang("to", "Tonga", languages);
            AddLang("tr", "Turkish", languages);
            AddLang("uk", "Ukrainian", languages);
            AddLang("ve", "Venda", languages);
            AddLang("vi", "Vietnamese", languages);
            AddLang("cy", "Welsh", languages);

            await this.db.Languages.AddRangeAsync(languages);
        }

        private void AddLang(string code, string name, List<Language> languages)
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
