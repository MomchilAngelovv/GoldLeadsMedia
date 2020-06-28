namespace GoldLeadsMedia.CoreApi.Models.Services.Input
{
    using System.Collections.Generic;

    public class ManagersConfirmLeadsInputServiceModel
    {
        public string ManagerId { get; set; }
        public IEnumerable<string> LeadIds { get; set; }
    }
}
