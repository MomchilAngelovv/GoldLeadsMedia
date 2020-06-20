using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace GoldLeadsMedia.CoreApi.Models.ServicesModels.InputModels
{
    public class AffiliatesCreateOrUpdateTrackerConfigurationInputServiceModel
    {
        public string AffiliateId { get; set; }
        public string LeadPostbackUrl { get; set; }
        public string FtdPostbackUrl { get; set; }
    }
}
