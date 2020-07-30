namespace GoldLeadsMedia.CoreApi.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Models.CoreApi.Input;
    using GoldLeadsMedia.CoreApi.Services.Common;
    using GoldLeadsMedia.CoreApi.Models.Services.Input;

    public class OffersController : ApiController
    {
        private readonly IOffersService offersService;
        private readonly IAffiliatesService affiliatesService;

        public OffersController(
            IOffersService offersService,
            IAffiliatesService affiliatesService)
        {
            this.offersService = offersService;
            this.affiliatesService = affiliatesService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<object>> All([FromQuery]OffersAllFilterModel filterModel)
        {
            var filter = new OffersGetAllFilterInputServiceModel
            {
                Name = filterModel.Name,
                CountryTierId = filterModel.CountryTierId,
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
                    CountryTier = offer.CountryTier.Name,
                    TargetDevice = offer.TargetDevice.Name,
                    PayPerAction = offer.PayPerAction.GetValueOrDefault().ToString("C0"),
                    PayPerClick = offer.PayPerClick.GetValueOrDefault().ToString("C0"),
                    PayPerLead = offer.PayPerLead.GetValueOrDefault().ToString("C0"),
                    Language = offer.Language.Name,
                    PayType = offer.PayType.Name,
                    offer.ActionFlow,
                    Vertical = offer.Vertical.Name,
                    Access = offer.Access.Name
                })
                .ToList();

            return offers;
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
                CountryTier = offer.CountryTier.Name,
                Vertical = offer.Vertical.Name,
                offer.Number,
                Language = offer.Language.Name,
                PayType = offer.PayType.Name,
                PayPerAction = offer.PayPerAction.GetValueOrDefault().ToString("C0"),
                PayPerClick = offer.PayPerClick.GetValueOrDefault().ToString("C0"),
                PayPerLead = offer.PayPerLead.GetValueOrDefault().ToString("C0"),
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
                CountryTierId = inputModel.CountryTierId,
                CreatedByManagerId = inputModel.CreatedByManagerId,
                LanguageId = inputModel.LanguageId,
                PayTypeId = inputModel.PayTypeId,
                TargetDeviceId = inputModel.TargetDeviceId,
                VerticalId = inputModel.VerticalId
            };

            var createdOffer = await this.offersService.CreateAsync(serviceModel);
            return createdOffer;
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
