namespace GoldLeadsMedia.CoreApi.Controllers
{
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;
    using GoldLeadsMedia.CoreApi.Services.Common;

    public class LanguagesController : ApiController
    {
        private readonly ILanguagesService languagesService;

        public LanguagesController(
            ILanguagesService languagesService)
        {
            this.languagesService = languagesService;
        }


        [HttpGet]
        public ActionResult<IEnumerable<object>> GetAll()
        {
            var languages = this.languagesService
                .GetAll()
                .Select(language => new 
                {
                    language.Id,
                    language.Name
                })
                .ToList();

            return languages;
        }
    }
}
