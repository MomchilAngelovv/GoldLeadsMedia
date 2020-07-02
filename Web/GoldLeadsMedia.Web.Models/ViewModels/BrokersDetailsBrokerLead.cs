namespace GoldLeadsMedia.Web.Models.ViewModels
{
    public class BrokersDetailsBrokerLead
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Status { get; set; }
        public bool HasBeenSend { get; set; }
        public bool HasBecomeFtd { get; set; }
    }
}
