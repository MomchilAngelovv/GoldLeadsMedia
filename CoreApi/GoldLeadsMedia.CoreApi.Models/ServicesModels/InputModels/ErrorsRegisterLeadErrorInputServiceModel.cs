﻿namespace GoldLeadsMedia.CoreApi.Models.ServicesModels.InputModels
{
    public class ErrorsRegisterLeadErrorInputServiceModel
    {
        public string LeadId { get; set; }
        public string BrokerId { get; set; }
        public string ErrorMessage { get; set; }
        public string Information { get; set; }
    }
}
