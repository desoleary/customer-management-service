using Castle.Core.Interceptor;
using CustomerManagement.Logging;
using CustomerManagement.Web;

namespace CustomerManagement.Infrastructure.Container.Interceptors
{
    public class SecurityLoggingInterceptor :IInterceptor
    {
        private readonly IWebOperationContext webOperationContext;

        public SecurityLoggingInterceptor(IWebOperationContext webOperationContext)
        {
            this.webOperationContext = webOperationContext;
        }

        public void Intercept(IInvocation invocation)
        {
            Log.For("SecurityLog").Info("ClientRequest {0} {1}", IncomingRequest.ClientAddress, IncomingRequest.RequestUri);
            invocation.Proceed();
        }

        private IIncomingRequest IncomingRequest
        {
            get { return webOperationContext.IncomingRequest; }
        }
    }
}