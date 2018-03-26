using System.Net;
using Castle.Core.Interceptor;
using CustomerManagement.Exceptions;
using CustomerManagement.Web;

namespace CustomerManagement.Infrastructure.Container.Interceptors
{
    public class RestExceptionInterceptor : IInterceptor
    {
        private readonly IWebOperationContext webOperationContext;

        public RestExceptionInterceptor(IWebOperationContext webOperationContext)
        {
            this.webOperationContext = webOperationContext;
        }

        public void Intercept(IInvocation invocation)
        {
            try
            {
                invocation.Proceed();
            }
            catch (RestException e)
            {
                switch (e.StatusCode)
                {
                    case HttpStatusCode.BadRequest:
                    case HttpStatusCode.NoContent:
                    case HttpStatusCode.NotFound:

                        OutgoingResponse.StatusCode = e.StatusCode;
                        OutgoingResponse.SuppressEntityBody = true;
                        break;

                    default:
                        throw;
                }
            }
        }

        private IOutgoingResponse OutgoingResponse
        {
            get { return webOperationContext.OutgoingResponse; }
        }
    }
}