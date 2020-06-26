namespace GoldLeadsMedia.Web.Infrastructure.Exceptions
{
    using System;

    public class HttpFailRequestException : Exception
    {
        public HttpFailRequestException(string message) 
            : base(message)
        {

        }
    }
}
