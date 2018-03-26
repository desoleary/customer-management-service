using System.Net;
using CustomerManagement.Infrastructure.Container;

namespace CustomerManagement.Web.Adapters
{
    [ExcludeFromContainer]
    public class WebResponse : IWebResponse
    {
        public WebResponse(HttpStatusCode statusCode, string body)
        {
            StatusCode = statusCode;
            Body = body;
        }

        public HttpStatusCode StatusCode { get; private set; }
        public string Body { get; private set; }
    }
}