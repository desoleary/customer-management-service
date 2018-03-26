using System.Net;

namespace CustomerManagement.Web
{
    public interface IWebResponse
    {
        HttpStatusCode StatusCode { get; }
        string Body { get; }
    }
}