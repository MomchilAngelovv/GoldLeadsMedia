namespace GoldLeadsMedia.AffiliatesApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class WelcomeController : ApiController
    {
        public ActionResult<string> Welcome()
        {
            return "Welcome to affiliates api!";
        }
    }
}
