using Castle.Core.Interceptor;
using CustomerManagement.Infrastructure.Container;

namespace CustomerManagement.Logging
{
    [ExcludeFromContainer]
    public class InvocationAdapter : IInvocationContext
    {
        private readonly IInvocation invocation;

        public InvocationAdapter(IInvocation invocation)
        {
            this.invocation = invocation;
        }

        public string ClassName
        {
            get { return invocation.TargetType.UnderlyingSystemType.Name; }
        }

        public string MethodName
        {
            get { return invocation.Method.Name; }
        }

        public string ReturnValue
        {
            get
            {
                return invocation.ReturnValue != null
                           ? invocation.ReturnValue.ToString()
                           : "Return value is null or empty";
            }
        }

        public override string ToString()
        {
            return string.Format("{0}.{1}", ClassName, MethodName);
        }
    }
}