namespace GoldLeadsMedia.CoreApi.Models.ServiceModels
{
    public class LeadsRegisterInputServiceModel
    {
        public string SenderId { get; set; }
        public string OfferId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int CountryId { get; set; }
        public string ClickId { get; set; }
    }
}
