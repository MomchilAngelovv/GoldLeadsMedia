namespace GoldLeadsMedia.Web.Models.ViewModels
{
    using System;

    public class ManagersConfirmedLeadsLead
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string CountryName { get; set; }
        public string OfferName { get; set; }
        public DateTime ApprovedOn { get; set; }
    }
}
