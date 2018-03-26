using System;
using System.Collections.Specialized;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace CustomerManagement.Web.Adapters
{
    public class IncomingRequest : IIncomingRequest
    {
        public Uri RequestUri
        {
            get { return WebOperationContext.IncomingRequest.UriTemplateMatch.RequestUri; }
        }
        
        public Uri BaseUri
        {
            get { return WebOperationContext.IncomingRequest.UriTemplateMatch.BaseUri; }
        }

        public ClientAddress ClientAddress
        {
            get { return new ClientAddress(RemoteEndpoint.Address, RemoteEndpoint.Port); }
        }

        public NameValueCollection QueryParameters
        {
            get { return WebOperationContext.IncomingRequest.UriTemplateMatch.QueryParameters; }
        }

        public string HttpMethod
        {
            get { return WebOperationContext.IncomingRequest.Method; }
        }

        private static RemoteEndpointMessageProperty RemoteEndpoint
        {
            get { return OperationContext.Current.RemoteEndpoint(); }
        }

        private static System.ServiceModel.Web.WebOperationContext WebOperationContext
        {
            get { return System.ServiceModel.Web.WebOperationContext.Current; }
        }
    }

    public static class IncomingRequestExtensions
    {
        public static RemoteEndpointMessageProperty RemoteEndpoint(this OperationContext context)
        {
            return (RemoteEndpointMessageProperty)context.IncomingMessageProperties[RemoteEndpointMessageProperty.Name];
        }
    }
}