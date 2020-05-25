namespace GoldLeadsMedia.Web.Infrastructure.Exceptions
{
    using System;

    public class CoreApiException : Exception
    {
        public CoreApiException(string message) 
            : base(message)
        {

        }
    }
}
