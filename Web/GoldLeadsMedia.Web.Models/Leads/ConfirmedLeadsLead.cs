using System;
using System.Collections.Generic;
using System.Text;

namespace GoldLeadsMedia.Web.Models.Leads
{
    public class ConfirmedLeadsLead
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirtName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string Offer { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
