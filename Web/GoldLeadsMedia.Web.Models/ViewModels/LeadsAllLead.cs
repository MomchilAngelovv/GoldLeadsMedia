namespace GoldLeadsMedia.Web.Models.ViewModels
{
    public class LeadsAllLead
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string CountryName { get; set; }
        public string OfferName { get; set; }
        public bool HasBeenSend { get; set; }
        public bool HasBecomeFtd { get; set; }
    }
}
