namespace GoldLeadsMedia.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;

    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.Web.Models.InputModels;
    using Microsoft.Extensions.Configuration;

    public class UsersController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly SignInManager<GoldLeadsMediaUser> signInManager;
        private readonly UserManager<GoldLeadsMediaUser> userManager;

        public UsersController(IConfiguration configuration,
            SignInManager<GoldLeadsMediaUser> signInManager,
            UserManager<GoldLeadsMediaUser> userManager)
        {
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }


        [HttpGet]
        public IActionResult Login()
        {
            this.ViewData["ConnString"] = this.configuration.GetConnectionString("DefaultConnection");
            this.ViewData["CoreApiUrl"] = this.configuration["CoreApiUrl"];
            return this.View();
        }
        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return this.Redirect("/Users/Login");
        }
        [HttpGet]
        public async Task<IActionResult> Settings()
        {
            var loggedUser = await this.userManager.GetUserAsync(this.User);
            var viewModel = new UsersSettingsInputModel
            {
                Email = loggedUser.Email,
                Skype = loggedUser.Skype,
                Experience = loggedUser.Experience
            };
            return this.View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Login(UsersLoginInputModel input)
        {
            if (ModelState.IsValid == false)
            {
                return this.View(input);
            }

            var result = await signInManager.PasswordSignInAsync(input.UserName, input.Password, false, false);

            if (result.Succeeded == false)
            {
                this.HttpContext.Items.Add("InvalidLogin", "Invalid username or password!");
                return this.View();
            }

            return this.Redirect("/Offers/Dashboard");
        }
        [HttpPost]
        public async Task<IActionResult> Settings(UsersSettingsInputModel input)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View(input);
            }

            var loggedUser = await this.userManager.GetUserAsync(this.User);

            loggedUser.Email = input.Email;
            loggedUser.Skype = input.Skype;
            loggedUser.Experience = input.Experience;

            await this.userManager.UpdateAsync(loggedUser);
            return this.Redirect("/Users/Settings");
        }

        //[HttpPost]
        //public ActionResult AssignManagerToAffiliates(string managerId, string userIds)
        //{
        //    this.GetCurrentUser();
        //    string url = string.Format("{0}api/User/AssignManagerToAffiliates", Constants.WebAPIUrl);
        //    var response = WebRequestManager.HttpPost(url, new { Manager_Id = managerId, Users_Ids = userIds });

        //    if (response.Code == 1)
        //    {
        //        return this.Redirect("/manager/index");
        //    }

        //    return new HttpStatusCodeResult(200);
        //}
    }
}
