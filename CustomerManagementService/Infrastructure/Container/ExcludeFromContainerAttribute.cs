using System;

namespace CustomerManagement.Infrastructure.Container
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class ExcludeFromContainerAttribute : Attribute
    {
    }
}