namespace GoldLeadsMedia.CoreApi.Models.Services.Input
{
    public class ErrorsRegisterDeveloperErrorInputServiceModel
    {
        public string Method { get; set; }
        public string Path { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string LoggedUserId { get; set; }
        public string Information { get; set; }
    }
}
