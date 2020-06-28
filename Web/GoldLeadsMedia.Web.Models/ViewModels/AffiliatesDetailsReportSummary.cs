using System;
using System.Collections.Generic;
using System.Text;

namespace GoldLeadsMedia.Web.Models.ViewModels
{
    public class AffiliatesDetailsReportSummary
    {
        public string AffiliateName { get; set; }

        //TODO: Need some thinking of how dates will be implemented
        //public string Date { get; set; }
        //public DateTime StartDate { get; set; }
        //public DateTime EndDate { get; set; }
        public IEnumerable<AffiliatesDetailsReportSummaryOfferReport> OfferReports { get; set; }
        public int TotalClicks { get; set; }
        public int TotalLeads { get; set; }
        public int TotalFtds { get; set; }
    }
}
