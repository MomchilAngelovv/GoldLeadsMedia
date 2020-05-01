﻿namespace GoldLeadsMedia.Web.Controllers
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

    public class SeederController : Controller
    {
        private readonly GoldLeadsMediaDbContext db;
        private readonly UserManager<GoldLeadsMediaUser> userManager;
        private readonly RoleManager<GoldLeadsMediaRole> roleManager;

        public SeederController(
            GoldLeadsMediaDbContext db,
            UserManager<GoldLeadsMediaUser> userManager,
            RoleManager<GoldLeadsMediaRole> roleManager)
        {
            this.db = db;
            this.userManager = userManager;
            this.roleManager = roleManager;
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
                ManagerId = user1.Id
            };


            var offerGroup = new OfferGroup
            {
                Name = "Best Offers",
            };

            var language = new Language
            {
                Name = "English",
            };

            var country = new Country
            {
                Name = "Bulgaria",
                IsoCode = "BG",
                PhonePrefix = "+359"
            };

            var verticalNames = new List<string> { "Crypto", "Forex", "Casino" };
            var accessNames = new List<string> { "Regular", "Private", "Vip" };
            var paymentTypeNames = new List<string> { "Per lead", "Per ftd" };
            var targetDeviceNames = new List<string> { "Computer", "Mobile", "Computer and mobile" };
            var landingPageUrls = new List<string> { "http://glm-cryptonews.com/bitcoinrevolution?of=", "http://glm-cryptonews.com/tesler?of=", "http://glm-cryptonews.com/bitcoinprofitnow?of=", "http://glm-cryptonews.com/bitcoinbillionaire?of=" };
            var roleNames = new List<string> { "Affiliate", "Manager", "Administrator",  };

            var varticals = new List<Vertical>();
            var accesses = new List<Access>();
            var paymentTypes = new List<PaymentType>();
            var targetDevices = new List<TargetDevice>();
            var landingPages = new List<LandingPage>();

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
                var paymentType = new PaymentType
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
            foreach (var url in landingPageUrls)
            {
                var langingPage = new LandingPage
                {
                    Name = $"Crypto News {cryptoNewsCounter++}",
                    RedirectUrl = url,
                };
                landingPages.Add(langingPage);
            }

            foreach (var name in roleNames)
            {
                var role = new GoldLeadsMediaRole
                {
                    Name = name,
                };

                await this.roleManager.CreateAsync(role);
            }

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
            await this.db.Countries.AddAsync(country);
            await this.db.OfferGroups.AddAsync(offerGroup);
            await this.db.PaymentTypes.AddRangeAsync(paymentTypes);
            await this.db.TargetDevices.AddRangeAsync(targetDevices);
            await this.db.LandingPages.AddRangeAsync(landingPages);

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
            stringBuilder.AppendLine($"[Landing pages:{db.LandingPages.Count()}]");

            return this.Content(stringBuilder.ToString());
        }
    }
}
