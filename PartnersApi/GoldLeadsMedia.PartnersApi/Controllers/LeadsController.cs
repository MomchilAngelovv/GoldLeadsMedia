using Microsoft.AspNetCore.Mvc;

namespace GoldLeadsMedia.PartnersApi.Controllers
{
    public class LeadsController : ApiController
    {
        public ActionResult<string> Get()
        {
            return "Test";
        }
    }
}
