using System;
using System.Net;
using Castle.Core.Interceptor;
using CustomerManagement.Logging;
using CustomerManagement.Web;

namespace CustomerManagement.Infrastructure.Container.Interceptors
{
    public class ExceptionSuppressionAndLoggingInterceptor : IInterceptor
    {
        private readonly IWebOperationContext webOperationContext;

        public ExceptionSuppressionAndLoggingInterceptor(IWebOperationContext webOperationContext)
        {
            this.webOperationContext = webOperationContext;
        }

        public void Intercept(IInvocation invocation)
        {
            try
            {
                invocation.Proceed();
            }
            catch (Exception exception)
            {
                OutgoingResponse.StatusCode = HttpStatusCode.InternalServerError;
                OutgoingResponse.SuppressEntityBody = true;
                Log.For(this).Exception("Exception", exception);
            }
        }

        private IOutgoingResponse OutgoingResponse
        {
            get { return webOperationContext.OutgoingResponse; }
        }
    }
}