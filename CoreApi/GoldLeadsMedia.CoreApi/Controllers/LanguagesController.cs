namespace GoldLeadsMedia.CoreApi.Controllers
{
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using GoldLeadsMedia.CoreApi.Services.Languages;
    using GoldLeadsMedia.CoreApi.Models.ResponseModels.Languages;

    public class LanguagesController : ApiController
    {
        private readonly ILanguagesService languagesService;

        public LanguagesController(
            ILanguagesService languagesService)
        {
            this.languagesService = languagesService;
        }

        public ActionResult<IEnumerable<LanguageResponseModel>> GetAll()
        {
            var languages = this.languagesService
                .GetAll()
                .Select(language => new LanguageResponseModel
                {
                    Id = language.Id,
                    Name = language.Name
                })
                .ToList();

            return languages;
        }
    }
}
