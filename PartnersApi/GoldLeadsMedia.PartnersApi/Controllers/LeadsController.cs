namespace GoldLeadsMedia.PartnersApi.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using GoldLeadsMedia.PartnersApi.HttpHelper;
    using GoldLeadsMedia.PartnersApi.Models.InputModels;
    using GoldLeadsMedia.PartnersApi.Models.CoreApiResponses;

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
            var greetingMessage = "Welcome to partners Api!";
            return greetingMessage;
        }

        [HttpPost]
        public async Task<ActionResult<object>> Register(LeadsRegisterInputModel inputModel)
        {
            var body = new
            {
                inputModel.SenderId,
                inputModel.OfferId,

                inputModel.FirstName,
                inputModel.LastName,
                inputModel.Email,
                inputModel.PhoneNumber,
                inputModel.CountryName,
            };

            var coreApiResponse = await this.httpClient.PostAsync<PostApiLeadsResponse>("Api/Leads", body);

            return coreApiResponse;
        }
    }
}
