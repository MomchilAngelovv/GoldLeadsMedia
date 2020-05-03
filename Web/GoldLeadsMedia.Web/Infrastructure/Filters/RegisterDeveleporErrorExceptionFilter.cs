using GoldLeadsMedia.Database;
using GoldLeadsMedia.Database.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldLeadsMedia.Web.Infrastructure.Filters
{
    public class RegisterDeveleporErrorExceptionFilter : IAsyncExceptionFilter
    {
        private readonly GoldLeadsMediaDbContext db;
        private readonly UserManager<GoldLeadsMediaUser> userManager;

        public RegisterDeveleporErrorExceptionFilter
            (GoldLeadsMediaDbContext db,
            UserManager<GoldLeadsMediaUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }
        
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            var loggedUser = await this.userManager.GetUserAsync(context.HttpContext.User);
            var developerError = new DeveloperError
            {
                Method = context.HttpContext.Request.Method,
                Path = context.HttpContext.Request.Path,
                Message = context.Exception.Message,
                StackTrace = context.Exception.StackTrace,
                UserId = loggedUser?.Id
            };

            await this.db.DeveloperErrors.AddAsync(developerError);
            await this.db.SaveChangesAsync();

            context.Result = new RedirectResult("/Home/Error");
        }
    }
}
