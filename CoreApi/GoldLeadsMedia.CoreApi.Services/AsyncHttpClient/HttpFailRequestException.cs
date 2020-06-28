namespace GoldLeadsMedia.CoreApi.Services.AsyncHttpClient
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
