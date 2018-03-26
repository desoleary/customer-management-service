using System;
using System.Net;
using System.Xml.Linq;
using CustomerManagement.Exceptions;
using CustomerManagement.Model.Students;
using CustomerManagement.View;
using CustomerManagement.Web;

namespace CustomerManagement.Handlers
{
    public class RequestHandler : IRequestHandler
    {
        private readonly IStudentRepository repository;
        private readonly IViewEngine viewEngine;
        private readonly IWebOperationContext webOperationContext;

        public RequestHandler(IStudentRepository repository, IViewEngine viewEngine, IWebOperationContext webOperationContext)
        {
            this.repository = repository;
            this.viewEngine = viewEngine;
            this.webOperationContext = webOperationContext;
        }

        public XElement HandleRequestFor(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                if (CalledHttpMethodMatches(HttpMethod.GET))
                {
                    var student = repository.Get(id);
                    return XElement.Parse(viewEngine.Render(student));
                }

                if (CalledHttpMethodMatches(HttpMethod.DELETE))
                {
                    repository.Delete(id);
                    return XElement.Parse(viewEngine.Render(new Student {Id = Convert.ToInt32(id)}));
                }
            }

            throw new RestException(HttpStatusCode.BadRequest);
        }

        private bool CalledHttpMethodMatches(IEquatable<string> httpMethod)
        {
            return httpMethod.Equals(webOperationContext.IncomingRequest.HttpMethod);
        }

        public XElement HandleRequestFor(Student originalStudent)
        {
            if(!Equals(null, originalStudent))
            {
                var student = repository.Modify(originalStudent);
                return XElement.Parse(viewEngine.Render(student));    
            }
            
            throw new RestException(HttpStatusCode.BadRequest);
        }
    }
}