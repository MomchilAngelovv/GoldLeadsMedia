namespace GoldLeadsMedia.CoreApi.Models.InputModels
{
    public class LeadsRegisterInputModel
    {
        //SenderId and OfferId comes only from API lead register
        public string SenderId { get; set; }
        public string OfferId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string CountryName { get; set; }

        //ClickId comes only from landing pages register
        public string ClickId { get; set; }
    }
}
