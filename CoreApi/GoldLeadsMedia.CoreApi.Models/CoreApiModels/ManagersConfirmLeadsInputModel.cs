namespace GoldLeadsMedia.CoreApi.Models.InputModels
{
    using System.Collections.Generic;

    public class ManagersConfirmLeadsInputModel
    {
        public string ManagerId { get; set; }
        public IEnumerable<string> LeadIds { get; set; }
    }
}
