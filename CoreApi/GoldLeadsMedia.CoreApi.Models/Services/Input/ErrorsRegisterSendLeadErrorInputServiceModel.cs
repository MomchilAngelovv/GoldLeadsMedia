namespace GoldLeadsMedia.CoreApi.Models.Services.Input
{
    public class ErrorsRegisterSendLeadErrorInputServiceModel
    {
        public string LeadId { get; set; }
        public string BrokerId { get; set; }
        public string ErrorMessage { get; set; }
        public string Information { get; set; }
    }
}
