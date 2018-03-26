using System;
using System.Collections.Specialized;

namespace CustomerManagement.Web
{
    public interface IIncomingRequest
    {
        Uri BaseUri { get; }
        Uri RequestUri { get; }
        ClientAddress ClientAddress { get; }
        NameValueCollection QueryParameters { get;}
        string HttpMethod { get;}
    }
}