using Castle.Core.Interceptor;
using CustomerManagement.Logging;

namespace CustomerManagement.Infrastructure.Container.Interceptors
{
    /// <summary>
    /// Intercepts and logs a method invocation of a registered component before and after
    /// it is invoked.
    /// </summary>

    public class TraceLoggingInterceptor : IInterceptor
    {
        /// <summary>
        /// Called by castle when a method to which this interceptor is registered to
        /// is invoked.
        /// </summary>
        /// <param name="invocation">The method invocation context.</param>
        public void Intercept(IInvocation invocation)
        {
            Log.For(this).Trace("Entering: {0}", GetInvocationContext(invocation));

            try
            {
                invocation.Proceed();
            }
            finally 
            {
                Log.For(this).Trace(new InvocationFormatProvider(), "Exiting: {0:R}", GetInvocationContext(invocation));
            }                                  
        }

        /// <summary>
        /// Returns the invocation context.
        /// </summary>
        /// <param name="invocation">The invocation context.</param>
        /// <returns>An invocation context.</returns>
        protected virtual IInvocationContext GetInvocationContext(IInvocation invocation)
        {
            return new InvocationAdapter(invocation);
        }
    }
}