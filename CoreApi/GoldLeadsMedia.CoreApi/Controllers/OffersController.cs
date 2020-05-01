namespace GoldLeadsMedia.CoreApi.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Models.InputModels;
    using GoldLeadsMedia.CoreApi.Services.Application.Common;
    using GoldLeadsMedia.CoreApi.Models.ServiceModels;

    public class OffersController : ApiController
    {
        private readonly IOffersService offersService;

        public OffersController(
            IOffersService offersService)
        {
            this.offersService = offersService;
        }

        public ActionResult<IEnumerable<object>> All()
        {
            var offers = this.offersService
                .GetAll()
                .Select(offer => new 
                {
                    offer.Id,
                    offer.Number,
                    offer.Name,
                    Country = offer.Country.Name,
                    TargetDevice = offer.TargetDevice.Name,
                    offer.PayPerClick,
                    Language = offer.Language.Name,
                    PayType = offer.PayType.Name,
                    offer.PayOut,
                    Vertical = offer.Vertical.Name,
                    Access = offer.Access.Name
                })
                .ToList();

            return offers;
        }
        [HttpPost]
        public async Task<ActionResult<Offer>> Create(OffersCreateInputModel inputModel)
        {
            var serviceModel = new OffersCreateServiceModel
            {
                Number = inputModel.Number,
                Name = inputModel.Name,
                Description = inputModel.Description,
                AccessId = inputModel.AccessId,
                PayPerClick = inputModel.PayPerClick,
                ActionFlow = inputModel.ActionFlow,
                CountryId = inputModel.CountryId,
                CreatedByManagerId = inputModel.CreatedByManagerId,
                DailyCap = inputModel.DailyCap,
                LanguageId = inputModel.LanguageId,
                PayOut = inputModel.PayOut,
                PayTypeId = inputModel.PayTypeId,
                TargetDeviceId = inputModel.TargetDeviceId,
                VerticalId = inputModel.VerticalId
            };

            var createdOffer = await this.offersService.CreateAsync(serviceModel);
            return createdOffer;
        }
        [HttpGet("{id}")]
        public ActionResult<object> Details(string id)
        {
            var offer = this.offersService.GetBy(id);

            if (offer == null)
            {
                return this.NotFound();
            }

            var offerResponseModel = new 
            {
                offer.Id,
                offer.Name,
                Access = offer.Access.Name,
                Device = offer.TargetDevice.Name,
                Country = offer.Country.Name,
                Vertical = offer.Vertical.Name,
                offer.Number,
                Language = offer.Language.Name,
                PayType = offer.PayType.Name,
                offer.PayPerClick,
                offer.PayOut,
                offer.Description,
                LandingPages = offer.OffersLandingPages
                    .Select(olp => new 
                    {
                        olp.LandingPage.Id,
                        olp.LandingPage.Name
                    })
            };

            return offerResponseModel;
        }
        [HttpPost("AssignLandingPages")]
        public async Task<ActionResult<int>> AssignLandingPages(OffersAssignLandingPagesInputModel inputModel)
        {
            var assignedLandingPagesCount = await this.offersService.AssignLandingPagesAsync(inputModel);
            return assignedLandingPagesCount;
        }
    }
}
