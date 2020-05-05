namespace GoldLeadsMedia.CoreApi.Models.CoreApiModels
{
    using System.Collections.Generic;

    public class PartnersSendLeadsInputModel
    {
        public IEnumerable<string> LeadIds { get; set; }
    }
}
