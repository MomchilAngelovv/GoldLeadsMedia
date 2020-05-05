using System;
using System.Collections.Generic;
using System.Text;

namespace GoldLeadsMedia.CoreApi.Models.ServiceModels
{
    public class ManagersConfirmLeadsInputServiceModel
    {
        public string ManagerId { get; set; }
        public IEnumerable<string> LeadIds { get; set; }
    }
}
