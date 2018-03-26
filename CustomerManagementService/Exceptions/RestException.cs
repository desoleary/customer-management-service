using System;
using System.Net;
using CustomerManagement.Infrastructure.Container;

namespace CustomerManagement.Exceptions
{
    [ExcludeFromContainer]
    public class RestException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }

        public RestException(HttpStatusCode statusCode) : base(string.Format("Failing status code of '{0}' received.", statusCode))
        {
            StatusCode = statusCode;
        }
    }
}