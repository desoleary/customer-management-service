using System;
using CustomerManagement.Infrastructure.Container;

namespace CustomerManagement.Exceptions
{
    [ExcludeFromContainer]
    public class TemplateResolutionException : Exception
    {
        public TemplateResolutionException(Type type)
            : base(string.Format("Failed to resolve template for object type \"{0}\"", type))
        {
        }
    }
}