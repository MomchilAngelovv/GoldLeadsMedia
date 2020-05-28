using System;
using System.Collections.Generic;

namespace GoldLeadsMedia.Web.Models.ViewModels
{
    //TODO: Implement DATE filter !!!
    public class ReportsSummaryViewModel
    {
        public string UserId { get; set; }
        //TODO: Need some thinking of how dates will be implemented
        //public string Date { get; set; }
        //public DateTime StartDate { get; set; }
        //public DateTime EndDate { get; set; }
        public IEnumerable<ReportsSummaryOfferReport> OfferReports { get; set; }
    }
}
