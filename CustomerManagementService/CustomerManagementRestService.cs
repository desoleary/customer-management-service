using System;
using System.Net.Security;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Xml.Linq;
using System.Xml.Serialization;
using CustomerManagement.Handlers;
using CustomerManagement.Model.Students;

namespace CustomerManagement
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
        ConcurrencyMode = ConcurrencyMode.Multiple, IncludeExceptionDetailInFaults = true)]
    public class CustomerManagementRestService : ICustomerManagementRestService
    {
        private readonly IRequestHandler requestHandler;
        private readonly ISearchRequestHandler searchRequestHandler;
        private readonly IRequestParameter requestParameter;

        public CustomerManagementRestService(IRequestHandler requestHandler, ISearchRequestHandler searchRequestHandler, IRequestParameter requestParameter)
        {
            this.requestHandler = requestHandler;
            this.searchRequestHandler = searchRequestHandler;
            this.requestParameter = requestParameter;
        }

        public virtual XElement Search()
        {
            return searchRequestHandler.HandleRequest(requestParameter);
        }

        public virtual XElement Get(string studentId)
        {
            return requestHandler.HandleRequestFor(studentId);
        }

        public virtual XElement Create(Student student)
        {
            return requestHandler.HandleRequestFor(student);
        }

        public virtual XElement Delete(string studentId)
        {
            return requestHandler.HandleRequestFor(studentId);
        }
    }
}