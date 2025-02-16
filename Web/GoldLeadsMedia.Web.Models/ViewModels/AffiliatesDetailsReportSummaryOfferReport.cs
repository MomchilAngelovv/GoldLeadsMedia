﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GoldLeadsMedia.Web.Models.ViewModels
{
    public class AffiliatesDetailsReportSummaryOfferReport
    {
        public string Id { get; set; }
        public string ImageUrl => $"/images/offers/crypto.jpg";
        public string Number { get; set; }
        public string Name { get; set; }
        public int ClicksCount { get; set; }
        public int LeadsCount { get; set; }
        public int FtdsCount { get; set; }
    }
}
