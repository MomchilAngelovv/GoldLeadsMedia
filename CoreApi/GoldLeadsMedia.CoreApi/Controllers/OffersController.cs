namespace GoldLeadsMedia.CoreApi.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Services.Offers;
    using GoldLeadsMedia.CoreApi.Models.InputModels.Offers;
    using GoldLeadsMedia.CoreApi.Models.ResponseModels.Offers;

    public class OffersController : ApiController
    {
        private readonly IOffersService offersService;

        public OffersController(
            IOffersService offersService)
        {
            this.offersService = offersService;
        }

        public ActionResult<IEnumerable<OfferResponseModel>> All()
        {
            var offers = this.offersService
                .GetAll()
                .Select(o => new OfferResponseModel
                {
                    Id = o.Id,
                    Number = o.Number,
                    Name = o.Name,
                    Country = o.Country.Name,
                    TargetDevice = o.TargetDevice.Name,
                    EarningPerClick = o.EarningPerClick,
                    Language = o.Language.Name,
                    PaymentType = o.PaymentType.Name,
                    PayOut = o.PayOut,
                    Vertical = o.Vertical.Name,
                    Access = o.Access.Name
                })
                .ToList();

            return offers;
        }
        [HttpPost]
        public async Task<ActionResult<Offer>> Create(CreateInputModel inputModel)
        {
            var createdOffer = await this.offersService.CreateAsync(inputModel);
            return createdOffer;
        }
        [HttpGet("{id}")]
        public ActionResult<DetailsResponseModel> Details(string id)
        {
            var offer = this.offersService.GetBy(id);

            if (offer == null)
            {
                return this.NotFound();
            }

            var offerResponseModel = new DetailsResponseModel
            {
                Id = offer.Id,
                Name = offer.Name,
                Access = offer.Access.Name,
                Device = offer.TargetDevice.Name,
                Country = offer.Country.Name,
                Vertical = offer.Vertical.Name,
                Number = offer.Number,
                Language = offer.Language.Name,
                PaymentType = offer.PaymentType.Name,
                EarningPerClick = offer.EarningPerClick,
                PayOut = offer.PayOut,
                Description = offer.Description,
                LandingPages = offer.OffersLandingPages
                    .Select(olp => new DetailsLandingPage
                    {
                        Id = olp.LandingPage.Id,
                        Name = olp.LandingPage.Name
                    })
            };

            return offerResponseModel;
        }
        [HttpPost("AssignLandingPages")]
        public async Task<ActionResult<int>> AssignLandingPages(AssignLandingPagesInputModel inputModel)
        {
            var assignedLandingPagesCount = await this.offersService.AssignLandingPagesAsync(inputModel);
            return assignedLandingPagesCount;
        }
    }
}
