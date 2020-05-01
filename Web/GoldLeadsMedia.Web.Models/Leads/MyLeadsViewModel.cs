using System;
using System.Collections.Generic;
using System.Text;

namespace GoldLeadsMedia.Web.Models.Leads
{
    public class MyLeadsViewModel
    {
        public IEnumerable<MyLeadsLead> Leads { get; set; }
    }
}
