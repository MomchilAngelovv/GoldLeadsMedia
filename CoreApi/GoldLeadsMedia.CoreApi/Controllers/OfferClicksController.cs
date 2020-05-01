namespace GoldLeadsMedia.CoreApi.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Services.OfferClicks;
    using GoldLeadsMedia.CoreApi.Models.InputModels.OfferClicks;

    public class OfferClicksController : ApiController
    {
        private readonly IOfferClicksService offerClicksService;

        public OfferClicksController(
            IOfferClicksService offerClicksService)
        {
            this.offerClicksService = offerClicksService;
        }

        [HttpPost]
        public async Task<ActionResult<OfferClick>> RegisterOfferClick(RegisterOfferClickInputModel inputModel)
        {
            var offerClick = await this.offerClicksService.RegisterClickAsync(inputModel);
            return offerClick;
        }
    }
}
