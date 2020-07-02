using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldLeadsMedia.Web.Models.ViewModels
{
    public class AdministratorsInformationViewModel
    {
        public IEnumerable<AdministratorsInformationDeveloperError> DeveloperErrors { get; set; }
        public IEnumerable<AdministratorsInformationSendLeadError> SendLeadErrors { get; set; }
        public IEnumerable<AdministratorsInformationFtdScanError> FtdScanErrors { get; set; }
        public int DeveloperErrorsCount { get; set; }
        public int SendLeadsErrorsCount { get; set; }
        public int FtdScanErrorsCount { get; set; }
    }
}
