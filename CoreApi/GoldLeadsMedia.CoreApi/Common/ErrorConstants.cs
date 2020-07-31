namespace GoldLeadsMedia.CoreApi.Common
{
    public static class ErrorConstants
    {
        public const string CountryNotFound = "Country not found! Make sure to provide correct country name!";
        public const string LeadNotFound = "Lead not found!";
        public const string BrokerNotFound = "Broker not found!";
        public const string LeadAlreadyDeposited = "Lead already deposited!";
        public const string ClickRegistrationNotFound = "Click not found!";
        public const string LeadAlreadyExists = "Lead already exists!";
        public const string CannotDepositLeadBeforeItIsSend = "Cannot deposit lead before it is send!";
        public const string CannotDepositLeadInRealBroker = "Cannot deposit lead in broker. Only leads sent to test broker can be hand-deposited!";
    }
}
