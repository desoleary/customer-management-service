using System;
using System.Net;

namespace CustomerManagement.Web.Adapters
{
    public class OutgoingResponse : IOutgoingResponse
    {
        public HttpStatusCode StatusCode
        {
            get { return WebOperationContext.OutgoingResponse.StatusCode; }
            set { WebOperationContext.OutgoingResponse.StatusCode = value; }
        }

        public bool SuppressEntityBody
        {
            get { return WebOperationContext.OutgoingResponse.SuppressEntityBody; }
            set { WebOperationContext.OutgoingResponse.SuppressEntityBody = value; }
        }

        public string ContentType
        {
            get { return WebOperationContext.OutgoingResponse.ContentType; }
            set { WebOperationContext.OutgoingResponse.ContentType = value; }
        }

        private static System.ServiceModel.Web.WebOperationContext WebOperationContext
        {
            get { return System.ServiceModel.Web.WebOperationContext.Current; }
        }
    }
}