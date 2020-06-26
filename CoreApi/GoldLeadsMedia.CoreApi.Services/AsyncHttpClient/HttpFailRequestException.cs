using System;
using System.Collections.Generic;
using System.Text;

namespace GoldLeadsMedia.CoreApi.Services.AsyncHttpClient
{
    public class HttpFailRequestException : Exception
    {
        public HttpFailRequestException(string message)
            : base(message)
        {

        }
    }
}
