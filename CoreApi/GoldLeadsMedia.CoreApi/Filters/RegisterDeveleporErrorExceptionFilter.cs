namespace GoldLeadsMedia.CoreApi.Filters
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc.Filters;

    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Services.Common;
    using GoldLeadsMedia.CoreApi.Models.Services.Input;

    public class RegisterDeveleporErrorExceptionFilter : IAsyncExceptionFilter
    {
        private readonly UserManager<GoldLeadsMediaUser> userManager;

        private readonly IErrorsService errorsService;

        public RegisterDeveleporErrorExceptionFilter(
            UserManager<GoldLeadsMediaUser> userManager,
            IErrorsService errorsService)
        {
            this.userManager = userManager;
            this.errorsService = errorsService;
        }

        public async Task OnExceptionAsync(ExceptionContext context)
        {
            var loggedUser = await userManager.GetUserAsync(context.HttpContext.User);

            var serviceModel = new ErrorsRegisterDeveloperErrorInputServiceModel
            {
                Method= context.HttpContext.Request.Method,
                Path = context.HttpContext.Request.Path,
                Message = context.Exception.Message,
                StackTrace = context.Exception.StackTrace,
                LoggedUserId = loggedUser?.Id,
                Information = "[CoreApi]"
            };

            var developerError = await this.errorsService.RegisterDeveloperErrorAsync(serviceModel);

            context.Result = new JsonResult(developerError);
        }
    }
}
