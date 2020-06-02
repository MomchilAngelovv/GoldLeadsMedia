namespace GoldLeadsMedia.Web.Models.ViewModels
{
    public class ReportsSummaryOfferReport
    {
        public string Id { get; set; }
        public string ImageUrl => $"/images/offers/{this.Id}.jpg";
        public string Number { get; set; }
        public string Name { get; set; }
        public int ClicksCount { get; set; }
        public int LeadsCount { get; set; }
        public int FtdsCount { get; set; }
    }
}
