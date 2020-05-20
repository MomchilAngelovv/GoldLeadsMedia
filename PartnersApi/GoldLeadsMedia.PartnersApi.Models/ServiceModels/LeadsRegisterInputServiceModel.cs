namespace GoldLeadsMedia.PartnersApi.Models.ServiceModels
{
    public class LeadsRegisterInputServiceModel
    {
        //Sender information
        public string AffiliateId { get; set; }
        public string OfferId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int CountryId { get; set; }
    }
}
