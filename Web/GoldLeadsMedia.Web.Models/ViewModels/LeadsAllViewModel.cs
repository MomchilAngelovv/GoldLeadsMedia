namespace GoldLeadsMedia.Web.Models.ViewModels
{
    using System.Collections.Generic;

    public class LeadsAllViewModel
    {
        public IEnumerable<LeadsAllLead> Leads { get; set; }
        public IEnumerable<LeadsAllBroker> Brokers { get; set; }
    }
}
