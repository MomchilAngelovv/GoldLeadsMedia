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

        public ActionResult<IEnumerable<object>> All([FromQuery]OffersAllFilterModel filterModel)
        {
            var filter = new OffersGetAllFilterInputServiceModel
            {
                NumberOrName = filterModel.NumberOrName,
                CountryId = filterModel.CountryId,
                VerticalId = filterModel.VerticalId,
                PayTypeId = filterModel.PayTypeId,
                TargetDeviceId = filterModel.TargetDeviceId,
                AccessId = filterModel.AccessId,
                GroupId = filterModel.GroupId
            };

            var offers = this.offersService
                .GetAll(filter)
                .Select(offer => new 
                {
                    offer.Id,
                    offer.Number,
                    offer.Name,
                    TierCountry = offer.TierCountries.Name,
                    TargetDevice = offer.TargetDevice.Name,
                    offer.PayPerAction,
                    offer.PayPerClick,
                    offer.PayPerLead,
                    Language = offer.Language.Name,
                    PayType = offer.PayType.Name,
                    offer.ActionFlow,
                    Vertical = offer.Vertical.Name,
                    Access = offer.Access.Name
                })
                .ToList();

            return offers;
        }
        [HttpPost]
        public async Task<ActionResult<Offer>> Create(OffersCreateInputModel inputModel)
        {
            var serviceModel = new OffersCreateInputServiceModel
            {
                Number = inputModel.Number,
                Name = inputModel.Name,
                Description = inputModel.Description,
                AccessId = inputModel.AccessId,
                PayPerAction = inputModel.PayPerAction,
                PayPerLead = inputModel.PayPerLead,
                PayPerClick = inputModel.PayPerClick,
                ActionFlow = inputModel.ActionFlow,
                TierCountryId = inputModel.TierCountryId,
                CreatedByManagerId = inputModel.CreatedByManagerId,
                LanguageId = inputModel.LanguageId,
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
                TierCountry = offer.TierCountries.Name,
                Vertical = offer.Vertical.Name,
                offer.Number,
                Language = offer.Language.Name,
                PayType = offer.PayType.Name,
                offer.PayPerAction,
                offer.PayPerClick,
                offer.PayPerLead,
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
            var serviceModel = new OffersAssignLandingPagesInputServiceModel
            {
                OfferId = inputModel.OfferId,
                LandingPageIds = inputModel.LandingPageIds
            };

            var assignedLandingPagesCount = await this.offersService.AssignLandingPagesAsync(serviceModel);
            return assignedLandingPagesCount;
        }
    }
}
