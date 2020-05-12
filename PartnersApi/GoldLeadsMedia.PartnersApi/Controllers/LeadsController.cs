using GoldLeadsMedia.PartnersApi.HttpHelper;
using GoldLeadsMedia.PartnersApi.Models.InputModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GoldLeadsMedia.PartnersApi.Controllers
{
    public class LeadsController : ApiController
    {
        private readonly IAsyncHttpClient httpClient;

        public LeadsController(
            IAsyncHttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public ActionResult<string> Get()
        {
            return "Test";
        }

        [HttpPost]
        public async Task<ActionResult<object>> Register(LeadsRegisterInputModel inputModel)
        {
            var body = new
            {
                inputModel.FirstName,
                inputModel.LastName,
                inputModel.Password,
                inputModel.Email,
                inputModel.PhoneNumber,
                inputModel.CountryName,
                inputModel.ClickId
            };

            var coreApiResponse = await this.httpClient.PostAsync<int>("Api/Leads", body);

            var response = new
            {
                TestResp = 1
            };

            return response;
        }
    }
}
