namespace GoldLeadsMedia.Web.Models.ViewModels
{
    public class LeadsDetailsLead
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string IsTest { get; set; }
        public string IdInBroker { get; set; }
        public string Status { get; set; }
        public string FtdBecameOn { get; set; }
        public decimal? DepositAmmount { get; set; }

        public string ClickRegistrationId { get; set; }
        public string ApiRegistrationId { get; set; }
        public string Country { get; set; }
        public string Broker { get; set; }

        public string CreatedOn { get; set; }
        public string UpdatedOn { get; set; }
    }
}
