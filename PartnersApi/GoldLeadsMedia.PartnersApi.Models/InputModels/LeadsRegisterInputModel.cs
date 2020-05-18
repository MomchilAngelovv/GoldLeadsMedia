namespace GoldLeadsMedia.PartnersApi.Models.InputModels
{
    public class LeadsRegisterInputModel
    {
        public string SenderId { get; set; }
        public string OfferId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string CountryName { get; set; }
    }
}
