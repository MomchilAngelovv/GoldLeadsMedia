using System;
using System.Collections.Generic;
using System.Text;

namespace GoldLeadsMedia.Web.Models.Managers
{
    public class MakePaymentInputModel
    {
        public string User_Id { get; set; }
        public decimal Price { get; set; }
        public string PayedByUser_Id { get; set; }
        public string Offer_Id { get; set; }
        public string Lead_Id { get; set; }
        public string OfferEntry_Id { get; set; }
        public string CPA { get; set; }
    }
}
