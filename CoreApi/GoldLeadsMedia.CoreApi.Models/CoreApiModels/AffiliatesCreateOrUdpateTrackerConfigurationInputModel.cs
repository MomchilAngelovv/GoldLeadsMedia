using System;
using System.Collections.Generic;
using System.Text;

namespace GoldLeadsMedia.CoreApi.Models.CoreApiModels
{
    public class AffiliatesCreateOrUdpateTrackerConfigurationInputModel
    {
        public string LeadPostbackUrl { get; set; }
        public string FtdPostbackUrl { get; set; }
    }
}
