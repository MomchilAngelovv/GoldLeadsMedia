namespace GoldLeadsMedia.Web.Models.ViewModels
{
    using System.Collections.Generic;

    public class LeadsAllViewModel
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<LeadsAllLead> Leads { get; set; }
        public IEnumerable<LeadsAllBroker> Brokers { get; set; }
    }
}
