namespace CustomerManagement.Logging
{
    public interface IInvocationContext
    {
        string ClassName { get; }
        string MethodName { get; }
        string ReturnValue { get; }
    }
}