using System.Net;

namespace CustomerManagement.Web
{
    public interface IOutgoingResponse
    {
        HttpStatusCode StatusCode { get; set; }
        bool SuppressEntityBody { get; set; }
        string ContentType { get; set; }
    }
}