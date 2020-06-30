namespace GoldLeadsMedia.AffiliatesApi.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;

    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.AffiliatesApi.Common;
    using GoldLeadsMedia.AffiliatesApi.Services.Common;
    using GoldLeadsMedia.AffiliatesApi.Models.AffiliatesApi.Input;
    using GoldLeadsMedia.AffiliatesApi.Models.Services.Input;

    public class LeadsController : ApiController
    {
        private readonly UserManager<GoldLeadsMediaUser> userManager;

        private readonly ILeadsService leadsService;
        private readonly ICountriesService countriesService;
        private readonly IOffersService offersService;

        public LeadsController(
            UserManager<GoldLeadsMediaUser> userManager,
            ILeadsService leadsService,
            ICountriesService countriesService,
            IOffersService offersService)
        {
            this.userManager = userManager;
            this.leadsService = leadsService;
            this.countriesService = countriesService;
            this.offersService = offersService;
        }

        [HttpGet("Welcome")]
        public ActionResult<string> Get()
        {
            var greetingMessage = "Welcome to partners Api!";
            return greetingMessage;
        }


        [HttpPost]
        public async Task<ActionResult<object>> Register(LeadsRegisterInputModel inputModel)
        {
            var country = countriesService.GetBy(inputModel.CountryName);
            if (country == null)
            {
                return BadRequest(ErrorMessages.InvalidCountryName);
            }

            var affiliate = await userManager.FindByIdAsync(inputModel.AffiliateId);
            if (affiliate == null)
            {
                return BadRequest(ErrorMessages.AffiliateNotFound);
            }

            var offerExists = offersService.ExistsCheckBy(inputModel.OfferId);
            if (offerExists == false)
            {
                return BadRequest(ErrorMessages.OfferNotFound);
            }

            var lead = this.leadsService.GetByEmail(inputModel.Email);
            if (lead != null)
            {
                return BadRequest(ErrorMessages.LeadAlreadyExists);
            }

            var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();

            var serviceModel = new LeadsRegisterInputServiceModel
            {
                AffiliateId = inputModel.AffiliateId,
                OfferId = inputModel.OfferId,
                FirstName = inputModel.FirstName,
                LastName = inputModel.LastName,
                Password = inputModel.Password,
                Email = inputModel.Email,
                PhoneNumber = inputModel.PhoneNumber,
                CountryId = country.Id,
                IpAddress = ipAddress
            };

            lead = await leadsService.RegisterAsync(serviceModel);

            var response = new
            {
                lead.Id,
                lead.FirstName,
                lead.LastName,
                lead.Password,
                lead.Country.PhonePrefix,
                lead.PhoneNumber,
                lead.Email,
                CountryName = lead.Country.Name,
                lead.FtdBecameOn,
                lead.CallStatus,
                lead.CreatedOn,
                inputModel.AffiliateId,
            };

            return response;
        }
    }
}
