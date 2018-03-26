using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using CustomerManagement.Web;

namespace CustomerManagement
{
    /// <summary>
    /// RequestParameter class intent is only to filter based on the known request parameter keys.
    /// Validation of these paramters must be handled by a separated class i.e. RequestHandler could delegate this task as appropriate.
    /// </summary>
    public class RequestParameter : IRequestParameter
    {
        private IDictionary<string, string> filteredRequestParameters;
        private readonly IWebOperationContext webOperationContext;
        private readonly IList<string> registeredRequestParametersNames;

        //TODO: Need to find a way to not be tightly coupled to RequestParameters to allow be testability
        public RequestParameter(IWebOperationContext webOperationContext)
            : this(webOperationContext, new List<string> {RequestParameterName.FirstName, RequestParameterName.LastName})
        {
        }

        private RequestParameter(IWebOperationContext webOperationContext, IList<string> registeredRequestParametersNames)
        {
            this.webOperationContext = webOperationContext;
            this.registeredRequestParametersNames = registeredRequestParametersNames;
        }

        public string FirstName
        {
            get
            {
                return FilteredRequestParameters.ContainsKey(RequestParameterName.FirstName)
                           ? filteredRequestParameters[RequestParameterName.FirstName]
                           : string.Empty;
            }
        }

        public string LastName
        {
            get
            {
                return FilteredRequestParameters.ContainsKey(RequestParameterName.LastName)
                           ? filteredRequestParameters[RequestParameterName.LastName]
                           : string.Empty;
            }
        }

        private IDictionary<string, string> FilteredRequestParameters
        {
            get
            {
                if(filteredRequestParameters == null)
                {
                    filteredRequestParameters = FilterUnexpectedRequestParametersFrom(webOperationContext.IncomingRequest.QueryParameters); 
                }

                return filteredRequestParameters;
            }
        }

        private IDictionary<string, string> FilterUnexpectedRequestParametersFrom(NameValueCollection queryParameters)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (var requestParameterKey in queryParameters.AllKeys)
            {
                foreach (var registeredKey in registeredRequestParametersNames)
                {
                    if (registeredKey.Equals(requestParameterKey, StringComparison.OrdinalIgnoreCase))
                    {
                        dictionary.Add(registeredKey, queryParameters[requestParameterKey]);
                    }
                }                
            }

            return dictionary;
        }
    }

    public interface IRequestParameter
    {
        string FirstName { get; }
        string LastName { get; }
    }
}