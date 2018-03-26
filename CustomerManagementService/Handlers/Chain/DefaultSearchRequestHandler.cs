using System;
using System.Net;
using System.Xml.Linq;
using CustomerManagement.Exceptions;

namespace CustomerManagement.Handlers.Chain
{
    public class DefaultSearchRequestHandler : ISearchRequestHandler
    {
        public XElement HandleRequest(IRequestParameter requestParameter)
        {
            throw new RestException(HttpStatusCode.BadRequest);
        }
    }
}