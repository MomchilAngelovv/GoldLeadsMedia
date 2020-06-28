namespace GoldLeadsMedia.CoreApi.Models.CoreApi.Input
{
    using System.Collections.Generic;

    public class PartnersSendLeadsInputModel
    {
        public IEnumerable<string> LeadIds { get; set; }
    }
}
