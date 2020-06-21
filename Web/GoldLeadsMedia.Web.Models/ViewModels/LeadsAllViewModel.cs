namespace GoldLeadsMedia.Web.Models.ViewModels
{
    using System.Collections.Generic;

    public class LeadsAllViewModel
    {
        public IEnumerable<LeadsAllBroker> Brokers { get; set; }
        public IEnumerable<LeadsAllLead> Leads { get; set; }
    }
}
