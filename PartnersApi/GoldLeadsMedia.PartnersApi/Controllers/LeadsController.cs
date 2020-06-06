namespace GoldLeadsMedia.PartnersApi.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;

    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.PartnersApi.Models.InputModels;
    using GoldLeadsMedia.PartnersApi.Models.ServiceModels;
    using GoldLeadsMedia.PartnersApi.Services.Application.Common;

    public class LeadsController : ApiController
    {
        private readonly ILeadsService leadsService;
        private readonly ICountriesService countriesService;
        private readonly IOffersService offersService;
        private readonly UserManager<GoldLeadsMediaUser> userManager;

        public LeadsController(
            ILeadsService leadsService,
            ICountriesService countriesService,
            IOffersService offersService,
            UserManager<GoldLeadsMediaUser> userManager)
        {
            this.leadsService = leadsService;
            this.countriesService = countriesService;
            this.offersService = offersService;
            this.userManager = userManager;
        }

        [HttpGet("Welcome")]
        public ActionResult<string> Get()
        {
            var greetingMessage = "Welcome to partners Api!";
            return greetingMessage;
        }

        //TODO MAKE GLOBAL CONSTANTS FOR ERROR MESSAGES AND OTHER STUFF
        [HttpPost]
        public async Task<ActionResult<object>> Register(LeadsRegisterInputModel inputModel)
        {
            var country = this.countriesService.GetBy(inputModel.CountryName);
            if (country == null)
            {
                return this.BadRequest("Invalid country name! Make sure to provide correct country name!");
            }

            var affiliate = await this.userManager.FindByIdAsync(inputModel.AffiliateId);
            if (affiliate == null)
            {
                return this.BadRequest("Invalid affiliateId!");
            }

            var offerExists = this.offersService.ExistsCheckBy(inputModel.OfferId);
            if (offerExists == false)
            {
                return this.BadRequest("Invalid offerId!");
            }

            var ipAddress = this.HttpContext.Connection.RemoteIpAddress.ToString();

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

            var lead = await this.leadsService.RegisterAsync(serviceModel);

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

        [HttpGet("{affiliateId}")]
        public ActionResult<object> LeadsByAffiliateId(string affiliateId)
        {
            //TODO: PAGINATION SOME DAY !!!
            var leads = this.leadsService
                .GetAllBy(affiliateId)
                .Select(lead => new 
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
                    affiliateId,
                });

            var response = new
            {
                Leads = leads
            };

            return response;
        }
    }
}
