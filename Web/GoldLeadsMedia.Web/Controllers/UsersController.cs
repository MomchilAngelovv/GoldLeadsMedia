namespace GoldLeadsMedia.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;

    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.Web.Models.InputModels;

    public class UsersController : Controller
    {
        private readonly SignInManager<GoldLeadsMediaUser> signInManager;
        private readonly UserManager<GoldLeadsMediaUser> userManager;

        public UsersController(
            SignInManager<GoldLeadsMediaUser> signInManager,
            UserManager<GoldLeadsMediaUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public IActionResult Login()
        {
            return this.View();
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

            return this.Redirect("/");
        }
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return this.Redirect("/Users/Login");
        }
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

        //public ActionResult InsertLead(InsertLeadBindingModel req)
        //{
        //    var response = new BaseResultModel();
        //    if (!ModelState.IsValid)
        //    {
        //        var errors = new List<string>();
        //        foreach (var state in ModelState)
        //        {
        //            foreach (var error in state.Value.Errors)
        //            {
        //                errors.Add(error.ErrorMessage);
        //            }
        //        }
        //        var response1 = new { errors = errors };
        //        response.Message = String.Join(",", response1.errors);
        //        response.Code = -4;
        //        return Json(response, JsonRequestBehavior.AllowGet);
        //    }
        //    BaseResultModel res = new BaseResultModel
        //    {
        //        Code = -1
        //    };

        //    string url = string.Format("{0}api/Offer/InsertLead?", Constants.WebAPIUrl);

        //    response = WebRequestManager.HttpPost(url, req);
        //    if (response.Code == 1)
        //    {
        //        var reqRes = JsonConvert.DeserializeObject<BaseResultModel>(response.Message);
        //        res = reqRes;
        //    }
        //    else
        //    {
        //        res = response;
        //    }
        //    return Json(res, JsonRequestBehavior.AllowGet);
        //}
        //[Authorize]
        //public ActionResult Settings()
        //{
        //    var user = GetCurrentUser();
        //    return View(user);
        //}
        //[Authorize]
        //public ActionResult EditUser()
        //{
        //    var user = GetCurrentUser();
        //    return View(user);
        //}
        //[Authorize]
        //[HttpPost]
        //public ActionResult EditUser(EditUserBindingModel req)
        //{
        //    var user = GetCurrentUser();
        //    var response = new BaseResultModel();
        //    if (!ModelState.IsValid)
        //    {
        //        return View(user);
        //    }

        //    BaseResultModel res = new BaseResultModel
        //    {
        //        Code = -1
        //    };

        //    string url = string.Format("{0}api/User/EditUser?", Constants.WebAPIUrl);

        //    response = WebRequestManager.HttpPost(url, req);
        //    if (response.Code == 1)
        //    {
        //        var reqRes = JsonConvert.DeserializeObject<BaseResultModel>(response.Message);
        //        res = reqRes;
        //    }
        //    else
        //    {
        //        res = response;
        //    }
        //    if (res.Code == 1)
        //    {
        //        TempData["Message"] = "Successfully edited user data.";
        //        user = GetCurrentUser();
        //    }
        //    else
        //    {
        //        TempData["Message"] = "Error in editing user data";
        //    }
        //    return View(user);
        //}
        //[Authorize]
        //[HttpGet]
        //public ActionResult ChangePassword()
        //{
        //    var userId = GetCurrentUser().User_Id;
        //    var emptyChangeUserPasswordBindingModel = new ChangeUserPasswordBindingModel
        //    {
        //        User_Id = userId
        //    };
        //    return View(emptyChangeUserPasswordBindingModel);
        //}
        //[Authorize]
        //[HttpPost]
        //public ActionResult ChangePassword(ChangeUserPasswordBindingModel req)
        //{
        //    if (req.OldPassword != req.RepeatOldPassword)
        //    {
        //        TempData["Message"] = "Error in editing user password";
        //    }
        //    var response = new BaseResultModel();
        //    if (!ModelState.IsValid)
        //    {
        //        return View(req);
        //    }
        //    var user = GetCurrentUser();

        //    BaseResultModel res = new BaseResultModel
        //    {
        //        Code = -1
        //    };

        //    string url = string.Format("{0}api/User/ChangePassword?", Constants.WebAPIUrl);

        //    response = WebRequestManager.HttpPost(url, req);
        //    if (response.Code == 1)
        //    {
        //        var reqRes = JsonConvert.DeserializeObject<BaseResultModel>(response.Message);
        //        res = reqRes;
        //    }
        //    else
        //    {
        //        res = response;
        //    }
        //    if (res.Code == 1)
        //    {
        //        TempData["Message"] = "Successfully edited user password.";
        //    }
        //    else
        //    {
        //        TempData["Message"] = "Error in editing user password";
        //    }
        //    return View();
        //}

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
