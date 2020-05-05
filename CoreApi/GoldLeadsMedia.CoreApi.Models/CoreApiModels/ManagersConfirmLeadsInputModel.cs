using System;
using System.Collections.Generic;
using System.Text;

namespace GoldLeadsMedia.CoreApi.Models.InputModels
{
    public class ManagersConfirmLeadsInputModel
    {
        public string ManagerId { get; set; }
        public IEnumerable<string> LeadIds { get; set; }
    }
}
