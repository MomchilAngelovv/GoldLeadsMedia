using System;
using System.Collections.Generic;
using System.Text;

namespace GoldLeadsMedia.Web.Models.ViewModels
{
    public class AdministratorsInformationSendLeadError
    {
        public string Id { get; set; }
        public string Information { get; set; }
        public string Message { get; set; }
        public string Broker { get; set; }
        public string Lead { get; set; }
        public string CreatedOn { get; set; }
    }
}
